using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200022F RID: 559
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ModelDrillthroughReport
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0002250D File Offset: 0x0002070D
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x00022515 File Offset: 0x00020715
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0002251E File Offset: 0x0002071E
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x00022526 File Offset: 0x00020726
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

		// Token: 0x0400069A RID: 1690
		private string pathField;

		// Token: 0x0400069B RID: 1691
		private DrillthroughType typeField;
	}
}
