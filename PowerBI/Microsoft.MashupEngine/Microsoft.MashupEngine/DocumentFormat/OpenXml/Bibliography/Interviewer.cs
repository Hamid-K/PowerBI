using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F9 RID: 10489
	[GeneratedCode("DomGen", "2.0")]
	internal class Interviewer : NameType
	{
		// Token: 0x17006994 RID: 27028
		// (get) Token: 0x06014ACA RID: 84682 RVA: 0x003154BA File Offset: 0x003136BA
		public override string LocalName
		{
			get
			{
				return "Interviewer";
			}
		}

		// Token: 0x17006995 RID: 27029
		// (get) Token: 0x06014ACB RID: 84683 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006996 RID: 27030
		// (get) Token: 0x06014ACC RID: 84684 RVA: 0x003154C1 File Offset: 0x003136C1
		internal override int ElementTypeId
		{
			get
			{
				return 10775;
			}
		}

		// Token: 0x06014ACD RID: 84685 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014ACE RID: 84686 RVA: 0x003153D6 File Offset: 0x003135D6
		public Interviewer()
		{
		}

		// Token: 0x06014ACF RID: 84687 RVA: 0x003153DE File Offset: 0x003135DE
		public Interviewer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AD0 RID: 84688 RVA: 0x003153E7 File Offset: 0x003135E7
		public Interviewer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AD1 RID: 84689 RVA: 0x003153F0 File Offset: 0x003135F0
		public Interviewer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AD2 RID: 84690 RVA: 0x003154C8 File Offset: 0x003136C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Interviewer>(deep);
		}

		// Token: 0x04008F76 RID: 36726
		private const string tagName = "Interviewer";

		// Token: 0x04008F77 RID: 36727
		private const byte tagNsId = 9;

		// Token: 0x04008F78 RID: 36728
		internal const int ElementTypeIdConst = 10775;
	}
}
