using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026EF RID: 9967
	[GeneratedCode("DomGen", "2.0")]
	internal class BlueModulation : PercentageType
	{
		// Token: 0x17005DF5 RID: 24053
		// (get) Token: 0x06013016 RID: 77846 RVA: 0x0030196F File Offset: 0x002FFB6F
		public override string LocalName
		{
			get
			{
				return "blueMod";
			}
		}

		// Token: 0x17005DF6 RID: 24054
		// (get) Token: 0x06013017 RID: 77847 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DF7 RID: 24055
		// (get) Token: 0x06013018 RID: 77848 RVA: 0x00301976 File Offset: 0x002FFB76
		internal override int ElementTypeId
		{
			get
			{
				return 10031;
			}
		}

		// Token: 0x06013019 RID: 77849 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601301B RID: 77851 RVA: 0x0030197D File Offset: 0x002FFB7D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlueModulation>(deep);
		}

		// Token: 0x04008437 RID: 33847
		private const string tagName = "blueMod";

		// Token: 0x04008438 RID: 33848
		private const byte tagNsId = 10;

		// Token: 0x04008439 RID: 33849
		internal const int ElementTypeIdConst = 10031;
	}
}
