using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002986 RID: 10630
	[GeneratedCode("DomGen", "2.0")]
	internal class WrapRight : OnOffType
	{
		// Token: 0x17006C9D RID: 27805
		// (get) Token: 0x060151C8 RID: 86472 RVA: 0x0031B66F File Offset: 0x0031986F
		public override string LocalName
		{
			get
			{
				return "wrapRight";
			}
		}

		// Token: 0x17006C9E RID: 27806
		// (get) Token: 0x060151C9 RID: 86473 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C9F RID: 27807
		// (get) Token: 0x060151CA RID: 86474 RVA: 0x0031B676 File Offset: 0x00319876
		internal override int ElementTypeId
		{
			get
			{
				return 10959;
			}
		}

		// Token: 0x060151CB RID: 86475 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151CD RID: 86477 RVA: 0x0031B67D File Offset: 0x0031987D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapRight>(deep);
		}

		// Token: 0x0400919E RID: 37278
		private const string tagName = "wrapRight";

		// Token: 0x0400919F RID: 37279
		private const byte tagNsId = 21;

		// Token: 0x040091A0 RID: 37280
		internal const int ElementTypeIdConst = 10959;
	}
}
