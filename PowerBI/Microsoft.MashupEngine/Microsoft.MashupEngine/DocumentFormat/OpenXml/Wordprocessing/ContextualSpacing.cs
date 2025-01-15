using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D79 RID: 11641
	[GeneratedCode("DomGen", "2.0")]
	internal class ContextualSpacing : OnOffType
	{
		// Token: 0x17008707 RID: 34567
		// (get) Token: 0x06018CCE RID: 101582 RVA: 0x00344B32 File Offset: 0x00342D32
		public override string LocalName
		{
			get
			{
				return "contextualSpacing";
			}
		}

		// Token: 0x17008708 RID: 34568
		// (get) Token: 0x06018CCF RID: 101583 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008709 RID: 34569
		// (get) Token: 0x06018CD0 RID: 101584 RVA: 0x00344B39 File Offset: 0x00342D39
		internal override int ElementTypeId
		{
			get
			{
				return 11515;
			}
		}

		// Token: 0x06018CD1 RID: 101585 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CD3 RID: 101587 RVA: 0x00344B40 File Offset: 0x00342D40
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextualSpacing>(deep);
		}

		// Token: 0x0400A4D6 RID: 42198
		private const string tagName = "contextualSpacing";

		// Token: 0x0400A4D7 RID: 42199
		private const byte tagNsId = 23;

		// Token: 0x0400A4D8 RID: 42200
		internal const int ElementTypeIdConst = 11515;
	}
}
