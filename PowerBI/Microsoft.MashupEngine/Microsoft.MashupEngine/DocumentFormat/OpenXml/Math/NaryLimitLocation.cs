using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C3 RID: 10691
	[GeneratedCode("DomGen", "2.0")]
	internal class NaryLimitLocation : LimitLocationType
	{
		// Token: 0x17006DCD RID: 28109
		// (get) Token: 0x06015475 RID: 87157 RVA: 0x0031D84A File Offset: 0x0031BA4A
		public override string LocalName
		{
			get
			{
				return "naryLim";
			}
		}

		// Token: 0x17006DCE RID: 28110
		// (get) Token: 0x06015476 RID: 87158 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DCF RID: 28111
		// (get) Token: 0x06015477 RID: 87159 RVA: 0x0031D851 File Offset: 0x0031BA51
		internal override int ElementTypeId
		{
			get
			{
				return 10961;
			}
		}

		// Token: 0x06015478 RID: 87160 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601547A RID: 87162 RVA: 0x0031D858 File Offset: 0x0031BA58
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NaryLimitLocation>(deep);
		}

		// Token: 0x0400927A RID: 37498
		private const string tagName = "naryLim";

		// Token: 0x0400927B RID: 37499
		private const byte tagNsId = 21;

		// Token: 0x0400927C RID: 37500
		internal const int ElementTypeIdConst = 10961;
	}
}
