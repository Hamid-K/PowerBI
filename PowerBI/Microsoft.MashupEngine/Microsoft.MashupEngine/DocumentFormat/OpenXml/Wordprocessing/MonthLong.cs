using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E63 RID: 11875
	[GeneratedCode("DomGen", "2.0")]
	internal class MonthLong : EmptyType
	{
		// Token: 0x17008A6A RID: 35434
		// (get) Token: 0x060193B5 RID: 103349 RVA: 0x00347A9E File Offset: 0x00345C9E
		public override string LocalName
		{
			get
			{
				return "monthLong";
			}
		}

		// Token: 0x17008A6B RID: 35435
		// (get) Token: 0x060193B6 RID: 103350 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A6C RID: 35436
		// (get) Token: 0x060193B7 RID: 103351 RVA: 0x00347AA5 File Offset: 0x00345CA5
		internal override int ElementTypeId
		{
			get
			{
				return 11554;
			}
		}

		// Token: 0x060193B8 RID: 103352 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193BA RID: 103354 RVA: 0x00347AAC File Offset: 0x00345CAC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MonthLong>(deep);
		}

		// Token: 0x0400A7C1 RID: 42945
		private const string tagName = "monthLong";

		// Token: 0x0400A7C2 RID: 42946
		private const byte tagNsId = 23;

		// Token: 0x0400A7C3 RID: 42947
		internal const int ElementTypeIdConst = 11554;
	}
}
