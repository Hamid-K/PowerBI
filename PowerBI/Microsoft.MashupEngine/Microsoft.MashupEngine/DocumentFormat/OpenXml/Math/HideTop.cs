using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200296F RID: 10607
	[GeneratedCode("DomGen", "2.0")]
	internal class HideTop : OnOffType
	{
		// Token: 0x17006C58 RID: 27736
		// (get) Token: 0x0601513E RID: 86334 RVA: 0x0031B45E File Offset: 0x0031965E
		public override string LocalName
		{
			get
			{
				return "hideTop";
			}
		}

		// Token: 0x17006C59 RID: 27737
		// (get) Token: 0x0601513F RID: 86335 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C5A RID: 27738
		// (get) Token: 0x06015140 RID: 86336 RVA: 0x0031B465 File Offset: 0x00319665
		internal override int ElementTypeId
		{
			get
			{
				return 10880;
			}
		}

		// Token: 0x06015141 RID: 86337 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015143 RID: 86339 RVA: 0x0031B46C File Offset: 0x0031966C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideTop>(deep);
		}

		// Token: 0x04009159 RID: 37209
		private const string tagName = "hideTop";

		// Token: 0x0400915A RID: 37210
		private const byte tagNsId = 21;

		// Token: 0x0400915B RID: 37211
		internal const int ElementTypeIdConst = 10880;
	}
}
