using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D9B RID: 11675
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartUnique : OnOffType
	{
		// Token: 0x1700876D RID: 34669
		// (get) Token: 0x06018D9A RID: 101786 RVA: 0x00344E01 File Offset: 0x00343001
		public override string LocalName
		{
			get
			{
				return "docPartUnique";
			}
		}

		// Token: 0x1700876E RID: 34670
		// (get) Token: 0x06018D9B RID: 101787 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700876F RID: 34671
		// (get) Token: 0x06018D9C RID: 101788 RVA: 0x00344E08 File Offset: 0x00343008
		internal override int ElementTypeId
		{
			get
			{
				return 11767;
			}
		}

		// Token: 0x06018D9D RID: 101789 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D9F RID: 101791 RVA: 0x00344E0F File Offset: 0x0034300F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartUnique>(deep);
		}

		// Token: 0x0400A53C RID: 42300
		private const string tagName = "docPartUnique";

		// Token: 0x0400A53D RID: 42301
		private const byte tagNsId = 23;

		// Token: 0x0400A53E RID: 42302
		internal const int ElementTypeIdConst = 11767;
	}
}
