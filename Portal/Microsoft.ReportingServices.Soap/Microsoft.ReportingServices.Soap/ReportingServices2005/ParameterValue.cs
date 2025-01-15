using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000202 RID: 514
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ParameterValue : ParameterValueOrFieldReference
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00021889 File Offset: 0x0001FA89
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00021891 File Offset: 0x0001FA91
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

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0002189A File Offset: 0x0001FA9A
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x000218A2 File Offset: 0x0001FAA2
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x000218AB File Offset: 0x0001FAAB
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x000218B3 File Offset: 0x0001FAB3
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

		// Token: 0x040005BA RID: 1466
		private string nameField;

		// Token: 0x040005BB RID: 1467
		private string valueField;

		// Token: 0x040005BC RID: 1468
		private string labelField;
	}
}
