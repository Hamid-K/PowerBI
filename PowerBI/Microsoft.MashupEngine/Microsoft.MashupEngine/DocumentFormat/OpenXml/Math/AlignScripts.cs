using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002983 RID: 10627
	[GeneratedCode("DomGen", "2.0")]
	internal class AlignScripts : OnOffType
	{
		// Token: 0x17006C94 RID: 27796
		// (get) Token: 0x060151B6 RID: 86454 RVA: 0x0031B62A File Offset: 0x0031982A
		public override string LocalName
		{
			get
			{
				return "alnScr";
			}
		}

		// Token: 0x17006C95 RID: 27797
		// (get) Token: 0x060151B7 RID: 86455 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C96 RID: 27798
		// (get) Token: 0x060151B8 RID: 86456 RVA: 0x0031B631 File Offset: 0x00319831
		internal override int ElementTypeId
		{
			get
			{
				return 10940;
			}
		}

		// Token: 0x060151B9 RID: 86457 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151BB RID: 86459 RVA: 0x0031B638 File Offset: 0x00319838
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlignScripts>(deep);
		}

		// Token: 0x04009195 RID: 37269
		private const string tagName = "alnScr";

		// Token: 0x04009196 RID: 37270
		private const byte tagNsId = 21;

		// Token: 0x04009197 RID: 37271
		internal const int ElementTypeIdConst = 10940;
	}
}
