using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA8 RID: 11944
	[GeneratedCode("DomGen", "2.0")]
	internal class TopLeftToBottomRightCellBorder : BorderType
	{
		// Token: 0x17008B93 RID: 35731
		// (get) Token: 0x06019618 RID: 103960 RVA: 0x0030E432 File Offset: 0x0030C632
		public override string LocalName
		{
			get
			{
				return "tl2br";
			}
		}

		// Token: 0x17008B94 RID: 35732
		// (get) Token: 0x06019619 RID: 103961 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B95 RID: 35733
		// (get) Token: 0x0601961A RID: 103962 RVA: 0x003490D8 File Offset: 0x003472D8
		internal override int ElementTypeId
		{
			get
			{
				return 12136;
			}
		}

		// Token: 0x0601961B RID: 103963 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601961D RID: 103965 RVA: 0x003490DF File Offset: 0x003472DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopLeftToBottomRightCellBorder>(deep);
		}

		// Token: 0x0400A8AE RID: 43182
		private const string tagName = "tl2br";

		// Token: 0x0400A8AF RID: 43183
		private const byte tagNsId = 23;

		// Token: 0x0400A8B0 RID: 43184
		internal const int ElementTypeIdConst = 12136;
	}
}
