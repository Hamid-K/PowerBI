using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002544 RID: 9540
	[GeneratedCode("DomGen", "2.0")]
	internal class SecondPiePoint : UnsignedIntegerType
	{
		// Token: 0x170054F1 RID: 21745
		// (get) Token: 0x06011BFD RID: 72701 RVA: 0x002F19B2 File Offset: 0x002EFBB2
		public override string LocalName
		{
			get
			{
				return "secondPiePt";
			}
		}

		// Token: 0x170054F2 RID: 21746
		// (get) Token: 0x06011BFE RID: 72702 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054F3 RID: 21747
		// (get) Token: 0x06011BFF RID: 72703 RVA: 0x002F19B9 File Offset: 0x002EFBB9
		internal override int ElementTypeId
		{
			get
			{
				return 10427;
			}
		}

		// Token: 0x06011C00 RID: 72704 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C02 RID: 72706 RVA: 0x002F19C0 File Offset: 0x002EFBC0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SecondPiePoint>(deep);
		}

		// Token: 0x04007C62 RID: 31842
		private const string tagName = "secondPiePt";

		// Token: 0x04007C63 RID: 31843
		private const byte tagNsId = 11;

		// Token: 0x04007C64 RID: 31844
		internal const int ElementTypeIdConst = 10427;
	}
}
