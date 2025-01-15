using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D89 RID: 11657
	[GeneratedCode("DomGen", "2.0")]
	internal class Shadow : OnOffType
	{
		// Token: 0x17008737 RID: 34615
		// (get) Token: 0x06018D2E RID: 101678 RVA: 0x002C0C98 File Offset: 0x002BEE98
		public override string LocalName
		{
			get
			{
				return "shadow";
			}
		}

		// Token: 0x17008738 RID: 34616
		// (get) Token: 0x06018D2F RID: 101679 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008739 RID: 34617
		// (get) Token: 0x06018D30 RID: 101680 RVA: 0x00344C86 File Offset: 0x00342E86
		internal override int ElementTypeId
		{
			get
			{
				return 11586;
			}
		}

		// Token: 0x06018D31 RID: 101681 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D33 RID: 101683 RVA: 0x00344C8D File Offset: 0x00342E8D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shadow>(deep);
		}

		// Token: 0x0400A506 RID: 42246
		private const string tagName = "shadow";

		// Token: 0x0400A507 RID: 42247
		private const byte tagNsId = 23;

		// Token: 0x0400A508 RID: 42248
		internal const int ElementTypeIdConst = 11586;
	}
}
