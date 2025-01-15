using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E3 RID: 9187
	[ChildElementInfo(typeof(CalculatedMembers), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Connection : OpenXmlCompositeElement
	{
		// Token: 0x17004D92 RID: 19858
		// (get) Token: 0x06010B72 RID: 68466 RVA: 0x002E658F File Offset: 0x002E478F
		public override string LocalName
		{
			get
			{
				return "connection";
			}
		}

		// Token: 0x17004D93 RID: 19859
		// (get) Token: 0x06010B73 RID: 68467 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D94 RID: 19860
		// (get) Token: 0x06010B74 RID: 68468 RVA: 0x002E6596 File Offset: 0x002E4796
		internal override int ElementTypeId
		{
			get
			{
				return 12913;
			}
		}

		// Token: 0x06010B75 RID: 68469 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D95 RID: 19861
		// (get) Token: 0x06010B76 RID: 68470 RVA: 0x002E659D File Offset: 0x002E479D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Connection.attributeTagNames;
			}
		}

		// Token: 0x17004D96 RID: 19862
		// (get) Token: 0x06010B77 RID: 68471 RVA: 0x002E65A4 File Offset: 0x002E47A4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Connection.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D97 RID: 19863
		// (get) Token: 0x06010B78 RID: 68472 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010B79 RID: 68473 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "culture")]
		public StringValue Culture
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

		// Token: 0x17004D98 RID: 19864
		// (get) Token: 0x06010B7A RID: 68474 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010B7B RID: 68475 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "embeddedDataId")]
		public StringValue EmbeddedDataId
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

		// Token: 0x06010B7C RID: 68476 RVA: 0x00293ECF File Offset: 0x002920CF
		public Connection()
		{
		}

		// Token: 0x06010B7D RID: 68477 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Connection(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010B7E RID: 68478 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Connection(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010B7F RID: 68479 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Connection(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010B80 RID: 68480 RVA: 0x002E65AB File Offset: 0x002E47AB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "calculatedMembers" == name)
			{
				return new CalculatedMembers();
			}
			return null;
		}

		// Token: 0x17004D99 RID: 19865
		// (get) Token: 0x06010B81 RID: 68481 RVA: 0x002E65C6 File Offset: 0x002E47C6
		internal override string[] ElementTagNames
		{
			get
			{
				return Connection.eleTagNames;
			}
		}

		// Token: 0x17004D9A RID: 19866
		// (get) Token: 0x06010B82 RID: 68482 RVA: 0x002E65CD File Offset: 0x002E47CD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Connection.eleNamespaceIds;
			}
		}

		// Token: 0x17004D9B RID: 19867
		// (get) Token: 0x06010B83 RID: 68483 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004D9C RID: 19868
		// (get) Token: 0x06010B84 RID: 68484 RVA: 0x002E65D4 File Offset: 0x002E47D4
		// (set) Token: 0x06010B85 RID: 68485 RVA: 0x002E65DD File Offset: 0x002E47DD
		public CalculatedMembers CalculatedMembers
		{
			get
			{
				return base.GetElement<CalculatedMembers>(0);
			}
			set
			{
				base.SetElement<CalculatedMembers>(0, value);
			}
		}

		// Token: 0x06010B86 RID: 68486 RVA: 0x002E65E7 File Offset: 0x002E47E7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "culture" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "embeddedDataId" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010B87 RID: 68487 RVA: 0x002E661D File Offset: 0x002E481D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Connection>(deep);
		}

		// Token: 0x06010B88 RID: 68488 RVA: 0x002E6628 File Offset: 0x002E4828
		// Note: this type is marked as 'beforefieldinit'.
		static Connection()
		{
			byte[] array = new byte[2];
			Connection.attributeNamespaceIds = array;
			Connection.eleTagNames = new string[] { "calculatedMembers" };
			Connection.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x04007606 RID: 30214
		private const string tagName = "connection";

		// Token: 0x04007607 RID: 30215
		private const byte tagNsId = 53;

		// Token: 0x04007608 RID: 30216
		internal const int ElementTypeIdConst = 12913;

		// Token: 0x04007609 RID: 30217
		private static string[] attributeTagNames = new string[] { "culture", "embeddedDataId" };

		// Token: 0x0400760A RID: 30218
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400760B RID: 30219
		private static readonly string[] eleTagNames;

		// Token: 0x0400760C RID: 30220
		private static readonly byte[] eleNamespaceIds;
	}
}
