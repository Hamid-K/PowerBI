using System;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D0 RID: 208
	internal class ButtonInputType : BaseInputType
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0002F6E3 File Offset: 0x0002D8E3
		public ButtonInputType(IHtmlInputElement input, string name)
			: base(input, name, false)
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002F6EE File Offset: 0x0002D8EE
		public override bool IsAppendingData(IHtmlElement submitter)
		{
			return !base.Name.Is(InputTypeNames.Reset) || submitter == base.Input;
		}
	}
}
