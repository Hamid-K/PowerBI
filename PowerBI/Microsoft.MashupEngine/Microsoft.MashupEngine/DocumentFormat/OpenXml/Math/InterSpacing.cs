using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029BA RID: 10682
	[GeneratedCode("DomGen", "2.0")]
	internal class InterSpacing : TwipsMeasureType
	{
		// Token: 0x17006DA6 RID: 28070
		// (get) Token: 0x0601541C RID: 87068 RVA: 0x0031D4FF File Offset: 0x0031B6FF
		public override string LocalName
		{
			get
			{
				return "interSp";
			}
		}

		// Token: 0x17006DA7 RID: 28071
		// (get) Token: 0x0601541D RID: 87069 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DA8 RID: 28072
		// (get) Token: 0x0601541E RID: 87070 RVA: 0x0031D506 File Offset: 0x0031B706
		internal override int ElementTypeId
		{
			get
			{
				return 10956;
			}
		}

		// Token: 0x0601541F RID: 87071 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015421 RID: 87073 RVA: 0x0031D50D File Offset: 0x0031B70D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InterSpacing>(deep);
		}

		// Token: 0x0400925E RID: 37470
		private const string tagName = "interSp";

		// Token: 0x0400925F RID: 37471
		private const byte tagNsId = 21;

		// Token: 0x04009260 RID: 37472
		internal const int ElementTypeIdConst = 10956;
	}
}
