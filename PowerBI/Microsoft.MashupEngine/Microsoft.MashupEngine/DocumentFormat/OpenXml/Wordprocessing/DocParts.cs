using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD1 RID: 12241
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocPart))]
	internal class DocParts : OpenXmlCompositeElement
	{
		// Token: 0x17009442 RID: 37954
		// (get) Token: 0x0601A90D RID: 108813 RVA: 0x00364523 File Offset: 0x00362723
		public override string LocalName
		{
			get
			{
				return "docParts";
			}
		}

		// Token: 0x17009443 RID: 37955
		// (get) Token: 0x0601A90E RID: 108814 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009444 RID: 37956
		// (get) Token: 0x0601A90F RID: 108815 RVA: 0x0036452A File Offset: 0x0036272A
		internal override int ElementTypeId
		{
			get
			{
				return 11947;
			}
		}

		// Token: 0x0601A910 RID: 108816 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A911 RID: 108817 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocParts()
		{
		}

		// Token: 0x0601A912 RID: 108818 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocParts(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A913 RID: 108819 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocParts(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A914 RID: 108820 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocParts(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A915 RID: 108821 RVA: 0x00364531 File Offset: 0x00362731
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "docPart" == name)
			{
				return new DocPart();
			}
			return null;
		}

		// Token: 0x0601A916 RID: 108822 RVA: 0x0036454C File Offset: 0x0036274C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocParts>(deep);
		}

		// Token: 0x0400AD88 RID: 44424
		private const string tagName = "docParts";

		// Token: 0x0400AD89 RID: 44425
		private const byte tagNsId = 23;

		// Token: 0x0400AD8A RID: 44426
		internal const int ElementTypeIdConst = 11947;
	}
}
