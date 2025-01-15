using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A23 RID: 10787
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Condition))]
	internal abstract class TimeListTimeConditionalListType : OpenXmlCompositeElement
	{
		// Token: 0x06015A8A RID: 88714 RVA: 0x00321D91 File Offset: 0x0031FF91
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cond" == name)
			{
				return new Condition();
			}
			return null;
		}

		// Token: 0x06015A8B RID: 88715 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TimeListTimeConditionalListType()
		{
		}

		// Token: 0x06015A8C RID: 88716 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TimeListTimeConditionalListType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A8D RID: 88717 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TimeListTimeConditionalListType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A8E RID: 88718 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TimeListTimeConditionalListType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
