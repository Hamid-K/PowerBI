using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022DA RID: 8922
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ItemBackstageItem), FileFormatVersions.Office2010)]
	internal class BackstageDropDown : OpenXmlCompositeElement
	{
		// Token: 0x1700454B RID: 17739
		// (get) Token: 0x0600F957 RID: 63831 RVA: 0x002CCC17 File Offset: 0x002CAE17
		public override string LocalName
		{
			get
			{
				return "dropDown";
			}
		}

		// Token: 0x1700454C RID: 17740
		// (get) Token: 0x0600F958 RID: 63832 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700454D RID: 17741
		// (get) Token: 0x0600F959 RID: 63833 RVA: 0x002D8AC5 File Offset: 0x002D6CC5
		internal override int ElementTypeId
		{
			get
			{
				return 13067;
			}
		}

		// Token: 0x0600F95A RID: 63834 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700454E RID: 17742
		// (get) Token: 0x0600F95B RID: 63835 RVA: 0x002D8ACC File Offset: 0x002D6CCC
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageDropDown.attributeTagNames;
			}
		}

		// Token: 0x1700454F RID: 17743
		// (get) Token: 0x0600F95C RID: 63836 RVA: 0x002D8AD3 File Offset: 0x002D6CD3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageDropDown.attributeNamespaceIds;
			}
		}

		// Token: 0x17004550 RID: 17744
		// (get) Token: 0x0600F95D RID: 63837 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F95E RID: 63838 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004551 RID: 17745
		// (get) Token: 0x0600F95F RID: 63839 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F960 RID: 63840 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004552 RID: 17746
		// (get) Token: 0x0600F961 RID: 63841 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F962 RID: 63842 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004553 RID: 17747
		// (get) Token: 0x0600F963 RID: 63843 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600F964 RID: 63844 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004554 RID: 17748
		// (get) Token: 0x0600F965 RID: 63845 RVA: 0x002D8858 File Offset: 0x002D6A58
		// (set) Token: 0x0600F966 RID: 63846 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004555 RID: 17749
		// (get) Token: 0x0600F967 RID: 63847 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600F968 RID: 63848 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17004556 RID: 17750
		// (get) Token: 0x0600F969 RID: 63849 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F96A RID: 63850 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17004557 RID: 17751
		// (get) Token: 0x0600F96B RID: 63851 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F96C RID: 63852 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17004558 RID: 17752
		// (get) Token: 0x0600F96D RID: 63853 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F96E RID: 63854 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17004559 RID: 17753
		// (get) Token: 0x0600F96F RID: 63855 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F970 RID: 63856 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x1700455A RID: 17754
		// (get) Token: 0x0600F971 RID: 63857 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F972 RID: 63858 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x1700455B RID: 17755
		// (get) Token: 0x0600F973 RID: 63859 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F974 RID: 63860 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x1700455C RID: 17756
		// (get) Token: 0x0600F975 RID: 63861 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F976 RID: 63862 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x1700455D RID: 17757
		// (get) Token: 0x0600F977 RID: 63863 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F978 RID: 63864 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x1700455E RID: 17758
		// (get) Token: 0x0600F979 RID: 63865 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F97A RID: 63866 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x1700455F RID: 17759
		// (get) Token: 0x0600F97B RID: 63867 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F97C RID: 63868 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004560 RID: 17760
		// (get) Token: 0x0600F97D RID: 63869 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F97E RID: 63870 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004561 RID: 17761
		// (get) Token: 0x0600F97F RID: 63871 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F980 RID: 63872 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17004562 RID: 17762
		// (get) Token: 0x0600F981 RID: 63873 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F982 RID: 63874 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
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

		// Token: 0x17004563 RID: 17763
		// (get) Token: 0x0600F983 RID: 63875 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F984 RID: 63876 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17004564 RID: 17764
		// (get) Token: 0x0600F985 RID: 63877 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F986 RID: 63878 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17004565 RID: 17765
		// (get) Token: 0x0600F987 RID: 63879 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F988 RID: 63880 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17004566 RID: 17766
		// (get) Token: 0x0600F989 RID: 63881 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F98A RID: 63882 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x0600F98B RID: 63883 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstageDropDown()
		{
		}

		// Token: 0x0600F98C RID: 63884 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstageDropDown(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F98D RID: 63885 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstageDropDown(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F98E RID: 63886 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstageDropDown(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F98F RID: 63887 RVA: 0x002D8ADA File Offset: 0x002D6CDA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "item" == name)
			{
				return new ItemBackstageItem();
			}
			return null;
		}

		// Token: 0x0600F990 RID: 63888 RVA: 0x002D8AF8 File Offset: 0x002D6CF8
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
			if (namespaceId == 0 && "onAction" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "screentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "supertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSupertip" == name)
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
			if (namespaceId == 0 && "getSelectedItemIndex" == name)
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

		// Token: 0x0600F991 RID: 63889 RVA: 0x002D8D07 File Offset: 0x002D6F07
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageDropDown>(deep);
		}

		// Token: 0x0600F992 RID: 63890 RVA: 0x002D8D10 File Offset: 0x002D6F10
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageDropDown()
		{
			byte[] array = new byte[23];
			BackstageDropDown.attributeNamespaceIds = array;
		}

		// Token: 0x0400716F RID: 29039
		private const string tagName = "dropDown";

		// Token: 0x04007170 RID: 29040
		private const byte tagNsId = 57;

		// Token: 0x04007171 RID: 29041
		internal const int ElementTypeIdConst = 13067;

		// Token: 0x04007172 RID: 29042
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "alignLabel", "expand", "enabled", "getEnabled", "label", "getLabel", "visible",
			"getVisible", "onAction", "screentip", "getScreentip", "supertip", "getSupertip", "keytip", "getKeytip", "getSelectedItemIndex", "sizeString",
			"getItemCount", "getItemLabel", "getItemID"
		};

		// Token: 0x04007173 RID: 29043
		private static byte[] attributeNamespaceIds;
	}
}
