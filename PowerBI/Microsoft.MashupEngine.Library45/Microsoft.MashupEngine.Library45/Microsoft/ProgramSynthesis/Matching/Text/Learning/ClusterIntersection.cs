using System;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils.Clustering;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x02001203 RID: 4611
	public class ClusterIntersection : IEquatable<ClusterIntersection>
	{
		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x06008B0E RID: 35598 RVA: 0x001D1F82 File Offset: 0x001D0182
		public Cluster<match> LeftCluster { get; }

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06008B0F RID: 35599 RVA: 0x001D1F8A File Offset: 0x001D018A
		public Cluster<match> RightCluster { get; }

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06008B10 RID: 35600 RVA: 0x001D1F92 File Offset: 0x001D0192
		public Cluster<match> Intersection { get; }

		// Token: 0x06008B11 RID: 35601 RVA: 0x001D1F9A File Offset: 0x001D019A
		public ClusterIntersection(Cluster<match> left, Cluster<match> right, Cluster<match> intersection)
		{
			this.LeftCluster = left;
			this.RightCluster = right;
			this.Intersection = intersection;
		}

		// Token: 0x06008B12 RID: 35602 RVA: 0x001D1FB7 File Offset: 0x001D01B7
		public bool Equals(ClusterIntersection other)
		{
			return other != null && (other == this || (this.LeftCluster.Equals(other.LeftCluster) && this.RightCluster.Equals(other.RightCluster)));
		}

		// Token: 0x06008B13 RID: 35603 RVA: 0x001D1FEC File Offset: 0x001D01EC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj == this)
			{
				return true;
			}
			ClusterIntersection clusterIntersection = obj as ClusterIntersection;
			return clusterIntersection != null && this.Equals(clusterIntersection);
		}

		// Token: 0x06008B14 RID: 35604 RVA: 0x001D2017 File Offset: 0x001D0217
		public override int GetHashCode()
		{
			return (this.LeftCluster.GetHashCode() * 1123) ^ this.RightCluster.GetHashCode();
		}
	}
}
