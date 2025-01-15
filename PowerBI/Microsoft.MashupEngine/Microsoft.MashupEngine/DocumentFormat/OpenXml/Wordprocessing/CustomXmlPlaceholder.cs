using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D56 RID: 11606
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlPlaceholder : StringType
	{
		// Token: 0x1700869E RID: 34462
		// (get) Token: 0x06018BFB RID: 101371 RVA: 0x003447E7 File Offset: 0x003429E7
		public override string LocalName
		{
			get
			{
				return "placeholder";
			}
		}

		// Token: 0x1700869F RID: 34463
		// (get) Token: 0x06018BFC RID: 101372 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086A0 RID: 34464
		// (get) Token: 0x06018BFD RID: 101373 RVA: 0x003447EE File Offset: 0x003429EE
		internal override int ElementTypeId
		{
			get
			{
				return 11778;
			}
		}

		// Token: 0x06018BFE RID: 101374 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C00 RID: 101376 RVA: 0x003447F5 File Offset: 0x003429F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlPlaceholder>(deep);
		}

		// Token: 0x0400A46E RID: 42094
		private const string tagName = "placeholder";

		// Token: 0x0400A46F RID: 42095
		private const byte tagNsId = 23;

		// Token: 0x0400A470 RID: 42096
		internal const int ElementTypeIdConst = 11778;
	}
}
