using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002272 RID: 8818
	[GeneratedCode("DomGen", "2.0")]
	internal class UnsizedToggleButton : OpenXmlLeafElement
	{
		// Token: 0x17003D70 RID: 15728
		// (get) Token: 0x0600E8D6 RID: 59606 RVA: 0x002C99C0 File Offset: 0x002C7BC0
		public override string LocalName
		{
			get
			{
				return "toggleButton";
			}
		}

		// Token: 0x17003D71 RID: 15729
		// (get) Token: 0x0600E8D7 RID: 59607 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003D72 RID: 15730
		// (get) Token: 0x0600E8D8 RID: 59608 RVA: 0x002C99C7 File Offset: 0x002C7BC7
		internal override int ElementTypeId
		{
			get
			{
				return 12577;
			}
		}

		// Token: 0x0600E8D9 RID: 59609 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003D73 RID: 15731
		// (get) Token: 0x0600E8DA RID: 59610 RVA: 0x002C99CE File Offset: 0x002C7BCE
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsizedToggleButton.attributeTagNames;
			}
		}

		// Token: 0x17003D74 RID: 15732
		// (get) Token: 0x0600E8DB RID: 59611 RVA: 0x002C99D5 File Offset: 0x002C7BD5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsizedToggleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17003D75 RID: 15733
		// (get) Token: 0x0600E8DC RID: 59612 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E8DD RID: 59613 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003D76 RID: 15734
		// (get) Token: 0x0600E8DE RID: 59614 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E8DF RID: 59615 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003D77 RID: 15735
		// (get) Token: 0x0600E8E0 RID: 59616 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600E8E1 RID: 59617 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003D78 RID: 15736
		// (get) Token: 0x0600E8E2 RID: 59618 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E8E3 RID: 59619 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003D79 RID: 15737
		// (get) Token: 0x0600E8E4 RID: 59620 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E8E5 RID: 59621 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003D7A RID: 15738
		// (get) Token: 0x0600E8E6 RID: 59622 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E8E7 RID: 59623 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003D7B RID: 15739
		// (get) Token: 0x0600E8E8 RID: 59624 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E8E9 RID: 59625 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003D7C RID: 15740
		// (get) Token: 0x0600E8EA RID: 59626 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E8EB RID: 59627 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003D7D RID: 15741
		// (get) Token: 0x0600E8EC RID: 59628 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E8ED RID: 59629 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003D7E RID: 15742
		// (get) Token: 0x0600E8EE RID: 59630 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E8EF RID: 59631 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003D7F RID: 15743
		// (get) Token: 0x0600E8F0 RID: 59632 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E8F1 RID: 59633 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003D80 RID: 15744
		// (get) Token: 0x0600E8F2 RID: 59634 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E8F3 RID: 59635 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003D81 RID: 15745
		// (get) Token: 0x0600E8F4 RID: 59636 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E8F5 RID: 59637 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003D82 RID: 15746
		// (get) Token: 0x0600E8F6 RID: 59638 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E8F7 RID: 59639 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003D83 RID: 15747
		// (get) Token: 0x0600E8F8 RID: 59640 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600E8F9 RID: 59641 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003D84 RID: 15748
		// (get) Token: 0x0600E8FA RID: 59642 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E8FB RID: 59643 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003D85 RID: 15749
		// (get) Token: 0x0600E8FC RID: 59644 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E8FD RID: 59645 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003D86 RID: 15750
		// (get) Token: 0x0600E8FE RID: 59646 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E8FF RID: 59647 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003D87 RID: 15751
		// (get) Token: 0x0600E900 RID: 59648 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E901 RID: 59649 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003D88 RID: 15752
		// (get) Token: 0x0600E902 RID: 59650 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E903 RID: 59651 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003D89 RID: 15753
		// (get) Token: 0x0600E904 RID: 59652 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600E905 RID: 59653 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003D8A RID: 15754
		// (get) Token: 0x0600E906 RID: 59654 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E907 RID: 59655 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003D8B RID: 15755
		// (get) Token: 0x0600E908 RID: 59656 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600E909 RID: 59657 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003D8C RID: 15756
		// (get) Token: 0x0600E90A RID: 59658 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x0600E90B RID: 59659 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17003D8D RID: 15757
		// (get) Token: 0x0600E90C RID: 59660 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600E90D RID: 59661 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003D8E RID: 15758
		// (get) Token: 0x0600E90E RID: 59662 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E90F RID: 59663 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003D8F RID: 15759
		// (get) Token: 0x0600E910 RID: 59664 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E911 RID: 59665 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003D90 RID: 15760
		// (get) Token: 0x0600E912 RID: 59666 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600E913 RID: 59667 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003D91 RID: 15761
		// (get) Token: 0x0600E914 RID: 59668 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E915 RID: 59669 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003D92 RID: 15762
		// (get) Token: 0x0600E916 RID: 59670 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600E917 RID: 59671 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17003D93 RID: 15763
		// (get) Token: 0x0600E918 RID: 59672 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600E919 RID: 59673 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600E91B RID: 59675 RVA: 0x002C9A0C File Offset: 0x002C7C0C
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

		// Token: 0x0600E91C RID: 59676 RVA: 0x002C9CCB File Offset: 0x002C7ECB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnsizedToggleButton>(deep);
		}

		// Token: 0x0600E91D RID: 59677 RVA: 0x002C9CD4 File Offset: 0x002C7ED4
		// Note: this type is marked as 'beforefieldinit'.
		static UnsizedToggleButton()
		{
			byte[] array = new byte[31];
			UnsizedToggleButton.attributeNamespaceIds = array;
		}

		// Token: 0x04006F99 RID: 28569
		private const string tagName = "toggleButton";

		// Token: 0x04006F9A RID: 28570
		private const byte tagNsId = 34;

		// Token: 0x04006F9B RID: 28571
		internal const int ElementTypeIdConst = 12577;

		// Token: 0x04006F9C RID: 28572
		private static string[] attributeTagNames = new string[]
		{
			"getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id",
			"idQ", "idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage",
			"getShowImage"
		};

		// Token: 0x04006F9D RID: 28573
		private static byte[] attributeNamespaceIds;
	}
}
