using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000013 RID: 19
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DaysOfWeekSelector
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000C858 File Offset: 0x0000AA58
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000C860 File Offset: 0x0000AA60
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000C869 File Offset: 0x0000AA69
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000C871 File Offset: 0x0000AA71
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000C87A File Offset: 0x0000AA7A
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000C882 File Offset: 0x0000AA82
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000C88B File Offset: 0x0000AA8B
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000C893 File Offset: 0x0000AA93
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000C89C File Offset: 0x0000AA9C
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000C8AD File Offset: 0x0000AAAD
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000C8B5 File Offset: 0x0000AAB5
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000C8BE File Offset: 0x0000AABE
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000C8C6 File Offset: 0x0000AAC6
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

		// Token: 0x04000134 RID: 308
		private bool sundayField;

		// Token: 0x04000135 RID: 309
		private bool mondayField;

		// Token: 0x04000136 RID: 310
		private bool tuesdayField;

		// Token: 0x04000137 RID: 311
		private bool wednesdayField;

		// Token: 0x04000138 RID: 312
		private bool thursdayField;

		// Token: 0x04000139 RID: 313
		private bool fridayField;

		// Token: 0x0400013A RID: 314
		private bool saturdayField;
	}
}
