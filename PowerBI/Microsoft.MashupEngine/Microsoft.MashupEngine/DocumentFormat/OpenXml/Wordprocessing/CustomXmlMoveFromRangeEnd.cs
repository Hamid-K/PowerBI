using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D4D RID: 11597
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlMoveFromRangeEnd : MarkupType
	{
		// Token: 0x17008683 RID: 34435
		// (get) Token: 0x06018BC4 RID: 101316 RVA: 0x003446C2 File Offset: 0x003428C2
		public override string LocalName
		{
			get
			{
				return "customXmlMoveFromRangeEnd";
			}
		}

		// Token: 0x17008684 RID: 34436
		// (get) Token: 0x06018BC5 RID: 101317 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008685 RID: 34437
		// (get) Token: 0x06018BC6 RID: 101318 RVA: 0x003446C9 File Offset: 0x003428C9
		internal override int ElementTypeId
		{
			get
			{
				return 11489;
			}
		}

		// Token: 0x06018BC7 RID: 101319 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BC9 RID: 101321 RVA: 0x003446D0 File Offset: 0x003428D0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlMoveFromRangeEnd>(deep);
		}

		// Token: 0x0400A454 RID: 42068
		private const string tagName = "customXmlMoveFromRangeEnd";

		// Token: 0x0400A455 RID: 42069
		private const byte tagNsId = 23;

		// Token: 0x0400A456 RID: 42070
		internal const int ElementTypeIdConst = 11489;
	}
}
