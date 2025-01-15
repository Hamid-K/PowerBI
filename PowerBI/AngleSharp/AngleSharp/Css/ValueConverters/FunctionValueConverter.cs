using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000134 RID: 308
	internal sealed class FunctionValueConverter : IValueConverter
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x0003FA87 File Offset: 0x0003DC87
		public FunctionValueConverter(string name, IValueConverter arguments)
		{
			this._name = name;
			this._arguments = arguments;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0003FAA0 File Offset: 0x0003DCA0
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			CssFunctionToken cssFunctionToken = value.OnlyOrDefault() as CssFunctionToken;
			if (!this.Check(cssFunctionToken))
			{
				return null;
			}
			IPropertyValue propertyValue = this._arguments.Convert(cssFunctionToken.ArgumentTokens);
			if (propertyValue == null)
			{
				return null;
			}
			return new FunctionValueConverter.FunctionValue(this._name, propertyValue, value);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0003FAE8 File Offset: 0x0003DCE8
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<FunctionValueConverter.FunctionValue>();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0003FAF0 File Offset: 0x0003DCF0
		private bool Check(CssFunctionToken function)
		{
			return function != null && function.Data.Equals(this._name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040008ED RID: 2285
		private readonly string _name;

		// Token: 0x040008EE RID: 2286
		private readonly IValueConverter _arguments;

		// Token: 0x020004BA RID: 1210
		private sealed class FunctionValue : IPropertyValue
		{
			// Token: 0x06002523 RID: 9507 RVA: 0x00060AFA File Offset: 0x0005ECFA
			public FunctionValue(string name, IPropertyValue arguments, IEnumerable<CssToken> tokens)
			{
				this._name = name;
				this._arguments = arguments;
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000A9E RID: 2718
			// (get) Token: 0x06002524 RID: 9508 RVA: 0x00060B1C File Offset: 0x0005ED1C
			public string CssText
			{
				get
				{
					return this._name.CssFunction(this._arguments.CssText);
				}
			}

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x06002525 RID: 9509 RVA: 0x00060B34 File Offset: 0x0005ED34
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06002526 RID: 9510 RVA: 0x00060B34 File Offset: 0x0005ED34
			public CssValue ExtractFor(string name)
			{
				return this._value;
			}

			// Token: 0x0400113E RID: 4414
			private readonly string _name;

			// Token: 0x0400113F RID: 4415
			private readonly IPropertyValue _arguments;

			// Token: 0x04001140 RID: 4416
			private readonly CssValue _value;
		}
	}
}
