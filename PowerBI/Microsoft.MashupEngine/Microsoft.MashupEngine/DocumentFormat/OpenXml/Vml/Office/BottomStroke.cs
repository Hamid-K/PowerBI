using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002210 RID: 8720
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomStroke : StrokeChildType
	{
		// Token: 0x170038ED RID: 14573
		// (get) Token: 0x0600DF73 RID: 57203 RVA: 0x002BF3AD File Offset: 0x002BD5AD
		public override string LocalName
		{
			get
			{
				return "bottom";
			}
		}

		// Token: 0x170038EE RID: 14574
		// (get) Token: 0x0600DF74 RID: 57204 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038EF RID: 14575
		// (get) Token: 0x0600DF75 RID: 57205 RVA: 0x002BF3B4 File Offset: 0x002BD5B4
		internal override int ElementTypeId
		{
			get
			{
				return 12413;
			}
		}

		// Token: 0x0600DF76 RID: 57206 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DF78 RID: 57208 RVA: 0x002BF3BB File Offset: 0x002BD5BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomStroke>(deep);
		}

		// Token: 0x04006D98 RID: 28056
		private const string tagName = "bottom";

		// Token: 0x04006D99 RID: 28057
		private const byte tagNsId = 27;

		// Token: 0x04006D9A RID: 28058
		internal const int ElementTypeIdConst = 12413;
	}
}
