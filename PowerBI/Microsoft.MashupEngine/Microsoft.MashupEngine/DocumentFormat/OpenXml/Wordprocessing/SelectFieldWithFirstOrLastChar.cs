using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E0C RID: 11788
	[GeneratedCode("DomGen", "2.0")]
	internal class SelectFieldWithFirstOrLastChar : OnOffType
	{
		// Token: 0x170088C0 RID: 35008
		// (get) Token: 0x06019040 RID: 102464 RVA: 0x00345821 File Offset: 0x00343A21
		public override string LocalName
		{
			get
			{
				return "selectFldWithFirstOrLastChar";
			}
		}

		// Token: 0x170088C1 RID: 35009
		// (get) Token: 0x06019041 RID: 102465 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088C2 RID: 35010
		// (get) Token: 0x06019042 RID: 102466 RVA: 0x00345828 File Offset: 0x00343A28
		internal override int ElementTypeId
		{
			get
			{
				return 12098;
			}
		}

		// Token: 0x06019043 RID: 102467 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019045 RID: 102469 RVA: 0x0034582F File Offset: 0x00343A2F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SelectFieldWithFirstOrLastChar>(deep);
		}

		// Token: 0x0400A68F RID: 42639
		private const string tagName = "selectFldWithFirstOrLastChar";

		// Token: 0x0400A690 RID: 42640
		private const byte tagNsId = 23;

		// Token: 0x0400A691 RID: 42641
		internal const int ElementTypeIdConst = 12098;
	}
}
