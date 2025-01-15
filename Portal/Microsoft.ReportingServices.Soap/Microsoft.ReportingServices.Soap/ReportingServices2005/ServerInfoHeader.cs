using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020001F9 RID: 505
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	[Serializable]
	public class ServerInfoHeader : SoapHeader
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00021618 File Offset: 0x0001F818
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x00021620 File Offset: 0x0001F820
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

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00021629 File Offset: 0x0001F829
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x00021631 File Offset: 0x0001F831
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

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0002163A File Offset: 0x0001F83A
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x00021642 File Offset: 0x0001F842
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

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0002164B File Offset: 0x0001F84B
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x00021653 File Offset: 0x0001F853
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

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0002165C File Offset: 0x0001F85C
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x00021664 File Offset: 0x0001F864
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

		// Token: 0x04000594 RID: 1428
		private string reportServerVersionNumberField;

		// Token: 0x04000595 RID: 1429
		private string reportServerEditionField;

		// Token: 0x04000596 RID: 1430
		private string reportServerVersionField;

		// Token: 0x04000597 RID: 1431
		private string reportServerDateTimeField;

		// Token: 0x04000598 RID: 1432
		private XmlAttribute[] anyAttrField;
	}
}
