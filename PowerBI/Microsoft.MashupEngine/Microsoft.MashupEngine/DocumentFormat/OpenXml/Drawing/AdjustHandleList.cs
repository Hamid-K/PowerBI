using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D5 RID: 10197
	[ChildElementInfo(typeof(AdjustHandlePolar))]
	[ChildElementInfo(typeof(AdjustHandleXY))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AdjustHandleList : OpenXmlCompositeElement
	{
		// Token: 0x170063E1 RID: 25569
		// (get) Token: 0x06013D53 RID: 81235 RVA: 0x0030C1F3 File Offset: 0x0030A3F3
		public override string LocalName
		{
			get
			{
				return "ahLst";
			}
		}

		// Token: 0x170063E2 RID: 25570
		// (get) Token: 0x06013D54 RID: 81236 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063E3 RID: 25571
		// (get) Token: 0x06013D55 RID: 81237 RVA: 0x0030C1FA File Offset: 0x0030A3FA
		internal override int ElementTypeId
		{
			get
			{
				return 10230;
			}
		}

		// Token: 0x06013D56 RID: 81238 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013D57 RID: 81239 RVA: 0x00293ECF File Offset: 0x002920CF
		public AdjustHandleList()
		{
		}

		// Token: 0x06013D58 RID: 81240 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AdjustHandleList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D59 RID: 81241 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AdjustHandleList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D5A RID: 81242 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AdjustHandleList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D5B RID: 81243 RVA: 0x0030C201 File Offset: 0x0030A401
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ahXY" == name)
			{
				return new AdjustHandleXY();
			}
			if (10 == namespaceId && "ahPolar" == name)
			{
				return new AdjustHandlePolar();
			}
			return null;
		}

		// Token: 0x06013D5C RID: 81244 RVA: 0x0030C234 File Offset: 0x0030A434
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdjustHandleList>(deep);
		}

		// Token: 0x040087FC RID: 34812
		private const string tagName = "ahLst";

		// Token: 0x040087FD RID: 34813
		private const byte tagNsId = 10;

		// Token: 0x040087FE RID: 34814
		internal const int ElementTypeIdConst = 10230;
	}
}
