using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C4 RID: 8900
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuSeparator), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CheckBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GalleryRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToggleButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlCloneRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SplitButtonWithTitle), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuWithTitle), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DynamicMenuRegular), FileFormatVersions.Office2010)]
	internal class MenuWithTitle : OpenXmlCompositeElement
	{
		// Token: 0x170042C1 RID: 17089
		// (get) Token: 0x0600F41F RID: 62495 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x170042C2 RID: 17090
		// (get) Token: 0x0600F420 RID: 62496 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170042C3 RID: 17091
		// (get) Token: 0x0600F421 RID: 62497 RVA: 0x002D3CA0 File Offset: 0x002D1EA0
		internal override int ElementTypeId
		{
			get
			{
				return 13045;
			}
		}

		// Token: 0x0600F422 RID: 62498 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170042C4 RID: 17092
		// (get) Token: 0x0600F423 RID: 62499 RVA: 0x002D3CA7 File Offset: 0x002D1EA7
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuWithTitle.attributeTagNames;
			}
		}

		// Token: 0x170042C5 RID: 17093
		// (get) Token: 0x0600F424 RID: 62500 RVA: 0x002D3CAE File Offset: 0x002D1EAE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuWithTitle.attributeNamespaceIds;
			}
		}

		// Token: 0x170042C6 RID: 17094
		// (get) Token: 0x0600F425 RID: 62501 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F426 RID: 62502 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170042C7 RID: 17095
		// (get) Token: 0x0600F427 RID: 62503 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F428 RID: 62504 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170042C8 RID: 17096
		// (get) Token: 0x0600F429 RID: 62505 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F42A RID: 62506 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170042C9 RID: 17097
		// (get) Token: 0x0600F42B RID: 62507 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F42C RID: 62508 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170042CA RID: 17098
		// (get) Token: 0x0600F42D RID: 62509 RVA: 0x002D3CB5 File Offset: 0x002D1EB5
		// (set) Token: 0x0600F42E RID: 62510 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170042CB RID: 17099
		// (get) Token: 0x0600F42F RID: 62511 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F430 RID: 62512 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170042CC RID: 17100
		// (get) Token: 0x0600F431 RID: 62513 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F432 RID: 62514 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x170042CD RID: 17101
		// (get) Token: 0x0600F433 RID: 62515 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F434 RID: 62516 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170042CE RID: 17102
		// (get) Token: 0x0600F435 RID: 62517 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F436 RID: 62518 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x170042CF RID: 17103
		// (get) Token: 0x0600F437 RID: 62519 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F438 RID: 62520 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170042D0 RID: 17104
		// (get) Token: 0x0600F439 RID: 62521 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F43A RID: 62522 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170042D1 RID: 17105
		// (get) Token: 0x0600F43B RID: 62523 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F43C RID: 62524 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170042D2 RID: 17106
		// (get) Token: 0x0600F43D RID: 62525 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F43E RID: 62526 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170042D3 RID: 17107
		// (get) Token: 0x0600F43F RID: 62527 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F440 RID: 62528 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170042D4 RID: 17108
		// (get) Token: 0x0600F441 RID: 62529 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600F442 RID: 62530 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x170042D5 RID: 17109
		// (get) Token: 0x0600F443 RID: 62531 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F444 RID: 62532 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x170042D6 RID: 17110
		// (get) Token: 0x0600F445 RID: 62533 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F446 RID: 62534 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x170042D7 RID: 17111
		// (get) Token: 0x0600F447 RID: 62535 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F448 RID: 62536 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x170042D8 RID: 17112
		// (get) Token: 0x0600F449 RID: 62537 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F44A RID: 62538 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x170042D9 RID: 17113
		// (get) Token: 0x0600F44B RID: 62539 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F44C RID: 62540 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x170042DA RID: 17114
		// (get) Token: 0x0600F44D RID: 62541 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F44E RID: 62542 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170042DB RID: 17115
		// (get) Token: 0x0600F44F RID: 62543 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F450 RID: 62544 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170042DC RID: 17116
		// (get) Token: 0x0600F451 RID: 62545 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600F452 RID: 62546 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x170042DD RID: 17117
		// (get) Token: 0x0600F453 RID: 62547 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F454 RID: 62548 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x170042DE RID: 17118
		// (get) Token: 0x0600F455 RID: 62549 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F456 RID: 62550 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x170042DF RID: 17119
		// (get) Token: 0x0600F457 RID: 62551 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F458 RID: 62552 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x170042E0 RID: 17120
		// (get) Token: 0x0600F459 RID: 62553 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600F45A RID: 62554 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x170042E1 RID: 17121
		// (get) Token: 0x0600F45B RID: 62555 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F45C RID: 62556 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x170042E2 RID: 17122
		// (get) Token: 0x0600F45D RID: 62557 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600F45E RID: 62558 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x170042E3 RID: 17123
		// (get) Token: 0x0600F45F RID: 62559 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F460 RID: 62560 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x0600F461 RID: 62561 RVA: 0x00293ECF File Offset: 0x002920CF
		public MenuWithTitle()
		{
		}

		// Token: 0x0600F462 RID: 62562 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MenuWithTitle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F463 RID: 62563 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MenuWithTitle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F464 RID: 62564 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MenuWithTitle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F465 RID: 62565 RVA: 0x002D3CC4 File Offset: 0x002D1EC4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "control" == name)
			{
				return new ControlCloneRegular();
			}
			if (57 == namespaceId && "button" == name)
			{
				return new ButtonRegular();
			}
			if (57 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (57 == namespaceId && "gallery" == name)
			{
				return new GalleryRegular();
			}
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new ToggleButtonRegular();
			}
			if (57 == namespaceId && "menuSeparator" == name)
			{
				return new MenuSeparator();
			}
			if (57 == namespaceId && "splitButton" == name)
			{
				return new SplitButtonWithTitle();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new MenuWithTitle();
			}
			if (57 == namespaceId && "dynamicMenu" == name)
			{
				return new DynamicMenuRegular();
			}
			return null;
		}

		// Token: 0x0600F466 RID: 62566 RVA: 0x002D3DAC File Offset: 0x002D1FAC
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
			if (namespaceId == 0 && "idMso" == name)
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

		// Token: 0x0600F467 RID: 62567 RVA: 0x002D4055 File Offset: 0x002D2255
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuWithTitle>(deep);
		}

		// Token: 0x0600F468 RID: 62568 RVA: 0x002D4060 File Offset: 0x002D2260
		// Note: this type is marked as 'beforefieldinit'.
		static MenuWithTitle()
		{
			byte[] array = new byte[30];
			MenuWithTitle.attributeNamespaceIds = array;
		}

		// Token: 0x04007101 RID: 28929
		private const string tagName = "menu";

		// Token: 0x04007102 RID: 28930
		private const byte tagNsId = 57;

		// Token: 0x04007103 RID: 28931
		internal const int ElementTypeIdConst = 13045;

		// Token: 0x04007104 RID: 28932
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "itemSize", "title", "getTitle", "image", "imageMso", "getImage",
			"screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04007105 RID: 28933
		private static byte[] attributeNamespaceIds;
	}
}
