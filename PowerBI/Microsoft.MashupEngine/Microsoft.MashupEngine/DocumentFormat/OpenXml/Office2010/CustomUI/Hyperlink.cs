using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022DD RID: 8925
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Hyperlink : OpenXmlLeafElement
	{
		// Token: 0x17004596 RID: 17814
		// (get) Token: 0x0600F9F9 RID: 63993 RVA: 0x002D9347 File Offset: 0x002D7547
		public override string LocalName
		{
			get
			{
				return "hyperlink";
			}
		}

		// Token: 0x17004597 RID: 17815
		// (get) Token: 0x0600F9FA RID: 63994 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004598 RID: 17816
		// (get) Token: 0x0600F9FB RID: 63995 RVA: 0x002D934E File Offset: 0x002D754E
		internal override int ElementTypeId
		{
			get
			{
				return 13070;
			}
		}

		// Token: 0x0600F9FC RID: 63996 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004599 RID: 17817
		// (get) Token: 0x0600F9FD RID: 63997 RVA: 0x002D9355 File Offset: 0x002D7555
		internal override string[] AttributeTagNames
		{
			get
			{
				return Hyperlink.attributeTagNames;
			}
		}

		// Token: 0x1700459A RID: 17818
		// (get) Token: 0x0600F9FE RID: 63998 RVA: 0x002D935C File Offset: 0x002D755C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Hyperlink.attributeNamespaceIds;
			}
		}

		// Token: 0x1700459B RID: 17819
		// (get) Token: 0x0600F9FF RID: 63999 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FA00 RID: 64000 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700459C RID: 17820
		// (get) Token: 0x0600FA01 RID: 64001 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FA02 RID: 64002 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700459D RID: 17821
		// (get) Token: 0x0600FA03 RID: 64003 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FA04 RID: 64004 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700459E RID: 17822
		// (get) Token: 0x0600FA05 RID: 64005 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600FA06 RID: 64006 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "alignLabel")]
		public EnumValue<ExpandValues> AlignLabel
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700459F RID: 17823
		// (get) Token: 0x0600FA07 RID: 64007 RVA: 0x002D8858 File Offset: 0x002D6A58
		// (set) Token: 0x0600FA08 RID: 64008 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170045A0 RID: 17824
		// (get) Token: 0x0600FA09 RID: 64009 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600FA0A RID: 64010 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170045A1 RID: 17825
		// (get) Token: 0x0600FA0B RID: 64011 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FA0C RID: 64012 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170045A2 RID: 17826
		// (get) Token: 0x0600FA0D RID: 64013 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0600FA0E RID: 64014 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170045A3 RID: 17827
		// (get) Token: 0x0600FA0F RID: 64015 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FA10 RID: 64016 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170045A4 RID: 17828
		// (get) Token: 0x0600FA11 RID: 64017 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FA12 RID: 64018 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x170045A5 RID: 17829
		// (get) Token: 0x0600FA13 RID: 64019 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FA14 RID: 64020 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x170045A6 RID: 17830
		// (get) Token: 0x0600FA15 RID: 64021 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FA16 RID: 64022 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170045A7 RID: 17831
		// (get) Token: 0x0600FA17 RID: 64023 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FA18 RID: 64024 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170045A8 RID: 17832
		// (get) Token: 0x0600FA19 RID: 64025 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FA1A RID: 64026 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x170045A9 RID: 17833
		// (get) Token: 0x0600FA1B RID: 64027 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FA1C RID: 64028 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170045AA RID: 17834
		// (get) Token: 0x0600FA1D RID: 64029 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FA1E RID: 64030 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170045AB RID: 17835
		// (get) Token: 0x0600FA1F RID: 64031 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FA20 RID: 64032 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x170045AC RID: 17836
		// (get) Token: 0x0600FA21 RID: 64033 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FA22 RID: 64034 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x170045AD RID: 17837
		// (get) Token: 0x0600FA23 RID: 64035 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FA24 RID: 64036 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x170045AE RID: 17838
		// (get) Token: 0x0600FA25 RID: 64037 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FA26 RID: 64038 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x170045AF RID: 17839
		// (get) Token: 0x0600FA27 RID: 64039 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FA28 RID: 64040 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x170045B0 RID: 17840
		// (get) Token: 0x0600FA29 RID: 64041 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600FA2A RID: 64042 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "target")]
		public StringValue Target
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

		// Token: 0x170045B1 RID: 17841
		// (get) Token: 0x0600FA2B RID: 64043 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600FA2C RID: 64044 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getTarget")]
		public StringValue GetTarget
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

		// Token: 0x0600FA2E RID: 64046 RVA: 0x002D9364 File Offset: 0x002D7564
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
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "alignLabel" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
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
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onAction" == name)
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
			if (namespaceId == 0 && "target" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getTarget" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FA2F RID: 64047 RVA: 0x002D9573 File Offset: 0x002D7773
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hyperlink>(deep);
		}

		// Token: 0x0600FA30 RID: 64048 RVA: 0x002D957C File Offset: 0x002D777C
		// Note: this type is marked as 'beforefieldinit'.
		static Hyperlink()
		{
			byte[] array = new byte[23];
			Hyperlink.attributeNamespaceIds = array;
		}

		// Token: 0x0400717E RID: 29054
		private const string tagName = "hyperlink";

		// Token: 0x0400717F RID: 29055
		private const byte tagNsId = 57;

		// Token: 0x04007180 RID: 29056
		internal const int ElementTypeIdConst = 13070;

		// Token: 0x04007181 RID: 29057
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "alignLabel", "expand", "enabled", "getEnabled", "visible", "getVisible", "keytip",
			"getKeytip", "label", "getLabel", "onAction", "image", "imageMso", "getImage", "screentip", "getScreentip", "supertip",
			"getSupertip", "target", "getTarget"
		};

		// Token: 0x04007182 RID: 29058
		private static byte[] attributeNamespaceIds;
	}
}
