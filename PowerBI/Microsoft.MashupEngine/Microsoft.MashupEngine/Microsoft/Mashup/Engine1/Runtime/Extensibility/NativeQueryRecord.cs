using System;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x02001718 RID: 5912
	internal static class NativeQueryRecord
	{
		// Token: 0x0600964F RID: 38479 RVA: 0x001F1DC4 File Offset: 0x001EFFC4
		public static bool TryGetNativeQuery(Value value, out string query, out int parameterCount, out string[] parameterNames)
		{
			query = null;
			parameterCount = 0;
			parameterNames = null;
			if (value.IsText)
			{
				query = value.AsString;
				return true;
			}
			if (!value.IsRecord)
			{
				return false;
			}
			Value value2;
			if (value.AsRecord.TryGetValue("Value", out value2) && value2.IsText)
			{
				query = value2.AsString;
			}
			if (value.AsRecord.TryGetValue("Parameters", out value2))
			{
				if (value2.IsNumber)
				{
					value2.AsNumber.TryGetInt32(out parameterCount);
				}
				if (value2.IsList)
				{
					if (value2.AsList.All((IValueReference v) => v.Value.IsText))
					{
						parameterNames = value2.AsList.Select((IValueReference v) => v.Value.AsString).ToArray<string>();
						parameterCount = parameterNames.Length;
					}
				}
			}
			return query != null;
		}

		// Token: 0x04004FE8 RID: 20456
		private const string Value = "Value";

		// Token: 0x04004FE9 RID: 20457
		private const string Parameters = "Parameters";
	}
}
