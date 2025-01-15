using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200013B RID: 315
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataSourcePrompt
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00017114 File Offset: 0x00015314
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0001711C File Offset: 0x0001531C
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00017125 File Offset: 0x00015325
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0001712D File Offset: 0x0001532D
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

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00017136 File Offset: 0x00015336
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x0001713E File Offset: 0x0001533E
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

		// Token: 0x040003E8 RID: 1000
		private string nameField;

		// Token: 0x040003E9 RID: 1001
		private string dataSourceIDField;

		// Token: 0x040003EA RID: 1002
		private string promptField;
	}
}
