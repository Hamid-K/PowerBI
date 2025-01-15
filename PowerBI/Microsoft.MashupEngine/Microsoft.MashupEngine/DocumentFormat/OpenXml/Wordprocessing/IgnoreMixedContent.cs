using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD5 RID: 11733
	[GeneratedCode("DomGen", "2.0")]
	internal class IgnoreMixedContent : OnOffType
	{
		// Token: 0x1700881B RID: 34843
		// (get) Token: 0x06018EF6 RID: 102134 RVA: 0x00345330 File Offset: 0x00343530
		public override string LocalName
		{
			get
			{
				return "ignoreMixedContent";
			}
		}

		// Token: 0x1700881C RID: 34844
		// (get) Token: 0x06018EF7 RID: 102135 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700881D RID: 34845
		// (get) Token: 0x06018EF8 RID: 102136 RVA: 0x00345337 File Offset: 0x00343537
		internal override int ElementTypeId
		{
			get
			{
				return 12026;
			}
		}

		// Token: 0x06018EF9 RID: 102137 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EFB RID: 102139 RVA: 0x0034533E File Offset: 0x0034353E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IgnoreMixedContent>(deep);
		}

		// Token: 0x0400A5EA RID: 42474
		private const string tagName = "ignoreMixedContent";

		// Token: 0x0400A5EB RID: 42475
		private const byte tagNsId = 23;

		// Token: 0x0400A5EC RID: 42476
		internal const int ElementTypeIdConst = 12026;
	}
}
