using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F8 RID: 10488
	[GeneratedCode("DomGen", "2.0")]
	internal class Interviewee : NameType
	{
		// Token: 0x17006991 RID: 27025
		// (get) Token: 0x06014AC1 RID: 84673 RVA: 0x003154A3 File Offset: 0x003136A3
		public override string LocalName
		{
			get
			{
				return "Interviewee";
			}
		}

		// Token: 0x17006992 RID: 27026
		// (get) Token: 0x06014AC2 RID: 84674 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006993 RID: 27027
		// (get) Token: 0x06014AC3 RID: 84675 RVA: 0x003154AA File Offset: 0x003136AA
		internal override int ElementTypeId
		{
			get
			{
				return 10774;
			}
		}

		// Token: 0x06014AC4 RID: 84676 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AC5 RID: 84677 RVA: 0x003153D6 File Offset: 0x003135D6
		public Interviewee()
		{
		}

		// Token: 0x06014AC6 RID: 84678 RVA: 0x003153DE File Offset: 0x003135DE
		public Interviewee(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AC7 RID: 84679 RVA: 0x003153E7 File Offset: 0x003135E7
		public Interviewee(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AC8 RID: 84680 RVA: 0x003153F0 File Offset: 0x003135F0
		public Interviewee(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AC9 RID: 84681 RVA: 0x003154B1 File Offset: 0x003136B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Interviewee>(deep);
		}

		// Token: 0x04008F73 RID: 36723
		private const string tagName = "Interviewee";

		// Token: 0x04008F74 RID: 36724
		private const byte tagNsId = 9;

		// Token: 0x04008F75 RID: 36725
		internal const int ElementTypeIdConst = 10774;
	}
}
