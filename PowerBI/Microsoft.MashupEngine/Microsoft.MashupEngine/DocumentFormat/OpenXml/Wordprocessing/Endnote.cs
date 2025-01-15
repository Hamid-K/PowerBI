using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC6 RID: 12230
	[GeneratedCode("DomGen", "2.0")]
	internal class Endnote : FootnoteEndnoteType
	{
		// Token: 0x17009404 RID: 37892
		// (get) Token: 0x0601A87E RID: 108670 RVA: 0x0035D8DB File Offset: 0x0035BADB
		public override string LocalName
		{
			get
			{
				return "endnote";
			}
		}

		// Token: 0x17009405 RID: 37893
		// (get) Token: 0x0601A87F RID: 108671 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009406 RID: 37894
		// (get) Token: 0x0601A880 RID: 108672 RVA: 0x00363C28 File Offset: 0x00361E28
		internal override int ElementTypeId
		{
			get
			{
				return 11938;
			}
		}

		// Token: 0x0601A881 RID: 108673 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A882 RID: 108674 RVA: 0x00363BFC File Offset: 0x00361DFC
		public Endnote()
		{
		}

		// Token: 0x0601A883 RID: 108675 RVA: 0x00363C04 File Offset: 0x00361E04
		public Endnote(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A884 RID: 108676 RVA: 0x00363C0D File Offset: 0x00361E0D
		public Endnote(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A885 RID: 108677 RVA: 0x00363C16 File Offset: 0x00361E16
		public Endnote(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A886 RID: 108678 RVA: 0x00363C2F File Offset: 0x00361E2F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Endnote>(deep);
		}

		// Token: 0x0400AD5C RID: 44380
		private const string tagName = "endnote";

		// Token: 0x0400AD5D RID: 44381
		private const byte tagNsId = 23;

		// Token: 0x0400AD5E RID: 44382
		internal const int ElementTypeIdConst = 11938;
	}
}
