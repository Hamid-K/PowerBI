using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026DC RID: 9948
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaModulation : PositivePercentageType
	{
		// Token: 0x17005DB6 RID: 23990
		// (get) Token: 0x06012F97 RID: 77719 RVA: 0x00301737 File Offset: 0x002FF937
		public override string LocalName
		{
			get
			{
				return "alphaMod";
			}
		}

		// Token: 0x17005DB7 RID: 23991
		// (get) Token: 0x06012F98 RID: 77720 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DB8 RID: 23992
		// (get) Token: 0x06012F99 RID: 77721 RVA: 0x0030173E File Offset: 0x002FF93E
		internal override int ElementTypeId
		{
			get
			{
				return 10013;
			}
		}

		// Token: 0x06012F9A RID: 77722 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F9C RID: 77724 RVA: 0x0030174D File Offset: 0x002FF94D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaModulation>(deep);
		}

		// Token: 0x040083FB RID: 33787
		private const string tagName = "alphaMod";

		// Token: 0x040083FC RID: 33788
		private const byte tagNsId = 10;

		// Token: 0x040083FD RID: 33789
		internal const int ElementTypeIdConst = 10013;
	}
}
