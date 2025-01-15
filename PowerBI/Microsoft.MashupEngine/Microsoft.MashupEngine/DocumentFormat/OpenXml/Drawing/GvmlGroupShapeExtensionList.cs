using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200283D RID: 10301
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GvmlGroupShapeExtension))]
	internal class GvmlGroupShapeExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17006663 RID: 26211
		// (get) Token: 0x06014368 RID: 82792 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17006664 RID: 26212
		// (get) Token: 0x06014369 RID: 82793 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006665 RID: 26213
		// (get) Token: 0x0601436A RID: 82794 RVA: 0x0031085F File Offset: 0x0030EA5F
		internal override int ElementTypeId
		{
			get
			{
				return 10337;
			}
		}

		// Token: 0x0601436B RID: 82795 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601436C RID: 82796 RVA: 0x00293ECF File Offset: 0x002920CF
		public GvmlGroupShapeExtensionList()
		{
		}

		// Token: 0x0601436D RID: 82797 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GvmlGroupShapeExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601436E RID: 82798 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GvmlGroupShapeExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601436F RID: 82799 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GvmlGroupShapeExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014370 RID: 82800 RVA: 0x00310866 File Offset: 0x0030EA66
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new GvmlGroupShapeExtension();
			}
			return null;
		}

		// Token: 0x06014371 RID: 82801 RVA: 0x00310881 File Offset: 0x0030EA81
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GvmlGroupShapeExtensionList>(deep);
		}

		// Token: 0x04008989 RID: 35209
		private const string tagName = "extLst";

		// Token: 0x0400898A RID: 35210
		private const byte tagNsId = 10;

		// Token: 0x0400898B RID: 35211
		internal const int ElementTypeIdConst = 10337;
	}
}
