using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200028A RID: 650
	[DataContract(Name = "Constant", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class QueryConstantExpression : QueryExpression
	{
	}
}
