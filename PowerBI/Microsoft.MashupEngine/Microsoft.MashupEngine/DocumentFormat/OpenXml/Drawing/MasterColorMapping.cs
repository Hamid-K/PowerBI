using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002769 RID: 10089
	[GeneratedCode("DomGen", "2.0")]
	internal class MasterColorMapping : EmptyType
	{
		// Token: 0x17006125 RID: 24869
		// (get) Token: 0x06013731 RID: 79665 RVA: 0x003073CF File Offset: 0x003055CF
		public override string LocalName
		{
			get
			{
				return "masterClrMapping";
			}
		}

		// Token: 0x17006126 RID: 24870
		// (get) Token: 0x06013732 RID: 79666 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006127 RID: 24871
		// (get) Token: 0x06013733 RID: 79667 RVA: 0x003073D6 File Offset: 0x003055D6
		internal override int ElementTypeId
		{
			get
			{
				return 10244;
			}
		}

		// Token: 0x06013734 RID: 79668 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013736 RID: 79670 RVA: 0x003073DD File Offset: 0x003055DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MasterColorMapping>(deep);
		}

		// Token: 0x04008642 RID: 34370
		private const string tagName = "masterClrMapping";

		// Token: 0x04008643 RID: 34371
		private const byte tagNsId = 10;

		// Token: 0x04008644 RID: 34372
		internal const int ElementTypeIdConst = 10244;
	}
}
