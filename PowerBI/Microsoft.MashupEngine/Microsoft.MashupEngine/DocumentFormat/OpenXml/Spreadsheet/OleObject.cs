using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF2 RID: 11250
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EmbeddedObjectProperties), FileFormatVersions.Office2010)]
	internal class OleObject : OpenXmlCompositeElement
	{
		// Token: 0x17007EBC RID: 32444
		// (get) Token: 0x060179D1 RID: 96721 RVA: 0x0033922A File Offset: 0x0033742A
		public override string LocalName
		{
			get
			{
				return "oleObject";
			}
		}

		// Token: 0x17007EBD RID: 32445
		// (get) Token: 0x060179D2 RID: 96722 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007EBE RID: 32446
		// (get) Token: 0x060179D3 RID: 96723 RVA: 0x00339231 File Offset: 0x00337431
		internal override int ElementTypeId
		{
			get
			{
				return 11222;
			}
		}

		// Token: 0x060179D4 RID: 96724 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007EBF RID: 32447
		// (get) Token: 0x060179D5 RID: 96725 RVA: 0x00339238 File Offset: 0x00337438
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleObject.attributeTagNames;
			}
		}

		// Token: 0x17007EC0 RID: 32448
		// (get) Token: 0x060179D6 RID: 96726 RVA: 0x0033923F File Offset: 0x0033743F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleObject.attributeNamespaceIds;
			}
		}

		// Token: 0x17007EC1 RID: 32449
		// (get) Token: 0x060179D7 RID: 96727 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060179D8 RID: 96728 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "progId")]
		public StringValue ProgId
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

		// Token: 0x17007EC2 RID: 32450
		// (get) Token: 0x060179D9 RID: 96729 RVA: 0x00339246 File Offset: 0x00337446
		// (set) Token: 0x060179DA RID: 96730 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dvAspect")]
		public EnumValue<DataViewAspectValues> DataOrViewAspect
		{
			get
			{
				return (EnumValue<DataViewAspectValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007EC3 RID: 32451
		// (get) Token: 0x060179DB RID: 96731 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060179DC RID: 96732 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "link")]
		public StringValue Link
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

		// Token: 0x17007EC4 RID: 32452
		// (get) Token: 0x060179DD RID: 96733 RVA: 0x00339255 File Offset: 0x00337455
		// (set) Token: 0x060179DE RID: 96734 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "oleUpdate")]
		public EnumValue<OleUpdateValues> OleUpdate
		{
			get
			{
				return (EnumValue<OleUpdateValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007EC5 RID: 32453
		// (get) Token: 0x060179DF RID: 96735 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060179E0 RID: 96736 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "autoLoad")]
		public BooleanValue AutoLoad
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

		// Token: 0x17007EC6 RID: 32454
		// (get) Token: 0x060179E1 RID: 96737 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x060179E2 RID: 96738 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "shapeId")]
		public UInt32Value ShapeId
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

		// Token: 0x17007EC7 RID: 32455
		// (get) Token: 0x060179E3 RID: 96739 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x060179E4 RID: 96740 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(19, "id")]
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

		// Token: 0x060179E5 RID: 96741 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleObject()
		{
		}

		// Token: 0x060179E6 RID: 96742 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060179E7 RID: 96743 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060179E8 RID: 96744 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060179E9 RID: 96745 RVA: 0x00339264 File Offset: 0x00337464
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "objectPr" == name)
			{
				return new EmbeddedObjectProperties();
			}
			return null;
		}

		// Token: 0x17007EC8 RID: 32456
		// (get) Token: 0x060179EA RID: 96746 RVA: 0x0033927F File Offset: 0x0033747F
		internal override string[] ElementTagNames
		{
			get
			{
				return OleObject.eleTagNames;
			}
		}

		// Token: 0x17007EC9 RID: 32457
		// (get) Token: 0x060179EB RID: 96747 RVA: 0x00339286 File Offset: 0x00337486
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OleObject.eleNamespaceIds;
			}
		}

		// Token: 0x17007ECA RID: 32458
		// (get) Token: 0x060179EC RID: 96748 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007ECB RID: 32459
		// (get) Token: 0x060179ED RID: 96749 RVA: 0x0033928D File Offset: 0x0033748D
		// (set) Token: 0x060179EE RID: 96750 RVA: 0x00339296 File Offset: 0x00337496
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public EmbeddedObjectProperties EmbeddedObjectProperties
		{
			get
			{
				return base.GetElement<EmbeddedObjectProperties>(0);
			}
			set
			{
				base.SetElement<EmbeddedObjectProperties>(0, value);
			}
		}

		// Token: 0x060179EF RID: 96751 RVA: 0x003392A0 File Offset: 0x003374A0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "progId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dvAspect" == name)
			{
				return new EnumValue<DataViewAspectValues>();
			}
			if (namespaceId == 0 && "link" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "oleUpdate" == name)
			{
				return new EnumValue<OleUpdateValues>();
			}
			if (namespaceId == 0 && "autoLoad" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "shapeId" == name)
			{
				return new UInt32Value();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060179F0 RID: 96752 RVA: 0x00339351 File Offset: 0x00337551
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleObject>(deep);
		}

		// Token: 0x04009CE6 RID: 40166
		private const string tagName = "oleObject";

		// Token: 0x04009CE7 RID: 40167
		private const byte tagNsId = 22;

		// Token: 0x04009CE8 RID: 40168
		internal const int ElementTypeIdConst = 11222;

		// Token: 0x04009CE9 RID: 40169
		private static string[] attributeTagNames = new string[] { "progId", "dvAspect", "link", "oleUpdate", "autoLoad", "shapeId", "id" };

		// Token: 0x04009CEA RID: 40170
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 0, 0, 0, 19 };

		// Token: 0x04009CEB RID: 40171
		private static readonly string[] eleTagNames = new string[] { "objectPr" };

		// Token: 0x04009CEC RID: 40172
		private static readonly byte[] eleNamespaceIds = new byte[] { 22 };
	}
}
