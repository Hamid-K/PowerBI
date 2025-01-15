using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000749 RID: 1865
	[Serializable]
	internal sealed class OWCChartInstance : ReportItemInstance, IPageItem
	{
		// Token: 0x0600677A RID: 26490 RVA: 0x00193E3D File Offset: 0x0019203D
		internal OWCChartInstance(ReportProcessing.ProcessingContext pc, OWCChart reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new OWCChartInstanceInfo(pc, reportItemDef, this);
			pc.QuickFind.Add(base.UniqueName, this);
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x00193E7A File Offset: 0x0019207A
		internal OWCChartInstance(ReportProcessing.ProcessingContext pc, OWCChart reportItemDef, VariantList[] chartData)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new OWCChartInstanceInfo(pc, reportItemDef, this, chartData);
			pc.QuickFind.Add(base.UniqueName, this);
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x00193EB8 File Offset: 0x001920B8
		internal OWCChartInstance()
		{
		}

		// Token: 0x1700248D RID: 9357
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x00193ECE File Offset: 0x001920CE
		internal OWCChartInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (OWCChartInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x1700248E RID: 9358
		// (get) Token: 0x0600677E RID: 26494 RVA: 0x00193EFA File Offset: 0x001920FA
		// (set) Token: 0x0600677F RID: 26495 RVA: 0x00193F02 File Offset: 0x00192102
		int IPageItem.StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x1700248F RID: 9359
		// (get) Token: 0x06006780 RID: 26496 RVA: 0x00193F0B File Offset: 0x0019210B
		// (set) Token: 0x06006781 RID: 26497 RVA: 0x00193F13 File Offset: 0x00192113
		int IPageItem.EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x00193F1C File Offset: 0x0019211C
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstance, memberInfoList);
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x00193F37 File Offset: 0x00192137
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadOWCChartInstanceInfo((OWCChart)this.m_reportItemDef);
		}

		// Token: 0x0400334E RID: 13134
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x0400334F RID: 13135
		[NonSerialized]
		private int m_endPage = -1;
	}
}
