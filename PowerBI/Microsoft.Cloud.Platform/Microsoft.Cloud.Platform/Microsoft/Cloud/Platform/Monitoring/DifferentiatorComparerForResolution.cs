using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200009B RID: 155
	internal class DifferentiatorComparerForResolution : IEqualityComparer<Differentiators>
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0001057C File Offset: 0x0000E77C
		internal DifferentiatorComparerForResolution(ICollection<int> indices)
		{
			this.m_indices = indices;
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Created {0}", new object[] { this });
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000105A8 File Offset: 0x0000E7A8
		public bool Equals(Differentiators x, Differentiators y)
		{
			return this.m_indices.All((int index) => x[index].Equals(y[index]));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000105E0 File Offset: 0x0000E7E0
		public int GetHashCode(Differentiators diffs)
		{
			int num = 0;
			foreach (int num2 in this.m_indices)
			{
				num ^= diffs[num2].GetHashCode();
			}
			return num;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010638 File Offset: 0x0000E838
		public override string ToString()
		{
			string text = this.m_indices.StringJoin(",");
			return string.Format(CultureInfo.InvariantCulture, "Comparer for differentiators with indices: {0}", new object[] { text });
		}

		// Token: 0x04000180 RID: 384
		private ICollection<int> m_indices;
	}
}
