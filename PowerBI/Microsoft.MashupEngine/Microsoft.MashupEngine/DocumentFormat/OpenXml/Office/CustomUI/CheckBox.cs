using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002270 RID: 8816
	[GeneratedCode("DomGen", "2.0")]
	internal class CheckBox : OpenXmlLeafElement
	{
		// Token: 0x17003D1E RID: 15646
		// (get) Token: 0x0600E82E RID: 59438 RVA: 0x002C8F4A File Offset: 0x002C714A
		public override string LocalName
		{
			get
			{
				return "checkBox";
			}
		}

		// Token: 0x17003D1F RID: 15647
		// (get) Token: 0x0600E82F RID: 59439 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003D20 RID: 15648
		// (get) Token: 0x0600E830 RID: 59440 RVA: 0x002C8F51 File Offset: 0x002C7151
		internal override int ElementTypeId
		{
			get
			{
				return 12575;
			}
		}

		// Token: 0x0600E831 RID: 59441 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003D21 RID: 15649
		// (get) Token: 0x0600E832 RID: 59442 RVA: 0x002C8F58 File Offset: 0x002C7158
		internal override string[] AttributeTagNames
		{
			get
			{
				return CheckBox.attributeTagNames;
			}
		}

		// Token: 0x17003D22 RID: 15650
		// (get) Token: 0x0600E833 RID: 59443 RVA: 0x002C8F5F File Offset: 0x002C715F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CheckBox.attributeNamespaceIds;
			}
		}

		// Token: 0x17003D23 RID: 15651
		// (get) Token: 0x0600E834 RID: 59444 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E835 RID: 59445 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003D24 RID: 15652
		// (get) Token: 0x0600E836 RID: 59446 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E837 RID: 59447 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003D25 RID: 15653
		// (get) Token: 0x0600E838 RID: 59448 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600E839 RID: 59449 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003D26 RID: 15654
		// (get) Token: 0x0600E83A RID: 59450 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E83B RID: 59451 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003D27 RID: 15655
		// (get) Token: 0x0600E83C RID: 59452 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E83D RID: 59453 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003D28 RID: 15656
		// (get) Token: 0x0600E83E RID: 59454 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E83F RID: 59455 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003D29 RID: 15657
		// (get) Token: 0x0600E840 RID: 59456 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E841 RID: 59457 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003D2A RID: 15658
		// (get) Token: 0x0600E842 RID: 59458 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E843 RID: 59459 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003D2B RID: 15659
		// (get) Token: 0x0600E844 RID: 59460 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E845 RID: 59461 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003D2C RID: 15660
		// (get) Token: 0x0600E846 RID: 59462 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E847 RID: 59463 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003D2D RID: 15661
		// (get) Token: 0x0600E848 RID: 59464 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E849 RID: 59465 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003D2E RID: 15662
		// (get) Token: 0x0600E84A RID: 59466 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E84B RID: 59467 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003D2F RID: 15663
		// (get) Token: 0x0600E84C RID: 59468 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E84D RID: 59469 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003D30 RID: 15664
		// (get) Token: 0x0600E84E RID: 59470 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E84F RID: 59471 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003D31 RID: 15665
		// (get) Token: 0x0600E850 RID: 59472 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600E851 RID: 59473 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003D32 RID: 15666
		// (get) Token: 0x0600E852 RID: 59474 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E853 RID: 59475 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003D33 RID: 15667
		// (get) Token: 0x0600E854 RID: 59476 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E855 RID: 59477 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003D34 RID: 15668
		// (get) Token: 0x0600E856 RID: 59478 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E857 RID: 59479 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003D35 RID: 15669
		// (get) Token: 0x0600E858 RID: 59480 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E859 RID: 59481 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003D36 RID: 15670
		// (get) Token: 0x0600E85A RID: 59482 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E85B RID: 59483 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003D37 RID: 15671
		// (get) Token: 0x0600E85C RID: 59484 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x0600E85D RID: 59485 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17003D38 RID: 15672
		// (get) Token: 0x0600E85E RID: 59486 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E85F RID: 59487 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003D39 RID: 15673
		// (get) Token: 0x0600E860 RID: 59488 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600E861 RID: 59489 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003D3A RID: 15674
		// (get) Token: 0x0600E862 RID: 59490 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600E863 RID: 59491 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x0600E865 RID: 59493 RVA: 0x002C8F88 File Offset: 0x002C7188
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E866 RID: 59494 RVA: 0x002C91AD File Offset: 0x002C73AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CheckBox>(deep);
		}

		// Token: 0x0600E867 RID: 59495 RVA: 0x002C91B8 File Offset: 0x002C73B8
		// Note: this type is marked as 'beforefieldinit'.
		static CheckBox()
		{
			byte[] array = new byte[24];
			CheckBox.attributeNamespaceIds = array;
		}

		// Token: 0x04006F8F RID: 28559
		private const string tagName = "checkBox";

		// Token: 0x04006F90 RID: 28560
		private const byte tagNsId = 34;

		// Token: 0x04006F91 RID: 28561
		internal const int ElementTypeIdConst = 12575;

		// Token: 0x04006F92 RID: 28562
		private static string[] attributeTagNames = new string[]
		{
			"getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "id", "idQ", "idMso", "tag",
			"screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip"
		};

		// Token: 0x04006F93 RID: 28563
		private static byte[] attributeNamespaceIds;
	}
}
