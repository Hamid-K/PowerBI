using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000133 RID: 307
	internal sealed class EndListValueConverter : IValueConverter
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x0003F8ED File Offset: 0x0003DAED
		public EndListValueConverter(IValueConverter listConverter, IValueConverter endConverter)
		{
			this._listConverter = listConverter;
			this._endConverter = endConverter;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0003F904 File Offset: 0x0003DB04
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = value.ToList();
			int num = list.Count - 1;
			IPropertyValue[] array = new IPropertyValue[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = this._listConverter.Convert(list[i]);
				if (array[i] == null)
				{
					return null;
				}
			}
			array[num] = this._endConverter.Convert(list[num]);
			if (array[num] == null)
			{
				return null;
			}
			return new EndListValueConverter.ListValue(array, value);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0003F978 File Offset: 0x0003DB78
		public IPropertyValue Construct(CssProperty[] properties)
		{
			List<List<CssToken>>[] array = new List<List<CssToken>>[properties.Length];
			CssProperty[] array2 = new CssProperty[properties.Length];
			int num = 0;
			for (int i = 0; i < properties.Length; i++)
			{
				IPropertyValue declaredValue = properties[i].DeclaredValue;
				array[i] = ((declaredValue != null) ? declaredValue.Original.ToList() : new List<List<CssToken>>());
				array2[i] = Factory.Properties.CreateLonghand(properties[i].Name);
				num = Math.Max(num, array[i].Count);
			}
			IPropertyValue[] array3 = new IPropertyValue[num];
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < array2.Length; k++)
				{
					List<List<CssToken>> list = array[k];
					IEnumerable<CssToken> enumerable;
					if (list.Count <= j)
					{
						enumerable = Enumerable.Empty<CssToken>();
					}
					else
					{
						IEnumerable<CssToken> enumerable2 = list[j];
						enumerable = enumerable2;
					}
					IEnumerable<CssToken> enumerable3 = enumerable;
					array2[k].TrySetValue(new CssValue(enumerable3));
				}
				IValueConverter valueConverter = ((j < num - 1) ? this._listConverter : this._endConverter);
				array3[j] = valueConverter.Construct(array2);
			}
			return new EndListValueConverter.ListValue(array3, Enumerable.Empty<CssToken>());
		}

		// Token: 0x040008EB RID: 2283
		private readonly IValueConverter _listConverter;

		// Token: 0x040008EC RID: 2284
		private readonly IValueConverter _endConverter;

		// Token: 0x020004B9 RID: 1209
		private sealed class ListValue : IPropertyValue
		{
			// Token: 0x0600251F RID: 9503 RVA: 0x00060A48 File Offset: 0x0005EC48
			public ListValue(IPropertyValue[] values, IEnumerable<CssToken> tokens)
			{
				this._values = values;
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000A9C RID: 2716
			// (get) Token: 0x06002520 RID: 9504 RVA: 0x00060A63 File Offset: 0x0005EC63
			public string CssText
			{
				get
				{
					return string.Join(", ", this._values.Select((IPropertyValue m) => m.CssText));
				}
			}

			// Token: 0x17000A9D RID: 2717
			// (get) Token: 0x06002521 RID: 9505 RVA: 0x00060A99 File Offset: 0x0005EC99
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06002522 RID: 9506 RVA: 0x00060AA4 File Offset: 0x0005ECA4
			public CssValue ExtractFor(string name)
			{
				List<CssToken> list = new List<CssToken>();
				IPropertyValue[] values = this._values;
				for (int i = 0; i < values.Length; i++)
				{
					CssValue cssValue = values[i].ExtractFor(name);
					if (cssValue != null)
					{
						if (list.Count > 0)
						{
							list.Add(CssToken.Whitespace);
						}
						list.AddRange(cssValue);
					}
				}
				return new CssValue(list);
			}

			// Token: 0x0400113C RID: 4412
			private readonly IPropertyValue[] _values;

			// Token: 0x0400113D RID: 4413
			private readonly CssValue _value;
		}
	}
}
