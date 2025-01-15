using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000220 RID: 544
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DataSourcePrompt
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00022096 File Offset: 0x00020296
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x0002209E File Offset: 0x0002029E
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

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000220A7 File Offset: 0x000202A7
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x000220AF File Offset: 0x000202AF
		public string DataSourceID
		{
			get
			{
				return this.dataSourceIDField;
			}
			set
			{
				this.dataSourceIDField = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x000220B8 File Offset: 0x000202B8
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x000220C0 File Offset: 0x000202C0
		public string Prompt
		{
			get
			{
				return this.promptField;
			}
			set
			{
				this.promptField = value;
			}
		}

		// Token: 0x0400063C RID: 1596
		private string nameField;

		// Token: 0x0400063D RID: 1597
		private string dataSourceIDField;

		// Token: 0x0400063E RID: 1598
		private string promptField;
	}
}
