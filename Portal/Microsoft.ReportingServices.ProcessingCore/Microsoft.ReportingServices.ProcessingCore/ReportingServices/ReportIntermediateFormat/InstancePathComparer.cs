using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003B8 RID: 952
	internal class InstancePathComparer : IEqualityComparer<List<InstancePathItem>>
	{
		// Token: 0x060026AE RID: 9902 RVA: 0x000B96D4 File Offset: 0x000B78D4
		private InstancePathComparer()
		{
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000B96DC File Offset: 0x000B78DC
		internal static InstancePathComparer Instance
		{
			get
			{
				return InstancePathComparer.m_instance;
			}
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000B96E3 File Offset: 0x000B78E3
		public bool Equals(List<InstancePathItem> instancePath1, List<InstancePathItem> instancePath2)
		{
			return InstancePathItem.IsSamePath(instancePath1, instancePath2);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000B96EC File Offset: 0x000B78EC
		public int GetHashCode(List<InstancePathItem> instancePath)
		{
			int num = 32452867;
			for (int i = 0; i < instancePath.Count; i++)
			{
				InstancePathItem instancePathItem = instancePath[i];
				if (!instancePathItem.IsEmpty)
				{
					int hashCode = instancePathItem.GetHashCode();
					num = ((num >> 27) ^ (num << 5)) + hashCode;
				}
			}
			return num;
		}

		// Token: 0x04001655 RID: 5717
		private static InstancePathComparer m_instance = new InstancePathComparer();
	}
}
