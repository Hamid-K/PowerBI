using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020001FA RID: 506
	[DomName("CSSRule")]
	public enum CssRuleType : byte
	{
		// Token: 0x04000A79 RID: 2681
		Unknown,
		// Token: 0x04000A7A RID: 2682
		[DomName("STYLE_RULE")]
		Style,
		// Token: 0x04000A7B RID: 2683
		[DomName("CHARSET_RULE")]
		Charset,
		// Token: 0x04000A7C RID: 2684
		[DomName("IMPORT_RULE")]
		Import,
		// Token: 0x04000A7D RID: 2685
		[DomName("MEDIA_RULE")]
		Media,
		// Token: 0x04000A7E RID: 2686
		[DomName("FONT_FACE_RULE")]
		FontFace,
		// Token: 0x04000A7F RID: 2687
		[DomName("PAGE_RULE")]
		Page,
		// Token: 0x04000A80 RID: 2688
		[DomName("KEYFRAMES_RULE")]
		Keyframes,
		// Token: 0x04000A81 RID: 2689
		[DomName("KEYFRAME_RULE")]
		Keyframe,
		// Token: 0x04000A82 RID: 2690
		[DomName("NAMESPACE_RULE")]
		Namespace = 10,
		// Token: 0x04000A83 RID: 2691
		[DomName("COUNTER_STYLE_RULE")]
		CounterStyle,
		// Token: 0x04000A84 RID: 2692
		[DomName("SUPPORTS_RULE")]
		Supports,
		// Token: 0x04000A85 RID: 2693
		[DomName("DOCUMENT_RULE")]
		Document,
		// Token: 0x04000A86 RID: 2694
		[DomName("FONT_FEATURE_VALUES_RULE")]
		FontFeatureValues,
		// Token: 0x04000A87 RID: 2695
		[DomName("VIEWPORT_RULE")]
		Viewport,
		// Token: 0x04000A88 RID: 2696
		[DomName("REGION_STYLE_RULE")]
		RegionStyle
	}
}
