using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XKit
{
    /// <summary>
    /// 针脚定义。
    /// </summary>
    public class Pin
    {
        public const int REG_A = 0B00001;       //通用A
        public const int REG_B = 0B00010;       //通用B
        public const int REG_C = 0B00011;       //通用C
        public const int REG_D = 0B00100;       //通用D
        public const int REG_CS = 0B00101;      //代码段
        public const int REG_DS = 0B00110;      //数据段
        public const int REG_SS = 0B00111;      //堆栈段
        public const int REG_SP = 0B01000;      //堆栈指针
        public const int REG_MSR = 0B01001;     //内存段寄存器
        public const int REG_MAR = 0B01010;     //内存地址寄存器
        public const int REG_SRC = 0B01011;     //源操作数寄存器
        public const int REG_DST = 0B01100;     //目的操作数寄存器
        public const int REG_IRL = 0B01101;     //指令寄存器低8位
        public const int REG_IRH = 0B01110;     //指令寄存器高8位

        public const int OP_ADD = 0B0001;       //加法
        public const int OP_SUB = 0B0010;       //减法
        public const int OP_MUL = 0B0011;       //无符号乘法
        public const int OP_IMUL = 0B0100;      //有符号乘法
        public const int OP_DIV = 0B0101;       //无符号除法
        public const int OP_IDIV = 0B0110;      //有符号除法
        public const int OP_MOD = 0B0111;       //无符号取余
        public const int OP_IMOD = 0B1000;      //有符号取余
        public const int OP_AND = 0B1001;       //与
        public const int OP_OR = 0B1010;        //或
        public const int OP_NOT = 0B1011;       //非
        public const int OP_XOR = 0B1100;       //异或
        public const int OP_CMP = 0B1101;       //无符号比较
        public const int OP_ICMP = 0B1110;      //有符号比较
        public const int OP_PSET = 0B1111;      //状态字设置
    }
}
