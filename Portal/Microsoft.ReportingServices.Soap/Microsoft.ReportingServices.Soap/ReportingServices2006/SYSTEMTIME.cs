using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000112 RID: 274
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class SYSTEMTIME
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000165ED File Offset: 0x000147ED
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x000165F5 File Offset: 0x000147F5
		public short year
		{
			get
			{
				return this.yearField;
			}
			set
			{
				this.yearField = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000165FE File Offset: 0x000147FE
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00016606 File Offset: 0x00014806
		public short month
		{
			get
			{
				return this.monthField;
			}
			set
			{
				this.monthField = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0001660F File Offset: 0x0001480F
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x00016617 File Offset: 0x00014817
		public short dayOfWeek
		{
			get
			{
				return this.dayOfWeekField;
			}
			set
			{
				this.dayOfWeekField = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00016620 File Offset: 0x00014820
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00016628 File Offset: 0x00014828
		public short day
		{
			get
			{
				return this.dayField;
			}
			set
			{
				this.dayField = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00016631 File Offset: 0x00014831
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x00016639 File Offset: 0x00014839
		public short hour
		{
			get
			{
				return this.hourField;
			}
			set
			{
				this.hourField = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00016642 File Offset: 0x00014842
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0001664A File Offset: 0x0001484A
		public short minute
		{
			get
			{
				return this.minuteField;
			}
			set
			{
				this.minuteField = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00016653 File Offset: 0x00014853
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0001665B File Offset: 0x0001485B
		public short second
		{
			get
			{
				return this.secondField;
			}
			set
			{
				this.secondField = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00016664 File Offset: 0x00014864
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0001666C File Offset: 0x0001486C
		public short milliseconds
		{
			get
			{
				return this.millisecondsField;
			}
			set
			{
				this.millisecondsField = value;
			}
		}

		// Token: 0x04000337 RID: 823
		private short yearField;

		// Token: 0x04000338 RID: 824
		private short monthField;

		// Token: 0x04000339 RID: 825
		private short dayOfWeekField;

		// Token: 0x0400033A RID: 826
		private short dayField;

		// Token: 0x0400033B RID: 827
		private short hourField;

		// Token: 0x0400033C RID: 828
		private short minuteField;

		// Token: 0x0400033D RID: 829
		private short secondField;

		// Token: 0x0400033E RID: 830
		private short millisecondsField;
	}
}
