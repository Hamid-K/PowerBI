using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200027C RID: 636
	[Serializable]
	internal class FunctionDataSource : BaseInternalExpression
	{
		// Token: 0x06001420 RID: 5152 RVA: 0x0002FAC6 File Offset: 0x0002DCC6
		public FunctionDataSource(IInternalExpression nameExpr)
		{
			this.m_nameExpr = nameExpr;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0002FAD5 File Offset: 0x0002DCD5
		public FunctionDataSource(IInternalExpression nameExpr, string propertyName)
			: this(nameExpr)
		{
			this.m_propertyName = propertyName;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0002FAE5 File Offset: 0x0002DCE5
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0002FAE8 File Offset: 0x0002DCE8
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("DataSources(");
			stringBuilder.Append(nameChanges.GetNewName(NameChanges.EntryType.DataSource, this.m_nameExpr.WriteSource()));
			stringBuilder.Append(")");
			if (!string.IsNullOrEmpty(this.m_propertyName))
			{
				stringBuilder.Append(".");
				stringBuilder.Append(this.m_propertyName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0002FB51 File Offset: 0x0002DD51
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			if (this.m_nameExpr != null)
			{
				this.m_nameExpr.Traverse(callback);
			}
		}

		// Token: 0x0400069E RID: 1694
		private readonly IInternalExpression m_nameExpr;

		// Token: 0x0400069F RID: 1695
		private readonly string m_propertyName;
	}
}
