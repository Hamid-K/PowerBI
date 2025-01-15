using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public abstract class XmlaMessage
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001FE68 File Offset: 0x0001E068
		internal XmlaMessage(string description, string source, string helpFile, XmlaMessageLocation location)
		{
			this.m_description = description;
			this.m_source = source;
			this.m_helpFile = helpFile;
			this.location = location;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001FE8D File Offset: 0x0001E08D
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001FE95 File Offset: 0x0001E095
		public string Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001FE9D File Offset: 0x0001E09D
		public string HelpFile
		{
			get
			{
				return this.m_helpFile;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001FEA5 File Offset: 0x0001E0A5
		public XmlaMessageLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x040003E0 RID: 992
		private string m_description;

		// Token: 0x040003E1 RID: 993
		private string m_source;

		// Token: 0x040003E2 RID: 994
		private string m_helpFile;

		// Token: 0x040003E3 RID: 995
		private XmlaMessageLocation location;
	}
}
