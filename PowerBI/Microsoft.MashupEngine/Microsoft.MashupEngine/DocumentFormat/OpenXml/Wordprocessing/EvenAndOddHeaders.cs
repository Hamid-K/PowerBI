using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DCA RID: 11722
	[GeneratedCode("DomGen", "2.0")]
	internal class EvenAndOddHeaders : OnOffType
	{
		// Token: 0x170087FA RID: 34810
		// (get) Token: 0x06018EB4 RID: 102068 RVA: 0x00345233 File Offset: 0x00343433
		public override string LocalName
		{
			get
			{
				return "evenAndOddHeaders";
			}
		}

		// Token: 0x170087FB RID: 34811
		// (get) Token: 0x06018EB5 RID: 102069 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087FC RID: 34812
		// (get) Token: 0x06018EB6 RID: 102070 RVA: 0x0034523A File Offset: 0x0034343A
		internal override int ElementTypeId
		{
			get
			{
				return 12005;
			}
		}

		// Token: 0x06018EB7 RID: 102071 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EB9 RID: 102073 RVA: 0x00345241 File Offset: 0x00343441
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EvenAndOddHeaders>(deep);
		}

		// Token: 0x0400A5C9 RID: 42441
		private const string tagName = "evenAndOddHeaders";

		// Token: 0x0400A5CA RID: 42442
		private const byte tagNsId = 23;

		// Token: 0x0400A5CB RID: 42443
		internal const int ElementTypeIdConst = 12005;
	}
}
