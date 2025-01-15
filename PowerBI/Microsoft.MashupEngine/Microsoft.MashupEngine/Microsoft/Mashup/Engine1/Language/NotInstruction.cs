using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200173D RID: 5949
	internal sealed class NotInstruction : ConditionInstruction
	{
		// Token: 0x06009753 RID: 38739 RVA: 0x001F56F6 File Offset: 0x001F38F6
		public NotInstruction(Instruction instruction)
		{
			this.instruction = instruction;
		}

		// Token: 0x06009754 RID: 38740 RVA: 0x001F5705 File Offset: 0x001F3905
		public override bool ExecuteCondition(Value frame)
		{
			return !this.instruction.ExecuteCondition(frame);
		}

		// Token: 0x06009755 RID: 38741 RVA: 0x001F5716 File Offset: 0x001F3916
		public override bool ExecuteCondition(ref MembersFrame0 frame)
		{
			return !this.instruction.ExecuteCondition(ref frame);
		}

		// Token: 0x06009756 RID: 38742 RVA: 0x001F5727 File Offset: 0x001F3927
		public override bool ExecuteCondition(ref MembersFrame1 frame)
		{
			return !this.instruction.ExecuteCondition(ref frame);
		}

		// Token: 0x06009757 RID: 38743 RVA: 0x001F5738 File Offset: 0x001F3938
		public override bool ExecuteCondition(ref MembersFrame2 frame)
		{
			return !this.instruction.ExecuteCondition(ref frame);
		}

		// Token: 0x06009758 RID: 38744 RVA: 0x001F5749 File Offset: 0x001F3949
		public override bool ExecuteCondition(ref MembersFrameN frame)
		{
			return !this.instruction.ExecuteCondition(ref frame);
		}

		// Token: 0x04005042 RID: 20546
		private readonly Instruction instruction;
	}
}
