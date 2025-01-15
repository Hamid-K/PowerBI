using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E23 RID: 11811
	[GeneratedCode("DomGen", "2.0")]
	internal class TemporarySdt : OnOffType
	{
		// Token: 0x17008905 RID: 35077
		// (get) Token: 0x060190CA RID: 102602 RVA: 0x00345A32 File Offset: 0x00343C32
		public override string LocalName
		{
			get
			{
				return "temporary";
			}
		}

		// Token: 0x17008906 RID: 35078
		// (get) Token: 0x060190CB RID: 102603 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008907 RID: 35079
		// (get) Token: 0x060190CC RID: 102604 RVA: 0x00345A39 File Offset: 0x00343C39
		internal override int ElementTypeId
		{
			get
			{
				return 12144;
			}
		}

		// Token: 0x060190CD RID: 102605 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190CF RID: 102607 RVA: 0x00345A40 File Offset: 0x00343C40
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TemporarySdt>(deep);
		}

		// Token: 0x0400A6D4 RID: 42708
		private const string tagName = "temporary";

		// Token: 0x0400A6D5 RID: 42709
		private const byte tagNsId = 23;

		// Token: 0x0400A6D6 RID: 42710
		internal const int ElementTypeIdConst = 12144;
	}
}
