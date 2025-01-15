using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F82 RID: 12162
	[ChildElementInfo(typeof(Div))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class DivsType : OpenXmlCompositeElement
	{
		// Token: 0x0601A35E RID: 107358 RVA: 0x0035F2A4 File Offset: 0x0035D4A4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "div" == name)
			{
				return new Div();
			}
			return null;
		}

		// Token: 0x0601A35F RID: 107359 RVA: 0x00293ECF File Offset: 0x002920CF
		protected DivsType()
		{
		}

		// Token: 0x0601A360 RID: 107360 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected DivsType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A361 RID: 107361 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected DivsType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A362 RID: 107362 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected DivsType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
