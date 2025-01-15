using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000515 RID: 1301
	internal sealed class ParagraphFilteredStyle : Style
	{
		// Token: 0x06004545 RID: 17733 RVA: 0x0012263E File Offset: 0x0012083E
		internal ParagraphFilteredStyle(Style style)
			: base(ConstructionPhase.Deserializing)
		{
			this.m_styleAttributes = style.StyleAttributes;
			this.m_expressionList = style.ExpressionList;
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x0012265F File Offset: 0x0012085F
		internal override bool GetAttributeInfo(string styleAttributeName, out AttributeInfo styleAttribute)
		{
			if (styleAttributeName == "TextAlign" || styleAttributeName == "LineHeight")
			{
				return base.GetAttributeInfo(styleAttributeName, out styleAttribute);
			}
			styleAttribute = null;
			return false;
		}
	}
}
