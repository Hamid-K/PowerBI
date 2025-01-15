using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F53 RID: 12115
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunProperties))]
	internal class SdtEndCharProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009029 RID: 36905
		// (get) Token: 0x0601A043 RID: 106563 RVA: 0x0035B50F File Offset: 0x0035970F
		public override string LocalName
		{
			get
			{
				return "sdtEndPr";
			}
		}

		// Token: 0x1700902A RID: 36906
		// (get) Token: 0x0601A044 RID: 106564 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700902B RID: 36907
		// (get) Token: 0x0601A045 RID: 106565 RVA: 0x0035B516 File Offset: 0x00359716
		internal override int ElementTypeId
		{
			get
			{
				return 11770;
			}
		}

		// Token: 0x0601A046 RID: 106566 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A047 RID: 106567 RVA: 0x00293ECF File Offset: 0x002920CF
		public SdtEndCharProperties()
		{
		}

		// Token: 0x0601A048 RID: 106568 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SdtEndCharProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A049 RID: 106569 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SdtEndCharProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A04A RID: 106570 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SdtEndCharProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A04B RID: 106571 RVA: 0x0034A1F1 File Offset: 0x003483F1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			return null;
		}

		// Token: 0x0601A04C RID: 106572 RVA: 0x0035B51D File Offset: 0x0035971D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtEndCharProperties>(deep);
		}

		// Token: 0x0400AB6A RID: 43882
		private const string tagName = "sdtEndPr";

		// Token: 0x0400AB6B RID: 43883
		private const byte tagNsId = 23;

		// Token: 0x0400AB6C RID: 43884
		internal const int ElementTypeIdConst = 11770;
	}
}
