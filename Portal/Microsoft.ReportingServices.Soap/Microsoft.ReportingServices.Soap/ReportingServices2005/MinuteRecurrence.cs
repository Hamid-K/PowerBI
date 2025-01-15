using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000216 RID: 534
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class MinuteRecurrence : RecurrencePattern
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00021F02 File Offset: 0x00020102
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x00021F0A File Offset: 0x0002010A
		public int MinutesInterval
		{
			get
			{
				return this.minutesIntervalField;
			}
			set
			{
				this.minutesIntervalField = value;
			}
		}

		// Token: 0x0400061C RID: 1564
		private int minutesIntervalField;
	}
}
