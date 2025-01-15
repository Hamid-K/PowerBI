using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000140 RID: 320
	internal sealed class PeriodicValueConverter : IValueConverter
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x0004025F File Offset: 0x0003E45F
		public PeriodicValueConverter(IValueConverter converter, string[] labels)
		{
			this._converter = converter;
			this._labels = ((labels.Length == 4) ? labels : Enumerable.Repeat<string>(string.Empty, 4).ToArray<string>());
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00040290 File Offset: 0x0003E490
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<CssToken> list = new List<CssToken>(value);
			IPropertyValue[] array = new IPropertyValue[4];
			if (list.Count == 0)
			{
				return null;
			}
			int num = 0;
			while (num < array.Length && list.Count != 0)
			{
				array[num] = this._converter.VaryStart(list);
				if (array[num] == null)
				{
					return null;
				}
				num++;
			}
			if (list.Count != 0)
			{
				return null;
			}
			return new PeriodicValueConverter.PeriodicValue(array, value, this._labels);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000402F8 File Offset: 0x0003E4F8
		public IPropertyValue Construct(CssProperty[] properties)
		{
			if (properties.Length != 4)
			{
				return null;
			}
			IPropertyValue[] array = new IPropertyValue[]
			{
				this._converter.Construct(properties.Where((CssProperty m) => m.Name == this._labels[0]).ToArray<CssProperty>()),
				this._converter.Construct(properties.Where((CssProperty m) => m.Name == this._labels[1]).ToArray<CssProperty>()),
				this._converter.Construct(properties.Where((CssProperty m) => m.Name == this._labels[2]).ToArray<CssProperty>()),
				this._converter.Construct(properties.Where((CssProperty m) => m.Name == this._labels[3]).ToArray<CssProperty>())
			};
			if (array[0] == null || array[1] == null || array[2] == null || array[3] == null)
			{
				return null;
			}
			return new PeriodicValueConverter.PeriodicValue(array, Enumerable.Empty<CssToken>(), this._labels);
		}

		// Token: 0x040008FF RID: 2303
		private readonly IValueConverter _converter;

		// Token: 0x04000900 RID: 2304
		private readonly string[] _labels;

		// Token: 0x020004C4 RID: 1220
		private sealed class PeriodicValue : IPropertyValue
		{
			// Token: 0x0600254D RID: 9549 RVA: 0x00060FB0 File Offset: 0x0005F1B0
			public PeriodicValue(IPropertyValue[] options, IEnumerable<CssToken> tokens, string[] labels)
			{
				this._top = options[0];
				this._right = options[1] ?? this._top;
				this._bottom = options[2] ?? this._top;
				this._left = options[3] ?? this._right;
				this._original = new CssValue(tokens);
				this._labels = labels;
			}

			// Token: 0x17000AB2 RID: 2738
			// (get) Token: 0x0600254E RID: 9550 RVA: 0x00061018 File Offset: 0x0005F218
			public string[] Values
			{
				get
				{
					string cssText = this._top.CssText;
					string cssText2 = this._right.CssText;
					string cssText3 = this._bottom.CssText;
					string cssText4 = this._left.CssText;
					if (!cssText2.Is(cssText4))
					{
						return new string[] { cssText, cssText2, cssText3, cssText4 };
					}
					if (!cssText.Is(cssText3))
					{
						return new string[] { cssText, cssText2, cssText3 };
					}
					if (cssText2.Is(cssText))
					{
						return new string[] { cssText };
					}
					return new string[] { cssText, cssText2 };
				}
			}

			// Token: 0x17000AB3 RID: 2739
			// (get) Token: 0x0600254F RID: 9551 RVA: 0x000610B3 File Offset: 0x0005F2B3
			public string CssText
			{
				get
				{
					return string.Join(" ", this.Values);
				}
			}

			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x06002550 RID: 9552 RVA: 0x000610C5 File Offset: 0x0005F2C5
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002551 RID: 9553 RVA: 0x000610D0 File Offset: 0x0005F2D0
			public CssValue ExtractFor(string name)
			{
				if (name.Is(this._labels[0]))
				{
					return this._top.Original;
				}
				if (name.Is(this._labels[1]))
				{
					return this._right.Original;
				}
				if (name.Is(this._labels[2]))
				{
					return this._bottom.Original;
				}
				if (name.Is(this._labels[3]))
				{
					return this._left.Original;
				}
				return null;
			}

			// Token: 0x04001156 RID: 4438
			private readonly IPropertyValue _top;

			// Token: 0x04001157 RID: 4439
			private readonly IPropertyValue _right;

			// Token: 0x04001158 RID: 4440
			private readonly IPropertyValue _bottom;

			// Token: 0x04001159 RID: 4441
			private readonly IPropertyValue _left;

			// Token: 0x0400115A RID: 4442
			private readonly CssValue _original;

			// Token: 0x0400115B RID: 4443
			private readonly string[] _labels;
		}
	}
}
