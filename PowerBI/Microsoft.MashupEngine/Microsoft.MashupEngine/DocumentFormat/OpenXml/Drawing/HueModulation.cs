using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026DD RID: 9949
	[GeneratedCode("DomGen", "2.0")]
	internal class HueModulation : PositivePercentageType
	{
		// Token: 0x17005DB9 RID: 23993
		// (get) Token: 0x06012F9D RID: 77725 RVA: 0x002EC9C5 File Offset: 0x002EABC5
		public override string LocalName
		{
			get
			{
				return "hueMod";
			}
		}

		// Token: 0x17005DBA RID: 23994
		// (get) Token: 0x06012F9E RID: 77726 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DBB RID: 23995
		// (get) Token: 0x06012F9F RID: 77727 RVA: 0x00301756 File Offset: 0x002FF956
		internal override int ElementTypeId
		{
			get
			{
				return 10016;
			}
		}

		// Token: 0x06012FA0 RID: 77728 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FA2 RID: 77730 RVA: 0x0030175D File Offset: 0x002FF95D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HueModulation>(deep);
		}

		// Token: 0x040083FE RID: 33790
		private const string tagName = "hueMod";

		// Token: 0x040083FF RID: 33791
		private const byte tagNsId = 10;

		// Token: 0x04008400 RID: 33792
		internal const int ElementTypeIdConst = 10016;
	}
}
