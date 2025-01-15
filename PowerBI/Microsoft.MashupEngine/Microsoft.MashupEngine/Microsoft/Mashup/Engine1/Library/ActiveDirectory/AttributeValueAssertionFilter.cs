using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FFC RID: 4092
	internal class AttributeValueAssertionFilter : Filter
	{
		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x06006B44 RID: 27460 RVA: 0x001716BE File Offset: 0x0016F8BE
		public RelationalOperator Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x06006B45 RID: 27461 RVA: 0x001716C6 File Offset: 0x0016F8C6
		public string AttributeName
		{
			get
			{
				return this.attributeName;
			}
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x06006B46 RID: 27462 RVA: 0x001716CE File Offset: 0x0016F8CE
		public AttributeValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x001716D6 File Offset: 0x0016F8D6
		public AttributeValueAssertionFilter(string attributeName, RelationalOperator op, AttributeValue value)
		{
			this.op = op;
			this.attributeName = attributeName;
			this.value = value;
		}

		// Token: 0x06006B48 RID: 27464 RVA: 0x001716F3 File Offset: 0x0016F8F3
		public override void Write(StringBuilder builder)
		{
			builder.Append("(");
			builder.Append(this.AttributeName);
			this.AppendOperator(builder);
			builder.Append(this.Value.Value);
			builder.Append(")");
		}

		// Token: 0x06006B49 RID: 27465 RVA: 0x00171734 File Offset: 0x0016F934
		private void AppendOperator(StringBuilder builder)
		{
			switch (this.Operator)
			{
			case RelationalOperator.Equal:
				builder.Append("=");
				return;
			case RelationalOperator.GreaterThanOrEquals:
				builder.Append(">=");
				return;
			case RelationalOperator.LesserThanOrEquals:
				builder.Append("<=");
				return;
			case RelationalOperator.Approx:
				builder.Append("~=");
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04003BA8 RID: 15272
		private readonly RelationalOperator op;

		// Token: 0x04003BA9 RID: 15273
		private readonly string attributeName;

		// Token: 0x04003BAA RID: 15274
		private readonly AttributeValue value;
	}
}
