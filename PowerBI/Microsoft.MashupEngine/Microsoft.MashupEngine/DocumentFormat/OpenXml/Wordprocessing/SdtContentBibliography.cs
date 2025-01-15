using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E75 RID: 11893
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentBibliography : EmptyType
	{
		// Token: 0x17008AA0 RID: 35488
		// (get) Token: 0x06019421 RID: 103457 RVA: 0x00347C20 File Offset: 0x00345E20
		public override string LocalName
		{
			get
			{
				return "bibliography";
			}
		}

		// Token: 0x17008AA1 RID: 35489
		// (get) Token: 0x06019422 RID: 103458 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AA2 RID: 35490
		// (get) Token: 0x06019423 RID: 103459 RVA: 0x00347C27 File Offset: 0x00345E27
		internal override int ElementTypeId
		{
			get
			{
				return 12158;
			}
		}

		// Token: 0x06019424 RID: 103460 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019426 RID: 103462 RVA: 0x00347C2E File Offset: 0x00345E2E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentBibliography>(deep);
		}

		// Token: 0x0400A7F7 RID: 42999
		private const string tagName = "bibliography";

		// Token: 0x0400A7F8 RID: 43000
		private const byte tagNsId = 23;

		// Token: 0x0400A7F9 RID: 43001
		internal const int ElementTypeIdConst = 12158;
	}
}
