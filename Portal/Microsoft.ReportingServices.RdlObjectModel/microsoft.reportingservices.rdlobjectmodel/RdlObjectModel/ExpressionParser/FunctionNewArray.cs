using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002BA RID: 698
	[Serializable]
	internal sealed class FunctionNewArray : BaseInternalExpression
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x00031F24 File Offset: 0x00030124
		internal FunctionNewArray(FunctionArrayType typeExpr, FunctionArrayInit initExpr)
		{
			this.m_typeExpr = typeExpr;
			this.m_initExpr = initExpr;
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00031F3A File Offset: 0x0003013A
		internal FunctionArrayType TypeExpr
		{
			get
			{
				return this.m_typeExpr;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00031F42 File Offset: 0x00030142
		internal FunctionArrayInit InitExpr
		{
			get
			{
				return this.m_initExpr;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00031F4A File Offset: 0x0003014A
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00031F51 File Offset: 0x00030151
		public override string WriteSource(NameChanges nameChanges)
		{
			return "New " + this.TypeExpr.WriteSource() + "()" + this.InitExpr.WriteSource();
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00031F78 File Offset: 0x00030178
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			this.m_typeExpr.Traverse(callback);
			this.m_initExpr.Traverse(callback);
		}

		// Token: 0x040006D2 RID: 1746
		private readonly FunctionArrayType m_typeExpr;

		// Token: 0x040006D3 RID: 1747
		private readonly FunctionArrayInit m_initExpr;
	}
}
