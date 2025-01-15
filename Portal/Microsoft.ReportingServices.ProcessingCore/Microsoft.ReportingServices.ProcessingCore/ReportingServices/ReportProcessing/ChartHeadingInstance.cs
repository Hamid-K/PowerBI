using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200074E RID: 1870
	[Serializable]
	internal sealed class ChartHeadingInstance : InstanceInfoOwner
	{
		// Token: 0x060067C6 RID: 26566 RVA: 0x00194CE0 File Offset: 0x00192EE0
		internal ChartHeadingInstance(ReportProcessing.ProcessingContext pc, int headingCellIndex, ChartHeading chartHeadingDef, int labelIndex, VariantList groupExpressionValues)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			if (chartHeadingDef.SubHeading != null)
			{
				this.m_subHeadingInstances = new ChartHeadingInstanceList();
			}
			this.m_instanceInfo = new ChartHeadingInstanceInfo(pc, headingCellIndex, chartHeadingDef, labelIndex, groupExpressionValues);
			this.m_chartHeadingDef = chartHeadingDef;
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x00194D20 File Offset: 0x00192F20
		internal ChartHeadingInstance()
		{
		}

		// Token: 0x170024AD RID: 9389
		// (get) Token: 0x060067C8 RID: 26568 RVA: 0x00194D28 File Offset: 0x00192F28
		// (set) Token: 0x060067C9 RID: 26569 RVA: 0x00194D30 File Offset: 0x00192F30
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x170024AE RID: 9390
		// (get) Token: 0x060067CA RID: 26570 RVA: 0x00194D39 File Offset: 0x00192F39
		// (set) Token: 0x060067CB RID: 26571 RVA: 0x00194D41 File Offset: 0x00192F41
		internal ChartHeading ChartHeadingDef
		{
			get
			{
				return this.m_chartHeadingDef;
			}
			set
			{
				this.m_chartHeadingDef = value;
			}
		}

		// Token: 0x170024AF RID: 9391
		// (get) Token: 0x060067CC RID: 26572 RVA: 0x00194D4A File Offset: 0x00192F4A
		// (set) Token: 0x060067CD RID: 26573 RVA: 0x00194D52 File Offset: 0x00192F52
		internal ChartHeadingInstanceList SubHeadingInstances
		{
			get
			{
				return this.m_subHeadingInstances;
			}
			set
			{
				this.m_subHeadingInstances = value;
			}
		}

		// Token: 0x170024B0 RID: 9392
		// (get) Token: 0x060067CE RID: 26574 RVA: 0x00194D5B File Offset: 0x00192F5B
		internal ChartHeadingInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (ChartHeadingInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x00194D87 File Offset: 0x00192F87
		internal ChartHeadingInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadChartHeadingInstanceInfo();
			}
			return (ChartHeadingInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x00194DC0 File Offset: 0x00192FC0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.SubHeadingInstances, ObjectType.ChartHeadingInstanceList)
			});
		}

		// Token: 0x04003364 RID: 13156
		private int m_uniqueName;

		// Token: 0x04003365 RID: 13157
		[Reference]
		private ChartHeading m_chartHeadingDef;

		// Token: 0x04003366 RID: 13158
		private ChartHeadingInstanceList m_subHeadingInstances;
	}
}
