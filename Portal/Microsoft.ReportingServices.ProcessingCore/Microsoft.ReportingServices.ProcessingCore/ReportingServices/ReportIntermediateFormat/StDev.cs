using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200046B RID: 1131
	internal sealed class StDev : Var
	{
		// Token: 0x06003416 RID: 13334 RVA: 0x000E61BC File Offset: 0x000E43BC
		internal override object Result()
		{
			if (1U == this.m_currentCount)
			{
				return null;
			}
			DataAggregate.DataTypeCode sumOfXType = this.m_sumOfXType;
			if (sumOfXType == DataAggregate.DataTypeCode.Null)
			{
				return null;
			}
			if (sumOfXType == DataAggregate.DataTypeCode.Double)
			{
				return Math.Sqrt((double)base.Result());
			}
			if (sumOfXType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return Math.Sqrt(Convert.ToDouble((decimal)base.Result()));
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000E6232 File Offset: 0x000E4432
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.StDev;
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000E6236 File Offset: 0x000E4436
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new StDev();
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000E623D File Offset: 0x000E443D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StDev;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000E6244 File Offset: 0x000E4444
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (StDev.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StDev, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Var, list);
			}
			return StDev.m_declaration;
		}

		// Token: 0x040019EC RID: 6636
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = StDev.GetDeclaration();
	}
}
