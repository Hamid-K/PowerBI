using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200173E RID: 5950
	internal sealed class IfInstruction : Instruction
	{
		// Token: 0x06009759 RID: 38745 RVA: 0x001F575A File Offset: 0x001F395A
		public IfInstruction(Instruction condition, Instruction trueBranch, Instruction falseBranch)
		{
			this.condition = condition;
			this.trueBranch = trueBranch;
			this.falseBranch = falseBranch;
		}

		// Token: 0x0600975A RID: 38746 RVA: 0x001F5777 File Offset: 0x001F3977
		public override Value Execute(Value frame)
		{
			if (!this.condition.ExecuteCondition(frame))
			{
				return this.falseBranch.Execute(frame);
			}
			return this.trueBranch.Execute(frame);
		}

		// Token: 0x0600975B RID: 38747 RVA: 0x001F57A0 File Offset: 0x001F39A0
		public override Value Execute(ref MembersFrame0 frame)
		{
			if (!this.condition.ExecuteCondition(ref frame))
			{
				return this.falseBranch.Execute(ref frame);
			}
			return this.trueBranch.Execute(ref frame);
		}

		// Token: 0x0600975C RID: 38748 RVA: 0x001F57C9 File Offset: 0x001F39C9
		public override Value Execute(ref MembersFrame1 frame)
		{
			if (!this.condition.ExecuteCondition(ref frame))
			{
				return this.falseBranch.Execute(ref frame);
			}
			return this.trueBranch.Execute(ref frame);
		}

		// Token: 0x0600975D RID: 38749 RVA: 0x001F57F2 File Offset: 0x001F39F2
		public override Value Execute(ref MembersFrame2 frame)
		{
			if (!this.condition.ExecuteCondition(ref frame))
			{
				return this.falseBranch.Execute(ref frame);
			}
			return this.trueBranch.Execute(ref frame);
		}

		// Token: 0x0600975E RID: 38750 RVA: 0x001F581B File Offset: 0x001F3A1B
		public override Value Execute(ref MembersFrameN frame)
		{
			if (!this.condition.ExecuteCondition(ref frame))
			{
				return this.falseBranch.Execute(ref frame);
			}
			return this.trueBranch.Execute(ref frame);
		}

		// Token: 0x04005043 RID: 20547
		private readonly Instruction condition;

		// Token: 0x04005044 RID: 20548
		private readonly Instruction trueBranch;

		// Token: 0x04005045 RID: 20549
		private readonly Instruction falseBranch;
	}
}
