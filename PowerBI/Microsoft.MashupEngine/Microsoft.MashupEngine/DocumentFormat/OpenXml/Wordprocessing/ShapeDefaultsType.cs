using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF0 RID: 12272
	[ChildElementInfo(typeof(ShapeLayout))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeDefaults))]
	internal abstract class ShapeDefaultsType : OpenXmlCompositeElement
	{
		// Token: 0x0601AB1D RID: 109341 RVA: 0x00365FC5 File Offset: 0x003641C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "shapedefaults" == name)
			{
				return new ShapeDefaults();
			}
			if (27 == namespaceId && "shapelayout" == name)
			{
				return new ShapeLayout();
			}
			return null;
		}

		// Token: 0x0601AB1E RID: 109342 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ShapeDefaultsType()
		{
		}

		// Token: 0x0601AB1F RID: 109343 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ShapeDefaultsType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB20 RID: 109344 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ShapeDefaultsType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB21 RID: 109345 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ShapeDefaultsType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
