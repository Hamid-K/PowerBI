using System;
using System.Text.RegularExpressions;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D6 RID: 214
	internal class EmailInputType : BaseInputType
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0002F70D File Offset: 0x0002D90D
		public EmailInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00030418 File Offset: 0x0002E618
		public override void Check(ValidityState state)
		{
			string text = base.Input.Value ?? string.Empty;
			state.IsPatternMismatch = BaseInputType.IsInvalidPattern(base.Input.Pattern, text);
			if (EmailInputType.IsInvalidEmail(base.Input.IsMultiple, text))
			{
				state.IsTypeMismatch = !string.IsNullOrEmpty(text);
				state.IsBadInput = state.IsTypeMismatch;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00030480 File Offset: 0x0002E680
		private static bool IsInvalidEmail(bool multiple, string value)
		{
			if (multiple)
			{
				foreach (string text in value.Split(new char[] { ',' }))
				{
					if (!EmailInputType.email.IsMatch(text.Trim()))
					{
						return true;
					}
				}
				return false;
			}
			return !EmailInputType.email.IsMatch(value.Trim());
		}

		// Token: 0x04000600 RID: 1536
		private static readonly Regex email = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
	}
}
