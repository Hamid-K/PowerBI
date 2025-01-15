using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001732 RID: 5938
	internal sealed class AndInstruction : Instruction
	{
		// Token: 0x0600970A RID: 38666 RVA: 0x001F4DB4 File Offset: 0x001F2FB4
		public AndInstruction(Instruction left, Instruction right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x0600970B RID: 38667 RVA: 0x001F4DCC File Offset: 0x001F2FCC
		public override Value Execute(Value frame)
		{
			Value value = this.left.Execute(frame);
			if (!value.IsNull && !value.AsBoolean)
			{
				return LogicalValue.False;
			}
			return AndInstruction.And(value, this.right.Execute(frame));
		}

		// Token: 0x0600970C RID: 38668 RVA: 0x001F4E10 File Offset: 0x001F3010
		public override Value Execute(ref MembersFrame0 frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && !value.AsBoolean)
			{
				return LogicalValue.False;
			}
			return AndInstruction.And(value, this.right.Execute(ref frame));
		}

		// Token: 0x0600970D RID: 38669 RVA: 0x001F4E54 File Offset: 0x001F3054
		public override Value Execute(ref MembersFrame1 frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && !value.AsBoolean)
			{
				return LogicalValue.False;
			}
			return AndInstruction.And(value, this.right.Execute(ref frame));
		}

		// Token: 0x0600970E RID: 38670 RVA: 0x001F4E98 File Offset: 0x001F3098
		public override Value Execute(ref MembersFrame2 frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && !value.AsBoolean)
			{
				return LogicalValue.False;
			}
			return AndInstruction.And(value, this.right.Execute(ref frame));
		}

		// Token: 0x0600970F RID: 38671 RVA: 0x001F4EDC File Offset: 0x001F30DC
		public override Value Execute(ref MembersFrameN frame)
		{
			Value value = this.left.Execute(ref frame);
			if (!value.IsNull && !value.AsBoolean)
			{
				return LogicalValue.False;
			}
			return AndInstruction.And(value, this.right.Execute(ref frame));
		}

		// Token: 0x06009710 RID: 38672 RVA: 0x001F4F20 File Offset: 0x001F3120
		public override bool ExecuteCondition(Value frame)
		{
			Value value = this.left.Execute(frame);
			return (value.IsNull || value.AsBoolean) && this.right.ExecuteCondition(frame) && value.AsBoolean;
		}

		// Token: 0x06009711 RID: 38673 RVA: 0x001F4F64 File Offset: 0x001F3164
		public override bool ExecuteCondition(ref MembersFrame0 frame)
		{
			Value value = this.left.Execute(ref frame);
			return (value.IsNull || value.AsBoolean) && this.right.ExecuteCondition(ref frame) && value.AsBoolean;
		}

		// Token: 0x06009712 RID: 38674 RVA: 0x001F4FA8 File Offset: 0x001F31A8
		public override bool ExecuteCondition(ref MembersFrame1 frame)
		{
			Value value = this.left.Execute(ref frame);
			return (value.IsNull || value.AsBoolean) && this.right.ExecuteCondition(ref frame) && value.AsBoolean;
		}

		// Token: 0x06009713 RID: 38675 RVA: 0x001F4FEC File Offset: 0x001F31EC
		public override bool ExecuteCondition(ref MembersFrame2 frame)
		{
			Value value = this.left.Execute(ref frame);
			return (value.IsNull || value.AsBoolean) && this.right.ExecuteCondition(ref frame) && value.AsBoolean;
		}

		// Token: 0x06009714 RID: 38676 RVA: 0x001F5030 File Offset: 0x001F3230
		public override bool ExecuteCondition(ref MembersFrameN frame)
		{
			Value value = this.left.Execute(ref frame);
			return (value.IsNull || value.AsBoolean) && this.right.ExecuteCondition(ref frame) && value.AsBoolean;
		}

		// Token: 0x06009715 RID: 38677 RVA: 0x001F5072 File Offset: 0x001F3272
		private static Value And(Value left, Value right)
		{
			if (left.IsNull)
			{
				if (!right.IsNull && !right.AsBoolean)
				{
					return LogicalValue.False;
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

		// Token: 0x0400502E RID: 20526
		private readonly Instruction left;

		// Token: 0x0400502F RID: 20527
		private readonly Instruction right;
	}
}
