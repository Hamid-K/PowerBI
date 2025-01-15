using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200014D RID: 333
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ModelDrillthroughReport
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0001765E File Offset: 0x0001585E
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x00017666 File Offset: 0x00015866
		public string Path
		{
			get
			{
				return this.pathField;
			}
			set
			{
				this.pathField = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0001766F File Offset: 0x0001586F
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00017677 File Offset: 0x00015877
		public DrillthroughType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x04000452 RID: 1106
		private string pathField;

		// Token: 0x04000453 RID: 1107
		private DrillthroughType typeField;
	}
}
