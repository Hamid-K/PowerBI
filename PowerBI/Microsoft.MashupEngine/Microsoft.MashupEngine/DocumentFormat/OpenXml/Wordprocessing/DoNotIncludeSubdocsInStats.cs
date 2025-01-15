using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DDE RID: 11742
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotIncludeSubdocsInStats : OnOffType
	{
		// Token: 0x17008836 RID: 34870
		// (get) Token: 0x06018F2C RID: 102188 RVA: 0x003453FF File Offset: 0x003435FF
		public override string LocalName
		{
			get
			{
				return "doNotIncludeSubdocsInStats";
			}
		}

		// Token: 0x17008837 RID: 34871
		// (get) Token: 0x06018F2D RID: 102189 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008838 RID: 34872
		// (get) Token: 0x06018F2E RID: 102190 RVA: 0x00345406 File Offset: 0x00343606
		internal override int ElementTypeId
		{
			get
			{
				return 12045;
			}
		}

		// Token: 0x06018F2F RID: 102191 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F31 RID: 102193 RVA: 0x0034540D File Offset: 0x0034360D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotIncludeSubdocsInStats>(deep);
		}

		// Token: 0x0400A605 RID: 42501
		private const string tagName = "doNotIncludeSubdocsInStats";

		// Token: 0x0400A606 RID: 42502
		private const byte tagNsId = 23;

		// Token: 0x0400A607 RID: 42503
		internal const int ElementTypeIdConst = 12045;
	}
}
