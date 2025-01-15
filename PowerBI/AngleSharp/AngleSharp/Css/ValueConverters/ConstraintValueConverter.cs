using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000130 RID: 304
	internal sealed class ConstraintValueConverter : IValueConverter
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0003F73A File Offset: 0x0003D93A
		public ConstraintValueConverter(IValueConverter converter, string[] labels)
		{
			this._converter = converter;
			this._labels = labels;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0003F750 File Offset: 0x0003D950
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			IPropertyValue propertyValue = this._converter.Convert(value);
			if (propertyValue == null)
			{
				return null;
			}
			return new ConstraintValueConverter.TransformationValueConverter(propertyValue, this._labels);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0003F77C File Offset: 0x0003D97C
		public IPropertyValue Construct(CssProperty[] properties)
		{
			IEnumerable<CssProperty> enumerable = properties.Where((CssProperty m) => this._labels.Contains(m.Name));
			string text = null;
			foreach (CssProperty cssProperty in enumerable)
			{
				string value = cssProperty.Value;
				if (text != null && value != text)
				{
					return null;
				}
				text = value;
			}
			IPropertyValue propertyValue = this._converter.Construct(enumerable.Take(1).ToArray<CssProperty>());
			if (propertyValue == null)
			{
				return null;
			}
			return new ConstraintValueConverter.TransformationValueConverter(propertyValue, this._labels);
		}

		// Token: 0x040008E7 RID: 2279
		private readonly IValueConverter _converter;

		// Token: 0x040008E8 RID: 2280
		private readonly string[] _labels;

		// Token: 0x020004B6 RID: 1206
		private sealed class TransformationValueConverter : IPropertyValue
		{
			// Token: 0x06002513 RID: 9491 RVA: 0x000608E7 File Offset: 0x0005EAE7
			public TransformationValueConverter(IPropertyValue value, string[] labels)
			{
				this._value = value;
				this._labels = labels;
			}

			// Token: 0x17000A96 RID: 2710
			// (get) Token: 0x06002514 RID: 9492 RVA: 0x000608FD File Offset: 0x0005EAFD
			public string CssText
			{
				get
				{
					return this._value.CssText;
				}
			}

			// Token: 0x17000A97 RID: 2711
			// (get) Token: 0x06002515 RID: 9493 RVA: 0x0006090A File Offset: 0x0005EB0A
			public CssValue Original
			{
				get
				{
					return this._value.Original;
				}
			}

			// Token: 0x06002516 RID: 9494 RVA: 0x00060917 File Offset: 0x0005EB17
			public CssValue ExtractFor(string name)
			{
				if (!this._labels.Contains(name))
				{
					return null;
				}
				return this._value.ExtractFor(name);
			}

			// Token: 0x04001135 RID: 4405
			private readonly IPropertyValue _value;

			// Token: 0x04001136 RID: 4406
			private readonly string[] _labels;
		}
	}
}
