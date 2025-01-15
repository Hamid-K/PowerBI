using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001736 RID: 5942
	internal sealed class InstructionInvocationInstruction0 : Instruction
	{
		// Token: 0x06009728 RID: 38696 RVA: 0x001F5231 File Offset: 0x001F3431
		public InstructionInvocationInstruction0(Instruction function)
		{
			this.function = function;
		}

		// Token: 0x06009729 RID: 38697 RVA: 0x001F5240 File Offset: 0x001F3440
		public override Value Execute(Value frame)
		{
			return this.function.Execute(frame).AsFunction.Invoke();
		}

		// Token: 0x0600972A RID: 38698 RVA: 0x001F5258 File Offset: 0x001F3458
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke();
		}

		// Token: 0x0600972B RID: 38699 RVA: 0x001F5270 File Offset: 0x001F3470
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke();
		}

		// Token: 0x0600972C RID: 38700 RVA: 0x001F5288 File Offset: 0x001F3488
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke();
		}

		// Token: 0x0600972D RID: 38701 RVA: 0x001F52A0 File Offset: 0x001F34A0
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke();
		}

		// Token: 0x04005036 RID: 20534
		private readonly Instruction function;
	}
}
