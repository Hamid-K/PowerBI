using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD4 RID: 11732
	[GeneratedCode("DomGen", "2.0")]
	internal class SaveInvalidXml : OnOffType
	{
		// Token: 0x17008818 RID: 34840
		// (get) Token: 0x06018EF0 RID: 102128 RVA: 0x00345319 File Offset: 0x00343519
		public override string LocalName
		{
			get
			{
				return "saveInvalidXml";
			}
		}

		// Token: 0x17008819 RID: 34841
		// (get) Token: 0x06018EF1 RID: 102129 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700881A RID: 34842
		// (get) Token: 0x06018EF2 RID: 102130 RVA: 0x00345320 File Offset: 0x00343520
		internal override int ElementTypeId
		{
			get
			{
				return 12025;
			}
		}

		// Token: 0x06018EF3 RID: 102131 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EF5 RID: 102133 RVA: 0x00345327 File Offset: 0x00343527
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaveInvalidXml>(deep);
		}

		// Token: 0x0400A5E7 RID: 42471
		private const string tagName = "saveInvalidXml";

		// Token: 0x0400A5E8 RID: 42472
		private const byte tagNsId = 23;

		// Token: 0x0400A5E9 RID: 42473
		internal const int ElementTypeIdConst = 12025;
	}
}
