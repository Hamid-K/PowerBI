using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200265C RID: 9820
	[ChildElementInfo(typeof(ColorTransformCategory))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorTransformCategories : OpenXmlCompositeElement
	{
		// Token: 0x17005B7A RID: 23418
		// (get) Token: 0x06012AA6 RID: 76454 RVA: 0x002FDBE7 File Offset: 0x002FBDE7
		public override string LocalName
		{
			get
			{
				return "catLst";
			}
		}

		// Token: 0x17005B7B RID: 23419
		// (get) Token: 0x06012AA7 RID: 76455 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B7C RID: 23420
		// (get) Token: 0x06012AA8 RID: 76456 RVA: 0x002FDBEE File Offset: 0x002FBDEE
		internal override int ElementTypeId
		{
			get
			{
				return 10637;
			}
		}

		// Token: 0x06012AA9 RID: 76457 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012AAA RID: 76458 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorTransformCategories()
		{
		}

		// Token: 0x06012AAB RID: 76459 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorTransformCategories(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012AAC RID: 76460 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorTransformCategories(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012AAD RID: 76461 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorTransformCategories(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012AAE RID: 76462 RVA: 0x002FDBF5 File Offset: 0x002FBDF5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "cat" == name)
			{
				return new ColorTransformCategory();
			}
			return null;
		}

		// Token: 0x06012AAF RID: 76463 RVA: 0x002FDC10 File Offset: 0x002FBE10
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorTransformCategories>(deep);
		}

		// Token: 0x04008122 RID: 33058
		private const string tagName = "catLst";

		// Token: 0x04008123 RID: 33059
		private const byte tagNsId = 14;

		// Token: 0x04008124 RID: 33060
		internal const int ElementTypeIdConst = 10637;
	}
}
