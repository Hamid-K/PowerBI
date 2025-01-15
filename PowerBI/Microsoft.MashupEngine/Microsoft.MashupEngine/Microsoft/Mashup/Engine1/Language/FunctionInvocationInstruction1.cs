using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001734 RID: 5940
	internal sealed class FunctionInvocationInstruction1 : Instruction
	{
		// Token: 0x0600971C RID: 38684 RVA: 0x001F50C8 File Offset: 0x001F32C8
		public FunctionInvocationInstruction1(FunctionValue function, Instruction arg0)
		{
			this.function = function;
			this.arg0 = arg0;
		}

		// Token: 0x0600971D RID: 38685 RVA: 0x001F50DE File Offset: 0x001F32DE
		public override Value Execute(Value frame)
		{
			return this.function.Invoke(this.arg0.Execute(frame));
		}

		// Token: 0x0600971E RID: 38686 RVA: 0x001F50F7 File Offset: 0x001F32F7
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x0600971F RID: 38687 RVA: 0x001F5110 File Offset: 0x001F3310
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x06009720 RID: 38688 RVA: 0x001F5129 File Offset: 0x001F3329
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x06009721 RID: 38689 RVA: 0x001F5142 File Offset: 0x001F3342
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame));
		}

		// Token: 0x04005031 RID: 20529
		private readonly FunctionValue function;

		// Token: 0x04005032 RID: 20530
		private readonly Instruction arg0;
	}
}
