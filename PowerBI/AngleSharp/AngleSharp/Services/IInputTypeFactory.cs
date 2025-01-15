using System;
using AngleSharp.Dom.Html;
using AngleSharp.Html.InputTypes;

namespace AngleSharp.Services
{
	// Token: 0x0200002E RID: 46
	internal interface IInputTypeFactory
	{
		// Token: 0x06000131 RID: 305
		BaseInputType Create(IHtmlInputElement input, string type);
	}
}
