using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B46 RID: 11078
	[ChildElementInfo(typeof(DatabaseProperties))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(Parameters))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OlapProperties))]
	[ChildElementInfo(typeof(WebQueryProperties))]
	[ChildElementInfo(typeof(ConnectionExtensionList))]
	internal class Connection : OpenXmlCompositeElement
	{
		// Token: 0x170077D4 RID: 30676
		// (get) Token: 0x06016B0C RID: 92940 RVA: 0x002E658F File Offset: 0x002E478F
		public override string LocalName
		{
			get
			{
				return "connection";
			}
		}

		// Token: 0x170077D5 RID: 30677
		// (get) Token: 0x06016B0D RID: 92941 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077D6 RID: 30678
		// (get) Token: 0x06016B0E RID: 92942 RVA: 0x0032DE6B File Offset: 0x0032C06B
		internal override int ElementTypeId
		{
			get
			{
				return 11061;
			}
		}

		// Token: 0x06016B0F RID: 92943 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170077D7 RID: 30679
		// (get) Token: 0x06016B10 RID: 92944 RVA: 0x0032DE72 File Offset: 0x0032C072
		internal override string[] AttributeTagNames
		{
			get
			{
				return Connection.attributeTagNames;
			}
		}

		// Token: 0x170077D8 RID: 30680
		// (get) Token: 0x06016B11 RID: 92945 RVA: 0x0032DE79 File Offset: 0x0032C079
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Connection.attributeNamespaceIds;
			}
		}

		// Token: 0x170077D9 RID: 30681
		// (get) Token: 0x06016B12 RID: 92946 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016B13 RID: 92947 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170077DA RID: 30682
		// (get) Token: 0x06016B14 RID: 92948 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016B15 RID: 92949 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sourceFile")]
		public StringValue SourceFile
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

		// Token: 0x170077DB RID: 30683
		// (get) Token: 0x06016B16 RID: 92950 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016B17 RID: 92951 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "odcFile")]
		public StringValue ConnectionFile
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

		// Token: 0x170077DC RID: 30684
		// (get) Token: 0x06016B18 RID: 92952 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016B19 RID: 92953 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "keepAlive")]
		public BooleanValue KeepAlive
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

		// Token: 0x170077DD RID: 30685
		// (get) Token: 0x06016B1A RID: 92954 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016B1B RID: 92955 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "interval")]
		public UInt32Value Interval
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

		// Token: 0x170077DE RID: 30686
		// (get) Token: 0x06016B1C RID: 92956 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06016B1D RID: 92957 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x170077DF RID: 30687
		// (get) Token: 0x06016B1E RID: 92958 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06016B1F RID: 92959 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x170077E0 RID: 30688
		// (get) Token: 0x06016B20 RID: 92960 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x06016B21 RID: 92961 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "type")]
		public UInt32Value Type
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170077E1 RID: 30689
		// (get) Token: 0x06016B22 RID: 92962 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06016B23 RID: 92963 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "reconnectionMethod")]
		public UInt32Value ReconnectionMethod
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

		// Token: 0x170077E2 RID: 30690
		// (get) Token: 0x06016B24 RID: 92964 RVA: 0x0032DE80 File Offset: 0x0032C080
		// (set) Token: 0x06016B25 RID: 92965 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "refreshedVersion")]
		public ByteValue RefreshedVersion
		{
			get
			{
				return (ByteValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170077E3 RID: 30691
		// (get) Token: 0x06016B26 RID: 92966 RVA: 0x0032DE90 File Offset: 0x0032C090
		// (set) Token: 0x06016B27 RID: 92967 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "minRefreshableVersion")]
		public ByteValue MinRefreshableVersion
		{
			get
			{
				return (ByteValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170077E4 RID: 30692
		// (get) Token: 0x06016B28 RID: 92968 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06016B29 RID: 92969 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "savePassword")]
		public BooleanValue SavePassword
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170077E5 RID: 30693
		// (get) Token: 0x06016B2A RID: 92970 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06016B2B RID: 92971 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "new")]
		public BooleanValue New
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

		// Token: 0x170077E6 RID: 30694
		// (get) Token: 0x06016B2C RID: 92972 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06016B2D RID: 92973 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "deleted")]
		public BooleanValue Deleted
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170077E7 RID: 30695
		// (get) Token: 0x06016B2E RID: 92974 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06016B2F RID: 92975 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "onlyUseConnectionFile")]
		public BooleanValue OnlyUseConnectionFile
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

		// Token: 0x170077E8 RID: 30696
		// (get) Token: 0x06016B30 RID: 92976 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06016B31 RID: 92977 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "background")]
		public BooleanValue Background
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170077E9 RID: 30697
		// (get) Token: 0x06016B32 RID: 92978 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06016B33 RID: 92979 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "refreshOnLoad")]
		public BooleanValue RefreshOnLoad
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170077EA RID: 30698
		// (get) Token: 0x06016B34 RID: 92980 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x06016B35 RID: 92981 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "saveData")]
		public BooleanValue SaveData
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170077EB RID: 30699
		// (get) Token: 0x06016B36 RID: 92982 RVA: 0x0032DEA0 File Offset: 0x0032C0A0
		// (set) Token: 0x06016B37 RID: 92983 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "credentials")]
		public EnumValue<CredentialsMethodValues> Credentials
		{
			get
			{
				return (EnumValue<CredentialsMethodValues>)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170077EC RID: 30700
		// (get) Token: 0x06016B38 RID: 92984 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x06016B39 RID: 92985 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "singleSignOnId")]
		public StringValue SingleSignOnId
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

		// Token: 0x06016B3A RID: 92986 RVA: 0x00293ECF File Offset: 0x002920CF
		public Connection()
		{
		}

		// Token: 0x06016B3B RID: 92987 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Connection(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016B3C RID: 92988 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Connection(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016B3D RID: 92989 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Connection(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016B3E RID: 92990 RVA: 0x0032DEB0 File Offset: 0x0032C0B0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dbPr" == name)
			{
				return new DatabaseProperties();
			}
			if (22 == namespaceId && "olapPr" == name)
			{
				return new OlapProperties();
			}
			if (22 == namespaceId && "webPr" == name)
			{
				return new WebQueryProperties();
			}
			if (22 == namespaceId && "textPr" == name)
			{
				return new TextProperties();
			}
			if (22 == namespaceId && "parameters" == name)
			{
				return new Parameters();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ConnectionExtensionList();
			}
			return null;
		}

		// Token: 0x170077ED RID: 30701
		// (get) Token: 0x06016B3F RID: 92991 RVA: 0x0032DF4E File Offset: 0x0032C14E
		internal override string[] ElementTagNames
		{
			get
			{
				return Connection.eleTagNames;
			}
		}

		// Token: 0x170077EE RID: 30702
		// (get) Token: 0x06016B40 RID: 92992 RVA: 0x0032DF55 File Offset: 0x0032C155
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Connection.eleNamespaceIds;
			}
		}

		// Token: 0x170077EF RID: 30703
		// (get) Token: 0x06016B41 RID: 92993 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170077F0 RID: 30704
		// (get) Token: 0x06016B42 RID: 92994 RVA: 0x0032DF5C File Offset: 0x0032C15C
		// (set) Token: 0x06016B43 RID: 92995 RVA: 0x0032DF65 File Offset: 0x0032C165
		public DatabaseProperties DatabaseProperties
		{
			get
			{
				return base.GetElement<DatabaseProperties>(0);
			}
			set
			{
				base.SetElement<DatabaseProperties>(0, value);
			}
		}

		// Token: 0x170077F1 RID: 30705
		// (get) Token: 0x06016B44 RID: 92996 RVA: 0x0032DF6F File Offset: 0x0032C16F
		// (set) Token: 0x06016B45 RID: 92997 RVA: 0x0032DF78 File Offset: 0x0032C178
		public OlapProperties OlapProperties
		{
			get
			{
				return base.GetElement<OlapProperties>(1);
			}
			set
			{
				base.SetElement<OlapProperties>(1, value);
			}
		}

		// Token: 0x170077F2 RID: 30706
		// (get) Token: 0x06016B46 RID: 92998 RVA: 0x0032DF82 File Offset: 0x0032C182
		// (set) Token: 0x06016B47 RID: 92999 RVA: 0x0032DF8B File Offset: 0x0032C18B
		public WebQueryProperties WebQueryProperties
		{
			get
			{
				return base.GetElement<WebQueryProperties>(2);
			}
			set
			{
				base.SetElement<WebQueryProperties>(2, value);
			}
		}

		// Token: 0x170077F3 RID: 30707
		// (get) Token: 0x06016B48 RID: 93000 RVA: 0x0032DF95 File Offset: 0x0032C195
		// (set) Token: 0x06016B49 RID: 93001 RVA: 0x0032DF9E File Offset: 0x0032C19E
		public TextProperties TextProperties
		{
			get
			{
				return base.GetElement<TextProperties>(3);
			}
			set
			{
				base.SetElement<TextProperties>(3, value);
			}
		}

		// Token: 0x170077F4 RID: 30708
		// (get) Token: 0x06016B4A RID: 93002 RVA: 0x0032DFA8 File Offset: 0x0032C1A8
		// (set) Token: 0x06016B4B RID: 93003 RVA: 0x0032DFB1 File Offset: 0x0032C1B1
		public Parameters Parameters
		{
			get
			{
				return base.GetElement<Parameters>(4);
			}
			set
			{
				base.SetElement<Parameters>(4, value);
			}
		}

		// Token: 0x170077F5 RID: 30709
		// (get) Token: 0x06016B4C RID: 93004 RVA: 0x0032DFBB File Offset: 0x0032C1BB
		// (set) Token: 0x06016B4D RID: 93005 RVA: 0x0032DFC4 File Offset: 0x0032C1C4
		public ConnectionExtensionList ConnectionExtensionList
		{
			get
			{
				return base.GetElement<ConnectionExtensionList>(5);
			}
			set
			{
				base.SetElement<ConnectionExtensionList>(5, value);
			}
		}

		// Token: 0x06016B4E RID: 93006 RVA: 0x0032DFD0 File Offset: 0x0032C1D0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sourceFile" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "odcFile" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keepAlive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "interval" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "reconnectionMethod" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "refreshedVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "minRefreshableVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "savePassword" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "new" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "deleted" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "onlyUseConnectionFile" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "background" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "refreshOnLoad" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "saveData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "credentials" == name)
			{
				return new EnumValue<CredentialsMethodValues>();
			}
			if (namespaceId == 0 && "singleSignOnId" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016B4F RID: 93007 RVA: 0x0032E19D File Offset: 0x0032C39D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Connection>(deep);
		}

		// Token: 0x06016B50 RID: 93008 RVA: 0x0032E1A8 File Offset: 0x0032C3A8
		// Note: this type is marked as 'beforefieldinit'.
		static Connection()
		{
			byte[] array = new byte[20];
			Connection.attributeNamespaceIds = array;
			Connection.eleTagNames = new string[] { "dbPr", "olapPr", "webPr", "textPr", "parameters", "extLst" };
			Connection.eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22, 22 };
		}

		// Token: 0x0400999A RID: 39322
		private const string tagName = "connection";

		// Token: 0x0400999B RID: 39323
		private const byte tagNsId = 22;

		// Token: 0x0400999C RID: 39324
		internal const int ElementTypeIdConst = 11061;

		// Token: 0x0400999D RID: 39325
		private static string[] attributeTagNames = new string[]
		{
			"id", "sourceFile", "odcFile", "keepAlive", "interval", "name", "description", "type", "reconnectionMethod", "refreshedVersion",
			"minRefreshableVersion", "savePassword", "new", "deleted", "onlyUseConnectionFile", "background", "refreshOnLoad", "saveData", "credentials", "singleSignOnId"
		};

		// Token: 0x0400999E RID: 39326
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400999F RID: 39327
		private static readonly string[] eleTagNames;

		// Token: 0x040099A0 RID: 39328
		private static readonly byte[] eleNamespaceIds;
	}
}
