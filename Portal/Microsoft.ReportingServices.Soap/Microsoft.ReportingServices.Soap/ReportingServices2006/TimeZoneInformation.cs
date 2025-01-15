using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000111 RID: 273
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class TimeZoneInformation
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00016590 File Offset: 0x00014790
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00016598 File Offset: 0x00014798
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000165A1 File Offset: 0x000147A1
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x000165A9 File Offset: 0x000147A9
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000165B2 File Offset: 0x000147B2
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x000165BA File Offset: 0x000147BA
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x000165C3 File Offset: 0x000147C3
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x000165CB File Offset: 0x000147CB
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000165D4 File Offset: 0x000147D4
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x000165DC File Offset: 0x000147DC
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

		// Token: 0x04000332 RID: 818
		private int biasField;

		// Token: 0x04000333 RID: 819
		private int standardBiasField;

		// Token: 0x04000334 RID: 820
		private SYSTEMTIME standardDateField;

		// Token: 0x04000335 RID: 821
		private int daylightBiasField;

		// Token: 0x04000336 RID: 822
		private SYSTEMTIME daylightDateField;
	}
}
