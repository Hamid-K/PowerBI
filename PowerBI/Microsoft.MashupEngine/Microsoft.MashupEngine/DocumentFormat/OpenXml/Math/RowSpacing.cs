using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A7 RID: 10663
	[GeneratedCode("DomGen", "2.0")]
	internal class RowSpacing : UnsignedShortType
	{
		// Token: 0x17006D3A RID: 27962
		// (get) Token: 0x06015333 RID: 86835 RVA: 0x0031CCA8 File Offset: 0x0031AEA8
		public override string LocalName
		{
			get
			{
				return "rSp";
			}
		}

		// Token: 0x17006D3B RID: 27963
		// (get) Token: 0x06015334 RID: 86836 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D3C RID: 27964
		// (get) Token: 0x06015335 RID: 86837 RVA: 0x0031CCAF File Offset: 0x0031AEAF
		internal override int ElementTypeId
		{
			get
			{
				return 10899;
			}
		}

		// Token: 0x06015336 RID: 86838 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015338 RID: 86840 RVA: 0x0031CCBE File Offset: 0x0031AEBE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowSpacing>(deep);
		}

		// Token: 0x04009210 RID: 37392
		private const string tagName = "rSp";

		// Token: 0x04009211 RID: 37393
		private const byte tagNsId = 21;

		// Token: 0x04009212 RID: 37394
		internal const int ElementTypeIdConst = 10899;
	}
}
