;初始化设置
call main
hlt
	
fun:
	mov [1], 0
	mov [2], 0
loop_begin:
	cmp [2], 10
	jge loop_end
	
	mov c, [2]
	inc c
	add [1], c
	
	inc [2]
	jmp loop_begin
loop_end:
	
	ret


main:
	call fun
	ret

	
	
	