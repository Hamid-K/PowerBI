using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A81 RID: 10881
	[GeneratedCode("DomGen", "2.0")]
	internal class Position : Point2DType
	{
		// Token: 0x17007354 RID: 29524
		// (get) Token: 0x060160A5 RID: 90277 RVA: 0x0030BA47 File Offset: 0x00309C47
		public override string LocalName
		{
			get
			{
				return "pos";
			}
		}

		// Token: 0x17007355 RID: 29525
		// (get) Token: 0x060160A6 RID: 90278 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007356 RID: 29526
		// (get) Token: 0x060160A7 RID: 90279 RVA: 0x00325F0F File Offset: 0x0032410F
		internal override int ElementTypeId
		{
			get
			{
				return 12314;
			}
		}

		// Token: 0x060160A8 RID: 90280 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060160AA RID: 90282 RVA: 0x00325F16 File Offset: 0x00324116
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Position>(deep);
		}

		// Token: 0x040095EE RID: 38382
		private const string tagName = "pos";

		// Token: 0x040095EF RID: 38383
		private const byte tagNsId = 24;

		// Token: 0x040095F0 RID: 38384
		internal const int ElementTypeIdConst = 12314;
	}
}
