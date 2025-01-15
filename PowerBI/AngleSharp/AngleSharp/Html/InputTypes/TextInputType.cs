using System;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000DD RID: 221
	internal class TextInputType : BaseInputType
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x0002F70D File Offset: 0x0002D90D
		public TextInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00030BB4 File Offset: 0x0002EDB4
		public override void Check(ValidityState state)
		{
			string text = base.Input.Value ?? string.Empty;
			state.IsPatternMismatch = BaseInputType.IsInvalidPattern(base.Input.Pattern, text);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00030BF0 File Offset: 0x0002EDF0
		public override void ConstructDataSet(FormDataSet dataSet)
		{
			base.ConstructDataSet(dataSet);
			string attribute = base.Input.GetAttribute(null, AttributeNames.DirName);
			if (!string.IsNullOrEmpty(attribute))
			{
				dataSet.Append(attribute, base.Input.Direction.ToLowerInvariant(), "Direction");
			}
		}
	}
}
