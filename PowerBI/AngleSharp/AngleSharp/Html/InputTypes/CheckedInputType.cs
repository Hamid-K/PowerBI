using System;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D1 RID: 209
	internal class CheckedInputType : BaseInputType
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x0002F70D File Offset: 0x0002D90D
		public CheckedInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002F718 File Offset: 0x0002D918
		public override void Check(ValidityState state)
		{
			state.IsValueMissing = base.Input.IsRequired && !base.Input.IsChecked;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002F740 File Offset: 0x0002D940
		public override void ConstructDataSet(FormDataSet dataSet)
		{
			if (base.Input.IsChecked)
			{
				string text = (base.Input.HasValue ? base.Input.Value : Keywords.On);
				dataSet.Append(base.Input.Name, text, base.Input.Type);
			}
		}
	}
}
