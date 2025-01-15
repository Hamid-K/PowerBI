using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000185 RID: 389
	internal sealed class QueryFormatExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600151A RID: 5402 RVA: 0x0003B7CA File Offset: 0x000399CA
		internal QueryFormatExpression(QueryExpression input, string formatString, string locale = null)
			: base(ConceptualPrimitiveResultType.Text)
		{
			this._input = input;
			this._formatString = formatString;
			this._locale = locale;
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0003B7EC File Offset: 0x000399EC
		public QueryExpression Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0003B7F4 File Offset: 0x000399F4
		public string FormatString
		{
			get
			{
				return this._formatString;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0003B7FC File Offset: 0x000399FC
		public string Locale
		{
			get
			{
				return this._locale;
			}
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0003B804 File Offset: 0x00039A04
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0003B810 File Offset: 0x00039A10
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryFormatExpression queryFormatExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryFormatExpression>(this, other, out flag, out queryFormatExpression))
			{
				return flag;
			}
			return this.Input.Equals(queryFormatExpression.Input) && this.FormatString.Equals(queryFormatExpression.FormatString, StringComparison.Ordinal) && this.Locale.Equals(queryFormatExpression.Locale, StringComparison.Ordinal);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0003B867 File Offset: 0x00039A67
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Input.GetHashCode(), this.FormatString.GetHashCode(), Hashing.GetHashCode<string>(this.Locale, null));
		}

		// Token: 0x04000B49 RID: 2889
		private readonly QueryExpression _input;

		// Token: 0x04000B4A RID: 2890
		private readonly string _formatString;

		// Token: 0x04000B4B RID: 2891
		private readonly string _locale;
	}
}
