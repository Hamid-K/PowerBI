using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000204 RID: 516
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ExtensionParameter
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00021976 File Offset: 0x0001FB76
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0002197E File Offset: 0x0001FB7E
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00021987 File Offset: 0x0001FB87
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0002198F File Offset: 0x0001FB8F
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00021998 File Offset: 0x0001FB98
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x000219A0 File Offset: 0x0001FBA0
		public bool Required
		{
			get
			{
				return this.requiredField;
			}
			set
			{
				this.requiredField = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000219A9 File Offset: 0x0001FBA9
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x000219B1 File Offset: 0x0001FBB1
		[XmlIgnore]
		public bool RequiredSpecified
		{
			get
			{
				return this.requiredFieldSpecified;
			}
			set
			{
				this.requiredFieldSpecified = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000219BA File Offset: 0x0001FBBA
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x000219C2 File Offset: 0x0001FBC2
		public bool ReadOnly
		{
			get
			{
				return this.readOnlyField;
			}
			set
			{
				this.readOnlyField = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x000219CB File Offset: 0x0001FBCB
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x000219D3 File Offset: 0x0001FBD3
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x000219DC File Offset: 0x0001FBDC
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x000219E4 File Offset: 0x0001FBE4
		public string Error
		{
			get
			{
				return this.errorField;
			}
			set
			{
				this.errorField = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x000219ED File Offset: 0x0001FBED
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x000219F5 File Offset: 0x0001FBF5
		public bool Encrypted
		{
			get
			{
				return this.encryptedField;
			}
			set
			{
				this.encryptedField = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x000219FE File Offset: 0x0001FBFE
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x00021A06 File Offset: 0x0001FC06
		public bool IsPassword
		{
			get
			{
				return this.isPasswordField;
			}
			set
			{
				this.isPasswordField = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00021A0F File Offset: 0x0001FC0F
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00021A17 File Offset: 0x0001FC17
		[XmlArrayItem("Value")]
		public ValidValue[] ValidValues
		{
			get
			{
				return this.validValuesField;
			}
			set
			{
				this.validValuesField = value;
			}
		}

		// Token: 0x040005C7 RID: 1479
		private string nameField;

		// Token: 0x040005C8 RID: 1480
		private string displayNameField;

		// Token: 0x040005C9 RID: 1481
		private bool requiredField;

		// Token: 0x040005CA RID: 1482
		private bool requiredFieldSpecified;

		// Token: 0x040005CB RID: 1483
		private bool readOnlyField;

		// Token: 0x040005CC RID: 1484
		private string valueField;

		// Token: 0x040005CD RID: 1485
		private string errorField;

		// Token: 0x040005CE RID: 1486
		private bool encryptedField;

		// Token: 0x040005CF RID: 1487
		private bool isPasswordField;

		// Token: 0x040005D0 RID: 1488
		private ValidValue[] validValuesField;
	}
}
