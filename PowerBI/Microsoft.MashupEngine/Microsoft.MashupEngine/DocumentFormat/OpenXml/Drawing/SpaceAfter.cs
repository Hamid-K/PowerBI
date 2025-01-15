using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002829 RID: 10281
	[GeneratedCode("DomGen", "2.0")]
	internal class SpaceAfter : TextSpacingType
	{
		// Token: 0x170065E0 RID: 26080
		// (get) Token: 0x06014228 RID: 82472 RVA: 0x0030FAE2 File Offset: 0x0030DCE2
		public override string LocalName
		{
			get
			{
				return "spcAft";
			}
		}

		// Token: 0x170065E1 RID: 26081
		// (get) Token: 0x06014229 RID: 82473 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065E2 RID: 26082
		// (get) Token: 0x0601422A RID: 82474 RVA: 0x0030FAE9 File Offset: 0x0030DCE9
		internal override int ElementTypeId
		{
			get
			{
				return 10313;
			}
		}

		// Token: 0x0601422B RID: 82475 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601422C RID: 82476 RVA: 0x0030FA9F File Offset: 0x0030DC9F
		public SpaceAfter()
		{
		}

		// Token: 0x0601422D RID: 82477 RVA: 0x0030FAA7 File Offset: 0x0030DCA7
		public SpaceAfter(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601422E RID: 82478 RVA: 0x0030FAB0 File Offset: 0x0030DCB0
		public SpaceAfter(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601422F RID: 82479 RVA: 0x0030FAB9 File Offset: 0x0030DCB9
		public SpaceAfter(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014230 RID: 82480 RVA: 0x0030FAF0 File Offset: 0x0030DCF0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpaceAfter>(deep);
		}

		// Token: 0x0400892D RID: 35117
		private const string tagName = "spcAft";

		// Token: 0x0400892E RID: 35118
		private const byte tagNsId = 10;

		// Token: 0x0400892F RID: 35119
		internal const int ElementTypeIdConst = 10313;
	}
}
