using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200027B RID: 635
	[Serializable]
	internal class FunctionDataSet : BaseInternalExpression
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x0002FA1B File Offset: 0x0002DC1B
		public FunctionDataSet(IInternalExpression nameExpr)
		{
			this.m_nameExpr = nameExpr;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0002FA2A File Offset: 0x0002DC2A
		public FunctionDataSet(IInternalExpression nameExpr, string propertyName)
			: this(nameExpr)
		{
			this.m_propertyName = propertyName;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0002FA3A File Offset: 0x0002DC3A
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0002FA40 File Offset: 0x0002DC40
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("DataSets(");
			stringBuilder.Append(nameChanges.GetNewName(NameChanges.EntryType.DataSet, this.m_nameExpr.WriteSource()));
			stringBuilder.Append(")");
			if (!string.IsNullOrEmpty(this.m_propertyName))
			{
				stringBuilder.Append(".");
				stringBuilder.Append(this.m_propertyName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0002FAA9 File Offset: 0x0002DCA9
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			if (this.m_nameExpr != null)
			{
				this.m_nameExpr.Traverse(callback);
			}
		}

		// Token: 0x0400069C RID: 1692
		private readonly IInternalExpression m_nameExpr;

		// Token: 0x0400069D RID: 1693
		private readonly string m_propertyName;
	}
}
