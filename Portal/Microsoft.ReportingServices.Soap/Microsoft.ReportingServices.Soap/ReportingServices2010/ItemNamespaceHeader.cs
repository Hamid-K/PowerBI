using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000042 RID: 66
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", IsNullable = false)]
	[Serializable]
	public class ItemNamespaceHeader : SoapHeader
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000D734 File Offset: 0x0000B934
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000D73C File Offset: 0x0000B93C
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000D745 File Offset: 0x0000B945
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0000D74D File Offset: 0x0000B94D
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

		// Token: 0x04000217 RID: 535
		private ItemNamespaceEnum itemNamespaceField;

		// Token: 0x04000218 RID: 536
		private XmlAttribute[] anyAttrField;
	}
}
