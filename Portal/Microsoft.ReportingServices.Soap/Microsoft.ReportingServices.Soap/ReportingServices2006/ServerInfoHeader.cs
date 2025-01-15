using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000110 RID: 272
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", IsNullable = false)]
	[Serializable]
	public class ServerInfoHeader : SoapHeader
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00016522 File Offset: 0x00014722
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0001652A File Offset: 0x0001472A
		public string ReportServerVersionNumber
		{
			get
			{
				return this.reportServerVersionNumberField;
			}
			set
			{
				this.reportServerVersionNumberField = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00016533 File Offset: 0x00014733
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0001653B File Offset: 0x0001473B
		public string ReportServerEdition
		{
			get
			{
				return this.reportServerEditionField;
			}
			set
			{
				this.reportServerEditionField = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00016544 File Offset: 0x00014744
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0001654C File Offset: 0x0001474C
		public string ReportServerVersion
		{
			get
			{
				return this.reportServerVersionField;
			}
			set
			{
				this.reportServerVersionField = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00016555 File Offset: 0x00014755
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0001655D File Offset: 0x0001475D
		public string ReportServerDateTime
		{
			get
			{
				return this.reportServerDateTimeField;
			}
			set
			{
				this.reportServerDateTimeField = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00016566 File Offset: 0x00014766
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0001656E File Offset: 0x0001476E
		public TimeZoneInformation ReportServerTimeZoneInfo
		{
			get
			{
				return this.reportServerTimeZoneInfoField;
			}
			set
			{
				this.reportServerTimeZoneInfoField = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00016577 File Offset: 0x00014777
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0001657F File Offset: 0x0001477F
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x0400032C RID: 812
		private string reportServerVersionNumberField;

		// Token: 0x0400032D RID: 813
		private string reportServerEditionField;

		// Token: 0x0400032E RID: 814
		private string reportServerVersionField;

		// Token: 0x0400032F RID: 815
		private string reportServerDateTimeField;

		// Token: 0x04000330 RID: 816
		private TimeZoneInformation reportServerTimeZoneInfoField;

		// Token: 0x04000331 RID: 817
		private XmlAttribute[] anyAttrField;
	}
}
