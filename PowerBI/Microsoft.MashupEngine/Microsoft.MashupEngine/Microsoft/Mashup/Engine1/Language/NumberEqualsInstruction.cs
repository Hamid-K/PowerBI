using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200173C RID: 5948
	internal sealed class NumberEqualsInstruction : ConditionInstruction
	{
		// Token: 0x0600974C RID: 38732 RVA: 0x001F5658 File Offset: 0x001F3858
		public NumberEqualsInstruction(Instruction left, NumberValue right)
		{
			this.left = left;
			this.right = right.AsDouble;
		}

		// Token: 0x0600974D RID: 38733 RVA: 0x001F5673 File Offset: 0x001F3873
		public override bool ExecuteCondition(Value frame)
		{
			return this.Compare(this.left.Execute(frame));
		}

		// Token: 0x0600974E RID: 38734 RVA: 0x001F5687 File Offset: 0x001F3887
		public override bool ExecuteCondition(ref MembersFrame0 frame)
		{
			return this.Compare(this.left.Execute(ref frame));
		}

		// Token: 0x0600974F RID: 38735 RVA: 0x001F569B File Offset: 0x001F389B
		public override bool ExecuteCondition(ref MembersFrame1 frame)
		{
			return this.Compare(this.left.Execute(ref frame));
		}

		// Token: 0x06009750 RID: 38736 RVA: 0x001F56AF File Offset: 0x001F38AF
		public override bool ExecuteCondition(ref MembersFrame2 frame)
		{
			return this.Compare(this.left.Execute(ref frame));
		}

		// Token: 0x06009751 RID: 38737 RVA: 0x001F56C3 File Offset: 0x001F38C3
		public override bool ExecuteCondition(ref MembersFrameN frame)
		{
			return this.Compare(this.left.Execute(ref frame));
		}

		// Token: 0x06009752 RID: 38738 RVA: 0x001F56D7 File Offset: 0x001F38D7
		private bool Compare(Value value)
		{
			return value.IsNumber && value.AsNumber.AsDouble == this.right;
		}

		// Token: 0x04005040 RID: 20544
		private readonly Instruction left;

		// Token: 0x04005041 RID: 20545
		private readonly double right;
	}
}
