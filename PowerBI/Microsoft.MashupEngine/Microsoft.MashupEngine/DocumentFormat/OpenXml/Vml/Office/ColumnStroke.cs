using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002211 RID: 8721
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnStroke : StrokeChildType
	{
		// Token: 0x170038F0 RID: 14576
		// (get) Token: 0x0600DF79 RID: 57209 RVA: 0x002BF3C4 File Offset: 0x002BD5C4
		public override string LocalName
		{
			get
			{
				return "column";
			}
		}

		// Token: 0x170038F1 RID: 14577
		// (get) Token: 0x0600DF7A RID: 57210 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038F2 RID: 14578
		// (get) Token: 0x0600DF7B RID: 57211 RVA: 0x002BF3CB File Offset: 0x002BD5CB
		internal override int ElementTypeId
		{
			get
			{
				return 12414;
			}
		}

		// Token: 0x0600DF7C RID: 57212 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DF7E RID: 57214 RVA: 0x002BF3D2 File Offset: 0x002BD5D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnStroke>(deep);
		}

		// Token: 0x04006D9B RID: 28059
		private const string tagName = "column";

		// Token: 0x04006D9C RID: 28060
		private const byte tagNsId = 27;

		// Token: 0x04006D9D RID: 28061
		internal const int ElementTypeIdConst = 12414;
	}
}
