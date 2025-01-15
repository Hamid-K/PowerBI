using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200258A RID: 9610
	[GeneratedCode("DomGen", "2.0")]
	internal class WidthMode : LayoutModeType
	{
		// Token: 0x17005650 RID: 22096
		// (get) Token: 0x06011F1D RID: 73501 RVA: 0x002F3E89 File Offset: 0x002F2089
		public override string LocalName
		{
			get
			{
				return "wMode";
			}
		}

		// Token: 0x17005651 RID: 22097
		// (get) Token: 0x06011F1E RID: 73502 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005652 RID: 22098
		// (get) Token: 0x06011F1F RID: 73503 RVA: 0x002F3E90 File Offset: 0x002F2090
		internal override int ElementTypeId
		{
			get
			{
				return 10409;
			}
		}

		// Token: 0x06011F20 RID: 73504 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011F22 RID: 73506 RVA: 0x002F3E97 File Offset: 0x002F2097
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WidthMode>(deep);
		}

		// Token: 0x04007D60 RID: 32096
		private const string tagName = "wMode";

		// Token: 0x04007D61 RID: 32097
		private const byte tagNsId = 11;

		// Token: 0x04007D62 RID: 32098
		internal const int ElementTypeIdConst = 10409;
	}
}
