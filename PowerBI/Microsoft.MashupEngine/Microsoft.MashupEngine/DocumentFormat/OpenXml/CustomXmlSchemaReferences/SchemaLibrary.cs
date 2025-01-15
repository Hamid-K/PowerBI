using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.CustomXmlSchemaReferences
{
	// Token: 0x02002B12 RID: 11026
	[ChildElementInfo(typeof(Schema))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SchemaLibrary : OpenXmlCompositeElement
	{
		// Token: 0x170075A5 RID: 30117
		// (get) Token: 0x060165EA RID: 91626 RVA: 0x003295E5 File Offset: 0x003277E5
		public override string LocalName
		{
			get
			{
				return "schemaLibrary";
			}
		}

		// Token: 0x170075A6 RID: 30118
		// (get) Token: 0x060165EB RID: 91627 RVA: 0x000E78B2 File Offset: 0x000E5AB2
		internal override byte NamespaceId
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170075A7 RID: 30119
		// (get) Token: 0x060165EC RID: 91628 RVA: 0x003295EC File Offset: 0x003277EC
		internal override int ElementTypeId
		{
			get
			{
				return 12397;
			}
		}

		// Token: 0x060165ED RID: 91629 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060165EE RID: 91630 RVA: 0x00293ECF File Offset: 0x002920CF
		public SchemaLibrary()
		{
		}

		// Token: 0x060165EF RID: 91631 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SchemaLibrary(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060165F0 RID: 91632 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SchemaLibrary(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060165F1 RID: 91633 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SchemaLibrary(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060165F2 RID: 91634 RVA: 0x003295F3 File Offset: 0x003277F3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (25 == namespaceId && "schema" == name)
			{
				return new Schema();
			}
			return null;
		}

		// Token: 0x060165F3 RID: 91635 RVA: 0x0032960E File Offset: 0x0032780E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SchemaLibrary>(deep);
		}

		// Token: 0x040098BD RID: 39101
		private const string tagName = "schemaLibrary";

		// Token: 0x040098BE RID: 39102
		private const byte tagNsId = 25;

		// Token: 0x040098BF RID: 39103
		internal const int ElementTypeIdConst = 12397;
	}
}
