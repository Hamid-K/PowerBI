using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB1 RID: 10929
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SlideId))]
	internal class SlideIdList : OpenXmlCompositeElement
	{
		// Token: 0x170074BC RID: 29884
		// (get) Token: 0x060163EA RID: 91114 RVA: 0x002E55A3 File Offset: 0x002E37A3
		public override string LocalName
		{
			get
			{
				return "sldIdLst";
			}
		}

		// Token: 0x170074BD RID: 29885
		// (get) Token: 0x060163EB RID: 91115 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074BE RID: 29886
		// (get) Token: 0x060163EC RID: 91116 RVA: 0x0032824C File Offset: 0x0032644C
		internal override int ElementTypeId
		{
			get
			{
				return 12344;
			}
		}

		// Token: 0x060163ED RID: 91117 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060163EE RID: 91118 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideIdList()
		{
		}

		// Token: 0x060163EF RID: 91119 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideIdList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163F0 RID: 91120 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideIdList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163F1 RID: 91121 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideIdList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060163F2 RID: 91122 RVA: 0x00328253 File Offset: 0x00326453
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sldId" == name)
			{
				return new SlideId();
			}
			return null;
		}

		// Token: 0x060163F3 RID: 91123 RVA: 0x0032826E File Offset: 0x0032646E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideIdList>(deep);
		}

		// Token: 0x040096D7 RID: 38615
		private const string tagName = "sldIdLst";

		// Token: 0x040096D8 RID: 38616
		private const byte tagNsId = 24;

		// Token: 0x040096D9 RID: 38617
		internal const int ElementTypeIdConst = 12344;
	}
}
