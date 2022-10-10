using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XKit
{
    /// <summary>
    /// 编译异常。
    /// </summary>
    public class CompileException : Exception
    {
        public CompileException(int ecode, int line, string message) : base(message)
        {
            ErrorCode = ecode;
            Line = line;
        }

        /// <summary>
        /// 错误码。(从0开始)
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// 行索引。(从0开始)
        /// </summary>
        public int Line { get; private set; }
    }

    /// <summary>
    /// 指令。
    /// </summary>
    public class Instruct
    {
        /// <summary>
        /// 获取命令值。
        /// </summary>
        /// <param name="name">命令名称。</param>
        /// <returns>命令值。</returns>
        public static int GetCommand(string name)
        {
            if (m_NameToCommand == null)
            {
                m_NameToCommand = new Dictionary<string, int>();
                m_NameToCommand.Add("NOP", Pin.AI_NOP);
                m_NameToCommand.Add("HLT", Pin.AI_HLT);
                m_NameToCommand.Add("RET", Pin.AI_RET);

                m_NameToCommand.Add("JMP", Pin.AI_JMP);
                m_NameToCommand.Add("JG", Pin.AI_JG);
                m_NameToCommand.Add("JGE", Pin.AI_JGE);
                m_NameToCommand.Add("JE", Pin.AI_JE);
                m_NameToCommand.Add("JNE", Pin.AI_JNE);
                m_NameToCommand.Add("JL", Pin.AI_JL);
                m_NameToCommand.Add("JLE", Pin.AI_JLE);
                m_NameToCommand.Add("PUSH", Pin.AI_PUSH);
                m_NameToCommand.Add("POP", Pin.AI_POP);
                m_NameToCommand.Add("CALL", Pin.AI_CALL);

                m_NameToCommand.Add("MOV", Pin.AI_MOV);
                m_NameToCommand.Add("ADD", Pin.AI_ADD);
                m_NameToCommand.Add("SUB", Pin.AI_SUB);
                m_NameToCommand.Add("MUL", Pin.AI_MUL);
                m_NameToCommand.Add("IMUL", Pin.AI_IMUL);
                m_NameToCommand.Add("DIV", Pin.AI_DIV);
                m_NameToCommand.Add("IDIV", Pin.AI_IDIV);
                m_NameToCommand.Add("MOD", Pin.AI_MOD);
                m_NameToCommand.Add("IMOD", Pin.AI_IMOD);
                m_NameToCommand.Add("AND", Pin.AI_AND);
                m_NameToCommand.Add("OR", Pin.AI_OR);
                m_NameToCommand.Add("NOT", Pin.AI_NOT);
                m_NameToCommand.Add("XOR", Pin.AI_XOR);
                m_NameToCommand.Add("CMP", Pin.AI_CMP);
                m_NameToCommand.Add("ICMP", Pin.AI_ICMP);
                m_NameToCommand.Add("PSET", Pin.AI_PSET);
            }

            int cmd = 0;
            m_NameToCommand.TryGetValue(name, out cmd);
            return cmd;
        }
        private static Dictionary<string, int> m_NameToCommand = null;

        /// <summary>
        /// 获取操作数数量。
        /// </summary>
        /// <param name="cmd">命令值。</param>
        /// <returns>操作数数量。</returns>
        public static int GetOpNumber(int cmd)
        {
            return (cmd & 0x800) != 0 ? 2 : ((cmd & 0xC00) != 0 ? 1 : 0);
        }

        /// <summary>
        /// 获取命令值。
        /// </summary>
        /// <param name="name">命令名称。</param>
        /// <returns>命令值。</returns>
        public static int GetRegister(string name)
        {
            if (m_Register == null)
            {
                m_Register = new Dictionary<string, int>();
                m_Register.Add("A", Pin.REG_A);
                m_Register.Add("B", Pin.REG_B);
                m_Register.Add("C", Pin.REG_C);
                m_Register.Add("D", Pin.REG_D);
                m_Register.Add("CS", Pin.REG_CS);
                m_Register.Add("DS", Pin.REG_DS);
                m_Register.Add("SS", Pin.REG_SS);
                m_Register.Add("SP", Pin.REG_SP);
            }

            int id = 0;
            m_Register.TryGetValue(name, out id);
            return id;
        }
        private static Dictionary<string, int> m_Register = null;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="line">行号。</param>
        /// <param name="text">代码内容。</param>
        public Instruct(int line, string text)
        {
            Line = line;
            Text = text;
        }

        /// <summary>
        /// 解析指令。
        /// </summary>
        /// <param name="get_label_value">获取标签值回调。</param>
        public void Parse(Func<string, int> get_label_value)
        {
            //指令名
            string name;
            int sp = Text.IndexOf(' ', 0);
            name = sp == -1 ? Text : Text.Substring(0, sp);
            Cmd = GetCommand(name);
            int opn = GetOpNumber(Cmd);
            if (opn == 0)
            {
                if (sp != -1)
                {
                    string msg = string.Format("{0} is a no-operand instruction.", name);
                    throw new CompileException(1, Line, msg);
                }
                return;
            }

            //操作数
            int dot = Text.IndexOf(',', sp);
            if (opn == 1)
            {
                if (dot != -1)
                {
                    string msg = string.Format("{0} is a single operand instruction.", name);
                    throw new CompileException(1, Line, msg);
                }
                string op1 = Text.Substring(sp + 1).Trim();
                int op1am, op1v;
                ParseOperand(op1, get_label_value, out op1am, out op1v);
                DstAM = op1am;
                DstValue = op1v;
            }
            else if (opn == 2)
            {
                if (dot == -1)
                {
                    string msg = string.Format("{0} is a two-operand instruction.", name);
                    throw new CompileException(1, Line, msg);
                }

                string op1 = Text.Substring(sp + 1, dot - sp - 1).Trim();
                string op2 = Text.Substring(dot + 1).Trim();
                int op1am, op1v, op2am, op2v;
                ParseOperand(op1, get_label_value, out op1am, out op1v);
                ParseOperand(op2, get_label_value, out op2am, out op2v);
                DstAM = op1am;
                DstValue = op1v;
                SrcAM = op2am;
                SrcValue = op2v;
            }
        }

        /// <summary>
        /// 解析操作数。
        /// </summary>
        /// <param name="text">操作数文本。</param>
        /// <param name="get_label_value">获取标签值回调。</param>
        /// <param name="am">寻址方式。</param>
        /// <param name="value">操作数值。</param>
        public void ParseOperand(string text, Func<string, int> get_label_value, out int am, out int value)
        {
            am = -1;
            value = 0;

            string vstr = text;
            bool is_mem = text.StartsWith("[");
            if (is_mem)
            {
                if (!text.EndsWith("]"))
                {
                    string msg = string.Format("[] Need to come in pairs.", text);
                    throw new CompileException(1, Line, msg);
                }
                vstr = text.Substring(1, text.Length - 2).Trim();
            }
            if (string.IsNullOrEmpty(vstr))
            {
                string msg = string.Format("{0} is a bad operand.", text);
                throw new CompileException(1, Line, msg);
            }

            //十六进制数字
            if (vstr.StartsWith("0x"))
            {
                if (!int.TryParse(vstr.Substring(2), NumberStyles.HexNumber, null, out value))
                {
                    string msg = string.Format("{0} is a bad hex number.", vstr);
                    throw new CompileException(1, Line, msg);
                }
                am = is_mem ? Pin.AM_MEM : Pin.AM_INS;
                return;
            }

            //寄存器或标签
            if (Compiler.IsLetterChar(vstr[0]))
            {
                //寄存器名称
                value = GetRegister(vstr);
                if (value > 0)
                {
                    am = is_mem ? Pin.AM_REG_MEM : Pin.AM_REG;
                    return;
                }

                //标签
                value = get_label_value != null ? get_label_value(vstr) : -1;
                if (value != -1)
                {
                    am = is_mem ? Pin.AM_MEM : Pin.AM_INS;
                    return;
                }

                string msg = string.Format("Label {0} not found.", vstr);
                throw new CompileException(1, Line, msg);
            }

            //十进制数
            if (!int.TryParse(vstr, out value))
            {
                string msg = string.Format("{0} is a bad number.", vstr);
                throw new CompileException(1, Line, msg);
            }
            am = is_mem ? Pin.AM_MEM : Pin.AM_INS;
        }

        /// <summary>
        /// 输出指令。
        /// </summary>
        public void Ouput()
        {
            int opn = GetOpNumber(Cmd);
            Console.Write("{0}:{1} -> ", Line, Text);
            if (opn == 0)
            {
                Console.WriteLine(Cmd);
            }
            else if (opn == 1)
            {
                Console.WriteLine("{0} {1} {2}", Cmd, DstAM, DstValue);
            }
            else if (opn == 2)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", Cmd, DstAM, DstValue, SrcAM, SrcValue);
            }
        }

        /// <summary>
        /// 生成指令数据。
        /// </summary>
        /// <param name="buffer">数据缓冲区。</param>
        /// <param name="index">数据索引。</param>
        public void Build(byte[] buffer, int index)
        {
            int opn = GetOpNumber(Cmd);
            if (opn == 0)
            {
                buffer[index] = (byte)((Cmd >> 8) & 0xFF);
                buffer[index + 1] = (byte)(Cmd & 0xFF);
                buffer[index + 2] = 0;
                buffer[index + 3] = 0;
            }
            else if (opn == 1)
            {
                int code = Cmd | (DstAM << 4);
                buffer[index] = (byte)((code >> 8) & 0xFF);
                buffer[index + 1] = (byte)(code & 0xFF);
                buffer[index + 2] = (byte)DstValue;
                buffer[index + 3] = 0;
            }
            else if (opn == 2)
            {
                int code = Cmd | (DstAM << 2) | SrcAM;
                buffer[index] = (byte)((code >> 8) & 0xFF);
                buffer[index + 1] = (byte)(code & 0xFF);
                buffer[index + 2] = (byte)DstValue;
                buffer[index + 3] = (byte)SrcValue;
            }
        }

        /// <summary>
        /// 行号。
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// 文本。
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 命令。
        /// </summary>
        public int Cmd { get; private set; } = 0;

        /// <summary>
        /// 目标操作数寻址方式。
        /// </summary>
        public int DstAM { get; private set; } = 0;

        /// <summary>
        /// 目标操作数的值。
        /// </summary>
        public int DstValue { get; private set; } = 0;

        /// <summary>
        /// 源操作数的寻址方式。
        /// </summary>
        public int SrcAM { get; private set; } = 0;

        /// <summary>
        /// 源操作数的值。
        /// </summary>
        public int SrcValue { get; private set; } = 0;
    }


    /// <summary>
    /// 汇编编译器。
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// 编译汇编程序。
        /// </summary>
        /// <param name="src">源文件。</param>
        /// <param name="dst">目标文件。</param>
        /// <param name="msg">错误消息。</param>
        /// <returns>编译结果。0表示成功。</returns>
        public static int Complie(string src, string dst, out string msg)
        {
            msg = string.Empty;
            try
            {
                //提取指令和标签
                List<Instruct> instructs = new List<Instruct>();
                Dictionary<string, int> labels = new Dictionary<string, int>();
                ReadInstructs(src, instructs, labels);

                //解析
                Func<string, int> fun = (lb) =>
                {
                    int i;
                    if (labels.TryGetValue(lb, out i))
                    {
                        return i * 4;       //一条指令占4个字节
                    }
                    return -1;
                };
                foreach (var inst in instructs)
                {
                    inst.Parse(fun);
                }

                //生成
                int ins_max = (1 << 8) >> 2;        //内存寻址8位，每条指令4个字节
                if (instructs.Count > ins_max)
                {
                    throw new CompileException(1, 0, "Program size exceeds capacity.");
                }
                byte[] buffer = new byte[instructs.Count << 2];
                for (int i = 0; i < instructs.Count; ++i)
                {
                    instructs[i].Build(buffer, i << 2);
                }
                if (File.Exists(dst))
                {
                    File.Delete(dst);
                }
                File.WriteAllBytes(dst, buffer);
            }
            catch (CompileException ce)
            {
                msg = ce.Message;
                return ce.ErrorCode;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 读取指令。
        /// </summary>
        /// <param name="src"></param>
        /// <param name="instructs"></param>
        /// <param name="labels"></param>
        public static void ReadInstructs(string src, List<Instruct> instructs, Dictionary<string, int> labels)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(src)))
            {
                int line = 0;
                while (sr.Peek() > -1)
                {
                    //获取起始内容位置
                    string line_text = sr.ReadLine();
                    string str = GetCodeLine(line_text);
                    if (string.IsNullOrEmpty(str))
                    {
                        ++line;
                        continue;
                    }
                    if (str.EndsWith(":"))
                    {
                        //标签
                        string lb = str.Substring(0, str.Length - 1);
                        if (string.IsNullOrEmpty(lb))
                        {
                            throw new CompileException(1, line, "The label name can not be empty.");
                        }
                        if (labels.ContainsKey(lb))
                        {
                            throw new CompileException(1, line, "The label name name is repeated.");
                        }
                        labels.Add(lb, instructs.Count);
                    }
                    else
                    {
                        //指令
                        instructs.Add(new Instruct(line, str));
                    }
                    ++line;
                }
            }
        }

        /// <summary>
        /// 获取代码行。
        /// </summary>
        /// <param name="line_text">原始行内容。</param>
        /// <returns>有效代码内容。</returns>
        public static string GetCodeLine(string line_text)
        {
            int start = GetParseIndex(line_text, 0);
            if (start >= line_text.Length)
            {
                //空行
                return string.Empty;
            }

            //获取结束内容位置
            int end = line_text.IndexOf(';', start);
            if (end == -1)
            {
                end = line_text.Length;
            }
            while (end > start)
            {
                if (!IsSplitChar(line_text[end - 1]))
                {
                    break;
                }
                --end;
            }
            if (end <= start)
            {
                //纯注释行
                return string.Empty;
            }

            return line_text.Substring(start, end - start).ToUpper();
        }

        /// <summary>
        /// 判定字符是否为分隔符。
        /// </summary>
        /// <param name="c">字符值。</param>
        /// <returns>是否为分隔符。</returns>
        public static bool IsSplitChar(char c)
        {
            return c == ' ' || c == '\t' || c == '\n';
        }

        /// <summary>
        /// 跳过空白字符，获取解析索引。
        /// </summary>
        /// <param name="text">文本内容。</param>
        /// <param name="index">开始判定的索引。</param>
        /// <returns>首个非空白字符索引。</returns>
        public static int GetParseIndex(string text, int index)
        {
            for (int i = index; i < text.Length; ++i)
            {
                char c = text[i];
                if (!IsSplitChar(c))
                {
                    return i;
                }
            }
            return text.Length;
        }

        /// <summary>
        /// 判定字符是否为字母。
        /// </summary>
        /// <param name="c">字符值。</param>
        /// <returns>是否为字母。</returns>
        public static bool IsLetterChar(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        /// <summary>
        /// 判定字符是否为数字。
        /// </summary>
        /// <param name="c">字符值。</param>
        /// <returns>是否为数字。</returns>
        public static bool IsNumberChar(char c)
        {
            return c >= '0' && c <= '9';
        }
    }
}
