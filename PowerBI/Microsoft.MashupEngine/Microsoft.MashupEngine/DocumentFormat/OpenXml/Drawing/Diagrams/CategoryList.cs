using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200267A RID: 9850
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Category))]
	internal class CategoryList : OpenXmlCompositeElement
	{
		// Token: 0x17005C84 RID: 23684
		// (get) Token: 0x06012D02 RID: 77058 RVA: 0x002FDBE7 File Offset: 0x002FBDE7
		public override string LocalName
		{
			get
			{
				return "catLst";
			}
		}

		// Token: 0x17005C85 RID: 23685
		// (get) Token: 0x06012D03 RID: 77059 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C86 RID: 23686
		// (get) Token: 0x06012D04 RID: 77060 RVA: 0x002FFB9B File Offset: 0x002FDD9B
		internal override int ElementTypeId
		{
			get
			{
				return 10665;
			}
		}

		// Token: 0x06012D05 RID: 77061 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012D06 RID: 77062 RVA: 0x00293ECF File Offset: 0x002920CF
		public CategoryList()
		{
		}

		// Token: 0x06012D07 RID: 77063 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CategoryList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D08 RID: 77064 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CategoryList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D09 RID: 77065 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CategoryList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012D0A RID: 77066 RVA: 0x002FFBA2 File Offset: 0x002FDDA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "cat" == name)
			{
				return new Category();
			}
			return null;
		}

		// Token: 0x06012D0B RID: 77067 RVA: 0x002FFBBD File Offset: 0x002FDDBD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CategoryList>(deep);
		}

		// Token: 0x040081B1 RID: 33201
		private const string tagName = "catLst";

		// Token: 0x040081B2 RID: 33202
		private const byte tagNsId = 14;

		// Token: 0x040081B3 RID: 33203
		internal const int ElementTypeIdConst = 10665;
	}
}
