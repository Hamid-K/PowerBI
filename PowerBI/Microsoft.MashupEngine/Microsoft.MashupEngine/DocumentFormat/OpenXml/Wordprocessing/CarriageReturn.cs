using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E6B RID: 11883
	[GeneratedCode("DomGen", "2.0")]
	internal class CarriageReturn : EmptyType
	{
		// Token: 0x17008A82 RID: 35458
		// (get) Token: 0x060193E5 RID: 103397 RVA: 0x00347B4F File Offset: 0x00345D4F
		public override string LocalName
		{
			get
			{
				return "cr";
			}
		}

		// Token: 0x17008A83 RID: 35459
		// (get) Token: 0x060193E6 RID: 103398 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A84 RID: 35460
		// (get) Token: 0x060193E7 RID: 103399 RVA: 0x00347B56 File Offset: 0x00345D56
		internal override int ElementTypeId
		{
			get
			{
				return 11563;
			}
		}

		// Token: 0x060193E8 RID: 103400 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193EA RID: 103402 RVA: 0x00347B5D File Offset: 0x00345D5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CarriageReturn>(deep);
		}

		// Token: 0x0400A7D9 RID: 42969
		private const string tagName = "cr";

		// Token: 0x0400A7DA RID: 42970
		private const byte tagNsId = 23;

		// Token: 0x0400A7DB RID: 42971
		internal const int ElementTypeIdConst = 11563;
	}
}
