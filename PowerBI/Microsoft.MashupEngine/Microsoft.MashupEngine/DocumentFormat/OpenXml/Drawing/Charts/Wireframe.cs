using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002512 RID: 9490
	[GeneratedCode("DomGen", "2.0")]
	internal class Wireframe : BooleanType
	{
		// Token: 0x1700544F RID: 21583
		// (get) Token: 0x06011A99 RID: 72345 RVA: 0x002F12C7 File Offset: 0x002EF4C7
		public override string LocalName
		{
			get
			{
				return "wireframe";
			}
		}

		// Token: 0x17005450 RID: 21584
		// (get) Token: 0x06011A9A RID: 72346 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005451 RID: 21585
		// (get) Token: 0x06011A9B RID: 72347 RVA: 0x002F12CE File Offset: 0x002EF4CE
		internal override int ElementTypeId
		{
			get
			{
				return 10370;
			}
		}

		// Token: 0x06011A9C RID: 72348 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A9E RID: 72350 RVA: 0x002F12D5 File Offset: 0x002EF4D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Wireframe>(deep);
		}

		// Token: 0x04007BCA RID: 31690
		private const string tagName = "wireframe";

		// Token: 0x04007BCB RID: 31691
		private const byte tagNsId = 11;

		// Token: 0x04007BCC RID: 31692
		internal const int ElementTypeIdConst = 10370;
	}
}
