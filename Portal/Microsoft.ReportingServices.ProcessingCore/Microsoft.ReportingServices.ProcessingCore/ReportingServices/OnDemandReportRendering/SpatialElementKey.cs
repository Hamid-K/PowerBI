using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000163 RID: 355
	internal class SpatialElementKey
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x0003F3C1 File Offset: 0x0003D5C1
		internal SpatialElementKey(List<object> values)
		{
			this.m_keyValues = values;
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
		internal List<object> KeyValues
		{
			get
			{
				return this.m_keyValues;
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003F3D8 File Offset: 0x0003D5D8
		public override bool Equals(object obj)
		{
			SpatialElementKey spatialElementKey = (SpatialElementKey)obj;
			if (this.m_keyValues == null || spatialElementKey.m_keyValues == null)
			{
				return false;
			}
			if (this.m_keyValues.Count != spatialElementKey.m_keyValues.Count)
			{
				return false;
			}
			for (int i = 0; i < this.m_keyValues.Count; i++)
			{
				object obj2 = this.m_keyValues[i];
				if (obj2 == null)
				{
					return false;
				}
				if (!obj2.Equals(spatialElementKey.m_keyValues[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003F458 File Offset: 0x0003D658
		public override int GetHashCode()
		{
			int num = 0;
			if (this.m_keyValues != null)
			{
				for (int i = 0; i < this.m_keyValues.Count; i++)
				{
					if (this.m_keyValues[i] != null)
					{
						num ^= this.m_keyValues[i].GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x04000703 RID: 1795
		private List<object> m_keyValues;
	}
}
