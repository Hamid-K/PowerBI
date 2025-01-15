using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000005 RID: 5
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class SYSTEMTIME
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000C4C9 File Offset: 0x0000A6C9
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000C4D1 File Offset: 0x0000A6D1
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000C4E2 File Offset: 0x0000A6E2
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000C4EB File Offset: 0x0000A6EB
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000C504 File Offset: 0x0000A704
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000C50D File Offset: 0x0000A70D
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000C515 File Offset: 0x0000A715
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000C51E File Offset: 0x0000A71E
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000C526 File Offset: 0x0000A726
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000C52F File Offset: 0x0000A72F
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000C537 File Offset: 0x0000A737
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000C540 File Offset: 0x0000A740
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000C548 File Offset: 0x0000A748
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

		// Token: 0x04000105 RID: 261
		private short yearField;

		// Token: 0x04000106 RID: 262
		private short monthField;

		// Token: 0x04000107 RID: 263
		private short dayOfWeekField;

		// Token: 0x04000108 RID: 264
		private short dayField;

		// Token: 0x04000109 RID: 265
		private short hourField;

		// Token: 0x0400010A RID: 266
		private short minuteField;

		// Token: 0x0400010B RID: 267
		private short secondField;

		// Token: 0x0400010C RID: 268
		private short millisecondsField;
	}
}
