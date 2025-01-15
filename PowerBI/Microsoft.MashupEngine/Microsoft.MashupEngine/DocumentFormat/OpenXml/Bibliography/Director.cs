using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F6 RID: 10486
	[GeneratedCode("DomGen", "2.0")]
	internal class Director : NameType
	{
		// Token: 0x1700698B RID: 27019
		// (get) Token: 0x06014AAF RID: 84655 RVA: 0x00315475 File Offset: 0x00313675
		public override string LocalName
		{
			get
			{
				return "Director";
			}
		}

		// Token: 0x1700698C RID: 27020
		// (get) Token: 0x06014AB0 RID: 84656 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700698D RID: 27021
		// (get) Token: 0x06014AB1 RID: 84657 RVA: 0x0031547C File Offset: 0x0031367C
		internal override int ElementTypeId
		{
			get
			{
				return 10772;
			}
		}

		// Token: 0x06014AB2 RID: 84658 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AB3 RID: 84659 RVA: 0x003153D6 File Offset: 0x003135D6
		public Director()
		{
		}

		// Token: 0x06014AB4 RID: 84660 RVA: 0x003153DE File Offset: 0x003135DE
		public Director(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AB5 RID: 84661 RVA: 0x003153E7 File Offset: 0x003135E7
		public Director(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AB6 RID: 84662 RVA: 0x003153F0 File Offset: 0x003135F0
		public Director(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AB7 RID: 84663 RVA: 0x00315483 File Offset: 0x00313683
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Director>(deep);
		}

		// Token: 0x04008F6D RID: 36717
		private const string tagName = "Director";

		// Token: 0x04008F6E RID: 36718
		private const byte tagNsId = 9;

		// Token: 0x04008F6F RID: 36719
		internal const int ElementTypeIdConst = 10772;
	}
}
