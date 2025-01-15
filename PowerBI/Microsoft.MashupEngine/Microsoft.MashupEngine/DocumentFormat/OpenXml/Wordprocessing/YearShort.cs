using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E61 RID: 11873
	[GeneratedCode("DomGen", "2.0")]
	internal class YearShort : EmptyType
	{
		// Token: 0x17008A64 RID: 35428
		// (get) Token: 0x060193A9 RID: 103337 RVA: 0x00347A70 File Offset: 0x00345C70
		public override string LocalName
		{
			get
			{
				return "yearShort";
			}
		}

		// Token: 0x17008A65 RID: 35429
		// (get) Token: 0x060193AA RID: 103338 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A66 RID: 35430
		// (get) Token: 0x060193AB RID: 103339 RVA: 0x00347A77 File Offset: 0x00345C77
		internal override int ElementTypeId
		{
			get
			{
				return 11552;
			}
		}

		// Token: 0x060193AC RID: 103340 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193AE RID: 103342 RVA: 0x00347A7E File Offset: 0x00345C7E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<YearShort>(deep);
		}

		// Token: 0x0400A7BB RID: 42939
		private const string tagName = "yearShort";

		// Token: 0x0400A7BC RID: 42940
		private const byte tagNsId = 23;

		// Token: 0x0400A7BD RID: 42941
		internal const int ElementTypeIdConst = 11552;
	}
}
