using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C4 RID: 196
	public sealed class ModelMetadata
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x00012A4E File Offset: 0x00010C4E
		internal ModelMetadata()
		{
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00012A56 File Offset: 0x00010C56
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00012A5E File Offset: 0x00010C5E
		public string VersionRequested { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00012A67 File Offset: 0x00010C67
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00012A6F File Offset: 0x00010C6F
		public string PerspectiveName { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00012A78 File Offset: 0x00010C78
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00012A80 File Offset: 0x00010C80
		public long? TotalTimeDataRetrieval { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00012A8C File Offset: 0x00010C8C
		[XmlIgnore]
		public bool TotalTimeDataRetrievalSpecified
		{
			get
			{
				return this.TotalTimeDataRetrieval != null;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00012AA7 File Offset: 0x00010CA7
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00012AAF File Offset: 0x00010CAF
		public long? ByteCount { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00012AB8 File Offset: 0x00010CB8
		[XmlIgnore]
		public bool ByteCountSpecified
		{
			get
			{
				return this.ByteCount != null;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00012AD3 File Offset: 0x00010CD3
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00012ADB File Offset: 0x00010CDB
		public long? TimeDataModelParsing { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00012AE4 File Offset: 0x00010CE4
		[XmlIgnore]
		public bool TimeDataModelParsingSpecified
		{
			get
			{
				return this.TimeDataModelParsing != null;
			}
		}
	}
}
