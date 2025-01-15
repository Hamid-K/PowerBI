using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200008A RID: 138
	[XmlInclude(typeof(ExecutionInfo3))]
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ExecutionInfo2 : ExecutionInfo
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001B7FB File Offset: 0x000199FB
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0001B803 File Offset: 0x00019A03
		public PageCountMode PageCountMode
		{
			get
			{
				return this.pageCountModeField;
			}
			set
			{
				this.pageCountModeField = value;
			}
		}

		// Token: 0x040001BE RID: 446
		private PageCountMode pageCountModeField;
	}
}
