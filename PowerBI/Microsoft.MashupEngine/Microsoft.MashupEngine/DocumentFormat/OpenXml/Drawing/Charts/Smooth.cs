using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002520 RID: 9504
	[GeneratedCode("DomGen", "2.0")]
	internal class Smooth : BooleanType
	{
		// Token: 0x17005479 RID: 21625
		// (get) Token: 0x06011AED RID: 72429 RVA: 0x002F1409 File Offset: 0x002EF609
		public override string LocalName
		{
			get
			{
				return "smooth";
			}
		}

		// Token: 0x1700547A RID: 21626
		// (get) Token: 0x06011AEE RID: 72430 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700547B RID: 21627
		// (get) Token: 0x06011AEF RID: 72431 RVA: 0x002F1410 File Offset: 0x002EF610
		internal override int ElementTypeId
		{
			get
			{
				return 10459;
			}
		}

		// Token: 0x06011AF0 RID: 72432 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AF2 RID: 72434 RVA: 0x002F1417 File Offset: 0x002EF617
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Smooth>(deep);
		}

		// Token: 0x04007BF4 RID: 31732
		private const string tagName = "smooth";

		// Token: 0x04007BF5 RID: 31733
		private const byte tagNsId = 11;

		// Token: 0x04007BF6 RID: 31734
		internal const int ElementTypeIdConst = 10459;
	}
}
