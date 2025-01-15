using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026ED RID: 9965
	[GeneratedCode("DomGen", "2.0")]
	internal class Blue : PercentageType
	{
		// Token: 0x17005DEF RID: 24047
		// (get) Token: 0x0601300A RID: 77834 RVA: 0x00301941 File Offset: 0x002FFB41
		public override string LocalName
		{
			get
			{
				return "blue";
			}
		}

		// Token: 0x17005DF0 RID: 24048
		// (get) Token: 0x0601300B RID: 77835 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DF1 RID: 24049
		// (get) Token: 0x0601300C RID: 77836 RVA: 0x00301948 File Offset: 0x002FFB48
		internal override int ElementTypeId
		{
			get
			{
				return 10029;
			}
		}

		// Token: 0x0601300D RID: 77837 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601300F RID: 77839 RVA: 0x0030194F File Offset: 0x002FFB4F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Blue>(deep);
		}

		// Token: 0x04008431 RID: 33841
		private const string tagName = "blue";

		// Token: 0x04008432 RID: 33842
		private const byte tagNsId = 10;

		// Token: 0x04008433 RID: 33843
		internal const int ElementTypeIdConst = 10029;
	}
}
