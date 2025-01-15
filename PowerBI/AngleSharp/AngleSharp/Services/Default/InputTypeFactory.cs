using System;
using System.Collections.Generic;
using AngleSharp.Dom.Html;
using AngleSharp.Html;
using AngleSharp.Html.InputTypes;

namespace AngleSharp.Services.Default
{
	// Token: 0x0200004B RID: 75
	internal sealed class InputTypeFactory : IInputTypeFactory
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000B1F4 File Offset: 0x000093F4
		public BaseInputType Create(IHtmlInputElement input, string type)
		{
			InputTypeFactory.Creator creator = null;
			if (string.IsNullOrEmpty(type))
			{
				type = InputTypeNames.Text;
			}
			if (!this.creators.TryGetValue(type, out creator))
			{
				creator = this.creators[InputTypeNames.Text];
			}
			return creator(input);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B23C File Offset: 0x0000943C
		public InputTypeFactory()
		{
			Dictionary<string, InputTypeFactory.Creator> dictionary = new Dictionary<string, InputTypeFactory.Creator>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(InputTypeNames.Text, (IHtmlInputElement input) => new TextInputType(input, InputTypeNames.Text));
			dictionary.Add(InputTypeNames.Date, (IHtmlInputElement input) => new DateInputType(input, InputTypeNames.Date));
			dictionary.Add(InputTypeNames.Week, (IHtmlInputElement input) => new WeekInputType(input, InputTypeNames.Week));
			dictionary.Add(InputTypeNames.Datetime, (IHtmlInputElement input) => new DatetimeInputType(input, InputTypeNames.Datetime));
			dictionary.Add(InputTypeNames.DatetimeLocal, (IHtmlInputElement input) => new DatetimeLocalInputType(input, InputTypeNames.DatetimeLocal));
			dictionary.Add(InputTypeNames.Time, (IHtmlInputElement input) => new TimeInputType(input, InputTypeNames.Time));
			dictionary.Add(InputTypeNames.Month, (IHtmlInputElement input) => new MonthInputType(input, InputTypeNames.Month));
			dictionary.Add(InputTypeNames.Range, (IHtmlInputElement input) => new NumberInputType(input, InputTypeNames.Range));
			dictionary.Add(InputTypeNames.Number, (IHtmlInputElement input) => new NumberInputType(input, InputTypeNames.Number));
			dictionary.Add(InputTypeNames.Hidden, (IHtmlInputElement input) => new ButtonInputType(input, InputTypeNames.Hidden));
			dictionary.Add(InputTypeNames.Search, (IHtmlInputElement input) => new TextInputType(input, InputTypeNames.Search));
			dictionary.Add(InputTypeNames.Email, (IHtmlInputElement input) => new EmailInputType(input, InputTypeNames.Email));
			dictionary.Add(InputTypeNames.Tel, (IHtmlInputElement input) => new PatternInputType(input, InputTypeNames.Tel));
			dictionary.Add(InputTypeNames.Url, (IHtmlInputElement input) => new UrlInputType(input, InputTypeNames.Url));
			dictionary.Add(InputTypeNames.Password, (IHtmlInputElement input) => new PatternInputType(input, InputTypeNames.Password));
			dictionary.Add(InputTypeNames.Color, (IHtmlInputElement input) => new ColorInputType(input, InputTypeNames.Color));
			dictionary.Add(InputTypeNames.Checkbox, (IHtmlInputElement input) => new CheckedInputType(input, InputTypeNames.Checkbox));
			dictionary.Add(InputTypeNames.Radio, (IHtmlInputElement input) => new CheckedInputType(input, InputTypeNames.Radio));
			dictionary.Add(InputTypeNames.File, (IHtmlInputElement input) => new FileInputType(input, InputTypeNames.File));
			dictionary.Add(InputTypeNames.Submit, (IHtmlInputElement input) => new SubmitInputType(input, InputTypeNames.Submit));
			dictionary.Add(InputTypeNames.Reset, (IHtmlInputElement input) => new ButtonInputType(input, InputTypeNames.Reset));
			dictionary.Add(InputTypeNames.Image, (IHtmlInputElement input) => new ImageInputType(input, InputTypeNames.Image));
			dictionary.Add(InputTypeNames.Button, (IHtmlInputElement input) => new ButtonInputType(input, InputTypeNames.Button));
			this.creators = dictionary;
			base..ctor();
		}

		// Token: 0x040001C9 RID: 457
		private readonly Dictionary<string, InputTypeFactory.Creator> creators;

		// Token: 0x0200042C RID: 1068
		// (Invoke) Token: 0x06002299 RID: 8857
		private delegate BaseInputType Creator(IHtmlInputElement input);
	}
}
