using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DEB RID: 11755
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotExpandShiftReturn : OnOffType
	{
		// Token: 0x1700885D RID: 34909
		// (get) Token: 0x06018F7A RID: 102266 RVA: 0x0034552A File Offset: 0x0034372A
		public override string LocalName
		{
			get
			{
				return "doNotExpandShiftReturn";
			}
		}

		// Token: 0x1700885E RID: 34910
		// (get) Token: 0x06018F7B RID: 102267 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700885F RID: 34911
		// (get) Token: 0x06018F7C RID: 102268 RVA: 0x00345531 File Offset: 0x00343731
		internal override int ElementTypeId
		{
			get
			{
				return 12065;
			}
		}

		// Token: 0x06018F7D RID: 102269 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F7F RID: 102271 RVA: 0x00345538 File Offset: 0x00343738
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotExpandShiftReturn>(deep);
		}

		// Token: 0x0400A62C RID: 42540
		private const string tagName = "doNotExpandShiftReturn";

		// Token: 0x0400A62D RID: 42541
		private const byte tagNsId = 23;

		// Token: 0x0400A62E RID: 42542
		internal const int ElementTypeIdConst = 12065;
	}
}
