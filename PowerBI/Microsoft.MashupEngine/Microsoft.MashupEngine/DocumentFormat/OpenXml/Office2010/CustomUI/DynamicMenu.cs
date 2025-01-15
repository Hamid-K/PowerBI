using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022CF RID: 8911
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DynamicMenu : OpenXmlLeafElement
	{
		// Token: 0x17004450 RID: 17488
		// (get) Token: 0x0600F751 RID: 63313 RVA: 0x002CA71A File Offset: 0x002C891A
		public override string LocalName
		{
			get
			{
				return "dynamicMenu";
			}
		}

		// Token: 0x17004451 RID: 17489
		// (get) Token: 0x0600F752 RID: 63314 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004452 RID: 17490
		// (get) Token: 0x0600F753 RID: 63315 RVA: 0x002D6C48 File Offset: 0x002D4E48
		internal override int ElementTypeId
		{
			get
			{
				return 13056;
			}
		}

		// Token: 0x0600F754 RID: 63316 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004453 RID: 17491
		// (get) Token: 0x0600F755 RID: 63317 RVA: 0x002D6C4F File Offset: 0x002D4E4F
		internal override string[] AttributeTagNames
		{
			get
			{
				return DynamicMenu.attributeTagNames;
			}
		}

		// Token: 0x17004454 RID: 17492
		// (get) Token: 0x0600F756 RID: 63318 RVA: 0x002D6C56 File Offset: 0x002D4E56
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DynamicMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x17004455 RID: 17493
		// (get) Token: 0x0600F757 RID: 63319 RVA: 0x002D42D0 File Offset: 0x002D24D0
		// (set) Token: 0x0600F758 RID: 63320 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004456 RID: 17494
		// (get) Token: 0x0600F759 RID: 63321 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F75A RID: 63322 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004457 RID: 17495
		// (get) Token: 0x0600F75B RID: 63323 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F75C RID: 63324 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17004458 RID: 17496
		// (get) Token: 0x0600F75D RID: 63325 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F75E RID: 63326 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17004459 RID: 17497
		// (get) Token: 0x0600F75F RID: 63327 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F760 RID: 63328 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700445A RID: 17498
		// (get) Token: 0x0600F761 RID: 63329 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F762 RID: 63330 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700445B RID: 17499
		// (get) Token: 0x0600F763 RID: 63331 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F764 RID: 63332 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700445C RID: 17500
		// (get) Token: 0x0600F765 RID: 63333 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F766 RID: 63334 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x1700445D RID: 17501
		// (get) Token: 0x0600F767 RID: 63335 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F768 RID: 63336 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getContent")]
		public StringValue GetContent
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

		// Token: 0x1700445E RID: 17502
		// (get) Token: 0x0600F769 RID: 63337 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F76A RID: 63338 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
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

		// Token: 0x1700445F RID: 17503
		// (get) Token: 0x0600F76B RID: 63339 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F76C RID: 63340 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004460 RID: 17504
		// (get) Token: 0x0600F76D RID: 63341 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F76E RID: 63342 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004461 RID: 17505
		// (get) Token: 0x0600F76F RID: 63343 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F770 RID: 63344 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004462 RID: 17506
		// (get) Token: 0x0600F771 RID: 63345 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F772 RID: 63346 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004463 RID: 17507
		// (get) Token: 0x0600F773 RID: 63347 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F774 RID: 63348 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004464 RID: 17508
		// (get) Token: 0x0600F775 RID: 63349 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F776 RID: 63350 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004465 RID: 17509
		// (get) Token: 0x0600F777 RID: 63351 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F778 RID: 63352 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004466 RID: 17510
		// (get) Token: 0x0600F779 RID: 63353 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x0600F77A RID: 63354 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17004467 RID: 17511
		// (get) Token: 0x0600F77B RID: 63355 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F77C RID: 63356 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004468 RID: 17512
		// (get) Token: 0x0600F77D RID: 63357 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F77E RID: 63358 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004469 RID: 17513
		// (get) Token: 0x0600F77F RID: 63359 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F780 RID: 63360 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x1700446A RID: 17514
		// (get) Token: 0x0600F781 RID: 63361 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F782 RID: 63362 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x1700446B RID: 17515
		// (get) Token: 0x0600F783 RID: 63363 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F784 RID: 63364 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x1700446C RID: 17516
		// (get) Token: 0x0600F785 RID: 63365 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F786 RID: 63366 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x1700446D RID: 17517
		// (get) Token: 0x0600F787 RID: 63367 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F788 RID: 63368 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x1700446E RID: 17518
		// (get) Token: 0x0600F789 RID: 63369 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x0600F78A RID: 63370 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x1700446F RID: 17519
		// (get) Token: 0x0600F78B RID: 63371 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F78C RID: 63372 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004470 RID: 17520
		// (get) Token: 0x0600F78D RID: 63373 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F78E RID: 63374 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004471 RID: 17521
		// (get) Token: 0x0600F78F RID: 63375 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F790 RID: 63376 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17004472 RID: 17522
		// (get) Token: 0x0600F791 RID: 63377 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600F792 RID: 63378 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17004473 RID: 17523
		// (get) Token: 0x0600F793 RID: 63379 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F794 RID: 63380 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x17004474 RID: 17524
		// (get) Token: 0x0600F795 RID: 63381 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600F796 RID: 63382 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x17004475 RID: 17525
		// (get) Token: 0x0600F797 RID: 63383 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600F798 RID: 63384 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x0600F79A RID: 63386 RVA: 0x002D6C60 File Offset: 0x002D4E60
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
			if (namespaceId == 0 && "getContent" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalidateContentOnDrop" == name)
			{
				return new BooleanValue();
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

		// Token: 0x0600F79B RID: 63387 RVA: 0x002D6F4B File Offset: 0x002D514B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DynamicMenu>(deep);
		}

		// Token: 0x0600F79C RID: 63388 RVA: 0x002D6F54 File Offset: 0x002D5154
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicMenu()
		{
			byte[] array = new byte[33];
			DynamicMenu.attributeNamespaceIds = array;
		}

		// Token: 0x04007138 RID: 28984
		private const string tagName = "dynamicMenu";

		// Token: 0x04007139 RID: 28985
		private const byte tagNsId = 57;

		// Token: 0x0400713A RID: 28986
		internal const int ElementTypeIdConst = 13056;

		// Token: 0x0400713B RID: 28987
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "description", "getDescription", "id", "idQ", "tag", "idMso", "getContent", "invalidateContentOnDrop",
			"image", "imageMso", "getImage", "screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label",
			"getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel",
			"getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x0400713C RID: 28988
		private static byte[] attributeNamespaceIds;
	}
}
