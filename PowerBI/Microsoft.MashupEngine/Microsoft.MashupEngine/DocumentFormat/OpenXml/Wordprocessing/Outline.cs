using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D88 RID: 11656
	[GeneratedCode("DomGen", "2.0")]
	internal class Outline : OnOffType
	{
		// Token: 0x17008734 RID: 34612
		// (get) Token: 0x06018D28 RID: 101672 RVA: 0x00333400 File Offset: 0x00331600
		public override string LocalName
		{
			get
			{
				return "outline";
			}
		}

		// Token: 0x17008735 RID: 34613
		// (get) Token: 0x06018D29 RID: 101673 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008736 RID: 34614
		// (get) Token: 0x06018D2A RID: 101674 RVA: 0x00344C76 File Offset: 0x00342E76
		internal override int ElementTypeId
		{
			get
			{
				return 11585;
			}
		}

		// Token: 0x06018D2B RID: 101675 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D2D RID: 101677 RVA: 0x00344C7D File Offset: 0x00342E7D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Outline>(deep);
		}

		// Token: 0x0400A503 RID: 42243
		private const string tagName = "outline";

		// Token: 0x0400A504 RID: 42244
		private const byte tagNsId = 23;

		// Token: 0x0400A505 RID: 42245
		internal const int ElementTypeIdConst = 11585;
	}
}
