using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DEE RID: 11758
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintBodyTextBeforeHeader : OnOffType
	{
		// Token: 0x17008866 RID: 34918
		// (get) Token: 0x06018F8C RID: 102284 RVA: 0x0034556F File Offset: 0x0034376F
		public override string LocalName
		{
			get
			{
				return "printBodyTextBeforeHeader";
			}
		}

		// Token: 0x17008867 RID: 34919
		// (get) Token: 0x06018F8D RID: 102285 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008868 RID: 34920
		// (get) Token: 0x06018F8E RID: 102286 RVA: 0x00345576 File Offset: 0x00343776
		internal override int ElementTypeId
		{
			get
			{
				return 12068;
			}
		}

		// Token: 0x06018F8F RID: 102287 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F91 RID: 102289 RVA: 0x0034557D File Offset: 0x0034377D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintBodyTextBeforeHeader>(deep);
		}

		// Token: 0x0400A635 RID: 42549
		private const string tagName = "printBodyTextBeforeHeader";

		// Token: 0x0400A636 RID: 42550
		private const byte tagNsId = 23;

		// Token: 0x0400A637 RID: 42551
		internal const int ElementTypeIdConst = 12068;
	}
}
