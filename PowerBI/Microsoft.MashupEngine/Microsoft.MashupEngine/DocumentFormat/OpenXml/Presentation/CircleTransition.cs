using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E7 RID: 10727
	[GeneratedCode("DomGen", "2.0")]
	internal class CircleTransition : EmptyType
	{
		// Token: 0x17006E4E RID: 28238
		// (get) Token: 0x0601558B RID: 87435 RVA: 0x0031E1D7 File Offset: 0x0031C3D7
		public override string LocalName
		{
			get
			{
				return "circle";
			}
		}

		// Token: 0x17006E4F RID: 28239
		// (get) Token: 0x0601558C RID: 87436 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E50 RID: 28240
		// (get) Token: 0x0601558D RID: 87437 RVA: 0x0031E1DE File Offset: 0x0031C3DE
		internal override int ElementTypeId
		{
			get
			{
				return 12377;
			}
		}

		// Token: 0x0601558E RID: 87438 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015590 RID: 87440 RVA: 0x0031E1E5 File Offset: 0x0031C3E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CircleTransition>(deep);
		}

		// Token: 0x0400930F RID: 37647
		private const string tagName = "circle";

		// Token: 0x04009310 RID: 37648
		private const byte tagNsId = 24;

		// Token: 0x04009311 RID: 37649
		internal const int ElementTypeIdConst = 12377;
	}
}
