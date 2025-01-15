using System;
using AngleSharp.Dom.Css;

namespace AngleSharp.Css
{
	// Token: 0x02000106 RID: 262
	internal static class PropertyExtensions
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x0003A861 File Offset: 0x00038A61
		public static IPropertyValue Guard<T>(this CssProperty[] properties)
		{
			if (properties.Length != 1)
			{
				return null;
			}
			if (!(properties[0].DeclaredValue is T))
			{
				return null;
			}
			return properties[0].DeclaredValue;
		}
	}
}
