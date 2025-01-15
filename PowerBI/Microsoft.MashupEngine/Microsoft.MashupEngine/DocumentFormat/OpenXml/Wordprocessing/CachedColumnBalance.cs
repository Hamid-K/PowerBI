using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E21 RID: 11809
	[GeneratedCode("DomGen", "2.0")]
	internal class CachedColumnBalance : OnOffType
	{
		// Token: 0x170088FF RID: 35071
		// (get) Token: 0x060190BE RID: 102590 RVA: 0x00345A04 File Offset: 0x00343C04
		public override string LocalName
		{
			get
			{
				return "cachedColBalance";
			}
		}

		// Token: 0x17008900 RID: 35072
		// (get) Token: 0x060190BF RID: 102591 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008901 RID: 35073
		// (get) Token: 0x060190C0 RID: 102592 RVA: 0x00345A0B File Offset: 0x00343C0B
		internal override int ElementTypeId
		{
			get
			{
				return 12119;
			}
		}

		// Token: 0x060190C1 RID: 102593 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190C3 RID: 102595 RVA: 0x00345A12 File Offset: 0x00343C12
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CachedColumnBalance>(deep);
		}

		// Token: 0x0400A6CE RID: 42702
		private const string tagName = "cachedColBalance";

		// Token: 0x0400A6CF RID: 42703
		private const byte tagNsId = 23;

		// Token: 0x0400A6D0 RID: 42704
		internal const int ElementTypeIdConst = 12119;
	}
}
