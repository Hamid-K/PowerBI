using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200220E RID: 8718
	[GeneratedCode("DomGen", "2.0")]
	internal class TopStroke : StrokeChildType
	{
		// Token: 0x170038E7 RID: 14567
		// (get) Token: 0x0600DF67 RID: 57191 RVA: 0x002BF37F File Offset: 0x002BD57F
		public override string LocalName
		{
			get
			{
				return "top";
			}
		}

		// Token: 0x170038E8 RID: 14568
		// (get) Token: 0x0600DF68 RID: 57192 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038E9 RID: 14569
		// (get) Token: 0x0600DF69 RID: 57193 RVA: 0x002BF386 File Offset: 0x002BD586
		internal override int ElementTypeId
		{
			get
			{
				return 12411;
			}
		}

		// Token: 0x0600DF6A RID: 57194 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DF6C RID: 57196 RVA: 0x002BF38D File Offset: 0x002BD58D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopStroke>(deep);
		}

		// Token: 0x04006D92 RID: 28050
		private const string tagName = "top";

		// Token: 0x04006D93 RID: 28051
		private const byte tagNsId = 27;

		// Token: 0x04006D94 RID: 28052
		internal const int ElementTypeIdConst = 12411;
	}
}
