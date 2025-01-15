using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ABA RID: 10938
	[ChildElementInfo(typeof(PresentationExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PresentationExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17007504 RID: 29956
		// (get) Token: 0x06016487 RID: 91271 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17007505 RID: 29957
		// (get) Token: 0x06016488 RID: 91272 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007506 RID: 29958
		// (get) Token: 0x06016489 RID: 91273 RVA: 0x00328924 File Offset: 0x00326B24
		internal override int ElementTypeId
		{
			get
			{
				return 12354;
			}
		}

		// Token: 0x0601648A RID: 91274 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601648B RID: 91275 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresentationExtensionList()
		{
		}

		// Token: 0x0601648C RID: 91276 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresentationExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601648D RID: 91277 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresentationExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601648E RID: 91278 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresentationExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601648F RID: 91279 RVA: 0x0032892B File Offset: 0x00326B2B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new PresentationExtension();
			}
			return null;
		}

		// Token: 0x06016490 RID: 91280 RVA: 0x00328946 File Offset: 0x00326B46
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationExtensionList>(deep);
		}

		// Token: 0x04009700 RID: 38656
		private const string tagName = "extLst";

		// Token: 0x04009701 RID: 38657
		private const byte tagNsId = 24;

		// Token: 0x04009702 RID: 38658
		internal const int ElementTypeIdConst = 12354;
	}
}
