using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200023B RID: 571
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	[Serializable]
	public class BatchHeader : SoapHeader
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0002270F File Offset: 0x0002090F
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x00022717 File Offset: 0x00020917
		public string BatchID
		{
			get
			{
				return this.batchIDField;
			}
			set
			{
				this.batchIDField = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00022720 File Offset: 0x00020920
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x00022728 File Offset: 0x00020928
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

		// Token: 0x040006C1 RID: 1729
		private string batchIDField;

		// Token: 0x040006C2 RID: 1730
		private XmlAttribute[] anyAttrField;
	}
}
