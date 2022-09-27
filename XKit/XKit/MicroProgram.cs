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
        /// 微程序容量。(数据宽度4个字节，8个地址线)
        /// </summary>
        public const int MICRO_SIZE = 4 * (1 << 8);

        /// <summary>
        /// 微程序缓冲区。
        /// </summary>
        public static byte[] MicroBuffer = new byte[MICRO_SIZE];

        #region 引脚定义----------------------------------------------------------------

        public const int PIN_D_CS = 1 << 0;
        public const int PIN_D_W = 1 << 1;
        public const int PIN_C_CS = 1 << 2;
        public const int PIN_C_W = 1 << 3;

        public const int PIN_OP_SHIFT = 4;
        public const int PIN_OP_ADD = 1 << PIN_OP_SHIFT;
        public const int PIN_OP_SUB = 2 << PIN_OP_SHIFT;
        public const int PIN_OP_MUL = 3 << PIN_OP_SHIFT;
        public const int PIN_OP_IMUL = 4 << PIN_OP_SHIFT;
        public const int PIN_OP_DIV = 5 << PIN_OP_SHIFT;
        public const int PIN_OP_IDIV = 6 << PIN_OP_SHIFT;
        public const int PIN_OP_MOD = 7 << PIN_OP_SHIFT;
        public const int PIN_OP_IMOD = 8 << PIN_OP_SHIFT;
        public const int PIN_OP_AND = 9 << PIN_OP_SHIFT;
        public const int PIN_OP_OR = 10 << PIN_OP_SHIFT;
        public const int PIN_OP_NOT = 11 << PIN_OP_SHIFT;
        public const int PIN_OP_XOR = 12 << PIN_OP_SHIFT;
        public const int PIN_OP_CMP = 13 << PIN_OP_SHIFT;
        public const int PIN_OP_ICMP = 14 << PIN_OP_SHIFT;
        public const int PIN_OP_PSET = 15 << PIN_OP_SHIFT;

        public const int PIN_B_CS = 1 << 8;
        public const int PIN_B_W = 1 << 9;
        public const int PIN_A_CS = 1 << 10;
        public const int PIN_A_W = 1 << 11;
        public const int PIN_PC_EN = 1 << 12;
        public const int PIN_PC_W = 1 << 13;
        public const int PIN_PC_CS = 1 << 14;
        public const int PIN_MAR_CS = 1 << 15;
        public const int PIN_MAR_W = 1 << 16;
        public const int PIN_MC_CS = 1 << 17;
        public const int PIN_MC_W = 1 << 18;
        public const int PIN_HALT = 1 << 30;

        #endregion


        /// <summary>
        /// 将微程序内容生成的文件中。
        /// </summary>
        /// <param name="file">保存微程序的二进制文件。</param>
        public static void Build(string file)
        {
            int[] pin_data = new int[]
            {
                PIN_PC_CS | PIN_MAR_W | PIN_MAR_CS,
                PIN_MC_CS | PIN_A_W | PIN_A_CS,
                PIN_PC_W | PIN_PC_CS | PIN_PC_EN,
                PIN_PC_CS | PIN_MAR_W | PIN_MAR_CS,
                PIN_MC_CS | PIN_B_W | PIN_B_CS,
                PIN_OP_SUB | PIN_C_W | PIN_C_CS,
                PIN_OP_DIV | PIN_D_W | PIN_D_CS,
                PIN_PC_W | PIN_PC_CS | PIN_PC_EN,
                PIN_PC_CS | PIN_MAR_W | PIN_MAR_CS,
                PIN_MC_W | PIN_MC_CS | PIN_C_CS,
                PIN_PC_W | PIN_PC_CS | PIN_PC_EN,
                PIN_PC_CS | PIN_MAR_W | PIN_MAR_CS,
                PIN_MC_W | PIN_MC_CS | PIN_D_CS,
            };

            for (int i=0;i<pin_data.Length; ++i)
            {
                WriteUInt32(MicroBuffer, (uint)(i << 2), (uint)pin_data[i]);
            }
            int addr = 1 << 8;
            for (int i = pin_data.Length; i < addr; ++i)
            {
                WriteUInt32(MicroBuffer, (uint)(i << 2), (uint)PIN_HALT);
            }

            File.WriteAllBytes(file, MicroBuffer);
            MessageBox.Show("Build micro program finished.");
        }

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
