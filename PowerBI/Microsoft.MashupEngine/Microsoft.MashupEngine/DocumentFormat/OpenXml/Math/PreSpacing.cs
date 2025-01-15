using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B8 RID: 10680
	[GeneratedCode("DomGen", "2.0")]
	internal class PreSpacing : TwipsMeasureType
	{
		// Token: 0x17006DA0 RID: 28064
		// (get) Token: 0x06015410 RID: 87056 RVA: 0x0031D4D1 File Offset: 0x0031B6D1
		public override string LocalName
		{
			get
			{
				return "preSp";
			}
		}

		// Token: 0x17006DA1 RID: 28065
		// (get) Token: 0x06015411 RID: 87057 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DA2 RID: 28066
		// (get) Token: 0x06015412 RID: 87058 RVA: 0x0031D4D8 File Offset: 0x0031B6D8
		internal override int ElementTypeId
		{
			get
			{
				return 10954;
			}
		}

		// Token: 0x06015413 RID: 87059 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015415 RID: 87061 RVA: 0x0031D4DF File Offset: 0x0031B6DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreSpacing>(deep);
		}

		// Token: 0x04009258 RID: 37464
		private const string tagName = "preSp";

		// Token: 0x04009259 RID: 37465
		private const byte tagNsId = 21;

		// Token: 0x0400925A RID: 37466
		internal const int ElementTypeIdConst = 10954;
	}
}
