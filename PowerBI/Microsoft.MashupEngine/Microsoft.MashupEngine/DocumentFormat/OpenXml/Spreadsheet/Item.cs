using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B74 RID: 11124
	[GeneratedCode("DomGen", "2.0")]
	internal class Item : OpenXmlLeafElement
	{
		// Token: 0x170079BF RID: 31167
		// (get) Token: 0x06016F40 RID: 94016 RVA: 0x002AD56D File Offset: 0x002AB76D
		public override string LocalName
		{
			get
			{
				return "item";
			}
		}

		// Token: 0x170079C0 RID: 31168
		// (get) Token: 0x06016F41 RID: 94017 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170079C1 RID: 31169
		// (get) Token: 0x06016F42 RID: 94018 RVA: 0x0033106B File Offset: 0x0032F26B
		internal override int ElementTypeId
		{
			get
			{
				return 11104;
			}
		}

		// Token: 0x06016F43 RID: 94019 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170079C2 RID: 31170
		// (get) Token: 0x06016F44 RID: 94020 RVA: 0x00331072 File Offset: 0x0032F272
		internal override string[] AttributeTagNames
		{
			get
			{
				return Item.attributeTagNames;
			}
		}

		// Token: 0x170079C3 RID: 31171
		// (get) Token: 0x06016F45 RID: 94021 RVA: 0x00331079 File Offset: 0x0032F279
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Item.attributeNamespaceIds;
			}
		}

		// Token: 0x170079C4 RID: 31172
		// (get) Token: 0x06016F46 RID: 94022 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016F47 RID: 94023 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "n")]
		public StringValue ItemName
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

		// Token: 0x170079C5 RID: 31173
		// (get) Token: 0x06016F48 RID: 94024 RVA: 0x00331080 File Offset: 0x0032F280
		// (set) Token: 0x06016F49 RID: 94025 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "t")]
		public EnumValue<ItemValues> ItemType
		{
			get
			{
				return (EnumValue<ItemValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170079C6 RID: 31174
		// (get) Token: 0x06016F4A RID: 94026 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016F4B RID: 94027 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "h")]
		public BooleanValue Hidden
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

		// Token: 0x170079C7 RID: 31175
		// (get) Token: 0x06016F4C RID: 94028 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016F4D RID: 94029 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "s")]
		public BooleanValue HasStringVlue
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170079C8 RID: 31176
		// (get) Token: 0x06016F4E RID: 94030 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016F4F RID: 94031 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "sd")]
		public BooleanValue HideDetails
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170079C9 RID: 31177
		// (get) Token: 0x06016F50 RID: 94032 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016F51 RID: 94033 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "f")]
		public BooleanValue Calculated
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

		// Token: 0x170079CA RID: 31178
		// (get) Token: 0x06016F52 RID: 94034 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06016F53 RID: 94035 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "m")]
		public BooleanValue Missing
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170079CB RID: 31179
		// (get) Token: 0x06016F54 RID: 94036 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06016F55 RID: 94037 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "c")]
		public BooleanValue ChildItems
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

		// Token: 0x170079CC RID: 31180
		// (get) Token: 0x06016F56 RID: 94038 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06016F57 RID: 94039 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "x")]
		public UInt32Value Index
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170079CD RID: 31181
		// (get) Token: 0x06016F58 RID: 94040 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06016F59 RID: 94041 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "d")]
		public BooleanValue Expanded
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

		// Token: 0x170079CE RID: 31182
		// (get) Token: 0x06016F5A RID: 94042 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016F5B RID: 94043 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "e")]
		public BooleanValue DrillAcrossAttributes
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

		// Token: 0x06016F5D RID: 94045 RVA: 0x00331090 File Offset: 0x0032F290
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<ItemValues>();
			}
			if (namespaceId == 0 && "h" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sd" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "f" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "m" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "c" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "x" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "d" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "e" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016F5E RID: 94046 RVA: 0x00331197 File Offset: 0x0032F397
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Item>(deep);
		}

		// Token: 0x06016F5F RID: 94047 RVA: 0x003311A0 File Offset: 0x0032F3A0
		// Note: this type is marked as 'beforefieldinit'.
		static Item()
		{
			byte[] array = new byte[11];
			Item.attributeNamespaceIds = array;
		}

		// Token: 0x04009A82 RID: 39554
		private const string tagName = "item";

		// Token: 0x04009A83 RID: 39555
		private const byte tagNsId = 22;

		// Token: 0x04009A84 RID: 39556
		internal const int ElementTypeIdConst = 11104;

		// Token: 0x04009A85 RID: 39557
		private static string[] attributeTagNames = new string[]
		{
			"n", "t", "h", "s", "sd", "f", "m", "c", "x", "d",
			"e"
		};

		// Token: 0x04009A86 RID: 39558
		private static byte[] attributeNamespaceIds;
	}
}
