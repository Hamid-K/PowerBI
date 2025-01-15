using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000208 RID: 520
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DataSetDefinition
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		public Field[] Fields
		{
			get
			{
				return this.fieldsField;
			}
			set
			{
				this.fieldsField = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00021AD9 File Offset: 0x0001FCD9
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x00021AE1 File Offset: 0x0001FCE1
		public QueryDefinition Query
		{
			get
			{
				return this.queryField;
			}
			set
			{
				this.queryField = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00021AEA File Offset: 0x0001FCEA
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x00021AF2 File Offset: 0x0001FCF2
		public SensitivityEnum CaseSensitivity
		{
			get
			{
				return this.caseSensitivityField;
			}
			set
			{
				this.caseSensitivityField = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00021AFB File Offset: 0x0001FCFB
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x00021B03 File Offset: 0x0001FD03
		[XmlIgnore]
		public bool CaseSensitivitySpecified
		{
			get
			{
				return this.caseSensitivityFieldSpecified;
			}
			set
			{
				this.caseSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00021B0C File Offset: 0x0001FD0C
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x00021B14 File Offset: 0x0001FD14
		public string Collation
		{
			get
			{
				return this.collationField;
			}
			set
			{
				this.collationField = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00021B1D File Offset: 0x0001FD1D
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x00021B25 File Offset: 0x0001FD25
		public SensitivityEnum AccentSensitivity
		{
			get
			{
				return this.accentSensitivityField;
			}
			set
			{
				this.accentSensitivityField = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00021B2E File Offset: 0x0001FD2E
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x00021B36 File Offset: 0x0001FD36
		[XmlIgnore]
		public bool AccentSensitivitySpecified
		{
			get
			{
				return this.accentSensitivityFieldSpecified;
			}
			set
			{
				this.accentSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00021B3F File Offset: 0x0001FD3F
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00021B47 File Offset: 0x0001FD47
		public SensitivityEnum KanatypeSensitivity
		{
			get
			{
				return this.kanatypeSensitivityField;
			}
			set
			{
				this.kanatypeSensitivityField = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00021B50 File Offset: 0x0001FD50
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x00021B58 File Offset: 0x0001FD58
		[XmlIgnore]
		public bool KanatypeSensitivitySpecified
		{
			get
			{
				return this.kanatypeSensitivityFieldSpecified;
			}
			set
			{
				this.kanatypeSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00021B61 File Offset: 0x0001FD61
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x00021B69 File Offset: 0x0001FD69
		public SensitivityEnum WidthSensitivity
		{
			get
			{
				return this.widthSensitivityField;
			}
			set
			{
				this.widthSensitivityField = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00021B72 File Offset: 0x0001FD72
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x00021B7A File Offset: 0x0001FD7A
		[XmlIgnore]
		public bool WidthSensitivitySpecified
		{
			get
			{
				return this.widthSensitivityFieldSpecified;
			}
			set
			{
				this.widthSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00021B83 File Offset: 0x0001FD83
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x00021B8B File Offset: 0x0001FD8B
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x040005D9 RID: 1497
		private Field[] fieldsField;

		// Token: 0x040005DA RID: 1498
		private QueryDefinition queryField;

		// Token: 0x040005DB RID: 1499
		private SensitivityEnum caseSensitivityField;

		// Token: 0x040005DC RID: 1500
		private bool caseSensitivityFieldSpecified;

		// Token: 0x040005DD RID: 1501
		private string collationField;

		// Token: 0x040005DE RID: 1502
		private SensitivityEnum accentSensitivityField;

		// Token: 0x040005DF RID: 1503
		private bool accentSensitivityFieldSpecified;

		// Token: 0x040005E0 RID: 1504
		private SensitivityEnum kanatypeSensitivityField;

		// Token: 0x040005E1 RID: 1505
		private bool kanatypeSensitivityFieldSpecified;

		// Token: 0x040005E2 RID: 1506
		private SensitivityEnum widthSensitivityField;

		// Token: 0x040005E3 RID: 1507
		private bool widthSensitivityFieldSpecified;

		// Token: 0x040005E4 RID: 1508
		private string nameField;
	}
}
