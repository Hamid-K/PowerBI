using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200003C RID: 60
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class SearchCondition
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000D4FD File Offset: 0x0000B6FD
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000D505 File Offset: 0x0000B705
		public ConditionEnum Condition
		{
			get
			{
				return this.conditionField;
			}
			set
			{
				this.conditionField = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000D50E File Offset: 0x0000B70E
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000D516 File Offset: 0x0000B716
		[XmlIgnore]
		public bool ConditionSpecified
		{
			get
			{
				return this.conditionFieldSpecified;
			}
			set
			{
				this.conditionFieldSpecified = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000D51F File Offset: 0x0000B71F
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000D527 File Offset: 0x0000B727
		[XmlArrayItem("Value")]
		public string[] Values
		{
			get
			{
				return this.valuesField;
			}
			set
			{
				this.valuesField = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000D530 File Offset: 0x0000B730
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0000D538 File Offset: 0x0000B738
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

		// Token: 0x040001F3 RID: 499
		private ConditionEnum conditionField;

		// Token: 0x040001F4 RID: 500
		private bool conditionFieldSpecified;

		// Token: 0x040001F5 RID: 501
		private string[] valuesField;

		// Token: 0x040001F6 RID: 502
		private string nameField;
	}
}
