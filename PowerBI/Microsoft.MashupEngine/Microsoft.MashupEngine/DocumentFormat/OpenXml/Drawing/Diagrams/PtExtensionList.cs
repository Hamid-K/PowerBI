using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002695 RID: 9877
	[ChildElementInfo(typeof(PtExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17005D5B RID: 23899
		// (get) Token: 0x06012ED3 RID: 77523 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17005D5C RID: 23900
		// (get) Token: 0x06012ED4 RID: 77524 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D5D RID: 23901
		// (get) Token: 0x06012ED5 RID: 77525 RVA: 0x00301030 File Offset: 0x002FF230
		internal override int ElementTypeId
		{
			get
			{
				return 10692;
			}
		}

		// Token: 0x06012ED6 RID: 77526 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012ED7 RID: 77527 RVA: 0x00293ECF File Offset: 0x002920CF
		public PtExtensionList()
		{
		}

		// Token: 0x06012ED8 RID: 77528 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012ED9 RID: 77529 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EDA RID: 77530 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012EDB RID: 77531 RVA: 0x00301037 File Offset: 0x002FF237
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new PtExtension();
			}
			return null;
		}

		// Token: 0x06012EDC RID: 77532 RVA: 0x00301052 File Offset: 0x002FF252
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PtExtensionList>(deep);
		}

		// Token: 0x04008233 RID: 33331
		private const string tagName = "extLst";

		// Token: 0x04008234 RID: 33332
		private const byte tagNsId = 14;

		// Token: 0x04008235 RID: 33333
		internal const int ElementTypeIdConst = 10692;
	}
}
