using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000075 RID: 117
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ServerInfoHeader : SoapHeader
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001B224 File Offset: 0x00019424
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0001B22C File Offset: 0x0001942C
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001B235 File Offset: 0x00019435
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0001B23D File Offset: 0x0001943D
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001B246 File Offset: 0x00019446
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0001B24E File Offset: 0x0001944E
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001B257 File Offset: 0x00019457
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0001B25F File Offset: 0x0001945F
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001B268 File Offset: 0x00019468
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0001B270 File Offset: 0x00019470
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

		// Token: 0x04000157 RID: 343
		private string reportServerVersionNumberField;

		// Token: 0x04000158 RID: 344
		private string reportServerEditionField;

		// Token: 0x04000159 RID: 345
		private string reportServerVersionField;

		// Token: 0x0400015A RID: 346
		private string reportServerDateTimeField;

		// Token: 0x0400015B RID: 347
		private XmlAttribute[] anyAttrField;
	}
}
