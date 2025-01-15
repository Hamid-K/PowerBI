using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200029C RID: 668
	public sealed class GroupExpressionCollection : ReportElementCollectionBase<ReportVariantProperty>
	{
		// Token: 0x060019CB RID: 6603 RVA: 0x0006870B File Offset: 0x0006690B
		internal GroupExpressionCollection()
		{
			this.m_list = new List<ReportVariantProperty>();
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00068720 File Offset: 0x00066920
		internal GroupExpressionCollection(Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping)
		{
			if (grouping == null || grouping.GroupExpressions == null)
			{
				this.m_list = new List<ReportVariantProperty>();
				return;
			}
			int count = grouping.GroupExpressions.Count;
			this.m_list = new List<ReportVariantProperty>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new ReportVariantProperty(grouping.GroupExpressions[i]));
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006878C File Offset: 0x0006698C
		internal GroupExpressionCollection(Microsoft.ReportingServices.ReportProcessing.Grouping grouping)
		{
			if (grouping == null || grouping.GroupExpressions == null)
			{
				this.m_list = new List<ReportVariantProperty>();
				return;
			}
			int count = grouping.GroupExpressions.Count;
			this.m_list = new List<ReportVariantProperty>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new ReportVariantProperty(grouping.GroupExpressions[i]));
			}
		}

		// Token: 0x17000ECE RID: 3790
		public override ReportVariantProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_list[index];
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0006884F File Offset: 0x00066A4F
		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x04000CE1 RID: 3297
		private List<ReportVariantProperty> m_list;
	}
}
