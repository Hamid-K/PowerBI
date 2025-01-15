using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200045F RID: 1119
	internal sealed class AggregatorFactory
	{
		// Token: 0x06003376 RID: 13174 RVA: 0x000E4590 File Offset: 0x000E2790
		private AggregatorFactory()
		{
			int length = Enum.GetValues(typeof(DataAggregateInfo.AggregateTypes)).Length;
			this.m_prototypes = new DataAggregate[length];
			this.Add(new First());
			this.Add(new Last());
			this.Add(new Sum());
			this.Add(new Avg());
			this.Add(new Max());
			this.Add(new Min());
			this.Add(new CountDistinct());
			this.Add(new CountRows());
			this.Add(new Count());
			this.Add(new StDev());
			this.Add(new Var());
			this.Add(new StDevP());
			this.Add(new VarP());
			this.Add(new Aggregate());
			this.Add(new Previous());
			this.Add(new Union());
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000E4674 File Offset: 0x000E2874
		private void Add(DataAggregate aggregator)
		{
			int aggregateType = (int)aggregator.AggregateType;
			this.m_prototypes[aggregateType] = aggregator;
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000E4694 File Offset: 0x000E2894
		[Conditional("DEBUG")]
		private void VerifyAllPrototypesCreated()
		{
			for (int i = 0; i < this.m_prototypes.Length; i++)
			{
				Global.Tracer.Assert(this.m_prototypes[i] != null, "Missing aggregate prototype for: {0}", new object[] { (DataAggregateInfo.AggregateTypes)i });
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000E46DD File Offset: 0x000E28DD
		public static AggregatorFactory Instance
		{
			get
			{
				if (AggregatorFactory.m_instance == null)
				{
					AggregatorFactory.m_instance = new AggregatorFactory();
				}
				return AggregatorFactory.m_instance;
			}
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000E46F5 File Offset: 0x000E28F5
		public DataAggregate CreateAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return this.GetPrototype(aggregateDef).ConstructAggregator(odpContext, aggregateDef);
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000E4705 File Offset: 0x000E2905
		public object GetNoRowsResult(DataAggregateInfo aggregateDef)
		{
			return this.GetPrototype(aggregateDef).Result();
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000E4713 File Offset: 0x000E2913
		private DataAggregate GetPrototype(DataAggregateInfo aggregateDef)
		{
			return this.m_prototypes[(int)aggregateDef.AggregateType];
		}

		// Token: 0x040019C4 RID: 6596
		private readonly DataAggregate[] m_prototypes;

		// Token: 0x040019C5 RID: 6597
		private static AggregatorFactory m_instance;
	}
}
