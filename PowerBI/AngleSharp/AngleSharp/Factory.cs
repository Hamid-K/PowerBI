using System;
using AngleSharp.Dom;
using AngleSharp.Services.Default;

namespace AngleSharp
{
	// Token: 0x0200001B RID: 27
	internal static class Factory
	{
		// Token: 0x04000053 RID: 83
		public static readonly HtmlElementFactory HtmlElements = new HtmlElementFactory();

		// Token: 0x04000054 RID: 84
		public static readonly MathElementFactory MathElements = new MathElementFactory();

		// Token: 0x04000055 RID: 85
		public static readonly SvgElementFactory SvgElements = new SvgElementFactory();

		// Token: 0x04000056 RID: 86
		public static readonly EventFactory Events = new EventFactory();

		// Token: 0x04000057 RID: 87
		public static readonly CssPropertyFactory Properties = new CssPropertyFactory();

		// Token: 0x04000058 RID: 88
		public static readonly InputTypeFactory InputTypes = new InputTypeFactory();

		// Token: 0x04000059 RID: 89
		public static readonly MediaFeatureFactory MediaFeatures = new MediaFeatureFactory();

		// Token: 0x0400005A RID: 90
		public static readonly AttributeSelectorFactory AttributeSelector = new AttributeSelectorFactory();

		// Token: 0x0400005B RID: 91
		public static readonly PseudoElementSelectorFactory PseudoElementSelector = new PseudoElementSelectorFactory();

		// Token: 0x0400005C RID: 92
		public static readonly PseudoClassSelectorFactory PseudoClassSelector = new PseudoClassSelectorFactory();

		// Token: 0x0400005D RID: 93
		public static readonly LinkRelationFactory LinkRelations = new LinkRelationFactory();

		// Token: 0x0400005E RID: 94
		public static readonly DocumentFactory Document = new DocumentFactory();

		// Token: 0x0400005F RID: 95
		public static readonly ContextFactory BrowsingContext = new ContextFactory();

		// Token: 0x04000060 RID: 96
		public static readonly ServiceFactory Service = new ServiceFactory();

		// Token: 0x04000061 RID: 97
		public static readonly DefaultAttributeObserver Observer = new DefaultAttributeObserver();
	}
}
