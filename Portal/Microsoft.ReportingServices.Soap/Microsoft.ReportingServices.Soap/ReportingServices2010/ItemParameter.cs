using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200001C RID: 28
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ItemParameter
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000CA96 File Offset: 0x0000AC96
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000CA9E File Offset: 0x0000AC9E
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000CAAF File Offset: 0x0000ACAF
		public string ParameterTypeName
		{
			get
			{
				return this.parameterTypeNameField;
			}
			set
			{
				this.parameterTypeNameField = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		public bool Nullable
		{
			get
			{
				return this.nullableField;
			}
			set
			{
				this.nullableField = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000CAC9 File Offset: 0x0000ACC9
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000CAD1 File Offset: 0x0000ACD1
		[XmlIgnore]
		public bool NullableSpecified
		{
			get
			{
				return this.nullableFieldSpecified;
			}
			set
			{
				this.nullableFieldSpecified = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000CADA File Offset: 0x0000ACDA
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0000CAE2 File Offset: 0x0000ACE2
		public bool AllowBlank
		{
			get
			{
				return this.allowBlankField;
			}
			set
			{
				this.allowBlankField = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000CAEB File Offset: 0x0000ACEB
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000CAF3 File Offset: 0x0000ACF3
		[XmlIgnore]
		public bool AllowBlankSpecified
		{
			get
			{
				return this.allowBlankFieldSpecified;
			}
			set
			{
				this.allowBlankFieldSpecified = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000CB04 File Offset: 0x0000AD04
		public bool MultiValue
		{
			get
			{
				return this.multiValueField;
			}
			set
			{
				this.multiValueField = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000CB0D File Offset: 0x0000AD0D
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000CB15 File Offset: 0x0000AD15
		[XmlIgnore]
		public bool MultiValueSpecified
		{
			get
			{
				return this.multiValueFieldSpecified;
			}
			set
			{
				this.multiValueFieldSpecified = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000CB1E File Offset: 0x0000AD1E
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000CB26 File Offset: 0x0000AD26
		public bool QueryParameter
		{
			get
			{
				return this.queryParameterField;
			}
			set
			{
				this.queryParameterField = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000CB2F File Offset: 0x0000AD2F
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000CB37 File Offset: 0x0000AD37
		[XmlIgnore]
		public bool QueryParameterSpecified
		{
			get
			{
				return this.queryParameterFieldSpecified;
			}
			set
			{
				this.queryParameterFieldSpecified = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000CB40 File Offset: 0x0000AD40
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public string Prompt
		{
			get
			{
				return this.promptField;
			}
			set
			{
				this.promptField = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000CB51 File Offset: 0x0000AD51
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000CB59 File Offset: 0x0000AD59
		public bool PromptUser
		{
			get
			{
				return this.promptUserField;
			}
			set
			{
				this.promptUserField = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000CB62 File Offset: 0x0000AD62
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		[XmlIgnore]
		public bool PromptUserSpecified
		{
			get
			{
				return this.promptUserFieldSpecified;
			}
			set
			{
				this.promptUserFieldSpecified = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000CB73 File Offset: 0x0000AD73
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		[XmlArrayItem("Dependency")]
		public string[] Dependencies
		{
			get
			{
				return this.dependenciesField;
			}
			set
			{
				this.dependenciesField = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000CB84 File Offset: 0x0000AD84
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		public bool ValidValuesQueryBased
		{
			get
			{
				return this.validValuesQueryBasedField;
			}
			set
			{
				this.validValuesQueryBasedField = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000CB95 File Offset: 0x0000AD95
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000CB9D File Offset: 0x0000AD9D
		[XmlIgnore]
		public bool ValidValuesQueryBasedSpecified
		{
			get
			{
				return this.validValuesQueryBasedFieldSpecified;
			}
			set
			{
				this.validValuesQueryBasedFieldSpecified = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		public ValidValue[] ValidValues
		{
			get
			{
				return this.validValuesField;
			}
			set
			{
				this.validValuesField = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000CBB7 File Offset: 0x0000ADB7
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000CBBF File Offset: 0x0000ADBF
		public bool DefaultValuesQueryBased
		{
			get
			{
				return this.defaultValuesQueryBasedField;
			}
			set
			{
				this.defaultValuesQueryBasedField = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		[XmlIgnore]
		public bool DefaultValuesQueryBasedSpecified
		{
			get
			{
				return this.defaultValuesQueryBasedFieldSpecified;
			}
			set
			{
				this.defaultValuesQueryBasedFieldSpecified = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000CBD9 File Offset: 0x0000ADD9
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		[XmlArrayItem("Value")]
		public string[] DefaultValues
		{
			get
			{
				return this.defaultValuesField;
			}
			set
			{
				this.defaultValuesField = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000CBEA File Offset: 0x0000ADEA
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		public string ParameterStateName
		{
			get
			{
				return this.parameterStateNameField;
			}
			set
			{
				this.parameterStateNameField = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000CBFB File Offset: 0x0000ADFB
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000CC03 File Offset: 0x0000AE03
		public string ErrorMessage
		{
			get
			{
				return this.errorMessageField;
			}
			set
			{
				this.errorMessageField = value;
			}
		}

		// Token: 0x04000158 RID: 344
		private string nameField;

		// Token: 0x04000159 RID: 345
		private string parameterTypeNameField;

		// Token: 0x0400015A RID: 346
		private bool nullableField;

		// Token: 0x0400015B RID: 347
		private bool nullableFieldSpecified;

		// Token: 0x0400015C RID: 348
		private bool allowBlankField;

		// Token: 0x0400015D RID: 349
		private bool allowBlankFieldSpecified;

		// Token: 0x0400015E RID: 350
		private bool multiValueField;

		// Token: 0x0400015F RID: 351
		private bool multiValueFieldSpecified;

		// Token: 0x04000160 RID: 352
		private bool queryParameterField;

		// Token: 0x04000161 RID: 353
		private bool queryParameterFieldSpecified;

		// Token: 0x04000162 RID: 354
		private string promptField;

		// Token: 0x04000163 RID: 355
		private bool promptUserField;

		// Token: 0x04000164 RID: 356
		private bool promptUserFieldSpecified;

		// Token: 0x04000165 RID: 357
		private string[] dependenciesField;

		// Token: 0x04000166 RID: 358
		private bool validValuesQueryBasedField;

		// Token: 0x04000167 RID: 359
		private bool validValuesQueryBasedFieldSpecified;

		// Token: 0x04000168 RID: 360
		private ValidValue[] validValuesField;

		// Token: 0x04000169 RID: 361
		private bool defaultValuesQueryBasedField;

		// Token: 0x0400016A RID: 362
		private bool defaultValuesQueryBasedFieldSpecified;

		// Token: 0x0400016B RID: 363
		private string[] defaultValuesField;

		// Token: 0x0400016C RID: 364
		private string parameterStateNameField;

		// Token: 0x0400016D RID: 365
		private string errorMessageField;
	}
}
