using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A52 RID: 10834
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NotesMasterId : OpenXmlCompositeElement
	{
		// Token: 0x170071E8 RID: 29160
		// (get) Token: 0x06015D85 RID: 89477 RVA: 0x00323AF7 File Offset: 0x00321CF7
		public override string LocalName
		{
			get
			{
				return "notesMasterId";
			}
		}

		// Token: 0x170071E9 RID: 29161
		// (get) Token: 0x06015D86 RID: 89478 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071EA RID: 29162
		// (get) Token: 0x06015D87 RID: 89479 RVA: 0x00323AFE File Offset: 0x00321CFE
		internal override int ElementTypeId
		{
			get
			{
				return 12253;
			}
		}

		// Token: 0x06015D88 RID: 89480 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071EB RID: 29163
		// (get) Token: 0x06015D89 RID: 89481 RVA: 0x00323B05 File Offset: 0x00321D05
		internal override string[] AttributeTagNames
		{
			get
			{
				return NotesMasterId.attributeTagNames;
			}
		}

		// Token: 0x170071EC RID: 29164
		// (get) Token: 0x06015D8A RID: 89482 RVA: 0x00323B0C File Offset: 0x00321D0C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NotesMasterId.attributeNamespaceIds;
			}
		}

		// Token: 0x170071ED RID: 29165
		// (get) Token: 0x06015D8B RID: 89483 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015D8C RID: 89484 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
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

		// Token: 0x06015D8D RID: 89485 RVA: 0x00293ECF File Offset: 0x002920CF
		public NotesMasterId()
		{
		}

		// Token: 0x06015D8E RID: 89486 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NotesMasterId(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D8F RID: 89487 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NotesMasterId(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D90 RID: 89488 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NotesMasterId(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015D91 RID: 89489 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170071EE RID: 29166
		// (get) Token: 0x06015D92 RID: 89490 RVA: 0x00323B13 File Offset: 0x00321D13
		internal override string[] ElementTagNames
		{
			get
			{
				return NotesMasterId.eleTagNames;
			}
		}

		// Token: 0x170071EF RID: 29167
		// (get) Token: 0x06015D93 RID: 89491 RVA: 0x00323B1A File Offset: 0x00321D1A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NotesMasterId.eleNamespaceIds;
			}
		}

		// Token: 0x170071F0 RID: 29168
		// (get) Token: 0x06015D94 RID: 89492 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170071F1 RID: 29169
		// (get) Token: 0x06015D95 RID: 89493 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06015D96 RID: 89494 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
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

		// Token: 0x06015D97 RID: 89495 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015D98 RID: 89496 RVA: 0x00323B21 File Offset: 0x00321D21
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesMasterId>(deep);
		}

		// Token: 0x04009511 RID: 38161
		private const string tagName = "notesMasterId";

		// Token: 0x04009512 RID: 38162
		private const byte tagNsId = 24;

		// Token: 0x04009513 RID: 38163
		internal const int ElementTypeIdConst = 12253;

		// Token: 0x04009514 RID: 38164
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009515 RID: 38165
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x04009516 RID: 38166
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x04009517 RID: 38167
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
