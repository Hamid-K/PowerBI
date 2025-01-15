using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D8 RID: 10200
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Path))]
	internal class PathList : OpenXmlCompositeElement
	{
		// Token: 0x170063F0 RID: 25584
		// (get) Token: 0x06013D79 RID: 81273 RVA: 0x0030C33F File Offset: 0x0030A53F
		public override string LocalName
		{
			get
			{
				return "pathLst";
			}
		}

		// Token: 0x170063F1 RID: 25585
		// (get) Token: 0x06013D7A RID: 81274 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063F2 RID: 25586
		// (get) Token: 0x06013D7B RID: 81275 RVA: 0x0030C346 File Offset: 0x0030A546
		internal override int ElementTypeId
		{
			get
			{
				return 10233;
			}
		}

		// Token: 0x06013D7C RID: 81276 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013D7D RID: 81277 RVA: 0x00293ECF File Offset: 0x002920CF
		public PathList()
		{
		}

		// Token: 0x06013D7E RID: 81278 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PathList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D7F RID: 81279 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PathList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D80 RID: 81280 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PathList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D81 RID: 81281 RVA: 0x0030C34D File Offset: 0x0030A54D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "path" == name)
			{
				return new Path();
			}
			return null;
		}

		// Token: 0x06013D82 RID: 81282 RVA: 0x0030C368 File Offset: 0x0030A568
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PathList>(deep);
		}

		// Token: 0x04008807 RID: 34823
		private const string tagName = "pathLst";

		// Token: 0x04008808 RID: 34824
		private const byte tagNsId = 10;

		// Token: 0x04008809 RID: 34825
		internal const int ElementTypeIdConst = 10233;
	}
}
