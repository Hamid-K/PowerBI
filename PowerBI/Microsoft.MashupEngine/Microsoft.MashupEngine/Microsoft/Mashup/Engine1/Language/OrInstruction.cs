using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001731 RID: 5937
	internal sealed class OrInstruction : Instruction
	{
		// Token: 0x060096FE RID: 38654 RVA: 0x001F4ABC File Offset: 0x001F2CBC
		public OrInstruction(Instruction left, Instruction right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x060096FF RID: 38655 RVA: 0x001F4AD4 File Offset: 0x001F2CD4
		public override Value Execute(Value frame)
		{
			Value value = this.left.Execute(frame);
			if (!value.IsNull && value.AsBoolean)
			{
				return LogicalValue.True;
			}
			return OrInstruction.Or(value, this.right.Execute(frame));
		}

		// Token: 0x06009700 RID: 38656 RVA: 0x001F4B18 File Offset: 0x001F2D18
		public override Value Execute(ref MembersFrame0 frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && value.AsBoolean)
			{
				return LogicalValue.True;
			}
			return OrInstruction.Or(value, this.right.Execute(ref frame));
		}

		// Token: 0x06009701 RID: 38657 RVA: 0x001F4B5C File Offset: 0x001F2D5C
		public override Value Execute(ref MembersFrame1 frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && value.AsBoolean)
			{
				return LogicalValue.True;
			}
			return OrInstruction.Or(value, this.right.Execute(ref frame));
		}

		// Token: 0x06009702 RID: 38658 RVA: 0x001F4BA0 File Offset: 0x001F2DA0
		public override Value Execute(ref MembersFrame2 frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && value.AsBoolean)
			{
				return LogicalValue.True;
			}
			return OrInstruction.Or(value, this.right.Execute(ref frame));
		}

		// Token: 0x06009703 RID: 38659 RVA: 0x001F4BE4 File Offset: 0x001F2DE4
		public override Value Execute(ref MembersFrameN frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && value.AsBoolean)
			{
				return LogicalValue.True;
			}
			return OrInstruction.Or(value, this.right.Execute(ref frame));
		}

		// Token: 0x06009704 RID: 38660 RVA: 0x001F4C28 File Offset: 0x001F2E28
		public override bool ExecuteCondition(Value frame)
		{
			Value value = this.left.Execute(frame);
			return (!value.IsNull && value.AsBoolean) || this.right.ExecuteCondition(frame) || value.AsBoolean;
		}

		// Token: 0x06009705 RID: 38661 RVA: 0x001F4C6C File Offset: 0x001F2E6C
		public override bool ExecuteCondition(ref MembersFrame0 frame)
		{
			Value value = this.left.Execute(ref frame);
			return (!value.IsNull && value.AsBoolean) || this.right.ExecuteCondition(ref frame) || value.AsBoolean;
		}

		// Token: 0x06009706 RID: 38662 RVA: 0x001F4CB0 File Offset: 0x001F2EB0
		public override bool ExecuteCondition(ref MembersFrame1 frame)
		{
			Value value = this.left.Execute(ref frame);
			return (!value.IsNull && value.AsBoolean) || this.right.ExecuteCondition(ref frame) || value.AsBoolean;
		}

		// Token: 0x06009707 RID: 38663 RVA: 0x001F4CF4 File Offset: 0x001F2EF4
		public override bool ExecuteCondition(ref MembersFrame2 frame)
		{
			Value value = this.left.Execute(ref frame);
			return (!value.IsNull && value.AsBoolean) || this.right.ExecuteCondition(ref frame) || value.AsBoolean;
		}

		// Token: 0x06009708 RID: 38664 RVA: 0x001F4D38 File Offset: 0x001F2F38
		public override bool ExecuteCondition(ref MembersFrameN frame)
		{
			Value value = this.left.Execute(ref frame);
			return (!value.IsNull && value.AsBoolean) || this.right.ExecuteCondition(ref frame) || value.AsBoolean;
		}

		// Token: 0x06009709 RID: 38665 RVA: 0x001F4D7A File Offset: 0x001F2F7A
		private static Value Or(Value left, Value right)
		{
			if (left.IsNull)
			{
				if (!right.IsNull && right.AsBoolean)
				{
					return LogicalValue.True;
				}
				return Value.Null;
			}
			else
			{
				if (right.IsNull)
				{
					return Value.Null;
				}
				return right.AsLogical;
			}
		}

		// Token: 0x0400502C RID: 20524
		private readonly Instruction left;

		// Token: 0x0400502D RID: 20525
		private readonly Instruction right;
	}
}
