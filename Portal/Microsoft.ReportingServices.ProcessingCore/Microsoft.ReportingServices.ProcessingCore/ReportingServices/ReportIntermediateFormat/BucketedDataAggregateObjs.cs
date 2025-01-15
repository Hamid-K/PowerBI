using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B6 RID: 1206
	public class BucketedDataAggregateObjs : BucketedAggregatesCollection<DataAggregateObj>
	{
		// Token: 0x06003C35 RID: 15413 RVA: 0x00103D09 File Offset: 0x00101F09
		protected override AggregateBucket<DataAggregateObj> CreateBucket(int level)
		{
			return new DataAggregateObjBucket(level);
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x00103D11 File Offset: 0x00101F11
		protected override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetSpecificDeclaration()
		{
			return BucketedDataAggregateObjs.m_Declaration;
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x00103D18 File Offset: 0x00101F18
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs;
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x00103D20 File Offset: 0x00101F20
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Buckets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjBucket)
			});
		}

		// Token: 0x04001C55 RID: 7253
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = BucketedDataAggregateObjs.GetDeclaration();
	}
}
