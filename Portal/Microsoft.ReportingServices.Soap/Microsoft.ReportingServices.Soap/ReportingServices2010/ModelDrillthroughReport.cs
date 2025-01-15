using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000022 RID: 34
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ModelDrillthroughReport
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0000CDDC File Offset: 0x0000AFDC
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000CDE5 File Offset: 0x0000AFE5
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0000CDED File Offset: 0x0000AFED
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

		// Token: 0x04000186 RID: 390
		private string pathField;

		// Token: 0x04000187 RID: 391
		private DrillthroughType typeField;
	}
}
