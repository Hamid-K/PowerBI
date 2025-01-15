using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026EC RID: 9964
	[GeneratedCode("DomGen", "2.0")]
	internal class GreenModulation : PercentageType
	{
		// Token: 0x17005DEC RID: 24044
		// (get) Token: 0x06013004 RID: 77828 RVA: 0x0030192A File Offset: 0x002FFB2A
		public override string LocalName
		{
			get
			{
				return "greenMod";
			}
		}

		// Token: 0x17005DED RID: 24045
		// (get) Token: 0x06013005 RID: 77829 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DEE RID: 24046
		// (get) Token: 0x06013006 RID: 77830 RVA: 0x00301931 File Offset: 0x002FFB31
		internal override int ElementTypeId
		{
			get
			{
				return 10028;
			}
		}

		// Token: 0x06013007 RID: 77831 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013009 RID: 77833 RVA: 0x00301938 File Offset: 0x002FFB38
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GreenModulation>(deep);
		}

		// Token: 0x0400842E RID: 33838
		private const string tagName = "greenMod";

		// Token: 0x0400842F RID: 33839
		private const byte tagNsId = 10;

		// Token: 0x04008430 RID: 33840
		internal const int ElementTypeIdConst = 10028;
	}
}
