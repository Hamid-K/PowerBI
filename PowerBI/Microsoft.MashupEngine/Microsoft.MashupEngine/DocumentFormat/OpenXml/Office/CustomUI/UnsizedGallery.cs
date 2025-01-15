using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002271 RID: 8817
	[ChildElementInfo(typeof(Item))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedButton))]
	internal class UnsizedGallery : OpenXmlCompositeElement
	{
		// Token: 0x17003D3B RID: 15675
		// (get) Token: 0x0600E868 RID: 59496 RVA: 0x002C92B0 File Offset: 0x002C74B0
		public override string LocalName
		{
			get
			{
				return "gallery";
			}
		}

		// Token: 0x17003D3C RID: 15676
		// (get) Token: 0x0600E869 RID: 59497 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003D3D RID: 15677
		// (get) Token: 0x0600E86A RID: 59498 RVA: 0x002C92B7 File Offset: 0x002C74B7
		internal override int ElementTypeId
		{
			get
			{
				return 12576;
			}
		}

		// Token: 0x0600E86B RID: 59499 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003D3E RID: 15678
		// (get) Token: 0x0600E86C RID: 59500 RVA: 0x002C92BE File Offset: 0x002C74BE
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsizedGallery.attributeTagNames;
			}
		}

		// Token: 0x17003D3F RID: 15679
		// (get) Token: 0x0600E86D RID: 59501 RVA: 0x002C92C5 File Offset: 0x002C74C5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsizedGallery.attributeNamespaceIds;
			}
		}

		// Token: 0x17003D40 RID: 15680
		// (get) Token: 0x0600E86E RID: 59502 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E86F RID: 59503 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17003D41 RID: 15681
		// (get) Token: 0x0600E870 RID: 59504 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E871 RID: 59505 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17003D42 RID: 15682
		// (get) Token: 0x0600E872 RID: 59506 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600E873 RID: 59507 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003D43 RID: 15683
		// (get) Token: 0x0600E874 RID: 59508 RVA: 0x002C92CC File Offset: 0x002C74CC
		// (set) Token: 0x0600E875 RID: 59509 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "columns")]
		public IntegerValue Columns
		{
			get
			{
				return (IntegerValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003D44 RID: 15684
		// (get) Token: 0x0600E876 RID: 59510 RVA: 0x002C92DB File Offset: 0x002C74DB
		// (set) Token: 0x0600E877 RID: 59511 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "rows")]
		public IntegerValue Rows
		{
			get
			{
				return (IntegerValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003D45 RID: 15685
		// (get) Token: 0x0600E878 RID: 59512 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0600E879 RID: 59513 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "itemWidth")]
		public IntegerValue ItemWidth
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

		// Token: 0x17003D46 RID: 15686
		// (get) Token: 0x0600E87A RID: 59514 RVA: 0x002BDE49 File Offset: 0x002BC049
		// (set) Token: 0x0600E87B RID: 59515 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "itemHeight")]
		public IntegerValue ItemHeight
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

		// Token: 0x17003D47 RID: 15687
		// (get) Token: 0x0600E87C RID: 59516 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E87D RID: 59517 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getItemWidth")]
		public StringValue GetItemWidth
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

		// Token: 0x17003D48 RID: 15688
		// (get) Token: 0x0600E87E RID: 59518 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E87F RID: 59519 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getItemHeight")]
		public StringValue GetItemHeight
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

		// Token: 0x17003D49 RID: 15689
		// (get) Token: 0x0600E880 RID: 59520 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600E881 RID: 59521 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "showItemLabel")]
		public BooleanValue ShowItemLabel
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

		// Token: 0x17003D4A RID: 15690
		// (get) Token: 0x0600E882 RID: 59522 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E883 RID: 59523 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17003D4B RID: 15691
		// (get) Token: 0x0600E884 RID: 59524 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600E885 RID: 59525 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17003D4C RID: 15692
		// (get) Token: 0x0600E886 RID: 59526 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E887 RID: 59527 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003D4D RID: 15693
		// (get) Token: 0x0600E888 RID: 59528 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E889 RID: 59529 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003D4E RID: 15694
		// (get) Token: 0x0600E88A RID: 59530 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600E88B RID: 59531 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003D4F RID: 15695
		// (get) Token: 0x0600E88C RID: 59532 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E88D RID: 59533 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003D50 RID: 15696
		// (get) Token: 0x0600E88E RID: 59534 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600E88F RID: 59535 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
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

		// Token: 0x17003D51 RID: 15697
		// (get) Token: 0x0600E890 RID: 59536 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E891 RID: 59537 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x17003D52 RID: 15698
		// (get) Token: 0x0600E892 RID: 59538 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E893 RID: 59539 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x17003D53 RID: 15699
		// (get) Token: 0x0600E894 RID: 59540 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E895 RID: 59541 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
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

		// Token: 0x17003D54 RID: 15700
		// (get) Token: 0x0600E896 RID: 59542 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600E897 RID: 59543 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
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

		// Token: 0x17003D55 RID: 15701
		// (get) Token: 0x0600E898 RID: 59544 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E899 RID: 59545 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
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

		// Token: 0x17003D56 RID: 15702
		// (get) Token: 0x0600E89A RID: 59546 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600E89B RID: 59547 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x17003D57 RID: 15703
		// (get) Token: 0x0600E89C RID: 59548 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600E89D RID: 59549 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x17003D58 RID: 15704
		// (get) Token: 0x0600E89E RID: 59550 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600E89F RID: 59551 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getSelectedItemID")]
		public StringValue GetSelectedItemID
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

		// Token: 0x17003D59 RID: 15705
		// (get) Token: 0x0600E8A0 RID: 59552 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E8A1 RID: 59553 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
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

		// Token: 0x17003D5A RID: 15706
		// (get) Token: 0x0600E8A2 RID: 59554 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E8A3 RID: 59555 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003D5B RID: 15707
		// (get) Token: 0x0600E8A4 RID: 59556 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E8A5 RID: 59557 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003D5C RID: 15708
		// (get) Token: 0x0600E8A6 RID: 59558 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E8A7 RID: 59559 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003D5D RID: 15709
		// (get) Token: 0x0600E8A8 RID: 59560 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E8A9 RID: 59561 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003D5E RID: 15710
		// (get) Token: 0x0600E8AA RID: 59562 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600E8AB RID: 59563 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003D5F RID: 15711
		// (get) Token: 0x0600E8AC RID: 59564 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600E8AD RID: 59565 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003D60 RID: 15712
		// (get) Token: 0x0600E8AE RID: 59566 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600E8AF RID: 59567 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003D61 RID: 15713
		// (get) Token: 0x0600E8B0 RID: 59568 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600E8B1 RID: 59569 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003D62 RID: 15714
		// (get) Token: 0x0600E8B2 RID: 59570 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600E8B3 RID: 59571 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003D63 RID: 15715
		// (get) Token: 0x0600E8B4 RID: 59572 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600E8B5 RID: 59573 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003D64 RID: 15716
		// (get) Token: 0x0600E8B6 RID: 59574 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600E8B7 RID: 59575 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003D65 RID: 15717
		// (get) Token: 0x0600E8B8 RID: 59576 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E8B9 RID: 59577 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003D66 RID: 15718
		// (get) Token: 0x0600E8BA RID: 59578 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600E8BB RID: 59579 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003D67 RID: 15719
		// (get) Token: 0x0600E8BC RID: 59580 RVA: 0x002C933A File Offset: 0x002C753A
		// (set) Token: 0x0600E8BD RID: 59581 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003D68 RID: 15720
		// (get) Token: 0x0600E8BE RID: 59582 RVA: 0x002C934A File Offset: 0x002C754A
		// (set) Token: 0x0600E8BF RID: 59583 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x17003D69 RID: 15721
		// (get) Token: 0x0600E8C0 RID: 59584 RVA: 0x002C33B2 File Offset: 0x002C15B2
		// (set) Token: 0x0600E8C1 RID: 59585 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003D6A RID: 15722
		// (get) Token: 0x0600E8C2 RID: 59586 RVA: 0x002C33C2 File Offset: 0x002C15C2
		// (set) Token: 0x0600E8C3 RID: 59587 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003D6B RID: 15723
		// (get) Token: 0x0600E8C4 RID: 59588 RVA: 0x002C33D2 File Offset: 0x002C15D2
		// (set) Token: 0x0600E8C5 RID: 59589 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003D6C RID: 15724
		// (get) Token: 0x0600E8C6 RID: 59590 RVA: 0x002C935A File Offset: 0x002C755A
		// (set) Token: 0x0600E8C7 RID: 59591 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x17003D6D RID: 15725
		// (get) Token: 0x0600E8C8 RID: 59592 RVA: 0x002C33F2 File Offset: 0x002C15F2
		// (set) Token: 0x0600E8C9 RID: 59593 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003D6E RID: 15726
		// (get) Token: 0x0600E8CA RID: 59594 RVA: 0x002C936A File Offset: 0x002C756A
		// (set) Token: 0x0600E8CB RID: 59595 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17003D6F RID: 15727
		// (get) Token: 0x0600E8CC RID: 59596 RVA: 0x002C3412 File Offset: 0x002C1612
		// (set) Token: 0x0600E8CD RID: 59597 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600E8CE RID: 59598 RVA: 0x00293ECF File Offset: 0x002920CF
		public UnsizedGallery()
		{
		}

		// Token: 0x0600E8CF RID: 59599 RVA: 0x00293ED7 File Offset: 0x002920D7
		public UnsizedGallery(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E8D0 RID: 59600 RVA: 0x00293EE0 File Offset: 0x002920E0
		public UnsizedGallery(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E8D1 RID: 59601 RVA: 0x00293EE9 File Offset: 0x002920E9
		public UnsizedGallery(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E8D2 RID: 59602 RVA: 0x002C937A File Offset: 0x002C757A
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

		// Token: 0x0600E8D3 RID: 59603 RVA: 0x002C93B0 File Offset: 0x002C75B0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x0600E8D4 RID: 59604 RVA: 0x002C97E5 File Offset: 0x002C79E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnsizedGallery>(deep);
		}

		// Token: 0x0600E8D5 RID: 59605 RVA: 0x002C97F0 File Offset: 0x002C79F0
		// Note: this type is marked as 'beforefieldinit'.
		static UnsizedGallery()
		{
			byte[] array = new byte[48];
			UnsizedGallery.attributeNamespaceIds = array;
		}

		// Token: 0x04006F94 RID: 28564
		private const string tagName = "gallery";

		// Token: 0x04006F95 RID: 28565
		private const byte tagNsId = 34;

		// Token: 0x04006F96 RID: 28566
		internal const int ElementTypeIdConst = 12576;

		// Token: 0x04006F97 RID: 28567
		private static string[] attributeTagNames = new string[]
		{
			"description", "getDescription", "invalidateContentOnDrop", "columns", "rows", "itemWidth", "itemHeight", "getItemWidth", "getItemHeight", "showItemLabel",
			"onAction", "enabled", "getEnabled", "image", "imageMso", "getImage", "showItemImage", "getItemCount", "getItemLabel", "getItemScreentip",
			"getItemSupertip", "getItemImage", "getItemID", "sizeString", "getSelectedItemID", "getSelectedItemIndex", "id", "idQ", "idMso", "tag",
			"screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006F98 RID: 28568
		private static byte[] attributeNamespaceIds;
	}
}
