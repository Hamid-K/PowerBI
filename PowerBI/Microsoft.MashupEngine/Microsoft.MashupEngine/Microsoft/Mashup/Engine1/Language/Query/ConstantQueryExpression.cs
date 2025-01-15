using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001888 RID: 6280
	internal class ConstantQueryExpression : QueryExpression
	{
		// Token: 0x06009F49 RID: 40777 RVA: 0x0020EA04 File Offset: 0x0020CC04
		public ConstantQueryExpression(Value value)
		{
			this.value = value;
		}

		// Token: 0x1700291D RID: 10525
		// (get) Token: 0x06009F4A RID: 40778 RVA: 0x00002139 File Offset: 0x00000339
		public override QueryExpressionKind Kind
		{
			get
			{
				return QueryExpressionKind.Constant;
			}
		}

		// Token: 0x1700291E RID: 10526
		// (get) Token: 0x06009F4B RID: 40779 RVA: 0x0020EA13 File Offset: 0x0020CC13
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06009F4C RID: 40780 RVA: 0x0020EA1B File Offset: 0x0020CC1B
		public override bool TryGetConstant(out Value value)
		{
			value = this.value;
			return true;
		}

		// Token: 0x06009F4D RID: 40781 RVA: 0x0020EA26 File Offset: 0x0020CC26
		public override void Analyze(Func<QueryExpression, bool> analyzer)
		{
			analyzer(this);
		}

		// Token: 0x06009F4E RID: 40782 RVA: 0x0020EA30 File Offset: 0x0020CC30
		public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
		{
			return rewrite(this);
		}

		// Token: 0x06009F4F RID: 40783 RVA: 0x0020EA39 File Offset: 0x0020CC39
		public bool Equals(ConstantQueryExpression other)
		{
			return other != null && this.value.Equals(other.Value);
		}

		// Token: 0x06009F50 RID: 40784 RVA: 0x0020EA51 File Offset: 0x0020CC51
		public override bool Equals(object other)
		{
			return this.Equals(other as ConstantQueryExpression);
		}

		// Token: 0x06009F51 RID: 40785 RVA: 0x0020EA5F File Offset: 0x0020CC5F
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x0400539C RID: 21404
		public static readonly QueryExpression Null = new ConstantQueryExpression(Value.Null);

		// Token: 0x0400539D RID: 21405
		public static readonly QueryExpression True = new ConstantQueryExpression(LogicalValue.True);

		// Token: 0x0400539E RID: 21406
		public static readonly QueryExpression False = new ConstantQueryExpression(LogicalValue.False);

		// Token: 0x0400539F RID: 21407
		private Value value;
	}
}
