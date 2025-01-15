using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022EC RID: 8940
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ControlCloneQat : OpenXmlLeafElement
	{
		// Token: 0x170046AD RID: 18093
		// (get) Token: 0x0600FC45 RID: 64581 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x170046AE RID: 18094
		// (get) Token: 0x0600FC46 RID: 64582 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170046AF RID: 18095
		// (get) Token: 0x0600FC47 RID: 64583 RVA: 0x002DB65B File Offset: 0x002D985B
		internal override int ElementTypeId
		{
			get
			{
				return 13085;
			}
		}

		// Token: 0x0600FC48 RID: 64584 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170046B0 RID: 18096
		// (get) Token: 0x0600FC49 RID: 64585 RVA: 0x002DB662 File Offset: 0x002D9862
		internal override string[] AttributeTagNames
		{
			get
			{
				return ControlCloneQat.attributeTagNames;
			}
		}

		// Token: 0x170046B1 RID: 18097
		// (get) Token: 0x0600FC4A RID: 64586 RVA: 0x002DB669 File Offset: 0x002D9869
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ControlCloneQat.attributeNamespaceIds;
			}
		}

		// Token: 0x170046B2 RID: 18098
		// (get) Token: 0x0600FC4B RID: 64587 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FC4C RID: 64588 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170046B3 RID: 18099
		// (get) Token: 0x0600FC4D RID: 64589 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FC4E RID: 64590 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x170046B4 RID: 18100
		// (get) Token: 0x0600FC4F RID: 64591 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FC50 RID: 64592 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170046B5 RID: 18101
		// (get) Token: 0x0600FC51 RID: 64593 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FC52 RID: 64594 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170046B6 RID: 18102
		// (get) Token: 0x0600FC53 RID: 64595 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FC54 RID: 64596 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170046B7 RID: 18103
		// (get) Token: 0x0600FC55 RID: 64597 RVA: 0x002DB670 File Offset: 0x002D9870
		// (set) Token: 0x0600FC56 RID: 64598 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "size")]
		public EnumValue<SizeValues> Size
		{
			get
			{
				return (EnumValue<SizeValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170046B8 RID: 18104
		// (get) Token: 0x0600FC57 RID: 64599 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FC58 RID: 64600 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getSize")]
		public StringValue GetSize
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

		// Token: 0x170046B9 RID: 18105
		// (get) Token: 0x0600FC59 RID: 64601 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FC5A RID: 64602 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170046BA RID: 18106
		// (get) Token: 0x0600FC5B RID: 64603 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FC5C RID: 64604 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170046BB RID: 18107
		// (get) Token: 0x0600FC5D RID: 64605 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FC5E RID: 64606 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x170046BC RID: 18108
		// (get) Token: 0x0600FC5F RID: 64607 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FC60 RID: 64608 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170046BD RID: 18109
		// (get) Token: 0x0600FC61 RID: 64609 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FC62 RID: 64610 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170046BE RID: 18110
		// (get) Token: 0x0600FC63 RID: 64611 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FC64 RID: 64612 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170046BF RID: 18111
		// (get) Token: 0x0600FC65 RID: 64613 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FC66 RID: 64614 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170046C0 RID: 18112
		// (get) Token: 0x0600FC67 RID: 64615 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600FC68 RID: 64616 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x170046C1 RID: 18113
		// (get) Token: 0x0600FC69 RID: 64617 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FC6A RID: 64618 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170046C2 RID: 18114
		// (get) Token: 0x0600FC6B RID: 64619 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FC6C RID: 64620 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x170046C3 RID: 18115
		// (get) Token: 0x0600FC6D RID: 64621 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FC6E RID: 64622 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x170046C4 RID: 18116
		// (get) Token: 0x0600FC6F RID: 64623 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FC70 RID: 64624 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x170046C5 RID: 18117
		// (get) Token: 0x0600FC71 RID: 64625 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FC72 RID: 64626 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x170046C6 RID: 18118
		// (get) Token: 0x0600FC73 RID: 64627 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FC74 RID: 64628 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x170046C7 RID: 18119
		// (get) Token: 0x0600FC75 RID: 64629 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600FC76 RID: 64630 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x170046C8 RID: 18120
		// (get) Token: 0x0600FC77 RID: 64631 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600FC78 RID: 64632 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x170046C9 RID: 18121
		// (get) Token: 0x0600FC79 RID: 64633 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600FC7A RID: 64634 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x170046CA RID: 18122
		// (get) Token: 0x0600FC7B RID: 64635 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600FC7C RID: 64636 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x170046CB RID: 18123
		// (get) Token: 0x0600FC7D RID: 64637 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600FC7E RID: 64638 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x170046CC RID: 18124
		// (get) Token: 0x0600FC7F RID: 64639 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600FC80 RID: 64640 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x170046CD RID: 18125
		// (get) Token: 0x0600FC81 RID: 64641 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600FC82 RID: 64642 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x170046CE RID: 18126
		// (get) Token: 0x0600FC83 RID: 64643 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600FC84 RID: 64644 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x170046CF RID: 18127
		// (get) Token: 0x0600FC85 RID: 64645 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600FC86 RID: 64646 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x0600FC88 RID: 64648 RVA: 0x002DB680 File Offset: 0x002D9880
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "size" == name)
			{
				return new EnumValue<SizeValues>();
			}
			if (namespaceId == 0 && "getSize" == name)
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

		// Token: 0x0600FC89 RID: 64649 RVA: 0x002DB929 File Offset: 0x002D9B29
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ControlCloneQat>(deep);
		}

		// Token: 0x0600FC8A RID: 64650 RVA: 0x002DB934 File Offset: 0x002D9B34
		// Note: this type is marked as 'beforefieldinit'.
		static ControlCloneQat()
		{
			byte[] array = new byte[30];
			ControlCloneQat.attributeNamespaceIds = array;
		}

		// Token: 0x040071CB RID: 29131
		private const string tagName = "control";

		// Token: 0x040071CC RID: 29132
		private const byte tagNsId = 57;

		// Token: 0x040071CD RID: 29133
		internal const int ElementTypeIdConst = 13085;

		// Token: 0x040071CE RID: 29134
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "idMso", "description", "getDescription", "size", "getSize", "image", "imageMso", "getImage",
			"screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x040071CF RID: 29135
		private static byte[] attributeNamespaceIds;
	}
}
