using System;
using System.Collections.Generic;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000137 RID: 311
	internal sealed class RadialGradientConverter : GradientConverter
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0003FC84 File Offset: 0x0003DE84
		public RadialGradientConverter(bool repeating)
			: base(repeating)
		{
			IValueConverter valueConverter = Converters.PointConverter.StartsWithKeyword(Keywords.At).Option(Point.Center);
			IValueConverter valueConverter2 = Converters.WithOrder(new IValueConverter[]
			{
				Converters.WithAny(new IValueConverter[]
				{
					Converters.Assign<bool>(Keywords.Circle, true).Option(true),
					Converters.LengthConverter.Option()
				}),
				valueConverter
			});
			IValueConverter valueConverter3 = Converters.WithOrder(new IValueConverter[]
			{
				Converters.WithAny(new IValueConverter[]
				{
					Converters.Assign<bool>(Keywords.Ellipse, false).Option(false),
					Converters.LengthOrPercentConverter.Many(2, 2).Option()
				}),
				valueConverter
			});
			IValueConverter valueConverter4 = Converters.WithOrder(new IValueConverter[]
			{
				Converters.WithAny(new IValueConverter[]
				{
					Converters.Toggle(Keywords.Circle, Keywords.Ellipse).Option(false),
					Map.RadialGradientSizeModes.ToConverter<RadialGradient.SizeMode>()
				}),
				valueConverter
			});
			this._converter = valueConverter2.Or(valueConverter3.Or(valueConverter4));
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0003FD8D File Offset: 0x0003DF8D
		protected override IPropertyValue ConvertFirstArgument(IEnumerable<CssToken> value)
		{
			return this._converter.Convert(value);
		}

		// Token: 0x040008F1 RID: 2289
		private readonly IValueConverter _converter;
	}
}
