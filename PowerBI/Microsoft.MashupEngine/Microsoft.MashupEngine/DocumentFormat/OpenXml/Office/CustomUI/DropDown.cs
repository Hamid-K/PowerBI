using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200227F RID: 8831
	[ChildElementInfo(typeof(Item))]
	[ChildElementInfo(typeof(UnsizedButton))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DropDown : OpenXmlCompositeElement
	{
		// Token: 0x17003F0C RID: 16140
		// (get) Token: 0x0600EC22 RID: 60450 RVA: 0x002CCC17 File Offset: 0x002CAE17
		public override string LocalName
		{
			get
			{
				return "dropDown";
			}
		}

		// Token: 0x17003F0D RID: 16141
		// (get) Token: 0x0600EC23 RID: 60451 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003F0E RID: 16142
		// (get) Token: 0x0600EC24 RID: 60452 RVA: 0x002CCC1E File Offset: 0x002CAE1E
		internal override int ElementTypeId
		{
			get
			{
				return 12590;
			}
		}

		// Token: 0x0600EC25 RID: 60453 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003F0F RID: 16143
		// (get) Token: 0x0600EC26 RID: 60454 RVA: 0x002CCC25 File Offset: 0x002CAE25
		internal override string[] AttributeTagNames
		{
			get
			{
				return DropDown.attributeTagNames;
			}
		}

		// Token: 0x17003F10 RID: 16144
		// (get) Token: 0x0600EC27 RID: 60455 RVA: 0x002CCC2C File Offset: 0x002CAE2C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DropDown.attributeNamespaceIds;
			}
		}

		// Token: 0x17003F11 RID: 16145
		// (get) Token: 0x0600EC28 RID: 60456 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EC29 RID: 60457 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17003F12 RID: 16146
		// (get) Token: 0x0600EC2A RID: 60458 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600EC2B RID: 60459 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003F13 RID: 16147
		// (get) Token: 0x0600EC2C RID: 60460 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EC2D RID: 60461 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003F14 RID: 16148
		// (get) Token: 0x0600EC2E RID: 60462 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EC2F RID: 60463 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003F15 RID: 16149
		// (get) Token: 0x0600EC30 RID: 60464 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EC31 RID: 60465 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003F16 RID: 16150
		// (get) Token: 0x0600EC32 RID: 60466 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EC33 RID: 60467 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003F17 RID: 16151
		// (get) Token: 0x0600EC34 RID: 60468 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0600EC35 RID: 60469 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17003F18 RID: 16152
		// (get) Token: 0x0600EC36 RID: 60470 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EC37 RID: 60471 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x17003F19 RID: 16153
		// (get) Token: 0x0600EC38 RID: 60472 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EC39 RID: 60473 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x17003F1A RID: 16154
		// (get) Token: 0x0600EC3A RID: 60474 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EC3B RID: 60475 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
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

		// Token: 0x17003F1B RID: 16155
		// (get) Token: 0x0600EC3C RID: 60476 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EC3D RID: 60477 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
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

		// Token: 0x17003F1C RID: 16156
		// (get) Token: 0x0600EC3E RID: 60478 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EC3F RID: 60479 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
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

		// Token: 0x17003F1D RID: 16157
		// (get) Token: 0x0600EC40 RID: 60480 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EC41 RID: 60481 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x17003F1E RID: 16158
		// (get) Token: 0x0600EC42 RID: 60482 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EC43 RID: 60483 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x17003F1F RID: 16159
		// (get) Token: 0x0600EC44 RID: 60484 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EC45 RID: 60485 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getSelectedItemID")]
		public StringValue GetSelectedItemID
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

		// Token: 0x17003F20 RID: 16160
		// (get) Token: 0x0600EC46 RID: 60486 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EC47 RID: 60487 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
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

		// Token: 0x17003F21 RID: 16161
		// (get) Token: 0x0600EC48 RID: 60488 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600EC49 RID: 60489 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "showItemLabel")]
		public BooleanValue ShowItemLabel
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

		// Token: 0x17003F22 RID: 16162
		// (get) Token: 0x0600EC4A RID: 60490 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EC4B RID: 60491 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003F23 RID: 16163
		// (get) Token: 0x0600EC4C RID: 60492 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EC4D RID: 60493 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003F24 RID: 16164
		// (get) Token: 0x0600EC4E RID: 60494 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EC4F RID: 60495 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003F25 RID: 16165
		// (get) Token: 0x0600EC50 RID: 60496 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EC51 RID: 60497 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003F26 RID: 16166
		// (get) Token: 0x0600EC52 RID: 60498 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EC53 RID: 60499 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003F27 RID: 16167
		// (get) Token: 0x0600EC54 RID: 60500 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600EC55 RID: 60501 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003F28 RID: 16168
		// (get) Token: 0x0600EC56 RID: 60502 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EC57 RID: 60503 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003F29 RID: 16169
		// (get) Token: 0x0600EC58 RID: 60504 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EC59 RID: 60505 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003F2A RID: 16170
		// (get) Token: 0x0600EC5A RID: 60506 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EC5B RID: 60507 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003F2B RID: 16171
		// (get) Token: 0x0600EC5C RID: 60508 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600EC5D RID: 60509 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003F2C RID: 16172
		// (get) Token: 0x0600EC5E RID: 60510 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EC5F RID: 60511 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003F2D RID: 16173
		// (get) Token: 0x0600EC60 RID: 60512 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600EC61 RID: 60513 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003F2E RID: 16174
		// (get) Token: 0x0600EC62 RID: 60514 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600EC63 RID: 60515 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003F2F RID: 16175
		// (get) Token: 0x0600EC64 RID: 60516 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600EC65 RID: 60517 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003F30 RID: 16176
		// (get) Token: 0x0600EC66 RID: 60518 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600EC67 RID: 60519 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003F31 RID: 16177
		// (get) Token: 0x0600EC68 RID: 60520 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600EC69 RID: 60521 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x17003F32 RID: 16178
		// (get) Token: 0x0600EC6A RID: 60522 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600EC6B RID: 60523 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x17003F33 RID: 16179
		// (get) Token: 0x0600EC6C RID: 60524 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600EC6D RID: 60525 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x17003F34 RID: 16180
		// (get) Token: 0x0600EC6E RID: 60526 RVA: 0x002CC6E3 File Offset: 0x002CA8E3
		// (set) Token: 0x0600EC6F RID: 60527 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x17003F35 RID: 16181
		// (get) Token: 0x0600EC70 RID: 60528 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600EC71 RID: 60529 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x17003F36 RID: 16182
		// (get) Token: 0x0600EC72 RID: 60530 RVA: 0x002CC6F3 File Offset: 0x002CA8F3
		// (set) Token: 0x0600EC73 RID: 60531 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x17003F37 RID: 16183
		// (get) Token: 0x0600EC74 RID: 60532 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600EC75 RID: 60533 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x0600EC76 RID: 60534 RVA: 0x00293ECF File Offset: 0x002920CF
		public DropDown()
		{
		}

		// Token: 0x0600EC77 RID: 60535 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DropDown(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EC78 RID: 60536 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DropDown(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EC79 RID: 60537 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DropDown(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EC7A RID: 60538 RVA: 0x002C937A File Offset: 0x002C757A
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

		// Token: 0x0600EC7B RID: 60539 RVA: 0x002CCC44 File Offset: 0x002CAE44
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "showItemLabel" == name)
			{
				return new BooleanValue();
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

		// Token: 0x0600EC7C RID: 60540 RVA: 0x002CCFB3 File Offset: 0x002CB1B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropDown>(deep);
		}

		// Token: 0x0600EC7D RID: 60541 RVA: 0x002CCFBC File Offset: 0x002CB1BC
		// Note: this type is marked as 'beforefieldinit'.
		static DropDown()
		{
			byte[] array = new byte[39];
			DropDown.attributeNamespaceIds = array;
		}

		// Token: 0x04006FDA RID: 28634
		private const string tagName = "dropDown";

		// Token: 0x04006FDB RID: 28635
		private const byte tagNsId = 34;

		// Token: 0x04006FDC RID: 28636
		internal const int ElementTypeIdConst = 12590;

		// Token: 0x04006FDD RID: 28637
		private static string[] attributeTagNames = new string[]
		{
			"onAction", "enabled", "getEnabled", "image", "imageMso", "getImage", "showItemImage", "getItemCount", "getItemLabel", "getItemScreentip",
			"getItemSupertip", "getItemImage", "getItemID", "sizeString", "getSelectedItemID", "getSelectedItemIndex", "showItemLabel", "id", "idQ", "idMso",
			"tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ",
			"insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FDE RID: 28638
		private static byte[] attributeNamespaceIds;
	}
}
