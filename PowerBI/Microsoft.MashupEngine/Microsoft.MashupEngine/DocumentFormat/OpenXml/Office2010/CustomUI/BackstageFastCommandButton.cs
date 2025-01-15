using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002309 RID: 8969
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageFastCommandButton : OpenXmlLeafElement
	{
		// Token: 0x170047F3 RID: 18419
		// (get) Token: 0x0600FF21 RID: 65313 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x170047F4 RID: 18420
		// (get) Token: 0x0600FF22 RID: 65314 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170047F5 RID: 18421
		// (get) Token: 0x0600FF23 RID: 65315 RVA: 0x002DDB93 File Offset: 0x002DBD93
		internal override int ElementTypeId
		{
			get
			{
				return 13111;
			}
		}

		// Token: 0x0600FF24 RID: 65316 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170047F6 RID: 18422
		// (get) Token: 0x0600FF25 RID: 65317 RVA: 0x002DDB9A File Offset: 0x002DBD9A
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageFastCommandButton.attributeTagNames;
			}
		}

		// Token: 0x170047F7 RID: 18423
		// (get) Token: 0x0600FF26 RID: 65318 RVA: 0x002DDBA1 File Offset: 0x002DBDA1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageFastCommandButton.attributeNamespaceIds;
			}
		}

		// Token: 0x170047F8 RID: 18424
		// (get) Token: 0x0600FF27 RID: 65319 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FF28 RID: 65320 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170047F9 RID: 18425
		// (get) Token: 0x0600FF29 RID: 65321 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FF2A RID: 65322 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x170047FA RID: 18426
		// (get) Token: 0x0600FF2B RID: 65323 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FF2C RID: 65324 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x170047FB RID: 18427
		// (get) Token: 0x0600FF2D RID: 65325 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FF2E RID: 65326 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170047FC RID: 18428
		// (get) Token: 0x0600FF2F RID: 65327 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FF30 RID: 65328 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170047FD RID: 18429
		// (get) Token: 0x0600FF31 RID: 65329 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FF32 RID: 65330 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170047FE RID: 18430
		// (get) Token: 0x0600FF33 RID: 65331 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FF34 RID: 65332 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170047FF RID: 18431
		// (get) Token: 0x0600FF35 RID: 65333 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FF36 RID: 65334 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17004800 RID: 18432
		// (get) Token: 0x0600FF37 RID: 65335 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FF38 RID: 65336 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004801 RID: 18433
		// (get) Token: 0x0600FF39 RID: 65337 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600FF3A RID: 65338 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "isDefinitive")]
		public BooleanValue IsDefinitive
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

		// Token: 0x17004802 RID: 18434
		// (get) Token: 0x0600FF3B RID: 65339 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600FF3C RID: 65340 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004803 RID: 18435
		// (get) Token: 0x0600FF3D RID: 65341 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FF3E RID: 65342 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004804 RID: 18436
		// (get) Token: 0x0600FF3F RID: 65343 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FF40 RID: 65344 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004805 RID: 18437
		// (get) Token: 0x0600FF41 RID: 65345 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FF42 RID: 65346 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004806 RID: 18438
		// (get) Token: 0x0600FF43 RID: 65347 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600FF44 RID: 65348 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17004807 RID: 18439
		// (get) Token: 0x0600FF45 RID: 65349 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FF46 RID: 65350 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004808 RID: 18440
		// (get) Token: 0x0600FF47 RID: 65351 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FF48 RID: 65352 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004809 RID: 18441
		// (get) Token: 0x0600FF49 RID: 65353 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FF4A RID: 65354 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x1700480A RID: 18442
		// (get) Token: 0x0600FF4B RID: 65355 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FF4C RID: 65356 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x1700480B RID: 18443
		// (get) Token: 0x0600FF4D RID: 65357 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FF4E RID: 65358 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x1700480C RID: 18444
		// (get) Token: 0x0600FF4F RID: 65359 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FF50 RID: 65360 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x0600FF52 RID: 65362 RVA: 0x002DDBA8 File Offset: 0x002DBDA8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idMso" == name)
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
			if (namespaceId == 0 && "onAction" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "isDefinitive" == name)
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
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FF53 RID: 65363 RVA: 0x002DDD8B File Offset: 0x002DBF8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageFastCommandButton>(deep);
		}

		// Token: 0x0600FF54 RID: 65364 RVA: 0x002DDD94 File Offset: 0x002DBF94
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageFastCommandButton()
		{
			byte[] array = new byte[21];
			BackstageFastCommandButton.attributeNamespaceIds = array;
		}

		// Token: 0x0400723D RID: 29245
		private const string tagName = "button";

		// Token: 0x0400723E RID: 29246
		private const byte tagNsId = 57;

		// Token: 0x0400723F RID: 29247
		internal const int ElementTypeIdConst = 13111;

		// Token: 0x04007240 RID: 29248
		private static string[] attributeTagNames = new string[]
		{
			"idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "id", "idQ", "tag", "onAction", "isDefinitive",
			"enabled", "getEnabled", "label", "getLabel", "visible", "getVisible", "keytip", "getKeytip", "image", "imageMso",
			"getImage"
		};

		// Token: 0x04007241 RID: 29249
		private static byte[] attributeNamespaceIds;
	}
}
