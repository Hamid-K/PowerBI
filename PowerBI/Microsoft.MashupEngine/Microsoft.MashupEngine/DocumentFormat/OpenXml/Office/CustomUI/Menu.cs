using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002281 RID: 8833
	[ChildElementInfo(typeof(UnsizedDynamicMenu))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedControlClone))]
	[ChildElementInfo(typeof(UnsizedToggleButton))]
	[ChildElementInfo(typeof(UnsizedSplitButton))]
	[ChildElementInfo(typeof(UnsizedMenu))]
	[ChildElementInfo(typeof(MenuSeparator))]
	[ChildElementInfo(typeof(UnsizedButton))]
	[ChildElementInfo(typeof(CheckBox))]
	[ChildElementInfo(typeof(UnsizedGallery))]
	internal class Menu : OpenXmlCompositeElement
	{
		// Token: 0x17003F6F RID: 16239
		// (get) Token: 0x0600ECF0 RID: 60656 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x17003F70 RID: 16240
		// (get) Token: 0x0600ECF1 RID: 60657 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003F71 RID: 16241
		// (get) Token: 0x0600ECF2 RID: 60658 RVA: 0x002CD7EE File Offset: 0x002CB9EE
		internal override int ElementTypeId
		{
			get
			{
				return 12592;
			}
		}

		// Token: 0x0600ECF3 RID: 60659 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003F72 RID: 16242
		// (get) Token: 0x0600ECF4 RID: 60660 RVA: 0x002CD7F5 File Offset: 0x002CB9F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Menu.attributeTagNames;
			}
		}

		// Token: 0x17003F73 RID: 16243
		// (get) Token: 0x0600ECF5 RID: 60661 RVA: 0x002CD7FC File Offset: 0x002CB9FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Menu.attributeNamespaceIds;
			}
		}

		// Token: 0x17003F74 RID: 16244
		// (get) Token: 0x0600ECF6 RID: 60662 RVA: 0x002CB2F7 File Offset: 0x002C94F7
		// (set) Token: 0x0600ECF7 RID: 60663 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "size")]
		public EnumValue<SizeValues> Size
		{
			get
			{
				return (EnumValue<SizeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003F75 RID: 16245
		// (get) Token: 0x0600ECF8 RID: 60664 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600ECF9 RID: 60665 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getSize")]
		public StringValue GetSize
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

		// Token: 0x17003F76 RID: 16246
		// (get) Token: 0x0600ECFA RID: 60666 RVA: 0x002CD803 File Offset: 0x002CBA03
		// (set) Token: 0x0600ECFB RID: 60667 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "itemSize")]
		public EnumValue<ItemSizeValues> ItemSize
		{
			get
			{
				return (EnumValue<ItemSizeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003F77 RID: 16247
		// (get) Token: 0x0600ECFC RID: 60668 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600ECFD RID: 60669 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17003F78 RID: 16248
		// (get) Token: 0x0600ECFE RID: 60670 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600ECFF RID: 60671 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17003F79 RID: 16249
		// (get) Token: 0x0600ED00 RID: 60672 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600ED01 RID: 60673 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003F7A RID: 16250
		// (get) Token: 0x0600ED02 RID: 60674 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600ED03 RID: 60675 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003F7B RID: 16251
		// (get) Token: 0x0600ED04 RID: 60676 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600ED05 RID: 60677 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003F7C RID: 16252
		// (get) Token: 0x0600ED06 RID: 60678 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600ED07 RID: 60679 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003F7D RID: 16253
		// (get) Token: 0x0600ED08 RID: 60680 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600ED09 RID: 60681 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003F7E RID: 16254
		// (get) Token: 0x0600ED0A RID: 60682 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600ED0B RID: 60683 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003F7F RID: 16255
		// (get) Token: 0x0600ED0C RID: 60684 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600ED0D RID: 60685 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003F80 RID: 16256
		// (get) Token: 0x0600ED0E RID: 60686 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600ED0F RID: 60687 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003F81 RID: 16257
		// (get) Token: 0x0600ED10 RID: 60688 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600ED11 RID: 60689 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003F82 RID: 16258
		// (get) Token: 0x0600ED12 RID: 60690 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600ED13 RID: 60691 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003F83 RID: 16259
		// (get) Token: 0x0600ED14 RID: 60692 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600ED15 RID: 60693 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003F84 RID: 16260
		// (get) Token: 0x0600ED16 RID: 60694 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600ED17 RID: 60695 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17003F85 RID: 16261
		// (get) Token: 0x0600ED18 RID: 60696 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600ED19 RID: 60697 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003F86 RID: 16262
		// (get) Token: 0x0600ED1A RID: 60698 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600ED1B RID: 60699 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003F87 RID: 16263
		// (get) Token: 0x0600ED1C RID: 60700 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600ED1D RID: 60701 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003F88 RID: 16264
		// (get) Token: 0x0600ED1E RID: 60702 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600ED1F RID: 60703 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003F89 RID: 16265
		// (get) Token: 0x0600ED20 RID: 60704 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600ED21 RID: 60705 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003F8A RID: 16266
		// (get) Token: 0x0600ED22 RID: 60706 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600ED23 RID: 60707 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003F8B RID: 16267
		// (get) Token: 0x0600ED24 RID: 60708 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600ED25 RID: 60709 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003F8C RID: 16268
		// (get) Token: 0x0600ED26 RID: 60710 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0600ED27 RID: 60711 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17003F8D RID: 16269
		// (get) Token: 0x0600ED28 RID: 60712 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600ED29 RID: 60713 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003F8E RID: 16270
		// (get) Token: 0x0600ED2A RID: 60714 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600ED2B RID: 60715 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17003F8F RID: 16271
		// (get) Token: 0x0600ED2C RID: 60716 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600ED2D RID: 60717 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003F90 RID: 16272
		// (get) Token: 0x0600ED2E RID: 60718 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600ED2F RID: 60719 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003F91 RID: 16273
		// (get) Token: 0x0600ED30 RID: 60720 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600ED31 RID: 60721 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003F92 RID: 16274
		// (get) Token: 0x0600ED32 RID: 60722 RVA: 0x002CB9E8 File Offset: 0x002C9BE8
		// (set) Token: 0x0600ED33 RID: 60723 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17003F93 RID: 16275
		// (get) Token: 0x0600ED34 RID: 60724 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600ED35 RID: 60725 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x0600ED36 RID: 60726 RVA: 0x00293ECF File Offset: 0x002920CF
		public Menu()
		{
		}

		// Token: 0x0600ED37 RID: 60727 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Menu(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600ED38 RID: 60728 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Menu(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600ED39 RID: 60729 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Menu(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600ED3A RID: 60730 RVA: 0x002CD814 File Offset: 0x002CBA14
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

		// Token: 0x0600ED3B RID: 60731 RVA: 0x002CD8FC File Offset: 0x002CBAFC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "size" == name)
			{
				return new EnumValue<SizeValues>();
			}
			if (namespaceId == 0 && "getSize" == name)
			{
				return new StringValue();
			}
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

		// Token: 0x0600ED3C RID: 60732 RVA: 0x002CDBD1 File Offset: 0x002CBDD1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Menu>(deep);
		}

		// Token: 0x0600ED3D RID: 60733 RVA: 0x002CDBDC File Offset: 0x002CBDDC
		// Note: this type is marked as 'beforefieldinit'.
		static Menu()
		{
			byte[] array = new byte[32];
			Menu.attributeNamespaceIds = array;
		}

		// Token: 0x04006FE4 RID: 28644
		private const string tagName = "menu";

		// Token: 0x04006FE5 RID: 28645
		private const byte tagNsId = 34;

		// Token: 0x04006FE6 RID: 28646
		internal const int ElementTypeIdConst = 12592;

		// Token: 0x04006FE7 RID: 28647
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "itemSize", "description", "getDescription", "id", "idQ", "idMso", "tag", "image",
			"imageMso", "getImage", "screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel",
			"insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel",
			"showImage", "getShowImage"
		};

		// Token: 0x04006FE8 RID: 28648
		private static byte[] attributeNamespaceIds;
	}
}
