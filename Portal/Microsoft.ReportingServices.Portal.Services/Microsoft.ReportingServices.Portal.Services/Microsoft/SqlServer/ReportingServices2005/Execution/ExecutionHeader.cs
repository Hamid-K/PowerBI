using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200008D RID: 141
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ExecutionHeader : SoapHeader
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001B82D File Offset: 0x00019A2D
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0001B835 File Offset: 0x00019A35
		public string ExecutionID
		{
			get
			{
				return this.executionIDField;
			}
			set
			{
				this.executionIDField = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0001B83E File Offset: 0x00019A3E
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0001B846 File Offset: 0x00019A46
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

		// Token: 0x040001C3 RID: 451
		private string executionIDField;

		// Token: 0x040001C4 RID: 452
		private XmlAttribute[] anyAttrField;
	}
}
