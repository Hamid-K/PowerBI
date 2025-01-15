using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200247A RID: 9338
	[ChildElementInfo(typeof(KeyMapEntry))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class KeymapsType : OpenXmlCompositeElement
	{
		// Token: 0x060113B0 RID: 70576 RVA: 0x002EBEC0 File Offset: 0x002EA0C0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "keymap" == name)
			{
				return new KeyMapEntry();
			}
			return null;
		}

		// Token: 0x060113B1 RID: 70577 RVA: 0x00293ECF File Offset: 0x002920CF
		protected KeymapsType()
		{
		}

		// Token: 0x060113B2 RID: 70578 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected KeymapsType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113B3 RID: 70579 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected KeymapsType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113B4 RID: 70580 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected KeymapsType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
