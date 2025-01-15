using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000034 RID: 52
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ExtensionParameter
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000D272 File Offset: 0x0000B472
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0000D27A File Offset: 0x0000B47A
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000D283 File Offset: 0x0000B483
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000D28B File Offset: 0x0000B48B
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000D294 File Offset: 0x0000B494
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000D29C File Offset: 0x0000B49C
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000D2A5 File Offset: 0x0000B4A5
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0000D2AD File Offset: 0x0000B4AD
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000D2B6 File Offset: 0x0000B4B6
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000D2BE File Offset: 0x0000B4BE
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000D2C7 File Offset: 0x0000B4C7
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000D2CF File Offset: 0x0000B4CF
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000D2E9 File Offset: 0x0000B4E9
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0000D2F1 File Offset: 0x0000B4F1
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000D2FA File Offset: 0x0000B4FA
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000D302 File Offset: 0x0000B502
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000D30B File Offset: 0x0000B50B
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000D313 File Offset: 0x0000B513
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

		// Token: 0x040001CC RID: 460
		private string nameField;

		// Token: 0x040001CD RID: 461
		private string displayNameField;

		// Token: 0x040001CE RID: 462
		private bool requiredField;

		// Token: 0x040001CF RID: 463
		private bool requiredFieldSpecified;

		// Token: 0x040001D0 RID: 464
		private bool readOnlyField;

		// Token: 0x040001D1 RID: 465
		private string valueField;

		// Token: 0x040001D2 RID: 466
		private string errorField;

		// Token: 0x040001D3 RID: 467
		private bool encryptedField;

		// Token: 0x040001D4 RID: 468
		private bool isPasswordField;

		// Token: 0x040001D5 RID: 469
		private ValidValue[] validValuesField;
	}
}
