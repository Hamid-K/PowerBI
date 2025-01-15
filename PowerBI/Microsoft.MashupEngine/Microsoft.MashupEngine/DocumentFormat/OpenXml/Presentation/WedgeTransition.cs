using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029ED RID: 10733
	[GeneratedCode("DomGen", "2.0")]
	internal class WedgeTransition : EmptyType
	{
		// Token: 0x17006E60 RID: 28256
		// (get) Token: 0x060155AF RID: 87471 RVA: 0x0031E25A File Offset: 0x0031C45A
		public override string LocalName
		{
			get
			{
				return "wedge";
			}
		}

		// Token: 0x17006E61 RID: 28257
		// (get) Token: 0x060155B0 RID: 87472 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E62 RID: 28258
		// (get) Token: 0x060155B1 RID: 87473 RVA: 0x0031E261 File Offset: 0x0031C461
		internal override int ElementTypeId
		{
			get
			{
				return 12392;
			}
		}

		// Token: 0x060155B2 RID: 87474 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060155B4 RID: 87476 RVA: 0x0031E268 File Offset: 0x0031C468
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WedgeTransition>(deep);
		}

		// Token: 0x04009321 RID: 37665
		private const string tagName = "wedge";

		// Token: 0x04009322 RID: 37666
		private const byte tagNsId = 24;

		// Token: 0x04009323 RID: 37667
		internal const int ElementTypeIdConst = 12392;
	}
}
