using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000015 RID: 21
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class MonthsOfYearSelector
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000C901 File Offset: 0x0000AB01
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000C909 File Offset: 0x0000AB09
		public bool January
		{
			get
			{
				return this.januaryField;
			}
			set
			{
				this.januaryField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000C912 File Offset: 0x0000AB12
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000C91A File Offset: 0x0000AB1A
		public bool February
		{
			get
			{
				return this.februaryField;
			}
			set
			{
				this.februaryField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000C923 File Offset: 0x0000AB23
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000C92B File Offset: 0x0000AB2B
		public bool March
		{
			get
			{
				return this.marchField;
			}
			set
			{
				this.marchField = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000C934 File Offset: 0x0000AB34
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public bool April
		{
			get
			{
				return this.aprilField;
			}
			set
			{
				this.aprilField = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000C945 File Offset: 0x0000AB45
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000C94D File Offset: 0x0000AB4D
		public bool May
		{
			get
			{
				return this.mayField;
			}
			set
			{
				this.mayField = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000C956 File Offset: 0x0000AB56
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000C95E File Offset: 0x0000AB5E
		public bool June
		{
			get
			{
				return this.juneField;
			}
			set
			{
				this.juneField = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000C967 File Offset: 0x0000AB67
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000C96F File Offset: 0x0000AB6F
		public bool July
		{
			get
			{
				return this.julyField;
			}
			set
			{
				this.julyField = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000C978 File Offset: 0x0000AB78
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0000C980 File Offset: 0x0000AB80
		public bool August
		{
			get
			{
				return this.augustField;
			}
			set
			{
				this.augustField = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000C989 File Offset: 0x0000AB89
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000C991 File Offset: 0x0000AB91
		public bool September
		{
			get
			{
				return this.septemberField;
			}
			set
			{
				this.septemberField = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000C99A File Offset: 0x0000AB9A
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000C9A2 File Offset: 0x0000ABA2
		public bool October
		{
			get
			{
				return this.octoberField;
			}
			set
			{
				this.octoberField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000C9AB File Offset: 0x0000ABAB
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0000C9B3 File Offset: 0x0000ABB3
		public bool November
		{
			get
			{
				return this.novemberField;
			}
			set
			{
				this.novemberField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public bool December
		{
			get
			{
				return this.decemberField;
			}
			set
			{
				this.decemberField = value;
			}
		}

		// Token: 0x0400013D RID: 317
		private bool januaryField;

		// Token: 0x0400013E RID: 318
		private bool februaryField;

		// Token: 0x0400013F RID: 319
		private bool marchField;

		// Token: 0x04000140 RID: 320
		private bool aprilField;

		// Token: 0x04000141 RID: 321
		private bool mayField;

		// Token: 0x04000142 RID: 322
		private bool juneField;

		// Token: 0x04000143 RID: 323
		private bool julyField;

		// Token: 0x04000144 RID: 324
		private bool augustField;

		// Token: 0x04000145 RID: 325
		private bool septemberField;

		// Token: 0x04000146 RID: 326
		private bool octoberField;

		// Token: 0x04000147 RID: 327
		private bool novemberField;

		// Token: 0x04000148 RID: 328
		private bool decemberField;
	}
}
