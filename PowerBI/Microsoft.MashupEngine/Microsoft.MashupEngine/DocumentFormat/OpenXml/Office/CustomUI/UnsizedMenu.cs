using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002275 RID: 8821
	[ChildElementInfo(typeof(UnsizedControlClone))]
	[ChildElementInfo(typeof(UnsizedDynamicMenu))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedGallery))]
	[ChildElementInfo(typeof(UnsizedButton))]
	[ChildElementInfo(typeof(CheckBox))]
	[ChildElementInfo(typeof(MenuSeparator))]
	[ChildElementInfo(typeof(UnsizedMenu))]
	[ChildElementInfo(typeof(UnsizedToggleButton))]
	[ChildElementInfo(typeof(UnsizedSplitButton))]
	internal class UnsizedMenu : OpenXmlCompositeElement
	{
		// Token: 0x17003DB6 RID: 15798
		// (get) Token: 0x0600E966 RID: 59750 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x17003DB7 RID: 15799
		// (get) Token: 0x0600E967 RID: 59751 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003DB8 RID: 15800
		// (get) Token: 0x0600E968 RID: 59752 RVA: 0x002CA22B File Offset: 0x002C842B
		internal override int ElementTypeId
		{
			get
			{
				return 12580;
			}
		}

		// Token: 0x0600E969 RID: 59753 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003DB9 RID: 15801
		// (get) Token: 0x0600E96A RID: 59754 RVA: 0x002CA232 File Offset: 0x002C8432
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsizedMenu.attributeTagNames;
			}
		}

		// Token: 0x17003DBA RID: 15802
		// (get) Token: 0x0600E96B RID: 59755 RVA: 0x002CA239 File Offset: 0x002C8439
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsizedMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x17003DBB RID: 15803
		// (get) Token: 0x0600E96C RID: 59756 RVA: 0x002CA240 File Offset: 0x002C8440
		// (set) Token: 0x0600E96D RID: 59757 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "itemSize")]
		public EnumValue<ItemSizeValues> ItemSize
		{
			get
			{
				return (EnumValue<ItemSizeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003DBC RID: 15804
		// (get) Token: 0x0600E96E RID: 59758 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E96F RID: 59759 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17003DBD RID: 15805
		// (get) Token: 0x0600E970 RID: 59760 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E971 RID: 59761 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17003DBE RID: 15806
		// (get) Token: 0x0600E972 RID: 59762 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E973 RID: 59763 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003DBF RID: 15807
		// (get) Token: 0x0600E974 RID: 59764 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E975 RID: 59765 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003DC0 RID: 15808
		// (get) Token: 0x0600E976 RID: 59766 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E977 RID: 59767 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003DC1 RID: 15809
		// (get) Token: 0x0600E978 RID: 59768 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E979 RID: 59769 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003DC2 RID: 15810
		// (get) Token: 0x0600E97A RID: 59770 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E97B RID: 59771 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003DC3 RID: 15811
		// (get) Token: 0x0600E97C RID: 59772 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E97D RID: 59773 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003DC4 RID: 15812
		// (get) Token: 0x0600E97E RID: 59774 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E97F RID: 59775 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003DC5 RID: 15813
		// (get) Token: 0x0600E980 RID: 59776 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E981 RID: 59777 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003DC6 RID: 15814
		// (get) Token: 0x0600E982 RID: 59778 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E983 RID: 59779 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003DC7 RID: 15815
		// (get) Token: 0x0600E984 RID: 59780 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E985 RID: 59781 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003DC8 RID: 15816
		// (get) Token: 0x0600E986 RID: 59782 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E987 RID: 59783 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003DC9 RID: 15817
		// (get) Token: 0x0600E988 RID: 59784 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600E989 RID: 59785 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17003DCA RID: 15818
		// (get) Token: 0x0600E98A RID: 59786 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E98B RID: 59787 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003DCB RID: 15819
		// (get) Token: 0x0600E98C RID: 59788 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E98D RID: 59789 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003DCC RID: 15820
		// (get) Token: 0x0600E98E RID: 59790 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E98F RID: 59791 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003DCD RID: 15821
		// (get) Token: 0x0600E990 RID: 59792 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E991 RID: 59793 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003DCE RID: 15822
		// (get) Token: 0x0600E992 RID: 59794 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E993 RID: 59795 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003DCF RID: 15823
		// (get) Token: 0x0600E994 RID: 59796 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600E995 RID: 59797 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003DD0 RID: 15824
		// (get) Token: 0x0600E996 RID: 59798 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E997 RID: 59799 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003DD1 RID: 15825
		// (get) Token: 0x0600E998 RID: 59800 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600E999 RID: 59801 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003DD2 RID: 15826
		// (get) Token: 0x0600E99A RID: 59802 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600E99B RID: 59803 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17003DD3 RID: 15827
		// (get) Token: 0x0600E99C RID: 59804 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600E99D RID: 59805 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17003DD4 RID: 15828
		// (get) Token: 0x0600E99E RID: 59806 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E99F RID: 59807 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17003DD5 RID: 15829
		// (get) Token: 0x0600E9A0 RID: 59808 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600E9A1 RID: 59809 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17003DD6 RID: 15830
		// (get) Token: 0x0600E9A2 RID: 59810 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E9A3 RID: 59811 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x17003DD7 RID: 15831
		// (get) Token: 0x0600E9A4 RID: 59812 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600E9A5 RID: 59813 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17003DD8 RID: 15832
		// (get) Token: 0x0600E9A6 RID: 59814 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E9A7 RID: 59815 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x0600E9A8 RID: 59816 RVA: 0x00293ECF File Offset: 0x002920CF
		public UnsizedMenu()
		{
		}

		// Token: 0x0600E9A9 RID: 59817 RVA: 0x00293ED7 File Offset: 0x002920D7
		public UnsizedMenu(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E9AA RID: 59818 RVA: 0x00293EE0 File Offset: 0x002920E0
		public UnsizedMenu(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E9AB RID: 59819 RVA: 0x00293EE9 File Offset: 0x002920E9
		public UnsizedMenu(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E9AC RID: 59820 RVA: 0x002CA250 File Offset: 0x002C8450
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "control" == name)
			{
				return new UnsizedControlClone();
			}
			if (34 == namespaceId && "button" == name)
			{
				return new UnsizedButton();
			}
			if (34 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (34 == namespaceId && "gallery" == name)
			{
				return new UnsizedGallery();
			}
			if (34 == namespaceId && "toggleButton" == name)
			{
				return new UnsizedToggleButton();
			}
			if (34 == namespaceId && "menuSeparator" == name)
			{
				return new MenuSeparator();
			}
			if (34 == namespaceId && "splitButton" == name)
			{
				return new UnsizedSplitButton();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new UnsizedMenu();
			}
			if (34 == namespaceId && "dynamicMenu" == name)
			{
				return new UnsizedDynamicMenu();
			}
			return null;
		}

		// Token: 0x0600E9AD RID: 59821 RVA: 0x002CA338 File Offset: 0x002C8538
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "itemSize" == name)
			{
				return new EnumValue<ItemSizeValues>();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "image" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getImage" == name)
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
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
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
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showImage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowImage" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E9AE RID: 59822 RVA: 0x002CA5E1 File Offset: 0x002C87E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnsizedMenu>(deep);
		}

		// Token: 0x0600E9AF RID: 59823 RVA: 0x002CA5EC File Offset: 0x002C87EC
		// Note: this type is marked as 'beforefieldinit'.
		static UnsizedMenu()
		{
			byte[] array = new byte[30];
			UnsizedMenu.attributeNamespaceIds = array;
		}

		// Token: 0x04006FA8 RID: 28584
		private const string tagName = "menu";

		// Token: 0x04006FA9 RID: 28585
		private const byte tagNsId = 34;

		// Token: 0x04006FAA RID: 28586
		internal const int ElementTypeIdConst = 12580;

		// Token: 0x04006FAB RID: 28587
		private static string[] attributeTagNames = new string[]
		{
			"itemSize", "description", "getDescription", "id", "idQ", "idMso", "tag", "image", "imageMso", "getImage",
			"screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FAC RID: 28588
		private static byte[] attributeNamespaceIds;
	}
}
