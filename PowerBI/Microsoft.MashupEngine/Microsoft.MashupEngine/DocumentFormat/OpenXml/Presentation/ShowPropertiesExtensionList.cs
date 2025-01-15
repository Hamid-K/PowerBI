using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AAD RID: 10925
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShowPropertiesExtension))]
	internal class ShowPropertiesExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170074A8 RID: 29864
		// (get) Token: 0x060163B6 RID: 91062 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170074A9 RID: 29865
		// (get) Token: 0x060163B7 RID: 91063 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074AA RID: 29866
		// (get) Token: 0x060163B8 RID: 91064 RVA: 0x003280DE File Offset: 0x003262DE
		internal override int ElementTypeId
		{
			get
			{
				return 12340;
			}
		}

		// Token: 0x060163B9 RID: 91065 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060163BA RID: 91066 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShowPropertiesExtensionList()
		{
		}

		// Token: 0x060163BB RID: 91067 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShowPropertiesExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163BC RID: 91068 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShowPropertiesExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163BD RID: 91069 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShowPropertiesExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060163BE RID: 91070 RVA: 0x003280E5 File Offset: 0x003262E5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new ShowPropertiesExtension();
			}
			return null;
		}

		// Token: 0x060163BF RID: 91071 RVA: 0x00328100 File Offset: 0x00326300
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowPropertiesExtensionList>(deep);
		}

		// Token: 0x040096C7 RID: 38599
		private const string tagName = "extLst";

		// Token: 0x040096C8 RID: 38600
		private const byte tagNsId = 24;

		// Token: 0x040096C9 RID: 38601
		internal const int ElementTypeIdConst = 12340;
	}
}
