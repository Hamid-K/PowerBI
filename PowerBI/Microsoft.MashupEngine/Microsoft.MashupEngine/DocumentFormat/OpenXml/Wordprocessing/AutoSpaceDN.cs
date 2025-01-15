using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D75 RID: 11637
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoSpaceDN : OnOffType
	{
		// Token: 0x170086FB RID: 34555
		// (get) Token: 0x06018CB6 RID: 101558 RVA: 0x00344AD6 File Offset: 0x00342CD6
		public override string LocalName
		{
			get
			{
				return "autoSpaceDN";
			}
		}

		// Token: 0x170086FC RID: 34556
		// (get) Token: 0x06018CB7 RID: 101559 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086FD RID: 34557
		// (get) Token: 0x06018CB8 RID: 101560 RVA: 0x00344ADD File Offset: 0x00342CDD
		internal override int ElementTypeId
		{
			get
			{
				return 11509;
			}
		}

		// Token: 0x06018CB9 RID: 101561 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CBB RID: 101563 RVA: 0x00344AE4 File Offset: 0x00342CE4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoSpaceDN>(deep);
		}

		// Token: 0x0400A4CA RID: 42186
		private const string tagName = "autoSpaceDN";

		// Token: 0x0400A4CB RID: 42187
		private const byte tagNsId = 23;

		// Token: 0x0400A4CC RID: 42188
		internal const int ElementTypeIdConst = 11509;
	}
}
