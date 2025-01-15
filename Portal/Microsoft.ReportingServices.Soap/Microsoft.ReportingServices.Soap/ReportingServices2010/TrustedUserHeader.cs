using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000044 RID: 68
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", IsNullable = false)]
	[Serializable]
	public class TrustedUserHeader : SoapHeader
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000D75E File Offset: 0x0000B95E
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000D766 File Offset: 0x0000B966
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000D76F File Offset: 0x0000B96F
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000D777 File Offset: 0x0000B977
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000D780 File Offset: 0x0000B980
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000D788 File Offset: 0x0000B988
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

		// Token: 0x0400021C RID: 540
		private string userNameField;

		// Token: 0x0400021D RID: 541
		private byte[] userTokenField;

		// Token: 0x0400021E RID: 542
		private XmlAttribute[] anyAttrField;
	}
}
