using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D3B RID: 11579
	[GeneratedCode("DomGen", "2.0")]
	internal class Inserted : TrackChangeType
	{
		// Token: 0x17008638 RID: 34360
		// (get) Token: 0x06018B2B RID: 101163 RVA: 0x0034411A File Offset: 0x0034231A
		public override string LocalName
		{
			get
			{
				return "ins";
			}
		}

		// Token: 0x17008639 RID: 34361
		// (get) Token: 0x06018B2C RID: 101164 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700863A RID: 34362
		// (get) Token: 0x06018B2D RID: 101165 RVA: 0x00344121 File Offset: 0x00342321
		internal override int ElementTypeId
		{
			get
			{
				return 11686;
			}
		}

		// Token: 0x06018B2E RID: 101166 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B30 RID: 101168 RVA: 0x00344128 File Offset: 0x00342328
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Inserted>(deep);
		}

		// Token: 0x0400A41D RID: 42013
		private const string tagName = "ins";

		// Token: 0x0400A41E RID: 42014
		private const byte tagNsId = 23;

		// Token: 0x0400A41F RID: 42015
		internal const int ElementTypeIdConst = 11686;
	}
}
