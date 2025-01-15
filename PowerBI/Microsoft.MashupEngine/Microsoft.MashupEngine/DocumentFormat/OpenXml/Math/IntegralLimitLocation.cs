using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C2 RID: 10690
	[GeneratedCode("DomGen", "2.0")]
	internal class IntegralLimitLocation : LimitLocationType
	{
		// Token: 0x17006DCA RID: 28106
		// (get) Token: 0x0601546F RID: 87151 RVA: 0x0031D833 File Offset: 0x0031BA33
		public override string LocalName
		{
			get
			{
				return "intLim";
			}
		}

		// Token: 0x17006DCB RID: 28107
		// (get) Token: 0x06015470 RID: 87152 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DCC RID: 28108
		// (get) Token: 0x06015471 RID: 87153 RVA: 0x0031D83A File Offset: 0x0031BA3A
		internal override int ElementTypeId
		{
			get
			{
				return 10960;
			}
		}

		// Token: 0x06015472 RID: 87154 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015474 RID: 87156 RVA: 0x0031D841 File Offset: 0x0031BA41
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IntegralLimitLocation>(deep);
		}

		// Token: 0x04009277 RID: 37495
		private const string tagName = "intLim";

		// Token: 0x04009278 RID: 37496
		private const byte tagNsId = 21;

		// Token: 0x04009279 RID: 37497
		internal const int ElementTypeIdConst = 10960;
	}
}
