using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B5 RID: 437
	internal sealed class QueryScanExpression : QueryExpression
	{
		// Token: 0x0600160C RID: 5644 RVA: 0x0003D38E File Offset: 0x0003B58E
		internal QueryScanExpression(ConceptualResultType conceptualResultType, EntitySet target, bool excludeBlankRow, IConceptualEntity targetEntity = null)
			: base(conceptualResultType)
		{
			this._target = target;
			this._targetEntity = targetEntity;
			this._excludeBlankRow = excludeBlankRow;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0003D3AD File Offset: 0x0003B5AD
		public EntitySet Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0003D3B5 File Offset: 0x0003B5B5
		public IConceptualEntity TargetEntity
		{
			get
			{
				return this._targetEntity;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0003D3BD File Offset: 0x0003B5BD
		public bool ExcludeBlankRow
		{
			get
			{
				return this._excludeBlankRow;
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0003D3C5 File Offset: 0x0003B5C5
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0003D3D8 File Offset: 0x0003B5D8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryScanExpression queryScanExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryScanExpression>(this, other, out flag, out queryScanExpression))
			{
				return flag;
			}
			return object.Equals(this.Target, queryScanExpression.Target) && this.ExcludeBlankRow == queryScanExpression.ExcludeBlankRow && ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(this.TargetEntity, queryScanExpression.TargetEntity);
		}

		// Token: 0x04000BC9 RID: 3017
		private readonly EntitySet _target;

		// Token: 0x04000BCA RID: 3018
		private readonly IConceptualEntity _targetEntity;

		// Token: 0x04000BCB RID: 3019
		private readonly bool _excludeBlankRow;
	}
}
