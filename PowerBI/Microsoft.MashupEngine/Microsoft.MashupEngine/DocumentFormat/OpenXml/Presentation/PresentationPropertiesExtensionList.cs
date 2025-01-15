using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AAC RID: 10924
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PresentationPropertiesExtension))]
	internal class PresentationPropertiesExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170074A5 RID: 29861
		// (get) Token: 0x060163AC RID: 91052 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170074A6 RID: 29862
		// (get) Token: 0x060163AD RID: 91053 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074A7 RID: 29863
		// (get) Token: 0x060163AE RID: 91054 RVA: 0x003280B3 File Offset: 0x003262B3
		internal override int ElementTypeId
		{
			get
			{
				return 12338;
			}
		}

		// Token: 0x060163AF RID: 91055 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060163B0 RID: 91056 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresentationPropertiesExtensionList()
		{
		}

		// Token: 0x060163B1 RID: 91057 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresentationPropertiesExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163B2 RID: 91058 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresentationPropertiesExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163B3 RID: 91059 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresentationPropertiesExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060163B4 RID: 91060 RVA: 0x003280BA File Offset: 0x003262BA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new PresentationPropertiesExtension();
			}
			return null;
		}

		// Token: 0x060163B5 RID: 91061 RVA: 0x003280D5 File Offset: 0x003262D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationPropertiesExtensionList>(deep);
		}

		// Token: 0x040096C4 RID: 38596
		private const string tagName = "extLst";

		// Token: 0x040096C5 RID: 38597
		private const byte tagNsId = 24;

		// Token: 0x040096C6 RID: 38598
		internal const int ElementTypeIdConst = 12338;
	}
}
