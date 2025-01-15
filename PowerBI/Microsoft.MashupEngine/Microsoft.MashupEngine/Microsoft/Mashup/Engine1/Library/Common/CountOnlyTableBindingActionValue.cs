using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001041 RID: 4161
	internal static class CountOnlyTableBindingActionValue
	{
		// Token: 0x06006C96 RID: 27798 RVA: 0x00176117 File Offset: 0x00174317
		public static ActionValue New(Func<bool, ActionValue> bindCountOnlyTable)
		{
			return new SimpleBindingActionValue(delegate(FunctionValue simpleBinding)
			{
				bool flag = simpleBinding == SimpleActionBinding.ReturnNull || simpleBinding == SimpleActionBinding.ReturnRowCount;
				return bindCountOnlyTable(flag).Bind(simpleBinding);
			});
		}
	}
}
