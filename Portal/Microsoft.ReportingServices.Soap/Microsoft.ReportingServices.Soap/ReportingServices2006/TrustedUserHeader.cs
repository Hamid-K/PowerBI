using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000153 RID: 339
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", IsNullable = false)]
	[Serializable]
	public class TrustedUserHeader : SoapHeader
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00017739 File Offset: 0x00015939
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00017741 File Offset: 0x00015941
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0001774A File Offset: 0x0001594A
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00017752 File Offset: 0x00015952
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0001775B File Offset: 0x0001595B
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00017763 File Offset: 0x00015963
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

		// Token: 0x04000467 RID: 1127
		private string userNameField;

		// Token: 0x04000468 RID: 1128
		private byte[] userTokenField;

		// Token: 0x04000469 RID: 1129
		private XmlAttribute[] anyAttrField;
	}
}
