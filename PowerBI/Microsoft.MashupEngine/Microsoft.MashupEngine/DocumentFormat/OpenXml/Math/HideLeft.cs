using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002971 RID: 10609
	[GeneratedCode("DomGen", "2.0")]
	internal class HideLeft : OnOffType
	{
		// Token: 0x17006C5E RID: 27742
		// (get) Token: 0x0601514A RID: 86346 RVA: 0x0031B48C File Offset: 0x0031968C
		public override string LocalName
		{
			get
			{
				return "hideLeft";
			}
		}

		// Token: 0x17006C5F RID: 27743
		// (get) Token: 0x0601514B RID: 86347 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C60 RID: 27744
		// (get) Token: 0x0601514C RID: 86348 RVA: 0x0031B493 File Offset: 0x00319693
		internal override int ElementTypeId
		{
			get
			{
				return 10882;
			}
		}

		// Token: 0x0601514D RID: 86349 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601514F RID: 86351 RVA: 0x0031B49A File Offset: 0x0031969A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideLeft>(deep);
		}

		// Token: 0x0400915F RID: 37215
		private const string tagName = "hideLeft";

		// Token: 0x04009160 RID: 37216
		private const byte tagNsId = 21;

		// Token: 0x04009161 RID: 37217
		internal const int ElementTypeIdConst = 10882;
	}
}
