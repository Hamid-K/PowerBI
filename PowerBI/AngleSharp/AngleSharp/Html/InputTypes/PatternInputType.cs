using System;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000DB RID: 219
	internal class PatternInputType : BaseInputType
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0002F70D File Offset: 0x0002D90D
		public PatternInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00030B70 File Offset: 0x0002ED70
		public override void Check(ValidityState state)
		{
			string text = base.Input.Value ?? string.Empty;
			state.IsPatternMismatch = BaseInputType.IsInvalidPattern(base.Input.Pattern, text);
		}
	}
}
