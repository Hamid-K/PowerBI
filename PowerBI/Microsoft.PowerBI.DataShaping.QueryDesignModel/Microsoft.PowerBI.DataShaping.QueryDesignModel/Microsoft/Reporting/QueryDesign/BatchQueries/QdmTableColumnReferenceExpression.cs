using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200025A RID: 602
	internal sealed class QdmTableColumnReferenceExpression : QueryExtensionExpression
	{
		// Token: 0x06001A31 RID: 6705 RVA: 0x00048219 File Offset: 0x00046419
		internal QdmTableColumnReferenceExpression(QueryTableColumn target)
			: base(target.ConceptualResultType)
		{
			this._target = target;
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0004822E File Offset: 0x0004642E
		public QueryTableColumn Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00048238 File Offset: 0x00046438
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QdmTableColumnReferenceExpression qdmTableColumnReferenceExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QdmTableColumnReferenceExpression>(this, other, out flag, out qdmTableColumnReferenceExpression))
			{
				return flag;
			}
			return this.Target.Equals(qdmTableColumnReferenceExpression.Target);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00048265 File Offset: 0x00046465
		public override int GetHashCode()
		{
			return this.Target.GetHashCode();
		}

		// Token: 0x04000E81 RID: 3713
		private readonly QueryTableColumn _target;
	}
}
