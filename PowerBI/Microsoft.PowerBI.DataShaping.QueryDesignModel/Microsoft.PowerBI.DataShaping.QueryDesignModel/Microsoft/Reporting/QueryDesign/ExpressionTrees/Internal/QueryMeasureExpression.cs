using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A3 RID: 419
	internal sealed class QueryMeasureExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
		internal QueryMeasureExpression(ConceptualResultType conceptualResultType, EntitySet target, EdmMeasure measure, IConceptualEntity targetEntity = null, IConceptualMeasure targetMeasure = null)
			: base(conceptualResultType)
		{
			this._target = target;
			this._measure = measure;
			this._targetEntity = targetEntity;
			this._targetMeasure = targetMeasure;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0003C8C7 File Offset: 0x0003AAC7
		public EntitySet Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0003C8CF File Offset: 0x0003AACF
		public IConceptualEntity TargetEntity
		{
			get
			{
				return this._targetEntity;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0003C8D7 File Offset: 0x0003AAD7
		public EdmMeasure Measure
		{
			get
			{
				return this._measure;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0003C8DF File Offset: 0x0003AADF
		public IConceptualMeasure TargetMeasure
		{
			get
			{
				return this._targetMeasure;
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0003C8E7 File Offset: 0x0003AAE7
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0003C8FC File Offset: 0x0003AAFC
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryMeasureExpression queryMeasureExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryMeasureExpression>(this, other, out flag, out queryMeasureExpression))
			{
				return flag;
			}
			return object.Equals(this.Target, queryMeasureExpression.Target) && object.Equals(this.Measure, queryMeasureExpression.Measure) && ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(this.TargetEntity, queryMeasureExpression.TargetEntity) && object.Equals(this.TargetMeasure, queryMeasureExpression.TargetMeasure);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0003C969 File Offset: 0x0003AB69
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(Microsoft.DataShaping.Common.Hashing.GetHashCode<EntitySet>(this.Target, null), Microsoft.DataShaping.Common.Hashing.GetHashCode<EdmMeasure>(this.Measure, null), ConceptualEntityExtensionAwareEqualityComparer.Instance.GetHashCode(this.TargetEntity), Microsoft.DataShaping.Common.Hashing.GetHashCode<IConceptualMeasure>(this.TargetMeasure, null));
		}

		// Token: 0x04000B97 RID: 2967
		private readonly EntitySet _target;

		// Token: 0x04000B98 RID: 2968
		private readonly IConceptualEntity _targetEntity;

		// Token: 0x04000B99 RID: 2969
		private readonly EdmMeasure _measure;

		// Token: 0x04000B9A RID: 2970
		private readonly IConceptualMeasure _targetMeasure;
	}
}
