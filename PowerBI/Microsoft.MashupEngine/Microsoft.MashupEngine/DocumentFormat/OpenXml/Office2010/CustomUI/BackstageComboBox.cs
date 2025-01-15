using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022DC RID: 8924
	[ChildElementInfo(typeof(ItemBackstageItem), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageComboBox : OpenXmlCompositeElement
	{
		// Token: 0x1700457E RID: 17790
		// (get) Token: 0x0600F9C5 RID: 63941 RVA: 0x002CC6B7 File Offset: 0x002CA8B7
		public override string LocalName
		{
			get
			{
				return "comboBox";
			}
		}

		// Token: 0x1700457F RID: 17791
		// (get) Token: 0x0600F9C6 RID: 63942 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004580 RID: 17792
		// (get) Token: 0x0600F9C7 RID: 63943 RVA: 0x002D90A6 File Offset: 0x002D72A6
		internal override int ElementTypeId
		{
			get
			{
				return 13069;
			}
		}

		// Token: 0x0600F9C8 RID: 63944 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004581 RID: 17793
		// (get) Token: 0x0600F9C9 RID: 63945 RVA: 0x002D90AD File Offset: 0x002D72AD
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageComboBox.attributeTagNames;
			}
		}

		// Token: 0x17004582 RID: 17794
		// (get) Token: 0x0600F9CA RID: 63946 RVA: 0x002D90B4 File Offset: 0x002D72B4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageComboBox.attributeNamespaceIds;
			}
		}

		// Token: 0x17004583 RID: 17795
		// (get) Token: 0x0600F9CB RID: 63947 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F9CC RID: 63948 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004584 RID: 17796
		// (get) Token: 0x0600F9CD RID: 63949 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F9CE RID: 63950 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004585 RID: 17797
		// (get) Token: 0x0600F9CF RID: 63951 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F9D0 RID: 63952 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004586 RID: 17798
		// (get) Token: 0x0600F9D1 RID: 63953 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600F9D2 RID: 63954 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "alignLabel")]
		public EnumValue<ExpandValues> AlignLabel
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004587 RID: 17799
		// (get) Token: 0x0600F9D3 RID: 63955 RVA: 0x002D8858 File Offset: 0x002D6A58
		// (set) Token: 0x0600F9D4 RID: 63956 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004588 RID: 17800
		// (get) Token: 0x0600F9D5 RID: 63957 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600F9D6 RID: 63958 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17004589 RID: 17801
		// (get) Token: 0x0600F9D7 RID: 63959 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F9D8 RID: 63960 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700458A RID: 17802
		// (get) Token: 0x0600F9D9 RID: 63961 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F9DA RID: 63962 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x1700458B RID: 17803
		// (get) Token: 0x0600F9DB RID: 63963 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F9DC RID: 63964 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x1700458C RID: 17804
		// (get) Token: 0x0600F9DD RID: 63965 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F9DE RID: 63966 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700458D RID: 17805
		// (get) Token: 0x0600F9DF RID: 63967 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F9E0 RID: 63968 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x1700458E RID: 17806
		// (get) Token: 0x0600F9E1 RID: 63969 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F9E2 RID: 63970 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x1700458F RID: 17807
		// (get) Token: 0x0600F9E3 RID: 63971 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F9E4 RID: 63972 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004590 RID: 17808
		// (get) Token: 0x0600F9E5 RID: 63973 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F9E6 RID: 63974 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getText")]
		public StringValue GetText
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004591 RID: 17809
		// (get) Token: 0x0600F9E7 RID: 63975 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F9E8 RID: 63976 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "onChange")]
		public StringValue OnChange
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004592 RID: 17810
		// (get) Token: 0x0600F9E9 RID: 63977 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F9EA RID: 63978 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17004593 RID: 17811
		// (get) Token: 0x0600F9EB RID: 63979 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F9EC RID: 63980 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17004594 RID: 17812
		// (get) Token: 0x0600F9ED RID: 63981 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F9EE RID: 63982 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x17004595 RID: 17813
		// (get) Token: 0x0600F9EF RID: 63983 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F9F0 RID: 63984 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x0600F9F1 RID: 63985 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstageComboBox()
		{
		}

		// Token: 0x0600F9F2 RID: 63986 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstageComboBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F9F3 RID: 63987 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstageComboBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F9F4 RID: 63988 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstageComboBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F9F5 RID: 63989 RVA: 0x002D8ADA File Offset: 0x002D6CDA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "item" == name)
			{
				return new ItemBackstageItem();
			}
			return null;
		}

		// Token: 0x0600F9F6 RID: 63990 RVA: 0x002D90BC File Offset: 0x002D72BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "alignLabel" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onChange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sizeString" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemCount" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemID" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F9F7 RID: 63991 RVA: 0x002D9273 File Offset: 0x002D7473
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageComboBox>(deep);
		}

		// Token: 0x0600F9F8 RID: 63992 RVA: 0x002D927C File Offset: 0x002D747C
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageComboBox()
		{
			byte[] array = new byte[19];
			BackstageComboBox.attributeNamespaceIds = array;
		}

		// Token: 0x04007179 RID: 29049
		private const string tagName = "comboBox";

		// Token: 0x0400717A RID: 29050
		private const byte tagNsId = 57;

		// Token: 0x0400717B RID: 29051
		internal const int ElementTypeIdConst = 13069;

		// Token: 0x0400717C RID: 29052
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "alignLabel", "expand", "enabled", "getEnabled", "label", "getLabel", "visible",
			"getVisible", "keytip", "getKeytip", "getText", "onChange", "sizeString", "getItemCount", "getItemLabel", "getItemID"
		};

		// Token: 0x0400717D RID: 29053
		private static byte[] attributeNamespaceIds;
	}
}
