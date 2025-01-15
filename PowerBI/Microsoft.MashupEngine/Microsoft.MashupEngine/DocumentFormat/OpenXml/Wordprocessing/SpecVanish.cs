using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D91 RID: 11665
	[GeneratedCode("DomGen", "2.0")]
	internal class SpecVanish : OnOffType
	{
		// Token: 0x1700874F RID: 34639
		// (get) Token: 0x06018D5E RID: 101726 RVA: 0x00344D29 File Offset: 0x00342F29
		public override string LocalName
		{
			get
			{
				return "specVanish";
			}
		}

		// Token: 0x17008750 RID: 34640
		// (get) Token: 0x06018D5F RID: 101727 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008751 RID: 34641
		// (get) Token: 0x06018D60 RID: 101728 RVA: 0x00344D30 File Offset: 0x00342F30
		internal override int ElementTypeId
		{
			get
			{
				return 11610;
			}
		}

		// Token: 0x06018D61 RID: 101729 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D63 RID: 101731 RVA: 0x00344D37 File Offset: 0x00342F37
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpecVanish>(deep);
		}

		// Token: 0x0400A51E RID: 42270
		private const string tagName = "specVanish";

		// Token: 0x0400A51F RID: 42271
		private const byte tagNsId = 23;

		// Token: 0x0400A520 RID: 42272
		internal const int ElementTypeIdConst = 11610;
	}
}
