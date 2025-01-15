using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001BE RID: 446
	internal sealed class QuerySwitchCase : IEquatable<QuerySwitchCase>
	{
		// Token: 0x06001637 RID: 5687 RVA: 0x0003D9E9 File Offset: 0x0003BBE9
		internal QuerySwitchCase(QueryExpression value, QueryExpression result)
		{
			this._value = value;
			this._result = result;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0003D9FF File Offset: 0x0003BBFF
		public QueryExpression Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0003DA07 File Offset: 0x0003BC07
		public QueryExpression Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0003DA0F File Offset: 0x0003BC0F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QuerySwitchCase);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0003DA1D File Offset: 0x0003BC1D
		public bool Equals(QuerySwitchCase other)
		{
			return this != other && other != null && this.Value.Equals(other.Value) && this.Result.Equals(other.Result);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Value.GetHashCode(), this.Result.GetHashCode());
		}

		// Token: 0x04000BDB RID: 3035
		private readonly QueryExpression _value;

		// Token: 0x04000BDC RID: 3036
		private readonly QueryExpression _result;
	}
}
