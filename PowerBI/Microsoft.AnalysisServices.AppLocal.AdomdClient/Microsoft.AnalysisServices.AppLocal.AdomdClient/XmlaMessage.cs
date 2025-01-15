using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000045 RID: 69
	internal abstract class XmlaMessage
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		internal XmlaMessage(string description, string source, string helpFile, XmlaMessageLocation location)
		{
			this.m_description = description;
			this.m_source = source;
			this.m_helpFile = helpFile;
			this.location = location;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001C4D5 File Offset: 0x0001A6D5
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001C4DD File Offset: 0x0001A6DD
		public string Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001C4E5 File Offset: 0x0001A6E5
		public string HelpFile
		{
			get
			{
				return this.m_helpFile;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001C4ED File Offset: 0x0001A6ED
		public XmlaMessageLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x040003B1 RID: 945
		private string m_description;

		// Token: 0x040003B2 RID: 946
		private string m_source;

		// Token: 0x040003B3 RID: 947
		private string m_helpFile;

		// Token: 0x040003B4 RID: 948
		private XmlaMessageLocation location;
	}
}
