using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000088 RID: 136
	internal sealed class DsrIntersectionCalculationsBuilder : DsrCalculationsBuilder
	{
		// Token: 0x06000329 RID: 809 RVA: 0x00009200 File Offset: 0x00007400
		internal DsrIntersectionCalculationsBuilder(string intersectionId)
		{
			this.EmptyDataIntersection = new DataIntersection
			{
				Id = intersectionId
			};
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000921A File Offset: 0x0000741A
		public DataIntersection EmptyDataIntersection { get; }

		// Token: 0x0600032B RID: 811 RVA: 0x00009224 File Offset: 0x00007424
		public override void SetCalcMetadata(IReadOnlyList<CalculationMetadata> metadata)
		{
			base.SetCalcMetadata(metadata);
			List<Calculation> list = new List<Calculation>(metadata.Count);
			for (int i = 0; i < metadata.Count; i++)
			{
				Calculation calculation = new Calculation
				{
					Id = metadata[i].Id
				};
				list.Add(calculation);
			}
			this.EmptyDataIntersection.Calculations = list;
		}
	}
}
