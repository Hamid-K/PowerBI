using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002289 RID: 8841
	[GeneratedCode("DomGen", "2.0")]
	internal class VisibleButton : OpenXmlLeafElement
	{
		// Token: 0x1700400C RID: 16396
		// (get) Token: 0x0600EE42 RID: 60994 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x1700400D RID: 16397
		// (get) Token: 0x0600EE43 RID: 60995 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x1700400E RID: 16398
		// (get) Token: 0x0600EE44 RID: 60996 RVA: 0x002CED87 File Offset: 0x002CCF87
		internal override int ElementTypeId
		{
			get
			{
				return 12600;
			}
		}

		// Token: 0x0600EE45 RID: 60997 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700400F RID: 16399
		// (get) Token: 0x0600EE46 RID: 60998 RVA: 0x002CED8E File Offset: 0x002CCF8E
		internal override string[] AttributeTagNames
		{
			get
			{
				return VisibleButton.attributeTagNames;
			}
		}

		// Token: 0x17004010 RID: 16400
		// (get) Token: 0x0600EE47 RID: 60999 RVA: 0x002CED95 File Offset: 0x002CCF95
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VisibleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17004011 RID: 16401
		// (get) Token: 0x0600EE48 RID: 61000 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EE49 RID: 61001 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004012 RID: 16402
		// (get) Token: 0x0600EE4A RID: 61002 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600EE4B RID: 61003 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004013 RID: 16403
		// (get) Token: 0x0600EE4C RID: 61004 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EE4D RID: 61005 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004014 RID: 16404
		// (get) Token: 0x0600EE4E RID: 61006 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EE4F RID: 61007 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004015 RID: 16405
		// (get) Token: 0x0600EE50 RID: 61008 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EE51 RID: 61009 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004016 RID: 16406
		// (get) Token: 0x0600EE52 RID: 61010 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EE53 RID: 61011 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17004017 RID: 16407
		// (get) Token: 0x0600EE54 RID: 61012 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EE55 RID: 61013 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17004018 RID: 16408
		// (get) Token: 0x0600EE56 RID: 61014 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EE57 RID: 61015 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17004019 RID: 16409
		// (get) Token: 0x0600EE58 RID: 61016 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EE59 RID: 61017 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x1700401A RID: 16410
		// (get) Token: 0x0600EE5A RID: 61018 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EE5B RID: 61019 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x1700401B RID: 16411
		// (get) Token: 0x0600EE5C RID: 61020 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EE5D RID: 61021 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700401C RID: 16412
		// (get) Token: 0x0600EE5E RID: 61022 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EE5F RID: 61023 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x1700401D RID: 16413
		// (get) Token: 0x0600EE60 RID: 61024 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EE61 RID: 61025 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x1700401E RID: 16414
		// (get) Token: 0x0600EE62 RID: 61026 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EE63 RID: 61027 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x1700401F RID: 16415
		// (get) Token: 0x0600EE64 RID: 61028 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EE65 RID: 61029 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17004020 RID: 16416
		// (get) Token: 0x0600EE66 RID: 61030 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EE67 RID: 61031 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17004021 RID: 16417
		// (get) Token: 0x0600EE68 RID: 61032 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EE69 RID: 61033 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17004022 RID: 16418
		// (get) Token: 0x0600EE6A RID: 61034 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EE6B RID: 61035 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17004023 RID: 16419
		// (get) Token: 0x0600EE6C RID: 61036 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EE6D RID: 61037 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17004024 RID: 16420
		// (get) Token: 0x0600EE6E RID: 61038 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EE6F RID: 61039 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17004025 RID: 16421
		// (get) Token: 0x0600EE70 RID: 61040 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EE71 RID: 61041 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17004026 RID: 16422
		// (get) Token: 0x0600EE72 RID: 61042 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EE73 RID: 61043 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17004027 RID: 16423
		// (get) Token: 0x0600EE74 RID: 61044 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600EE75 RID: 61045 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17004028 RID: 16424
		// (get) Token: 0x0600EE76 RID: 61046 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EE77 RID: 61047 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17004029 RID: 16425
		// (get) Token: 0x0600EE78 RID: 61048 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0600EE79 RID: 61049 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x1700402A RID: 16426
		// (get) Token: 0x0600EE7A RID: 61050 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EE7B RID: 61051 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x1700402B RID: 16427
		// (get) Token: 0x0600EE7C RID: 61052 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600EE7D RID: 61053 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x1700402C RID: 16428
		// (get) Token: 0x0600EE7E RID: 61054 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EE7F RID: 61055 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x0600EE81 RID: 61057 RVA: 0x002CED9C File Offset: 0x002CCF9C
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

		// Token: 0x0600EE82 RID: 61058 RVA: 0x002CF019 File Offset: 0x002CD219
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VisibleButton>(deep);
		}

		// Token: 0x0600EE83 RID: 61059 RVA: 0x002CF024 File Offset: 0x002CD224
		// Note: this type is marked as 'beforefieldinit'.
		static VisibleButton()
		{
			byte[] array = new byte[28];
			VisibleButton.attributeNamespaceIds = array;
		}

		// Token: 0x0400700E RID: 28686
		private const string tagName = "button";

		// Token: 0x0400700F RID: 28687
		private const byte tagNsId = 34;

		// Token: 0x04007010 RID: 28688
		internal const int ElementTypeIdConst = 12600;

		// Token: 0x04007011 RID: 28689
		private static string[] attributeTagNames = new string[]
		{
			"onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id", "idQ",
			"idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04007012 RID: 28690
		private static byte[] attributeNamespaceIds;
	}
}
