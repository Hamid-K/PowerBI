using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F5A RID: 12122
	[ChildElementInfo(typeof(SmartTagAttribute))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTagProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009042 RID: 36930
		// (get) Token: 0x0601A08F RID: 106639 RVA: 0x0033EC1D File Offset: 0x0033CE1D
		public override string LocalName
		{
			get
			{
				return "smartTagPr";
			}
		}

		// Token: 0x17009043 RID: 36931
		// (get) Token: 0x0601A090 RID: 106640 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009044 RID: 36932
		// (get) Token: 0x0601A091 RID: 106641 RVA: 0x0035CAE9 File Offset: 0x0035ACE9
		internal override int ElementTypeId
		{
			get
			{
				return 11777;
			}
		}

		// Token: 0x0601A092 RID: 106642 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A093 RID: 106643 RVA: 0x00293ECF File Offset: 0x002920CF
		public SmartTagProperties()
		{
		}

		// Token: 0x0601A094 RID: 106644 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SmartTagProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A095 RID: 106645 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SmartTagProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A096 RID: 106646 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SmartTagProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A097 RID: 106647 RVA: 0x0035CAF0 File Offset: 0x0035ACF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "attr" == name)
			{
				return new SmartTagAttribute();
			}
			return null;
		}

		// Token: 0x0601A098 RID: 106648 RVA: 0x0035CB0B File Offset: 0x0035AD0B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTagProperties>(deep);
		}

		// Token: 0x0400AB81 RID: 43905
		private const string tagName = "smartTagPr";

		// Token: 0x0400AB82 RID: 43906
		private const byte tagNsId = 23;

		// Token: 0x0400AB83 RID: 43907
		internal const int ElementTypeIdConst = 11777;
	}
}
