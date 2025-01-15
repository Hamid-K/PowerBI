using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002278 RID: 8824
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MenuWithTitle))]
	[ChildElementInfo(typeof(UnsizedDynamicMenu))]
	[ChildElementInfo(typeof(UnsizedToggleButton))]
	[ChildElementInfo(typeof(CheckBox))]
	[ChildElementInfo(typeof(SplitButtonWithTitle))]
	[ChildElementInfo(typeof(UnsizedButton))]
	[ChildElementInfo(typeof(UnsizedControlClone))]
	[ChildElementInfo(typeof(UnsizedGallery))]
	[ChildElementInfo(typeof(MenuSeparator))]
	internal class MenuWithTitle : OpenXmlCompositeElement
	{
		// Token: 0x17003E12 RID: 15890
		// (get) Token: 0x0600EA26 RID: 59942 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x17003E13 RID: 15891
		// (get) Token: 0x0600EA27 RID: 59943 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003E14 RID: 15892
		// (get) Token: 0x0600EA28 RID: 59944 RVA: 0x002CADF4 File Offset: 0x002C8FF4
		internal override int ElementTypeId
		{
			get
			{
				return 12583;
			}
		}

		// Token: 0x0600EA29 RID: 59945 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003E15 RID: 15893
		// (get) Token: 0x0600EA2A RID: 59946 RVA: 0x002CADFB File Offset: 0x002C8FFB
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuWithTitle.attributeTagNames;
			}
		}

		// Token: 0x17003E16 RID: 15894
		// (get) Token: 0x0600EA2B RID: 59947 RVA: 0x002CAE02 File Offset: 0x002C9002
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuWithTitle.attributeNamespaceIds;
			}
		}

		// Token: 0x17003E17 RID: 15895
		// (get) Token: 0x0600EA2C RID: 59948 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EA2D RID: 59949 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003E18 RID: 15896
		// (get) Token: 0x0600EA2E RID: 59950 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EA2F RID: 59951 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003E19 RID: 15897
		// (get) Token: 0x0600EA30 RID: 59952 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EA31 RID: 59953 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003E1A RID: 15898
		// (get) Token: 0x0600EA32 RID: 59954 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EA33 RID: 59955 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003E1B RID: 15899
		// (get) Token: 0x0600EA34 RID: 59956 RVA: 0x002CAE09 File Offset: 0x002C9009
		// (set) Token: 0x0600EA35 RID: 59957 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "itemSize")]
		public EnumValue<ItemSizeValues> ItemSize
		{
			get
			{
				return (EnumValue<ItemSizeValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003E1C RID: 15900
		// (get) Token: 0x0600EA36 RID: 59958 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EA37 RID: 59959 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x17003E1D RID: 15901
		// (get) Token: 0x0600EA38 RID: 59960 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EA39 RID: 59961 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getTitle")]
		public StringValue GetTitle
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

		// Token: 0x17003E1E RID: 15902
		// (get) Token: 0x0600EA3A RID: 59962 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EA3B RID: 59963 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003E1F RID: 15903
		// (get) Token: 0x0600EA3C RID: 59964 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EA3D RID: 59965 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003E20 RID: 15904
		// (get) Token: 0x0600EA3E RID: 59966 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EA3F RID: 59967 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003E21 RID: 15905
		// (get) Token: 0x0600EA40 RID: 59968 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EA41 RID: 59969 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003E22 RID: 15906
		// (get) Token: 0x0600EA42 RID: 59970 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EA43 RID: 59971 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003E23 RID: 15907
		// (get) Token: 0x0600EA44 RID: 59972 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EA45 RID: 59973 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003E24 RID: 15908
		// (get) Token: 0x0600EA46 RID: 59974 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EA47 RID: 59975 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003E25 RID: 15909
		// (get) Token: 0x0600EA48 RID: 59976 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600EA49 RID: 59977 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003E26 RID: 15910
		// (get) Token: 0x0600EA4A RID: 59978 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EA4B RID: 59979 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003E27 RID: 15911
		// (get) Token: 0x0600EA4C RID: 59980 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EA4D RID: 59981 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003E28 RID: 15912
		// (get) Token: 0x0600EA4E RID: 59982 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EA4F RID: 59983 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003E29 RID: 15913
		// (get) Token: 0x0600EA50 RID: 59984 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EA51 RID: 59985 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003E2A RID: 15914
		// (get) Token: 0x0600EA52 RID: 59986 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EA53 RID: 59987 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003E2B RID: 15915
		// (get) Token: 0x0600EA54 RID: 59988 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EA55 RID: 59989 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003E2C RID: 15916
		// (get) Token: 0x0600EA56 RID: 59990 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EA57 RID: 59991 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003E2D RID: 15917
		// (get) Token: 0x0600EA58 RID: 59992 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600EA59 RID: 59993 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003E2E RID: 15918
		// (get) Token: 0x0600EA5A RID: 59994 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EA5B RID: 59995 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003E2F RID: 15919
		// (get) Token: 0x0600EA5C RID: 59996 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EA5D RID: 59997 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003E30 RID: 15920
		// (get) Token: 0x0600EA5E RID: 59998 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EA5F RID: 59999 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003E31 RID: 15921
		// (get) Token: 0x0600EA60 RID: 60000 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600EA61 RID: 60001 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003E32 RID: 15922
		// (get) Token: 0x0600EA62 RID: 60002 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EA63 RID: 60003 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003E33 RID: 15923
		// (get) Token: 0x0600EA64 RID: 60004 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600EA65 RID: 60005 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003E34 RID: 15924
		// (get) Token: 0x0600EA66 RID: 60006 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600EA67 RID: 60007 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x0600EA68 RID: 60008 RVA: 0x00293ECF File Offset: 0x002920CF
		public MenuWithTitle()
		{
		}

		// Token: 0x0600EA69 RID: 60009 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MenuWithTitle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EA6A RID: 60010 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MenuWithTitle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EA6B RID: 60011 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MenuWithTitle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EA6C RID: 60012 RVA: 0x002CAE18 File Offset: 0x002C9018
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
				return new SplitButtonWithTitle();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new MenuWithTitle();
			}
			if (34 == namespaceId && "dynamicMenu" == name)
			{
				return new UnsizedDynamicMenu();
			}
			return null;
		}

		// Token: 0x0600EA6D RID: 60013 RVA: 0x002CAF00 File Offset: 0x002C9100
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
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "itemSize" == name)
			{
				return new EnumValue<ItemSizeValues>();
			}
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getTitle" == name)
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

		// Token: 0x0600EA6E RID: 60014 RVA: 0x002CB1A9 File Offset: 0x002C93A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuWithTitle>(deep);
		}

		// Token: 0x0600EA6F RID: 60015 RVA: 0x002CB1B4 File Offset: 0x002C93B4
		// Note: this type is marked as 'beforefieldinit'.
		static MenuWithTitle()
		{
			byte[] array = new byte[30];
			MenuWithTitle.attributeNamespaceIds = array;
		}

		// Token: 0x04006FB7 RID: 28599
		private const string tagName = "menu";

		// Token: 0x04006FB8 RID: 28600
		private const byte tagNsId = 34;

		// Token: 0x04006FB9 RID: 28601
		internal const int ElementTypeIdConst = 12583;

		// Token: 0x04006FBA RID: 28602
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "idMso", "tag", "itemSize", "title", "getTitle", "image", "imageMso", "getImage",
			"screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FBB RID: 28603
		private static byte[] attributeNamespaceIds;
	}
}
