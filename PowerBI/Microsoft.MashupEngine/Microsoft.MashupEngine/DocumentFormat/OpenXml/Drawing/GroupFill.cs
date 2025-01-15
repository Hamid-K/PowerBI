using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002704 RID: 9988
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupFill : OpenXmlLeafElement
	{
		// Token: 0x17005E99 RID: 24217
		// (get) Token: 0x06013183 RID: 78211 RVA: 0x003037F7 File Offset: 0x003019F7
		public override string LocalName
		{
			get
			{
				return "grpFill";
			}
		}

		// Token: 0x17005E9A RID: 24218
		// (get) Token: 0x06013184 RID: 78212 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E9B RID: 24219
		// (get) Token: 0x06013185 RID: 78213 RVA: 0x003037FE File Offset: 0x003019FE
		internal override int ElementTypeId
		{
			get
			{
				return 10052;
			}
		}

		// Token: 0x06013186 RID: 78214 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013188 RID: 78216 RVA: 0x00303805 File Offset: 0x00301A05
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupFill>(deep);
		}

		// Token: 0x040084A2 RID: 33954
		private const string tagName = "grpFill";

		// Token: 0x040084A3 RID: 33955
		private const byte tagNsId = 10;

		// Token: 0x040084A4 RID: 33956
		internal const int ElementTypeIdConst = 10052;
	}
}
