using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF3 RID: 12019
	[GeneratedCode("DomGen", "2.0")]
	internal class FlatBorders : OnOffOnlyType
	{
		// Token: 0x17008D8D RID: 36237
		// (get) Token: 0x06019A6A RID: 105066 RVA: 0x00353937 File Offset: 0x00351B37
		public override string LocalName
		{
			get
			{
				return "flatBorders";
			}
		}

		// Token: 0x17008D8E RID: 36238
		// (get) Token: 0x06019A6B RID: 105067 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D8F RID: 36239
		// (get) Token: 0x06019A6C RID: 105068 RVA: 0x0035393E File Offset: 0x00351B3E
		internal override int ElementTypeId
		{
			get
			{
				return 11858;
			}
		}

		// Token: 0x06019A6D RID: 105069 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A6F RID: 105071 RVA: 0x00353945 File Offset: 0x00351B45
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FlatBorders>(deep);
		}

		// Token: 0x0400A9E6 RID: 43494
		private const string tagName = "flatBorders";

		// Token: 0x0400A9E7 RID: 43495
		private const byte tagNsId = 23;

		// Token: 0x0400A9E8 RID: 43496
		internal const int ElementTypeIdConst = 11858;
	}
}
