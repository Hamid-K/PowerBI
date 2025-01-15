using System;
using System.Collections.Generic;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;

namespace AngleSharp.Css
{
	// Token: 0x02000107 RID: 263
	internal static class Map
	{
		// Token: 0x040006EC RID: 1772
		public static readonly Dictionary<string, Whitespace> WhitespaceModes = new Dictionary<string, Whitespace>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				Whitespace.Normal
			},
			{
				Keywords.Pre,
				Whitespace.Pre
			},
			{
				Keywords.Nowrap,
				Whitespace.NoWrap
			},
			{
				Keywords.PreWrap,
				Whitespace.PreWrap
			},
			{
				Keywords.PreLine,
				Whitespace.PreLine
			}
		};

		// Token: 0x040006ED RID: 1773
		public static readonly Dictionary<string, TextTransform> TextTransforms = new Dictionary<string, TextTransform>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				TextTransform.None
			},
			{
				Keywords.Capitalize,
				TextTransform.Capitalize
			},
			{
				Keywords.Uppercase,
				TextTransform.Uppercase
			},
			{
				Keywords.Lowercase,
				TextTransform.Lowercase
			},
			{
				Keywords.FullWidth,
				TextTransform.FullWidth
			}
		};

		// Token: 0x040006EE RID: 1774
		public static readonly Dictionary<string, TextAlignLast> TextAlignmentsLast = new Dictionary<string, TextAlignLast>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Auto,
				TextAlignLast.Auto
			},
			{
				Keywords.Start,
				TextAlignLast.Start
			},
			{
				Keywords.End,
				TextAlignLast.End
			},
			{
				Keywords.Right,
				TextAlignLast.Right
			},
			{
				Keywords.Left,
				TextAlignLast.Left
			},
			{
				Keywords.Center,
				TextAlignLast.Center
			},
			{
				Keywords.Justify,
				TextAlignLast.Justify
			}
		};

		// Token: 0x040006EF RID: 1775
		public static readonly Dictionary<string, TextAnchor> TextAnchors = new Dictionary<string, TextAnchor>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Start,
				TextAnchor.Start
			},
			{
				Keywords.Middle,
				TextAnchor.Middle
			},
			{
				Keywords.End,
				TextAnchor.End
			}
		};

		// Token: 0x040006F0 RID: 1776
		public static readonly Dictionary<string, TextJustify> TextJustifyOptions = new Dictionary<string, TextJustify>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Auto,
				TextJustify.Auto
			},
			{
				Keywords.Distribute,
				TextJustify.Distribute
			},
			{
				Keywords.DistributeAllLines,
				TextJustify.DistributeAllLines
			},
			{
				Keywords.DistributeCenterLast,
				TextJustify.DistributeCenterLast
			},
			{
				Keywords.InterCluster,
				TextJustify.InterCluster
			},
			{
				Keywords.InterIdeograph,
				TextJustify.InterIdeograph
			},
			{
				Keywords.InterWord,
				TextJustify.InterWord
			},
			{
				Keywords.Kashida,
				TextJustify.Kashida
			},
			{
				Keywords.Newspaper,
				TextJustify.Newspaper
			}
		};

		// Token: 0x040006F1 RID: 1777
		public static readonly Dictionary<string, HorizontalAlignment> HorizontalAlignments = new Dictionary<string, HorizontalAlignment>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Left,
				HorizontalAlignment.Left
			},
			{
				Keywords.Right,
				HorizontalAlignment.Right
			},
			{
				Keywords.Center,
				HorizontalAlignment.Center
			},
			{
				Keywords.Justify,
				HorizontalAlignment.Justify
			}
		};

		// Token: 0x040006F2 RID: 1778
		public static readonly Dictionary<string, VerticalAlignment> VerticalAlignments = new Dictionary<string, VerticalAlignment>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Baseline,
				VerticalAlignment.Baseline
			},
			{
				Keywords.Sub,
				VerticalAlignment.Sub
			},
			{
				Keywords.Super,
				VerticalAlignment.Super
			},
			{
				Keywords.TextTop,
				VerticalAlignment.TextTop
			},
			{
				Keywords.TextBottom,
				VerticalAlignment.TextBottom
			},
			{
				Keywords.Middle,
				VerticalAlignment.Middle
			},
			{
				Keywords.Top,
				VerticalAlignment.Top
			},
			{
				Keywords.Bottom,
				VerticalAlignment.Bottom
			}
		};

		// Token: 0x040006F3 RID: 1779
		public static readonly Dictionary<string, LineStyle> LineStyles = new Dictionary<string, LineStyle>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				LineStyle.None
			},
			{
				Keywords.Solid,
				LineStyle.Solid
			},
			{
				Keywords.Double,
				LineStyle.Double
			},
			{
				Keywords.Dotted,
				LineStyle.Dotted
			},
			{
				Keywords.Dashed,
				LineStyle.Dashed
			},
			{
				Keywords.Inset,
				LineStyle.Inset
			},
			{
				Keywords.Outset,
				LineStyle.Outset
			},
			{
				Keywords.Ridge,
				LineStyle.Ridge
			},
			{
				Keywords.Groove,
				LineStyle.Groove
			},
			{
				Keywords.Hidden,
				LineStyle.Hidden
			}
		};

		// Token: 0x040006F4 RID: 1780
		public static readonly Dictionary<string, BoxModel> BoxModels = new Dictionary<string, BoxModel>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.BorderBox,
				BoxModel.BorderBox
			},
			{
				Keywords.PaddingBox,
				BoxModel.PaddingBox
			},
			{
				Keywords.ContentBox,
				BoxModel.ContentBox
			}
		};

		// Token: 0x040006F5 RID: 1781
		public static readonly Dictionary<string, ITimingFunction> TimingFunctions = new Dictionary<string, ITimingFunction>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Ease,
				new CubicBezierTimingFunction(0.25f, 0.1f, 0.25f, 1f)
			},
			{
				Keywords.EaseIn,
				new CubicBezierTimingFunction(0.42f, 0f, 1f, 1f)
			},
			{
				Keywords.EaseOut,
				new CubicBezierTimingFunction(0f, 0f, 0.58f, 1f)
			},
			{
				Keywords.EaseInOut,
				new CubicBezierTimingFunction(0.42f, 0f, 0.58f, 1f)
			},
			{
				Keywords.Linear,
				new CubicBezierTimingFunction(0f, 0f, 1f, 1f)
			},
			{
				Keywords.StepStart,
				new StepsTimingFunction(1, true)
			},
			{
				Keywords.StepEnd,
				new StepsTimingFunction(1, false)
			}
		};

		// Token: 0x040006F6 RID: 1782
		public static readonly Dictionary<string, AnimationFillStyle> AnimationFillStyles = new Dictionary<string, AnimationFillStyle>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				AnimationFillStyle.None
			},
			{
				Keywords.Forwards,
				AnimationFillStyle.Forwards
			},
			{
				Keywords.Backwards,
				AnimationFillStyle.Backwards
			},
			{
				Keywords.Both,
				AnimationFillStyle.Both
			}
		};

		// Token: 0x040006F7 RID: 1783
		public static readonly Dictionary<string, AnimationDirection> AnimationDirections = new Dictionary<string, AnimationDirection>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				AnimationDirection.Normal
			},
			{
				Keywords.Reverse,
				AnimationDirection.Reverse
			},
			{
				Keywords.Alternate,
				AnimationDirection.Alternate
			},
			{
				Keywords.AlternateReverse,
				AnimationDirection.AlternateReverse
			}
		};

		// Token: 0x040006F8 RID: 1784
		public static readonly Dictionary<string, Visibility> Visibilities = new Dictionary<string, Visibility>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Visible,
				Visibility.Visible
			},
			{
				Keywords.Hidden,
				Visibility.Hidden
			},
			{
				Keywords.Collapse,
				Visibility.Collapse
			}
		};

		// Token: 0x040006F9 RID: 1785
		public static readonly Dictionary<string, PlayState> PlayStates = new Dictionary<string, PlayState>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Running,
				PlayState.Running
			},
			{
				Keywords.Paused,
				PlayState.Paused
			}
		};

		// Token: 0x040006FA RID: 1786
		public static readonly Dictionary<string, FontVariant> FontVariants = new Dictionary<string, FontVariant>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				FontVariant.Normal
			},
			{
				Keywords.SmallCaps,
				FontVariant.SmallCaps
			}
		};

		// Token: 0x040006FB RID: 1787
		public static readonly Dictionary<string, DirectionMode> DirectionModes = new Dictionary<string, DirectionMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Ltr,
				DirectionMode.Ltr
			},
			{
				Keywords.Rtl,
				DirectionMode.Rtl
			}
		};

		// Token: 0x040006FC RID: 1788
		public static readonly Dictionary<string, ListStyle> ListStyles = new Dictionary<string, ListStyle>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Disc,
				ListStyle.Disc
			},
			{
				Keywords.Circle,
				ListStyle.Circle
			},
			{
				Keywords.Square,
				ListStyle.Square
			},
			{
				Keywords.Decimal,
				ListStyle.Decimal
			},
			{
				Keywords.DecimalLeadingZero,
				ListStyle.DecimalLeadingZero
			},
			{
				Keywords.LowerRoman,
				ListStyle.LowerRoman
			},
			{
				Keywords.UpperRoman,
				ListStyle.UpperRoman
			},
			{
				Keywords.LowerGreek,
				ListStyle.LowerGreek
			},
			{
				Keywords.LowerLatin,
				ListStyle.LowerLatin
			},
			{
				Keywords.UpperLatin,
				ListStyle.UpperLatin
			},
			{
				Keywords.Armenian,
				ListStyle.Armenian
			},
			{
				Keywords.Georgian,
				ListStyle.Georgian
			},
			{
				Keywords.LowerAlpha,
				ListStyle.LowerLatin
			},
			{
				Keywords.UpperAlpha,
				ListStyle.UpperLatin
			},
			{
				Keywords.None,
				ListStyle.None
			}
		};

		// Token: 0x040006FD RID: 1789
		public static readonly Dictionary<string, ListPosition> ListPositions = new Dictionary<string, ListPosition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Inside,
				ListPosition.Inside
			},
			{
				Keywords.Outside,
				ListPosition.Outside
			}
		};

		// Token: 0x040006FE RID: 1790
		public static readonly Dictionary<string, FontSize> FontSizes = new Dictionary<string, FontSize>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.XxSmall,
				FontSize.Tiny
			},
			{
				Keywords.XSmall,
				FontSize.Little
			},
			{
				Keywords.Small,
				FontSize.Small
			},
			{
				Keywords.Medium,
				FontSize.Medium
			},
			{
				Keywords.Large,
				FontSize.Large
			},
			{
				Keywords.XLarge,
				FontSize.Big
			},
			{
				Keywords.XxLarge,
				FontSize.Huge
			},
			{
				Keywords.Larger,
				FontSize.Smaller
			},
			{
				Keywords.Smaller,
				FontSize.Larger
			}
		};

		// Token: 0x040006FF RID: 1791
		public static readonly Dictionary<string, TextDecorationStyle> TextDecorationStyles = new Dictionary<string, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Solid,
				TextDecorationStyle.Solid
			},
			{
				Keywords.Double,
				TextDecorationStyle.Double
			},
			{
				Keywords.Dotted,
				TextDecorationStyle.Dotted
			},
			{
				Keywords.Dashed,
				TextDecorationStyle.Dashed
			},
			{
				Keywords.Wavy,
				TextDecorationStyle.Wavy
			}
		};

		// Token: 0x04000700 RID: 1792
		public static readonly Dictionary<string, TextDecorationLine> TextDecorationLines = new Dictionary<string, TextDecorationLine>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Underline,
				TextDecorationLine.Underline
			},
			{
				Keywords.Overline,
				TextDecorationLine.Overline
			},
			{
				Keywords.LineThrough,
				TextDecorationLine.LineThrough
			},
			{
				Keywords.Blink,
				TextDecorationLine.Blink
			}
		};

		// Token: 0x04000701 RID: 1793
		public static readonly Dictionary<string, BorderRepeat> BorderRepeatModes = new Dictionary<string, BorderRepeat>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Stretch,
				BorderRepeat.Stretch
			},
			{
				Keywords.Repeat,
				BorderRepeat.Repeat
			},
			{
				Keywords.Round,
				BorderRepeat.Round
			}
		};

		// Token: 0x04000702 RID: 1794
		public static readonly Dictionary<string, string> DefaultFontFamilies = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Serif,
				"Times New Roman"
			},
			{
				Keywords.SansSerif,
				"Arial"
			},
			{
				Keywords.Monospace,
				"Consolas"
			},
			{
				Keywords.Cursive,
				"Cursive"
			},
			{
				Keywords.Fantasy,
				"Comic Sans"
			}
		};

		// Token: 0x04000703 RID: 1795
		public static readonly Dictionary<string, BackgroundAttachment> BackgroundAttachments = new Dictionary<string, BackgroundAttachment>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Fixed,
				BackgroundAttachment.Fixed
			},
			{
				Keywords.Local,
				BackgroundAttachment.Local
			},
			{
				Keywords.Scroll,
				BackgroundAttachment.Scroll
			}
		};

		// Token: 0x04000704 RID: 1796
		public static readonly Dictionary<string, FontStyle> FontStyles = new Dictionary<string, FontStyle>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				FontStyle.Normal
			},
			{
				Keywords.Italic,
				FontStyle.Italic
			},
			{
				Keywords.Oblique,
				FontStyle.Oblique
			}
		};

		// Token: 0x04000705 RID: 1797
		public static readonly Dictionary<string, FontStretch> FontStretches = new Dictionary<string, FontStretch>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				FontStretch.Normal
			},
			{
				Keywords.UltraCondensed,
				FontStretch.UltraCondensed
			},
			{
				Keywords.ExtraCondensed,
				FontStretch.ExtraCondensed
			},
			{
				Keywords.Condensed,
				FontStretch.Condensed
			},
			{
				Keywords.SemiCondensed,
				FontStretch.SemiCondensed
			},
			{
				Keywords.SemiExpanded,
				FontStretch.SemiExpanded
			},
			{
				Keywords.Expanded,
				FontStretch.Expanded
			},
			{
				Keywords.ExtraExpanded,
				FontStretch.ExtraExpanded
			},
			{
				Keywords.UltraExpanded,
				FontStretch.UltraExpanded
			}
		};

		// Token: 0x04000706 RID: 1798
		public static readonly Dictionary<string, BreakMode> BreakModes = new Dictionary<string, BreakMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Auto,
				BreakMode.Auto
			},
			{
				Keywords.Always,
				BreakMode.Always
			},
			{
				Keywords.Avoid,
				BreakMode.Avoid
			},
			{
				Keywords.Left,
				BreakMode.Left
			},
			{
				Keywords.Right,
				BreakMode.Right
			},
			{
				Keywords.Page,
				BreakMode.Page
			},
			{
				Keywords.Column,
				BreakMode.Column
			},
			{
				Keywords.AvoidPage,
				BreakMode.AvoidPage
			},
			{
				Keywords.AvoidColumn,
				BreakMode.AvoidColumn
			}
		};

		// Token: 0x04000707 RID: 1799
		public static readonly Dictionary<string, BreakMode> PageBreakModes = new Dictionary<string, BreakMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Auto,
				BreakMode.Auto
			},
			{
				Keywords.Always,
				BreakMode.Always
			},
			{
				Keywords.Avoid,
				BreakMode.Avoid
			},
			{
				Keywords.Left,
				BreakMode.Left
			},
			{
				Keywords.Right,
				BreakMode.Right
			}
		};

		// Token: 0x04000708 RID: 1800
		public static readonly Dictionary<string, BreakMode> BreakInsideModes = new Dictionary<string, BreakMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Auto,
				BreakMode.Auto
			},
			{
				Keywords.Avoid,
				BreakMode.Avoid
			},
			{
				Keywords.AvoidPage,
				BreakMode.AvoidPage
			},
			{
				Keywords.AvoidColumn,
				BreakMode.AvoidColumn
			},
			{
				Keywords.AvoidRegion,
				BreakMode.AvoidRegion
			}
		};

		// Token: 0x04000709 RID: 1801
		public static readonly Dictionary<string, float> HorizontalModes = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Left,
				0f
			},
			{
				Keywords.Center,
				0.5f
			},
			{
				Keywords.Right,
				1f
			}
		};

		// Token: 0x0400070A RID: 1802
		public static readonly Dictionary<string, float> VerticalModes = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Top,
				0f
			},
			{
				Keywords.Center,
				0.5f
			},
			{
				Keywords.Bottom,
				1f
			}
		};

		// Token: 0x0400070B RID: 1803
		public static readonly Dictionary<string, UnicodeMode> UnicodeModes = new Dictionary<string, UnicodeMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				UnicodeMode.Normal
			},
			{
				Keywords.Embed,
				UnicodeMode.Embed
			},
			{
				Keywords.Isolate,
				UnicodeMode.Isolate
			},
			{
				Keywords.IsolateOverride,
				UnicodeMode.IsolateOverride
			},
			{
				Keywords.BidiOverride,
				UnicodeMode.BidiOverride
			},
			{
				Keywords.Plaintext,
				UnicodeMode.Plaintext
			}
		};

		// Token: 0x0400070C RID: 1804
		public static readonly Dictionary<string, SystemCursor> Cursors = new Dictionary<string, SystemCursor>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Auto,
				SystemCursor.Auto
			},
			{
				Keywords.Default,
				SystemCursor.Default
			},
			{
				Keywords.None,
				SystemCursor.None
			},
			{
				Keywords.ContextMenu,
				SystemCursor.ContextMenu
			},
			{
				Keywords.Help,
				SystemCursor.Help
			},
			{
				Keywords.Pointer,
				SystemCursor.Pointer
			},
			{
				Keywords.Progress,
				SystemCursor.Progress
			},
			{
				Keywords.Wait,
				SystemCursor.Wait
			},
			{
				Keywords.Cell,
				SystemCursor.Cell
			},
			{
				Keywords.Crosshair,
				SystemCursor.Crosshair
			},
			{
				Keywords.Text,
				SystemCursor.Text
			},
			{
				Keywords.VerticalText,
				SystemCursor.VerticalText
			},
			{
				Keywords.Alias,
				SystemCursor.Alias
			},
			{
				Keywords.Copy,
				SystemCursor.Copy
			},
			{
				Keywords.Move,
				SystemCursor.Move
			},
			{
				Keywords.NoDrop,
				SystemCursor.NoDrop
			},
			{
				Keywords.NotAllowed,
				SystemCursor.NotAllowed
			},
			{
				Keywords.EastResize,
				SystemCursor.EResize
			},
			{
				Keywords.NorthResize,
				SystemCursor.NResize
			},
			{
				Keywords.NorthEastResize,
				SystemCursor.NeResize
			},
			{
				Keywords.NorthWestResize,
				SystemCursor.NwResize
			},
			{
				Keywords.SouthResize,
				SystemCursor.SResize
			},
			{
				Keywords.SouthEastResize,
				SystemCursor.SeResize
			},
			{
				Keywords.SouthWestResize,
				SystemCursor.WResize
			},
			{
				Keywords.WestResize,
				SystemCursor.WResize
			},
			{
				Keywords.EastWestResize,
				SystemCursor.EwResize
			},
			{
				Keywords.NorthSouthResize,
				SystemCursor.NsResize
			},
			{
				Keywords.NorthEastSouthWestResize,
				SystemCursor.NeswResize
			},
			{
				Keywords.NorthWestSouthEastResize,
				SystemCursor.NwseResize
			},
			{
				Keywords.ColResize,
				SystemCursor.ColResize
			},
			{
				Keywords.RowResize,
				SystemCursor.RowResize
			},
			{
				Keywords.AllScroll,
				SystemCursor.AllScroll
			},
			{
				Keywords.ZoomIn,
				SystemCursor.ZoomIn
			},
			{
				Keywords.ZoomOut,
				SystemCursor.ZoomOut
			},
			{
				Keywords.Grab,
				SystemCursor.Grab
			},
			{
				Keywords.Grabbing,
				SystemCursor.Grabbing
			}
		};

		// Token: 0x0400070D RID: 1805
		public static readonly Dictionary<string, PositionMode> PositionModes = new Dictionary<string, PositionMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Static,
				PositionMode.Static
			},
			{
				Keywords.Relative,
				PositionMode.Relative
			},
			{
				Keywords.Absolute,
				PositionMode.Absolute
			},
			{
				Keywords.Sticky,
				PositionMode.Sticky
			},
			{
				Keywords.Fixed,
				PositionMode.Fixed
			}
		};

		// Token: 0x0400070E RID: 1806
		public static readonly Dictionary<string, OverflowMode> OverflowModes = new Dictionary<string, OverflowMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Visible,
				OverflowMode.Visible
			},
			{
				Keywords.Hidden,
				OverflowMode.Hidden
			},
			{
				Keywords.Scroll,
				OverflowMode.Scroll
			},
			{
				Keywords.Auto,
				OverflowMode.Auto
			}
		};

		// Token: 0x0400070F RID: 1807
		public static readonly Dictionary<string, Floating> FloatingModes = new Dictionary<string, Floating>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				Floating.None
			},
			{
				Keywords.Left,
				Floating.Left
			},
			{
				Keywords.Right,
				Floating.Right
			}
		};

		// Token: 0x04000710 RID: 1808
		public static readonly Dictionary<string, DisplayMode> DisplayModes = new Dictionary<string, DisplayMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				DisplayMode.None
			},
			{
				Keywords.Inline,
				DisplayMode.Inline
			},
			{
				Keywords.Block,
				DisplayMode.Block
			},
			{
				Keywords.InlineBlock,
				DisplayMode.InlineBlock
			},
			{
				Keywords.ListItem,
				DisplayMode.ListItem
			},
			{
				Keywords.InlineTable,
				DisplayMode.InlineTable
			},
			{
				Keywords.Table,
				DisplayMode.Table
			},
			{
				Keywords.TableCaption,
				DisplayMode.TableCaption
			},
			{
				Keywords.TableCell,
				DisplayMode.TableCell
			},
			{
				Keywords.TableColumn,
				DisplayMode.TableColumn
			},
			{
				Keywords.TableColumnGroup,
				DisplayMode.TableColumnGroup
			},
			{
				Keywords.TableFooterGroup,
				DisplayMode.TableFooterGroup
			},
			{
				Keywords.TableHeaderGroup,
				DisplayMode.TableHeaderGroup
			},
			{
				Keywords.TableRow,
				DisplayMode.TableRow
			},
			{
				Keywords.TableRowGroup,
				DisplayMode.TableRowGroup
			},
			{
				Keywords.Flex,
				DisplayMode.Flex
			},
			{
				Keywords.InlineFlex,
				DisplayMode.InlineFlex
			},
			{
				Keywords.Grid,
				DisplayMode.Grid
			},
			{
				Keywords.InlineGrid,
				DisplayMode.InlineGrid
			}
		};

		// Token: 0x04000711 RID: 1809
		public static readonly Dictionary<string, ClearMode> ClearModes = new Dictionary<string, ClearMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				ClearMode.None
			},
			{
				Keywords.Left,
				ClearMode.Left
			},
			{
				Keywords.Right,
				ClearMode.Right
			},
			{
				Keywords.Both,
				ClearMode.Both
			}
		};

		// Token: 0x04000712 RID: 1810
		public static readonly Dictionary<string, BackgroundRepeat> BackgroundRepeats = new Dictionary<string, BackgroundRepeat>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.NoRepeat,
				BackgroundRepeat.NoRepeat
			},
			{
				Keywords.Repeat,
				BackgroundRepeat.Repeat
			},
			{
				Keywords.Round,
				BackgroundRepeat.Round
			},
			{
				Keywords.Space,
				BackgroundRepeat.Space
			}
		};

		// Token: 0x04000713 RID: 1811
		public static readonly Dictionary<string, BlendMode> BlendModes = new Dictionary<string, BlendMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Color,
				BlendMode.Color
			},
			{
				Keywords.ColorBurn,
				BlendMode.ColorBurn
			},
			{
				Keywords.ColorDodge,
				BlendMode.ColorDodge
			},
			{
				Keywords.Darken,
				BlendMode.Darken
			},
			{
				Keywords.Difference,
				BlendMode.Difference
			},
			{
				Keywords.Exclusion,
				BlendMode.Exclusion
			},
			{
				Keywords.HardLight,
				BlendMode.HardLight
			},
			{
				Keywords.Hue,
				BlendMode.Hue
			},
			{
				Keywords.Lighten,
				BlendMode.Lighten
			},
			{
				Keywords.Luminosity,
				BlendMode.Luminosity
			},
			{
				Keywords.Multiply,
				BlendMode.Multiply
			},
			{
				Keywords.Normal,
				BlendMode.Normal
			},
			{
				Keywords.Overlay,
				BlendMode.Overlay
			},
			{
				Keywords.Saturation,
				BlendMode.Saturation
			},
			{
				Keywords.Screen,
				BlendMode.Screen
			},
			{
				Keywords.SoftLight,
				BlendMode.SoftLight
			}
		};

		// Token: 0x04000714 RID: 1812
		public static readonly Dictionary<string, UpdateFrequency> UpdateFrequencies = new Dictionary<string, UpdateFrequency>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				UpdateFrequency.None
			},
			{
				Keywords.Slow,
				UpdateFrequency.Slow
			},
			{
				Keywords.Normal,
				UpdateFrequency.Normal
			}
		};

		// Token: 0x04000715 RID: 1813
		public static readonly Dictionary<string, ScriptingState> ScriptingStates = new Dictionary<string, ScriptingState>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				ScriptingState.None
			},
			{
				Keywords.InitialOnly,
				ScriptingState.InitialOnly
			},
			{
				Keywords.Enabled,
				ScriptingState.Enabled
			}
		};

		// Token: 0x04000716 RID: 1814
		public static readonly Dictionary<string, PointerAccuracy> PointerAccuracies = new Dictionary<string, PointerAccuracy>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				PointerAccuracy.None
			},
			{
				Keywords.Coarse,
				PointerAccuracy.Coarse
			},
			{
				Keywords.Fine,
				PointerAccuracy.Fine
			}
		};

		// Token: 0x04000717 RID: 1815
		public static readonly Dictionary<string, HoverAbility> HoverAbilities = new Dictionary<string, HoverAbility>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				HoverAbility.None
			},
			{
				Keywords.OnDemand,
				HoverAbility.OnDemand
			},
			{
				Keywords.Hover,
				HoverAbility.Hover
			}
		};

		// Token: 0x04000718 RID: 1816
		public static readonly Dictionary<string, RadialGradient.SizeMode> RadialGradientSizeModes = new Dictionary<string, RadialGradient.SizeMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.ClosestSide,
				RadialGradient.SizeMode.ClosestSide
			},
			{
				Keywords.FarthestSide,
				RadialGradient.SizeMode.FarthestSide
			},
			{
				Keywords.ClosestCorner,
				RadialGradient.SizeMode.ClosestCorner
			},
			{
				Keywords.FarthestCorner,
				RadialGradient.SizeMode.FarthestCorner
			}
		};

		// Token: 0x04000719 RID: 1817
		public static readonly Dictionary<string, ObjectFitting> ObjectFittings = new Dictionary<string, ObjectFitting>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.None,
				ObjectFitting.None
			},
			{
				Keywords.Cover,
				ObjectFitting.Cover
			},
			{
				Keywords.Contain,
				ObjectFitting.Contain
			},
			{
				Keywords.Fill,
				ObjectFitting.Fill
			},
			{
				Keywords.ScaleDown,
				ObjectFitting.ScaleDown
			}
		};

		// Token: 0x0400071A RID: 1818
		public static readonly Dictionary<string, FontWeight> FontWeights = new Dictionary<string, FontWeight>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				FontWeight.Normal
			},
			{
				Keywords.Bold,
				FontWeight.Bold
			},
			{
				Keywords.Bolder,
				FontWeight.Bolder
			},
			{
				Keywords.Lighter,
				FontWeight.Lighter
			}
		};

		// Token: 0x0400071B RID: 1819
		public static readonly Dictionary<string, SystemFont> SystemFonts = new Dictionary<string, SystemFont>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Caption,
				SystemFont.Caption
			},
			{
				Keywords.Icon,
				SystemFont.Icon
			},
			{
				Keywords.Menu,
				SystemFont.Menu
			},
			{
				Keywords.MessageBox,
				SystemFont.MessageBox
			},
			{
				Keywords.SmallCaption,
				SystemFont.SmallCaption
			},
			{
				Keywords.StatusBar,
				SystemFont.StatusBar
			}
		};

		// Token: 0x0400071C RID: 1820
		public static readonly Dictionary<string, StrokeLinecap> StrokeLinecaps = new Dictionary<string, StrokeLinecap>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Butt,
				StrokeLinecap.Butt
			},
			{
				Keywords.Round,
				StrokeLinecap.Round
			},
			{
				Keywords.Square,
				StrokeLinecap.Square
			}
		};

		// Token: 0x0400071D RID: 1821
		public static readonly Dictionary<string, StrokeLinejoin> StrokeLinejoins = new Dictionary<string, StrokeLinejoin>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Miter,
				StrokeLinejoin.Miter
			},
			{
				Keywords.Round,
				StrokeLinejoin.Round
			},
			{
				Keywords.Bevel,
				StrokeLinejoin.Bevel
			}
		};

		// Token: 0x0400071E RID: 1822
		public static readonly Dictionary<string, WordBreak> WordBreaks = new Dictionary<string, WordBreak>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				WordBreak.Normal
			},
			{
				Keywords.BreakAll,
				WordBreak.BreakAll
			},
			{
				Keywords.KeepAll,
				WordBreak.KeepAll
			}
		};

		// Token: 0x0400071F RID: 1823
		public static readonly Dictionary<string, OverflowWrap> OverflowWraps = new Dictionary<string, OverflowWrap>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.Normal,
				OverflowWrap.Normal
			},
			{
				Keywords.BreakWord,
				OverflowWrap.BreakWord
			}
		};
	}
}
