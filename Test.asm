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

HLT