using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000605 RID: 1541
	public class Dendrogram<TData> : IComparable<Dendrogram<TData>>, IComparable, IEquatable<Dendrogram<TData>>
	{
		// Token: 0x06002190 RID: 8592 RVA: 0x0005F534 File Offset: 0x0005D734
		private Dendrogram(Lazy<IReadOnlyList<TData>> dataLazy, double cost, uint pointsCount)
		{
			this.Id = (Dendrogram<TData>._id += 1UL);
			this.Cost = cost;
			this.PointsCount = pointsCount;
			this.LeftChild = (this.RightChild = null);
			this._data = dataLazy;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0005F580 File Offset: 0x0005D780
		public Dendrogram(TData data, double cost, uint pointsCount = 1U)
			: this(new Lazy<IReadOnlyList<TData>>(() => new TData[] { data }), cost, pointsCount)
		{
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		public Dendrogram(Dendrogram<TData> childA, Dendrogram<TData> childB, double cost)
		{
			if (childA == null)
			{
				throw new ArgumentNullException("childA");
			}
			if (childB == null)
			{
				throw new ArgumentNullException("childB");
			}
			if (cost < childA.Cost || cost < childB.Cost)
			{
				throw new ArgumentException("Cost of a cluster cannot be smaller than the cost of its sub-clusters.");
			}
			this.Id = (Dendrogram<TData>._id += 1UL);
			this.Cost = cost;
			this.LeftChild = childA;
			this.RightChild = childB;
			this.PointsCount = childA.PointsCount + childB.PointsCount;
			this._data = new Lazy<IReadOnlyList<TData>>(() => this.LeftChild.Data.Concat(this.RightChild.Data).ToArray<TData>());
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x0005F65F File Offset: 0x0005D85F
		public ulong Id { get; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0005F667 File Offset: 0x0005D867
		public double Cost { get; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x0005F66F File Offset: 0x0005D86F
		public uint PointsCount { get; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0005F677 File Offset: 0x0005D877
		public Dendrogram<TData> LeftChild { get; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x0005F67F File Offset: 0x0005D87F
		public Dendrogram<TData> RightChild { get; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0005F687 File Offset: 0x0005D887
		public IReadOnlyList<TData> Data
		{
			get
			{
				return this._data.Value;
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0005F694 File Offset: 0x0005D894
		public int CompareTo(object obj)
		{
			Dendrogram<TData> dendrogram = obj as Dendrogram<TData>;
			if (dendrogram == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Not a {0}", new object[] { "Dendrogram" })), "obj");
			}
			return this.CompareTo(dendrogram);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0005F6E0 File Offset: 0x0005D8E0
		public int CompareTo(Dendrogram<TData> other)
		{
			if (!(other == null))
			{
				return -Record.Create<double, ulong>(this.Cost, this.Id).CompareTo(Record.Create<double, ulong>(other.Cost, other.Id));
			}
			return -1;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0005F723 File Offset: 0x0005D923
		public bool Equals(Dendrogram<TData> other)
		{
			return other != null && (this == other || this.Id == other.Id);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0005F73E File Offset: 0x0005D93E
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Dendrogram<TData>)obj)));
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0005F76C File Offset: 0x0005D96C
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Dendrogram<TData> left, Dendrogram<TData> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Dendrogram<TData> left, Dendrogram<TData> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0005F787 File Offset: 0x0005D987
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} {1} ({2})", new object[] { "Dendrogram", this.Id, this.PointsCount }));
		}

		// Token: 0x04000FEC RID: 4076
		private static ulong _id;

		// Token: 0x04000FED RID: 4077
		private readonly Lazy<IReadOnlyList<TData>> _data;
	}
}
