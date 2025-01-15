using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000218 RID: 536
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DaysOfWeekSelector
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00021F56 File Offset: 0x00020156
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x00021F5E File Offset: 0x0002015E
		public bool Sunday
		{
			get
			{
				return this.sundayField;
			}
			set
			{
				this.sundayField = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00021F67 File Offset: 0x00020167
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x00021F6F File Offset: 0x0002016F
		public bool Monday
		{
			get
			{
				return this.mondayField;
			}
			set
			{
				this.mondayField = value;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00021F78 File Offset: 0x00020178
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x00021F80 File Offset: 0x00020180
		public bool Tuesday
		{
			get
			{
				return this.tuesdayField;
			}
			set
			{
				this.tuesdayField = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00021F89 File Offset: 0x00020189
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x00021F91 File Offset: 0x00020191
		public bool Wednesday
		{
			get
			{
				return this.wednesdayField;
			}
			set
			{
				this.wednesdayField = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x00021F9A File Offset: 0x0002019A
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x00021FA2 File Offset: 0x000201A2
		public bool Thursday
		{
			get
			{
				return this.thursdayField;
			}
			set
			{
				this.thursdayField = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00021FAB File Offset: 0x000201AB
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x00021FB3 File Offset: 0x000201B3
		public bool Friday
		{
			get
			{
				return this.fridayField;
			}
			set
			{
				this.fridayField = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00021FBC File Offset: 0x000201BC
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x00021FC4 File Offset: 0x000201C4
		public bool Saturday
		{
			get
			{
				return this.saturdayField;
			}
			set
			{
				this.saturdayField = value;
			}
		}

		// Token: 0x04000620 RID: 1568
		private bool sundayField;

		// Token: 0x04000621 RID: 1569
		private bool mondayField;

		// Token: 0x04000622 RID: 1570
		private bool tuesdayField;

		// Token: 0x04000623 RID: 1571
		private bool wednesdayField;

		// Token: 0x04000624 RID: 1572
		private bool thursdayField;

		// Token: 0x04000625 RID: 1573
		private bool fridayField;

		// Token: 0x04000626 RID: 1574
		private bool saturdayField;
	}
}
