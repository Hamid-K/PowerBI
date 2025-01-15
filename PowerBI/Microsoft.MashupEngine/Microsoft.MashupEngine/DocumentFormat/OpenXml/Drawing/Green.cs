using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026EA RID: 9962
	[GeneratedCode("DomGen", "2.0")]
	internal class Green : PercentageType
	{
		// Token: 0x17005DE6 RID: 24038
		// (get) Token: 0x06012FF8 RID: 77816 RVA: 0x003018FC File Offset: 0x002FFAFC
		public override string LocalName
		{
			get
			{
				return "green";
			}
		}

		// Token: 0x17005DE7 RID: 24039
		// (get) Token: 0x06012FF9 RID: 77817 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DE8 RID: 24040
		// (get) Token: 0x06012FFA RID: 77818 RVA: 0x00301903 File Offset: 0x002FFB03
		internal override int ElementTypeId
		{
			get
			{
				return 10026;
			}
		}

		// Token: 0x06012FFB RID: 77819 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FFD RID: 77821 RVA: 0x0030190A File Offset: 0x002FFB0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Green>(deep);
		}

		// Token: 0x04008428 RID: 33832
		private const string tagName = "green";

		// Token: 0x04008429 RID: 33833
		private const byte tagNsId = 10;

		// Token: 0x0400842A RID: 33834
		internal const int ElementTypeIdConst = 10026;
	}
}
