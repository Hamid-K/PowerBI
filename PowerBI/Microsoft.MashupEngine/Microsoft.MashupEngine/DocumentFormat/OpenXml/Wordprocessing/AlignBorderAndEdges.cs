using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB9 RID: 11705
	[GeneratedCode("DomGen", "2.0")]
	internal class AlignBorderAndEdges : OnOffType
	{
		// Token: 0x170087C7 RID: 34759
		// (get) Token: 0x06018E4E RID: 101966 RVA: 0x003450AC File Offset: 0x003432AC
		public override string LocalName
		{
			get
			{
				return "alignBordersAndEdges";
			}
		}

		// Token: 0x170087C8 RID: 34760
		// (get) Token: 0x06018E4F RID: 101967 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087C9 RID: 34761
		// (get) Token: 0x06018E50 RID: 101968 RVA: 0x003450B3 File Offset: 0x003432B3
		internal override int ElementTypeId
		{
			get
			{
				return 11973;
			}
		}

		// Token: 0x06018E51 RID: 101969 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E53 RID: 101971 RVA: 0x003450BA File Offset: 0x003432BA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlignBorderAndEdges>(deep);
		}

		// Token: 0x0400A596 RID: 42390
		private const string tagName = "alignBordersAndEdges";

		// Token: 0x0400A597 RID: 42391
		private const byte tagNsId = 23;

		// Token: 0x0400A598 RID: 42392
		internal const int ElementTypeIdConst = 11973;
	}
}
