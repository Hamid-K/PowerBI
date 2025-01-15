using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D3 RID: 10195
	[GeneratedCode("DomGen", "2.0")]
	internal class AdjustValueList : GeometryGuideListType
	{
		// Token: 0x170063DB RID: 25563
		// (get) Token: 0x06013D41 RID: 81217 RVA: 0x0030C1A2 File Offset: 0x0030A3A2
		public override string LocalName
		{
			get
			{
				return "avLst";
			}
		}

		// Token: 0x170063DC RID: 25564
		// (get) Token: 0x06013D42 RID: 81218 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063DD RID: 25565
		// (get) Token: 0x06013D43 RID: 81219 RVA: 0x0030C1A9 File Offset: 0x0030A3A9
		internal override int ElementTypeId
		{
			get
			{
				return 10228;
			}
		}

		// Token: 0x06013D44 RID: 81220 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013D45 RID: 81221 RVA: 0x0030C1B0 File Offset: 0x0030A3B0
		public AdjustValueList()
		{
		}

		// Token: 0x06013D46 RID: 81222 RVA: 0x0030C1B8 File Offset: 0x0030A3B8
		public AdjustValueList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D47 RID: 81223 RVA: 0x0030C1C1 File Offset: 0x0030A3C1
		public AdjustValueList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D48 RID: 81224 RVA: 0x0030C1CA File Offset: 0x0030A3CA
		public AdjustValueList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D49 RID: 81225 RVA: 0x0030C1D3 File Offset: 0x0030A3D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdjustValueList>(deep);
		}

		// Token: 0x040087F6 RID: 34806
		private const string tagName = "avLst";

		// Token: 0x040087F7 RID: 34807
		private const byte tagNsId = 10;

		// Token: 0x040087F8 RID: 34808
		internal const int ElementTypeIdConst = 10228;
	}
}
