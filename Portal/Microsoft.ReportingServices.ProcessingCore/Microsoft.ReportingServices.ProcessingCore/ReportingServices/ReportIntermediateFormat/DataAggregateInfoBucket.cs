using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B5 RID: 1205
	internal class DataAggregateInfoBucket : AggregateBucket<DataAggregateInfo>
	{
		// Token: 0x06003C2F RID: 15407 RVA: 0x00103C91 File Offset: 0x00101E91
		internal DataAggregateInfoBucket()
		{
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x00103C99 File Offset: 0x00101E99
		internal DataAggregateInfoBucket(int level)
			: base(level)
		{
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00103CA2 File Offset: 0x00101EA2
		protected override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetSpecificDeclaration()
		{
			return DataAggregateInfoBucket.m_Declaration;
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x00103CAC File Offset: 0x00101EAC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfoBucket, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Aggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.Level, Token.Int32)
			});
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x00103CF6 File Offset: 0x00101EF6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfoBucket;
		}

		// Token: 0x04001C54 RID: 7252
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataAggregateInfoBucket.GetDeclaration();
	}
}
