using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002750 RID: 10064
	[GeneratedCode("DomGen", "2.0")]
	internal class NoBullet : OpenXmlLeafElement
	{
		// Token: 0x17006097 RID: 24727
		// (get) Token: 0x060135DF RID: 79327 RVA: 0x003065A2 File Offset: 0x003047A2
		public override string LocalName
		{
			get
			{
				return "buNone";
			}
		}

		// Token: 0x17006098 RID: 24728
		// (get) Token: 0x060135E0 RID: 79328 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006099 RID: 24729
		// (get) Token: 0x060135E1 RID: 79329 RVA: 0x003065A9 File Offset: 0x003047A9
		internal override int ElementTypeId
		{
			get
			{
				return 10109;
			}
		}

		// Token: 0x060135E2 RID: 79330 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060135E4 RID: 79332 RVA: 0x003065B0 File Offset: 0x003047B0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoBullet>(deep);
		}

		// Token: 0x040085E1 RID: 34273
		private const string tagName = "buNone";

		// Token: 0x040085E2 RID: 34274
		private const byte tagNsId = 10;

		// Token: 0x040085E3 RID: 34275
		internal const int ElementTypeIdConst = 10109;
	}
}
