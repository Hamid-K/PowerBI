using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB7 RID: 11703
	[GeneratedCode("DomGen", "2.0")]
	internal class SaveFormsData : OnOffType
	{
		// Token: 0x170087C1 RID: 34753
		// (get) Token: 0x06018E42 RID: 101954 RVA: 0x0034507E File Offset: 0x0034327E
		public override string LocalName
		{
			get
			{
				return "saveFormsData";
			}
		}

		// Token: 0x170087C2 RID: 34754
		// (get) Token: 0x06018E43 RID: 101955 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087C3 RID: 34755
		// (get) Token: 0x06018E44 RID: 101956 RVA: 0x00345085 File Offset: 0x00343285
		internal override int ElementTypeId
		{
			get
			{
				return 11971;
			}
		}

		// Token: 0x06018E45 RID: 101957 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E47 RID: 101959 RVA: 0x0034508C File Offset: 0x0034328C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaveFormsData>(deep);
		}

		// Token: 0x0400A590 RID: 42384
		private const string tagName = "saveFormsData";

		// Token: 0x0400A591 RID: 42385
		private const byte tagNsId = 23;

		// Token: 0x0400A592 RID: 42386
		internal const int ElementTypeIdConst = 11971;
	}
}
