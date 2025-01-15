using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002976 RID: 10614
	[GeneratedCode("DomGen", "2.0")]
	internal class StrikeTopLeftToBottomRight : OnOffType
	{
		// Token: 0x17006C6D RID: 27757
		// (get) Token: 0x06015168 RID: 86376 RVA: 0x0031B4FF File Offset: 0x003196FF
		public override string LocalName
		{
			get
			{
				return "strikeTLBR";
			}
		}

		// Token: 0x17006C6E RID: 27758
		// (get) Token: 0x06015169 RID: 86377 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C6F RID: 27759
		// (get) Token: 0x0601516A RID: 86378 RVA: 0x0031B506 File Offset: 0x00319706
		internal override int ElementTypeId
		{
			get
			{
				return 10887;
			}
		}

		// Token: 0x0601516B RID: 86379 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601516D RID: 86381 RVA: 0x0031B50D File Offset: 0x0031970D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StrikeTopLeftToBottomRight>(deep);
		}

		// Token: 0x0400916E RID: 37230
		private const string tagName = "strikeTLBR";

		// Token: 0x0400916F RID: 37231
		private const byte tagNsId = 21;

		// Token: 0x04009170 RID: 37232
		internal const int ElementTypeIdConst = 10887;
	}
}
