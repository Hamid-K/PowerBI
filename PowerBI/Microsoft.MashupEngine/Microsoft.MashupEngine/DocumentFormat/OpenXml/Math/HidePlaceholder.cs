using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200297A RID: 10618
	[GeneratedCode("DomGen", "2.0")]
	internal class HidePlaceholder : OnOffType
	{
		// Token: 0x17006C79 RID: 27769
		// (get) Token: 0x06015180 RID: 86400 RVA: 0x0031B55B File Offset: 0x0031975B
		public override string LocalName
		{
			get
			{
				return "plcHide";
			}
		}

		// Token: 0x17006C7A RID: 27770
		// (get) Token: 0x06015181 RID: 86401 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C7B RID: 27771
		// (get) Token: 0x06015182 RID: 86402 RVA: 0x0031B562 File Offset: 0x00319762
		internal override int ElementTypeId
		{
			get
			{
				return 10916;
			}
		}

		// Token: 0x06015183 RID: 86403 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015185 RID: 86405 RVA: 0x0031B569 File Offset: 0x00319769
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HidePlaceholder>(deep);
		}

		// Token: 0x0400917A RID: 37242
		private const string tagName = "plcHide";

		// Token: 0x0400917B RID: 37243
		private const byte tagNsId = 21;

		// Token: 0x0400917C RID: 37244
		internal const int ElementTypeIdConst = 10916;
	}
}
