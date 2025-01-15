using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D87 RID: 11655
	[GeneratedCode("DomGen", "2.0")]
	internal class DoubleStrike : OnOffType
	{
		// Token: 0x17008731 RID: 34609
		// (get) Token: 0x06018D22 RID: 101666 RVA: 0x00344C5F File Offset: 0x00342E5F
		public override string LocalName
		{
			get
			{
				return "dstrike";
			}
		}

		// Token: 0x17008732 RID: 34610
		// (get) Token: 0x06018D23 RID: 101667 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008733 RID: 34611
		// (get) Token: 0x06018D24 RID: 101668 RVA: 0x00344C66 File Offset: 0x00342E66
		internal override int ElementTypeId
		{
			get
			{
				return 11584;
			}
		}

		// Token: 0x06018D25 RID: 101669 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D27 RID: 101671 RVA: 0x00344C6D File Offset: 0x00342E6D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoubleStrike>(deep);
		}

		// Token: 0x0400A500 RID: 42240
		private const string tagName = "dstrike";

		// Token: 0x0400A501 RID: 42241
		private const byte tagNsId = 23;

		// Token: 0x0400A502 RID: 42242
		internal const int ElementTypeIdConst = 11584;
	}
}
