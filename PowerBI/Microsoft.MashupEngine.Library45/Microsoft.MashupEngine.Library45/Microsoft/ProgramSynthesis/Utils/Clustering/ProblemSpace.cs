using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000624 RID: 1572
	public class ProblemSpace<TData> where TData : IEquatable<TData>
	{
		// Token: 0x06002206 RID: 8710 RVA: 0x00060EB4 File Offset: 0x0005F0B4
		public ProblemSpace(IReadOnlyDictionary<TData, uint> pointsWithCounts, Func<TData, TData, KeyValuePair<double, ProblemSpace<TData>.DistanceBoundFunction>> distanceFunction, double singletonCost = 0.0)
		{
			this.PointsWithCounts = pointsWithCounts;
			this.SingletonCost = singletonCost;
			this.Count = this.PointsWithCounts.Sum((KeyValuePair<TData, uint> pair) => (long)((ulong)pair.Value));
			this._pointClusters = new Lazy<Dictionary<ulong, Dendrogram<TData>>>(() => this.PointsWithCounts.Select((KeyValuePair<TData, uint> pointWithCount) => new Dendrogram<TData>(pointWithCount.Key, this.SingletonCost, pointWithCount.Value)).ToDictionary((Dendrogram<TData> dendrogram) => dendrogram.Id, (Dendrogram<TData> dendrogram) => dendrogram));
			this._distanceFunction = distanceFunction;
			this._distances = new Dictionary<EquatablePair<TData, TData>, EquatablePair<double, bool>>();
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00060F2E File Offset: 0x0005F12E
		private static Func<TData, TData, KeyValuePair<double, ProblemSpace<TData>.DistanceBoundFunction>> NoBoundDistanceFunction(Func<TData, TData, double> distanceFunction)
		{
			return (TData a, TData b) => new KeyValuePair<double, ProblemSpace<TData>.DistanceBoundFunction>(distanceFunction(a, b), null);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00060F47 File Offset: 0x0005F147
		public ProblemSpace(IReadOnlyDictionary<TData, uint> pointsWithCounts, Func<TData, TData, double> distanceFunction, double singletonCost = 0.0)
			: this(pointsWithCounts, ProblemSpace<TData>.NoBoundDistanceFunction(distanceFunction), singletonCost)
		{
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00060F58 File Offset: 0x0005F158
		public ProblemSpace(IEnumerable<TData> points, IEqualityComparer<TData> equalityComparer, Func<TData, TData, KeyValuePair<double, ProblemSpace<TData>.DistanceBoundFunction>> distanceFunction, double singletonCost = 0.0)
			: this(points.GroupBy((TData p) => p, equalityComparer).ToDictionary((IGrouping<TData, TData> p) => p.Key, (IGrouping<TData, TData> p) => Convert.ToUInt32(p.Count<TData>()), equalityComparer), distanceFunction, singletonCost)
		{
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00060FD8 File Offset: 0x0005F1D8
		public ProblemSpace(IEnumerable<TData> points, IEqualityComparer<TData> equalityComparer, Func<TData, TData, double> distanceFunction, double singletonCost = 0.0)
			: this(points, equalityComparer, ProblemSpace<TData>.NoBoundDistanceFunction(distanceFunction), singletonCost)
		{
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x00060FEA File Offset: 0x0005F1EA
		public long Count { get; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00060FF2 File Offset: 0x0005F1F2
		public double SingletonCost { get; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00060FFA File Offset: 0x0005F1FA
		public IReadOnlyDictionary<TData, uint> PointsWithCounts { get; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00061002 File Offset: 0x0005F202
		public Dictionary<ulong, Dendrogram<TData>> PointClusters
		{
			get
			{
				return this._pointClusters.Value;
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00061010 File Offset: 0x0005F210
		private void UpdateMaxDistance(TData pointA, TData pointB, double newMaxDistance)
		{
			EquatablePair<TData, TData> equatablePair = EquatablePair.Create<TData, TData>(pointA, pointB);
			EquatablePair<double, bool> equatablePair2;
			if (!this._distances.TryGetValue(equatablePair, out equatablePair2) || (!equatablePair2.Item2 && equatablePair2.Item1 >= newMaxDistance))
			{
				this._distances[equatablePair] = (this._distances[equatablePair.Reverse()] = EquatablePair.Create<double, bool>(newMaxDistance, false));
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00061070 File Offset: 0x0005F270
		private double ComputeAndUpdateDistances(EquatablePair<TData, TData> pairKey, bool updateMaxDistances = true)
		{
			KeyValuePair<double, ProblemSpace<TData>.DistanceBoundFunction> keyValuePair = this._distanceFunction(pairKey.Item1, pairKey.Item2);
			if (updateMaxDistances && keyValuePair.Value != null)
			{
				foreach (TData tdata in this.PointsWithCounts.Keys.Except(pairKey.AsEnumerable<TData>()))
				{
					EquatablePair<double, double>? equatablePair = keyValuePair.Value(tdata);
					if (equatablePair != null)
					{
						this.UpdateMaxDistance(pairKey.Item1, tdata, equatablePair.Value.Item1);
						this.UpdateMaxDistance(pairKey.Item2, tdata, equatablePair.Value.Item2);
					}
				}
			}
			this._distances[pairKey] = (this._distances[pairKey.Reverse()] = EquatablePair.Create<double, bool>(keyValuePair.Key, true));
			return keyValuePair.Key;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0006117C File Offset: 0x0005F37C
		public double ExactDistanceBetween(TData d1, TData d2, bool updateMaxDistances = true)
		{
			EquatablePair<TData, TData> equatablePair = EquatablePair.Create<TData, TData>(d1, d2);
			EquatablePair<double, bool> equatablePair2;
			if (!this._distances.TryGetValue(equatablePair, out equatablePair2) || !equatablePair2.Item2)
			{
				return this.ComputeAndUpdateDistances(equatablePair, updateMaxDistances);
			}
			return equatablePair2.Item1;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000611BC File Offset: 0x0005F3BC
		public double MaxDistanceBetween(TData d1, TData d2)
		{
			EquatablePair<double, bool> equatablePair;
			if (!this._distances.TryGetValue(EquatablePair.Create<TData, TData>(d1, d2), out equatablePair))
			{
				return this.ExactDistanceBetween(d1, d2, true);
			}
			return equatablePair.Item1;
		}

		// Token: 0x0400104E RID: 4174
		private readonly Func<TData, TData, KeyValuePair<double, ProblemSpace<TData>.DistanceBoundFunction>> _distanceFunction;

		// Token: 0x0400104F RID: 4175
		private readonly Dictionary<EquatablePair<TData, TData>, EquatablePair<double, bool>> _distances;

		// Token: 0x04001050 RID: 4176
		private readonly Lazy<Dictionary<ulong, Dendrogram<TData>>> _pointClusters;

		// Token: 0x02000625 RID: 1573
		// (Invoke) Token: 0x06002216 RID: 8726
		public delegate EquatablePair<double, double>? DistanceBoundFunction(TData d);
	}
}
