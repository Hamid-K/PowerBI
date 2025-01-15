using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C1 RID: 10689
	[GeneratedCode("DomGen", "2.0")]
	internal class LimitLocation : LimitLocationType
	{
		// Token: 0x17006DC7 RID: 28103
		// (get) Token: 0x06015469 RID: 87145 RVA: 0x0031D814 File Offset: 0x0031BA14
		public override string LocalName
		{
			get
			{
				return "limLoc";
			}
		}

		// Token: 0x17006DC8 RID: 28104
		// (get) Token: 0x0601546A RID: 87146 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DC9 RID: 28105
		// (get) Token: 0x0601546B RID: 87147 RVA: 0x0031D81B File Offset: 0x0031BA1B
		internal override int ElementTypeId
		{
			get
			{
				return 10923;
			}
		}

		// Token: 0x0601546C RID: 87148 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601546E RID: 87150 RVA: 0x0031D82A File Offset: 0x0031BA2A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LimitLocation>(deep);
		}

		// Token: 0x04009274 RID: 37492
		private const string tagName = "limLoc";

		// Token: 0x04009275 RID: 37493
		private const byte tagNsId = 21;

		// Token: 0x04009276 RID: 37494
		internal const int ElementTypeIdConst = 10923;
	}
}
