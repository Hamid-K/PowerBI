using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC7 RID: 11719
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoHyphenation : OnOffType
	{
		// Token: 0x170087F1 RID: 34801
		// (get) Token: 0x06018EA2 RID: 102050 RVA: 0x003451EE File Offset: 0x003433EE
		public override string LocalName
		{
			get
			{
				return "autoHyphenation";
			}
		}

		// Token: 0x170087F2 RID: 34802
		// (get) Token: 0x06018EA3 RID: 102051 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087F3 RID: 34803
		// (get) Token: 0x06018EA4 RID: 102052 RVA: 0x003451F5 File Offset: 0x003433F5
		internal override int ElementTypeId
		{
			get
			{
				return 11997;
			}
		}

		// Token: 0x06018EA5 RID: 102053 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EA7 RID: 102055 RVA: 0x003451FC File Offset: 0x003433FC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoHyphenation>(deep);
		}

		// Token: 0x0400A5C0 RID: 42432
		private const string tagName = "autoHyphenation";

		// Token: 0x0400A5C1 RID: 42433
		private const byte tagNsId = 23;

		// Token: 0x0400A5C2 RID: 42434
		internal const int ElementTypeIdConst = 11997;
	}
}
