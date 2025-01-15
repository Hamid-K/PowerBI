using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F4 RID: 8948
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Tab), FileFormatVersions.Office2010)]
	internal class Tabs : OpenXmlCompositeElement
	{
		// Token: 0x17004702 RID: 18178
		// (get) Token: 0x0600FD04 RID: 64772 RVA: 0x002D05BD File Offset: 0x002CE7BD
		public override string LocalName
		{
			get
			{
				return "tabs";
			}
		}

		// Token: 0x17004703 RID: 18179
		// (get) Token: 0x0600FD05 RID: 64773 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004704 RID: 18180
		// (get) Token: 0x0600FD06 RID: 64774 RVA: 0x002DBF81 File Offset: 0x002DA181
		internal override int ElementTypeId
		{
			get
			{
				return 13092;
			}
		}

		// Token: 0x0600FD07 RID: 64775 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FD08 RID: 64776 RVA: 0x00293ECF File Offset: 0x002920CF
		public Tabs()
		{
		}

		// Token: 0x0600FD09 RID: 64777 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Tabs(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD0A RID: 64778 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Tabs(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD0B RID: 64779 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Tabs(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FD0C RID: 64780 RVA: 0x002DBD37 File Offset: 0x002D9F37
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "tab" == name)
			{
				return new Tab();
			}
			return null;
		}

		// Token: 0x0600FD0D RID: 64781 RVA: 0x002DBF88 File Offset: 0x002DA188
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tabs>(deep);
		}

		// Token: 0x040071EA RID: 29162
		private const string tagName = "tabs";

		// Token: 0x040071EB RID: 29163
		private const byte tagNsId = 57;

		// Token: 0x040071EC RID: 29164
		internal const int ElementTypeIdConst = 13092;
	}
}
