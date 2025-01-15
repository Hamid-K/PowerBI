using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC8 RID: 11720
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotHyphenateCaps : OnOffType
	{
		// Token: 0x170087F4 RID: 34804
		// (get) Token: 0x06018EA8 RID: 102056 RVA: 0x00345205 File Offset: 0x00343405
		public override string LocalName
		{
			get
			{
				return "doNotHyphenateCaps";
			}
		}

		// Token: 0x170087F5 RID: 34805
		// (get) Token: 0x06018EA9 RID: 102057 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087F6 RID: 34806
		// (get) Token: 0x06018EAA RID: 102058 RVA: 0x0034520C File Offset: 0x0034340C
		internal override int ElementTypeId
		{
			get
			{
				return 12000;
			}
		}

		// Token: 0x06018EAB RID: 102059 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EAD RID: 102061 RVA: 0x00345213 File Offset: 0x00343413
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotHyphenateCaps>(deep);
		}

		// Token: 0x0400A5C3 RID: 42435
		private const string tagName = "doNotHyphenateCaps";

		// Token: 0x0400A5C4 RID: 42436
		private const byte tagNsId = 23;

		// Token: 0x0400A5C5 RID: 42437
		internal const int ElementTypeIdConst = 12000;
	}
}
