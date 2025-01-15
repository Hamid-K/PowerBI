using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200017D RID: 381
	[DataContract(Name = "Median", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingMedianAggregate : DataShapeBindingSimpleAggregate<DataShapeBindingMedianAggregate>
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x00014347 File Offset: 0x00012547
		private DataShapeBindingMedianAggregate()
		{
		}

		// Token: 0x0400058E RID: 1422
		internal static readonly DataShapeBindingAggregateContainer ContainerInstance = new DataShapeBindingAggregateContainer
		{
			Median = new DataShapeBindingMedianAggregate()
		};
	}
}
