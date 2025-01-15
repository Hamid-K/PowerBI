using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000004 RID: 4
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class TimeZoneInformation
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000C46C File Offset: 0x0000A66C
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000C474 File Offset: 0x0000A674
		public int Bias
		{
			get
			{
				return this.biasField;
			}
			set
			{
				this.biasField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000C47D File Offset: 0x0000A67D
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000C485 File Offset: 0x0000A685
		public int StandardBias
		{
			get
			{
				return this.standardBiasField;
			}
			set
			{
				this.standardBiasField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000C48E File Offset: 0x0000A68E
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000C496 File Offset: 0x0000A696
		public SYSTEMTIME StandardDate
		{
			get
			{
				return this.standardDateField;
			}
			set
			{
				this.standardDateField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000C49F File Offset: 0x0000A69F
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000C4A7 File Offset: 0x0000A6A7
		public int DaylightBias
		{
			get
			{
				return this.daylightBiasField;
			}
			set
			{
				this.daylightBiasField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
		public SYSTEMTIME DaylightDate
		{
			get
			{
				return this.daylightDateField;
			}
			set
			{
				this.daylightDateField = value;
			}
		}

		// Token: 0x04000100 RID: 256
		private int biasField;

		// Token: 0x04000101 RID: 257
		private int standardBiasField;

		// Token: 0x04000102 RID: 258
		private SYSTEMTIME standardDateField;

		// Token: 0x04000103 RID: 259
		private int daylightBiasField;

		// Token: 0x04000104 RID: 260
		private SYSTEMTIME daylightDateField;
	}
}
