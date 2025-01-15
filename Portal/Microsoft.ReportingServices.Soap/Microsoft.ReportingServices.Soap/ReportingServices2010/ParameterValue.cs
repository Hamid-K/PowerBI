using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000032 RID: 50
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ParameterValue : ParameterValueOrFieldReference
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000D163 File Offset: 0x0000B363
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000D16B File Offset: 0x0000B36B
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000D174 File Offset: 0x0000B374
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000D17C File Offset: 0x0000B37C
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000D185 File Offset: 0x0000B385
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0000D18D File Offset: 0x0000B38D
		public string Label
		{
			get
			{
				return this.labelField;
			}
			set
			{
				this.labelField = value;
			}
		}

		// Token: 0x040001BD RID: 445
		private string nameField;

		// Token: 0x040001BE RID: 446
		private string valueField;

		// Token: 0x040001BF RID: 447
		private string labelField;
	}
}
