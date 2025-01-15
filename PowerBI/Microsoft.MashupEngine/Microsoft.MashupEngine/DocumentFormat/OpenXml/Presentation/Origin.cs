using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A80 RID: 10880
	[GeneratedCode("DomGen", "2.0")]
	internal class Origin : Point2DType
	{
		// Token: 0x17007351 RID: 29521
		// (get) Token: 0x0601609F RID: 90271 RVA: 0x002A3468 File Offset: 0x002A1668
		public override string LocalName
		{
			get
			{
				return "origin";
			}
		}

		// Token: 0x17007352 RID: 29522
		// (get) Token: 0x060160A0 RID: 90272 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007353 RID: 29523
		// (get) Token: 0x060160A1 RID: 90273 RVA: 0x00325EF7 File Offset: 0x003240F7
		internal override int ElementTypeId
		{
			get
			{
				return 12294;
			}
		}

		// Token: 0x060160A2 RID: 90274 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060160A4 RID: 90276 RVA: 0x00325F06 File Offset: 0x00324106
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Origin>(deep);
		}

		// Token: 0x040095EB RID: 38379
		private const string tagName = "origin";

		// Token: 0x040095EC RID: 38380
		private const byte tagNsId = 24;

		// Token: 0x040095ED RID: 38381
		internal const int ElementTypeIdConst = 12294;
	}
}
