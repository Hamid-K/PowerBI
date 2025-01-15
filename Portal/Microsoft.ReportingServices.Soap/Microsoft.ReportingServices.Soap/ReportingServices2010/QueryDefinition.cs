using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000035 RID: 53
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class QueryDefinition
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000D324 File Offset: 0x0000B524
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000D32C File Offset: 0x0000B52C
		public string CommandType
		{
			get
			{
				return this.commandTypeField;
			}
			set
			{
				this.commandTypeField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000D335 File Offset: 0x0000B535
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000D33D File Offset: 0x0000B53D
		public string CommandText
		{
			get
			{
				return this.commandTextField;
			}
			set
			{
				this.commandTextField = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000D346 File Offset: 0x0000B546
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0000D34E File Offset: 0x0000B54E
		public int Timeout
		{
			get
			{
				return this.timeoutField;
			}
			set
			{
				this.timeoutField = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000D357 File Offset: 0x0000B557
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0000D35F File Offset: 0x0000B55F
		[XmlIgnore]
		public bool TimeoutSpecified
		{
			get
			{
				return this.timeoutFieldSpecified;
			}
			set
			{
				this.timeoutFieldSpecified = value;
			}
		}

		// Token: 0x040001D6 RID: 470
		private string commandTypeField;

		// Token: 0x040001D7 RID: 471
		private string commandTextField;

		// Token: 0x040001D8 RID: 472
		private int timeoutField;

		// Token: 0x040001D9 RID: 473
		private bool timeoutFieldSpecified;
	}
}
