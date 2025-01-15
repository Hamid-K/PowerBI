using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C0 RID: 192
	public sealed class DataShape
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x00012818 File Offset: 0x00010A18
		internal DataShape()
		{
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00012820 File Offset: 0x00010A20
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00012828 File Offset: 0x00010A28
		public string ID { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00012831 File Offset: 0x00010A31
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00012839 File Offset: 0x00010A39
		public long? TimeDataRetrieval { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00012844 File Offset: 0x00010A44
		[XmlIgnore]
		public bool TimeDataRetrievalSpecified
		{
			get
			{
				return this.TimeDataRetrieval != null;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001285F File Offset: 0x00010A5F
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00012867 File Offset: 0x00010A67
		public long? TimeQueryTranslation { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00012870 File Offset: 0x00010A70
		[XmlIgnore]
		public bool TimeQueryTranslationSpecified
		{
			get
			{
				return this.TimeQueryTranslation != null;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001288B File Offset: 0x00010A8B
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00012893 File Offset: 0x00010A93
		public long? TimeProcessing { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001289C File Offset: 0x00010A9C
		[XmlIgnore]
		public bool TimeProcessingSpecified
		{
			get
			{
				return this.TimeProcessing != null;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000128B7 File Offset: 0x00010AB7
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x000128BF File Offset: 0x00010ABF
		public long? TimeRendering { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000128C8 File Offset: 0x00010AC8
		[XmlIgnore]
		public bool TimeRenderingSpecified
		{
			get
			{
				return this.TimeRendering != null;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000128E3 File Offset: 0x00010AE3
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x000128EB File Offset: 0x00010AEB
		public ScaleTimeCategory ScalabilityTime { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x000128F4 File Offset: 0x00010AF4
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x000128FC File Offset: 0x00010AFC
		public EstimatedMemoryUsageKBCategory EstimatedMemoryUsageKB { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00012905 File Offset: 0x00010B05
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001290D File Offset: 0x00010B0D
		public List<Connection> Connections { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00012916 File Offset: 0x00010B16
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0001291E File Offset: 0x00010B1E
		public string QueryPattern { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00012927 File Offset: 0x00010B27
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001292F File Offset: 0x00010B2F
		public string QueryPatternReasons { get; set; }
	}
}
