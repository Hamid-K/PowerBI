using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E7 RID: 8935
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class VisibleButton : OpenXmlLeafElement
	{
		// Token: 0x17004639 RID: 17977
		// (get) Token: 0x0600FB57 RID: 64343 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x1700463A RID: 17978
		// (get) Token: 0x0600FB58 RID: 64344 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700463B RID: 17979
		// (get) Token: 0x0600FB59 RID: 64345 RVA: 0x002DA81B File Offset: 0x002D8A1B
		internal override int ElementTypeId
		{
			get
			{
				return 13080;
			}
		}

		// Token: 0x0600FB5A RID: 64346 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700463C RID: 17980
		// (get) Token: 0x0600FB5B RID: 64347 RVA: 0x002DA822 File Offset: 0x002D8A22
		internal override string[] AttributeTagNames
		{
			get
			{
				return VisibleButton.attributeTagNames;
			}
		}

		// Token: 0x1700463D RID: 17981
		// (get) Token: 0x0600FB5C RID: 64348 RVA: 0x002DA829 File Offset: 0x002D8A29
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VisibleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x1700463E RID: 17982
		// (get) Token: 0x0600FB5D RID: 64349 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FB5E RID: 64350 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700463F RID: 17983
		// (get) Token: 0x0600FB5F RID: 64351 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600FB60 RID: 64352 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004640 RID: 17984
		// (get) Token: 0x0600FB61 RID: 64353 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FB62 RID: 64354 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004641 RID: 17985
		// (get) Token: 0x0600FB63 RID: 64355 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FB64 RID: 64356 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17004642 RID: 17986
		// (get) Token: 0x0600FB65 RID: 64357 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FB66 RID: 64358 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17004643 RID: 17987
		// (get) Token: 0x0600FB67 RID: 64359 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FB68 RID: 64360 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004644 RID: 17988
		// (get) Token: 0x0600FB69 RID: 64361 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FB6A RID: 64362 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004645 RID: 17989
		// (get) Token: 0x0600FB6B RID: 64363 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FB6C RID: 64364 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004646 RID: 17990
		// (get) Token: 0x0600FB6D RID: 64365 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FB6E RID: 64366 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004647 RID: 17991
		// (get) Token: 0x0600FB6F RID: 64367 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FB70 RID: 64368 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004648 RID: 17992
		// (get) Token: 0x0600FB71 RID: 64369 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FB72 RID: 64370 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17004649 RID: 17993
		// (get) Token: 0x0600FB73 RID: 64371 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FB74 RID: 64372 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700464A RID: 17994
		// (get) Token: 0x0600FB75 RID: 64373 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FB76 RID: 64374 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x1700464B RID: 17995
		// (get) Token: 0x0600FB77 RID: 64375 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FB78 RID: 64376 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x1700464C RID: 17996
		// (get) Token: 0x0600FB79 RID: 64377 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FB7A RID: 64378 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x1700464D RID: 17997
		// (get) Token: 0x0600FB7B RID: 64379 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FB7C RID: 64380 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x1700464E RID: 17998
		// (get) Token: 0x0600FB7D RID: 64381 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FB7E RID: 64382 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x1700464F RID: 17999
		// (get) Token: 0x0600FB7F RID: 64383 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FB80 RID: 64384 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17004650 RID: 18000
		// (get) Token: 0x0600FB81 RID: 64385 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FB82 RID: 64386 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17004651 RID: 18001
		// (get) Token: 0x0600FB83 RID: 64387 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FB84 RID: 64388 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17004652 RID: 18002
		// (get) Token: 0x0600FB85 RID: 64389 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FB86 RID: 64390 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17004653 RID: 18003
		// (get) Token: 0x0600FB87 RID: 64391 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600FB88 RID: 64392 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17004654 RID: 18004
		// (get) Token: 0x0600FB89 RID: 64393 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600FB8A RID: 64394 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004655 RID: 18005
		// (get) Token: 0x0600FB8B RID: 64395 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600FB8C RID: 64396 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17004656 RID: 18006
		// (get) Token: 0x0600FB8D RID: 64397 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0600FB8E RID: 64398 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17004657 RID: 18007
		// (get) Token: 0x0600FB8F RID: 64399 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600FB90 RID: 64400 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17004658 RID: 18008
		// (get) Token: 0x0600FB91 RID: 64401 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600FB92 RID: 64402 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17004659 RID: 18009
		// (get) Token: 0x0600FB93 RID: 64403 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600FB94 RID: 64404 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600FB96 RID: 64406 RVA: 0x002DA830 File Offset: 0x002D8A30
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

		// Token: 0x0600FB97 RID: 64407 RVA: 0x002DAAAD File Offset: 0x002D8CAD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VisibleButton>(deep);
		}

		// Token: 0x0600FB98 RID: 64408 RVA: 0x002DAAB8 File Offset: 0x002D8CB8
		// Note: this type is marked as 'beforefieldinit'.
		static VisibleButton()
		{
			byte[] array = new byte[28];
			VisibleButton.attributeNamespaceIds = array;
		}

		// Token: 0x040071B2 RID: 29106
		private const string tagName = "button";

		// Token: 0x040071B3 RID: 29107
		private const byte tagNsId = 57;

		// Token: 0x040071B4 RID: 29108
		internal const int ElementTypeIdConst = 13080;

		// Token: 0x040071B5 RID: 29109
		private static string[] attributeTagNames = new string[]
		{
			"onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id", "idQ",
			"tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x040071B6 RID: 29110
		private static byte[] attributeNamespaceIds;
	}
}
