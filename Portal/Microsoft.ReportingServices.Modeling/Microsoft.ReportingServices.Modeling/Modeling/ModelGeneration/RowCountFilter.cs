using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F9 RID: 249
	public sealed class RowCountFilter : Filter, ITableFilter
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002934B File Offset: 0x0002754B
		public NumericCompareExpression CompareExpression
		{
			get
			{
				return this.m_compareExpr;
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00029354 File Offset: 0x00027554
		public bool IsMatch(DsvTable table)
		{
			return this.m_compareExpr.Evaluate(table.RowCount.GetValueOrDefault());
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00029380 File Offset: 0x00027580
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (!xr.IsDefaultNamespace || !(xr.LocalName == "count"))
			{
				return base.LoadXmlAttribute(xr, objectFactory);
			}
			if (!this.m_compareExpr.Parse(xr.ReadValueAsString()))
			{
				throw new RuleConfigurationException("Invalid count expression");
			}
			return true;
		}

		// Token: 0x04000528 RID: 1320
		private const string CountAttr = "count";

		// Token: 0x04000529 RID: 1321
		private readonly NumericCompareExpression m_compareExpr = new NumericCompareExpression();
	}
}
