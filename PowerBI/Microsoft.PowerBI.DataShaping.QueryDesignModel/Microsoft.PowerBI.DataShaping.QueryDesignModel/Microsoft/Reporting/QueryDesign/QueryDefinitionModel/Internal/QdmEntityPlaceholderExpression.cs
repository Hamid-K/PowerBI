using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000FC RID: 252
	internal sealed class QdmEntityPlaceholderExpression : QueryExtensionExpression
	{
		// Token: 0x06000EAF RID: 3759 RVA: 0x00027860 File Offset: 0x00025A60
		internal QdmEntityPlaceholderExpression(ConceptualResultType conceptualResultType, EntitySet target, IConceptualEntity targetEntity)
			: base(conceptualResultType)
		{
			this._target = target;
			this._targetEntity = targetEntity;
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00027877 File Offset: 0x00025A77
		public EntitySet Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0002787F File Offset: 0x00025A7F
		public IConceptualEntity TargetEntity
		{
			get
			{
				return this._targetEntity;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00027888 File Offset: 0x00025A88
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QdmEntityPlaceholderExpression>(this, other, out flag, out qdmEntityPlaceholderExpression))
			{
				return flag;
			}
			return object.Equals(this.Target, qdmEntityPlaceholderExpression.Target) && ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(this.TargetEntity, qdmEntityPlaceholderExpression.TargetEntity);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000278CF File Offset: 0x00025ACF
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(Microsoft.DataShaping.Common.Hashing.GetHashCode<EntitySet>(this.Target, null), ConceptualEntityExtensionAwareEqualityComparer.Instance.GetHashCode(this.TargetEntity));
		}

		// Token: 0x040009D1 RID: 2513
		private readonly EntitySet _target;

		// Token: 0x040009D2 RID: 2514
		private readonly IConceptualEntity _targetEntity;
	}
}
