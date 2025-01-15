using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200228F RID: 8847
	[ChildElementInfo(typeof(UnsizedButton))]
	[ChildElementInfo(typeof(QuickAccessToolbarControlClone))]
	[ChildElementInfo(typeof(VerticalSeparator))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class QatItemsType : OpenXmlCompositeElement
	{
		// Token: 0x0600EF70 RID: 61296 RVA: 0x002CFF74 File Offset: 0x002CE174
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "control" == name)
			{
				return new QuickAccessToolbarControlClone();
			}
			if (34 == namespaceId && "button" == name)
			{
				return new UnsizedButton();
			}
			if (34 == namespaceId && "separator" == name)
			{
				return new VerticalSeparator();
			}
			return null;
		}

		// Token: 0x0600EF71 RID: 61297 RVA: 0x00293ECF File Offset: 0x002920CF
		protected QatItemsType()
		{
		}

		// Token: 0x0600EF72 RID: 61298 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected QatItemsType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF73 RID: 61299 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected QatItemsType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF74 RID: 61300 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected QatItemsType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
