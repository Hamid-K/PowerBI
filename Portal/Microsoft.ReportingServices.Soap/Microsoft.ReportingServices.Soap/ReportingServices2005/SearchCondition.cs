using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000237 RID: 567
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class SearchCondition : Property
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00022691 File Offset: 0x00020891
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x00022699 File Offset: 0x00020899
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

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x000226A2 File Offset: 0x000208A2
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x000226AA File Offset: 0x000208AA
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

		// Token: 0x040006B8 RID: 1720
		private ConditionEnum conditionField;

		// Token: 0x040006B9 RID: 1721
		private bool conditionFieldSpecified;
	}
}
