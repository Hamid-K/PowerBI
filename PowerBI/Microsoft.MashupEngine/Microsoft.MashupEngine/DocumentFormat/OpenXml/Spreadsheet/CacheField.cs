using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B4E RID: 11086
	[ChildElementInfo(typeof(CacheFieldExtensionList))]
	[ChildElementInfo(typeof(SharedItems))]
	[ChildElementInfo(typeof(FieldGroup))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MemberPropertiesMap))]
	internal class CacheField : OpenXmlCompositeElement
	{
		// Token: 0x17007827 RID: 30759
		// (get) Token: 0x06016BBB RID: 93115 RVA: 0x002E69E7 File Offset: 0x002E4BE7
		public override string LocalName
		{
			get
			{
				return "cacheField";
			}
		}

		// Token: 0x17007828 RID: 30760
		// (get) Token: 0x06016BBC RID: 93116 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007829 RID: 30761
		// (get) Token: 0x06016BBD RID: 93117 RVA: 0x0032E727 File Offset: 0x0032C927
		internal override int ElementTypeId
		{
			get
			{
				return 11069;
			}
		}

		// Token: 0x06016BBE RID: 93118 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700782A RID: 30762
		// (get) Token: 0x06016BBF RID: 93119 RVA: 0x0032E72E File Offset: 0x0032C92E
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheField.attributeTagNames;
			}
		}

		// Token: 0x1700782B RID: 30763
		// (get) Token: 0x06016BC0 RID: 93120 RVA: 0x0032E735 File Offset: 0x0032C935
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheField.attributeNamespaceIds;
			}
		}

		// Token: 0x1700782C RID: 30764
		// (get) Token: 0x06016BC1 RID: 93121 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016BC2 RID: 93122 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x1700782D RID: 30765
		// (get) Token: 0x06016BC3 RID: 93123 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016BC4 RID: 93124 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "caption")]
		public StringValue Caption
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

		// Token: 0x1700782E RID: 30766
		// (get) Token: 0x06016BC5 RID: 93125 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016BC6 RID: 93126 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "propertyName")]
		public StringValue PropertyName
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

		// Token: 0x1700782F RID: 30767
		// (get) Token: 0x06016BC7 RID: 93127 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016BC8 RID: 93128 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "serverField")]
		public BooleanValue ServerField
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

		// Token: 0x17007830 RID: 30768
		// (get) Token: 0x06016BC9 RID: 93129 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016BCA RID: 93130 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "uniqueList")]
		public BooleanValue UniqueList
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

		// Token: 0x17007831 RID: 30769
		// (get) Token: 0x06016BCB RID: 93131 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06016BCC RID: 93132 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "numFmtId")]
		public UInt32Value NumberFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007832 RID: 30770
		// (get) Token: 0x06016BCD RID: 93133 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06016BCE RID: 93134 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "formula")]
		public StringValue Formula
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

		// Token: 0x17007833 RID: 30771
		// (get) Token: 0x06016BCF RID: 93135 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x06016BD0 RID: 93136 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "sqlType")]
		public Int32Value SqlType
		{
			get
			{
				return (Int32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007834 RID: 30772
		// (get) Token: 0x06016BD1 RID: 93137 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x06016BD2 RID: 93138 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "hierarchy")]
		public Int32Value Hierarchy
		{
			get
			{
				return (Int32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007835 RID: 30773
		// (get) Token: 0x06016BD3 RID: 93139 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x06016BD4 RID: 93140 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "level")]
		public UInt32Value Level
		{
			get
			{
				return (UInt32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007836 RID: 30774
		// (get) Token: 0x06016BD5 RID: 93141 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016BD6 RID: 93142 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "databaseField")]
		public BooleanValue DatabaseField
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

		// Token: 0x17007837 RID: 30775
		// (get) Token: 0x06016BD7 RID: 93143 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x06016BD8 RID: 93144 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "mappingCount")]
		public UInt32Value MappingCount
		{
			get
			{
				return (UInt32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007838 RID: 30776
		// (get) Token: 0x06016BD9 RID: 93145 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06016BDA RID: 93146 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "memberPropertyField")]
		public BooleanValue MemberPropertyField
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x06016BDB RID: 93147 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheField()
		{
		}

		// Token: 0x06016BDC RID: 93148 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheField(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016BDD RID: 93149 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheField(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016BDE RID: 93150 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheField(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016BDF RID: 93151 RVA: 0x0032E73C File Offset: 0x0032C93C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sharedItems" == name)
			{
				return new SharedItems();
			}
			if (22 == namespaceId && "fieldGroup" == name)
			{
				return new FieldGroup();
			}
			if (22 == namespaceId && "mpMap" == name)
			{
				return new MemberPropertiesMap();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new CacheFieldExtensionList();
			}
			return null;
		}

		// Token: 0x17007839 RID: 30777
		// (get) Token: 0x06016BE0 RID: 93152 RVA: 0x0032E7AA File Offset: 0x0032C9AA
		internal override string[] ElementTagNames
		{
			get
			{
				return CacheField.eleTagNames;
			}
		}

		// Token: 0x1700783A RID: 30778
		// (get) Token: 0x06016BE1 RID: 93153 RVA: 0x0032E7B1 File Offset: 0x0032C9B1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CacheField.eleNamespaceIds;
			}
		}

		// Token: 0x1700783B RID: 30779
		// (get) Token: 0x06016BE2 RID: 93154 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700783C RID: 30780
		// (get) Token: 0x06016BE3 RID: 93155 RVA: 0x0032E7B8 File Offset: 0x0032C9B8
		// (set) Token: 0x06016BE4 RID: 93156 RVA: 0x0032E7C1 File Offset: 0x0032C9C1
		public SharedItems SharedItems
		{
			get
			{
				return base.GetElement<SharedItems>(0);
			}
			set
			{
				base.SetElement<SharedItems>(0, value);
			}
		}

		// Token: 0x1700783D RID: 30781
		// (get) Token: 0x06016BE5 RID: 93157 RVA: 0x0032E7CB File Offset: 0x0032C9CB
		// (set) Token: 0x06016BE6 RID: 93158 RVA: 0x0032E7D4 File Offset: 0x0032C9D4
		public FieldGroup FieldGroup
		{
			get
			{
				return base.GetElement<FieldGroup>(1);
			}
			set
			{
				base.SetElement<FieldGroup>(1, value);
			}
		}

		// Token: 0x06016BE7 RID: 93159 RVA: 0x0032E7E0 File Offset: 0x0032C9E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "propertyName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "serverField" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "uniqueList" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "numFmtId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "formula" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sqlType" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "hierarchy" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "level" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "databaseField" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "mappingCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "memberPropertyField" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016BE8 RID: 93160 RVA: 0x0032E913 File Offset: 0x0032CB13
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheField>(deep);
		}

		// Token: 0x06016BE9 RID: 93161 RVA: 0x0032E91C File Offset: 0x0032CB1C
		// Note: this type is marked as 'beforefieldinit'.
		static CacheField()
		{
			byte[] array = new byte[13];
			CacheField.attributeNamespaceIds = array;
			CacheField.eleTagNames = new string[] { "sharedItems", "fieldGroup", "mpMap", "extLst" };
			CacheField.eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
		}

		// Token: 0x040099C2 RID: 39362
		private const string tagName = "cacheField";

		// Token: 0x040099C3 RID: 39363
		private const byte tagNsId = 22;

		// Token: 0x040099C4 RID: 39364
		internal const int ElementTypeIdConst = 11069;

		// Token: 0x040099C5 RID: 39365
		private static string[] attributeTagNames = new string[]
		{
			"name", "caption", "propertyName", "serverField", "uniqueList", "numFmtId", "formula", "sqlType", "hierarchy", "level",
			"databaseField", "mappingCount", "memberPropertyField"
		};

		// Token: 0x040099C6 RID: 39366
		private static byte[] attributeNamespaceIds;

		// Token: 0x040099C7 RID: 39367
		private static readonly string[] eleTagNames;

		// Token: 0x040099C8 RID: 39368
		private static readonly byte[] eleNamespaceIds;
	}
}
