using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001733 RID: 5939
	internal sealed class FunctionInvocationInstruction0 : Instruction
	{
		// Token: 0x06009716 RID: 38678 RVA: 0x001F50AC File Offset: 0x001F32AC
		public FunctionInvocationInstruction0(FunctionValue function)
		{
			this.function = function;
		}

		// Token: 0x06009717 RID: 38679 RVA: 0x001F50BB File Offset: 0x001F32BB
		public override Value Execute(Value frame)
		{
			return this.function.Invoke();
		}

		// Token: 0x06009718 RID: 38680 RVA: 0x001F50BB File Offset: 0x001F32BB
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.function.Invoke();
		}

		// Token: 0x06009719 RID: 38681 RVA: 0x001F50BB File Offset: 0x001F32BB
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.function.Invoke();
		}

		// Token: 0x0600971A RID: 38682 RVA: 0x001F50BB File Offset: 0x001F32BB
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.function.Invoke();
		}

		// Token: 0x0600971B RID: 38683 RVA: 0x001F50BB File Offset: 0x001F32BB
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.function.Invoke();
		}

		// Token: 0x04005030 RID: 20528
		private readonly FunctionValue function;
	}
}
