using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E62 RID: 11874
	[GeneratedCode("DomGen", "2.0")]
	internal class DayLong : EmptyType
	{
		// Token: 0x17008A67 RID: 35431
		// (get) Token: 0x060193AF RID: 103343 RVA: 0x00347A87 File Offset: 0x00345C87
		public override string LocalName
		{
			get
			{
				return "dayLong";
			}
		}

		// Token: 0x17008A68 RID: 35432
		// (get) Token: 0x060193B0 RID: 103344 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A69 RID: 35433
		// (get) Token: 0x060193B1 RID: 103345 RVA: 0x00347A8E File Offset: 0x00345C8E
		internal override int ElementTypeId
		{
			get
			{
				return 11553;
			}
		}

		// Token: 0x060193B2 RID: 103346 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193B4 RID: 103348 RVA: 0x00347A95 File Offset: 0x00345C95
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DayLong>(deep);
		}

		// Token: 0x0400A7BE RID: 42942
		private const string tagName = "dayLong";

		// Token: 0x0400A7BF RID: 42943
		private const byte tagNsId = 23;

		// Token: 0x0400A7C0 RID: 42944
		internal const int ElementTypeIdConst = 11553;
	}
}
