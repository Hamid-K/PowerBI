using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001737 RID: 5943
	internal sealed class InstructionInvocationInstruction1 : Instruction
	{
		// Token: 0x0600972E RID: 38702 RVA: 0x001F52B8 File Offset: 0x001F34B8
		public InstructionInvocationInstruction1(Instruction function, Instruction arg0)
		{
			this.function = function;
			this.arg0 = arg0;
		}

		// Token: 0x0600972F RID: 38703 RVA: 0x001F52CE File Offset: 0x001F34CE
		public override Value Execute(Value frame)
		{
			return this.function.Execute(frame).AsFunction.Invoke(this.arg0.Execute(frame));
		}

		// Token: 0x06009730 RID: 38704 RVA: 0x001F52F2 File Offset: 0x001F34F2
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x06009731 RID: 38705 RVA: 0x001F5316 File Offset: 0x001F3516
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x06009732 RID: 38706 RVA: 0x001F533A File Offset: 0x001F353A
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x06009733 RID: 38707 RVA: 0x001F535E File Offset: 0x001F355E
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x04005037 RID: 20535
		private readonly Instruction function;

		// Token: 0x04005038 RID: 20536
		private readonly Instruction arg0;
	}
}
