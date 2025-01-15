using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002707 RID: 9991
	[GeneratedCode("DomGen", "2.0")]
	internal class EffectDag : EffectContainerType
	{
		// Token: 0x17005EA3 RID: 24227
		// (get) Token: 0x0601319F RID: 78239 RVA: 0x00303BB1 File Offset: 0x00301DB1
		public override string LocalName
		{
			get
			{
				return "effectDag";
			}
		}

		// Token: 0x17005EA4 RID: 24228
		// (get) Token: 0x060131A0 RID: 78240 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EA5 RID: 24229
		// (get) Token: 0x060131A1 RID: 78241 RVA: 0x00303BB8 File Offset: 0x00301DB8
		internal override int ElementTypeId
		{
			get
			{
				return 10084;
			}
		}

		// Token: 0x060131A2 RID: 78242 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060131A3 RID: 78243 RVA: 0x00303B85 File Offset: 0x00301D85
		public EffectDag()
		{
		}

		// Token: 0x060131A4 RID: 78244 RVA: 0x00303B8D File Offset: 0x00301D8D
		public EffectDag(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060131A5 RID: 78245 RVA: 0x00303B96 File Offset: 0x00301D96
		public EffectDag(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060131A6 RID: 78246 RVA: 0x00303B9F File Offset: 0x00301D9F
		public EffectDag(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060131A7 RID: 78247 RVA: 0x00303BBF File Offset: 0x00301DBF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectDag>(deep);
		}

		// Token: 0x040084AA RID: 33962
		private const string tagName = "effectDag";

		// Token: 0x040084AB RID: 33963
		private const byte tagNsId = 10;

		// Token: 0x040084AC RID: 33964
		internal const int ElementTypeIdConst = 10084;
	}
}
