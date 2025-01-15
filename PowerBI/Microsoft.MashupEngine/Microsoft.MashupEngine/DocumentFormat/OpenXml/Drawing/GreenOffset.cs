using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026EB RID: 9963
	[GeneratedCode("DomGen", "2.0")]
	internal class GreenOffset : PercentageType
	{
		// Token: 0x17005DE9 RID: 24041
		// (get) Token: 0x06012FFE RID: 77822 RVA: 0x00301913 File Offset: 0x002FFB13
		public override string LocalName
		{
			get
			{
				return "greenOff";
			}
		}

		// Token: 0x17005DEA RID: 24042
		// (get) Token: 0x06012FFF RID: 77823 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DEB RID: 24043
		// (get) Token: 0x06013000 RID: 77824 RVA: 0x0030191A File Offset: 0x002FFB1A
		internal override int ElementTypeId
		{
			get
			{
				return 10027;
			}
		}

		// Token: 0x06013001 RID: 77825 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013003 RID: 77827 RVA: 0x00301921 File Offset: 0x002FFB21
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GreenOffset>(deep);
		}

		// Token: 0x0400842B RID: 33835
		private const string tagName = "greenOff";

		// Token: 0x0400842C RID: 33836
		private const byte tagNsId = 10;

		// Token: 0x0400842D RID: 33837
		internal const int ElementTypeIdConst = 10027;
	}
}
