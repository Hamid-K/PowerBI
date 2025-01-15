using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD2 RID: 11730
	[GeneratedCode("DomGen", "2.0")]
	internal class SavePreviewPicture : OnOffType
	{
		// Token: 0x17008812 RID: 34834
		// (get) Token: 0x06018EE4 RID: 102116 RVA: 0x003452EB File Offset: 0x003434EB
		public override string LocalName
		{
			get
			{
				return "savePreviewPicture";
			}
		}

		// Token: 0x17008813 RID: 34835
		// (get) Token: 0x06018EE5 RID: 102117 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008814 RID: 34836
		// (get) Token: 0x06018EE6 RID: 102118 RVA: 0x003452F2 File Offset: 0x003434F2
		internal override int ElementTypeId
		{
			get
			{
				return 12023;
			}
		}

		// Token: 0x06018EE7 RID: 102119 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EE9 RID: 102121 RVA: 0x003452F9 File Offset: 0x003434F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SavePreviewPicture>(deep);
		}

		// Token: 0x0400A5E1 RID: 42465
		private const string tagName = "savePreviewPicture";

		// Token: 0x0400A5E2 RID: 42466
		private const byte tagNsId = 23;

		// Token: 0x0400A5E3 RID: 42467
		internal const int ElementTypeIdConst = 12023;
	}
}
