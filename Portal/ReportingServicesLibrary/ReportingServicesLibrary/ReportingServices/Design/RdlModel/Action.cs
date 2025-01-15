using System;
using System.ComponentModel;
using System.Drawing.Design;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003A5 RID: 933
	[Editor("ActionEditorDialog", typeof(UITypeEditor))]
	public sealed class Action : IVoluntarySerializable
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0007D6D2 File Offset: 0x0007B8D2
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0007D6DA File Offset: 0x0007B8DA
		[DefaultValue("")]
		public string Hyperlink
		{
			get
			{
				return this.m_hyperlink;
			}
			set
			{
				this.m_hyperlink = value;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0007D6E3 File Offset: 0x0007B8E3
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x0007D6EB File Offset: 0x0007B8EB
		[DefaultValue("")]
		public string BookmarkLink
		{
			get
			{
				return this.m_bookmarkLink;
			}
			set
			{
				this.m_bookmarkLink = value;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0007D6F4 File Offset: 0x0007B8F4
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0007D6FC File Offset: 0x0007B8FC
		[DefaultValue("")]
		[XmlParentElement("Drillthrough")]
		public string ReportName
		{
			get
			{
				return this.m_drillthroughReport;
			}
			set
			{
				this.m_drillthroughReport = value;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0007D705 File Offset: 0x0007B905
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x0007D725 File Offset: 0x0007B925
		[XmlParentElement("Drillthrough")]
		public Parameters Parameters
		{
			get
			{
				if (this.m_drillthroughReport != null && this.m_drillthroughReport.Length > 0)
				{
					return this.m_drillthroughParameters;
				}
				return null;
			}
			set
			{
				this.m_drillthroughParameters = value;
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0007D72E File Offset: 0x0007B92E
		public override string ToString()
		{
			if (((IVoluntarySerializable)this).ShouldBeSerialized())
			{
				return "...";
			}
			return null;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0007D73F File Offset: 0x0007B93F
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return this.m_bookmarkLink.Length != 0 || this.m_hyperlink.Length != 0 || this.m_drillthroughReport.Length != 0;
		}

		// Token: 0x04000CF6 RID: 3318
		private string m_hyperlink = "";

		// Token: 0x04000CF7 RID: 3319
		private string m_bookmarkLink = "";

		// Token: 0x04000CF8 RID: 3320
		private string m_drillthroughReport = "";

		// Token: 0x04000CF9 RID: 3321
		private Parameters m_drillthroughParameters;
	}
}
