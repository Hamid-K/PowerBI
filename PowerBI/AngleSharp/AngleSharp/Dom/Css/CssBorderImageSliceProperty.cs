using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000282 RID: 642
	internal sealed class CssBorderImageSliceProperty : CssProperty
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x0004C387 File Offset: 0x0004A587
		internal CssBorderImageSliceProperty()
			: base(PropertyNames.BorderImageSlice, PropertyFlags.None)
		{
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0004C395 File Offset: 0x0004A595
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderImageSliceProperty.StyleConverter;
			}
		}

		// Token: 0x04000C15 RID: 3093
		internal static readonly IValueConverter TheConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.BorderSliceConverter.Option(new Length(100f, Length.Unit.Percent)),
			Converters.BorderSliceConverter.Option(),
			Converters.BorderSliceConverter.Option(),
			Converters.BorderSliceConverter.Option(),
			Converters.Assign<bool>(Keywords.Fill, true).Option(false)
		});

		// Token: 0x04000C16 RID: 3094
		private static readonly IValueConverter StyleConverter = CssBorderImageSliceProperty.TheConverter.OrDefault(Length.Full);
	}
}
