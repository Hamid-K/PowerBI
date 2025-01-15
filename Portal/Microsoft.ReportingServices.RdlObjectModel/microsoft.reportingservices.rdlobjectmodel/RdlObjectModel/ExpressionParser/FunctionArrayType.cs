using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B2 RID: 690
	[Serializable]
	internal sealed class FunctionArrayType : BaseInternalExpression
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x000318F8 File Offset: 0x0002FAF8
		internal FunctionArrayType(IInternalExpression typeExpr, int rank)
		{
			this.m_typeExpr = typeExpr;
			this.m_rank = rank;
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0003190E File Offset: 0x0002FB0E
		internal IInternalExpression TypeExpr
		{
			get
			{
				return this.m_typeExpr;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00031916 File Offset: 0x0002FB16
		internal int Rank
		{
			get
			{
				return this.m_rank;
			}
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0003191E File Offset: 0x0002FB1E
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00031925 File Offset: 0x0002FB25
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this.TypeExpr == null)
			{
				return "";
			}
			return this.TypeExpr.WriteSource();
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00031940 File Offset: 0x0002FB40
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this.m_typeExpr != null)
			{
				this.m_typeExpr.Traverse(callback);
			}
		}

		// Token: 0x040006BE RID: 1726
		private readonly IInternalExpression m_typeExpr;

		// Token: 0x040006BF RID: 1727
		private readonly int m_rank;
	}
}
