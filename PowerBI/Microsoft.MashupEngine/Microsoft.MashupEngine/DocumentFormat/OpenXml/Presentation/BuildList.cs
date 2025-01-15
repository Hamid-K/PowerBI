using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A43 RID: 10819
	[ChildElementInfo(typeof(BuildDiagram))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BuildParagraph))]
	[ChildElementInfo(typeof(BuildOleChart))]
	[ChildElementInfo(typeof(BuildGraphics))]
	internal class BuildList : OpenXmlCompositeElement
	{
		// Token: 0x17007171 RID: 29041
		// (get) Token: 0x06015C7B RID: 89211 RVA: 0x00323147 File Offset: 0x00321347
		public override string LocalName
		{
			get
			{
				return "bldLst";
			}
		}

		// Token: 0x17007172 RID: 29042
		// (get) Token: 0x06015C7C RID: 89212 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007173 RID: 29043
		// (get) Token: 0x06015C7D RID: 89213 RVA: 0x0032314E File Offset: 0x0032134E
		internal override int ElementTypeId
		{
			get
			{
				return 12238;
			}
		}

		// Token: 0x06015C7E RID: 89214 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015C7F RID: 89215 RVA: 0x00293ECF File Offset: 0x002920CF
		public BuildList()
		{
		}

		// Token: 0x06015C80 RID: 89216 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BuildList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C81 RID: 89217 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BuildList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C82 RID: 89218 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BuildList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015C83 RID: 89219 RVA: 0x00323158 File Offset: 0x00321358
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "bldP" == name)
			{
				return new BuildParagraph();
			}
			if (24 == namespaceId && "bldDgm" == name)
			{
				return new BuildDiagram();
			}
			if (24 == namespaceId && "bldOleChart" == name)
			{
				return new BuildOleChart();
			}
			if (24 == namespaceId && "bldGraphic" == name)
			{
				return new BuildGraphics();
			}
			return null;
		}

		// Token: 0x06015C84 RID: 89220 RVA: 0x003231C6 File Offset: 0x003213C6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildList>(deep);
		}

		// Token: 0x040094C7 RID: 38087
		private const string tagName = "bldLst";

		// Token: 0x040094C8 RID: 38088
		private const byte tagNsId = 24;

		// Token: 0x040094C9 RID: 38089
		internal const int ElementTypeIdConst = 12238;
	}
}
