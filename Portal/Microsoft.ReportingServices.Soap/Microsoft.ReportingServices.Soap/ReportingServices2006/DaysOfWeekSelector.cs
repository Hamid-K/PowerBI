using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000132 RID: 306
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DaysOfWeekSelector
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00016FBB File Offset: 0x000151BB
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00016FC3 File Offset: 0x000151C3
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

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00016FCC File Offset: 0x000151CC
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00016FD4 File Offset: 0x000151D4
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

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00016FDD File Offset: 0x000151DD
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00016FE5 File Offset: 0x000151E5
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00016FEE File Offset: 0x000151EE
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x00016FF6 File Offset: 0x000151F6
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

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00016FFF File Offset: 0x000151FF
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00017007 File Offset: 0x00015207
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

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00017010 File Offset: 0x00015210
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x00017018 File Offset: 0x00015218
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00017021 File Offset: 0x00015221
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x00017029 File Offset: 0x00015229
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

		// Token: 0x040003CB RID: 971
		private bool sundayField;

		// Token: 0x040003CC RID: 972
		private bool mondayField;

		// Token: 0x040003CD RID: 973
		private bool tuesdayField;

		// Token: 0x040003CE RID: 974
		private bool wednesdayField;

		// Token: 0x040003CF RID: 975
		private bool thursdayField;

		// Token: 0x040003D0 RID: 976
		private bool fridayField;

		// Token: 0x040003D1 RID: 977
		private bool saturdayField;
	}
}
