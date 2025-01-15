using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200007E RID: 126
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class Warning
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001B3BB File Offset: 0x000195BB
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0001B3C3 File Offset: 0x000195C3
		public string Code
		{
			get
			{
				return this.codeField;
			}
			set
			{
				this.codeField = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001B3CC File Offset: 0x000195CC
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001B3D4 File Offset: 0x000195D4
		public string Severity
		{
			get
			{
				return this.severityField;
			}
			set
			{
				this.severityField = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001B3DD File Offset: 0x000195DD
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0001B3E5 File Offset: 0x000195E5
		public string ObjectName
		{
			get
			{
				return this.objectNameField;
			}
			set
			{
				this.objectNameField = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001B3EE File Offset: 0x000195EE
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0001B3F6 File Offset: 0x000195F6
		public string ObjectType
		{
			get
			{
				return this.objectTypeField;
			}
			set
			{
				this.objectTypeField = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001B3FF File Offset: 0x000195FF
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0001B407 File Offset: 0x00019607
		public string Message
		{
			get
			{
				return this.messageField;
			}
			set
			{
				this.messageField = value;
			}
		}

		// Token: 0x04000173 RID: 371
		private string codeField;

		// Token: 0x04000174 RID: 372
		private string severityField;

		// Token: 0x04000175 RID: 373
		private string objectNameField;

		// Token: 0x04000176 RID: 374
		private string objectTypeField;

		// Token: 0x04000177 RID: 375
		private string messageField;
	}
}
