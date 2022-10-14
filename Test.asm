;微程序测试
;---MOV---
;MOV A, 0x12
;MOV B, A
;MOV C, [0x0A]
;MOV D, [A]

;MOV A, 0x4
;MOV [0x40], 0x11
;MOV [0x41], A
;MOV [0x42], [0x40]
;MOV [0x43], [A]

;MOV A, 0x50
;MOV B, 0x51
;MOV C, 0x52
;MOV D, 0x53
;MOV [A], 0x33
;MOV [B], A
;MOV [C], [0x5]
;MOV [D], [C]

;---ADD---
;MOV A, 0x3
;ADD C, 0x2
;ADD D, C
;ADD C, [0x02]
;ADD D, [C]

;MOV A, 0x3
;MOV C, 0x5
;ADD [0x40], 0x2
;ADD [0x41], C
;ADD [0x42], [0x3]
;ADD [0x43], [C]

;MOV A, 0x3
;MOV C, 0x5
;MOV D, 0x50
;ADD [D], 0x2
;ADD [D], C
;ADD [D], [0x4]
;ADD [D], [C]

;---ICMP---
;MOV C, 1
;ICMP -2, -1
;ICMP -5, C
;ICMP 3, [0x2]
;ICMP 5, [C]

;MOV C, 5
;MOV D, 5
;ICMP D, -1
;ICMP D, C
;ICMP D, [0x2]
;ICMP D, [C]

;MOV C, 5
;MOV D, 2
;ICMP [0x1], 9
;ICMP [0x1], C
;ICMP [0x1], [0x2]
;ICMP [0x1], [C]

;MOV C, 5
;MOV D, 1
;ICMP [D], 9
;ICMP [D], C
;ICMP [D], [0x2]
;ICMP [D], [C]

;---JMP---
;MOV A, LB1
;JMP LB1
;JMP A
;JMP [0x3]
;MOV D, 0x3
;JMP [D]
;MOV B, 20
;LB1:
;MOV C, 30

;---JMP-C---
;CMP 1, 2
;JG LB1
;JGE LB1
;JE LB1
;JNE LB1
;JL LB1
;JLE LB1
;MOV [0x40], 1
;LB1:
;CMP 2, 2
;JG LB2
;JGE LB2
;JE LB2
;JNE LB2
;JL LB2
;JLE LB2
;MOV [0x41], 1
;LB2:
;CMP 3, 2
;JG LB3
;JGE LB3
;JE LB3
;JNE LB3
;JL LB3
;JLE LB3
;MOV [0x42], 1
;LB3:


HLT





























