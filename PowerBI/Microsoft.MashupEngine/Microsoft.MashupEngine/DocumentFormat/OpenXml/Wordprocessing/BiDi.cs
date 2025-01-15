using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D76 RID: 11638
	[GeneratedCode("DomGen", "2.0")]
	internal class BiDi : OnOffType
	{
		// Token: 0x170086FE RID: 34558
		// (get) Token: 0x06018CBC RID: 101564 RVA: 0x00344AED File Offset: 0x00342CED
		public override string LocalName
		{
			get
			{
				return "bidi";
			}
		}

		// Token: 0x170086FF RID: 34559
		// (get) Token: 0x06018CBD RID: 101565 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008700 RID: 34560
		// (get) Token: 0x06018CBE RID: 101566 RVA: 0x00344AF4 File Offset: 0x00342CF4
		internal override int ElementTypeId
		{
			get
			{
				return 11510;
			}
		}

		// Token: 0x06018CBF RID: 101567 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CC1 RID: 101569 RVA: 0x00344AFB File Offset: 0x00342CFB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BiDi>(deep);
		}

		// Token: 0x0400A4CD RID: 42189
		private const string tagName = "bidi";

		// Token: 0x0400A4CE RID: 42190
		private const byte tagNsId = 23;

		// Token: 0x0400A4CF RID: 42191
		internal const int ElementTypeIdConst = 11510;
	}
}
