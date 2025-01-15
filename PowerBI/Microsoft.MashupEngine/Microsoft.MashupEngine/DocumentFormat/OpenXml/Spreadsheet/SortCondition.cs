using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CDA RID: 11482
	[GeneratedCode("DomGen", "2.0")]
	internal class SortCondition : OpenXmlLeafElement
	{
		// Token: 0x170085D5 RID: 34261
		// (get) Token: 0x06018A5C RID: 100956 RVA: 0x002E6C17 File Offset: 0x002E4E17
		public override string LocalName
		{
			get
			{
				return "sortCondition";
			}
		}

		// Token: 0x170085D6 RID: 34262
		// (get) Token: 0x06018A5D RID: 100957 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085D7 RID: 34263
		// (get) Token: 0x06018A5E RID: 100958 RVA: 0x00343852 File Offset: 0x00341A52
		internal override int ElementTypeId
		{
			get
			{
				return 11464;
			}
		}

		// Token: 0x06018A5F RID: 100959 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170085D8 RID: 34264
		// (get) Token: 0x06018A60 RID: 100960 RVA: 0x00343859 File Offset: 0x00341A59
		internal override string[] AttributeTagNames
		{
			get
			{
				return SortCondition.attributeTagNames;
			}
		}

		// Token: 0x170085D9 RID: 34265
		// (get) Token: 0x06018A61 RID: 100961 RVA: 0x00343860 File Offset: 0x00341A60
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SortCondition.attributeNamespaceIds;
			}
		}

		// Token: 0x170085DA RID: 34266
		// (get) Token: 0x06018A62 RID: 100962 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018A63 RID: 100963 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "descending")]
		public BooleanValue Descending
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170085DB RID: 34267
		// (get) Token: 0x06018A64 RID: 100964 RVA: 0x002E6C33 File Offset: 0x002E4E33
		// (set) Token: 0x06018A65 RID: 100965 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sortBy")]
		public EnumValue<SortByValues> SortBy
		{
			get
			{
				return (EnumValue<SortByValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170085DC RID: 34268
		// (get) Token: 0x06018A66 RID: 100966 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06018A67 RID: 100967 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x170085DD RID: 34269
		// (get) Token: 0x06018A68 RID: 100968 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06018A69 RID: 100969 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "customList")]
		public StringValue CustomList
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

		// Token: 0x170085DE RID: 34270
		// (get) Token: 0x06018A6A RID: 100970 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06018A6B RID: 100971 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "dxfId")]
		public UInt32Value FormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170085DF RID: 34271
		// (get) Token: 0x06018A6C RID: 100972 RVA: 0x00343867 File Offset: 0x00341A67
		// (set) Token: 0x06018A6D RID: 100973 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "iconSet")]
		public EnumValue<IconSetValues> IconSet
		{
			get
			{
				return (EnumValue<IconSetValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170085E0 RID: 34272
		// (get) Token: 0x06018A6E RID: 100974 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06018A6F RID: 100975 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "iconId")]
		public UInt32Value IconId
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06018A71 RID: 100977 RVA: 0x00343878 File Offset: 0x00341A78
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "descending" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sortBy" == name)
			{
				return new EnumValue<SortByValues>();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "customList" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new EnumValue<IconSetValues>();
			}
			if (namespaceId == 0 && "iconId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018A72 RID: 100978 RVA: 0x00343927 File Offset: 0x00341B27
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SortCondition>(deep);
		}

		// Token: 0x06018A73 RID: 100979 RVA: 0x00343930 File Offset: 0x00341B30
		// Note: this type is marked as 'beforefieldinit'.
		static SortCondition()
		{
			byte[] array = new byte[7];
			SortCondition.attributeNamespaceIds = array;
		}

		// Token: 0x0400A123 RID: 41251
		private const string tagName = "sortCondition";

		// Token: 0x0400A124 RID: 41252
		private const byte tagNsId = 22;

		// Token: 0x0400A125 RID: 41253
		internal const int ElementTypeIdConst = 11464;

		// Token: 0x0400A126 RID: 41254
		private static string[] attributeTagNames = new string[] { "descending", "sortBy", "ref", "customList", "dxfId", "iconSet", "iconId" };

		// Token: 0x0400A127 RID: 41255
		private static byte[] attributeNamespaceIds;
	}
}
