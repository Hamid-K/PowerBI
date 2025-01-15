using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA6 RID: 11686
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotSaveAsSingleFile : OnOffType
	{
		// Token: 0x1700878E RID: 34702
		// (get) Token: 0x06018DDC RID: 101852 RVA: 0x00344EF7 File Offset: 0x003430F7
		public override string LocalName
		{
			get
			{
				return "doNotSaveAsSingleFile";
			}
		}

		// Token: 0x1700878F RID: 34703
		// (get) Token: 0x06018DDD RID: 101853 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008790 RID: 34704
		// (get) Token: 0x06018DDE RID: 101854 RVA: 0x00344EFE File Offset: 0x003430FE
		internal override int ElementTypeId
		{
			get
			{
				return 11842;
			}
		}

		// Token: 0x06018DDF RID: 101855 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DE1 RID: 101857 RVA: 0x00344F05 File Offset: 0x00343105
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotSaveAsSingleFile>(deep);
		}

		// Token: 0x0400A55D RID: 42333
		private const string tagName = "doNotSaveAsSingleFile";

		// Token: 0x0400A55E RID: 42334
		private const byte tagNsId = 23;

		// Token: 0x0400A55F RID: 42335
		internal const int ElementTypeIdConst = 11842;
	}
}
