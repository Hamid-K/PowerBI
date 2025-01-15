using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C1 RID: 8897
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GalleryRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToggleButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuSeparator), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SplitButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DynamicMenuRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CheckBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlCloneRegular), FileFormatVersions.Office2010)]
	internal class MenuRegular : OpenXmlCompositeElement
	{
		// Token: 0x17004265 RID: 16997
		// (get) Token: 0x0600F35F RID: 62303 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x17004266 RID: 16998
		// (get) Token: 0x0600F360 RID: 62304 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004267 RID: 16999
		// (get) Token: 0x0600F361 RID: 62305 RVA: 0x002D3100 File Offset: 0x002D1300
		internal override int ElementTypeId
		{
			get
			{
				return 13042;
			}
		}

		// Token: 0x0600F362 RID: 62306 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004268 RID: 17000
		// (get) Token: 0x0600F363 RID: 62307 RVA: 0x002D3107 File Offset: 0x002D1307
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuRegular.attributeTagNames;
			}
		}

		// Token: 0x17004269 RID: 17001
		// (get) Token: 0x0600F364 RID: 62308 RVA: 0x002D310E File Offset: 0x002D130E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x1700426A RID: 17002
		// (get) Token: 0x0600F365 RID: 62309 RVA: 0x002D3115 File Offset: 0x002D1315
		// (set) Token: 0x0600F366 RID: 62310 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700426B RID: 17003
		// (get) Token: 0x0600F367 RID: 62311 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F368 RID: 62312 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700426C RID: 17004
		// (get) Token: 0x0600F369 RID: 62313 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F36A RID: 62314 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700426D RID: 17005
		// (get) Token: 0x0600F36B RID: 62315 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F36C RID: 62316 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x1700426E RID: 17006
		// (get) Token: 0x0600F36D RID: 62317 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F36E RID: 62318 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700426F RID: 17007
		// (get) Token: 0x0600F36F RID: 62319 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F370 RID: 62320 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17004270 RID: 17008
		// (get) Token: 0x0600F371 RID: 62321 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F372 RID: 62322 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17004271 RID: 17009
		// (get) Token: 0x0600F373 RID: 62323 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F374 RID: 62324 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17004272 RID: 17010
		// (get) Token: 0x0600F375 RID: 62325 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F376 RID: 62326 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17004273 RID: 17011
		// (get) Token: 0x0600F377 RID: 62327 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F378 RID: 62328 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17004274 RID: 17012
		// (get) Token: 0x0600F379 RID: 62329 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F37A RID: 62330 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17004275 RID: 17013
		// (get) Token: 0x0600F37B RID: 62331 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F37C RID: 62332 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17004276 RID: 17014
		// (get) Token: 0x0600F37D RID: 62333 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F37E RID: 62334 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17004277 RID: 17015
		// (get) Token: 0x0600F37F RID: 62335 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F380 RID: 62336 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17004278 RID: 17016
		// (get) Token: 0x0600F381 RID: 62337 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600F382 RID: 62338 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17004279 RID: 17017
		// (get) Token: 0x0600F383 RID: 62339 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F384 RID: 62340 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x1700427A RID: 17018
		// (get) Token: 0x0600F385 RID: 62341 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F386 RID: 62342 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x1700427B RID: 17019
		// (get) Token: 0x0600F387 RID: 62343 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F388 RID: 62344 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x1700427C RID: 17020
		// (get) Token: 0x0600F389 RID: 62345 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F38A RID: 62346 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x1700427D RID: 17021
		// (get) Token: 0x0600F38B RID: 62347 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F38C RID: 62348 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x1700427E RID: 17022
		// (get) Token: 0x0600F38D RID: 62349 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F38E RID: 62350 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x1700427F RID: 17023
		// (get) Token: 0x0600F38F RID: 62351 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F390 RID: 62352 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17004280 RID: 17024
		// (get) Token: 0x0600F391 RID: 62353 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600F392 RID: 62354 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17004281 RID: 17025
		// (get) Token: 0x0600F393 RID: 62355 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F394 RID: 62356 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17004282 RID: 17026
		// (get) Token: 0x0600F395 RID: 62357 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F396 RID: 62358 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17004283 RID: 17027
		// (get) Token: 0x0600F397 RID: 62359 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F398 RID: 62360 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17004284 RID: 17028
		// (get) Token: 0x0600F399 RID: 62361 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600F39A RID: 62362 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17004285 RID: 17029
		// (get) Token: 0x0600F39B RID: 62363 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F39C RID: 62364 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17004286 RID: 17030
		// (get) Token: 0x0600F39D RID: 62365 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600F39E RID: 62366 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17004287 RID: 17031
		// (get) Token: 0x0600F39F RID: 62367 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F3A0 RID: 62368 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x0600F3A1 RID: 62369 RVA: 0x00293ECF File Offset: 0x002920CF
		public MenuRegular()
		{
		}

		// Token: 0x0600F3A2 RID: 62370 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MenuRegular(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F3A3 RID: 62371 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MenuRegular(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F3A4 RID: 62372 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MenuRegular(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F3A5 RID: 62373 RVA: 0x002D3124 File Offset: 0x002D1324
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
				return new SplitButtonRegular();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new MenuRegular();
			}
			if (57 == namespaceId && "dynamicMenu" == name)
			{
				return new DynamicMenuRegular();
			}
			return null;
		}

		// Token: 0x0600F3A6 RID: 62374 RVA: 0x002D320C File Offset: 0x002D140C
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
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
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

		// Token: 0x0600F3A7 RID: 62375 RVA: 0x002D34B5 File Offset: 0x002D16B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuRegular>(deep);
		}

		// Token: 0x0600F3A8 RID: 62376 RVA: 0x002D34C0 File Offset: 0x002D16C0
		// Note: this type is marked as 'beforefieldinit'.
		static MenuRegular()
		{
			byte[] array = new byte[30];
			MenuRegular.attributeNamespaceIds = array;
		}

		// Token: 0x040070F2 RID: 28914
		private const string tagName = "menu";

		// Token: 0x040070F3 RID: 28915
		private const byte tagNsId = 57;

		// Token: 0x040070F4 RID: 28916
		internal const int ElementTypeIdConst = 13042;

		// Token: 0x040070F5 RID: 28917
		private static string[] attributeTagNames = new string[]
		{
			"itemSize", "description", "getDescription", "id", "idQ", "tag", "idMso", "image", "imageMso", "getImage",
			"screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x040070F6 RID: 28918
		private static byte[] attributeNamespaceIds;
	}
}
