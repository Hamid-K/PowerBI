using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA1 RID: 10913
	[ChildElementInfo(typeof(CommonSlideDataExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CommonSlideDataExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17007439 RID: 29753
		// (get) Token: 0x060162BA RID: 90810 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700743A RID: 29754
		// (get) Token: 0x060162BB RID: 90811 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700743B RID: 29755
		// (get) Token: 0x060162BC RID: 90812 RVA: 0x0032736B File Offset: 0x0032556B
		internal override int ElementTypeId
		{
			get
			{
				return 12326;
			}
		}

		// Token: 0x060162BD RID: 90813 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060162BE RID: 90814 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonSlideDataExtensionList()
		{
		}

		// Token: 0x060162BF RID: 90815 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonSlideDataExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162C0 RID: 90816 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonSlideDataExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162C1 RID: 90817 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonSlideDataExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060162C2 RID: 90818 RVA: 0x00327372 File Offset: 0x00325572
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new CommonSlideDataExtension();
			}
			return null;
		}

		// Token: 0x060162C3 RID: 90819 RVA: 0x0032738D File Offset: 0x0032558D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonSlideDataExtensionList>(deep);
		}

		// Token: 0x04009689 RID: 38537
		private const string tagName = "extLst";

		// Token: 0x0400968A RID: 38538
		private const byte tagNsId = 24;

		// Token: 0x0400968B RID: 38539
		internal const int ElementTypeIdConst = 12326;
	}
}
