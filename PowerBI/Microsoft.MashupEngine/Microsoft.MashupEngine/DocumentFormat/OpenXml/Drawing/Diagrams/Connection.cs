using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200265F RID: 9823
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Connection : OpenXmlCompositeElement
	{
		// Token: 0x17005B9C RID: 23452
		// (get) Token: 0x06012AF0 RID: 76528 RVA: 0x002FDFE6 File Offset: 0x002FC1E6
		public override string LocalName
		{
			get
			{
				return "cxn";
			}
		}

		// Token: 0x17005B9D RID: 23453
		// (get) Token: 0x06012AF1 RID: 76529 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B9E RID: 23454
		// (get) Token: 0x06012AF2 RID: 76530 RVA: 0x002FDFED File Offset: 0x002FC1ED
		internal override int ElementTypeId
		{
			get
			{
				return 10640;
			}
		}

		// Token: 0x06012AF3 RID: 76531 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B9F RID: 23455
		// (get) Token: 0x06012AF4 RID: 76532 RVA: 0x002FDFF4 File Offset: 0x002FC1F4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Connection.attributeTagNames;
			}
		}

		// Token: 0x17005BA0 RID: 23456
		// (get) Token: 0x06012AF5 RID: 76533 RVA: 0x002FDFFB File Offset: 0x002FC1FB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Connection.attributeNamespaceIds;
			}
		}

		// Token: 0x17005BA1 RID: 23457
		// (get) Token: 0x06012AF6 RID: 76534 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012AF7 RID: 76535 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "modelId")]
		public StringValue ModelId
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

		// Token: 0x17005BA2 RID: 23458
		// (get) Token: 0x06012AF8 RID: 76536 RVA: 0x002FE002 File Offset: 0x002FC202
		// (set) Token: 0x06012AF9 RID: 76537 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<ConnectionValues> Type
		{
			get
			{
				return (EnumValue<ConnectionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005BA3 RID: 23459
		// (get) Token: 0x06012AFA RID: 76538 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06012AFB RID: 76539 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "srcId")]
		public StringValue SourceId
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

		// Token: 0x17005BA4 RID: 23460
		// (get) Token: 0x06012AFC RID: 76540 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06012AFD RID: 76541 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "destId")]
		public StringValue DestinationId
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

		// Token: 0x17005BA5 RID: 23461
		// (get) Token: 0x06012AFE RID: 76542 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06012AFF RID: 76543 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "srcOrd")]
		public UInt32Value SourcePosition
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

		// Token: 0x17005BA6 RID: 23462
		// (get) Token: 0x06012B00 RID: 76544 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06012B01 RID: 76545 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "destOrd")]
		public UInt32Value DestinationPosition
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

		// Token: 0x17005BA7 RID: 23463
		// (get) Token: 0x06012B02 RID: 76546 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06012B03 RID: 76547 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "parTransId")]
		public StringValue ParentTransitionId
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

		// Token: 0x17005BA8 RID: 23464
		// (get) Token: 0x06012B04 RID: 76548 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06012B05 RID: 76549 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "sibTransId")]
		public StringValue SiblingTransitionId
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

		// Token: 0x17005BA9 RID: 23465
		// (get) Token: 0x06012B06 RID: 76550 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06012B07 RID: 76551 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "presId")]
		public StringValue PresentationId
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

		// Token: 0x06012B08 RID: 76552 RVA: 0x00293ECF File Offset: 0x002920CF
		public Connection()
		{
		}

		// Token: 0x06012B09 RID: 76553 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Connection(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B0A RID: 76554 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Connection(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B0B RID: 76555 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Connection(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012B0C RID: 76556 RVA: 0x002FE011 File Offset: 0x002FC211
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005BAA RID: 23466
		// (get) Token: 0x06012B0D RID: 76557 RVA: 0x002FE02C File Offset: 0x002FC22C
		internal override string[] ElementTagNames
		{
			get
			{
				return Connection.eleTagNames;
			}
		}

		// Token: 0x17005BAB RID: 23467
		// (get) Token: 0x06012B0E RID: 76558 RVA: 0x002FE033 File Offset: 0x002FC233
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Connection.eleNamespaceIds;
			}
		}

		// Token: 0x17005BAC RID: 23468
		// (get) Token: 0x06012B0F RID: 76559 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005BAD RID: 23469
		// (get) Token: 0x06012B10 RID: 76560 RVA: 0x002FE03A File Offset: 0x002FC23A
		// (set) Token: 0x06012B11 RID: 76561 RVA: 0x002FE043 File Offset: 0x002FC243
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06012B12 RID: 76562 RVA: 0x002FE050 File Offset: 0x002FC250
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "modelId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ConnectionValues>();
			}
			if (namespaceId == 0 && "srcId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "destId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "srcOrd" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "destOrd" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "parTransId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sibTransId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "presId" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012B13 RID: 76563 RVA: 0x002FE12B File Offset: 0x002FC32B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Connection>(deep);
		}

		// Token: 0x06012B14 RID: 76564 RVA: 0x002FE134 File Offset: 0x002FC334
		// Note: this type is marked as 'beforefieldinit'.
		static Connection()
		{
			byte[] array = new byte[9];
			Connection.attributeNamespaceIds = array;
			Connection.eleTagNames = new string[] { "extLst" };
			Connection.eleNamespaceIds = new byte[] { 14 };
		}

		// Token: 0x04008133 RID: 33075
		private const string tagName = "cxn";

		// Token: 0x04008134 RID: 33076
		private const byte tagNsId = 14;

		// Token: 0x04008135 RID: 33077
		internal const int ElementTypeIdConst = 10640;

		// Token: 0x04008136 RID: 33078
		private static string[] attributeTagNames = new string[] { "modelId", "type", "srcId", "destId", "srcOrd", "destOrd", "parTransId", "sibTransId", "presId" };

		// Token: 0x04008137 RID: 33079
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008138 RID: 33080
		private static readonly string[] eleTagNames;

		// Token: 0x04008139 RID: 33081
		private static readonly byte[] eleNamespaceIds;
	}
}
