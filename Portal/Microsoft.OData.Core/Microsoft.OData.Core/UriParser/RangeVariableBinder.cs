using System;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012F RID: 303
	internal sealed class RangeVariableBinder
	{
		// Token: 0x06001027 RID: 4135 RVA: 0x0002A1D4 File Offset: 0x000283D4
		internal static SingleValueNode BindRangeVariableToken(RangeVariableToken rangeVariableToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<RangeVariableToken>(rangeVariableToken, "rangeVariableToken");
			RangeVariable rangeVariable = state.RangeVariables.SingleOrDefault((RangeVariable p) => p.Name == rangeVariableToken.Name);
			if (rangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_ParameterNotInScope(rangeVariableToken.Name));
			}
			return NodeFactory.CreateRangeVariableReferenceNode(rangeVariable);
		}
	}
}
