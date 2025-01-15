using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200173A RID: 5946
	internal abstract class ConditionInstruction : Instruction
	{
		// Token: 0x06009740 RID: 38720 RVA: 0x001F5559 File Offset: 0x001F3759
		public sealed override Value Execute(Value frame)
		{
			return LogicalValue.New(this.ExecuteCondition(frame));
		}

		// Token: 0x06009741 RID: 38721 RVA: 0x001F5567 File Offset: 0x001F3767
		public sealed override Value Execute(ref MembersFrame0 frame)
		{
			return LogicalValue.New(this.ExecuteCondition(ref frame));
		}

		// Token: 0x06009742 RID: 38722 RVA: 0x001F5575 File Offset: 0x001F3775
		public sealed override Value Execute(ref MembersFrame1 frame)
		{
			return LogicalValue.New(this.ExecuteCondition(ref frame));
		}

		// Token: 0x06009743 RID: 38723 RVA: 0x001F5583 File Offset: 0x001F3783
		public sealed override Value Execute(ref MembersFrame2 frame)
		{
			return LogicalValue.New(this.ExecuteCondition(ref frame));
		}

		// Token: 0x06009744 RID: 38724 RVA: 0x001F5591 File Offset: 0x001F3791
		public sealed override Value Execute(ref MembersFrameN frame)
		{
			return LogicalValue.New(this.ExecuteCondition(ref frame));
		}
	}
}
