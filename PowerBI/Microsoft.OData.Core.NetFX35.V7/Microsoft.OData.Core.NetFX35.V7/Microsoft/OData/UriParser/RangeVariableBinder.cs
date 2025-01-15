using System;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000F1 RID: 241
	internal sealed class RangeVariableBinder
	{
		// Token: 0x06000BCA RID: 3018 RVA: 0x0001E740 File Offset: 0x0001C940
		internal static SingleValueNode BindRangeVariableToken(RangeVariableToken rangeVariableToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<RangeVariableToken>(rangeVariableToken, "rangeVariableToken");
			RangeVariable rangeVariable = Enumerable.SingleOrDefault<RangeVariable>(state.RangeVariables, (RangeVariable p) => p.Name == rangeVariableToken.Name);
			if (rangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_ParameterNotInScope(rangeVariableToken.Name));
			}
			return NodeFactory.CreateRangeVariableReferenceNode(rangeVariable);
		}
	}
}
