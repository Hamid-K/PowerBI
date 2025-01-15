using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000229 RID: 553
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ReportParameter
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000221E7 File Offset: 0x000203E7
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x000221EF File Offset: 0x000203EF
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

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000221F8 File Offset: 0x000203F8
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x00022200 File Offset: 0x00020400
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

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00022209 File Offset: 0x00020409
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x00022211 File Offset: 0x00020411
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0002221A File Offset: 0x0002041A
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x00022222 File Offset: 0x00020422
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

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0002222B File Offset: 0x0002042B
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x00022233 File Offset: 0x00020433
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0002223C File Offset: 0x0002043C
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x00022244 File Offset: 0x00020444
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

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0002224D File Offset: 0x0002044D
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x00022255 File Offset: 0x00020455
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

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0002225E File Offset: 0x0002045E
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x00022266 File Offset: 0x00020466
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

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0002226F File Offset: 0x0002046F
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x00022277 File Offset: 0x00020477
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

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00022280 File Offset: 0x00020480
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x00022288 File Offset: 0x00020488
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00022291 File Offset: 0x00020491
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x00022299 File Offset: 0x00020499
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

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x000222A2 File Offset: 0x000204A2
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x000222AA File Offset: 0x000204AA
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x000222B3 File Offset: 0x000204B3
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x000222BB File Offset: 0x000204BB
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

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x000222C4 File Offset: 0x000204C4
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x000222CC File Offset: 0x000204CC
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

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000222D5 File Offset: 0x000204D5
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x000222DD File Offset: 0x000204DD
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

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x000222E6 File Offset: 0x000204E6
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x000222EE File Offset: 0x000204EE
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

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x000222F7 File Offset: 0x000204F7
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x000222FF File Offset: 0x000204FF
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

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x00022308 File Offset: 0x00020508
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x00022310 File Offset: 0x00020510
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

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00022319 File Offset: 0x00020519
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x00022321 File Offset: 0x00020521
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0002232A File Offset: 0x0002052A
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x00022332 File Offset: 0x00020532
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0002233B File Offset: 0x0002053B
		// (set) Token: 0x0600151C RID: 5404 RVA: 0x00022343 File Offset: 0x00020543
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

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0002234C File Offset: 0x0002054C
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x00022354 File Offset: 0x00020554
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

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0002235D File Offset: 0x0002055D
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x00022365 File Offset: 0x00020565
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

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0002236E File Offset: 0x0002056E
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x00022376 File Offset: 0x00020576
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

		// Token: 0x04000659 RID: 1625
		private string nameField;

		// Token: 0x0400065A RID: 1626
		private ParameterTypeEnum typeField;

		// Token: 0x0400065B RID: 1627
		private bool typeFieldSpecified;

		// Token: 0x0400065C RID: 1628
		private bool nullableField;

		// Token: 0x0400065D RID: 1629
		private bool nullableFieldSpecified;

		// Token: 0x0400065E RID: 1630
		private bool allowBlankField;

		// Token: 0x0400065F RID: 1631
		private bool allowBlankFieldSpecified;

		// Token: 0x04000660 RID: 1632
		private bool multiValueField;

		// Token: 0x04000661 RID: 1633
		private bool multiValueFieldSpecified;

		// Token: 0x04000662 RID: 1634
		private bool queryParameterField;

		// Token: 0x04000663 RID: 1635
		private bool queryParameterFieldSpecified;

		// Token: 0x04000664 RID: 1636
		private string promptField;

		// Token: 0x04000665 RID: 1637
		private bool promptUserField;

		// Token: 0x04000666 RID: 1638
		private bool promptUserFieldSpecified;

		// Token: 0x04000667 RID: 1639
		private string[] dependenciesField;

		// Token: 0x04000668 RID: 1640
		private bool validValuesQueryBasedField;

		// Token: 0x04000669 RID: 1641
		private bool validValuesQueryBasedFieldSpecified;

		// Token: 0x0400066A RID: 1642
		private ValidValue[] validValuesField;

		// Token: 0x0400066B RID: 1643
		private bool defaultValuesQueryBasedField;

		// Token: 0x0400066C RID: 1644
		private bool defaultValuesQueryBasedFieldSpecified;

		// Token: 0x0400066D RID: 1645
		private string[] defaultValuesField;

		// Token: 0x0400066E RID: 1646
		private ParameterStateEnum stateField;

		// Token: 0x0400066F RID: 1647
		private bool stateFieldSpecified;

		// Token: 0x04000670 RID: 1648
		private string errorMessageField;
	}
}
