using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DBF RID: 11711
	[GeneratedCode("DomGen", "2.0")]
	internal class FormsDesign : OnOffType
	{
		// Token: 0x170087D9 RID: 34777
		// (get) Token: 0x06018E72 RID: 102002 RVA: 0x00345136 File Offset: 0x00343336
		public override string LocalName
		{
			get
			{
				return "formsDesign";
			}
		}

		// Token: 0x170087DA RID: 34778
		// (get) Token: 0x06018E73 RID: 102003 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087DB RID: 34779
		// (get) Token: 0x06018E74 RID: 102004 RVA: 0x0034513D File Offset: 0x0034333D
		internal override int ElementTypeId
		{
			get
			{
				return 11981;
			}
		}

		// Token: 0x06018E75 RID: 102005 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E77 RID: 102007 RVA: 0x00345144 File Offset: 0x00343344
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormsDesign>(deep);
		}

		// Token: 0x0400A5A8 RID: 42408
		private const string tagName = "formsDesign";

		// Token: 0x0400A5A9 RID: 42409
		private const byte tagNsId = 23;

		// Token: 0x0400A5AA RID: 42410
		internal const int ElementTypeIdConst = 11981;
	}
}
