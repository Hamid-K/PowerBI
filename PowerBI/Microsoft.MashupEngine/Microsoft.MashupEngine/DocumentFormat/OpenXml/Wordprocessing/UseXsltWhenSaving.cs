using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD9 RID: 11737
	[GeneratedCode("DomGen", "2.0")]
	internal class UseXsltWhenSaving : OnOffType
	{
		// Token: 0x17008827 RID: 34855
		// (get) Token: 0x06018F0E RID: 102158 RVA: 0x0034538C File Offset: 0x0034358C
		public override string LocalName
		{
			get
			{
				return "useXSLTWhenSaving";
			}
		}

		// Token: 0x17008828 RID: 34856
		// (get) Token: 0x06018F0F RID: 102159 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008829 RID: 34857
		// (get) Token: 0x06018F10 RID: 102160 RVA: 0x00345393 File Offset: 0x00343593
		internal override int ElementTypeId
		{
			get
			{
				return 12030;
			}
		}

		// Token: 0x06018F11 RID: 102161 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F13 RID: 102163 RVA: 0x0034539A File Offset: 0x0034359A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseXsltWhenSaving>(deep);
		}

		// Token: 0x0400A5F6 RID: 42486
		private const string tagName = "useXSLTWhenSaving";

		// Token: 0x0400A5F7 RID: 42487
		private const byte tagNsId = 23;

		// Token: 0x0400A5F8 RID: 42488
		internal const int ElementTypeIdConst = 12030;
	}
}
