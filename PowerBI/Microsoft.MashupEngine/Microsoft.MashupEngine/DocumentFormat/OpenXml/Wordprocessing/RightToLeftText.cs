using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D8F RID: 11663
	[GeneratedCode("DomGen", "2.0")]
	internal class RightToLeftText : OnOffType
	{
		// Token: 0x17008749 RID: 34633
		// (get) Token: 0x06018D52 RID: 101714 RVA: 0x0030FEE7 File Offset: 0x0030E0E7
		public override string LocalName
		{
			get
			{
				return "rtl";
			}
		}

		// Token: 0x1700874A RID: 34634
		// (get) Token: 0x06018D53 RID: 101715 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700874B RID: 34635
		// (get) Token: 0x06018D54 RID: 101716 RVA: 0x00344D09 File Offset: 0x00342F09
		internal override int ElementTypeId
		{
			get
			{
				return 11605;
			}
		}

		// Token: 0x06018D55 RID: 101717 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D57 RID: 101719 RVA: 0x00344D10 File Offset: 0x00342F10
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightToLeftText>(deep);
		}

		// Token: 0x0400A518 RID: 42264
		private const string tagName = "rtl";

		// Token: 0x0400A519 RID: 42265
		private const byte tagNsId = 23;

		// Token: 0x0400A51A RID: 42266
		internal const int ElementTypeIdConst = 11605;
	}
}
