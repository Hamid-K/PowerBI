using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D3B RID: 3387
	internal static class ParameterValueHelpers
	{
		// Token: 0x06005B06 RID: 23302 RVA: 0x0013DC98 File Offset: 0x0013BE98
		public static ParameterValue AsParameter(this FunctionValue function)
		{
			ParameterValue parameterValue;
			if (function.TryGetAs<ParameterValue>(out parameterValue))
			{
				return parameterValue;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.Cube_FunctionNotAParameter, function, null);
		}
	}
}
