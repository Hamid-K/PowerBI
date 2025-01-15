using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF2 RID: 12018
	[GeneratedCode("DomGen", "2.0")]
	internal class NoBorder : OnOffOnlyType
	{
		// Token: 0x17008D8A RID: 36234
		// (get) Token: 0x06019A64 RID: 105060 RVA: 0x00353920 File Offset: 0x00351B20
		public override string LocalName
		{
			get
			{
				return "noBorder";
			}
		}

		// Token: 0x17008D8B RID: 36235
		// (get) Token: 0x06019A65 RID: 105061 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D8C RID: 36236
		// (get) Token: 0x06019A66 RID: 105062 RVA: 0x00353927 File Offset: 0x00351B27
		internal override int ElementTypeId
		{
			get
			{
				return 11857;
			}
		}

		// Token: 0x06019A67 RID: 105063 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A69 RID: 105065 RVA: 0x0035392E File Offset: 0x00351B2E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoBorder>(deep);
		}

		// Token: 0x0400A9E3 RID: 43491
		private const string tagName = "noBorder";

		// Token: 0x0400A9E4 RID: 43492
		private const byte tagNsId = 23;

		// Token: 0x0400A9E5 RID: 43493
		internal const int ElementTypeIdConst = 11857;
	}
}
