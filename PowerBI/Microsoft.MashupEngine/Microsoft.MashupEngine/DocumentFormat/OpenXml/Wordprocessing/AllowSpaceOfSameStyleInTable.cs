using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E16 RID: 11798
	[GeneratedCode("DomGen", "2.0")]
	internal class AllowSpaceOfSameStyleInTable : OnOffType
	{
		// Token: 0x170088DE RID: 35038
		// (get) Token: 0x0601907C RID: 102524 RVA: 0x00345907 File Offset: 0x00343B07
		public override string LocalName
		{
			get
			{
				return "allowSpaceOfSameStyleInTable";
			}
		}

		// Token: 0x170088DF RID: 35039
		// (get) Token: 0x0601907D RID: 102525 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088E0 RID: 35040
		// (get) Token: 0x0601907E RID: 102526 RVA: 0x0034590E File Offset: 0x00343B0E
		internal override int ElementTypeId
		{
			get
			{
				return 12108;
			}
		}

		// Token: 0x0601907F RID: 102527 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019081 RID: 102529 RVA: 0x00345915 File Offset: 0x00343B15
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AllowSpaceOfSameStyleInTable>(deep);
		}

		// Token: 0x0400A6AD RID: 42669
		private const string tagName = "allowSpaceOfSameStyleInTable";

		// Token: 0x0400A6AE RID: 42670
		private const byte tagNsId = 23;

		// Token: 0x0400A6AF RID: 42671
		internal const int ElementTypeIdConst = 12108;
	}
}
