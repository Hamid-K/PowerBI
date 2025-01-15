using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022BC RID: 8892
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CheckBox : OpenXmlLeafElement
	{
		// Token: 0x170041CB RID: 16843
		// (get) Token: 0x0600F223 RID: 61987 RVA: 0x002C8F4A File Offset: 0x002C714A
		public override string LocalName
		{
			get
			{
				return "checkBox";
			}
		}

		// Token: 0x170041CC RID: 16844
		// (get) Token: 0x0600F224 RID: 61988 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170041CD RID: 16845
		// (get) Token: 0x0600F225 RID: 61989 RVA: 0x002D1EEE File Offset: 0x002D00EE
		internal override int ElementTypeId
		{
			get
			{
				return 13037;
			}
		}

		// Token: 0x0600F226 RID: 61990 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170041CE RID: 16846
		// (get) Token: 0x0600F227 RID: 61991 RVA: 0x002D1EF5 File Offset: 0x002D00F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return CheckBox.attributeTagNames;
			}
		}

		// Token: 0x170041CF RID: 16847
		// (get) Token: 0x0600F228 RID: 61992 RVA: 0x002D1EFC File Offset: 0x002D00FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CheckBox.attributeNamespaceIds;
			}
		}

		// Token: 0x170041D0 RID: 16848
		// (get) Token: 0x0600F229 RID: 61993 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F22A RID: 61994 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170041D1 RID: 16849
		// (get) Token: 0x0600F22B RID: 61995 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F22C RID: 61996 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170041D2 RID: 16850
		// (get) Token: 0x0600F22D RID: 61997 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600F22E RID: 61998 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170041D3 RID: 16851
		// (get) Token: 0x0600F22F RID: 61999 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F230 RID: 62000 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170041D4 RID: 16852
		// (get) Token: 0x0600F231 RID: 62001 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F232 RID: 62002 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170041D5 RID: 16853
		// (get) Token: 0x0600F233 RID: 62003 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F234 RID: 62004 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170041D6 RID: 16854
		// (get) Token: 0x0600F235 RID: 62005 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F236 RID: 62006 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x170041D7 RID: 16855
		// (get) Token: 0x0600F237 RID: 62007 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F238 RID: 62008 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170041D8 RID: 16856
		// (get) Token: 0x0600F239 RID: 62009 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F23A RID: 62010 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170041D9 RID: 16857
		// (get) Token: 0x0600F23B RID: 62011 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F23C RID: 62012 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170041DA RID: 16858
		// (get) Token: 0x0600F23D RID: 62013 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F23E RID: 62014 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170041DB RID: 16859
		// (get) Token: 0x0600F23F RID: 62015 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F240 RID: 62016 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170041DC RID: 16860
		// (get) Token: 0x0600F241 RID: 62017 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F242 RID: 62018 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170041DD RID: 16861
		// (get) Token: 0x0600F243 RID: 62019 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F244 RID: 62020 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170041DE RID: 16862
		// (get) Token: 0x0600F245 RID: 62021 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F246 RID: 62022 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x170041DF RID: 16863
		// (get) Token: 0x0600F247 RID: 62023 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F248 RID: 62024 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x170041E0 RID: 16864
		// (get) Token: 0x0600F249 RID: 62025 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F24A RID: 62026 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x170041E1 RID: 16865
		// (get) Token: 0x0600F24B RID: 62027 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F24C RID: 62028 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x170041E2 RID: 16866
		// (get) Token: 0x0600F24D RID: 62029 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F24E RID: 62030 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170041E3 RID: 16867
		// (get) Token: 0x0600F24F RID: 62031 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F250 RID: 62032 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170041E4 RID: 16868
		// (get) Token: 0x0600F251 RID: 62033 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x0600F252 RID: 62034 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x170041E5 RID: 16869
		// (get) Token: 0x0600F253 RID: 62035 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F254 RID: 62036 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x170041E6 RID: 16870
		// (get) Token: 0x0600F255 RID: 62037 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F256 RID: 62038 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x170041E7 RID: 16871
		// (get) Token: 0x0600F257 RID: 62039 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F258 RID: 62040 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x0600F25A RID: 62042 RVA: 0x002D1F04 File Offset: 0x002D0104
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

		// Token: 0x0600F25B RID: 62043 RVA: 0x002D2129 File Offset: 0x002D0329
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CheckBox>(deep);
		}

		// Token: 0x0600F25C RID: 62044 RVA: 0x002D2134 File Offset: 0x002D0334
		// Note: this type is marked as 'beforefieldinit'.
		static CheckBox()
		{
			byte[] array = new byte[24];
			CheckBox.attributeNamespaceIds = array;
		}

		// Token: 0x040070D9 RID: 28889
		private const string tagName = "checkBox";

		// Token: 0x040070DA RID: 28890
		private const byte tagNsId = 57;

		// Token: 0x040070DB RID: 28891
		internal const int ElementTypeIdConst = 13037;

		// Token: 0x040070DC RID: 28892
		private static string[] attributeTagNames = new string[]
		{
			"getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "id", "idQ", "tag", "idMso",
			"screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip"
		};

		// Token: 0x040070DD RID: 28893
		private static byte[] attributeNamespaceIds;
	}
}
