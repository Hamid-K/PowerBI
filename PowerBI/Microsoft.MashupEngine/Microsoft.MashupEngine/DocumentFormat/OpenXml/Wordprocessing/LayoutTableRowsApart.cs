using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E08 RID: 11784
	[GeneratedCode("DomGen", "2.0")]
	internal class LayoutTableRowsApart : OnOffType
	{
		// Token: 0x170088B4 RID: 34996
		// (get) Token: 0x06019028 RID: 102440 RVA: 0x003457C5 File Offset: 0x003439C5
		public override string LocalName
		{
			get
			{
				return "layoutTableRowsApart";
			}
		}

		// Token: 0x170088B5 RID: 34997
		// (get) Token: 0x06019029 RID: 102441 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088B6 RID: 34998
		// (get) Token: 0x0601902A RID: 102442 RVA: 0x003457CC File Offset: 0x003439CC
		internal override int ElementTypeId
		{
			get
			{
				return 12094;
			}
		}

		// Token: 0x0601902B RID: 102443 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601902D RID: 102445 RVA: 0x003457D3 File Offset: 0x003439D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutTableRowsApart>(deep);
		}

		// Token: 0x0400A683 RID: 42627
		private const string tagName = "layoutTableRowsApart";

		// Token: 0x0400A684 RID: 42628
		private const byte tagNsId = 23;

		// Token: 0x0400A685 RID: 42629
		internal const int ElementTypeIdConst = 12094;
	}
}
