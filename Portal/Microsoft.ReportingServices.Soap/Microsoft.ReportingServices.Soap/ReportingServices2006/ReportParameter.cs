using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000144 RID: 324
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ReportParameter
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00017265 File Offset: 0x00015465
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0001726D File Offset: 0x0001546D
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

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00017276 File Offset: 0x00015476
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0001727E File Offset: 0x0001547E
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00017287 File Offset: 0x00015487
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0001728F File Offset: 0x0001548F
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

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00017298 File Offset: 0x00015498
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000172A0 File Offset: 0x000154A0
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000172A9 File Offset: 0x000154A9
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000172B1 File Offset: 0x000154B1
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

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x000172BA File Offset: 0x000154BA
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x000172C2 File Offset: 0x000154C2
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

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x000172CB File Offset: 0x000154CB
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x000172D3 File Offset: 0x000154D3
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x000172DC File Offset: 0x000154DC
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x000172E4 File Offset: 0x000154E4
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

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000172ED File Offset: 0x000154ED
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x000172F5 File Offset: 0x000154F5
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000172FE File Offset: 0x000154FE
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00017306 File Offset: 0x00015506
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0001730F File Offset: 0x0001550F
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00017317 File Offset: 0x00015517
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

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00017320 File Offset: 0x00015520
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00017328 File Offset: 0x00015528
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

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00017331 File Offset: 0x00015531
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00017339 File Offset: 0x00015539
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

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00017342 File Offset: 0x00015542
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0001734A File Offset: 0x0001554A
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

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00017353 File Offset: 0x00015553
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x0001735B File Offset: 0x0001555B
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00017364 File Offset: 0x00015564
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x0001736C File Offset: 0x0001556C
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

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00017375 File Offset: 0x00015575
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x0001737D File Offset: 0x0001557D
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

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00017386 File Offset: 0x00015586
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x0001738E File Offset: 0x0001558E
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00017397 File Offset: 0x00015597
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x0001739F File Offset: 0x0001559F
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

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x000173A8 File Offset: 0x000155A8
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x000173B0 File Offset: 0x000155B0
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

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000173B9 File Offset: 0x000155B9
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x000173C1 File Offset: 0x000155C1
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000173CA File Offset: 0x000155CA
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x000173D2 File Offset: 0x000155D2
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000173DB File Offset: 0x000155DB
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x000173E3 File Offset: 0x000155E3
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000173EC File Offset: 0x000155EC
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x000173F4 File Offset: 0x000155F4
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

		// Token: 0x04000406 RID: 1030
		private string nameField;

		// Token: 0x04000407 RID: 1031
		private ParameterTypeEnum typeField;

		// Token: 0x04000408 RID: 1032
		private bool typeFieldSpecified;

		// Token: 0x04000409 RID: 1033
		private bool nullableField;

		// Token: 0x0400040A RID: 1034
		private bool nullableFieldSpecified;

		// Token: 0x0400040B RID: 1035
		private bool allowBlankField;

		// Token: 0x0400040C RID: 1036
		private bool allowBlankFieldSpecified;

		// Token: 0x0400040D RID: 1037
		private bool multiValueField;

		// Token: 0x0400040E RID: 1038
		private bool multiValueFieldSpecified;

		// Token: 0x0400040F RID: 1039
		private bool queryParameterField;

		// Token: 0x04000410 RID: 1040
		private bool queryParameterFieldSpecified;

		// Token: 0x04000411 RID: 1041
		private string promptField;

		// Token: 0x04000412 RID: 1042
		private bool promptUserField;

		// Token: 0x04000413 RID: 1043
		private bool promptUserFieldSpecified;

		// Token: 0x04000414 RID: 1044
		private string[] dependenciesField;

		// Token: 0x04000415 RID: 1045
		private bool validValuesQueryBasedField;

		// Token: 0x04000416 RID: 1046
		private bool validValuesQueryBasedFieldSpecified;

		// Token: 0x04000417 RID: 1047
		private ValidValue[] validValuesField;

		// Token: 0x04000418 RID: 1048
		private bool defaultValuesQueryBasedField;

		// Token: 0x04000419 RID: 1049
		private bool defaultValuesQueryBasedFieldSpecified;

		// Token: 0x0400041A RID: 1050
		private string[] defaultValuesField;

		// Token: 0x0400041B RID: 1051
		private ParameterStateEnum stateField;

		// Token: 0x0400041C RID: 1052
		private bool stateFieldSpecified;

		// Token: 0x0400041D RID: 1053
		private string errorMessageField;
	}
}
