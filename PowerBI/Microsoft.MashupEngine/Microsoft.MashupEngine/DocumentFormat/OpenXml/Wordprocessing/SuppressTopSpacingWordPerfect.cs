using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF6 RID: 11766
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressTopSpacingWordPerfect : OnOffType
	{
		// Token: 0x1700887E RID: 34942
		// (get) Token: 0x06018FBC RID: 102332 RVA: 0x00345627 File Offset: 0x00343827
		public override string LocalName
		{
			get
			{
				return "suppressTopSpacingWP";
			}
		}

		// Token: 0x1700887F RID: 34943
		// (get) Token: 0x06018FBD RID: 102333 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008880 RID: 34944
		// (get) Token: 0x06018FBE RID: 102334 RVA: 0x0034562E File Offset: 0x0034382E
		internal override int ElementTypeId
		{
			get
			{
				return 12076;
			}
		}

		// Token: 0x06018FBF RID: 102335 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FC1 RID: 102337 RVA: 0x00345635 File Offset: 0x00343835
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressTopSpacingWordPerfect>(deep);
		}

		// Token: 0x0400A64D RID: 42573
		private const string tagName = "suppressTopSpacingWP";

		// Token: 0x0400A64E RID: 42574
		private const byte tagNsId = 23;

		// Token: 0x0400A64F RID: 42575
		internal const int ElementTypeIdConst = 12076;
	}
}
