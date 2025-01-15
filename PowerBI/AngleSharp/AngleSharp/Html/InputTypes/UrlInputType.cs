using System;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000DF RID: 223
	internal class UrlInputType : BaseInputType
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x0002F70D File Offset: 0x0002D90D
		public UrlInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00030F74 File Offset: 0x0002F174
		public override void Check(ValidityState state)
		{
			string text = base.Input.Value ?? string.Empty;
			state.IsPatternMismatch = BaseInputType.IsInvalidPattern(base.Input.Pattern, text);
			if (UrlInputType.IsInvalidUrl(text))
			{
				state.IsTypeMismatch = !string.IsNullOrEmpty(text);
				state.IsBadInput = state.IsTypeMismatch;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00030FD0 File Offset: 0x0002F1D0
		private static bool IsInvalidUrl(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Url url = new Url(value);
				return url.IsInvalid || url.IsRelative;
			}
			return false;
		}
	}
}
