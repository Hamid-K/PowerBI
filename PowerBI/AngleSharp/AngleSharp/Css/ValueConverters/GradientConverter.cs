using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000135 RID: 309
	internal abstract class GradientConverter : IValueConverter
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0003FB09 File Offset: 0x0003DD09
		public GradientConverter(bool repeating)
		{
			this._repeating = repeating;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0003FB18 File Offset: 0x0003DD18
		private static IPropertyValue[] ToGradientStops(List<List<CssToken>> values, int offset)
		{
			IPropertyValue[] array = new IPropertyValue[values.Count - offset];
			int i = offset;
			int num = 0;
			while (i < values.Count)
			{
				array[num] = GradientConverter.ToGradientStop(values[i]);
				if (array[num] == null)
				{
					return null;
				}
				i++;
				num++;
			}
			return array;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0003FB64 File Offset: 0x0003DD64
		private static IPropertyValue ToGradientStop(List<CssToken> value)
		{
			IPropertyValue propertyValue = null;
			IPropertyValue propertyValue2 = null;
			List<List<CssToken>> list = value.ToItems();
			if (list.Count != 0)
			{
				propertyValue2 = Converters.LengthOrPercentConverter.Convert(list[list.Count - 1]);
				if (propertyValue2 != null)
				{
					list.RemoveAt(list.Count - 1);
				}
			}
			if (list.Count != 0)
			{
				propertyValue = Converters.ColorConverter.Convert(list[list.Count - 1]);
				if (propertyValue != null)
				{
					list.RemoveAt(list.Count - 1);
				}
			}
			if (list.Count != 0)
			{
				return null;
			}
			return new GradientConverter.StopValue(propertyValue, propertyValue2, value);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0003FBF4 File Offset: 0x0003DDF4
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = value.ToList();
			IPropertyValue propertyValue = ((list.Count != 0) ? this.ConvertFirstArgument(list[0]) : null);
			int num = ((propertyValue != null) ? 1 : 0);
			IPropertyValue[] array = GradientConverter.ToGradientStops(list, num);
			if (array == null)
			{
				return null;
			}
			return new GradientConverter.GradientValue(this._repeating, propertyValue, array, value);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0003FC44 File Offset: 0x0003DE44
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<GradientConverter.GradientValue>();
		}

		// Token: 0x060009BE RID: 2494
		protected abstract IPropertyValue ConvertFirstArgument(IEnumerable<CssToken> value);

		// Token: 0x040008EF RID: 2287
		private readonly bool _repeating;

		// Token: 0x020004BB RID: 1211
		private sealed class StopValue : IPropertyValue
		{
			// Token: 0x06002527 RID: 9511 RVA: 0x00060B3C File Offset: 0x0005ED3C
			public StopValue(IPropertyValue color, IPropertyValue position, IEnumerable<CssToken> tokens)
			{
				this._color = color;
				this._position = position;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x06002528 RID: 9512 RVA: 0x00060B60 File Offset: 0x0005ED60
			public string CssText
			{
				get
				{
					if (this._color == null && this._position != null)
					{
						return this._position.CssText;
					}
					if (this._color != null && this._position == null)
					{
						return this._color.CssText;
					}
					return this._color.CssText + " " + this._position.CssText;
				}
			}

			// Token: 0x17000AA1 RID: 2721
			// (get) Token: 0x06002529 RID: 9513 RVA: 0x00060BC5 File Offset: 0x0005EDC5
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x0600252A RID: 9514 RVA: 0x00060BC5 File Offset: 0x0005EDC5
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x04001141 RID: 4417
			private readonly IPropertyValue _color;

			// Token: 0x04001142 RID: 4418
			private readonly IPropertyValue _position;

			// Token: 0x04001143 RID: 4419
			private readonly CssValue _original;
		}

		// Token: 0x020004BC RID: 1212
		private sealed class GradientValue : IPropertyValue
		{
			// Token: 0x0600252B RID: 9515 RVA: 0x00060BCD File Offset: 0x0005EDCD
			public GradientValue(bool repeating, IPropertyValue initial, IPropertyValue[] stops, IEnumerable<CssToken> tokens)
			{
				this._repeating = repeating;
				this._initial = initial;
				this._stops = stops;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AA2 RID: 2722
			// (get) Token: 0x0600252C RID: 9516 RVA: 0x00060BF8 File Offset: 0x0005EDF8
			public string CssText
			{
				get
				{
					int num = this._stops.Length;
					if (this._initial != null)
					{
						num++;
					}
					string[] array = new string[num];
					num = 0;
					if (this._initial != null)
					{
						array[num++] = this._initial.CssText;
					}
					for (int i = 0; i < this._stops.Length; i++)
					{
						array[num++] = this._stops[i].CssText;
					}
					return string.Join(", ", array);
				}
			}

			// Token: 0x17000AA3 RID: 2723
			// (get) Token: 0x0600252D RID: 9517 RVA: 0x00060C6F File Offset: 0x0005EE6F
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x0600252E RID: 9518 RVA: 0x00060C6F File Offset: 0x0005EE6F
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x04001144 RID: 4420
			private readonly bool _repeating;

			// Token: 0x04001145 RID: 4421
			private readonly IPropertyValue _initial;

			// Token: 0x04001146 RID: 4422
			private readonly IPropertyValue[] _stops;

			// Token: 0x04001147 RID: 4423
			private readonly CssValue _original;
		}
	}
}
