using System;
using System.Text.RegularExpressions;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D2 RID: 210
	internal class ColorInputType : BaseInputType
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0002F70D File Offset: 0x0002D90D
		public ColorInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002F798 File Offset: 0x0002D998
		public override void Check(ValidityState state)
		{
			string text = base.Input.Value ?? string.Empty;
			state.IsBadInput = !ColorInputType.color.IsMatch(text);
			state.IsValueMissing = base.Input.IsRequired && state.IsBadInput;
		}

		// Token: 0x040005FF RID: 1535
		private static readonly Regex color = new Regex("^\\#[0-9A-Fa-f]{6}$");
	}
}
