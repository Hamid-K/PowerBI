using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BEE RID: 11246
	[ChildElementInfo(typeof(Formula1))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formula2))]
	internal class DataValidation : OpenXmlCompositeElement
	{
		// Token: 0x17007E5B RID: 32347
		// (get) Token: 0x06017908 RID: 96520 RVA: 0x002E79FD File Offset: 0x002E5BFD
		public override string LocalName
		{
			get
			{
				return "dataValidation";
			}
		}

		// Token: 0x17007E5C RID: 32348
		// (get) Token: 0x06017909 RID: 96521 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E5D RID: 32349
		// (get) Token: 0x0601790A RID: 96522 RVA: 0x00338643 File Offset: 0x00336843
		internal override int ElementTypeId
		{
			get
			{
				return 11218;
			}
		}

		// Token: 0x0601790B RID: 96523 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E5E RID: 32350
		// (get) Token: 0x0601790C RID: 96524 RVA: 0x0033864A File Offset: 0x0033684A
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataValidation.attributeTagNames;
			}
		}

		// Token: 0x17007E5F RID: 32351
		// (get) Token: 0x0601790D RID: 96525 RVA: 0x00338651 File Offset: 0x00336851
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataValidation.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E60 RID: 32352
		// (get) Token: 0x0601790E RID: 96526 RVA: 0x002E7A19 File Offset: 0x002E5C19
		// (set) Token: 0x0601790F RID: 96527 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<DataValidationValues> Type
		{
			get
			{
				return (EnumValue<DataValidationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007E61 RID: 32353
		// (get) Token: 0x06017910 RID: 96528 RVA: 0x002E7A28 File Offset: 0x002E5C28
		// (set) Token: 0x06017911 RID: 96529 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "errorStyle")]
		public EnumValue<DataValidationErrorStyleValues> ErrorStyle
		{
			get
			{
				return (EnumValue<DataValidationErrorStyleValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007E62 RID: 32354
		// (get) Token: 0x06017912 RID: 96530 RVA: 0x002E7A37 File Offset: 0x002E5C37
		// (set) Token: 0x06017913 RID: 96531 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "imeMode")]
		public EnumValue<DataValidationImeModeValues> ImeMode
		{
			get
			{
				return (EnumValue<DataValidationImeModeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007E63 RID: 32355
		// (get) Token: 0x06017914 RID: 96532 RVA: 0x002E7A46 File Offset: 0x002E5C46
		// (set) Token: 0x06017915 RID: 96533 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "operator")]
		public EnumValue<DataValidationOperatorValues> Operator
		{
			get
			{
				return (EnumValue<DataValidationOperatorValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007E64 RID: 32356
		// (get) Token: 0x06017916 RID: 96534 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017917 RID: 96535 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "allowBlank")]
		public BooleanValue AllowBlank
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007E65 RID: 32357
		// (get) Token: 0x06017918 RID: 96536 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017919 RID: 96537 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "showDropDown")]
		public BooleanValue ShowDropDown
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007E66 RID: 32358
		// (get) Token: 0x0601791A RID: 96538 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0601791B RID: 96539 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "showInputMessage")]
		public BooleanValue ShowInputMessage
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007E67 RID: 32359
		// (get) Token: 0x0601791C RID: 96540 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0601791D RID: 96541 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "showErrorMessage")]
		public BooleanValue ShowErrorMessage
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007E68 RID: 32360
		// (get) Token: 0x0601791E RID: 96542 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601791F RID: 96543 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "errorTitle")]
		public StringValue ErrorTitle
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007E69 RID: 32361
		// (get) Token: 0x06017920 RID: 96544 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06017921 RID: 96545 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "error")]
		public StringValue Error
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007E6A RID: 32362
		// (get) Token: 0x06017922 RID: 96546 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06017923 RID: 96547 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "promptTitle")]
		public StringValue PromptTitle
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007E6B RID: 32363
		// (get) Token: 0x06017924 RID: 96548 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x06017925 RID: 96549 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "prompt")]
		public StringValue Prompt
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007E6C RID: 32364
		// (get) Token: 0x06017926 RID: 96550 RVA: 0x00338658 File Offset: 0x00336858
		// (set) Token: 0x06017927 RID: 96551 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "sqref")]
		public ListValue<StringValue> SequenceOfReferences
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x06017928 RID: 96552 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataValidation()
		{
		}

		// Token: 0x06017929 RID: 96553 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataValidation(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601792A RID: 96554 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataValidation(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601792B RID: 96555 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataValidation(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601792C RID: 96556 RVA: 0x00338668 File Offset: 0x00336868
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "formula1" == name)
			{
				return new Formula1();
			}
			if (22 == namespaceId && "formula2" == name)
			{
				return new Formula2();
			}
			return null;
		}

		// Token: 0x17007E6D RID: 32365
		// (get) Token: 0x0601792D RID: 96557 RVA: 0x0033869B File Offset: 0x0033689B
		internal override string[] ElementTagNames
		{
			get
			{
				return DataValidation.eleTagNames;
			}
		}

		// Token: 0x17007E6E RID: 32366
		// (get) Token: 0x0601792E RID: 96558 RVA: 0x003386A2 File Offset: 0x003368A2
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataValidation.eleNamespaceIds;
			}
		}

		// Token: 0x17007E6F RID: 32367
		// (get) Token: 0x0601792F RID: 96559 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007E70 RID: 32368
		// (get) Token: 0x06017930 RID: 96560 RVA: 0x003386A9 File Offset: 0x003368A9
		// (set) Token: 0x06017931 RID: 96561 RVA: 0x003386B2 File Offset: 0x003368B2
		public Formula1 Formula1
		{
			get
			{
				return base.GetElement<Formula1>(0);
			}
			set
			{
				base.SetElement<Formula1>(0, value);
			}
		}

		// Token: 0x17007E71 RID: 32369
		// (get) Token: 0x06017932 RID: 96562 RVA: 0x003386BC File Offset: 0x003368BC
		// (set) Token: 0x06017933 RID: 96563 RVA: 0x003386C5 File Offset: 0x003368C5
		public Formula2 Formula2
		{
			get
			{
				return base.GetElement<Formula2>(1);
			}
			set
			{
				base.SetElement<Formula2>(1, value);
			}
		}

		// Token: 0x06017934 RID: 96564 RVA: 0x003386D0 File Offset: 0x003368D0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<DataValidationValues>();
			}
			if (namespaceId == 0 && "errorStyle" == name)
			{
				return new EnumValue<DataValidationErrorStyleValues>();
			}
			if (namespaceId == 0 && "imeMode" == name)
			{
				return new EnumValue<DataValidationImeModeValues>();
			}
			if (namespaceId == 0 && "operator" == name)
			{
				return new EnumValue<DataValidationOperatorValues>();
			}
			if (namespaceId == 0 && "allowBlank" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showDropDown" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showInputMessage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showErrorMessage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "errorTitle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "error" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "promptTitle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "prompt" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sqref" == name)
			{
				return new ListValue<StringValue>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017935 RID: 96565 RVA: 0x00338803 File Offset: 0x00336A03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataValidation>(deep);
		}

		// Token: 0x06017936 RID: 96566 RVA: 0x0033880C File Offset: 0x00336A0C
		// Note: this type is marked as 'beforefieldinit'.
		static DataValidation()
		{
			byte[] array = new byte[13];
			DataValidation.attributeNamespaceIds = array;
			DataValidation.eleTagNames = new string[] { "formula1", "formula2" };
			DataValidation.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009CCC RID: 40140
		private const string tagName = "dataValidation";

		// Token: 0x04009CCD RID: 40141
		private const byte tagNsId = 22;

		// Token: 0x04009CCE RID: 40142
		internal const int ElementTypeIdConst = 11218;

		// Token: 0x04009CCF RID: 40143
		private static string[] attributeTagNames = new string[]
		{
			"type", "errorStyle", "imeMode", "operator", "allowBlank", "showDropDown", "showInputMessage", "showErrorMessage", "errorTitle", "error",
			"promptTitle", "prompt", "sqref"
		};

		// Token: 0x04009CD0 RID: 40144
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009CD1 RID: 40145
		private static readonly string[] eleTagNames;

		// Token: 0x04009CD2 RID: 40146
		private static readonly byte[] eleNamespaceIds;
	}
}
