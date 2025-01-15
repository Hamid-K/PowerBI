using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200268F RID: 9871
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StyleDisplayCategory))]
	internal class StyleDisplayCategories : OpenXmlCompositeElement
	{
		// Token: 0x17005D0F RID: 23823
		// (get) Token: 0x06012E2E RID: 77358 RVA: 0x002FDBE7 File Offset: 0x002FBDE7
		public override string LocalName
		{
			get
			{
				return "catLst";
			}
		}

		// Token: 0x17005D10 RID: 23824
		// (get) Token: 0x06012E2F RID: 77359 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D11 RID: 23825
		// (get) Token: 0x06012E30 RID: 77360 RVA: 0x00300613 File Offset: 0x002FE813
		internal override int ElementTypeId
		{
			get
			{
				return 10686;
			}
		}

		// Token: 0x06012E31 RID: 77361 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012E32 RID: 77362 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleDisplayCategories()
		{
		}

		// Token: 0x06012E33 RID: 77363 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleDisplayCategories(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E34 RID: 77364 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleDisplayCategories(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E35 RID: 77365 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleDisplayCategories(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012E36 RID: 77366 RVA: 0x0030061A File Offset: 0x002FE81A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "cat" == name)
			{
				return new StyleDisplayCategory();
			}
			return null;
		}

		// Token: 0x06012E37 RID: 77367 RVA: 0x00300635 File Offset: 0x002FE835
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleDisplayCategories>(deep);
		}

		// Token: 0x04008213 RID: 33299
		private const string tagName = "catLst";

		// Token: 0x04008214 RID: 33300
		private const byte tagNsId = 14;

		// Token: 0x04008215 RID: 33301
		internal const int ElementTypeIdConst = 10686;
	}
}
