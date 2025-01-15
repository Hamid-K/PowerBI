using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200046E RID: 1134
	internal class VarP : VarBase
	{
		// Token: 0x0600342B RID: 13355 RVA: 0x000E6478 File Offset: 0x000E4678
		internal override object Result()
		{
			DataAggregate.DataTypeCode sumOfXType = this.m_sumOfXType;
			if (sumOfXType == DataAggregate.DataTypeCode.Null)
			{
				return null;
			}
			if (sumOfXType == DataAggregate.DataTypeCode.Double)
			{
				return (this.m_currentCount * (double)this.m_sumOfXSquared - (double)this.m_sumOfX * (double)this.m_sumOfX) / (this.m_currentCount * this.m_currentCount);
			}
			if (sumOfXType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return (this.m_currentCount * (decimal)this.m_sumOfXSquared - (decimal)this.m_sumOfX * (decimal)this.m_sumOfX) / (this.m_currentCount * this.m_currentCount);
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000E654F File Offset: 0x000E474F
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.VarP;
			}
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000E6553 File Offset: 0x000E4753
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new VarP();
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000E655A File Offset: 0x000E475A
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarP;
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000E6560 File Offset: 0x000E4760
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (VarP.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarP, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarBase, list);
			}
			return VarP.m_declaration;
		}

		// Token: 0x040019EF RID: 6639
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = VarP.GetDeclaration();
	}
}
