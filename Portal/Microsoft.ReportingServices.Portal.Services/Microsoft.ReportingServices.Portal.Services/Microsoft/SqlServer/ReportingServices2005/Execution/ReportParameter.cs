using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000086 RID: 134
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ReportParameter
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001B553 File Offset: 0x00019753
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0001B55B File Offset: 0x0001975B
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001B564 File Offset: 0x00019764
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001B56C File Offset: 0x0001976C
		public ParameterTypeEnum Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001B575 File Offset: 0x00019775
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0001B57D File Offset: 0x0001977D
		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return this.typeFieldSpecified;
			}
			set
			{
				this.typeFieldSpecified = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001B586 File Offset: 0x00019786
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001B58E File Offset: 0x0001978E
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001B597 File Offset: 0x00019797
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001B59F File Offset: 0x0001979F
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001B5A8 File Offset: 0x000197A8
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0001B5B0 File Offset: 0x000197B0
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001B5B9 File Offset: 0x000197B9
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0001B5C1 File Offset: 0x000197C1
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001B5CA File Offset: 0x000197CA
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0001B5D2 File Offset: 0x000197D2
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001B5DB File Offset: 0x000197DB
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001B5E3 File Offset: 0x000197E3
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001B5EC File Offset: 0x000197EC
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001B5F4 File Offset: 0x000197F4
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001B5FD File Offset: 0x000197FD
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0001B605 File Offset: 0x00019805
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001B60E File Offset: 0x0001980E
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0001B616 File Offset: 0x00019816
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001B61F File Offset: 0x0001981F
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0001B627 File Offset: 0x00019827
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001B630 File Offset: 0x00019830
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0001B638 File Offset: 0x00019838
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001B641 File Offset: 0x00019841
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0001B649 File Offset: 0x00019849
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001B652 File Offset: 0x00019852
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0001B65A File Offset: 0x0001985A
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001B663 File Offset: 0x00019863
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0001B66B File Offset: 0x0001986B
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

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001B674 File Offset: 0x00019874
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001B67C File Offset: 0x0001987C
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001B685 File Offset: 0x00019885
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0001B68D File Offset: 0x0001988D
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001B696 File Offset: 0x00019896
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0001B69E File Offset: 0x0001989E
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001B6A7 File Offset: 0x000198A7
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001B6AF File Offset: 0x000198AF
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001B6B8 File Offset: 0x000198B8
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001B6C0 File Offset: 0x000198C0
		public ParameterStateEnum State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001B6C9 File Offset: 0x000198C9
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0001B6D1 File Offset: 0x000198D1
		[XmlIgnore]
		public bool StateSpecified
		{
			get
			{
				return this.stateFieldSpecified;
			}
			set
			{
				this.stateFieldSpecified = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001B6DA File Offset: 0x000198DA
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001B6E2 File Offset: 0x000198E2
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

		// Token: 0x0400018B RID: 395
		private string nameField;

		// Token: 0x0400018C RID: 396
		private ParameterTypeEnum typeField;

		// Token: 0x0400018D RID: 397
		private bool typeFieldSpecified;

		// Token: 0x0400018E RID: 398
		private bool nullableField;

		// Token: 0x0400018F RID: 399
		private bool nullableFieldSpecified;

		// Token: 0x04000190 RID: 400
		private bool allowBlankField;

		// Token: 0x04000191 RID: 401
		private bool allowBlankFieldSpecified;

		// Token: 0x04000192 RID: 402
		private bool multiValueField;

		// Token: 0x04000193 RID: 403
		private bool multiValueFieldSpecified;

		// Token: 0x04000194 RID: 404
		private bool queryParameterField;

		// Token: 0x04000195 RID: 405
		private bool queryParameterFieldSpecified;

		// Token: 0x04000196 RID: 406
		private string promptField;

		// Token: 0x04000197 RID: 407
		private bool promptUserField;

		// Token: 0x04000198 RID: 408
		private bool promptUserFieldSpecified;

		// Token: 0x04000199 RID: 409
		private string[] dependenciesField;

		// Token: 0x0400019A RID: 410
		private bool validValuesQueryBasedField;

		// Token: 0x0400019B RID: 411
		private bool validValuesQueryBasedFieldSpecified;

		// Token: 0x0400019C RID: 412
		private ValidValue[] validValuesField;

		// Token: 0x0400019D RID: 413
		private bool defaultValuesQueryBasedField;

		// Token: 0x0400019E RID: 414
		private bool defaultValuesQueryBasedFieldSpecified;

		// Token: 0x0400019F RID: 415
		private string[] defaultValuesField;

		// Token: 0x040001A0 RID: 416
		private ParameterStateEnum stateField;

		// Token: 0x040001A1 RID: 417
		private bool stateFieldSpecified;

		// Token: 0x040001A2 RID: 418
		private string errorMessageField;
	}
}
