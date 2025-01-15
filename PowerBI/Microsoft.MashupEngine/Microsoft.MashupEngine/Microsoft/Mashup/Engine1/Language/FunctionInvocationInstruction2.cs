using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001735 RID: 5941
	internal sealed class FunctionInvocationInstruction2 : Instruction
	{
		// Token: 0x06009722 RID: 38690 RVA: 0x001F515B File Offset: 0x001F335B
		public FunctionInvocationInstruction2(FunctionValue function, Instruction arg0, Instruction arg1)
		{
			this.function = function;
			this.arg0 = arg0;
			this.arg1 = arg1;
		}

		// Token: 0x06009723 RID: 38691 RVA: 0x001F5178 File Offset: 0x001F3378
		public override Value Execute(Value frame)
		{
			return this.function.Invoke(this.arg0.Execute(frame), this.arg1.Execute(frame));
		}

		// Token: 0x06009724 RID: 38692 RVA: 0x001F519D File Offset: 0x001F339D
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x06009725 RID: 38693 RVA: 0x001F51C2 File Offset: 0x001F33C2
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x06009726 RID: 38694 RVA: 0x001F51E7 File Offset: 0x001F33E7
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x06009727 RID: 38695 RVA: 0x001F520C File Offset: 0x001F340C
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.function.Invoke(this.arg0.Execute(ref frame), this.arg1.Execute(ref frame));
		}

		// Token: 0x04005033 RID: 20531
		private readonly FunctionValue function;

		// Token: 0x04005034 RID: 20532
		private readonly Instruction arg0;

		// Token: 0x04005035 RID: 20533
		private readonly Instruction arg1;
	}
}
