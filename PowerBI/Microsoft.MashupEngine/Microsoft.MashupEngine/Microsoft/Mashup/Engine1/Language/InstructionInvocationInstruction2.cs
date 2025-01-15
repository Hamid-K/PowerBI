using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001738 RID: 5944
	internal sealed class InstructionInvocationInstruction2 : Instruction
	{
		// Token: 0x06009734 RID: 38708 RVA: 0x001F5382 File Offset: 0x001F3582
		public InstructionInvocationInstruction2(Instruction function, Instruction arg0, Instruction arg1)
		{
			this.function = function;
			this.arg0 = arg0;
			this.arg1 = arg1;
		}

		// Token: 0x06009735 RID: 38709 RVA: 0x001F539F File Offset: 0x001F359F
		public override Value Execute(Value frame)
		{
			return this.function.Execute(frame).AsFunction.Invoke(this.arg0.Execute(frame), this.arg1.Execute(frame));
		}

		// Token: 0x06009736 RID: 38710 RVA: 0x001F53CF File Offset: 0x001F35CF
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x06009737 RID: 38711 RVA: 0x001F53FF File Offset: 0x001F35FF
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x06009738 RID: 38712 RVA: 0x001F542F File Offset: 0x001F362F
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x06009739 RID: 38713 RVA: 0x001F545F File Offset: 0x001F365F
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.function.Execute(ref frame).AsFunction.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x04005039 RID: 20537
		private readonly Instruction function;

		// Token: 0x0400503A RID: 20538
		private readonly Instruction arg0;

		// Token: 0x0400503B RID: 20539
		private readonly Instruction arg1;
	}
}
