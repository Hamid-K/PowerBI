using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E66 RID: 11878
	[GeneratedCode("DomGen", "2.0")]
	internal class FootnoteReferenceMark : EmptyType
	{
		// Token: 0x17008A73 RID: 35443
		// (get) Token: 0x060193C7 RID: 103367 RVA: 0x00347AE3 File Offset: 0x00345CE3
		public override string LocalName
		{
			get
			{
				return "footnoteRef";
			}
		}

		// Token: 0x17008A74 RID: 35444
		// (get) Token: 0x060193C8 RID: 103368 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A75 RID: 35445
		// (get) Token: 0x060193C9 RID: 103369 RVA: 0x00347AEA File Offset: 0x00345CEA
		internal override int ElementTypeId
		{
			get
			{
				return 11557;
			}
		}

		// Token: 0x060193CA RID: 103370 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193CC RID: 103372 RVA: 0x00347AF1 File Offset: 0x00345CF1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FootnoteReferenceMark>(deep);
		}

		// Token: 0x0400A7CA RID: 42954
		private const string tagName = "footnoteRef";

		// Token: 0x0400A7CB RID: 42955
		private const byte tagNsId = 23;

		// Token: 0x0400A7CC RID: 42956
		internal const int ElementTypeIdConst = 11557;
	}
}
