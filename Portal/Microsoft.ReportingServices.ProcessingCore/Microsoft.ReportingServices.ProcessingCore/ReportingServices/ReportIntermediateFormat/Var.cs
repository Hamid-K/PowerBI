using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200046C RID: 1132
	internal class Var : VarBase
	{
		// Token: 0x0600341D RID: 13341 RVA: 0x000E6284 File Offset: 0x000E4484
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
				return (this.m_currentCount * (double)this.m_sumOfXSquared - (double)this.m_sumOfX * (double)this.m_sumOfX) / (this.m_currentCount * (this.m_currentCount - 1U));
			}
			if (sumOfXType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return (this.m_currentCount * (decimal)this.m_sumOfXSquared - (decimal)this.m_sumOfX * (decimal)this.m_sumOfX) / (this.m_currentCount * (this.m_currentCount - 1U));
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000E636A File Offset: 0x000E456A
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Var;
			}
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000E636E File Offset: 0x000E456E
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Var();
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000E6375 File Offset: 0x000E4575
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Var;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000E637C File Offset: 0x000E457C
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Var.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Var, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarBase, list);
			}
			return Var.m_declaration;
		}

		// Token: 0x040019ED RID: 6637
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Var.GetDeclaration();
	}
}
