using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200220F RID: 8719
	[GeneratedCode("DomGen", "2.0")]
	internal class RightStroke : StrokeChildType
	{
		// Token: 0x170038EA RID: 14570
		// (get) Token: 0x0600DF6D RID: 57197 RVA: 0x002BF396 File Offset: 0x002BD596
		public override string LocalName
		{
			get
			{
				return "right";
			}
		}

		// Token: 0x170038EB RID: 14571
		// (get) Token: 0x0600DF6E RID: 57198 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038EC RID: 14572
		// (get) Token: 0x0600DF6F RID: 57199 RVA: 0x002BF39D File Offset: 0x002BD59D
		internal override int ElementTypeId
		{
			get
			{
				return 12412;
			}
		}

		// Token: 0x0600DF70 RID: 57200 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DF72 RID: 57202 RVA: 0x002BF3A4 File Offset: 0x002BD5A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightStroke>(deep);
		}

		// Token: 0x04006D95 RID: 28053
		private const string tagName = "right";

		// Token: 0x04006D96 RID: 28054
		private const byte tagNsId = 27;

		// Token: 0x04006D97 RID: 28055
		internal const int ElementTypeIdConst = 12412;
	}
}
