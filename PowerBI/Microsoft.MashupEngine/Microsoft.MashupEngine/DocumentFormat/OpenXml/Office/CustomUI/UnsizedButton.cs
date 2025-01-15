using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200226F RID: 8815
	[GeneratedCode("DomGen", "2.0")]
	internal class UnsizedButton : OpenXmlLeafElement
	{
		// Token: 0x17003CFB RID: 15611
		// (get) Token: 0x0600E7E8 RID: 59368 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x17003CFC RID: 15612
		// (get) Token: 0x0600E7E9 RID: 59369 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003CFD RID: 15613
		// (get) Token: 0x0600E7EA RID: 59370 RVA: 0x002C8B21 File Offset: 0x002C6D21
		internal override int ElementTypeId
		{
			get
			{
				return 12574;
			}
		}

		// Token: 0x0600E7EB RID: 59371 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003CFE RID: 15614
		// (get) Token: 0x0600E7EC RID: 59372 RVA: 0x002C8B28 File Offset: 0x002C6D28
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsizedButton.attributeTagNames;
			}
		}

		// Token: 0x17003CFF RID: 15615
		// (get) Token: 0x0600E7ED RID: 59373 RVA: 0x002C8B2F File Offset: 0x002C6D2F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsizedButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17003D00 RID: 15616
		// (get) Token: 0x0600E7EE RID: 59374 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E7EF RID: 59375 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003D01 RID: 15617
		// (get) Token: 0x0600E7F0 RID: 59376 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600E7F1 RID: 59377 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003D02 RID: 15618
		// (get) Token: 0x0600E7F2 RID: 59378 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E7F3 RID: 59379 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003D03 RID: 15619
		// (get) Token: 0x0600E7F4 RID: 59380 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E7F5 RID: 59381 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003D04 RID: 15620
		// (get) Token: 0x0600E7F6 RID: 59382 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E7F7 RID: 59383 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003D05 RID: 15621
		// (get) Token: 0x0600E7F8 RID: 59384 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E7F9 RID: 59385 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003D06 RID: 15622
		// (get) Token: 0x0600E7FA RID: 59386 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E7FB RID: 59387 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003D07 RID: 15623
		// (get) Token: 0x0600E7FC RID: 59388 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E7FD RID: 59389 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003D08 RID: 15624
		// (get) Token: 0x0600E7FE RID: 59390 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E7FF RID: 59391 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003D09 RID: 15625
		// (get) Token: 0x0600E800 RID: 59392 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E801 RID: 59393 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003D0A RID: 15626
		// (get) Token: 0x0600E802 RID: 59394 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E803 RID: 59395 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003D0B RID: 15627
		// (get) Token: 0x0600E804 RID: 59396 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E805 RID: 59397 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003D0C RID: 15628
		// (get) Token: 0x0600E806 RID: 59398 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E807 RID: 59399 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003D0D RID: 15629
		// (get) Token: 0x0600E808 RID: 59400 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E809 RID: 59401 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003D0E RID: 15630
		// (get) Token: 0x0600E80A RID: 59402 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600E80B RID: 59403 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003D0F RID: 15631
		// (get) Token: 0x0600E80C RID: 59404 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E80D RID: 59405 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003D10 RID: 15632
		// (get) Token: 0x0600E80E RID: 59406 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E80F RID: 59407 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003D11 RID: 15633
		// (get) Token: 0x0600E810 RID: 59408 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E811 RID: 59409 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003D12 RID: 15634
		// (get) Token: 0x0600E812 RID: 59410 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E813 RID: 59411 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003D13 RID: 15635
		// (get) Token: 0x0600E814 RID: 59412 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E815 RID: 59413 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003D14 RID: 15636
		// (get) Token: 0x0600E816 RID: 59414 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600E817 RID: 59415 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003D15 RID: 15637
		// (get) Token: 0x0600E818 RID: 59416 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E819 RID: 59417 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003D16 RID: 15638
		// (get) Token: 0x0600E81A RID: 59418 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600E81B RID: 59419 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003D17 RID: 15639
		// (get) Token: 0x0600E81C RID: 59420 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600E81D RID: 59421 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003D18 RID: 15640
		// (get) Token: 0x0600E81E RID: 59422 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600E81F RID: 59423 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003D19 RID: 15641
		// (get) Token: 0x0600E820 RID: 59424 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E821 RID: 59425 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003D1A RID: 15642
		// (get) Token: 0x0600E822 RID: 59426 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600E823 RID: 59427 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003D1B RID: 15643
		// (get) Token: 0x0600E824 RID: 59428 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E825 RID: 59429 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003D1C RID: 15644
		// (get) Token: 0x0600E826 RID: 59430 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600E827 RID: 59431 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17003D1D RID: 15645
		// (get) Token: 0x0600E828 RID: 59432 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E829 RID: 59433 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600E82B RID: 59435 RVA: 0x002C8B68 File Offset: 0x002C6D68
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

		// Token: 0x0600E82C RID: 59436 RVA: 0x002C8E11 File Offset: 0x002C7011
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnsizedButton>(deep);
		}

		// Token: 0x0600E82D RID: 59437 RVA: 0x002C8E1C File Offset: 0x002C701C
		// Note: this type is marked as 'beforefieldinit'.
		static UnsizedButton()
		{
			byte[] array = new byte[30];
			UnsizedButton.attributeNamespaceIds = array;
		}

		// Token: 0x04006F8A RID: 28554
		private const string tagName = "button";

		// Token: 0x04006F8B RID: 28555
		private const byte tagNsId = 34;

		// Token: 0x04006F8C RID: 28556
		internal const int ElementTypeIdConst = 12574;

		// Token: 0x04006F8D RID: 28557
		private static string[] attributeTagNames = new string[]
		{
			"onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id", "idQ",
			"idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006F8E RID: 28558
		private static byte[] attributeNamespaceIds;
	}
}
