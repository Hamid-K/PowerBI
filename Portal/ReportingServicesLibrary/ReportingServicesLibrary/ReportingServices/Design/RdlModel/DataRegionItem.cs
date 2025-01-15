using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D1 RID: 977
	public abstract class DataRegionItem : ReportItem
	{
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0007E39D File Offset: 0x0007C59D
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x0007E3A5 File Offset: 0x0007C5A5
		[DefaultValue(false)]
		public bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0007E3AE File Offset: 0x0007C5AE
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x0007E3B6 File Offset: 0x0007C5B6
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return this.m_pageBreakAtStart;
			}
			set
			{
				this.m_pageBreakAtStart = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0007E3BF File Offset: 0x0007C5BF
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x0007E3C7 File Offset: 0x0007C5C7
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return this.m_pageBreakAtEnd;
			}
			set
			{
				this.m_pageBreakAtEnd = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0007E3D0 File Offset: 0x0007C5D0
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x0007E3D8 File Offset: 0x0007C5D8
		[DefaultValue("")]
		public string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0007E3E1 File Offset: 0x0007C5E1
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0007E3E9 File Offset: 0x0007C5E9
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x00005C88 File Offset: 0x00003E88
		[Browsable(false)]
		[XmlIgnore]
		public new string RepeatWith
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x0007E3F2 File Offset: 0x0007C5F2
		// (set) Token: 0x06001F53 RID: 8019 RVA: 0x0007E3FA File Offset: 0x0007C5FA
		public Filters Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		// Token: 0x04000DAC RID: 3500
		private string m_dataSetName;

		// Token: 0x04000DAD RID: 3501
		private bool m_keepTogether;

		// Token: 0x04000DAE RID: 3502
		private bool m_pageBreakAtStart;

		// Token: 0x04000DAF RID: 3503
		private bool m_pageBreakAtEnd;

		// Token: 0x04000DB0 RID: 3504
		private string m_noRows;

		// Token: 0x04000DB1 RID: 3505
		private Filters m_filters;
	}
}
