using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000201 RID: 513
	internal static class FunctionHelper
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x00042813 File Offset: 0x00040A13
		internal static bool CanInvokeFunction(this IReadOnlyList<EdmFunction> functions, string functionName, IReadOnlyList<ConceptualResultType> argumentTypes)
		{
			return FunctionHelper.ResolveUserFunction(functions, functionName, argumentTypes, false) != null;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00042821 File Offset: 0x00040A21
		internal static EdmFunction InvokeUserFunction(IReadOnlyList<EdmFunction> functions, string functionName, IReadOnlyList<ConceptualResultType> argumentTypes)
		{
			return FunctionHelper.ResolveUserFunction(functions, functionName, argumentTypes, true);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0004282C File Offset: 0x00040A2C
		private static EdmFunction ResolveUserFunction(IReadOnlyList<EdmFunction> functions, string functionName, IReadOnlyList<ConceptualResultType> conceptualArgumentTypes, bool throwOnError)
		{
			bool flag = false;
			EdmFunction edmFunction = FunctionOverloadResolver.ResolveFunctionOverloads(functions, conceptualArgumentTypes, out flag);
			if (flag)
			{
				throw new ArgumentException(StringUtil.FormatInvariant("Encountered ambiguous match for {0}", new object[] { functionName }));
			}
			if (edmFunction == null && throwOnError)
			{
				throw new ArgumentException(StringUtil.FormatInvariant("Did not find match for {0}", new object[] { functionName }));
			}
			return edmFunction;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00042887 File Offset: 0x00040A87
		internal static bool CanInvokeOperator(this IReadOnlyList<EdmOperator> operators, string operatorName, IReadOnlyList<ConceptualResultType> argumentTypes)
		{
			return FunctionHelper.ResolveUserOperator(operators, operatorName, argumentTypes, false) != null;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00042895 File Offset: 0x00040A95
		internal static EdmOperator ResolveUserOperator(IReadOnlyList<EdmOperator> operators, string operatorName, IReadOnlyList<ConceptualResultType> conceptualArgumentTypes, bool throwOnError)
		{
			if (operators.Count != 1)
			{
				throw new ArgumentException(StringUtil.FormatInvariant("Only one overload is supported for operators. Found {0}", new object[] { operators.Count }));
			}
			return operators[0];
		}
	}
}
