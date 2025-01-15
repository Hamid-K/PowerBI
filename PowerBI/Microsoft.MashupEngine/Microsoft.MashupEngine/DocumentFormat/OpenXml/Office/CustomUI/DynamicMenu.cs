using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002282 RID: 8834
	[GeneratedCode("DomGen", "2.0")]
	internal class DynamicMenu : OpenXmlLeafElement
	{
		// Token: 0x17003F94 RID: 16276
		// (get) Token: 0x0600ED3E RID: 60734 RVA: 0x002CA71A File Offset: 0x002C891A
		public override string LocalName
		{
			get
			{
				return "dynamicMenu";
			}
		}

		// Token: 0x17003F95 RID: 16277
		// (get) Token: 0x0600ED3F RID: 60735 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003F96 RID: 16278
		// (get) Token: 0x0600ED40 RID: 60736 RVA: 0x002CDD1C File Offset: 0x002CBF1C
		internal override int ElementTypeId
		{
			get
			{
				return 12593;
			}
		}

		// Token: 0x0600ED41 RID: 60737 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003F97 RID: 16279
		// (get) Token: 0x0600ED42 RID: 60738 RVA: 0x002CDD23 File Offset: 0x002CBF23
		internal override string[] AttributeTagNames
		{
			get
			{
				return DynamicMenu.attributeTagNames;
			}
		}

		// Token: 0x17003F98 RID: 16280
		// (get) Token: 0x0600ED43 RID: 60739 RVA: 0x002CDD2A File Offset: 0x002CBF2A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DynamicMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x17003F99 RID: 16281
		// (get) Token: 0x0600ED44 RID: 60740 RVA: 0x002CB2F7 File Offset: 0x002C94F7
		// (set) Token: 0x0600ED45 RID: 60741 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003F9A RID: 16282
		// (get) Token: 0x0600ED46 RID: 60742 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600ED47 RID: 60743 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003F9B RID: 16283
		// (get) Token: 0x0600ED48 RID: 60744 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600ED49 RID: 60745 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003F9C RID: 16284
		// (get) Token: 0x0600ED4A RID: 60746 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600ED4B RID: 60747 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003F9D RID: 16285
		// (get) Token: 0x0600ED4C RID: 60748 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600ED4D RID: 60749 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003F9E RID: 16286
		// (get) Token: 0x0600ED4E RID: 60750 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600ED4F RID: 60751 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003F9F RID: 16287
		// (get) Token: 0x0600ED50 RID: 60752 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600ED51 RID: 60753 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003FA0 RID: 16288
		// (get) Token: 0x0600ED52 RID: 60754 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600ED53 RID: 60755 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003FA1 RID: 16289
		// (get) Token: 0x0600ED54 RID: 60756 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600ED55 RID: 60757 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003FA2 RID: 16290
		// (get) Token: 0x0600ED56 RID: 60758 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600ED57 RID: 60759 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003FA3 RID: 16291
		// (get) Token: 0x0600ED58 RID: 60760 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600ED59 RID: 60761 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003FA4 RID: 16292
		// (get) Token: 0x0600ED5A RID: 60762 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600ED5B RID: 60763 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003FA5 RID: 16293
		// (get) Token: 0x0600ED5C RID: 60764 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600ED5D RID: 60765 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003FA6 RID: 16294
		// (get) Token: 0x0600ED5E RID: 60766 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600ED5F RID: 60767 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003FA7 RID: 16295
		// (get) Token: 0x0600ED60 RID: 60768 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600ED61 RID: 60769 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003FA8 RID: 16296
		// (get) Token: 0x0600ED62 RID: 60770 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600ED63 RID: 60771 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003FA9 RID: 16297
		// (get) Token: 0x0600ED64 RID: 60772 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600ED65 RID: 60773 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003FAA RID: 16298
		// (get) Token: 0x0600ED66 RID: 60774 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x0600ED67 RID: 60775 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003FAB RID: 16299
		// (get) Token: 0x0600ED68 RID: 60776 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600ED69 RID: 60777 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003FAC RID: 16300
		// (get) Token: 0x0600ED6A RID: 60778 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600ED6B RID: 60779 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003FAD RID: 16301
		// (get) Token: 0x0600ED6C RID: 60780 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600ED6D RID: 60781 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003FAE RID: 16302
		// (get) Token: 0x0600ED6E RID: 60782 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600ED6F RID: 60783 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003FAF RID: 16303
		// (get) Token: 0x0600ED70 RID: 60784 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600ED71 RID: 60785 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003FB0 RID: 16304
		// (get) Token: 0x0600ED72 RID: 60786 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600ED73 RID: 60787 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003FB1 RID: 16305
		// (get) Token: 0x0600ED74 RID: 60788 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600ED75 RID: 60789 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003FB2 RID: 16306
		// (get) Token: 0x0600ED76 RID: 60790 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x0600ED77 RID: 60791 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003FB3 RID: 16307
		// (get) Token: 0x0600ED78 RID: 60792 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600ED79 RID: 60793 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003FB4 RID: 16308
		// (get) Token: 0x0600ED7A RID: 60794 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600ED7B RID: 60795 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003FB5 RID: 16309
		// (get) Token: 0x0600ED7C RID: 60796 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600ED7D RID: 60797 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003FB6 RID: 16310
		// (get) Token: 0x0600ED7E RID: 60798 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600ED7F RID: 60799 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003FB7 RID: 16311
		// (get) Token: 0x0600ED80 RID: 60800 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600ED81 RID: 60801 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003FB8 RID: 16312
		// (get) Token: 0x0600ED82 RID: 60802 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600ED83 RID: 60803 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003FB9 RID: 16313
		// (get) Token: 0x0600ED84 RID: 60804 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600ED85 RID: 60805 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x0600ED87 RID: 60807 RVA: 0x002CDD44 File Offset: 0x002CBF44
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
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
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

		// Token: 0x0600ED88 RID: 60808 RVA: 0x002CE02F File Offset: 0x002CC22F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DynamicMenu>(deep);
		}

		// Token: 0x0600ED89 RID: 60809 RVA: 0x002CE038 File Offset: 0x002CC238
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicMenu()
		{
			byte[] array = new byte[33];
			DynamicMenu.attributeNamespaceIds = array;
		}

		// Token: 0x04006FE9 RID: 28649
		private const string tagName = "dynamicMenu";

		// Token: 0x04006FEA RID: 28650
		private const byte tagNsId = 34;

		// Token: 0x04006FEB RID: 28651
		internal const int ElementTypeIdConst = 12593;

		// Token: 0x04006FEC RID: 28652
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "description", "getDescription", "id", "idQ", "idMso", "tag", "getContent", "invalidateContentOnDrop",
			"image", "imageMso", "getImage", "screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label",
			"getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel",
			"getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FED RID: 28653
		private static byte[] attributeNamespaceIds;
	}
}
