using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Css.ValueConverters;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css
{
	// Token: 0x02000100 RID: 256
	internal static class Converters
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x000395F5 File Offset: 0x000377F5
		public static IValueConverter Assign<T>(string identifier, T result)
		{
			return new IdentifierValueConverter<T>(identifier, result);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000395FE File Offset: 0x000377FE
		public static IValueConverter Toggle(string on, string off)
		{
			return Converters.Assign<bool>(on, true).Or(off, false);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0003960E File Offset: 0x0003780E
		public static IValueConverter WithOrder(params IValueConverter[] converters)
		{
			return new OrderedOptionsConverter(converters);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00039616 File Offset: 0x00037816
		public static IValueConverter WithAny(params IValueConverter[] converters)
		{
			return new UnorderedOptionsConverter(converters);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0003961E File Offset: 0x0003781E
		public static IValueConverter Continuous(IValueConverter converter)
		{
			return new ContinuousValueConverter(converter);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00039626 File Offset: 0x00037826
		private static IValueConverter Construct(Func<IValueConverter> f)
		{
			return f();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0003962E File Offset: 0x0003782E
		private static IValueConverter WithArgs(IValueConverter converter, int arguments)
		{
			return Converters.WithArgs(Enumerable.Repeat<IValueConverter>(converter, arguments).ToArray<IValueConverter>());
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00039641 File Offset: 0x00037841
		private static IValueConverter WithArgs(IValueConverter converter)
		{
			return new ArgumentsValueConverter(new IValueConverter[] { converter });
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00039652 File Offset: 0x00037852
		private static IValueConverter WithArgs(params IValueConverter[] converters)
		{
			return new ArgumentsValueConverter(converters);
		}

		// Token: 0x04000618 RID: 1560
		public static readonly IValueConverter LineWidthConverter = new StructValueConverter<Length>(new Func<IEnumerable<CssToken>, Length?>(ValueExtensions.ToBorderWidth));

		// Token: 0x04000619 RID: 1561
		public static readonly IValueConverter LengthConverter = new StructValueConverter<Length>(new Func<IEnumerable<CssToken>, Length?>(ValueExtensions.ToLength));

		// Token: 0x0400061A RID: 1562
		public static readonly IValueConverter ResolutionConverter = new StructValueConverter<Resolution>(new Func<IEnumerable<CssToken>, Resolution?>(ValueExtensions.ToResolution));

		// Token: 0x0400061B RID: 1563
		public static readonly IValueConverter FrequencyConverter = new StructValueConverter<Frequency>(new Func<IEnumerable<CssToken>, Frequency?>(ValueExtensions.ToFrequency));

		// Token: 0x0400061C RID: 1564
		public static readonly IValueConverter TimeConverter = new StructValueConverter<Time>(new Func<IEnumerable<CssToken>, Time?>(ValueExtensions.ToTime));

		// Token: 0x0400061D RID: 1565
		public static readonly IValueConverter UrlConverter = new UrlValueConverter();

		// Token: 0x0400061E RID: 1566
		public static readonly IValueConverter StringConverter = new StringValueConverter();

		// Token: 0x0400061F RID: 1567
		public static readonly IValueConverter EvenStringsConverter = new StringsValueConverter();

		// Token: 0x04000620 RID: 1568
		public static readonly IValueConverter LiteralsConverter = new IdentifierValueConverter(new Func<IEnumerable<CssToken>, string>(ValueExtensions.ToLiterals));

		// Token: 0x04000621 RID: 1569
		public static readonly IValueConverter IdentifierConverter = new IdentifierValueConverter(new Func<IEnumerable<CssToken>, string>(ValueExtensions.ToIdentifier));

		// Token: 0x04000622 RID: 1570
		public static readonly IValueConverter AnimatableConverter = new IdentifierValueConverter(new Func<IEnumerable<CssToken>, string>(ValueExtensions.ToAnimatableIdentifier));

		// Token: 0x04000623 RID: 1571
		public static readonly IValueConverter IntegerConverter = new StructValueConverter<int>(new Func<IEnumerable<CssToken>, int?>(ValueExtensions.ToInteger));

		// Token: 0x04000624 RID: 1572
		public static readonly IValueConverter NaturalIntegerConverter = new StructValueConverter<int>(new Func<IEnumerable<CssToken>, int?>(ValueExtensions.ToNaturalInteger));

		// Token: 0x04000625 RID: 1573
		public static readonly IValueConverter WeightIntegerConverter = new StructValueConverter<int>(new Func<IEnumerable<CssToken>, int?>(ValueExtensions.ToWeightInteger));

		// Token: 0x04000626 RID: 1574
		public static readonly IValueConverter PositiveIntegerConverter = new StructValueConverter<int>(new Func<IEnumerable<CssToken>, int?>(ValueExtensions.ToPositiveInteger));

		// Token: 0x04000627 RID: 1575
		public static readonly IValueConverter BinaryConverter = new StructValueConverter<int>(new Func<IEnumerable<CssToken>, int?>(ValueExtensions.ToBinary));

		// Token: 0x04000628 RID: 1576
		public static readonly IValueConverter AngleConverter = new StructValueConverter<Angle>(new Func<IEnumerable<CssToken>, Angle?>(ValueExtensions.ToAngle));

		// Token: 0x04000629 RID: 1577
		public static readonly IValueConverter NumberConverter = new StructValueConverter<float>(new Func<IEnumerable<CssToken>, float?>(ValueExtensions.ToSingle));

		// Token: 0x0400062A RID: 1578
		public static readonly IValueConverter NaturalNumberConverter = new StructValueConverter<float>(new Func<IEnumerable<CssToken>, float?>(ValueExtensions.ToNaturalSingle));

		// Token: 0x0400062B RID: 1579
		public static readonly IValueConverter PercentConverter = new StructValueConverter<Percent>(new Func<IEnumerable<CssToken>, Percent?>(ValueExtensions.ToPercent));

		// Token: 0x0400062C RID: 1580
		public static readonly IValueConverter RgbComponentConverter = new StructValueConverter<byte>(new Func<IEnumerable<CssToken>, byte?>(ValueExtensions.ToRgbComponent));

		// Token: 0x0400062D RID: 1581
		public static readonly IValueConverter AlphaValueConverter = new StructValueConverter<float>(new Func<IEnumerable<CssToken>, float?>(ValueExtensions.ToAlphaValue));

		// Token: 0x0400062E RID: 1582
		public static readonly IValueConverter PureColorConverter = new StructValueConverter<Color>(new Func<IEnumerable<CssToken>, Color?>(ValueExtensions.ToColor));

		// Token: 0x0400062F RID: 1583
		public static readonly IValueConverter LengthOrPercentConverter = new StructValueConverter<Length>(new Func<IEnumerable<CssToken>, Length?>(ValueExtensions.ToDistance));

		// Token: 0x04000630 RID: 1584
		public static readonly IValueConverter AngleNumberConverter = new StructValueConverter<Angle>(new Func<IEnumerable<CssToken>, Angle?>(ValueExtensions.ToAngleNumber));

		// Token: 0x04000631 RID: 1585
		public static readonly IValueConverter SideOrCornerConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.Assign<double>(Keywords.Left, -1.0).Or(Keywords.Right, 1.0).Option(0.0),
			Converters.Assign<double>(Keywords.Top, 1.0).Or(Keywords.Bottom, -1.0).Option(0.0)
		});

		// Token: 0x04000632 RID: 1586
		public static readonly IValueConverter PointConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter = Converters.Assign<Length>(Keywords.Left, Length.Zero).Or(Keywords.Right, new Length(100f, Length.Unit.Percent)).Or(Keywords.Center, new Length(50f, Length.Unit.Percent));
			IValueConverter valueConverter2 = Converters.Assign<Length>(Keywords.Top, Length.Zero).Or(Keywords.Bottom, new Length(100f, Length.Unit.Percent)).Or(Keywords.Center, new Length(50f, Length.Unit.Percent));
			IValueConverter valueConverter3 = valueConverter.Or(Converters.LengthOrPercentConverter).Required();
			IValueConverter valueConverter4 = valueConverter2.Or(Converters.LengthOrPercentConverter).Required();
			return Converters.LengthOrPercentConverter.Or(Converters.Toggle(Keywords.Left, Keywords.Right)).Or(Converters.Toggle(Keywords.Top, Keywords.Bottom)).Or(Keywords.Center, Point.Center)
				.Or(Converters.WithOrder(new IValueConverter[] { valueConverter3, valueConverter4 }))
				.Or(Converters.WithOrder(new IValueConverter[] { valueConverter4, valueConverter3 }))
				.Or(Converters.WithOrder(new IValueConverter[]
				{
					valueConverter,
					valueConverter2,
					Converters.LengthOrPercentConverter
				}))
				.Or(Converters.WithOrder(new IValueConverter[]
				{
					valueConverter,
					Converters.LengthOrPercentConverter,
					valueConverter2
				}))
				.Or(Converters.WithOrder(new IValueConverter[]
				{
					valueConverter,
					Converters.LengthOrPercentConverter,
					valueConverter2,
					Converters.LengthOrPercentConverter
				}));
		});

		// Token: 0x04000633 RID: 1587
		public static readonly IValueConverter AttrConverter = new FunctionValueConverter(FunctionNames.Attr, Converters.WithArgs(Converters.StringConverter.Or(Converters.IdentifierConverter)));

		// Token: 0x04000634 RID: 1588
		public static readonly IValueConverter StepsConverter = new FunctionValueConverter(FunctionNames.Steps, Converters.WithArgs(new IValueConverter[]
		{
			Converters.IntegerConverter.Required(),
			Converters.Assign<bool>(Keywords.Start, true).Or(Keywords.End, false).Option(false)
		}));

		// Token: 0x04000635 RID: 1589
		public static readonly IValueConverter CubicBezierConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter5 = Converters.NumberConverter.Required();
			return new FunctionValueConverter(FunctionNames.CubicBezier, Converters.WithArgs(new IValueConverter[] { valueConverter5, valueConverter5, valueConverter5, valueConverter5 }));
		});

		// Token: 0x04000636 RID: 1590
		public static readonly IValueConverter CounterConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter6 = Converters.IdentifierConverter.Required();
			IValueConverter valueConverter7 = Converters.IdentifierConverter.Option(Keywords.Decimal);
			IValueConverter valueConverter8 = Converters.StringConverter.Required();
			return new FunctionValueConverter(FunctionNames.Counter, Converters.WithArgs(new IValueConverter[] { valueConverter6, valueConverter7 }).Or(new FunctionValueConverter(FunctionNames.Counters, Converters.WithArgs(new IValueConverter[] { valueConverter6, valueConverter8, valueConverter7 }))));
		});

		// Token: 0x04000637 RID: 1591
		public static readonly IValueConverter ShapeConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter9 = Converters.LengthConverter.Required();
			return new FunctionValueConverter(FunctionNames.Rect, Converters.WithArgs(new IValueConverter[] { valueConverter9, valueConverter9, valueConverter9, valueConverter9 }).Or(Converters.WithArgs(Converters.LengthConverter.Many(4, 4))));
		}).OrAuto();

		// Token: 0x04000638 RID: 1592
		public static readonly IValueConverter LinearGradientConverter = Converters.Construct(() => new FunctionValueConverter(FunctionNames.LinearGradient, new LinearGradientConverter(false)).Or(new FunctionValueConverter(FunctionNames.RepeatingLinearGradient, new LinearGradientConverter(true))));

		// Token: 0x04000639 RID: 1593
		public static readonly IValueConverter RadialGradientConverter = Converters.Construct(() => new FunctionValueConverter(FunctionNames.RadialGradient, new RadialGradientConverter(false)).Or(new FunctionValueConverter(FunctionNames.RepeatingRadialGradient, new RadialGradientConverter(true))));

		// Token: 0x0400063A RID: 1594
		public static readonly IValueConverter RgbColorConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter10 = Converters.RgbComponentConverter.Required();
			return new FunctionValueConverter(FunctionNames.Rgb, Converters.WithArgs(new IValueConverter[] { valueConverter10, valueConverter10, valueConverter10 }));
		});

		// Token: 0x0400063B RID: 1595
		public static readonly IValueConverter RgbaColorConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter11 = Converters.RgbComponentConverter.Required();
			IValueConverter valueConverter12 = Converters.AlphaValueConverter.Required();
			return new FunctionValueConverter(FunctionNames.Rgba, Converters.WithArgs(new IValueConverter[] { valueConverter11, valueConverter11, valueConverter11, valueConverter12 }));
		});

		// Token: 0x0400063C RID: 1596
		public static readonly IValueConverter HslColorConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter13 = Converters.AngleNumberConverter.Required();
			IValueConverter valueConverter14 = Converters.PercentConverter.Required();
			return new FunctionValueConverter(FunctionNames.Hsl, Converters.WithArgs(new IValueConverter[] { valueConverter13, valueConverter14, valueConverter14 }));
		});

		// Token: 0x0400063D RID: 1597
		public static readonly IValueConverter HslaColorConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter15 = Converters.AngleNumberConverter.Required();
			IValueConverter valueConverter16 = Converters.PercentConverter.Required();
			IValueConverter valueConverter17 = Converters.AlphaValueConverter.Required();
			return new FunctionValueConverter(FunctionNames.Hsla, Converters.WithArgs(new IValueConverter[] { valueConverter15, valueConverter16, valueConverter16, valueConverter17 }));
		});

		// Token: 0x0400063E RID: 1598
		public static readonly IValueConverter GrayColorConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter18 = Converters.RgbComponentConverter.Required();
			IValueConverter valueConverter19 = Converters.AlphaValueConverter.Option(1f);
			return new FunctionValueConverter(FunctionNames.Gray, Converters.WithArgs(new IValueConverter[] { valueConverter18, valueConverter19 }));
		});

		// Token: 0x0400063F RID: 1599
		public static readonly IValueConverter HwbColorConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter20 = Converters.AngleNumberConverter.Required();
			IValueConverter valueConverter21 = Converters.PercentConverter.Required();
			IValueConverter valueConverter22 = Converters.AlphaValueConverter.Option(1f);
			return new FunctionValueConverter(FunctionNames.Hwb, Converters.WithArgs(new IValueConverter[] { valueConverter20, valueConverter21, valueConverter21, valueConverter22 }));
		});

		// Token: 0x04000640 RID: 1600
		public static readonly IValueConverter PerspectiveConverter = Converters.Construct(() => new FunctionValueConverter(FunctionNames.Perspective, Converters.WithArgs(Converters.LengthConverter)));

		// Token: 0x04000641 RID: 1601
		public static readonly IValueConverter MatrixTransformConverter = Converters.Construct(() => new FunctionValueConverter(FunctionNames.Matrix, Converters.WithArgs(Converters.NumberConverter, 6)).Or(new FunctionValueConverter(FunctionNames.Matrix3d, Converters.WithArgs(Converters.NumberConverter, 16))));

		// Token: 0x04000642 RID: 1602
		public static readonly IValueConverter TranslateTransformConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter23 = Converters.LengthOrPercentConverter.Required();
			IValueConverter valueConverter24 = Converters.LengthOrPercentConverter.Option(Length.Zero);
			return new FunctionValueConverter(FunctionNames.Translate, Converters.WithArgs(new IValueConverter[] { valueConverter23, valueConverter24 })).Or(new FunctionValueConverter(FunctionNames.Translate3d, Converters.WithArgs(new IValueConverter[] { valueConverter23, valueConverter24, valueConverter24 }))).Or(new FunctionValueConverter(FunctionNames.TranslateX, Converters.WithArgs(Converters.LengthOrPercentConverter))).Or(new FunctionValueConverter(FunctionNames.TranslateY, Converters.WithArgs(Converters.LengthOrPercentConverter)))
				.Or(new FunctionValueConverter(FunctionNames.TranslateZ, Converters.WithArgs(Converters.LengthOrPercentConverter)));
		});

		// Token: 0x04000643 RID: 1603
		public static readonly IValueConverter ScaleTransformConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter25 = Converters.NumberConverter.Required();
			IValueConverter valueConverter26 = Converters.NumberConverter.Option(float.NaN);
			return new FunctionValueConverter(FunctionNames.Scale, Converters.WithArgs(new IValueConverter[] { valueConverter25, valueConverter26 })).Or(new FunctionValueConverter(FunctionNames.Scale3d, Converters.WithArgs(new IValueConverter[] { valueConverter25, valueConverter26, valueConverter26 }))).Or(new FunctionValueConverter(FunctionNames.ScaleX, Converters.WithArgs(Converters.NumberConverter))).Or(new FunctionValueConverter(FunctionNames.ScaleY, Converters.WithArgs(Converters.NumberConverter)))
				.Or(new FunctionValueConverter(FunctionNames.ScaleZ, Converters.WithArgs(Converters.NumberConverter)));
		});

		// Token: 0x04000644 RID: 1604
		public static readonly IValueConverter RotateTransformConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter27 = Converters.NumberConverter.Required();
			return new FunctionValueConverter(FunctionNames.Rotate, Converters.WithArgs(Converters.AngleConverter)).Or(new FunctionValueConverter(FunctionNames.Rotate3d, Converters.WithArgs(new IValueConverter[]
			{
				valueConverter27,
				valueConverter27,
				valueConverter27,
				Converters.AngleConverter.Required()
			}))).Or(new FunctionValueConverter(FunctionNames.RotateX, Converters.WithArgs(Converters.AngleConverter))).Or(new FunctionValueConverter(FunctionNames.RotateY, Converters.WithArgs(Converters.AngleConverter)))
				.Or(new FunctionValueConverter(FunctionNames.RotateZ, Converters.WithArgs(Converters.AngleConverter)));
		});

		// Token: 0x04000645 RID: 1605
		public static readonly IValueConverter SkewTransformConverter = Converters.Construct(delegate
		{
			IValueConverter valueConverter28 = Converters.AngleConverter.Required();
			return new FunctionValueConverter(FunctionNames.Skew, Converters.WithArgs(new IValueConverter[] { valueConverter28, valueConverter28 })).Or(new FunctionValueConverter(FunctionNames.SkewX, Converters.WithArgs(Converters.AngleConverter))).Or(new FunctionValueConverter(FunctionNames.SkewY, Converters.WithArgs(Converters.AngleConverter)));
		});

		// Token: 0x04000646 RID: 1606
		public static readonly IValueConverter DefaultFontFamiliesConverter = Map.DefaultFontFamilies.ToConverter<string>();

		// Token: 0x04000647 RID: 1607
		public static readonly IValueConverter LineStyleConverter = Map.LineStyles.ToConverter<LineStyle>();

		// Token: 0x04000648 RID: 1608
		public static readonly IValueConverter BackgroundAttachmentConverter = Map.BackgroundAttachments.ToConverter<BackgroundAttachment>();

		// Token: 0x04000649 RID: 1609
		public static readonly IValueConverter BackgroundRepeatConverter = Map.BackgroundRepeats.ToConverter<BackgroundRepeat>();

		// Token: 0x0400064A RID: 1610
		public static readonly IValueConverter BoxModelConverter = Map.BoxModels.ToConverter<BoxModel>();

		// Token: 0x0400064B RID: 1611
		public static readonly IValueConverter AnimationDirectionConverter = Map.AnimationDirections.ToConverter<AnimationDirection>();

		// Token: 0x0400064C RID: 1612
		public static readonly IValueConverter AnimationFillStyleConverter = Map.AnimationFillStyles.ToConverter<AnimationFillStyle>();

		// Token: 0x0400064D RID: 1613
		public static readonly IValueConverter TextDecorationStyleConverter = Map.TextDecorationStyles.ToConverter<TextDecorationStyle>();

		// Token: 0x0400064E RID: 1614
		public static readonly IValueConverter TextDecorationLinesConverter = Map.TextDecorationLines.ToConverter<TextDecorationLine>().Many(1, 65535).OrNone();

		// Token: 0x0400064F RID: 1615
		public static readonly IValueConverter ListPositionConverter = Map.ListPositions.ToConverter<ListPosition>();

		// Token: 0x04000650 RID: 1616
		public static readonly IValueConverter ListStyleConverter = Map.ListStyles.ToConverter<ListStyle>();

		// Token: 0x04000651 RID: 1617
		public static readonly IValueConverter BreakModeConverter = Map.BreakModes.ToConverter<BreakMode>();

		// Token: 0x04000652 RID: 1618
		public static readonly IValueConverter BreakInsideModeConverter = Map.BreakInsideModes.ToConverter<BreakMode>();

		// Token: 0x04000653 RID: 1619
		public static readonly IValueConverter PageBreakModeConverter = Map.PageBreakModes.ToConverter<BreakMode>();

		// Token: 0x04000654 RID: 1620
		public static readonly IValueConverter UnicodeModeConverter = Map.UnicodeModes.ToConverter<UnicodeMode>();

		// Token: 0x04000655 RID: 1621
		public static readonly IValueConverter VisibilityConverter = Map.Visibilities.ToConverter<Visibility>();

		// Token: 0x04000656 RID: 1622
		public static readonly IValueConverter PlayStateConverter = Map.PlayStates.ToConverter<PlayState>();

		// Token: 0x04000657 RID: 1623
		public static readonly IValueConverter FontVariantConverter = Map.FontVariants.ToConverter<FontVariant>();

		// Token: 0x04000658 RID: 1624
		public static readonly IValueConverter DirectionModeConverter = Map.DirectionModes.ToConverter<DirectionMode>();

		// Token: 0x04000659 RID: 1625
		public static readonly IValueConverter HorizontalAlignmentConverter = Map.HorizontalAlignments.ToConverter<HorizontalAlignment>();

		// Token: 0x0400065A RID: 1626
		public static readonly IValueConverter VerticalAlignmentConverter = Map.VerticalAlignments.ToConverter<VerticalAlignment>();

		// Token: 0x0400065B RID: 1627
		public static readonly IValueConverter WhitespaceConverter = Map.WhitespaceModes.ToConverter<Whitespace>();

		// Token: 0x0400065C RID: 1628
		public static readonly IValueConverter TextTransformConverter = Map.TextTransforms.ToConverter<TextTransform>();

		// Token: 0x0400065D RID: 1629
		public static readonly IValueConverter TextAlignLastConverter = Map.TextAlignmentsLast.ToConverter<TextAlignLast>();

		// Token: 0x0400065E RID: 1630
		public static readonly IValueConverter TextAnchorConverter = Map.TextAnchors.ToConverter<TextAnchor>();

		// Token: 0x0400065F RID: 1631
		public static readonly IValueConverter TextJustifyConverter = Map.TextJustifyOptions.ToConverter<TextJustify>();

		// Token: 0x04000660 RID: 1632
		public static readonly IValueConverter ObjectFittingConverter = Map.ObjectFittings.ToConverter<ObjectFitting>();

		// Token: 0x04000661 RID: 1633
		public static readonly IValueConverter PositionModeConverter = Map.PositionModes.ToConverter<PositionMode>();

		// Token: 0x04000662 RID: 1634
		public static readonly IValueConverter OverflowModeConverter = Map.OverflowModes.ToConverter<OverflowMode>();

		// Token: 0x04000663 RID: 1635
		public static readonly IValueConverter FloatingConverter = Map.FloatingModes.ToConverter<Floating>();

		// Token: 0x04000664 RID: 1636
		public static readonly IValueConverter DisplayModeConverter = Map.DisplayModes.ToConverter<DisplayMode>();

		// Token: 0x04000665 RID: 1637
		public static readonly IValueConverter ClearModeConverter = Map.ClearModes.ToConverter<ClearMode>();

		// Token: 0x04000666 RID: 1638
		public static readonly IValueConverter FontStretchConverter = Map.FontStretches.ToConverter<FontStretch>();

		// Token: 0x04000667 RID: 1639
		public static readonly IValueConverter FontStyleConverter = Map.FontStyles.ToConverter<FontStyle>();

		// Token: 0x04000668 RID: 1640
		public static readonly IValueConverter FontWeightConverter = Map.FontWeights.ToConverter<FontWeight>();

		// Token: 0x04000669 RID: 1641
		public static readonly IValueConverter SystemFontConverter = Map.SystemFonts.ToConverter<SystemFont>();

		// Token: 0x0400066A RID: 1642
		public static readonly IValueConverter StrokeLinecapConverter = Map.StrokeLinecaps.ToConverter<StrokeLinecap>();

		// Token: 0x0400066B RID: 1643
		public static readonly IValueConverter StrokeLinejoinConverter = Map.StrokeLinejoins.ToConverter<StrokeLinejoin>();

		// Token: 0x0400066C RID: 1644
		public static readonly IValueConverter WordBreakConverter = Map.WordBreaks.ToConverter<WordBreak>();

		// Token: 0x0400066D RID: 1645
		public static readonly IValueConverter OverflowWrapConverter = Map.OverflowWraps.ToConverter<OverflowWrap>();

		// Token: 0x0400066E RID: 1646
		public static readonly IValueConverter OptionalIntegerConverter = Converters.IntegerConverter.OrAuto();

		// Token: 0x0400066F RID: 1647
		public static readonly IValueConverter PositiveOrInfiniteNumberConverter = Converters.NaturalNumberConverter.Or(Keywords.Infinite, float.PositiveInfinity);

		// Token: 0x04000670 RID: 1648
		public static readonly IValueConverter OptionalNumberConverter = Converters.NumberConverter.OrNone();

		// Token: 0x04000671 RID: 1649
		public static readonly IValueConverter LengthOrNormalConverter = Converters.LengthConverter.Or(Keywords.Normal, new Length(1f, Length.Unit.Em));

		// Token: 0x04000672 RID: 1650
		public static readonly IValueConverter OptionalLengthConverter = Converters.LengthConverter.Or(Keywords.Normal);

		// Token: 0x04000673 RID: 1651
		public static readonly IValueConverter AutoLengthConverter = Converters.LengthConverter.OrAuto();

		// Token: 0x04000674 RID: 1652
		public static readonly IValueConverter OptionalLengthOrPercentConverter = Converters.LengthOrPercentConverter.OrNone();

		// Token: 0x04000675 RID: 1653
		public static readonly IValueConverter AutoLengthOrPercentConverter = Converters.LengthOrPercentConverter.OrAuto();

		// Token: 0x04000676 RID: 1654
		public static readonly IValueConverter FontSizeConverter = Converters.LengthOrPercentConverter.Or(Map.FontSizes.ToConverter<FontSize>());

		// Token: 0x04000677 RID: 1655
		public static readonly IValueConverter LineHeightConverter = Converters.LengthOrPercentConverter.Or(Converters.NumberConverter).Or(Keywords.Normal);

		// Token: 0x04000678 RID: 1656
		public static readonly IValueConverter BorderSliceConverter = Converters.PercentConverter.Or(Converters.NumberConverter);

		// Token: 0x04000679 RID: 1657
		public static readonly IValueConverter ImageBorderWidthConverter = Converters.LengthOrPercentConverter.Or(Converters.NumberConverter).Or(Keywords.Auto);

		// Token: 0x0400067A RID: 1658
		public static readonly IValueConverter TransitionConverter = new DictionaryValueConverter<ITimingFunction>(Map.TimingFunctions).Or(Converters.StepsConverter).Or(Converters.CubicBezierConverter);

		// Token: 0x0400067B RID: 1659
		public static readonly IValueConverter GradientConverter = Converters.LinearGradientConverter.Or(Converters.RadialGradientConverter);

		// Token: 0x0400067C RID: 1660
		public static readonly IValueConverter TransformConverter = Converters.MatrixTransformConverter.Or(Converters.ScaleTransformConverter).Or(Converters.RotateTransformConverter).Or(Converters.TranslateTransformConverter)
			.Or(Converters.SkewTransformConverter)
			.Or(Converters.PerspectiveConverter);

		// Token: 0x0400067D RID: 1661
		public static readonly IValueConverter ColorConverter = Converters.PureColorConverter.Or(Converters.RgbColorConverter.Or(Converters.RgbaColorConverter)).Or(Converters.HslColorConverter.Or(Converters.HslaColorConverter)).Or(Converters.GrayColorConverter.Or(Converters.HwbColorConverter));

		// Token: 0x0400067E RID: 1662
		public static readonly IValueConverter CurrentColorConverter = Converters.ColorConverter.WithCurrentColor();

		// Token: 0x0400067F RID: 1663
		public static readonly IValueConverter InvertedColorConverter = Converters.CurrentColorConverter.Or(Keywords.Invert);

		// Token: 0x04000680 RID: 1664
		public static readonly IValueConverter PaintConverter = Converters.UrlConverter.Or(Converters.CurrentColorConverter.OrNone());

		// Token: 0x04000681 RID: 1665
		public static readonly IValueConverter StrokeDasharrayConverter = Converters.LengthOrPercentConverter.Or(Converters.NumberConverter).Many(1, 65535).OrNone();

		// Token: 0x04000682 RID: 1666
		public static readonly IValueConverter StrokeMiterlimitConverter = new StructValueConverter<float>(new Func<IEnumerable<CssToken>, float?>(ValueExtensions.ToGreaterOrEqualOneSingle));

		// Token: 0x04000683 RID: 1667
		public static readonly IValueConverter RatioConverter = Converters.WithOrder(new IValueConverter[]
		{
			Converters.IntegerConverter.Required(),
			Converters.IntegerConverter.StartsWithDelimiter().Required()
		});

		// Token: 0x04000684 RID: 1668
		public static readonly IValueConverter ShadowConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.Assign<bool>(Keywords.Inset, true).Option(false),
			Converters.LengthConverter.Many(2, 4).Required(),
			Converters.ColorConverter.WithCurrentColor().Option(Color.Black)
		});

		// Token: 0x04000685 RID: 1669
		public static readonly IValueConverter MultipleShadowConverter = Converters.ShadowConverter.FromList().OrNone();

		// Token: 0x04000686 RID: 1670
		public static readonly IValueConverter ImageSourceConverter = Converters.UrlConverter.Or(Converters.GradientConverter);

		// Token: 0x04000687 RID: 1671
		public static readonly IValueConverter OptionalImageSourceConverter = Converters.ImageSourceConverter.OrNone();

		// Token: 0x04000688 RID: 1672
		public static readonly IValueConverter MultipleImageSourceConverter = Converters.OptionalImageSourceConverter.FromList();

		// Token: 0x04000689 RID: 1673
		public static readonly IValueConverter BorderRadiusShorthandConverter = new BorderRadiusConverter();

		// Token: 0x0400068A RID: 1674
		public static readonly IValueConverter BorderRadiusConverter = Converters.WithOrder(new IValueConverter[]
		{
			Converters.LengthOrPercentConverter.Required(),
			Converters.LengthOrPercentConverter.Option()
		});

		// Token: 0x0400068B RID: 1675
		public static readonly IValueConverter FontFamiliesConverter = Converters.DefaultFontFamiliesConverter.Or(Converters.StringConverter).Or(Converters.LiteralsConverter).FromList();

		// Token: 0x0400068C RID: 1676
		public static readonly IValueConverter BackgroundSizeConverter = Converters.AutoLengthOrPercentConverter.Or(Keywords.Cover).Or(Keywords.Contain).Or(Converters.WithOrder(new IValueConverter[]
		{
			Converters.AutoLengthOrPercentConverter.Required(),
			Converters.AutoLengthOrPercentConverter.Required()
		}));

		// Token: 0x0400068D RID: 1677
		public static readonly IValueConverter BackgroundRepeatsConverter = Converters.BackgroundRepeatConverter.Or(Keywords.RepeatX).Or(Keywords.RepeatY).Or(Converters.WithOrder(new IValueConverter[]
		{
			Converters.BackgroundRepeatConverter.Required(),
			Converters.BackgroundRepeatConverter.Required()
		}));

		// Token: 0x0400068E RID: 1678
		public static readonly IValueConverter TableLayoutConverter = Converters.Toggle(Keywords.Fixed, Keywords.Auto);

		// Token: 0x0400068F RID: 1679
		public static readonly IValueConverter EmptyCellsConverter = Converters.Toggle(Keywords.Show, Keywords.Hide);

		// Token: 0x04000690 RID: 1680
		public static readonly IValueConverter CaptionSideConverter = Converters.Toggle(Keywords.Top, Keywords.Bottom);

		// Token: 0x04000691 RID: 1681
		public static readonly IValueConverter BackfaceVisibilityConverter = Converters.Toggle(Keywords.Visible, Keywords.Hidden);

		// Token: 0x04000692 RID: 1682
		public static readonly IValueConverter BorderCollapseConverter = Converters.Toggle(Keywords.Separate, Keywords.Collapse);

		// Token: 0x04000693 RID: 1683
		public static readonly IValueConverter BoxDecorationConverter = Converters.Toggle(Keywords.Clone, Keywords.Slice);

		// Token: 0x04000694 RID: 1684
		public static readonly IValueConverter ColumnSpanConverter = Converters.Toggle(Keywords.All, Keywords.None);

		// Token: 0x04000695 RID: 1685
		public static readonly IValueConverter ColumnFillConverter = Converters.Toggle(Keywords.Balance, Keywords.Auto);

		// Token: 0x04000696 RID: 1686
		public static IValueConverter Any = new AnyValueConverter();
	}
}
