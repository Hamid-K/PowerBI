using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D4E RID: 11598
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlMoveToRangeEnd : MarkupType
	{
		// Token: 0x17008686 RID: 34438
		// (get) Token: 0x06018BCA RID: 101322 RVA: 0x003446D9 File Offset: 0x003428D9
		public override string LocalName
		{
			get
			{
				return "customXmlMoveToRangeEnd";
			}
		}

		// Token: 0x17008687 RID: 34439
		// (get) Token: 0x06018BCB RID: 101323 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008688 RID: 34440
		// (get) Token: 0x06018BCC RID: 101324 RVA: 0x003446E0 File Offset: 0x003428E0
		internal override int ElementTypeId
		{
			get
			{
				return 11491;
			}
		}

		// Token: 0x06018BCD RID: 101325 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BCF RID: 101327 RVA: 0x003446E7 File Offset: 0x003428E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlMoveToRangeEnd>(deep);
		}

		// Token: 0x0400A457 RID: 42071
		private const string tagName = "customXmlMoveToRangeEnd";

		// Token: 0x0400A458 RID: 42072
		private const byte tagNsId = 23;

		// Token: 0x0400A459 RID: 42073
		internal const int ElementTypeIdConst = 11491;
	}
}
