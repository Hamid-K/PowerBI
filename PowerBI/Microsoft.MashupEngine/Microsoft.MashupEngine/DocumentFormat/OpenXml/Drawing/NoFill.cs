using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026FF RID: 9983
	[GeneratedCode("DomGen", "2.0")]
	internal class NoFill : OpenXmlLeafElement
	{
		// Token: 0x17005E68 RID: 24168
		// (get) Token: 0x0601311C RID: 78108 RVA: 0x002ECF97 File Offset: 0x002EB197
		public override string LocalName
		{
			get
			{
				return "noFill";
			}
		}

		// Token: 0x17005E69 RID: 24169
		// (get) Token: 0x0601311D RID: 78109 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E6A RID: 24170
		// (get) Token: 0x0601311E RID: 78110 RVA: 0x00303320 File Offset: 0x00301520
		internal override int ElementTypeId
		{
			get
			{
				return 10047;
			}
		}

		// Token: 0x0601311F RID: 78111 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013121 RID: 78113 RVA: 0x00303327 File Offset: 0x00301527
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoFill>(deep);
		}

		// Token: 0x04008485 RID: 33925
		private const string tagName = "noFill";

		// Token: 0x04008486 RID: 33926
		private const byte tagNsId = 10;

		// Token: 0x04008487 RID: 33927
		internal const int ElementTypeIdConst = 10047;
	}
}
