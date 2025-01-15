using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200023C RID: 572
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	[Serializable]
	public class ItemNamespaceHeader : SoapHeader
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00022739 File Offset: 0x00020939
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x00022741 File Offset: 0x00020941
		public ItemNamespaceEnum ItemNamespace
		{
			get
			{
				return this.itemNamespaceField;
			}
			set
			{
				this.itemNamespaceField = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0002274A File Offset: 0x0002094A
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x00022752 File Offset: 0x00020952
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

		// Token: 0x040006C3 RID: 1731
		private ItemNamespaceEnum itemNamespaceField;

		// Token: 0x040006C4 RID: 1732
		private XmlAttribute[] anyAttrField;
	}
}
