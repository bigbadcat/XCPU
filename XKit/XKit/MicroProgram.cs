using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XKit
{
    /// <summary>
    /// 微程序。
    /// </summary>
    public class MicroProgram
    {
        /// <summary>
        /// 微程序容量。(数据宽度4个字节，16个地址线)
        /// </summary>
        public const int MICRO_SIZE = 4 * (1 << 16);

        /// <summary>
        /// 微程序缓冲区。
        /// </summary>
        public static byte[] MicroBuffer = new byte[MICRO_SIZE];

        #region 引脚定义----------------------------------------------------------------

        public const int SHIFT_REG_R = 0;       //寄存器读偏移量
        public const int SHIFT_REG_W = 5;       //寄存器写偏移量
        public const int A_R = Pin.REG_A << SHIFT_REG_R;
        public const int B_R = Pin.REG_B << SHIFT_REG_R;
        public const int C_R = Pin.REG_C << SHIFT_REG_R;
        public const int D_R = Pin.REG_D << SHIFT_REG_R;
        public const int CS_R = Pin.REG_CS << SHIFT_REG_R;
        public const int DS_R = Pin.REG_DS << SHIFT_REG_R;
        public const int SS_R = Pin.REG_SS << SHIFT_REG_R;
        public const int SP_R = Pin.REG_SP << SHIFT_REG_R;
        public const int MSR_R = Pin.REG_MSR << SHIFT_REG_R;
        public const int MAR_R = Pin.REG_MAR << SHIFT_REG_R;
        public const int MDR_R = Pin.REG_MDR << SHIFT_REG_R;
        public const int SRC_R = Pin.REG_SRC << SHIFT_REG_R;
        public const int DST_R = Pin.REG_DST << SHIFT_REG_R;
        public const int IRL_R = Pin.REG_IRL << SHIFT_REG_R;
        public const int IRH_R = Pin.REG_IRH << SHIFT_REG_R;
        public const int A_W = Pin.REG_A << SHIFT_REG_W;
        public const int B_W = Pin.REG_B << SHIFT_REG_W;
        public const int C_W = Pin.REG_C << SHIFT_REG_W;
        public const int D_W = Pin.REG_D << SHIFT_REG_W;
        public const int CS_W = Pin.REG_CS << SHIFT_REG_W;
        public const int DS_W = Pin.REG_DS << SHIFT_REG_W;
        public const int SS_W = Pin.REG_SS << SHIFT_REG_W;
        public const int SP_W = Pin.REG_SP << SHIFT_REG_W;
        public const int MSR_W = Pin.REG_MSR << SHIFT_REG_W;
        public const int MAR_W = Pin.REG_MAR << SHIFT_REG_W;
        public const int MDR_W = Pin.REG_MDR << SHIFT_REG_W;
        public const int SRC_W = Pin.REG_SRC << SHIFT_REG_W;
        public const int DST_W = Pin.REG_DST << SHIFT_REG_W;
        public const int IRL_W = Pin.REG_IRL << SHIFT_REG_W;
        public const int IRH_W = Pin.REG_IRH << SHIFT_REG_W;

        public const int S_REG_R = 1 << 10;         //源指定的寄存器读
        public const int S_REG_W = 1 << 11;         //源指定的寄存器写
        public const int D_REG_R = 1 << 12;         //目标指定的寄存器读
        public const int D_REG_W = 1 << 13;         //目标指定的寄存器写

        public const int PC_R = 1 << 14;            //PC读
        public const int PC_W = 3 << 14;            //PC写
        public const int PC_INC = 7 << 14;          //PC自增

        public const int SHIFT_OP = 17;                         //操作偏移量
        public const int OP_ADD = Pin.OP_ADD << SHIFT_OP;       //加法
        public const int OP_SUB = Pin.OP_SUB << SHIFT_OP;       //减法
        public const int OP_MUL = Pin.OP_MUL << SHIFT_OP;       //无符号乘法
        public const int OP_IMUL = Pin.OP_IMUL << SHIFT_OP;     //有符号乘法
        public const int OP_DIV = Pin.OP_DIV << SHIFT_OP;       //无符号除法
        public const int OP_IDIV = Pin.OP_IDIV << SHIFT_OP;     //有符号除法
        public const int OP_MOD = Pin.OP_MOD << SHIFT_OP;       //无符号取余
        public const int OP_IMOD = Pin.OP_IMOD << SHIFT_OP;     //有符号取余
        public const int OP_AND = Pin.OP_AND << SHIFT_OP;       //与
        public const int OP_OR = Pin.OP_OR << SHIFT_OP;         //或
        public const int OP_NOT = Pin.OP_NOT << SHIFT_OP;       //非
        public const int OP_XOR = Pin.OP_XOR << SHIFT_OP;       //异或
        public const int OP_CMP = Pin.OP_CMP << SHIFT_OP;       //无符号比较
        public const int OP_ICMP = Pin.OP_ICMP << SHIFT_OP;     //有符号比较
        public const int OP_PSET = Pin.OP_PSET << SHIFT_OP;     //状态字设置

        public const int MC_R = 1 << 21;            //内存读
        public const int MC_W = 3 << 21;            //内存写

        public const int CYC_RS = 1 << 29;          //重置微指令周期
        public const int HALT = 1 << 30;            //停止

        #endregion

        #region 指令定义----------------------------------------------------------------

        /// <summary>
        /// 取指。
        /// </summary>
        public static int[] Fetch = new int[]
        {
            CS_R | MSR_W,
            PC_R | MAR_W,
            MC_R | IRH_W | PC_INC,
            PC_R | MAR_W,
            MC_R | IRL_W | PC_INC,
            PC_R | MAR_W,
            MC_R | DST_W | PC_INC,
            PC_R | MAR_W,
            MC_R | SRC_W | PC_INC,
        };

        #region 无操作数指令

        /// <summary>
        /// 无操作。
        /// </summary>
        public static int[] NoOperate = new int[]
        {
        };

        /// <summary>
        /// 停机。
        /// </summary>
        public static int[] Halt = new int[]
        {
            HALT,
        };

        #endregion

        #region 单操作数指令

        #endregion

        #region 双操作数指令

        /// <summary>
        /// 数据转移微程序。
        /// </summary>
        public static Dictionary<int, int[]> MoveMicro
        {
            get
            {
                Dictionary<int, int[]> micro = new Dictionary<int, int[]>();
                micro.Add((Pin.AM_REG << 2) | Pin.AM_INS, new int[]
                {
                    D_REG_W | SRC_R,
                });
                micro.Add((Pin.AM_REG << 2) | Pin.AM_REG, new int[]
                {
                    D_REG_W | S_REG_R,
                });
                micro.Add((Pin.AM_REG << 2) | Pin.AM_MEM, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | SRC_R,
                    D_REG_W | MC_R,
                });
                micro.Add((Pin.AM_REG << 2) | Pin.AM_REG_MEM, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | S_REG_R,
                    D_REG_W | MC_R,
                });

                micro.Add((Pin.AM_MEM << 2) | Pin.AM_INS, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | DST_R,
                    MC_W | SRC_R,
                });
                micro.Add((Pin.AM_MEM << 2) | Pin.AM_REG, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | DST_R,
                    MC_W | S_REG_R,
                });
                micro.Add((Pin.AM_MEM << 2) | Pin.AM_MEM, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | SRC_R,
                    MDR_W | MC_R,
                    MAR_W | DST_R,
                    MC_W | MDR_R,
                });
                micro.Add((Pin.AM_MEM << 2) | Pin.AM_REG_MEM, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | S_REG_R,
                    MDR_W | MC_R,
                    MAR_W | DST_R,
                    MC_W | MDR_R,
                });

                micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_INS, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | D_REG_R,
                    MC_W | SRC_R,
                });
                micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_REG, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | D_REG_R,
                    MC_W | S_REG_R,
                });
                micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_MEM, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | SRC_R,
                    MDR_W | MC_R,
                    MAR_W | D_REG_R,
                    MC_W | MDR_R,
                });
                micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_REG_MEM, new int[]
                {
                    MSR_W | DS_R,
                    MAR_W | S_REG_R,
                    MDR_W | MC_R,
                    MAR_W | D_REG_R,
                    MC_W | MDR_R,
                });

                return micro;
            }
        }

        /// <summary>
        /// 获取运算微指令。
        /// </summary>
        /// <param name="op">运算码。</param>
        /// <returns>运算微指令。</returns>
        public static Dictionary<int, int[]> GetOPMicro(int op)
        {
            Dictionary<int, int[]> micro = new Dictionary<int, int[]>();
            micro.Add((Pin.AM_REG << 2) | Pin.AM_INS, new int[]
            {
                B_W | SRC_R,
                D_REG_W | op,
            });
            micro.Add((Pin.AM_REG << 2) | Pin.AM_REG, new int[]
            {
                B_W | S_REG_R,
                D_REG_W | op,
            });
            micro.Add((Pin.AM_REG << 2) | Pin.AM_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | SRC_R,
                B_W | MC_R,
                D_REG_W | op,
            });
            micro.Add((Pin.AM_REG << 2) | Pin.AM_REG_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | S_REG_R,
                B_W | MC_R,
                D_REG_W | op,
            });

            micro.Add((Pin.AM_MEM << 2) | Pin.AM_INS, new int[]
            {
                B_W | SRC_R,
                MSR_W | DS_R,
                MAR_W | DST_R,
                MC_W | op,
            });
            micro.Add((Pin.AM_MEM << 2) | Pin.AM_REG, new int[]
            {
                B_W | S_REG_R,
                MSR_W | DS_R,
                MAR_W | DST_R,
                MC_W | op,
            });
            micro.Add((Pin.AM_MEM << 2) | Pin.AM_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | SRC_R,
                B_W | MC_R,
                MAR_W | DST_R,
                MC_W | op,
            });
            micro.Add((Pin.AM_MEM << 2) | Pin.AM_REG_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | S_REG_R,
                B_W | MC_R,
                MAR_W | DST_R,
                MC_W | op,
            });

            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_INS, new int[]
            {
                B_W | SRC_R,
                MSR_W | DS_R,
                MAR_W | D_REG_R,
                MC_W | op,
            });
            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_REG, new int[]
            {
                B_W | S_REG_R,
                MSR_W | DS_R,
                MAR_W | D_REG_R,
                MC_W | op,
            });
            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | SRC_R,
                B_W | MC_R,
                MAR_W | D_REG_R,
                MC_W | op,
            });
            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_REG_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | S_REG_R,
                B_W | MC_R,
                MAR_W | D_REG_R,
                MC_W | op,
            });

            return micro;
        }

        /// <summary>
        /// 获取无结果运算微指令。
        /// </summary>
        /// <param name="op">运算码。</param>
        /// <returns>无结果运算微指令。</returns>
        public static Dictionary<int, int[]> GetNROPMicro(int op)
        {
            Dictionary<int, int[]> micro = new Dictionary<int, int[]>();
            micro.Add((Pin.AM_INS << 2) | Pin.AM_INS, new int[]
            {
                A_W | DST_R,
                B_W | SRC_R,
                op,
            });
            micro.Add((Pin.AM_INS << 2) | Pin.AM_REG, new int[]
            {
                A_W | DST_R,
                B_W | S_REG_R,
                op,
            });
            micro.Add((Pin.AM_INS << 2) | Pin.AM_MEM, new int[]
            {
                A_W | DST_R,
                MSR_W | DS_R,
                MAR_W | SRC_R,
                B_W | MC_R,
                op,
            });
            micro.Add((Pin.AM_INS << 2) | Pin.AM_REG_MEM, new int[]
            {
                A_W | DST_R,
                MSR_W | DS_R,
                MAR_W | S_REG_R,
                B_W | MC_R,
                op,
            });

            micro.Add((Pin.AM_REG << 2) | Pin.AM_INS, new int[]
            {
                A_W | D_REG_R,
                B_W | SRC_R,
                op,
            });
            micro.Add((Pin.AM_REG << 2) | Pin.AM_REG, new int[]
            {
                A_W | D_REG_R,
                B_W | S_REG_R,
                op,
            });
            micro.Add((Pin.AM_REG << 2) | Pin.AM_MEM, new int[]
            {
                A_W | D_REG_R,
                MSR_W | DS_R,
                MAR_W | SRC_R,
                B_W | MC_R,
                op,
            });
            micro.Add((Pin.AM_REG << 2) | Pin.AM_REG_MEM, new int[]
            {
                A_W | D_REG_R,
                MSR_W | DS_R,
                MAR_W | S_REG_R,
                B_W | MC_R,
                op,
            });

            micro.Add((Pin.AM_MEM << 2) | Pin.AM_INS, new int[]
            {
                MSR_W | DS_R,
                MAR_W | DST_R,
                A_W | MC_R,
                B_W | SRC_R,
                op,
            });
            micro.Add((Pin.AM_MEM << 2) | Pin.AM_REG, new int[]
            {
                MSR_W | DS_R,
                MAR_W | DST_R,
                A_W | MC_R,
                B_W | S_REG_R,
                op,
            });
            micro.Add((Pin.AM_MEM << 2) | Pin.AM_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | DST_R,
                A_W | MC_R,
                MAR_W | SRC_R,
                B_W | MC_R,
                op,
            });
            micro.Add((Pin.AM_MEM << 2) | Pin.AM_REG_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | DST_R,
                A_W | MC_R,
                MAR_W | S_REG_R,
                B_W | MC_R,
                op,
            });

            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_INS, new int[]
            {
                MSR_W | DS_R,
                MAR_W | D_REG_R,
                A_W | MC_R,
                B_W | SRC_R,
                op,
            });
            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_REG, new int[]
            {
                MSR_W | DS_R,
                MAR_W | D_REG_R,
                A_W | MC_R,
                B_W | S_REG_R,
                op,
            });
            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | D_REG_R,
                A_W | MC_R,
                MAR_W | SRC_R,
                B_W | MC_R,
                op,
            });
            micro.Add((Pin.AM_REG_MEM << 2) | Pin.AM_REG_MEM, new int[]
            {
                MSR_W | DS_R,
                MAR_W | D_REG_R,
                A_W | MC_R,
                MAR_W | S_REG_R,
                B_W | MC_R,
                op,
            });

            return micro;
        }

        #endregion

        #endregion


        /// <summary>
        /// 将微程序内容生成的文件中。
        /// </summary>
        /// <param name="file">保存微程序的二进制文件。</param>
        public static void Build(string file)
        {
            //int addr = MicroBuffer.Length >> 6;
            int addr = 1 << 12;
            for (int instruct = 0; instruct < addr; ++instruct)
            {
                //取指令
                int cyc = 0;
                int iaddr = instruct << 4;
                for (; cyc < Fetch.Length; ++cyc)
                {
                    WriteUInt32(MicroBuffer, (uint)((iaddr + cyc) << 2), (uint)Fetch[cyc]);
                }

                //指令执行
                int[] mp_data = null;
                if ((instruct & 0x800) != 0)
                {
                    //双操作数
                    int src = instruct & 0x3;
                    int dst = (instruct >> 2) & 0x3;
                    int cmd = instruct & 0xFF0;
                    mp_data = GetMicroInstruct2(cmd, dst, src);
                }
                else if ((instruct & 0x400) != 0)
                {
                    //单操作数
                    int psw = instruct & 0xF;
                    int dst = (instruct >> 4) & 0x3;
                    int cmd = instruct & 0xFC0;
                    mp_data = GetMicroInstruct1(cmd, dst, psw);
                }
                else
                {
                    //无操作数
                    int psw = instruct & 0xF;
                    int cmd = instruct & 0xFF0;
                    mp_data = GetMicroInstruct0(cmd, psw);
                }
                if (mp_data != null)
                {
                    for (int i = 0; i < mp_data.Length; ++i, ++cyc)
                    {
                        WriteUInt32(MicroBuffer, (uint)((iaddr + cyc) << 2), (uint)mp_data[i]);
                    }
                }

                //指令结束
                for (; cyc < 16; ++cyc)
                {
                    WriteUInt32(MicroBuffer, (uint)((iaddr + cyc) << 2), (uint)CYC_RS);
                }
            }
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.WriteAllBytes(file, MicroBuffer);            
        }

        /// <summary>
        /// 获取双操作数的指令。
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int[] GetMicroInstruct2(int cmd, int dst, int src)
        {
            if (s_TwoOperandMicro == null)
            {
                s_TwoOperandMicro = new Dictionary<int, Dictionary<int, int[]>>();
                s_TwoOperandMicro.Add(Pin.AI_MOV, MoveMicro);
                s_TwoOperandMicro.Add(Pin.AI_ADD, GetOPMicro(OP_ADD));
                s_TwoOperandMicro.Add(Pin.AI_SUB, GetOPMicro(OP_SUB));
                s_TwoOperandMicro.Add(Pin.AI_MUL, GetOPMicro(OP_MUL));
                s_TwoOperandMicro.Add(Pin.AI_IMUL, GetOPMicro(OP_IMUL));
                s_TwoOperandMicro.Add(Pin.AI_DIV, GetOPMicro(OP_DIV));
                s_TwoOperandMicro.Add(Pin.AI_IDIV, GetOPMicro(OP_IDIV));
                s_TwoOperandMicro.Add(Pin.AI_MOD, GetOPMicro(OP_MOD));
                s_TwoOperandMicro.Add(Pin.AI_IMOD, GetOPMicro(OP_IMOD));
                s_TwoOperandMicro.Add(Pin.AI_AND, GetOPMicro(OP_AND));
                s_TwoOperandMicro.Add(Pin.AI_OR, GetOPMicro(OP_OR));
                s_TwoOperandMicro.Add(Pin.AI_NOT, GetOPMicro(OP_NOT));
                s_TwoOperandMicro.Add(Pin.AI_XOR, GetOPMicro(OP_XOR));
                s_TwoOperandMicro.Add(Pin.AI_CMP, GetNROPMicro(OP_CMP));
                s_TwoOperandMicro.Add(Pin.AI_ICMP, GetNROPMicro(OP_ICMP));
                s_TwoOperandMicro.Add(Pin.AI_PSET, GetNROPMicro(OP_PSET));
            }

            Dictionary<int, int[]> cmdop;
            if (s_TwoOperandMicro.TryGetValue(cmd, out cmdop))
            {
                int[] micro;
                cmdop.TryGetValue(dst << 2 | src, out micro);
                return micro;
            }
            return null;
        }
        private static Dictionary<int, Dictionary<int, int[]>> s_TwoOperandMicro = null;

        /// <summary>
        /// 获取单操作数的指令。
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dst"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        public static int[] GetMicroInstruct1(int cmd, int dst, int psw)
        {
            return null;
        }

        /// <summary>
        /// 获取无操作数的指令。
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        public static int[] GetMicroInstruct0(int cmd, int psw)
        {
            if (s_NoOperandMicro == null)
            {
                s_NoOperandMicro = new Dictionary<int, int[]>();
                s_NoOperandMicro.Add(Pin.AI_NOP, NoOperate);
                s_NoOperandMicro.Add(Pin.AI_HLT, Halt);
            }

            int[] micro;
            s_NoOperandMicro.TryGetValue(cmd, out micro);
            return micro;
        }
        private static Dictionary<int, int[]> s_NoOperandMicro = null;

        /// <summary>
        /// 写入一个无符号整数。
        /// </summary>
        /// <param name="src">要写入的字节数组。</param>
        /// <param name="index">要写入的起始位置。</param>
        /// <param name="value">要写入的值。</param>
        /// <returns>下一个写入索引。</returns>
        public static uint WriteUInt32(byte[] src, uint index, uint value)
        {
            src[index + 3] = (byte)((value >> 24) & 0xFF);                  //最高位
            src[index + 2] = (byte)((value >> 16) & 0xFF);                  //次高位            
            src[index + 1] = (byte)((value >> 8) & 0xFF);                   //次低位
            src[index + 0] = (byte)(value & 0xFF);                          //最低位
            return index + 4;
        }
    }
}
