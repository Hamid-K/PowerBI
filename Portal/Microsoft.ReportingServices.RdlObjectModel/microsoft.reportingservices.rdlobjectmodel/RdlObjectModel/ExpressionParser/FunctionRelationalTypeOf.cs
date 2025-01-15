using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002AF RID: 687
	[Serializable]
	internal sealed class FunctionRelationalTypeOf : BaseInternalExpression
	{
		// Token: 0x06001543 RID: 5443 RVA: 0x000316FE File Offset: 0x0002F8FE
		public FunctionRelationalTypeOf(IInternalExpression lhs, FunctionType typeNameExpr)
		{
			this.m_lhs = lhs;
			this.m_typeNameExpr = typeNameExpr;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00031714 File Offset: 0x0002F914
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x0003171C File Offset: 0x0002F91C
		public IInternalExpression Lhs
		{
			get
			{
				return this.m_lhs;
			}
			set
			{
				this.m_lhs = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00031725 File Offset: 0x0002F925
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0003172D File Offset: 0x0002F92D
		public FunctionType TypeNameExpr
		{
			get
			{
				return this.m_typeNameExpr;
			}
			set
			{
				this.m_typeNameExpr = value;
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00031736 File Offset: 0x0002F936
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00031739 File Offset: 0x0002F939
		public override object Evaluate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00031740 File Offset: 0x0002F940
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00031744 File Offset: 0x0002F944
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("TypeOf ");
			stringBuilder.Append(this.m_lhs.WriteSource(nameChanges));
			stringBuilder.Append(" Is ");
			stringBuilder.Append(this.m_typeNameExpr.WriteSource(nameChanges));
			return stringBuilder.ToString();
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00031792 File Offset: 0x0002F992
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			this.m_lhs.Traverse(callback);
			this.m_typeNameExpr.Traverse(callback);
		}

		// Token: 0x040006B8 RID: 1720
		private IInternalExpression m_lhs;

		// Token: 0x040006B9 RID: 1721
		private FunctionType m_typeNameExpr;
	}
}
