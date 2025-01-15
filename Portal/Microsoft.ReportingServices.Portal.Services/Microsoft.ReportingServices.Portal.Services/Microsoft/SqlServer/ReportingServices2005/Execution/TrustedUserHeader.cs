using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200008E RID: 142
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class TrustedUserHeader : SoapHeader
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001B84F File Offset: 0x00019A4F
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0001B857 File Offset: 0x00019A57
		public string UserName
		{
			get
			{
				return this.userNameField;
			}
			set
			{
				this.userNameField = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001B860 File Offset: 0x00019A60
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001B868 File Offset: 0x00019A68
		[XmlElement(DataType = "base64Binary")]
		public byte[] UserToken
		{
			get
			{
				return this.userTokenField;
			}
			set
			{
				this.userTokenField = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001B871 File Offset: 0x00019A71
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001B879 File Offset: 0x00019A79
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

		// Token: 0x040001C5 RID: 453
		private string userNameField;

		// Token: 0x040001C6 RID: 454
		private byte[] userTokenField;

		// Token: 0x040001C7 RID: 455
		private XmlAttribute[] anyAttrField;
	}
}
