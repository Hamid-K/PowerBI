using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A86 RID: 10886
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Guide))]
	internal class GuideList : OpenXmlCompositeElement
	{
		// Token: 0x17007373 RID: 29555
		// (get) Token: 0x060160E8 RID: 90344 RVA: 0x00326183 File Offset: 0x00324383
		public override string LocalName
		{
			get
			{
				return "guideLst";
			}
		}

		// Token: 0x17007374 RID: 29556
		// (get) Token: 0x060160E9 RID: 90345 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007375 RID: 29557
		// (get) Token: 0x060160EA RID: 90346 RVA: 0x0032618A File Offset: 0x0032438A
		internal override int ElementTypeId
		{
			get
			{
				return 12299;
			}
		}

		// Token: 0x060160EB RID: 90347 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060160EC RID: 90348 RVA: 0x00293ECF File Offset: 0x002920CF
		public GuideList()
		{
		}

		// Token: 0x060160ED RID: 90349 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GuideList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060160EE RID: 90350 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GuideList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060160EF RID: 90351 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GuideList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060160F0 RID: 90352 RVA: 0x00326191 File Offset: 0x00324391
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "guide" == name)
			{
				return new Guide();
			}
			return null;
		}

		// Token: 0x060160F1 RID: 90353 RVA: 0x003261AC File Offset: 0x003243AC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GuideList>(deep);
		}

		// Token: 0x04009605 RID: 38405
		private const string tagName = "guideLst";

		// Token: 0x04009606 RID: 38406
		private const byte tagNsId = 24;

		// Token: 0x04009607 RID: 38407
		internal const int ElementTypeIdConst = 12299;
	}
}
