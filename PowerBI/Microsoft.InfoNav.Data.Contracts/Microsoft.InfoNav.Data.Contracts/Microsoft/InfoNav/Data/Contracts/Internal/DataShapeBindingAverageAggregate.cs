using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200017E RID: 382
	[DataContract(Name = "Average", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAverageAggregate : DataShapeBindingSimpleAggregate<DataShapeBindingAverageAggregate>
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x00014366 File Offset: 0x00012566
		private DataShapeBindingAverageAggregate()
		{
		}

		// Token: 0x0400058F RID: 1423
		internal static readonly DataShapeBindingAggregateContainer ContainerInstance = new DataShapeBindingAggregateContainer
		{
			Average = new DataShapeBindingAverageAggregate()
		};
	}
}
