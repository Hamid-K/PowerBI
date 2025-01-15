using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D5A RID: 11610
	[GeneratedCode("DomGen", "2.0")]
	internal class MappedName : StringType
	{
		// Token: 0x170086AA RID: 34474
		// (get) Token: 0x06018C13 RID: 101395 RVA: 0x0034483C File Offset: 0x00342A3C
		public override string LocalName
		{
			get
			{
				return "mappedName";
			}
		}

		// Token: 0x170086AB RID: 34475
		// (get) Token: 0x06018C14 RID: 101396 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086AC RID: 34476
		// (get) Token: 0x06018C15 RID: 101397 RVA: 0x00344843 File Offset: 0x00342A43
		internal override int ElementTypeId
		{
			get
			{
				return 11802;
			}
		}

		// Token: 0x06018C16 RID: 101398 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C18 RID: 101400 RVA: 0x0034484A File Offset: 0x00342A4A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MappedName>(deep);
		}

		// Token: 0x0400A47A RID: 42106
		private const string tagName = "mappedName";

		// Token: 0x0400A47B RID: 42107
		private const byte tagNsId = 23;

		// Token: 0x0400A47C RID: 42108
		internal const int ElementTypeIdConst = 11802;
	}
}
