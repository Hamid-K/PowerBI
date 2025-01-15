using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200007C RID: 124
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ParameterValue : ParameterValueOrFieldReference
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001B355 File Offset: 0x00019555
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001B35D File Offset: 0x0001955D
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001B366 File Offset: 0x00019566
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001B36E File Offset: 0x0001956E
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001B377 File Offset: 0x00019577
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0001B37F File Offset: 0x0001957F
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

		// Token: 0x0400016D RID: 365
		private string nameField;

		// Token: 0x0400016E RID: 366
		private string valueField;

		// Token: 0x0400016F RID: 367
		private string labelField;
	}
}
