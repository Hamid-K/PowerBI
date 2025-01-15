using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC6 RID: 11718
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleLockStylesPart : OnOffType
	{
		// Token: 0x170087EE RID: 34798
		// (get) Token: 0x06018E9C RID: 102044 RVA: 0x003451D7 File Offset: 0x003433D7
		public override string LocalName
		{
			get
			{
				return "styleLockQFSet";
			}
		}

		// Token: 0x170087EF RID: 34799
		// (get) Token: 0x06018E9D RID: 102045 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087F0 RID: 34800
		// (get) Token: 0x06018E9E RID: 102046 RVA: 0x003451DE File Offset: 0x003433DE
		internal override int ElementTypeId
		{
			get
			{
				return 11995;
			}
		}

		// Token: 0x06018E9F RID: 102047 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EA1 RID: 102049 RVA: 0x003451E5 File Offset: 0x003433E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleLockStylesPart>(deep);
		}

		// Token: 0x0400A5BD RID: 42429
		private const string tagName = "styleLockQFSet";

		// Token: 0x0400A5BE RID: 42430
		private const byte tagNsId = 23;

		// Token: 0x0400A5BF RID: 42431
		internal const int ElementTypeIdConst = 11995;
	}
}
