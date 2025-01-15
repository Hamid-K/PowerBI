using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB8 RID: 11704
	[GeneratedCode("DomGen", "2.0")]
	internal class MirrorMargins : OnOffType
	{
		// Token: 0x170087C4 RID: 34756
		// (get) Token: 0x06018E48 RID: 101960 RVA: 0x00345095 File Offset: 0x00343295
		public override string LocalName
		{
			get
			{
				return "mirrorMargins";
			}
		}

		// Token: 0x170087C5 RID: 34757
		// (get) Token: 0x06018E49 RID: 101961 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087C6 RID: 34758
		// (get) Token: 0x06018E4A RID: 101962 RVA: 0x0034509C File Offset: 0x0034329C
		internal override int ElementTypeId
		{
			get
			{
				return 11972;
			}
		}

		// Token: 0x06018E4B RID: 101963 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E4D RID: 101965 RVA: 0x003450A3 File Offset: 0x003432A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MirrorMargins>(deep);
		}

		// Token: 0x0400A593 RID: 42387
		private const string tagName = "mirrorMargins";

		// Token: 0x0400A594 RID: 42388
		private const byte tagNsId = 23;

		// Token: 0x0400A595 RID: 42389
		internal const int ElementTypeIdConst = 11972;
	}
}
