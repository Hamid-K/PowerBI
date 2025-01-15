using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D2 RID: 10194
	[ChildElementInfo(typeof(ShapeGuide))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class GeometryGuideListType : OpenXmlCompositeElement
	{
		// Token: 0x06013D3C RID: 81212 RVA: 0x0030C187 File Offset: 0x0030A387
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "gd" == name)
			{
				return new ShapeGuide();
			}
			return null;
		}

		// Token: 0x06013D3D RID: 81213 RVA: 0x00293ECF File Offset: 0x002920CF
		protected GeometryGuideListType()
		{
		}

		// Token: 0x06013D3E RID: 81214 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected GeometryGuideListType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D3F RID: 81215 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected GeometryGuideListType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D40 RID: 81216 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected GeometryGuideListType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
