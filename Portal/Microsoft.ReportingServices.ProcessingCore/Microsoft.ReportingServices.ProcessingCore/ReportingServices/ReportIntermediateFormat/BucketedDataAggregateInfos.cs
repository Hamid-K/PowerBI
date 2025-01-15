using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B4 RID: 1204
	internal class BucketedDataAggregateInfos : BucketedAggregatesCollection<DataAggregateInfo>
	{
		// Token: 0x06003C29 RID: 15401 RVA: 0x00103C2F File Offset: 0x00101E2F
		protected override AggregateBucket<DataAggregateInfo> CreateBucket(int level)
		{
			return new DataAggregateInfoBucket(level);
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x00103C37 File Offset: 0x00101E37
		protected override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetSpecificDeclaration()
		{
			return BucketedDataAggregateInfos.m_Declaration;
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x00103C3E File Offset: 0x00101E3E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateInfos;
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x00103C48 File Offset: 0x00101E48
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateInfos, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Buckets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfoBucket)
			});
		}

		// Token: 0x04001C53 RID: 7251
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = BucketedDataAggregateInfos.GetDeclaration();
	}
}
