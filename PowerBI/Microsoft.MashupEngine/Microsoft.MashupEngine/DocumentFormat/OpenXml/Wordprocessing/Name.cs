using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D59 RID: 11609
	[GeneratedCode("DomGen", "2.0")]
	internal class Name : StringType
	{
		// Token: 0x170086A7 RID: 34471
		// (get) Token: 0x06018C0D RID: 101389 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x170086A8 RID: 34472
		// (get) Token: 0x06018C0E RID: 101390 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086A9 RID: 34473
		// (get) Token: 0x06018C0F RID: 101391 RVA: 0x0034482C File Offset: 0x00342A2C
		internal override int ElementTypeId
		{
			get
			{
				return 11801;
			}
		}

		// Token: 0x06018C10 RID: 101392 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C12 RID: 101394 RVA: 0x00344833 File Offset: 0x00342A33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Name>(deep);
		}

		// Token: 0x0400A477 RID: 42103
		private const string tagName = "name";

		// Token: 0x0400A478 RID: 42104
		private const byte tagNsId = 23;

		// Token: 0x0400A479 RID: 42105
		internal const int ElementTypeIdConst = 11801;
	}
}
