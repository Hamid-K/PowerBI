using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F5 RID: 8949
	[ChildElementInfo(typeof(TabSet), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ContextualTabs : OpenXmlCompositeElement
	{
		// Token: 0x17004705 RID: 18181
		// (get) Token: 0x0600FD0E RID: 64782 RVA: 0x002D05D4 File Offset: 0x002CE7D4
		public override string LocalName
		{
			get
			{
				return "contextualTabs";
			}
		}

		// Token: 0x17004706 RID: 18182
		// (get) Token: 0x0600FD0F RID: 64783 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004707 RID: 18183
		// (get) Token: 0x0600FD10 RID: 64784 RVA: 0x002DBF91 File Offset: 0x002DA191
		internal override int ElementTypeId
		{
			get
			{
				return 13093;
			}
		}

		// Token: 0x0600FD11 RID: 64785 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FD12 RID: 64786 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContextualTabs()
		{
		}

		// Token: 0x0600FD13 RID: 64787 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContextualTabs(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD14 RID: 64788 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContextualTabs(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD15 RID: 64789 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContextualTabs(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FD16 RID: 64790 RVA: 0x002DBF98 File Offset: 0x002DA198
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "tabSet" == name)
			{
				return new TabSet();
			}
			return null;
		}

		// Token: 0x0600FD17 RID: 64791 RVA: 0x002DBFB3 File Offset: 0x002DA1B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextualTabs>(deep);
		}

		// Token: 0x040071ED RID: 29165
		private const string tagName = "contextualTabs";

		// Token: 0x040071EE RID: 29166
		private const byte tagNsId = 57;

		// Token: 0x040071EF RID: 29167
		internal const int ElementTypeIdConst = 13093;
	}
}
