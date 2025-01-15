using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200013F RID: 319
	internal sealed class OrValueConverter : IValueConverter
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x0004020D File Offset: 0x0003E40D
		public OrValueConverter(IValueConverter previous, IValueConverter next)
		{
			this._previous = previous;
			this._next = next;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00040223 File Offset: 0x0003E423
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			return this._previous.Convert(value) ?? this._next.Convert(value);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00040241 File Offset: 0x0003E441
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return this._previous.Construct(properties) ?? this._next.Construct(properties);
		}

		// Token: 0x040008FD RID: 2301
		private readonly IValueConverter _previous;

		// Token: 0x040008FE RID: 2302
		private readonly IValueConverter _next;
	}
}
