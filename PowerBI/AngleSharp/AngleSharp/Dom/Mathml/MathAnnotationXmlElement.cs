using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Mathml
{
	// Token: 0x020001BC RID: 444
	internal sealed class MathAnnotationXmlElement : MathElement
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x00046E6D File Offset: 0x0004506D
		public MathAnnotationXmlElement(Document owner, string prefix = null)
			: base(owner, TagNames.AnnotationXml, prefix, NodeFlags.Special | NodeFlags.Scoped)
		{
		}
	}
}
