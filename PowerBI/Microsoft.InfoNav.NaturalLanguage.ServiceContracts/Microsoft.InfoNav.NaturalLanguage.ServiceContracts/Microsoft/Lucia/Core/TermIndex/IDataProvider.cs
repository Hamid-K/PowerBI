using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000165 RID: 357
	public interface IDataProvider : IDisposable
	{
		// Token: 0x06000707 RID: 1799
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Value", "Weight" })]
		IEnumerable<global::System.ValueTuple<string, double?>> GetStrings(EdmPropertyRef valueColumn, EdmPropertyRef? weightColumn, int maxLength);

		// Token: 0x06000708 RID: 1800
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Synonym", "Value", "Weight" })]
		IEnumerable<global::System.ValueTuple<string, string, double?>> GetStringPairs(EdmPropertyRef synonymColumn, EdmPropertyRef valueColumn, EdmPropertyRef? weightColumn, int maxLength);
	}
}
