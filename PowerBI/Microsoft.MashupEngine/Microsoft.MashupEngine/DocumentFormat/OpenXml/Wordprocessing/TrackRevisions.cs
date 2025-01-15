using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC1 RID: 11713
	[GeneratedCode("DomGen", "2.0")]
	internal class TrackRevisions : OnOffType
	{
		// Token: 0x170087DF RID: 34783
		// (get) Token: 0x06018E7E RID: 102014 RVA: 0x00345164 File Offset: 0x00343364
		public override string LocalName
		{
			get
			{
				return "trackRevisions";
			}
		}

		// Token: 0x170087E0 RID: 34784
		// (get) Token: 0x06018E7F RID: 102015 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087E1 RID: 34785
		// (get) Token: 0x06018E80 RID: 102016 RVA: 0x0034516B File Offset: 0x0034336B
		internal override int ElementTypeId
		{
			get
			{
				return 11989;
			}
		}

		// Token: 0x06018E81 RID: 102017 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E83 RID: 102019 RVA: 0x00345172 File Offset: 0x00343372
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TrackRevisions>(deep);
		}

		// Token: 0x0400A5AE RID: 42414
		private const string tagName = "trackRevisions";

		// Token: 0x0400A5AF RID: 42415
		private const byte tagNsId = 23;

		// Token: 0x0400A5B0 RID: 42416
		internal const int ElementTypeIdConst = 11989;
	}
}
