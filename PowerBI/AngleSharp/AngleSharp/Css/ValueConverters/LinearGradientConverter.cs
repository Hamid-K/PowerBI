using System;
using System.Collections.Generic;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000136 RID: 310
	internal sealed class LinearGradientConverter : GradientConverter
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x0003FC4C File Offset: 0x0003DE4C
		public LinearGradientConverter(bool repeating)
			: base(repeating)
		{
			this._converter = Converters.AngleConverter.Or(Converters.SideOrCornerConverter.StartsWithKeyword(Keywords.To));
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0003FC74 File Offset: 0x0003DE74
		protected override IPropertyValue ConvertFirstArgument(IEnumerable<CssToken> value)
		{
			return this._converter.Convert(value);
		}

		// Token: 0x040008F0 RID: 2288
		private readonly IValueConverter _converter;
	}
}
