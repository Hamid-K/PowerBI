using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002297 RID: 8855
	[ChildElementInfo(typeof(Tab))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Tabs : OpenXmlCompositeElement
	{
		// Token: 0x170040D5 RID: 16597
		// (get) Token: 0x0600EFF3 RID: 61427 RVA: 0x002D05BD File Offset: 0x002CE7BD
		public override string LocalName
		{
			get
			{
				return "tabs";
			}
		}

		// Token: 0x170040D6 RID: 16598
		// (get) Token: 0x0600EFF4 RID: 61428 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040D7 RID: 16599
		// (get) Token: 0x0600EFF5 RID: 61429 RVA: 0x002D05C4 File Offset: 0x002CE7C4
		internal override int ElementTypeId
		{
			get
			{
				return 12613;
			}
		}

		// Token: 0x0600EFF6 RID: 61430 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600EFF7 RID: 61431 RVA: 0x00293ECF File Offset: 0x002920CF
		public Tabs()
		{
		}

		// Token: 0x0600EFF8 RID: 61432 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Tabs(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFF9 RID: 61433 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Tabs(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFFA RID: 61434 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Tabs(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EFFB RID: 61435 RVA: 0x002D0262 File Offset: 0x002CE462
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "tab" == name)
			{
				return new Tab();
			}
			return null;
		}

		// Token: 0x0600EFFC RID: 61436 RVA: 0x002D05CB File Offset: 0x002CE7CB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tabs>(deep);
		}

		// Token: 0x04007049 RID: 28745
		private const string tagName = "tabs";

		// Token: 0x0400704A RID: 28746
		private const byte tagNsId = 34;

		// Token: 0x0400704B RID: 28747
		internal const int ElementTypeIdConst = 12613;
	}
}
