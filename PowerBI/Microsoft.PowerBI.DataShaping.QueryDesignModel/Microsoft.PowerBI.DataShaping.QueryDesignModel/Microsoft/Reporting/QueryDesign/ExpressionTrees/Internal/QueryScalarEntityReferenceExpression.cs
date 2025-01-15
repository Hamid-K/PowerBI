using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B4 RID: 436
	internal sealed class QueryScalarEntityReferenceExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x0003D2EA File Offset: 0x0003B4EA
		internal QueryScalarEntityReferenceExpression(ConceptualResultType conceptualResultType, EntitySet target, IConceptualEntity targetEntity = null)
			: base(conceptualResultType)
		{
			this._target = target;
			this._targetEntity = targetEntity;
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0003D301 File Offset: 0x0003B501
		public EntitySet Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0003D309 File Offset: 0x0003B509
		public IConceptualEntity TargetEntity
		{
			get
			{
				return this._targetEntity;
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0003D311 File Offset: 0x0003B511
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0003D324 File Offset: 0x0003B524
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryScalarEntityReferenceExpression queryScalarEntityReferenceExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryScalarEntityReferenceExpression>(this, other, out flag, out queryScalarEntityReferenceExpression))
			{
				return flag;
			}
			return object.Equals(this.Target, queryScalarEntityReferenceExpression.Target) && ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(this.TargetEntity, queryScalarEntityReferenceExpression.TargetEntity);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0003D36B File Offset: 0x0003B56B
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(Microsoft.DataShaping.Common.Hashing.GetHashCode<EntitySet>(this.Target, null), ConceptualEntityExtensionAwareEqualityComparer.Instance.GetHashCode(this.TargetEntity));
		}

		// Token: 0x04000BC7 RID: 3015
		private readonly EntitySet _target;

		// Token: 0x04000BC8 RID: 3016
		private readonly IConceptualEntity _targetEntity;
	}
}
