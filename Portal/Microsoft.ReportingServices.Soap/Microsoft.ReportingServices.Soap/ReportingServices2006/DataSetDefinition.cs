using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000123 RID: 291
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataSetDefinition
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00016B46 File Offset: 0x00014D46
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00016B4E File Offset: 0x00014D4E
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00016B57 File Offset: 0x00014D57
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00016B5F File Offset: 0x00014D5F
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

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00016B68 File Offset: 0x00014D68
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00016B70 File Offset: 0x00014D70
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

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00016B79 File Offset: 0x00014D79
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00016B81 File Offset: 0x00014D81
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00016B8A File Offset: 0x00014D8A
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00016B92 File Offset: 0x00014D92
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00016B9B File Offset: 0x00014D9B
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00016BA3 File Offset: 0x00014DA3
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

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00016BAC File Offset: 0x00014DAC
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x00016BB4 File Offset: 0x00014DB4
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

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00016BBD File Offset: 0x00014DBD
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00016BC5 File Offset: 0x00014DC5
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

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00016BCE File Offset: 0x00014DCE
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00016BD6 File Offset: 0x00014DD6
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00016BDF File Offset: 0x00014DDF
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00016BE7 File Offset: 0x00014DE7
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

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00016BF0 File Offset: 0x00014DF0
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00016BF8 File Offset: 0x00014DF8
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00016C01 File Offset: 0x00014E01
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00016C09 File Offset: 0x00014E09
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

		// Token: 0x04000385 RID: 901
		private Field[] fieldsField;

		// Token: 0x04000386 RID: 902
		private QueryDefinition queryField;

		// Token: 0x04000387 RID: 903
		private SensitivityEnum caseSensitivityField;

		// Token: 0x04000388 RID: 904
		private bool caseSensitivityFieldSpecified;

		// Token: 0x04000389 RID: 905
		private string collationField;

		// Token: 0x0400038A RID: 906
		private SensitivityEnum accentSensitivityField;

		// Token: 0x0400038B RID: 907
		private bool accentSensitivityFieldSpecified;

		// Token: 0x0400038C RID: 908
		private SensitivityEnum kanatypeSensitivityField;

		// Token: 0x0400038D RID: 909
		private bool kanatypeSensitivityFieldSpecified;

		// Token: 0x0400038E RID: 910
		private SensitivityEnum widthSensitivityField;

		// Token: 0x0400038F RID: 911
		private bool widthSensitivityFieldSpecified;

		// Token: 0x04000390 RID: 912
		private string nameField;
	}
}
