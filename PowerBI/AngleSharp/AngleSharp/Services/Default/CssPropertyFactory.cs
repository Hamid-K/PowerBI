using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000047 RID: 71
	internal sealed class CssPropertyFactory : ICssPropertyFactory
	{
		// Token: 0x0600016B RID: 363 RVA: 0x0000751C File Offset: 0x0000571C
		public CssPropertyFactory()
		{
			this.AddShorthand(PropertyNames.Animation, () => new CssAnimationProperty(), new string[]
			{
				PropertyNames.AnimationName,
				PropertyNames.AnimationDuration,
				PropertyNames.AnimationTimingFunction,
				PropertyNames.AnimationDelay,
				PropertyNames.AnimationDirection,
				PropertyNames.AnimationFillMode,
				PropertyNames.AnimationIterationCount,
				PropertyNames.AnimationPlayState
			});
			this.AddLonghand(PropertyNames.AnimationDelay, () => new CssAnimationDelayProperty(), false, false);
			this.AddLonghand(PropertyNames.AnimationDirection, () => new CssAnimationDirectionProperty(), false, false);
			this.AddLonghand(PropertyNames.AnimationDuration, () => new CssAnimationDurationProperty(), false, false);
			this.AddLonghand(PropertyNames.AnimationFillMode, () => new CssAnimationFillModeProperty(), false, false);
			this.AddLonghand(PropertyNames.AnimationIterationCount, () => new CssAnimationIterationCountProperty(), false, false);
			this.AddLonghand(PropertyNames.AnimationName, () => new CssAnimationNameProperty(), false, false);
			this.AddLonghand(PropertyNames.AnimationPlayState, () => new CssAnimationPlayStateProperty(), false, false);
			this.AddLonghand(PropertyNames.AnimationTimingFunction, () => new CssAnimationTimingFunctionProperty(), false, false);
			this.AddShorthand(PropertyNames.Background, () => new CssBackgroundProperty(), new string[]
			{
				PropertyNames.BackgroundAttachment,
				PropertyNames.BackgroundClip,
				PropertyNames.BackgroundColor,
				PropertyNames.BackgroundImage,
				PropertyNames.BackgroundOrigin,
				PropertyNames.BackgroundPosition,
				PropertyNames.BackgroundRepeat,
				PropertyNames.BackgroundSize
			});
			this.AddLonghand(PropertyNames.BackgroundAttachment, () => new CssBackgroundAttachmentProperty(), false, false);
			this.AddLonghand(PropertyNames.BackgroundColor, () => new CssBackgroundColorProperty(), true, false);
			this.AddLonghand(PropertyNames.BackgroundClip, () => new CssBackgroundClipProperty(), false, false);
			this.AddLonghand(PropertyNames.BackgroundOrigin, () => new CssBackgroundOriginProperty(), false, false);
			this.AddLonghand(PropertyNames.BackgroundSize, () => new CssBackgroundSizeProperty(), true, false);
			this.AddLonghand(PropertyNames.BackgroundImage, () => new CssBackgroundImageProperty(), false, false);
			this.AddLonghand(PropertyNames.BackgroundPosition, () => new CssBackgroundPositionProperty(), true, false);
			this.AddLonghand(PropertyNames.BackgroundRepeat, () => new CssBackgroundRepeatProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderSpacing, () => new CssBorderSpacingProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderCollapse, () => new CssBorderCollapseProperty(), false, false);
			this.AddLonghand(PropertyNames.BoxShadow, () => new CssBoxShadowProperty(), true, false);
			this.AddLonghand(PropertyNames.BoxDecorationBreak, () => new CssBoxDecorationBreak(), false, false);
			this.AddLonghand(PropertyNames.BreakAfter, () => new CssBreakAfterProperty(), false, false);
			this.AddLonghand(PropertyNames.BreakBefore, () => new CssBreakBeforeProperty(), false, false);
			this.AddLonghand(PropertyNames.BreakInside, () => new CssBreakInsideProperty(), false, false);
			this.AddLonghand(PropertyNames.BackfaceVisibility, () => new CssBackfaceVisibilityProperty(), false, false);
			this.AddShorthand(PropertyNames.BorderRadius, () => new CssBorderRadiusProperty(), new string[]
			{
				PropertyNames.BorderTopLeftRadius,
				PropertyNames.BorderTopRightRadius,
				PropertyNames.BorderBottomRightRadius,
				PropertyNames.BorderBottomLeftRadius
			});
			this.AddLonghand(PropertyNames.BorderTopLeftRadius, () => new CssBorderTopLeftRadiusProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderTopRightRadius, () => new CssBorderTopRightRadiusProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderBottomLeftRadius, () => new CssBorderBottomLeftRadiusProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderBottomRightRadius, () => new CssBorderBottomRightRadiusProperty(), true, false);
			this.AddShorthand(PropertyNames.BorderImage, () => new CssBorderImageProperty(), new string[]
			{
				PropertyNames.BorderImageOutset,
				PropertyNames.BorderImageRepeat,
				PropertyNames.BorderImageSlice,
				PropertyNames.BorderImageSource,
				PropertyNames.BorderImageWidth
			});
			this.AddLonghand(PropertyNames.BorderImageOutset, () => new CssBorderImageOutsetProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderImageRepeat, () => new CssBorderImageRepeatProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderImageSource, () => new CssBorderImageSourceProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderImageSlice, () => new CssBorderImageSliceProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderImageWidth, () => new CssBorderImageWidthProperty(), false, false);
			this.AddShorthand(PropertyNames.BorderColor, () => new CssBorderColorProperty(), new string[]
			{
				PropertyNames.BorderTopColor,
				PropertyNames.BorderRightColor,
				PropertyNames.BorderBottomColor,
				PropertyNames.BorderLeftColor
			});
			this.AddShorthand(PropertyNames.BorderStyle, () => new CssBorderStyleProperty(), new string[]
			{
				PropertyNames.BorderTopStyle,
				PropertyNames.BorderRightStyle,
				PropertyNames.BorderBottomStyle,
				PropertyNames.BorderLeftStyle
			});
			this.AddShorthand(PropertyNames.BorderWidth, () => new CssBorderWidthProperty(), new string[]
			{
				PropertyNames.BorderTopWidth,
				PropertyNames.BorderRightWidth,
				PropertyNames.BorderBottomWidth,
				PropertyNames.BorderLeftWidth
			});
			this.AddShorthand(PropertyNames.BorderTop, () => new CssBorderTopProperty(), new string[]
			{
				PropertyNames.BorderTopWidth,
				PropertyNames.BorderTopStyle,
				PropertyNames.BorderTopColor
			});
			this.AddShorthand(PropertyNames.BorderRight, () => new CssBorderRightProperty(), new string[]
			{
				PropertyNames.BorderRightWidth,
				PropertyNames.BorderRightStyle,
				PropertyNames.BorderRightColor
			});
			this.AddShorthand(PropertyNames.BorderBottom, () => new CssBorderBottomProperty(), new string[]
			{
				PropertyNames.BorderBottomWidth,
				PropertyNames.BorderBottomStyle,
				PropertyNames.BorderBottomColor
			});
			this.AddShorthand(PropertyNames.BorderLeft, () => new CssBorderLeftProperty(), new string[]
			{
				PropertyNames.BorderLeftWidth,
				PropertyNames.BorderLeftStyle,
				PropertyNames.BorderLeftColor
			});
			this.AddShorthand(PropertyNames.Border, () => new CssBorderProperty(), new string[]
			{
				PropertyNames.BorderTopWidth,
				PropertyNames.BorderTopStyle,
				PropertyNames.BorderTopColor,
				PropertyNames.BorderRightWidth,
				PropertyNames.BorderRightStyle,
				PropertyNames.BorderRightColor,
				PropertyNames.BorderBottomWidth,
				PropertyNames.BorderBottomStyle,
				PropertyNames.BorderBottomColor,
				PropertyNames.BorderLeftWidth,
				PropertyNames.BorderLeftStyle,
				PropertyNames.BorderLeftColor
			});
			this.AddLonghand(PropertyNames.BorderTopColor, () => new CssBorderTopColorProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderLeftColor, () => new CssBorderLeftColorProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderRightColor, () => new CssBorderRightColorProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderBottomColor, () => new CssBorderBottomColorProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderTopStyle, () => new CssBorderTopStyleProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderLeftStyle, () => new CssBorderLeftStyleProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderRightStyle, () => new CssBorderRightStyleProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderBottomStyle, () => new CssBorderBottomStyleProperty(), false, false);
			this.AddLonghand(PropertyNames.BorderTopWidth, () => new CssBorderTopWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderLeftWidth, () => new CssBorderLeftWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderRightWidth, () => new CssBorderRightWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.BorderBottomWidth, () => new CssBorderBottomWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.Bottom, () => new CssBottomProperty(), true, false);
			this.AddShorthand(PropertyNames.Columns, () => new CssColumnsProperty(), new string[]
			{
				PropertyNames.ColumnWidth,
				PropertyNames.ColumnCount
			});
			this.AddLonghand(PropertyNames.ColumnCount, () => new CssColumnCountProperty(), true, false);
			this.AddLonghand(PropertyNames.ColumnWidth, () => new CssColumnWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.ColumnFill, () => new CssColumnFillProperty(), false, false);
			this.AddLonghand(PropertyNames.ColumnGap, () => new CssColumnGapProperty(), true, false);
			this.AddLonghand(PropertyNames.ColumnSpan, () => new CssColumnSpanProperty(), false, false);
			this.AddShorthand(PropertyNames.ColumnRule, () => new CssColumnRuleProperty(), new string[]
			{
				PropertyNames.ColumnRuleWidth,
				PropertyNames.ColumnRuleStyle,
				PropertyNames.ColumnRuleColor
			});
			this.AddLonghand(PropertyNames.ColumnRuleColor, () => new CssColumnRuleColorProperty(), true, false);
			this.AddLonghand(PropertyNames.ColumnRuleStyle, () => new CssColumnRuleStyleProperty(), false, false);
			this.AddLonghand(PropertyNames.ColumnRuleWidth, () => new CssColumnRuleWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.CaptionSide, () => new CssCaptionSideProperty(), false, false);
			this.AddLonghand(PropertyNames.Clear, () => new CssClearProperty(), false, false);
			this.AddLonghand(PropertyNames.Clip, () => new CssClipProperty(), true, false);
			this.AddLonghand(PropertyNames.Color, () => new CssColorProperty(), true, false);
			this.AddLonghand(PropertyNames.Content, () => new CssContentProperty(), false, false);
			this.AddLonghand(PropertyNames.CounterIncrement, () => new CssCounterIncrementProperty(), false, false);
			this.AddLonghand(PropertyNames.CounterReset, () => new CssCounterResetProperty(), false, false);
			this.AddLonghand(PropertyNames.Cursor, () => new CssCursorProperty(), false, false);
			this.AddLonghand(PropertyNames.Direction, () => new CssDirectionProperty(), false, false);
			this.AddLonghand(PropertyNames.Display, () => new CssDisplayProperty(), false, false);
			this.AddLonghand(PropertyNames.EmptyCells, () => new CssEmptyCellsProperty(), false, false);
			this.AddLonghand(PropertyNames.Float, () => new CssFloatProperty(), false, false);
			this.AddShorthand(PropertyNames.Font, () => new CssFontProperty(), new string[]
			{
				PropertyNames.FontFamily,
				PropertyNames.FontSize,
				PropertyNames.FontStretch,
				PropertyNames.FontStyle,
				PropertyNames.FontVariant,
				PropertyNames.FontWeight,
				PropertyNames.LineHeight
			});
			this.AddLonghand(PropertyNames.FontFamily, () => new CssFontFamilyProperty(), false, true);
			this.AddLonghand(PropertyNames.FontSize, () => new CssFontSizeProperty(), true, false);
			this.AddLonghand(PropertyNames.FontSizeAdjust, () => new CssFontSizeAdjustProperty(), true, false);
			this.AddLonghand(PropertyNames.FontStyle, () => new CssFontStyleProperty(), false, true);
			this.AddLonghand(PropertyNames.FontVariant, () => new CssFontVariantProperty(), false, true);
			this.AddLonghand(PropertyNames.FontWeight, () => new CssFontWeightProperty(), true, true);
			this.AddLonghand(PropertyNames.FontStretch, () => new CssFontStretchProperty(), true, true);
			this.AddLonghand(PropertyNames.LineHeight, () => new CssLineHeightProperty(), true, false);
			this.AddLonghand(PropertyNames.Height, () => new CssHeightProperty(), true, false);
			this.AddLonghand(PropertyNames.Left, () => new CssLeftProperty(), true, false);
			this.AddLonghand(PropertyNames.LetterSpacing, () => new CssLetterSpacingProperty(), false, false);
			this.AddShorthand(PropertyNames.ListStyle, () => new CssListStyleProperty(), new string[]
			{
				PropertyNames.ListStyleType,
				PropertyNames.ListStyleImage,
				PropertyNames.ListStylePosition
			});
			this.AddLonghand(PropertyNames.ListStyleImage, () => new CssListStyleImageProperty(), false, false);
			this.AddLonghand(PropertyNames.ListStylePosition, () => new CssListStylePositionProperty(), false, false);
			this.AddLonghand(PropertyNames.ListStyleType, () => new CssListStyleTypeProperty(), false, false);
			this.AddShorthand(PropertyNames.Margin, () => new CssMarginProperty(), new string[]
			{
				PropertyNames.MarginTop,
				PropertyNames.MarginRight,
				PropertyNames.MarginBottom,
				PropertyNames.MarginLeft
			});
			this.AddLonghand(PropertyNames.MarginRight, () => new CssMarginRightProperty(), true, false);
			this.AddLonghand(PropertyNames.MarginLeft, () => new CssMarginLeftProperty(), true, false);
			this.AddLonghand(PropertyNames.MarginTop, () => new CssMarginTopProperty(), true, false);
			this.AddLonghand(PropertyNames.MarginBottom, () => new CssMarginBottomProperty(), true, false);
			this.AddLonghand(PropertyNames.MaxHeight, () => new CssMaxHeightProperty(), true, false);
			this.AddLonghand(PropertyNames.MaxWidth, () => new CssMaxWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.MinHeight, () => new CssMinHeightProperty(), true, false);
			this.AddLonghand(PropertyNames.MinWidth, () => new CssMinWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.Opacity, () => new CssOpacityProperty(), true, false);
			this.AddLonghand(PropertyNames.Orphans, () => new CssOrphansProperty(), false, false);
			this.AddShorthand(PropertyNames.Outline, () => new CssOutlineProperty(), new string[]
			{
				PropertyNames.OutlineWidth,
				PropertyNames.OutlineStyle,
				PropertyNames.OutlineColor
			});
			this.AddLonghand(PropertyNames.OutlineColor, () => new CssOutlineColorProperty(), true, false);
			this.AddLonghand(PropertyNames.OutlineStyle, () => new CssOutlineStyleProperty(), false, false);
			this.AddLonghand(PropertyNames.OutlineWidth, () => new CssOutlineWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.Overflow, () => new CssOverflowProperty(), false, false);
			this.AddLonghand(PropertyNames.OverflowWrap, () => new CssOverflowWrapProperty(), false, false);
			this.AddShorthand(PropertyNames.Padding, () => new CssPaddingProperty(), new string[]
			{
				PropertyNames.PaddingTop,
				PropertyNames.PaddingRight,
				PropertyNames.PaddingBottom,
				PropertyNames.PaddingLeft
			});
			this.AddLonghand(PropertyNames.PaddingTop, () => new CssPaddingTopProperty(), true, false);
			this.AddLonghand(PropertyNames.PaddingRight, () => new CssPaddingRightProperty(), true, false);
			this.AddLonghand(PropertyNames.PaddingLeft, () => new CssPaddingLeftProperty(), true, false);
			this.AddLonghand(PropertyNames.PaddingBottom, () => new CssPaddingBottomProperty(), true, false);
			this.AddLonghand(PropertyNames.PageBreakAfter, () => new CssPageBreakAfterProperty(), false, false);
			this.AddLonghand(PropertyNames.PageBreakBefore, () => new CssPageBreakBeforeProperty(), false, false);
			this.AddLonghand(PropertyNames.PageBreakInside, () => new CssPageBreakInsideProperty(), false, false);
			this.AddLonghand(PropertyNames.Perspective, () => new CssPerspectiveProperty(), true, false);
			this.AddLonghand(PropertyNames.PerspectiveOrigin, () => new CssPerspectiveOriginProperty(), true, false);
			this.AddLonghand(PropertyNames.Position, () => new CssPositionProperty(), false, false);
			this.AddLonghand(PropertyNames.Quotes, () => new CssQuotesProperty(), false, false);
			this.AddLonghand(PropertyNames.Right, () => new CssRightProperty(), true, false);
			this.AddLonghand(PropertyNames.Stroke, () => new CssStrokeProperty(), true, false);
			this.AddLonghand(PropertyNames.StrokeDasharray, () => new CssStrokeDasharrayProperty(), true, false);
			this.AddLonghand(PropertyNames.StrokeDashoffset, () => new CssStrokeDashoffsetProperty(), true, false);
			this.AddLonghand(PropertyNames.StrokeLinecap, () => new CssStrokeLinecapProperty(), true, false);
			this.AddLonghand(PropertyNames.StrokeLinejoin, () => new CssStrokeLinejoinProperty(), true, false);
			this.AddLonghand(PropertyNames.StrokeMiterlimit, () => new CssStrokeMiterlimitProperty(), true, false);
			this.AddLonghand(PropertyNames.StrokeOpacity, () => new CssStrokeOpacityProperty(), true, false);
			this.AddLonghand(PropertyNames.StrokeWidth, () => new CssStrokeWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.TableLayout, () => new CssTableLayoutProperty(), false, false);
			this.AddLonghand(PropertyNames.TextAlign, () => new CssTextAlignProperty(), false, false);
			this.AddLonghand(PropertyNames.TextAlignLast, () => new CssTextAlignLastProperty(), false, false);
			this.AddLonghand(PropertyNames.TextAnchor, () => new CssTextAnchorProperty(), false, false);
			this.AddShorthand(PropertyNames.TextDecoration, () => new CssTextDecorationProperty(), new string[]
			{
				PropertyNames.TextDecorationLine,
				PropertyNames.TextDecorationStyle,
				PropertyNames.TextDecorationColor
			});
			this.AddLonghand(PropertyNames.TextDecorationStyle, () => new CssTextDecorationStyleProperty(), false, false);
			this.AddLonghand(PropertyNames.TextDecorationLine, () => new CssTextDecorationLineProperty(), false, false);
			this.AddLonghand(PropertyNames.TextDecorationColor, () => new CssTextDecorationColorProperty(), true, false);
			this.AddLonghand(PropertyNames.TextIndent, () => new CssTextIndentProperty(), true, false);
			this.AddLonghand(PropertyNames.TextJustify, () => new CssTextJustifyProperty(), false, false);
			this.AddLonghand(PropertyNames.TextTransform, () => new CssTextTransformProperty(), false, false);
			this.AddLonghand(PropertyNames.TextShadow, () => new CssTextShadowProperty(), true, false);
			this.AddLonghand(PropertyNames.Transform, () => new CssTransformProperty(), true, false);
			this.AddLonghand(PropertyNames.TransformOrigin, () => new CssTransformOriginProperty(), true, false);
			this.AddLonghand(PropertyNames.TransformStyle, () => new CssTransformStyleProperty(), false, false);
			this.AddShorthand(PropertyNames.Transition, () => new CssTransitionProperty(), new string[]
			{
				PropertyNames.TransitionProperty,
				PropertyNames.TransitionDuration,
				PropertyNames.TransitionTimingFunction,
				PropertyNames.TransitionDelay
			});
			this.AddLonghand(PropertyNames.TransitionDelay, () => new CssTransitionDelayProperty(), false, false);
			this.AddLonghand(PropertyNames.TransitionDuration, () => new CssTransitionDurationProperty(), false, false);
			this.AddLonghand(PropertyNames.TransitionTimingFunction, () => new CssTransitionTimingFunctionProperty(), false, false);
			this.AddLonghand(PropertyNames.TransitionProperty, () => new CssTransitionPropertyProperty(), false, false);
			this.AddLonghand(PropertyNames.Top, () => new CssTopProperty(), true, false);
			this.AddLonghand(PropertyNames.UnicodeBidi, () => new CssUnicodeBidiProperty(), false, false);
			this.AddLonghand(PropertyNames.VerticalAlign, () => new CssVerticalAlignProperty(), true, false);
			this.AddLonghand(PropertyNames.Visibility, () => new CssVisibilityProperty(), true, false);
			this.AddLonghand(PropertyNames.WhiteSpace, () => new CssWhiteSpaceProperty(), false, false);
			this.AddLonghand(PropertyNames.Widows, () => new CssWidowsProperty(), false, false);
			this.AddLonghand(PropertyNames.Width, () => new CssWidthProperty(), true, false);
			this.AddLonghand(PropertyNames.WordBreak, () => new CssWordBreakProperty(), true, false);
			this.AddLonghand(PropertyNames.WordSpacing, () => new CssWordSpacingProperty(), true, false);
			this.AddLonghand(PropertyNames.WordWrap, () => new CssOverflowWrapProperty(), false, false);
			this.AddLonghand(PropertyNames.ZIndex, () => new CssZIndexProperty(), true, false);
			this.AddLonghand(PropertyNames.ObjectFit, () => new CssObjectFitProperty(), false, false);
			this.AddLonghand(PropertyNames.ObjectPosition, () => new CssObjectPositionProperty(), true, false);
			this.fonts.Add(PropertyNames.Src, () => new CssSrcProperty());
			this.fonts.Add(PropertyNames.UnicodeRange, () => new CssUnicodeRangeProperty());
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000095CF File Offset: 0x000077CF
		private void AddShorthand(string name, CssPropertyFactory.ShorthandCreator creator, params string[] longhands)
		{
			this.shorthands.Add(name, creator);
			this.mappings.Add(name, longhands);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000095EB File Offset: 0x000077EB
		private void AddLonghand(string name, CssPropertyFactory.LonghandCreator creator, bool animatable = false, bool font = false)
		{
			this.longhands.Add(name, creator);
			if (animatable)
			{
				this.animatables.Add(name);
			}
			if (font)
			{
				this.fonts.Add(name, creator);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000961A File Offset: 0x0000781A
		public CssProperty Create(string name)
		{
			return this.CreateLonghand(name) ?? this.CreateShorthand(name);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009630 File Offset: 0x00007830
		public CssProperty CreateFont(string name)
		{
			CssPropertyFactory.LonghandCreator longhandCreator = null;
			if (this.fonts.TryGetValue(name, out longhandCreator))
			{
				return longhandCreator();
			}
			return null;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009658 File Offset: 0x00007858
		public CssProperty CreateViewport(string name)
		{
			MediaFeature mediaFeature = Factory.MediaFeatures.Create(name);
			if (mediaFeature == null)
			{
				return null;
			}
			return new CssFeatureProperty(mediaFeature);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000967C File Offset: 0x0000787C
		public CssProperty CreateLonghand(string name)
		{
			CssPropertyFactory.LonghandCreator longhandCreator = null;
			if (this.longhands.TryGetValue(name, out longhandCreator))
			{
				return longhandCreator();
			}
			return null;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000096A4 File Offset: 0x000078A4
		public CssShorthandProperty CreateShorthand(string name)
		{
			CssPropertyFactory.ShorthandCreator shorthandCreator = null;
			if (this.shorthands.TryGetValue(name, out shorthandCreator))
			{
				return shorthandCreator();
			}
			return null;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000096CC File Offset: 0x000078CC
		public CssProperty[] CreateLonghandsFor(string name)
		{
			string[] array = this.GetLonghands(name);
			List<CssProperty> list = new List<CssProperty>();
			foreach (string text in array)
			{
				list.Add(this.CreateLonghand(text));
			}
			return list.ToArray();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000970C File Offset: 0x0000790C
		public bool IsLonghand(string name)
		{
			return this.longhands.ContainsKey(name);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000971A File Offset: 0x0000791A
		public bool IsShorthand(string name)
		{
			return this.shorthands.ContainsKey(name);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009728 File Offset: 0x00007928
		public bool IsAnimatable(string name)
		{
			if (this.IsLonghand(name))
			{
				return this.animatables.Contains(name);
			}
			foreach (string text in this.GetLonghands(name))
			{
				if (this.animatables.Contains(name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009776 File Offset: 0x00007976
		public string[] GetLonghands(string name)
		{
			if (this.mappings.ContainsKey(name))
			{
				return this.mappings[name];
			}
			return new string[0];
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00009799 File Offset: 0x00007999
		public IEnumerable<string> GetShorthands(string name)
		{
			foreach (KeyValuePair<string, string[]> keyValuePair in this.mappings)
			{
				if (keyValuePair.Value.Contains(name, StringComparison.OrdinalIgnoreCase))
				{
					yield return keyValuePair.Key;
				}
			}
			Dictionary<string, string[]>.Enumerator enumerator = default(Dictionary<string, string[]>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x040001C1 RID: 449
		private readonly Dictionary<string, CssPropertyFactory.LonghandCreator> longhands = new Dictionary<string, CssPropertyFactory.LonghandCreator>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001C2 RID: 450
		private readonly Dictionary<string, CssPropertyFactory.ShorthandCreator> shorthands = new Dictionary<string, CssPropertyFactory.ShorthandCreator>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001C3 RID: 451
		private readonly Dictionary<string, CssPropertyFactory.LonghandCreator> fonts = new Dictionary<string, CssPropertyFactory.LonghandCreator>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001C4 RID: 452
		private readonly Dictionary<string, string[]> mappings = new Dictionary<string, string[]>();

		// Token: 0x040001C5 RID: 453
		private readonly List<string> animatables = new List<string>();

		// Token: 0x02000423 RID: 1059
		// (Invoke) Token: 0x0600213E RID: 8510
		private delegate CssProperty LonghandCreator();

		// Token: 0x02000424 RID: 1060
		// (Invoke) Token: 0x06002142 RID: 8514
		private delegate CssShorthandProperty ShorthandCreator();
	}
}
