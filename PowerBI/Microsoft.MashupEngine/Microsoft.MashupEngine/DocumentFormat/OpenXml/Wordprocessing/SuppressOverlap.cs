using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D7B RID: 11643
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressOverlap : OnOffType
	{
		// Token: 0x1700870D RID: 34573
		// (get) Token: 0x06018CDA RID: 101594 RVA: 0x00344B60 File Offset: 0x00342D60
		public override string LocalName
		{
			get
			{
				return "suppressOverlap";
			}
		}

		// Token: 0x1700870E RID: 34574
		// (get) Token: 0x06018CDB RID: 101595 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700870F RID: 34575
		// (get) Token: 0x06018CDC RID: 101596 RVA: 0x00344B67 File Offset: 0x00342D67
		internal override int ElementTypeId
		{
			get
			{
				return 11517;
			}
		}

		// Token: 0x06018CDD RID: 101597 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CDF RID: 101599 RVA: 0x00344B6E File Offset: 0x00342D6E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressOverlap>(deep);
		}

		// Token: 0x0400A4DC RID: 42204
		private const string tagName = "suppressOverlap";

		// Token: 0x0400A4DD RID: 42205
		private const byte tagNsId = 23;

		// Token: 0x0400A4DE RID: 42206
		internal const int ElementTypeIdConst = 11517;
	}
}
