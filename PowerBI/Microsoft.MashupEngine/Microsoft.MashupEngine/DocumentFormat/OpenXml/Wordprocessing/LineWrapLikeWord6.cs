using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DED RID: 11757
	[GeneratedCode("DomGen", "2.0")]
	internal class LineWrapLikeWord6 : OnOffType
	{
		// Token: 0x17008863 RID: 34915
		// (get) Token: 0x06018F86 RID: 102278 RVA: 0x00345558 File Offset: 0x00343758
		public override string LocalName
		{
			get
			{
				return "lineWrapLikeWord6";
			}
		}

		// Token: 0x17008864 RID: 34916
		// (get) Token: 0x06018F87 RID: 102279 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008865 RID: 34917
		// (get) Token: 0x06018F88 RID: 102280 RVA: 0x0034555F File Offset: 0x0034375F
		internal override int ElementTypeId
		{
			get
			{
				return 12067;
			}
		}

		// Token: 0x06018F89 RID: 102281 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F8B RID: 102283 RVA: 0x00345566 File Offset: 0x00343766
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineWrapLikeWord6>(deep);
		}

		// Token: 0x0400A632 RID: 42546
		private const string tagName = "lineWrapLikeWord6";

		// Token: 0x0400A633 RID: 42547
		private const byte tagNsId = 23;

		// Token: 0x0400A634 RID: 42548
		internal const int ElementTypeIdConst = 12067;
	}
}
