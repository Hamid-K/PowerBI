using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002298 RID: 8856
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ContextualTabSet))]
	internal class ContextualTabSets : OpenXmlCompositeElement
	{
		// Token: 0x170040D8 RID: 16600
		// (get) Token: 0x0600EFFD RID: 61437 RVA: 0x002D05D4 File Offset: 0x002CE7D4
		public override string LocalName
		{
			get
			{
				return "contextualTabs";
			}
		}

		// Token: 0x170040D9 RID: 16601
		// (get) Token: 0x0600EFFE RID: 61438 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040DA RID: 16602
		// (get) Token: 0x0600EFFF RID: 61439 RVA: 0x002D05DB File Offset: 0x002CE7DB
		internal override int ElementTypeId
		{
			get
			{
				return 12614;
			}
		}

		// Token: 0x0600F000 RID: 61440 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F001 RID: 61441 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContextualTabSets()
		{
		}

		// Token: 0x0600F002 RID: 61442 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContextualTabSets(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F003 RID: 61443 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContextualTabSets(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F004 RID: 61444 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContextualTabSets(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F005 RID: 61445 RVA: 0x002D05E2 File Offset: 0x002CE7E2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "tabSet" == name)
			{
				return new ContextualTabSet();
			}
			return null;
		}

		// Token: 0x0600F006 RID: 61446 RVA: 0x002D05FD File Offset: 0x002CE7FD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextualTabSets>(deep);
		}

		// Token: 0x0400704C RID: 28748
		private const string tagName = "contextualTabs";

		// Token: 0x0400704D RID: 28749
		private const byte tagNsId = 34;

		// Token: 0x0400704E RID: 28750
		internal const int ElementTypeIdConst = 12614;
	}
}
