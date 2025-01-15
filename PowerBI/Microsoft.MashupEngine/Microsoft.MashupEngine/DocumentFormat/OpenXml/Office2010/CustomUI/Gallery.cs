using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022CD RID: 8909
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Item), FileFormatVersions.Office2010)]
	internal class Gallery : OpenXmlCompositeElement
	{
		// Token: 0x170043F3 RID: 17395
		// (get) Token: 0x0600F68F RID: 63119 RVA: 0x002C92B0 File Offset: 0x002C74B0
		public override string LocalName
		{
			get
			{
				return "gallery";
			}
		}

		// Token: 0x170043F4 RID: 17396
		// (get) Token: 0x0600F690 RID: 63120 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170043F5 RID: 17397
		// (get) Token: 0x0600F691 RID: 63121 RVA: 0x002D605B File Offset: 0x002D425B
		internal override int ElementTypeId
		{
			get
			{
				return 13054;
			}
		}

		// Token: 0x0600F692 RID: 63122 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170043F6 RID: 17398
		// (get) Token: 0x0600F693 RID: 63123 RVA: 0x002D6062 File Offset: 0x002D4262
		internal override string[] AttributeTagNames
		{
			get
			{
				return Gallery.attributeTagNames;
			}
		}

		// Token: 0x170043F7 RID: 17399
		// (get) Token: 0x0600F694 RID: 63124 RVA: 0x002D6069 File Offset: 0x002D4269
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Gallery.attributeNamespaceIds;
			}
		}

		// Token: 0x170043F8 RID: 17400
		// (get) Token: 0x0600F695 RID: 63125 RVA: 0x002D42D0 File Offset: 0x002D24D0
		// (set) Token: 0x0600F696 RID: 63126 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170043F9 RID: 17401
		// (get) Token: 0x0600F697 RID: 63127 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F698 RID: 63128 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170043FA RID: 17402
		// (get) Token: 0x0600F699 RID: 63129 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F69A RID: 63130 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170043FB RID: 17403
		// (get) Token: 0x0600F69B RID: 63131 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F69C RID: 63132 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170043FC RID: 17404
		// (get) Token: 0x0600F69D RID: 63133 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0600F69E RID: 63134 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170043FD RID: 17405
		// (get) Token: 0x0600F69F RID: 63135 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0600F6A0 RID: 63136 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "columns")]
		public IntegerValue Columns
		{
			get
			{
				return (IntegerValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170043FE RID: 17406
		// (get) Token: 0x0600F6A1 RID: 63137 RVA: 0x002BDE49 File Offset: 0x002BC049
		// (set) Token: 0x0600F6A2 RID: 63138 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "rows")]
		public IntegerValue Rows
		{
			get
			{
				return (IntegerValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170043FF RID: 17407
		// (get) Token: 0x0600F6A3 RID: 63139 RVA: 0x002BDE58 File Offset: 0x002BC058
		// (set) Token: 0x0600F6A4 RID: 63140 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "itemWidth")]
		public IntegerValue ItemWidth
		{
			get
			{
				return (IntegerValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004400 RID: 17408
		// (get) Token: 0x0600F6A5 RID: 63141 RVA: 0x002CD150 File Offset: 0x002CB350
		// (set) Token: 0x0600F6A6 RID: 63142 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "itemHeight")]
		public IntegerValue ItemHeight
		{
			get
			{
				return (IntegerValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004401 RID: 17409
		// (get) Token: 0x0600F6A7 RID: 63143 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F6A8 RID: 63144 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getItemWidth")]
		public StringValue GetItemWidth
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

		// Token: 0x17004402 RID: 17410
		// (get) Token: 0x0600F6A9 RID: 63145 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F6AA RID: 63146 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getItemHeight")]
		public StringValue GetItemHeight
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

		// Token: 0x17004403 RID: 17411
		// (get) Token: 0x0600F6AB RID: 63147 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600F6AC RID: 63148 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "showItemLabel")]
		public BooleanValue ShowItemLabel
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

		// Token: 0x17004404 RID: 17412
		// (get) Token: 0x0600F6AD RID: 63149 RVA: 0x002D6070 File Offset: 0x002D4270
		// (set) Token: 0x0600F6AE RID: 63150 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "showInRibbon")]
		public EnumValue<GalleryShowInRibbonValues> ShowInRibbon
		{
			get
			{
				return (EnumValue<GalleryShowInRibbonValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004405 RID: 17413
		// (get) Token: 0x0600F6AF RID: 63151 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F6B0 RID: 63152 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004406 RID: 17414
		// (get) Token: 0x0600F6B1 RID: 63153 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600F6B2 RID: 63154 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17004407 RID: 17415
		// (get) Token: 0x0600F6B3 RID: 63155 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F6B4 RID: 63156 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17004408 RID: 17416
		// (get) Token: 0x0600F6B5 RID: 63157 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F6B6 RID: 63158 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004409 RID: 17417
		// (get) Token: 0x0600F6B7 RID: 63159 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F6B8 RID: 63160 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x1700440A RID: 17418
		// (get) Token: 0x0600F6B9 RID: 63161 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F6BA RID: 63162 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x1700440B RID: 17419
		// (get) Token: 0x0600F6BB RID: 63163 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x0600F6BC RID: 63164 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
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

		// Token: 0x1700440C RID: 17420
		// (get) Token: 0x0600F6BD RID: 63165 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F6BE RID: 63166 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x1700440D RID: 17421
		// (get) Token: 0x0600F6BF RID: 63167 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F6C0 RID: 63168 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x1700440E RID: 17422
		// (get) Token: 0x0600F6C1 RID: 63169 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F6C2 RID: 63170 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
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

		// Token: 0x1700440F RID: 17423
		// (get) Token: 0x0600F6C3 RID: 63171 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F6C4 RID: 63172 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
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

		// Token: 0x17004410 RID: 17424
		// (get) Token: 0x0600F6C5 RID: 63173 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F6C6 RID: 63174 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
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

		// Token: 0x17004411 RID: 17425
		// (get) Token: 0x0600F6C7 RID: 63175 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F6C8 RID: 63176 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x17004412 RID: 17426
		// (get) Token: 0x0600F6C9 RID: 63177 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F6CA RID: 63178 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x17004413 RID: 17427
		// (get) Token: 0x0600F6CB RID: 63179 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F6CC RID: 63180 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getSelectedItemID")]
		public StringValue GetSelectedItemID
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

		// Token: 0x17004414 RID: 17428
		// (get) Token: 0x0600F6CD RID: 63181 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F6CE RID: 63182 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
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

		// Token: 0x17004415 RID: 17429
		// (get) Token: 0x0600F6CF RID: 63183 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F6D0 RID: 63184 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004416 RID: 17430
		// (get) Token: 0x0600F6D1 RID: 63185 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F6D2 RID: 63186 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004417 RID: 17431
		// (get) Token: 0x0600F6D3 RID: 63187 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600F6D4 RID: 63188 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17004418 RID: 17432
		// (get) Token: 0x0600F6D5 RID: 63189 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600F6D6 RID: 63190 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17004419 RID: 17433
		// (get) Token: 0x0600F6D7 RID: 63191 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600F6D8 RID: 63192 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
		{
			get
			{
				return (StringValue)base.Attributes[33];
			}
			set
			{
				base.Attributes[33] = value;
			}
		}

		// Token: 0x1700441A RID: 17434
		// (get) Token: 0x0600F6D9 RID: 63193 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600F6DA RID: 63194 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
		{
			get
			{
				return (StringValue)base.Attributes[34];
			}
			set
			{
				base.Attributes[34] = value;
			}
		}

		// Token: 0x1700441B RID: 17435
		// (get) Token: 0x0600F6DB RID: 63195 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600F6DC RID: 63196 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
		{
			get
			{
				return (StringValue)base.Attributes[35];
			}
			set
			{
				base.Attributes[35] = value;
			}
		}

		// Token: 0x1700441C RID: 17436
		// (get) Token: 0x0600F6DD RID: 63197 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600F6DE RID: 63198 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
		{
			get
			{
				return (StringValue)base.Attributes[36];
			}
			set
			{
				base.Attributes[36] = value;
			}
		}

		// Token: 0x1700441D RID: 17437
		// (get) Token: 0x0600F6DF RID: 63199 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600F6E0 RID: 63200 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "label")]
		public StringValue Label
		{
			get
			{
				return (StringValue)base.Attributes[37];
			}
			set
			{
				base.Attributes[37] = value;
			}
		}

		// Token: 0x1700441E RID: 17438
		// (get) Token: 0x0600F6E1 RID: 63201 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600F6E2 RID: 63202 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[38];
			}
			set
			{
				base.Attributes[38] = value;
			}
		}

		// Token: 0x1700441F RID: 17439
		// (get) Token: 0x0600F6E3 RID: 63203 RVA: 0x002C933A File Offset: 0x002C753A
		// (set) Token: 0x0600F6E4 RID: 63204 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[39];
			}
			set
			{
				base.Attributes[39] = value;
			}
		}

		// Token: 0x17004420 RID: 17440
		// (get) Token: 0x0600F6E5 RID: 63205 RVA: 0x002C33A2 File Offset: 0x002C15A2
		// (set) Token: 0x0600F6E6 RID: 63206 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x17004421 RID: 17441
		// (get) Token: 0x0600F6E7 RID: 63207 RVA: 0x002C33B2 File Offset: 0x002C15B2
		// (set) Token: 0x0600F6E8 RID: 63208 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[41];
			}
			set
			{
				base.Attributes[41] = value;
			}
		}

		// Token: 0x17004422 RID: 17442
		// (get) Token: 0x0600F6E9 RID: 63209 RVA: 0x002C33C2 File Offset: 0x002C15C2
		// (set) Token: 0x0600F6EA RID: 63210 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[42];
			}
			set
			{
				base.Attributes[42] = value;
			}
		}

		// Token: 0x17004423 RID: 17443
		// (get) Token: 0x0600F6EB RID: 63211 RVA: 0x002D6090 File Offset: 0x002D4290
		// (set) Token: 0x0600F6EC RID: 63212 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[43];
			}
			set
			{
				base.Attributes[43] = value;
			}
		}

		// Token: 0x17004424 RID: 17444
		// (get) Token: 0x0600F6ED RID: 63213 RVA: 0x002C33E2 File Offset: 0x002C15E2
		// (set) Token: 0x0600F6EE RID: 63214 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x17004425 RID: 17445
		// (get) Token: 0x0600F6EF RID: 63215 RVA: 0x002C33F2 File Offset: 0x002C15F2
		// (set) Token: 0x0600F6F0 RID: 63216 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[45];
			}
			set
			{
				base.Attributes[45] = value;
			}
		}

		// Token: 0x17004426 RID: 17446
		// (get) Token: 0x0600F6F1 RID: 63217 RVA: 0x002C3402 File Offset: 0x002C1602
		// (set) Token: 0x0600F6F2 RID: 63218 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x17004427 RID: 17447
		// (get) Token: 0x0600F6F3 RID: 63219 RVA: 0x002D2271 File Offset: 0x002D0471
		// (set) Token: 0x0600F6F4 RID: 63220 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[47];
			}
			set
			{
				base.Attributes[47] = value;
			}
		}

		// Token: 0x17004428 RID: 17448
		// (get) Token: 0x0600F6F5 RID: 63221 RVA: 0x002C3422 File Offset: 0x002C1622
		// (set) Token: 0x0600F6F6 RID: 63222 RVA: 0x002C15EC File Offset: 0x002BF7EC
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[48];
			}
			set
			{
				base.Attributes[48] = value;
			}
		}

		// Token: 0x17004429 RID: 17449
		// (get) Token: 0x0600F6F7 RID: 63223 RVA: 0x002D60A0 File Offset: 0x002D42A0
		// (set) Token: 0x0600F6F8 RID: 63224 RVA: 0x002C1608 File Offset: 0x002BF808
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[49];
			}
			set
			{
				base.Attributes[49] = value;
			}
		}

		// Token: 0x1700442A RID: 17450
		// (get) Token: 0x0600F6F9 RID: 63225 RVA: 0x002C4805 File Offset: 0x002C2A05
		// (set) Token: 0x0600F6FA RID: 63226 RVA: 0x002C1624 File Offset: 0x002BF824
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[50];
			}
			set
			{
				base.Attributes[50] = value;
			}
		}

		// Token: 0x0600F6FB RID: 63227 RVA: 0x00293ECF File Offset: 0x002920CF
		public Gallery()
		{
		}

		// Token: 0x0600F6FC RID: 63228 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Gallery(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F6FD RID: 63229 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Gallery(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F6FE RID: 63230 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Gallery(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F6FF RID: 63231 RVA: 0x002D2281 File Offset: 0x002D0481
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "item" == name)
			{
				return new Item();
			}
			if (57 == namespaceId && "button" == name)
			{
				return new ButtonRegular();
			}
			return null;
		}

		// Token: 0x0600F700 RID: 63232 RVA: 0x002D60B0 File Offset: 0x002D42B0
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
			if (namespaceId == 0 && "invalidateContentOnDrop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "columns" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "rows" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "itemWidth" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "itemHeight" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "getItemWidth" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemHeight" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showItemLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showInRibbon" == name)
			{
				return new EnumValue<GalleryShowInRibbonValues>();
			}
			if (namespaceId == 0 && "onAction" == name)
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
			if (namespaceId == 0 && "showItemImage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getItemCount" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemSupertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemImage" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sizeString" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSelectedItemID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSelectedItemIndex" == name)
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

		// Token: 0x0600F701 RID: 63233 RVA: 0x002D6527 File Offset: 0x002D4727
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Gallery>(deep);
		}

		// Token: 0x0600F702 RID: 63234 RVA: 0x002D6530 File Offset: 0x002D4730
		// Note: this type is marked as 'beforefieldinit'.
		static Gallery()
		{
			byte[] array = new byte[51];
			Gallery.attributeNamespaceIds = array;
		}

		// Token: 0x0400712E RID: 28974
		private const string tagName = "gallery";

		// Token: 0x0400712F RID: 28975
		private const byte tagNsId = 57;

		// Token: 0x04007130 RID: 28976
		internal const int ElementTypeIdConst = 13054;

		// Token: 0x04007131 RID: 28977
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "description", "getDescription", "invalidateContentOnDrop", "columns", "rows", "itemWidth", "itemHeight", "getItemWidth",
			"getItemHeight", "showItemLabel", "showInRibbon", "onAction", "enabled", "getEnabled", "image", "imageMso", "getImage", "showItemImage",
			"getItemCount", "getItemLabel", "getItemScreentip", "getItemSupertip", "getItemImage", "getItemID", "sizeString", "getSelectedItemID", "getSelectedItemIndex", "id",
			"idQ", "tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage",
			"getShowImage"
		};

		// Token: 0x04007132 RID: 28978
		private static byte[] attributeNamespaceIds;
	}
}
