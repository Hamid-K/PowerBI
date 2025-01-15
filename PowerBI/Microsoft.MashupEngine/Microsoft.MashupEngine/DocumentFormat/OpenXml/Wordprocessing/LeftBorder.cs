using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E9F RID: 11935
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftBorder : BorderType
	{
		// Token: 0x17008B78 RID: 35704
		// (get) Token: 0x060195E2 RID: 103906 RVA: 0x002BF360 File Offset: 0x002BD560
		public override string LocalName
		{
			get
			{
				return "left";
			}
		}

		// Token: 0x17008B79 RID: 35705
		// (get) Token: 0x060195E3 RID: 103907 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B7A RID: 35706
		// (get) Token: 0x060195E4 RID: 103908 RVA: 0x00349041 File Offset: 0x00347241
		internal override int ElementTypeId
		{
			get
			{
				return 11716;
			}
		}

		// Token: 0x060195E5 RID: 103909 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060195E7 RID: 103911 RVA: 0x00349048 File Offset: 0x00347248
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftBorder>(deep);
		}

		// Token: 0x0400A893 RID: 43155
		private const string tagName = "left";

		// Token: 0x0400A894 RID: 43156
		private const byte tagNsId = 23;

		// Token: 0x0400A895 RID: 43157
		internal const int ElementTypeIdConst = 11716;
	}
}
