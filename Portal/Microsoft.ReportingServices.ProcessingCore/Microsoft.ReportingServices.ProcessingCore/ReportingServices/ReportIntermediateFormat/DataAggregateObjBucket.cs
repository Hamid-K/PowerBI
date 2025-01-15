using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B7 RID: 1207
	internal class DataAggregateObjBucket : AggregateBucket<DataAggregateObj>
	{
		// Token: 0x06003C3B RID: 15419 RVA: 0x00103D69 File Offset: 0x00101F69
		internal DataAggregateObjBucket()
		{
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x00103D71 File Offset: 0x00101F71
		internal DataAggregateObjBucket(int level)
			: base(level)
		{
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x00103D7A File Offset: 0x00101F7A
		protected override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetSpecificDeclaration()
		{
			return DataAggregateObjBucket.m_Declaration;
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00103D84 File Offset: 0x00101F84
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjBucket, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Aggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
				new MemberInfo(MemberName.Level, Token.Int32)
			});
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00103DCB File Offset: 0x00101FCB
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjBucket;
		}

		// Token: 0x04001C56 RID: 7254
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataAggregateObjBucket.GetDeclaration();
	}
}
