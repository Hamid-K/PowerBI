using System;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D4 RID: 468
	internal sealed class RangeVariableBinder
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x0003D640 File Offset: 0x0003B840
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
