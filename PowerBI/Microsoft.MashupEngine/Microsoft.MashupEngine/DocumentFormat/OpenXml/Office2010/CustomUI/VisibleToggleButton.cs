using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E8 RID: 8936
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class VisibleToggleButton : OpenXmlLeafElement
	{
		// Token: 0x1700465A RID: 18010
		// (get) Token: 0x0600FB99 RID: 64409 RVA: 0x002C99C0 File Offset: 0x002C7BC0
		public override string LocalName
		{
			get
			{
				return "toggleButton";
			}
		}

		// Token: 0x1700465B RID: 18011
		// (get) Token: 0x0600FB9A RID: 64410 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700465C RID: 18012
		// (get) Token: 0x0600FB9B RID: 64411 RVA: 0x002DABD4 File Offset: 0x002D8DD4
		internal override int ElementTypeId
		{
			get
			{
				return 13081;
			}
		}

		// Token: 0x0600FB9C RID: 64412 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700465D RID: 18013
		// (get) Token: 0x0600FB9D RID: 64413 RVA: 0x002DABDB File Offset: 0x002D8DDB
		internal override string[] AttributeTagNames
		{
			get
			{
				return VisibleToggleButton.attributeTagNames;
			}
		}

		// Token: 0x1700465E RID: 18014
		// (get) Token: 0x0600FB9E RID: 64414 RVA: 0x002DABE2 File Offset: 0x002D8DE2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VisibleToggleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x1700465F RID: 18015
		// (get) Token: 0x0600FB9F RID: 64415 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FBA0 RID: 64416 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x17004660 RID: 18016
		// (get) Token: 0x0600FBA1 RID: 64417 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FBA2 RID: 64418 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004661 RID: 18017
		// (get) Token: 0x0600FBA3 RID: 64419 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600FBA4 RID: 64420 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17004662 RID: 18018
		// (get) Token: 0x0600FBA5 RID: 64421 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FBA6 RID: 64422 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004663 RID: 18019
		// (get) Token: 0x0600FBA7 RID: 64423 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FBA8 RID: 64424 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17004664 RID: 18020
		// (get) Token: 0x0600FBA9 RID: 64425 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FBAA RID: 64426 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17004665 RID: 18021
		// (get) Token: 0x0600FBAB RID: 64427 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FBAC RID: 64428 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004666 RID: 18022
		// (get) Token: 0x0600FBAD RID: 64429 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FBAE RID: 64430 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004667 RID: 18023
		// (get) Token: 0x0600FBAF RID: 64431 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FBB0 RID: 64432 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004668 RID: 18024
		// (get) Token: 0x0600FBB1 RID: 64433 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FBB2 RID: 64434 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004669 RID: 18025
		// (get) Token: 0x0600FBB3 RID: 64435 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FBB4 RID: 64436 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700466A RID: 18026
		// (get) Token: 0x0600FBB5 RID: 64437 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FBB6 RID: 64438 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700466B RID: 18027
		// (get) Token: 0x0600FBB7 RID: 64439 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FBB8 RID: 64440 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700466C RID: 18028
		// (get) Token: 0x0600FBB9 RID: 64441 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FBBA RID: 64442 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x1700466D RID: 18029
		// (get) Token: 0x0600FBBB RID: 64443 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FBBC RID: 64444 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x1700466E RID: 18030
		// (get) Token: 0x0600FBBD RID: 64445 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FBBE RID: 64446 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x1700466F RID: 18031
		// (get) Token: 0x0600FBBF RID: 64447 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FBC0 RID: 64448 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17004670 RID: 18032
		// (get) Token: 0x0600FBC1 RID: 64449 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FBC2 RID: 64450 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004671 RID: 18033
		// (get) Token: 0x0600FBC3 RID: 64451 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FBC4 RID: 64452 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004672 RID: 18034
		// (get) Token: 0x0600FBC5 RID: 64453 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FBC6 RID: 64454 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17004673 RID: 18035
		// (get) Token: 0x0600FBC7 RID: 64455 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FBC8 RID: 64456 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17004674 RID: 18036
		// (get) Token: 0x0600FBC9 RID: 64457 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600FBCA RID: 64458 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x17004675 RID: 18037
		// (get) Token: 0x0600FBCB RID: 64459 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600FBCC RID: 64460 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x17004676 RID: 18038
		// (get) Token: 0x0600FBCD RID: 64461 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600FBCE RID: 64462 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004677 RID: 18039
		// (get) Token: 0x0600FBCF RID: 64463 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600FBD0 RID: 64464 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17004678 RID: 18040
		// (get) Token: 0x0600FBD1 RID: 64465 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x0600FBD2 RID: 64466 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17004679 RID: 18041
		// (get) Token: 0x0600FBD3 RID: 64467 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600FBD4 RID: 64468 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x1700467A RID: 18042
		// (get) Token: 0x0600FBD5 RID: 64469 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600FBD6 RID: 64470 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x1700467B RID: 18043
		// (get) Token: 0x0600FBD7 RID: 64471 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600FBD8 RID: 64472 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600FBDA RID: 64474 RVA: 0x002DABEC File Offset: 0x002D8DEC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "getPressed" == name)
			{
				return new StringValue();
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
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
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

		// Token: 0x0600FBDB RID: 64475 RVA: 0x002DAE7F File Offset: 0x002D907F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VisibleToggleButton>(deep);
		}

		// Token: 0x0600FBDC RID: 64476 RVA: 0x002DAE88 File Offset: 0x002D9088
		// Note: this type is marked as 'beforefieldinit'.
		static VisibleToggleButton()
		{
			byte[] array = new byte[29];
			VisibleToggleButton.attributeNamespaceIds = array;
		}

		// Token: 0x040071B7 RID: 29111
		private const string tagName = "toggleButton";

		// Token: 0x040071B8 RID: 29112
		private const byte tagNsId = 57;

		// Token: 0x040071B9 RID: 29113
		internal const int ElementTypeIdConst = 13081;

		// Token: 0x040071BA RID: 29114
		private static string[] attributeTagNames = new string[]
		{
			"getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id",
			"idQ", "tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x040071BB RID: 29115
		private static byte[] attributeNamespaceIds;
	}
}
