using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F1 RID: 9201
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ListItems), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class FormControlProperties : OpenXmlPartRootElement
	{
		// Token: 0x17004DFC RID: 19964
		// (get) Token: 0x06010C5A RID: 68698 RVA: 0x002E6EA2 File Offset: 0x002E50A2
		public override string LocalName
		{
			get
			{
				return "formControlPr";
			}
		}

		// Token: 0x17004DFD RID: 19965
		// (get) Token: 0x06010C5B RID: 68699 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DFE RID: 19966
		// (get) Token: 0x06010C5C RID: 68700 RVA: 0x002E6EA9 File Offset: 0x002E50A9
		internal override int ElementTypeId
		{
			get
			{
				return 12927;
			}
		}

		// Token: 0x06010C5D RID: 68701 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DFF RID: 19967
		// (get) Token: 0x06010C5E RID: 68702 RVA: 0x002E6EB0 File Offset: 0x002E50B0
		internal override string[] AttributeTagNames
		{
			get
			{
				return FormControlProperties.attributeTagNames;
			}
		}

		// Token: 0x17004E00 RID: 19968
		// (get) Token: 0x06010C5F RID: 68703 RVA: 0x002E6EB7 File Offset: 0x002E50B7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FormControlProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004E01 RID: 19969
		// (get) Token: 0x06010C60 RID: 68704 RVA: 0x002E6EBE File Offset: 0x002E50BE
		// (set) Token: 0x06010C61 RID: 68705 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "objectType")]
		public EnumValue<ObjectTypeValues> ObjectType
		{
			get
			{
				return (EnumValue<ObjectTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004E02 RID: 19970
		// (get) Token: 0x06010C62 RID: 68706 RVA: 0x002E6ECD File Offset: 0x002E50CD
		// (set) Token: 0x06010C63 RID: 68707 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "checked")]
		public EnumValue<CheckedValues> Checked
		{
			get
			{
				return (EnumValue<CheckedValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004E03 RID: 19971
		// (get) Token: 0x06010C64 RID: 68708 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010C65 RID: 68709 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "colored")]
		public BooleanValue Colored
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004E04 RID: 19972
		// (get) Token: 0x06010C66 RID: 68710 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06010C67 RID: 68711 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "dropLines")]
		public UInt32Value DropLines
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004E05 RID: 19973
		// (get) Token: 0x06010C68 RID: 68712 RVA: 0x002E6EDC File Offset: 0x002E50DC
		// (set) Token: 0x06010C69 RID: 68713 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "dropStyle")]
		public EnumValue<DropStyleValues> DropStyle
		{
			get
			{
				return (EnumValue<DropStyleValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004E06 RID: 19974
		// (get) Token: 0x06010C6A RID: 68714 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06010C6B RID: 68715 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "dx")]
		public UInt32Value ScrollBarWidth
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004E07 RID: 19975
		// (get) Token: 0x06010C6C RID: 68716 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06010C6D RID: 68717 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "firstButton")]
		public BooleanValue FirstButton
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

		// Token: 0x17004E08 RID: 19976
		// (get) Token: 0x06010C6E RID: 68718 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06010C6F RID: 68719 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "fmlaGroup")]
		public StringValue FmlaGroup
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004E09 RID: 19977
		// (get) Token: 0x06010C70 RID: 68720 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06010C71 RID: 68721 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "fmlaLink")]
		public StringValue FmlaLink
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

		// Token: 0x17004E0A RID: 19978
		// (get) Token: 0x06010C72 RID: 68722 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06010C73 RID: 68723 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "fmlaRange")]
		public StringValue FmlaRange
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

		// Token: 0x17004E0B RID: 19979
		// (get) Token: 0x06010C74 RID: 68724 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06010C75 RID: 68725 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "fmlaTxbx")]
		public StringValue FmlaTextbox
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

		// Token: 0x17004E0C RID: 19980
		// (get) Token: 0x06010C76 RID: 68726 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06010C77 RID: 68727 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "horiz")]
		public BooleanValue Horizontal
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17004E0D RID: 19981
		// (get) Token: 0x06010C78 RID: 68728 RVA: 0x002E6EFA File Offset: 0x002E50FA
		// (set) Token: 0x06010C79 RID: 68729 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "inc")]
		public UInt32Value Incremental
		{
			get
			{
				return (UInt32Value)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004E0E RID: 19982
		// (get) Token: 0x06010C7A RID: 68730 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06010C7B RID: 68731 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "justLastX")]
		public BooleanValue JustLastX
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004E0F RID: 19983
		// (get) Token: 0x06010C7C RID: 68732 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06010C7D RID: 68733 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "lockText")]
		public BooleanValue LockText
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004E10 RID: 19984
		// (get) Token: 0x06010C7E RID: 68734 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x06010C7F RID: 68735 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "max")]
		public UInt32Value Max
		{
			get
			{
				return (UInt32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17004E11 RID: 19985
		// (get) Token: 0x06010C80 RID: 68736 RVA: 0x002E6F1A File Offset: 0x002E511A
		// (set) Token: 0x06010C81 RID: 68737 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "min")]
		public UInt32Value Min
		{
			get
			{
				return (UInt32Value)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17004E12 RID: 19986
		// (get) Token: 0x06010C82 RID: 68738 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x06010C83 RID: 68739 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "multiSel")]
		public StringValue MultipleSelection
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17004E13 RID: 19987
		// (get) Token: 0x06010C84 RID: 68740 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x06010C85 RID: 68741 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "noThreeD")]
		public BooleanValue NoThreeD
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17004E14 RID: 19988
		// (get) Token: 0x06010C86 RID: 68742 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x06010C87 RID: 68743 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "noThreeD2")]
		public BooleanValue NoThreeD2
		{
			get
			{
				return (BooleanValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17004E15 RID: 19989
		// (get) Token: 0x06010C88 RID: 68744 RVA: 0x002E6F2A File Offset: 0x002E512A
		// (set) Token: 0x06010C89 RID: 68745 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "page")]
		public UInt32Value Page
		{
			get
			{
				return (UInt32Value)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17004E16 RID: 19990
		// (get) Token: 0x06010C8A RID: 68746 RVA: 0x002E6F3A File Offset: 0x002E513A
		// (set) Token: 0x06010C8B RID: 68747 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "sel")]
		public UInt32Value Selected
		{
			get
			{
				return (UInt32Value)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17004E17 RID: 19991
		// (get) Token: 0x06010C8C RID: 68748 RVA: 0x002E6F4A File Offset: 0x002E514A
		// (set) Token: 0x06010C8D RID: 68749 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "seltype")]
		public EnumValue<SelectionTypeValues> SelectionType
		{
			get
			{
				return (EnumValue<SelectionTypeValues>)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17004E18 RID: 19992
		// (get) Token: 0x06010C8E RID: 68750 RVA: 0x002E6F5A File Offset: 0x002E515A
		// (set) Token: 0x06010C8F RID: 68751 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "textHAlign")]
		public EnumValue<TextHorizontalAlignmentValues> TextHorizontalAlign
		{
			get
			{
				return (EnumValue<TextHorizontalAlignmentValues>)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17004E19 RID: 19993
		// (get) Token: 0x06010C90 RID: 68752 RVA: 0x002E6F6A File Offset: 0x002E516A
		// (set) Token: 0x06010C91 RID: 68753 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "textVAlign")]
		public EnumValue<TextVerticalAlignmentValues> TextVerticalAlign
		{
			get
			{
				return (EnumValue<TextVerticalAlignmentValues>)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17004E1A RID: 19994
		// (get) Token: 0x06010C92 RID: 68754 RVA: 0x002E6F7A File Offset: 0x002E517A
		// (set) Token: 0x06010C93 RID: 68755 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "val")]
		public UInt32Value Val
		{
			get
			{
				return (UInt32Value)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17004E1B RID: 19995
		// (get) Token: 0x06010C94 RID: 68756 RVA: 0x002E6F8A File Offset: 0x002E518A
		// (set) Token: 0x06010C95 RID: 68757 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "widthMin")]
		public UInt32Value MinimumWidth
		{
			get
			{
				return (UInt32Value)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17004E1C RID: 19996
		// (get) Token: 0x06010C96 RID: 68758 RVA: 0x002E6F9A File Offset: 0x002E519A
		// (set) Token: 0x06010C97 RID: 68759 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "editVal")]
		public EnumValue<EditValidationValues> EditVal
		{
			get
			{
				return (EnumValue<EditValidationValues>)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x17004E1D RID: 19997
		// (get) Token: 0x06010C98 RID: 68760 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x06010C99 RID: 68761 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "multiLine")]
		public BooleanValue MultipleLines
		{
			get
			{
				return (BooleanValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17004E1E RID: 19998
		// (get) Token: 0x06010C9A RID: 68762 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x06010C9B RID: 68763 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "verticalBar")]
		public BooleanValue VerticalBar
		{
			get
			{
				return (BooleanValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17004E1F RID: 19999
		// (get) Token: 0x06010C9C RID: 68764 RVA: 0x002CB9E8 File Offset: 0x002C9BE8
		// (set) Token: 0x06010C9D RID: 68765 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "passwordEdit")]
		public BooleanValue PasswordEdit
		{
			get
			{
				return (BooleanValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x06010C9E RID: 68766 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal FormControlProperties(ControlPropertiesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06010C9F RID: 68767 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ControlPropertiesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17004E20 RID: 20000
		// (get) Token: 0x06010CA0 RID: 68768 RVA: 0x002E6FAA File Offset: 0x002E51AA
		// (set) Token: 0x06010CA1 RID: 68769 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ControlPropertiesPart ControlPropertiesPart
		{
			get
			{
				return base.OpenXmlPart as ControlPropertiesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06010CA2 RID: 68770 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public FormControlProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CA3 RID: 68771 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public FormControlProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CA4 RID: 68772 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public FormControlProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010CA5 RID: 68773 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public FormControlProperties()
		{
		}

		// Token: 0x06010CA6 RID: 68774 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ControlPropertiesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06010CA7 RID: 68775 RVA: 0x002E6FB7 File Offset: 0x002E51B7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "itemLst" == name)
			{
				return new ListItems();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004E21 RID: 20001
		// (get) Token: 0x06010CA8 RID: 68776 RVA: 0x002E6FEA File Offset: 0x002E51EA
		internal override string[] ElementTagNames
		{
			get
			{
				return FormControlProperties.eleTagNames;
			}
		}

		// Token: 0x17004E22 RID: 20002
		// (get) Token: 0x06010CA9 RID: 68777 RVA: 0x002E6FF1 File Offset: 0x002E51F1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FormControlProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004E23 RID: 20003
		// (get) Token: 0x06010CAA RID: 68778 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004E24 RID: 20004
		// (get) Token: 0x06010CAB RID: 68779 RVA: 0x002E6FF8 File Offset: 0x002E51F8
		// (set) Token: 0x06010CAC RID: 68780 RVA: 0x002E7001 File Offset: 0x002E5201
		public ListItems ListItems
		{
			get
			{
				return base.GetElement<ListItems>(0);
			}
			set
			{
				base.SetElement<ListItems>(0, value);
			}
		}

		// Token: 0x17004E25 RID: 20005
		// (get) Token: 0x06010CAD RID: 68781 RVA: 0x002E700B File Offset: 0x002E520B
		// (set) Token: 0x06010CAE RID: 68782 RVA: 0x002E7014 File Offset: 0x002E5214
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06010CAF RID: 68783 RVA: 0x002E7020 File Offset: 0x002E5220
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "objectType" == name)
			{
				return new EnumValue<ObjectTypeValues>();
			}
			if (namespaceId == 0 && "checked" == name)
			{
				return new EnumValue<CheckedValues>();
			}
			if (namespaceId == 0 && "colored" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dropLines" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "dropStyle" == name)
			{
				return new EnumValue<DropStyleValues>();
			}
			if (namespaceId == 0 && "dx" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstButton" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fmlaGroup" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fmlaLink" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fmlaRange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fmlaTxbx" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "horiz" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "inc" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "justLastX" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "lockText" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "max" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "min" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "multiSel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "noThreeD" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noThreeD2" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "page" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sel" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "seltype" == name)
			{
				return new EnumValue<SelectionTypeValues>();
			}
			if (namespaceId == 0 && "textHAlign" == name)
			{
				return new EnumValue<TextHorizontalAlignmentValues>();
			}
			if (namespaceId == 0 && "textVAlign" == name)
			{
				return new EnumValue<TextVerticalAlignmentValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "widthMin" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "editVal" == name)
			{
				return new EnumValue<EditValidationValues>();
			}
			if (namespaceId == 0 && "multiLine" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "verticalBar" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "passwordEdit" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010CB0 RID: 68784 RVA: 0x002E72DF File Offset: 0x002E54DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormControlProperties>(deep);
		}

		// Token: 0x06010CB1 RID: 68785 RVA: 0x002E72E8 File Offset: 0x002E54E8
		// Note: this type is marked as 'beforefieldinit'.
		static FormControlProperties()
		{
			byte[] array = new byte[31];
			FormControlProperties.attributeNamespaceIds = array;
			FormControlProperties.eleTagNames = new string[] { "itemLst", "extLst" };
			FormControlProperties.eleNamespaceIds = new byte[] { 53, 53 };
		}

		// Token: 0x04007650 RID: 30288
		private const string tagName = "formControlPr";

		// Token: 0x04007651 RID: 30289
		private const byte tagNsId = 53;

		// Token: 0x04007652 RID: 30290
		internal const int ElementTypeIdConst = 12927;

		// Token: 0x04007653 RID: 30291
		private static string[] attributeTagNames = new string[]
		{
			"objectType", "checked", "colored", "dropLines", "dropStyle", "dx", "firstButton", "fmlaGroup", "fmlaLink", "fmlaRange",
			"fmlaTxbx", "horiz", "inc", "justLastX", "lockText", "max", "min", "multiSel", "noThreeD", "noThreeD2",
			"page", "sel", "seltype", "textHAlign", "textVAlign", "val", "widthMin", "editVal", "multiLine", "verticalBar",
			"passwordEdit"
		};

		// Token: 0x04007654 RID: 30292
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007655 RID: 30293
		private static readonly string[] eleTagNames;

		// Token: 0x04007656 RID: 30294
		private static readonly byte[] eleNamespaceIds;
	}
}
