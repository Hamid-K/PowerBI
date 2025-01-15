using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200011F RID: 287
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ExtensionParameter
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000169F4 File Offset: 0x00014BF4
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x000169FC File Offset: 0x00014BFC
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

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00016A05 File Offset: 0x00014C05
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00016A0D File Offset: 0x00014C0D
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00016A16 File Offset: 0x00014C16
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00016A1E File Offset: 0x00014C1E
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00016A27 File Offset: 0x00014C27
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00016A2F File Offset: 0x00014C2F
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

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00016A38 File Offset: 0x00014C38
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00016A40 File Offset: 0x00014C40
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

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00016A49 File Offset: 0x00014C49
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x00016A51 File Offset: 0x00014C51
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00016A5A File Offset: 0x00014C5A
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00016A62 File Offset: 0x00014C62
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

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00016A6B File Offset: 0x00014C6B
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00016A73 File Offset: 0x00014C73
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

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00016A7C File Offset: 0x00014C7C
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00016A84 File Offset: 0x00014C84
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

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00016A8D File Offset: 0x00014C8D
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00016A95 File Offset: 0x00014C95
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

		// Token: 0x04000373 RID: 883
		private string nameField;

		// Token: 0x04000374 RID: 884
		private string displayNameField;

		// Token: 0x04000375 RID: 885
		private bool requiredField;

		// Token: 0x04000376 RID: 886
		private bool requiredFieldSpecified;

		// Token: 0x04000377 RID: 887
		private bool readOnlyField;

		// Token: 0x04000378 RID: 888
		private string valueField;

		// Token: 0x04000379 RID: 889
		private string errorField;

		// Token: 0x0400037A RID: 890
		private bool encryptedField;

		// Token: 0x0400037B RID: 891
		private bool isPasswordField;

		// Token: 0x0400037C RID: 892
		private ValidValue[] validValuesField;
	}
}
