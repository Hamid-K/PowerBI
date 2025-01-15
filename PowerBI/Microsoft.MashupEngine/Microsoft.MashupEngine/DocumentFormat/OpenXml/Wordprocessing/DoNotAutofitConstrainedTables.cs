using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E18 RID: 11800
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotAutofitConstrainedTables : OnOffType
	{
		// Token: 0x170088E4 RID: 35044
		// (get) Token: 0x06019088 RID: 102536 RVA: 0x00345935 File Offset: 0x00343B35
		public override string LocalName
		{
			get
			{
				return "doNotAutofitConstrainedTables";
			}
		}

		// Token: 0x170088E5 RID: 35045
		// (get) Token: 0x06019089 RID: 102537 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088E6 RID: 35046
		// (get) Token: 0x0601908A RID: 102538 RVA: 0x0034593C File Offset: 0x00343B3C
		internal override int ElementTypeId
		{
			get
			{
				return 12110;
			}
		}

		// Token: 0x0601908B RID: 102539 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601908D RID: 102541 RVA: 0x00345943 File Offset: 0x00343B43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotAutofitConstrainedTables>(deep);
		}

		// Token: 0x0400A6B3 RID: 42675
		private const string tagName = "doNotAutofitConstrainedTables";

		// Token: 0x0400A6B4 RID: 42676
		private const byte tagNsId = 23;

		// Token: 0x0400A6B5 RID: 42677
		internal const int ElementTypeIdConst = 12110;
	}
}
