using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200006F RID: 111
	public sealed class ReportMetadataComparer : IEqualityComparer<ReportMetadata>
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000A026 File Offset: 0x00008226
		private ReportMetadataComparer()
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A030 File Offset: 0x00008230
		public bool Equals(ReportMetadata rm1, ReportMetadata rm2)
		{
			bool? flag = Util.AreEqual<ReportMetadata>(rm1, rm2);
			if (flag != null)
			{
				return flag.Value;
			}
			return ReportMetadataComparer.GetLength<Pod>(rm1.Pods) == ReportMetadataComparer.GetLength<Pod>(rm2.Pods) && rm1.LinguisticSchema == null == (rm2.LinguisticSchema == null) && (rm1.LinguisticSchema == null || rm2.LinguisticSchema == null || (ReportMetadataComparer.GetLength<ModelLinguisticEntity>(rm1.LinguisticSchema.Entities) == ReportMetadataComparer.GetLength<ModelLinguisticEntity>(rm2.LinguisticSchema.Entities) && !(rm1.LinguisticSchema.ToJsonString() != rm2.LinguisticSchema.ToJsonString()))) && JsonConvert.SerializeObject(rm1.Pods) == JsonConvert.SerializeObject(rm2.Pods);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A0F2 File Offset: 0x000082F2
		public int GetHashCode(ReportMetadata obj)
		{
			return Hashing.CombineHash(ReportMetadataComparer.GetLength<Pod>(obj.Pods), (obj.LinguisticSchema != null) ? ReportMetadataComparer.GetLength<ModelLinguisticEntity>(obj.LinguisticSchema.Entities) : (-48879));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A123 File Offset: 0x00008323
		private static int GetLength<T>(IReadOnlyCollection<T> collection)
		{
			if (collection == null)
			{
				return 0;
			}
			return collection.Count;
		}

		// Token: 0x04000166 RID: 358
		public static readonly ReportMetadataComparer Instance = new ReportMetadataComparer();
	}
}
