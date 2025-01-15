using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200227E RID: 8830
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Item))]
	internal class ComboBox : OpenXmlCompositeElement
	{
		// Token: 0x17003EE0 RID: 16096
		// (get) Token: 0x0600EBC6 RID: 60358 RVA: 0x002CC6B7 File Offset: 0x002CA8B7
		public override string LocalName
		{
			get
			{
				return "comboBox";
			}
		}

		// Token: 0x17003EE1 RID: 16097
		// (get) Token: 0x0600EBC7 RID: 60359 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003EE2 RID: 16098
		// (get) Token: 0x0600EBC8 RID: 60360 RVA: 0x002CC6BE File Offset: 0x002CA8BE
		internal override int ElementTypeId
		{
			get
			{
				return 12589;
			}
		}

		// Token: 0x0600EBC9 RID: 60361 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003EE3 RID: 16099
		// (get) Token: 0x0600EBCA RID: 60362 RVA: 0x002CC6C5 File Offset: 0x002CA8C5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ComboBox.attributeTagNames;
			}
		}

		// Token: 0x17003EE4 RID: 16100
		// (get) Token: 0x0600EBCB RID: 60363 RVA: 0x002CC6CC File Offset: 0x002CA8CC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ComboBox.attributeNamespaceIds;
			}
		}

		// Token: 0x17003EE5 RID: 16101
		// (get) Token: 0x0600EBCC RID: 60364 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600EBCD RID: 60365 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003EE6 RID: 16102
		// (get) Token: 0x0600EBCE RID: 60366 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EBCF RID: 60367 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x17003EE7 RID: 16103
		// (get) Token: 0x0600EBD0 RID: 60368 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EBD1 RID: 60369 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x17003EE8 RID: 16104
		// (get) Token: 0x0600EBD2 RID: 60370 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EBD3 RID: 60371 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
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

		// Token: 0x17003EE9 RID: 16105
		// (get) Token: 0x0600EBD4 RID: 60372 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EBD5 RID: 60373 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
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

		// Token: 0x17003EEA RID: 16106
		// (get) Token: 0x0600EBD6 RID: 60374 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EBD7 RID: 60375 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
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

		// Token: 0x17003EEB RID: 16107
		// (get) Token: 0x0600EBD8 RID: 60376 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EBD9 RID: 60377 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x17003EEC RID: 16108
		// (get) Token: 0x0600EBDA RID: 60378 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EBDB RID: 60379 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x17003EED RID: 16109
		// (get) Token: 0x0600EBDC RID: 60380 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600EBDD RID: 60381 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17003EEE RID: 16110
		// (get) Token: 0x0600EBDE RID: 60382 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600EBDF RID: 60383 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17003EEF RID: 16111
		// (get) Token: 0x0600EBE0 RID: 60384 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EBE1 RID: 60385 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003EF0 RID: 16112
		// (get) Token: 0x0600EBE2 RID: 60386 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EBE3 RID: 60387 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003EF1 RID: 16113
		// (get) Token: 0x0600EBE4 RID: 60388 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EBE5 RID: 60389 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003EF2 RID: 16114
		// (get) Token: 0x0600EBE6 RID: 60390 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EBE7 RID: 60391 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003EF3 RID: 16115
		// (get) Token: 0x0600EBE8 RID: 60392 RVA: 0x002CC6D3 File Offset: 0x002CA8D3
		// (set) Token: 0x0600EBE9 RID: 60393 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "maxLength")]
		public IntegerValue MaxLength
		{
			get
			{
				return (IntegerValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17003EF4 RID: 16116
		// (get) Token: 0x0600EBEA RID: 60394 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EBEB RID: 60395 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getText")]
		public StringValue GetText
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

		// Token: 0x17003EF5 RID: 16117
		// (get) Token: 0x0600EBEC RID: 60396 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EBED RID: 60397 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "onChange")]
		public StringValue OnChange
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

		// Token: 0x17003EF6 RID: 16118
		// (get) Token: 0x0600EBEE RID: 60398 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EBEF RID: 60399 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003EF7 RID: 16119
		// (get) Token: 0x0600EBF0 RID: 60400 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EBF1 RID: 60401 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003EF8 RID: 16120
		// (get) Token: 0x0600EBF2 RID: 60402 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EBF3 RID: 60403 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003EF9 RID: 16121
		// (get) Token: 0x0600EBF4 RID: 60404 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EBF5 RID: 60405 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003EFA RID: 16122
		// (get) Token: 0x0600EBF6 RID: 60406 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EBF7 RID: 60407 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003EFB RID: 16123
		// (get) Token: 0x0600EBF8 RID: 60408 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600EBF9 RID: 60409 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003EFC RID: 16124
		// (get) Token: 0x0600EBFA RID: 60410 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EBFB RID: 60411 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003EFD RID: 16125
		// (get) Token: 0x0600EBFC RID: 60412 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EBFD RID: 60413 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003EFE RID: 16126
		// (get) Token: 0x0600EBFE RID: 60414 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EBFF RID: 60415 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003EFF RID: 16127
		// (get) Token: 0x0600EC00 RID: 60416 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600EC01 RID: 60417 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003F00 RID: 16128
		// (get) Token: 0x0600EC02 RID: 60418 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EC03 RID: 60419 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003F01 RID: 16129
		// (get) Token: 0x0600EC04 RID: 60420 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600EC05 RID: 60421 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003F02 RID: 16130
		// (get) Token: 0x0600EC06 RID: 60422 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600EC07 RID: 60423 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003F03 RID: 16131
		// (get) Token: 0x0600EC08 RID: 60424 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600EC09 RID: 60425 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003F04 RID: 16132
		// (get) Token: 0x0600EC0A RID: 60426 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600EC0B RID: 60427 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003F05 RID: 16133
		// (get) Token: 0x0600EC0C RID: 60428 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600EC0D RID: 60429 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003F06 RID: 16134
		// (get) Token: 0x0600EC0E RID: 60430 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600EC0F RID: 60431 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003F07 RID: 16135
		// (get) Token: 0x0600EC10 RID: 60432 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600EC11 RID: 60433 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003F08 RID: 16136
		// (get) Token: 0x0600EC12 RID: 60434 RVA: 0x002CC6E3 File Offset: 0x002CA8E3
		// (set) Token: 0x0600EC13 RID: 60435 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[35];
			}
			set
			{
				base.Attributes[35] = value;
			}
		}

		// Token: 0x17003F09 RID: 16137
		// (get) Token: 0x0600EC14 RID: 60436 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600EC15 RID: 60437 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003F0A RID: 16138
		// (get) Token: 0x0600EC16 RID: 60438 RVA: 0x002CC6F3 File Offset: 0x002CA8F3
		// (set) Token: 0x0600EC17 RID: 60439 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[37];
			}
			set
			{
				base.Attributes[37] = value;
			}
		}

		// Token: 0x17003F0B RID: 16139
		// (get) Token: 0x0600EC18 RID: 60440 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600EC19 RID: 60441 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600EC1A RID: 60442 RVA: 0x00293ECF File Offset: 0x002920CF
		public ComboBox()
		{
		}

		// Token: 0x0600EC1B RID: 60443 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ComboBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EC1C RID: 60444 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ComboBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EC1D RID: 60445 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ComboBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EC1E RID: 60446 RVA: 0x002CC703 File Offset: 0x002CA903
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "item" == name)
			{
				return new Item();
			}
			return null;
		}

		// Token: 0x0600EC1F RID: 60447 RVA: 0x002CC720 File Offset: 0x002CA920
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "invalidateContentOnDrop" == name)
			{
				return new BooleanValue();
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
			if (namespaceId == 0 && "maxLength" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "getText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onChange" == name)
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

		// Token: 0x0600EC20 RID: 60448 RVA: 0x002CCA8F File Offset: 0x002CAC8F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ComboBox>(deep);
		}

		// Token: 0x0600EC21 RID: 60449 RVA: 0x002CCA98 File Offset: 0x002CAC98
		// Note: this type is marked as 'beforefieldinit'.
		static ComboBox()
		{
			byte[] array = new byte[39];
			ComboBox.attributeNamespaceIds = array;
		}

		// Token: 0x04006FD5 RID: 28629
		private const string tagName = "comboBox";

		// Token: 0x04006FD6 RID: 28630
		private const byte tagNsId = 34;

		// Token: 0x04006FD7 RID: 28631
		internal const int ElementTypeIdConst = 12589;

		// Token: 0x04006FD8 RID: 28632
		private static string[] attributeTagNames = new string[]
		{
			"showItemImage", "getItemCount", "getItemLabel", "getItemScreentip", "getItemSupertip", "getItemImage", "getItemID", "sizeString", "invalidateContentOnDrop", "enabled",
			"getEnabled", "image", "imageMso", "getImage", "maxLength", "getText", "onChange", "id", "idQ", "idMso",
			"tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ",
			"insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FD9 RID: 28633
		private static byte[] attributeNamespaceIds;
	}
}
