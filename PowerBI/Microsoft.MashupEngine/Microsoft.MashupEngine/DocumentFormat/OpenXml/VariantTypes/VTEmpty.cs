using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002916 RID: 10518
	[GeneratedCode("DomGen", "2.0")]
	internal class VTEmpty : OpenXmlLeafElement
	{
		// Token: 0x17006A9D RID: 27293
		// (get) Token: 0x06014D27 RID: 85287 RVA: 0x00317DE8 File Offset: 0x00315FE8
		public override string LocalName
		{
			get
			{
				return "empty";
			}
		}

		// Token: 0x17006A9E RID: 27294
		// (get) Token: 0x06014D28 RID: 85288 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A9F RID: 27295
		// (get) Token: 0x06014D29 RID: 85289 RVA: 0x00317DEF File Offset: 0x00315FEF
		internal override int ElementTypeId
		{
			get
			{
				return 10968;
			}
		}

		// Token: 0x06014D2A RID: 85290 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D2C RID: 85292 RVA: 0x00317DF6 File Offset: 0x00315FF6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTEmpty>(deep);
		}

		// Token: 0x04008FF4 RID: 36852
		private const string tagName = "empty";

		// Token: 0x04008FF5 RID: 36853
		private const byte tagNsId = 5;

		// Token: 0x04008FF6 RID: 36854
		internal const int ElementTypeIdConst = 10968;
	}
}
