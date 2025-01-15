using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000215 RID: 533
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class MonthsOfYearSelector
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00021E2E File Offset: 0x0002002E
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x00021E36 File Offset: 0x00020036
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

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00021E3F File Offset: 0x0002003F
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x00021E47 File Offset: 0x00020047
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00021E50 File Offset: 0x00020050
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x00021E58 File Offset: 0x00020058
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

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00021E61 File Offset: 0x00020061
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x00021E69 File Offset: 0x00020069
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

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00021E72 File Offset: 0x00020072
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00021E7A File Offset: 0x0002007A
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

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00021E83 File Offset: 0x00020083
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00021E8B File Offset: 0x0002008B
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00021E94 File Offset: 0x00020094
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00021E9C File Offset: 0x0002009C
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00021EA5 File Offset: 0x000200A5
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00021EAD File Offset: 0x000200AD
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

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00021EB6 File Offset: 0x000200B6
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00021EBE File Offset: 0x000200BE
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

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00021EC7 File Offset: 0x000200C7
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00021ECF File Offset: 0x000200CF
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00021ED8 File Offset: 0x000200D8
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00021EE0 File Offset: 0x000200E0
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00021EE9 File Offset: 0x000200E9
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x00021EF1 File Offset: 0x000200F1
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

		// Token: 0x04000610 RID: 1552
		private bool januaryField;

		// Token: 0x04000611 RID: 1553
		private bool februaryField;

		// Token: 0x04000612 RID: 1554
		private bool marchField;

		// Token: 0x04000613 RID: 1555
		private bool aprilField;

		// Token: 0x04000614 RID: 1556
		private bool mayField;

		// Token: 0x04000615 RID: 1557
		private bool juneField;

		// Token: 0x04000616 RID: 1558
		private bool julyField;

		// Token: 0x04000617 RID: 1559
		private bool augustField;

		// Token: 0x04000618 RID: 1560
		private bool septemberField;

		// Token: 0x04000619 RID: 1561
		private bool octoberField;

		// Token: 0x0400061A RID: 1562
		private bool novemberField;

		// Token: 0x0400061B RID: 1563
		private bool decemberField;
	}
}
