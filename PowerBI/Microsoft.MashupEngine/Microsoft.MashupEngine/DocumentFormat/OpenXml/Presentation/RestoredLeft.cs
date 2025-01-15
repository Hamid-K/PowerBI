using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A7C RID: 10876
	[GeneratedCode("DomGen", "2.0")]
	internal class RestoredLeft : NormalViewPortionType
	{
		// Token: 0x1700733F RID: 29503
		// (get) Token: 0x06016078 RID: 90232 RVA: 0x00325DBB File Offset: 0x00323FBB
		public override string LocalName
		{
			get
			{
				return "restoredLeft";
			}
		}

		// Token: 0x17007340 RID: 29504
		// (get) Token: 0x06016079 RID: 90233 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007341 RID: 29505
		// (get) Token: 0x0601607A RID: 90234 RVA: 0x00325DC2 File Offset: 0x00323FC2
		internal override int ElementTypeId
		{
			get
			{
				return 12291;
			}
		}

		// Token: 0x0601607B RID: 90235 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601607D RID: 90237 RVA: 0x00325DD1 File Offset: 0x00323FD1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RestoredLeft>(deep);
		}

		// Token: 0x040095DE RID: 38366
		private const string tagName = "restoredLeft";

		// Token: 0x040095DF RID: 38367
		private const byte tagNsId = 24;

		// Token: 0x040095E0 RID: 38368
		internal const int ElementTypeIdConst = 12291;
	}
}
