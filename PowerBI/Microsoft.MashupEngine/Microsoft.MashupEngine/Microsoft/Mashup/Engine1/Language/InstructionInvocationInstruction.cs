using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001739 RID: 5945
	internal sealed class InstructionInvocationInstruction : Instruction
	{
		// Token: 0x0600973A RID: 38714 RVA: 0x001F548F File Offset: 0x001F368F
		public InstructionInvocationInstruction(Instruction function, Instruction[] arguments)
		{
			this.function = function;
			this.arguments = arguments;
		}

		// Token: 0x0600973B RID: 38715 RVA: 0x001F54A5 File Offset: 0x001F36A5
		public override Value Execute(Value frame)
		{
			return this.function.Execute(frame).AsFunction.Invoke(Instruction.Execute(frame, this.arguments));
		}

		// Token: 0x0600973C RID: 38716 RVA: 0x001F54C9 File Offset: 0x001F36C9
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(Instruction.Execute(ref frame, this.arguments));
		}

		// Token: 0x0600973D RID: 38717 RVA: 0x001F54ED File Offset: 0x001F36ED
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(Instruction.Execute(ref frame, this.arguments));
		}

		// Token: 0x0600973E RID: 38718 RVA: 0x001F5511 File Offset: 0x001F3711
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(Instruction.Execute(ref frame, this.arguments));
		}

		// Token: 0x0600973F RID: 38719 RVA: 0x001F5535 File Offset: 0x001F3735
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(Instruction.Execute(ref frame, this.arguments));
		}

		// Token: 0x0400503C RID: 20540
		private readonly Instruction function;

		// Token: 0x0400503D RID: 20541
		private readonly Instruction[] arguments;
	}
}
