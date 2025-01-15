using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200046D RID: 1133
	internal sealed class StDevP : VarP
	{
		// Token: 0x06003424 RID: 13348 RVA: 0x000E63BC File Offset: 0x000E45BC
		internal override object Result()
		{
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

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000E6427 File Offset: 0x000E4627
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.StDevP;
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000E642B File Offset: 0x000E462B
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new StDevP();
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000E6432 File Offset: 0x000E4632
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StDevP;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000E6438 File Offset: 0x000E4638
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (StDevP.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StDevP, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarP, list);
			}
			return StDevP.m_declaration;
		}

		// Token: 0x040019EE RID: 6638
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = StDevP.GetDeclaration();
	}
}
