using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA9 RID: 11945
	[GeneratedCode("DomGen", "2.0")]
	internal class TopRightToBottomLeftCellBorder : BorderType
	{
		// Token: 0x17008B96 RID: 35734
		// (get) Token: 0x0601961E RID: 103966 RVA: 0x0030E449 File Offset: 0x0030C649
		public override string LocalName
		{
			get
			{
				return "tr2bl";
			}
		}

		// Token: 0x17008B97 RID: 35735
		// (get) Token: 0x0601961F RID: 103967 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B98 RID: 35736
		// (get) Token: 0x06019620 RID: 103968 RVA: 0x003490E8 File Offset: 0x003472E8
		internal override int ElementTypeId
		{
			get
			{
				return 12137;
			}
		}

		// Token: 0x06019621 RID: 103969 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019623 RID: 103971 RVA: 0x003490EF File Offset: 0x003472EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopRightToBottomLeftCellBorder>(deep);
		}

		// Token: 0x0400A8B1 RID: 43185
		private const string tagName = "tr2bl";

		// Token: 0x0400A8B2 RID: 43186
		private const byte tagNsId = 23;

		// Token: 0x0400A8B3 RID: 43187
		internal const int ElementTypeIdConst = 12137;
	}
}
