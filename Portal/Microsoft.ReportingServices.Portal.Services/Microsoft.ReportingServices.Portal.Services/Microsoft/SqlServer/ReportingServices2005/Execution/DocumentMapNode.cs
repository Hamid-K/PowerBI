using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000078 RID: 120
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class DocumentMapNode
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001B2D6 File Offset: 0x000194D6
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0001B2DE File Offset: 0x000194DE
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

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001B2E7 File Offset: 0x000194E7
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0001B2EF File Offset: 0x000194EF
		public string UniqueName
		{
			get
			{
				return this.uniqueNameField;
			}
			set
			{
				this.uniqueNameField = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001B2F8 File Offset: 0x000194F8
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001B300 File Offset: 0x00019500
		public DocumentMapNode[] Children
		{
			get
			{
				return this.childrenField;
			}
			set
			{
				this.childrenField = value;
			}
		}

		// Token: 0x04000166 RID: 358
		private string labelField;

		// Token: 0x04000167 RID: 359
		private string uniqueNameField;

		// Token: 0x04000168 RID: 360
		private DocumentMapNode[] childrenField;
	}
}
