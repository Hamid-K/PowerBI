using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ABF RID: 10943
	[GeneratedCode("DomGen", "2.0")]
	internal class RotationCenter : TimeListType
	{
		// Token: 0x17007514 RID: 29972
		// (get) Token: 0x060164AC RID: 91308 RVA: 0x00328A03 File Offset: 0x00326C03
		public override string LocalName
		{
			get
			{
				return "rCtr";
			}
		}

		// Token: 0x17007515 RID: 29973
		// (get) Token: 0x060164AD RID: 91309 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007516 RID: 29974
		// (get) Token: 0x060164AE RID: 91310 RVA: 0x00328A0A File Offset: 0x00326C0A
		internal override int ElementTypeId
		{
			get
			{
				return 12358;
			}
		}

		// Token: 0x060164AF RID: 91311 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060164B1 RID: 91313 RVA: 0x00328A11 File Offset: 0x00326C11
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RotationCenter>(deep);
		}

		// Token: 0x0400970E RID: 38670
		private const string tagName = "rCtr";

		// Token: 0x0400970F RID: 38671
		private const byte tagNsId = 24;

		// Token: 0x04009710 RID: 38672
		internal const int ElementTypeIdConst = 12358;
	}
}
