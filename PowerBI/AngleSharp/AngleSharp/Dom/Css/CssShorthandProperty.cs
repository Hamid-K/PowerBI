using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000252 RID: 594
	internal abstract class CssShorthandProperty : CssProperty
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x0004B43D File Offset: 0x0004963D
		public CssShorthandProperty(string name, PropertyFlags flags = PropertyFlags.None)
			: base(name, flags | PropertyFlags.Shorthand)
		{
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0004B44A File Offset: 0x0004964A
		public string Stringify(CssProperty[] properties)
		{
			IPropertyValue propertyValue = this.Converter.Construct(properties);
			if (propertyValue == null)
			{
				return null;
			}
			return propertyValue.CssText;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0004B464 File Offset: 0x00049664
		public void Export(CssProperty[] properties)
		{
			foreach (CssProperty cssProperty in properties)
			{
				CssValue cssValue = base.DeclaredValue.ExtractFor(cssProperty.Name);
				cssProperty.TrySetValue(cssValue);
			}
		}
	}
}
