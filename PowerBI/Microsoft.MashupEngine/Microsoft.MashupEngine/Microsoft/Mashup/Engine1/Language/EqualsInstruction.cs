using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200173B RID: 5947
	internal sealed class EqualsInstruction : ConditionInstruction
	{
		// Token: 0x06009746 RID: 38726 RVA: 0x001F55A7 File Offset: 0x001F37A7
		public EqualsInstruction(Instruction left, Instruction right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x06009747 RID: 38727 RVA: 0x001F55BD File Offset: 0x001F37BD
		public override bool ExecuteCondition(Value frame)
		{
			return this.left.Execute(frame).Equals(this.right.Execute(frame));
		}

		// Token: 0x06009748 RID: 38728 RVA: 0x001F55DC File Offset: 0x001F37DC
		public override bool ExecuteCondition(ref MembersFrame0 frame)
		{
			return this.left.Execute(ref frame).Equals(this.right.Execute(ref frame));
		}

		// Token: 0x06009749 RID: 38729 RVA: 0x001F55FB File Offset: 0x001F37FB
		public override bool ExecuteCondition(ref MembersFrame1 frame)
		{
			return this.left.Execute(ref frame).Equals(this.right.Execute(ref frame));
		}

		// Token: 0x0600974A RID: 38730 RVA: 0x001F561A File Offset: 0x001F381A
		public override bool ExecuteCondition(ref MembersFrame2 frame)
		{
			return this.left.Execute(ref frame).Equals(this.right.Execute(ref frame));
		}

		// Token: 0x0600974B RID: 38731 RVA: 0x001F5639 File Offset: 0x001F3839
		public override bool ExecuteCondition(ref MembersFrameN frame)
		{
			return this.left.Execute(ref frame).Equals(this.right.Execute(ref frame));
		}

		// Token: 0x0400503E RID: 20542
		private readonly Instruction left;

		// Token: 0x0400503F RID: 20543
		private readonly Instruction right;
	}
}
