using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000045 RID: 69
	internal abstract class XmlaMessage
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0001C180 File Offset: 0x0001A380
		internal XmlaMessage(string description, string source, string helpFile, XmlaMessageLocation location)
		{
			this.m_description = description;
			this.m_source = source;
			this.m_helpFile = helpFile;
			this.location = location;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001C1AD File Offset: 0x0001A3AD
		public string Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001C1B5 File Offset: 0x0001A3B5
		public string HelpFile
		{
			get
			{
				return this.m_helpFile;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001C1BD File Offset: 0x0001A3BD
		public XmlaMessageLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x040003A4 RID: 932
		private string m_description;

		// Token: 0x040003A5 RID: 933
		private string m_source;

		// Token: 0x040003A6 RID: 934
		private string m_helpFile;

		// Token: 0x040003A7 RID: 935
		private XmlaMessageLocation location;
	}
}
