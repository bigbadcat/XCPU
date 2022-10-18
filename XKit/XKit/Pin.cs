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
        public const int REG_MDR = 0B01011;     //内存数据寄存器
        public const int REG_SRC = 0B01100;     //源操作数寄存器
        public const int REG_DST = 0B01101;     //目的操作数寄存器
        public const int REG_IRL = 0B01110;     //指令寄存器低8位
        public const int REG_IRH = 0B01111;     //指令寄存器高8位

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

        public const int AM_INS = 0;            //立即寻址
        public const int AM_REG = 1;            //寄存器寻址
        public const int AM_MEM = 2;            //间接寻址
        public const int AM_REG_MEM = 3;        //寄存器间接寻址

        public const int AI_NOP = 0B000000000000;       //无操作
        public const int AI_HLT = 0B000000010000;       //停机
        public const int AI_RET = 0B000000100000;       //函数返回

        public const int AI_JMP = 0B010000000000;       //无条件跳转
        public const int AI_JG = 0B010001000000;        //大于跳转
        public const int AI_JGE = 0B010010000000;       //大于等于跳转
        public const int AI_JE = 0B010011000000;        //等于跳转
        public const int AI_JNE = 0B010100000000;       //不等于跳转
        public const int AI_JL = 0B010101000000;        //小于跳转
        public const int AI_JLE = 0B010110000000;       //小于等于跳转
        public const int AI_PUSH = 0B010111000000;      //数据入栈
        public const int AI_POP = 0B011000000000;       //数据出栈
        public const int AI_WL = 0B011001000000;        //字长获取
        public const int AI_CALL = 0B011001000000;      //函数调用

        public const int AI_MOV = 0B100000000000;       //数据移动
        public const int AI_ADD = 0B100000010000;       //加法
        public const int AI_SUB = 0B100000100000;       //减法
        public const int AI_MUL = 0B100000110000;       //无符号乘法
        public const int AI_IMUL = 0B100001000000;      //有符号乘法
        public const int AI_DIV = 0B100001010000;       //无符号除法
        public const int AI_IDIV = 0B100001100000;      //有符号除法
        public const int AI_MOD = 0B100001110000;       //无符号取余
        public const int AI_IMOD = 0B100010000000;      //有符号取余
        public const int AI_AND = 0B100010010000;       //与
        public const int AI_OR = 0B100010100000;        //或
        public const int AI_NOT = 0B100010110000;       //非
        public const int AI_XOR = 0B100011000000;       //异或
        public const int AI_CMP = 0B100011010000;       //无符号比较
        public const int AI_ICMP = 0B100011100000;      //有符号比较
        public const int AI_PSET = 0B100011110000;      //状态字设置
    }
}
