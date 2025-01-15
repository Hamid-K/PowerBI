using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D66 RID: 11622
	[GeneratedCode("DomGen", "2.0")]
	internal class ListSeparator : StringType
	{
		// Token: 0x170086CE RID: 34510
		// (get) Token: 0x06018C5B RID: 101467 RVA: 0x00344934 File Offset: 0x00342B34
		public override string LocalName
		{
			get
			{
				return "listSeparator";
			}
		}

		// Token: 0x170086CF RID: 34511
		// (get) Token: 0x06018C5C RID: 101468 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086D0 RID: 34512
		// (get) Token: 0x06018C5D RID: 101469 RVA: 0x0034493B File Offset: 0x00342B3B
		internal override int ElementTypeId
		{
			get
			{
				return 12054;
			}
		}

		// Token: 0x06018C5E RID: 101470 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C60 RID: 101472 RVA: 0x00344942 File Offset: 0x00342B42
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListSeparator>(deep);
		}

		// Token: 0x0400A49E RID: 42142
		private const string tagName = "listSeparator";

		// Token: 0x0400A49F RID: 42143
		private const byte tagNsId = 23;

		// Token: 0x0400A4A0 RID: 42144
		internal const int ElementTypeIdConst = 12054;
	}
}
