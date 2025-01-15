using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028FF RID: 10495
	[GeneratedCode("DomGen", "2.0")]
	internal class Author : NameOrCorporateType
	{
		// Token: 0x170069A8 RID: 27048
		// (get) Token: 0x06014B04 RID: 84740 RVA: 0x003155C5 File Offset: 0x003137C5
		public override string LocalName
		{
			get
			{
				return "Author";
			}
		}

		// Token: 0x170069A9 RID: 27049
		// (get) Token: 0x06014B05 RID: 84741 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170069AA RID: 27050
		// (get) Token: 0x06014B06 RID: 84742 RVA: 0x003155CC File Offset: 0x003137CC
		internal override int ElementTypeId
		{
			get
			{
				return 10766;
			}
		}

		// Token: 0x06014B07 RID: 84743 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014B08 RID: 84744 RVA: 0x003155D3 File Offset: 0x003137D3
		public Author()
		{
		}

		// Token: 0x06014B09 RID: 84745 RVA: 0x003155DB File Offset: 0x003137DB
		public Author(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B0A RID: 84746 RVA: 0x003155E4 File Offset: 0x003137E4
		public Author(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B0B RID: 84747 RVA: 0x003155ED File Offset: 0x003137ED
		public Author(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014B0C RID: 84748 RVA: 0x003155F6 File Offset: 0x003137F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Author>(deep);
		}

		// Token: 0x04008F87 RID: 36743
		private const string tagName = "Author";

		// Token: 0x04008F88 RID: 36744
		private const byte tagNsId = 9;

		// Token: 0x04008F89 RID: 36745
		internal const int ElementTypeIdConst = 10766;
	}
}
