using System;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000DC RID: 220
	internal class SubmitInputType : BaseInputType
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0002F70D File Offset: 0x0002D90D
		public SubmitInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00030BA9 File Offset: 0x0002EDA9
		public override bool IsAppendingData(IHtmlElement submitter)
		{
			return submitter == base.Input;
		}
	}
}
