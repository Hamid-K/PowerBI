using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002280 RID: 8832
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Item))]
	[ChildElementInfo(typeof(UnsizedButton))]
	internal class Gallery : OpenXmlCompositeElement
	{
		// Token: 0x17003F38 RID: 16184
		// (get) Token: 0x0600EC7E RID: 60542 RVA: 0x002C92B0 File Offset: 0x002C74B0
		public override string LocalName
		{
			get
			{
				return "gallery";
			}
		}

		// Token: 0x17003F39 RID: 16185
		// (get) Token: 0x0600EC7F RID: 60543 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003F3A RID: 16186
		// (get) Token: 0x0600EC80 RID: 60544 RVA: 0x002CD13B File Offset: 0x002CB33B
		internal override int ElementTypeId
		{
			get
			{
				return 12591;
			}
		}

		// Token: 0x0600EC81 RID: 60545 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003F3B RID: 16187
		// (get) Token: 0x0600EC82 RID: 60546 RVA: 0x002CD142 File Offset: 0x002CB342
		internal override string[] AttributeTagNames
		{
			get
			{
				return Gallery.attributeTagNames;
			}
		}

		// Token: 0x17003F3C RID: 16188
		// (get) Token: 0x0600EC83 RID: 60547 RVA: 0x002CD149 File Offset: 0x002CB349
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Gallery.attributeNamespaceIds;
			}
		}

		// Token: 0x17003F3D RID: 16189
		// (get) Token: 0x0600EC84 RID: 60548 RVA: 0x002CB2F7 File Offset: 0x002C94F7
		// (set) Token: 0x0600EC85 RID: 60549 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003F3E RID: 16190
		// (get) Token: 0x0600EC86 RID: 60550 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EC87 RID: 60551 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003F3F RID: 16191
		// (get) Token: 0x0600EC88 RID: 60552 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EC89 RID: 60553 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003F40 RID: 16192
		// (get) Token: 0x0600EC8A RID: 60554 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EC8B RID: 60555 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003F41 RID: 16193
		// (get) Token: 0x0600EC8C RID: 60556 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0600EC8D RID: 60557 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003F42 RID: 16194
		// (get) Token: 0x0600EC8E RID: 60558 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0600EC8F RID: 60559 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003F43 RID: 16195
		// (get) Token: 0x0600EC90 RID: 60560 RVA: 0x002BDE49 File Offset: 0x002BC049
		// (set) Token: 0x0600EC91 RID: 60561 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003F44 RID: 16196
		// (get) Token: 0x0600EC92 RID: 60562 RVA: 0x002BDE58 File Offset: 0x002BC058
		// (set) Token: 0x0600EC93 RID: 60563 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003F45 RID: 16197
		// (get) Token: 0x0600EC94 RID: 60564 RVA: 0x002CD150 File Offset: 0x002CB350
		// (set) Token: 0x0600EC95 RID: 60565 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003F46 RID: 16198
		// (get) Token: 0x0600EC96 RID: 60566 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EC97 RID: 60567 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003F47 RID: 16199
		// (get) Token: 0x0600EC98 RID: 60568 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EC99 RID: 60569 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003F48 RID: 16200
		// (get) Token: 0x0600EC9A RID: 60570 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600EC9B RID: 60571 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003F49 RID: 16201
		// (get) Token: 0x0600EC9C RID: 60572 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EC9D RID: 60573 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17003F4A RID: 16202
		// (get) Token: 0x0600EC9E RID: 60574 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x0600EC9F RID: 60575 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17003F4B RID: 16203
		// (get) Token: 0x0600ECA0 RID: 60576 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600ECA1 RID: 60577 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003F4C RID: 16204
		// (get) Token: 0x0600ECA2 RID: 60578 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600ECA3 RID: 60579 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003F4D RID: 16205
		// (get) Token: 0x0600ECA4 RID: 60580 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600ECA5 RID: 60581 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003F4E RID: 16206
		// (get) Token: 0x0600ECA6 RID: 60582 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600ECA7 RID: 60583 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003F4F RID: 16207
		// (get) Token: 0x0600ECA8 RID: 60584 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0600ECA9 RID: 60585 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17003F50 RID: 16208
		// (get) Token: 0x0600ECAA RID: 60586 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600ECAB RID: 60587 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x17003F51 RID: 16209
		// (get) Token: 0x0600ECAC RID: 60588 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600ECAD RID: 60589 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x17003F52 RID: 16210
		// (get) Token: 0x0600ECAE RID: 60590 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600ECAF RID: 60591 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
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

		// Token: 0x17003F53 RID: 16211
		// (get) Token: 0x0600ECB0 RID: 60592 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600ECB1 RID: 60593 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
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

		// Token: 0x17003F54 RID: 16212
		// (get) Token: 0x0600ECB2 RID: 60594 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600ECB3 RID: 60595 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
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

		// Token: 0x17003F55 RID: 16213
		// (get) Token: 0x0600ECB4 RID: 60596 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600ECB5 RID: 60597 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x17003F56 RID: 16214
		// (get) Token: 0x0600ECB6 RID: 60598 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600ECB7 RID: 60599 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x17003F57 RID: 16215
		// (get) Token: 0x0600ECB8 RID: 60600 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600ECB9 RID: 60601 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getSelectedItemID")]
		public StringValue GetSelectedItemID
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

		// Token: 0x17003F58 RID: 16216
		// (get) Token: 0x0600ECBA RID: 60602 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600ECBB RID: 60603 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
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

		// Token: 0x17003F59 RID: 16217
		// (get) Token: 0x0600ECBC RID: 60604 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600ECBD RID: 60605 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003F5A RID: 16218
		// (get) Token: 0x0600ECBE RID: 60606 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600ECBF RID: 60607 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003F5B RID: 16219
		// (get) Token: 0x0600ECC0 RID: 60608 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600ECC1 RID: 60609 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003F5C RID: 16220
		// (get) Token: 0x0600ECC2 RID: 60610 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600ECC3 RID: 60611 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003F5D RID: 16221
		// (get) Token: 0x0600ECC4 RID: 60612 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600ECC5 RID: 60613 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003F5E RID: 16222
		// (get) Token: 0x0600ECC6 RID: 60614 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600ECC7 RID: 60615 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003F5F RID: 16223
		// (get) Token: 0x0600ECC8 RID: 60616 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600ECC9 RID: 60617 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003F60 RID: 16224
		// (get) Token: 0x0600ECCA RID: 60618 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600ECCB RID: 60619 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003F61 RID: 16225
		// (get) Token: 0x0600ECCC RID: 60620 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600ECCD RID: 60621 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003F62 RID: 16226
		// (get) Token: 0x0600ECCE RID: 60622 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600ECCF RID: 60623 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003F63 RID: 16227
		// (get) Token: 0x0600ECD0 RID: 60624 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600ECD1 RID: 60625 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003F64 RID: 16228
		// (get) Token: 0x0600ECD2 RID: 60626 RVA: 0x002C933A File Offset: 0x002C753A
		// (set) Token: 0x0600ECD3 RID: 60627 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003F65 RID: 16229
		// (get) Token: 0x0600ECD4 RID: 60628 RVA: 0x002C33A2 File Offset: 0x002C15A2
		// (set) Token: 0x0600ECD5 RID: 60629 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003F66 RID: 16230
		// (get) Token: 0x0600ECD6 RID: 60630 RVA: 0x002C33B2 File Offset: 0x002C15B2
		// (set) Token: 0x0600ECD7 RID: 60631 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003F67 RID: 16231
		// (get) Token: 0x0600ECD8 RID: 60632 RVA: 0x002CD16F File Offset: 0x002CB36F
		// (set) Token: 0x0600ECD9 RID: 60633 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[42];
			}
			set
			{
				base.Attributes[42] = value;
			}
		}

		// Token: 0x17003F68 RID: 16232
		// (get) Token: 0x0600ECDA RID: 60634 RVA: 0x002C33D2 File Offset: 0x002C15D2
		// (set) Token: 0x0600ECDB RID: 60635 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[43];
			}
			set
			{
				base.Attributes[43] = value;
			}
		}

		// Token: 0x17003F69 RID: 16233
		// (get) Token: 0x0600ECDC RID: 60636 RVA: 0x002C33E2 File Offset: 0x002C15E2
		// (set) Token: 0x0600ECDD RID: 60637 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003F6A RID: 16234
		// (get) Token: 0x0600ECDE RID: 60638 RVA: 0x002C33F2 File Offset: 0x002C15F2
		// (set) Token: 0x0600ECDF RID: 60639 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003F6B RID: 16235
		// (get) Token: 0x0600ECE0 RID: 60640 RVA: 0x002C936A File Offset: 0x002C756A
		// (set) Token: 0x0600ECE1 RID: 60641 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x17003F6C RID: 16236
		// (get) Token: 0x0600ECE2 RID: 60642 RVA: 0x002C3412 File Offset: 0x002C1612
		// (set) Token: 0x0600ECE3 RID: 60643 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[47];
			}
			set
			{
				base.Attributes[47] = value;
			}
		}

		// Token: 0x17003F6D RID: 16237
		// (get) Token: 0x0600ECE4 RID: 60644 RVA: 0x002CD17F File Offset: 0x002CB37F
		// (set) Token: 0x0600ECE5 RID: 60645 RVA: 0x002C15EC File Offset: 0x002BF7EC
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[48];
			}
			set
			{
				base.Attributes[48] = value;
			}
		}

		// Token: 0x17003F6E RID: 16238
		// (get) Token: 0x0600ECE6 RID: 60646 RVA: 0x002CD18F File Offset: 0x002CB38F
		// (set) Token: 0x0600ECE7 RID: 60647 RVA: 0x002C1608 File Offset: 0x002BF808
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[49];
			}
			set
			{
				base.Attributes[49] = value;
			}
		}

		// Token: 0x0600ECE8 RID: 60648 RVA: 0x00293ECF File Offset: 0x002920CF
		public Gallery()
		{
		}

		// Token: 0x0600ECE9 RID: 60649 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Gallery(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600ECEA RID: 60650 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Gallery(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600ECEB RID: 60651 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Gallery(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600ECEC RID: 60652 RVA: 0x002C937A File Offset: 0x002C757A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "item" == name)
			{
				return new Item();
			}
			if (34 == namespaceId && "button" == name)
			{
				return new UnsizedButton();
			}
			return null;
		}

		// Token: 0x0600ECED RID: 60653 RVA: 0x002CD1A0 File Offset: 0x002CB3A0
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
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
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

		// Token: 0x0600ECEE RID: 60654 RVA: 0x002CD601 File Offset: 0x002CB801
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Gallery>(deep);
		}

		// Token: 0x0600ECEF RID: 60655 RVA: 0x002CD60C File Offset: 0x002CB80C
		// Note: this type is marked as 'beforefieldinit'.
		static Gallery()
		{
			byte[] array = new byte[50];
			Gallery.attributeNamespaceIds = array;
		}

		// Token: 0x04006FDF RID: 28639
		private const string tagName = "gallery";

		// Token: 0x04006FE0 RID: 28640
		private const byte tagNsId = 34;

		// Token: 0x04006FE1 RID: 28641
		internal const int ElementTypeIdConst = 12591;

		// Token: 0x04006FE2 RID: 28642
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "description", "getDescription", "invalidateContentOnDrop", "columns", "rows", "itemWidth", "itemHeight", "getItemWidth",
			"getItemHeight", "showItemLabel", "onAction", "enabled", "getEnabled", "image", "imageMso", "getImage", "showItemImage", "getItemCount",
			"getItemLabel", "getItemScreentip", "getItemSupertip", "getItemImage", "getItemID", "sizeString", "getSelectedItemID", "getSelectedItemIndex", "id", "idQ",
			"idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FE3 RID: 28643
		private static byte[] attributeNamespaceIds;
	}
}
