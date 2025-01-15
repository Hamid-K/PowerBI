using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000003 RID: 3
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", IsNullable = false)]
	[Serializable]
	public class ServerInfoHeader : SoapHeader
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000C3FE File Offset: 0x0000A5FE
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000C406 File Offset: 0x0000A606
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000C40F File Offset: 0x0000A60F
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000C417 File Offset: 0x0000A617
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000C420 File Offset: 0x0000A620
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000C428 File Offset: 0x0000A628
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000C431 File Offset: 0x0000A631
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000C439 File Offset: 0x0000A639
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000C442 File Offset: 0x0000A642
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000C44A File Offset: 0x0000A64A
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000C453 File Offset: 0x0000A653
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000C45B File Offset: 0x0000A65B
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

		// Token: 0x040000FA RID: 250
		private string reportServerVersionNumberField;

		// Token: 0x040000FB RID: 251
		private string reportServerEditionField;

		// Token: 0x040000FC RID: 252
		private string reportServerVersionField;

		// Token: 0x040000FD RID: 253
		private string reportServerDateTimeField;

		// Token: 0x040000FE RID: 254
		private TimeZoneInformation reportServerTimeZoneInfoField;

		// Token: 0x040000FF RID: 255
		private XmlAttribute[] anyAttrField;
	}
}
