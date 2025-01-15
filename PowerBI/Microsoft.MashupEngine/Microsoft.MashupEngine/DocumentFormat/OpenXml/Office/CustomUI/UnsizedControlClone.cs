using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200226E RID: 8814
	[GeneratedCode("DomGen", "2.0")]
	internal class UnsizedControlClone : OpenXmlLeafElement
	{
		// Token: 0x17003CDC RID: 15580
		// (get) Token: 0x0600E7AA RID: 59306 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x17003CDD RID: 15581
		// (get) Token: 0x0600E7AB RID: 59307 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003CDE RID: 15582
		// (get) Token: 0x0600E7AC RID: 59308 RVA: 0x002C874D File Offset: 0x002C694D
		internal override int ElementTypeId
		{
			get
			{
				return 12573;
			}
		}

		// Token: 0x0600E7AD RID: 59309 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003CDF RID: 15583
		// (get) Token: 0x0600E7AE RID: 59310 RVA: 0x002C8754 File Offset: 0x002C6954
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsizedControlClone.attributeTagNames;
			}
		}

		// Token: 0x17003CE0 RID: 15584
		// (get) Token: 0x0600E7AF RID: 59311 RVA: 0x002C875B File Offset: 0x002C695B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsizedControlClone.attributeNamespaceIds;
			}
		}

		// Token: 0x17003CE1 RID: 15585
		// (get) Token: 0x0600E7B0 RID: 59312 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E7B1 RID: 59313 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003CE2 RID: 15586
		// (get) Token: 0x0600E7B2 RID: 59314 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E7B3 RID: 59315 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003CE3 RID: 15587
		// (get) Token: 0x0600E7B4 RID: 59316 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E7B5 RID: 59317 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003CE4 RID: 15588
		// (get) Token: 0x0600E7B6 RID: 59318 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E7B7 RID: 59319 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003CE5 RID: 15589
		// (get) Token: 0x0600E7B8 RID: 59320 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E7B9 RID: 59321 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003CE6 RID: 15590
		// (get) Token: 0x0600E7BA RID: 59322 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E7BB RID: 59323 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003CE7 RID: 15591
		// (get) Token: 0x0600E7BC RID: 59324 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E7BD RID: 59325 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003CE8 RID: 15592
		// (get) Token: 0x0600E7BE RID: 59326 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E7BF RID: 59327 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003CE9 RID: 15593
		// (get) Token: 0x0600E7C0 RID: 59328 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E7C1 RID: 59329 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003CEA RID: 15594
		// (get) Token: 0x0600E7C2 RID: 59330 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E7C3 RID: 59331 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003CEB RID: 15595
		// (get) Token: 0x0600E7C4 RID: 59332 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600E7C5 RID: 59333 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003CEC RID: 15596
		// (get) Token: 0x0600E7C6 RID: 59334 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E7C7 RID: 59335 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003CED RID: 15597
		// (get) Token: 0x0600E7C8 RID: 59336 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E7C9 RID: 59337 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003CEE RID: 15598
		// (get) Token: 0x0600E7CA RID: 59338 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E7CB RID: 59339 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003CEF RID: 15599
		// (get) Token: 0x0600E7CC RID: 59340 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600E7CD RID: 59341 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003CF0 RID: 15600
		// (get) Token: 0x0600E7CE RID: 59342 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E7CF RID: 59343 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003CF1 RID: 15601
		// (get) Token: 0x0600E7D0 RID: 59344 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E7D1 RID: 59345 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003CF2 RID: 15602
		// (get) Token: 0x0600E7D2 RID: 59346 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E7D3 RID: 59347 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003CF3 RID: 15603
		// (get) Token: 0x0600E7D4 RID: 59348 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0600E7D5 RID: 59349 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003CF4 RID: 15604
		// (get) Token: 0x0600E7D6 RID: 59350 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E7D7 RID: 59351 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003CF5 RID: 15605
		// (get) Token: 0x0600E7D8 RID: 59352 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600E7D9 RID: 59353 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003CF6 RID: 15606
		// (get) Token: 0x0600E7DA RID: 59354 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E7DB RID: 59355 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003CF7 RID: 15607
		// (get) Token: 0x0600E7DC RID: 59356 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600E7DD RID: 59357 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003CF8 RID: 15608
		// (get) Token: 0x0600E7DE RID: 59358 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600E7DF RID: 59359 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003CF9 RID: 15609
		// (get) Token: 0x0600E7E0 RID: 59360 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0600E7E1 RID: 59361 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17003CFA RID: 15610
		// (get) Token: 0x0600E7E2 RID: 59362 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E7E3 RID: 59363 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600E7E5 RID: 59365 RVA: 0x002C87B4 File Offset: 0x002C69B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x0600E7E6 RID: 59366 RVA: 0x002C8A05 File Offset: 0x002C6C05
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnsizedControlClone>(deep);
		}

		// Token: 0x0600E7E7 RID: 59367 RVA: 0x002C8A10 File Offset: 0x002C6C10
		// Note: this type is marked as 'beforefieldinit'.
		static UnsizedControlClone()
		{
			byte[] array = new byte[26];
			UnsizedControlClone.attributeNamespaceIds = array;
		}

		// Token: 0x04006F85 RID: 28549
		private const string tagName = "control";

		// Token: 0x04006F86 RID: 28550
		private const byte tagNsId = 34;

		// Token: 0x04006F87 RID: 28551
		internal const int ElementTypeIdConst = 12573;

		// Token: 0x04006F88 RID: 28552
		private static string[] attributeTagNames = new string[]
		{
			"idQ", "idMso", "tag", "image", "imageMso", "getImage", "screentip", "getScreentip", "supertip", "getSupertip",
			"enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible",
			"keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006F89 RID: 28553
		private static byte[] attributeNamespaceIds;
	}
}
