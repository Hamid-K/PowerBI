using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.LongProperties
{
	// Token: 0x020022B6 RID: 8886
	[ChildElementInfo(typeof(LongProperty))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LongProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700416A RID: 16746
		// (get) Token: 0x0600F15B RID: 61787 RVA: 0x002D1404 File Offset: 0x002CF604
		public override string LocalName
		{
			get
			{
				return "LongProperties";
			}
		}

		// Token: 0x1700416B RID: 16747
		// (get) Token: 0x0600F15C RID: 61788 RVA: 0x002D140B File Offset: 0x002CF60B
		internal override byte NamespaceId
		{
			get
			{
				return 40;
			}
		}

		// Token: 0x1700416C RID: 16748
		// (get) Token: 0x0600F15D RID: 61789 RVA: 0x002D140F File Offset: 0x002CF60F
		internal override int ElementTypeId
		{
			get
			{
				return 12640;
			}
		}

		// Token: 0x0600F15E RID: 61790 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F15F RID: 61791 RVA: 0x00293ECF File Offset: 0x002920CF
		public LongProperties()
		{
		}

		// Token: 0x0600F160 RID: 61792 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LongProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F161 RID: 61793 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LongProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F162 RID: 61794 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LongProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F163 RID: 61795 RVA: 0x002D1416 File Offset: 0x002CF616
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (40 == namespaceId && "LongProp" == name)
			{
				return new LongProperty();
			}
			return null;
		}

		// Token: 0x0600F164 RID: 61796 RVA: 0x002D1431 File Offset: 0x002CF631
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LongProperties>(deep);
		}

		// Token: 0x040070C0 RID: 28864
		private const string tagName = "LongProperties";

		// Token: 0x040070C1 RID: 28865
		private const byte tagNsId = 40;

		// Token: 0x040070C2 RID: 28866
		internal const int ElementTypeIdConst = 12640;
	}
}
