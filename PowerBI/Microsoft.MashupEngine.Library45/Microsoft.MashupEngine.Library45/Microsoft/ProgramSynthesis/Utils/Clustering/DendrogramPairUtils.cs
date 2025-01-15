using System;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000607 RID: 1543
	public static class DendrogramPairUtils
	{
		// Token: 0x060021A4 RID: 8612 RVA: 0x0005F7F9 File Offset: 0x0005D9F9
		public static EquatablePair<Dendrogram<TData>, Dendrogram<TData>> CreateSorted<TData>(Dendrogram<TData> d1, Dendrogram<TData> d2)
		{
			if (d1.CompareTo(d2) >= 0)
			{
				return EquatablePair.Create<Dendrogram<TData>, Dendrogram<TData>>(d2, d1);
			}
			return EquatablePair.Create<Dendrogram<TData>, Dendrogram<TData>>(d1, d2);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0005F814 File Offset: 0x0005DA14
		public static bool ContainsId<TData>(this EquatablePair<Dendrogram<TData>, Dendrogram<TData>> pair, ulong id)
		{
			return pair.Item1.Id == id || pair.Item2.Id == id;
		}
	}
}
