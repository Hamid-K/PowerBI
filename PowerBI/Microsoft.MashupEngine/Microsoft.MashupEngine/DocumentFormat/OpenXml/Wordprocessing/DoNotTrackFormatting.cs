using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC3 RID: 11715
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotTrackFormatting : OnOffType
	{
		// Token: 0x170087E5 RID: 34789
		// (get) Token: 0x06018E8A RID: 102026 RVA: 0x00345192 File Offset: 0x00343392
		public override string LocalName
		{
			get
			{
				return "doNotTrackFormatting";
			}
		}

		// Token: 0x170087E6 RID: 34790
		// (get) Token: 0x06018E8B RID: 102027 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087E7 RID: 34791
		// (get) Token: 0x06018E8C RID: 102028 RVA: 0x00345199 File Offset: 0x00343399
		internal override int ElementTypeId
		{
			get
			{
				return 11991;
			}
		}

		// Token: 0x06018E8D RID: 102029 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E8F RID: 102031 RVA: 0x003451A0 File Offset: 0x003433A0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotTrackFormatting>(deep);
		}

		// Token: 0x0400A5B4 RID: 42420
		private const string tagName = "doNotTrackFormatting";

		// Token: 0x0400A5B5 RID: 42421
		private const byte tagNsId = 23;

		// Token: 0x0400A5B6 RID: 42422
		internal const int ElementTypeIdConst = 11991;
	}
}
