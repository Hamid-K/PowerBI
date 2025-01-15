using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Presentation
{
	// Token: 0x02002234 RID: 8756
	[GeneratedCode("DomGen", "2.0")]
	internal class InkAnnotationFlag : OpenXmlLeafElement
	{
		// Token: 0x17003955 RID: 14677
		// (get) Token: 0x0600E059 RID: 57433 RVA: 0x002BFCE0 File Offset: 0x002BDEE0
		public override string LocalName
		{
			get
			{
				return "iscomment";
			}
		}

		// Token: 0x17003956 RID: 14678
		// (get) Token: 0x0600E05A RID: 57434 RVA: 0x0012AF11 File Offset: 0x00129111
		internal override byte NamespaceId
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x17003957 RID: 14679
		// (get) Token: 0x0600E05B RID: 57435 RVA: 0x002BFCE7 File Offset: 0x002BDEE7
		internal override int ElementTypeId
		{
			get
			{
				return 12504;
			}
		}

		// Token: 0x0600E05C RID: 57436 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E05E RID: 57438 RVA: 0x002BFCEE File Offset: 0x002BDEEE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InkAnnotationFlag>(deep);
		}

		// Token: 0x04006E43 RID: 28227
		private const string tagName = "iscomment";

		// Token: 0x04006E44 RID: 28228
		private const byte tagNsId = 30;

		// Token: 0x04006E45 RID: 28229
		internal const int ElementTypeIdConst = 12504;
	}
}
