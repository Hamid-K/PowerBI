using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC2 RID: 11714
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotTrackMoves : OnOffType
	{
		// Token: 0x170087E2 RID: 34786
		// (get) Token: 0x06018E84 RID: 102020 RVA: 0x0034517B File Offset: 0x0034337B
		public override string LocalName
		{
			get
			{
				return "doNotTrackMoves";
			}
		}

		// Token: 0x170087E3 RID: 34787
		// (get) Token: 0x06018E85 RID: 102021 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087E4 RID: 34788
		// (get) Token: 0x06018E86 RID: 102022 RVA: 0x00345182 File Offset: 0x00343382
		internal override int ElementTypeId
		{
			get
			{
				return 11990;
			}
		}

		// Token: 0x06018E87 RID: 102023 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E89 RID: 102025 RVA: 0x00345189 File Offset: 0x00343389
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotTrackMoves>(deep);
		}

		// Token: 0x0400A5B1 RID: 42417
		private const string tagName = "doNotTrackMoves";

		// Token: 0x0400A5B2 RID: 42418
		private const byte tagNsId = 23;

		// Token: 0x0400A5B3 RID: 42419
		internal const int ElementTypeIdConst = 11990;
	}
}
