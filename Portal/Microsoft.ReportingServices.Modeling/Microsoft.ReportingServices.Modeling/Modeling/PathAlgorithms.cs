using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000AA RID: 170
	public static class PathAlgorithms
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x0001E886 File Offset: 0x0001CA86
		public static CardinalityContext GetCardinalityContext<T>(IList<T> path) where T : IPathItem
		{
			return PathAlgorithms.GetCardinalityContext<T>(path, path.Count);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001E894 File Offset: 0x0001CA94
		public static CardinalityContext GetCardinalityContext<T>(IList<T> path, int atPosition) where T : IPathItem
		{
			if (atPosition < 0 || atPosition > path.Count)
			{
				throw new ArgumentOutOfRangeException("atPosition");
			}
			CardinalityContext cardinalityContext = CardinalityContext.Scalar;
			for (int i = 0; i < atPosition; i++)
			{
				T t = path[i];
				if (t.Cardinality == Cardinality.Many)
				{
					cardinalityContext = CardinalityContext.Set;
				}
				else if (cardinalityContext == CardinalityContext.Set)
				{
					t = path[i];
					if (t.ReverseCardinality == Cardinality.Many)
					{
						t = path[i];
						if (t.Cardinality == Cardinality.One)
						{
							cardinalityContext = CardinalityContext.NonUniqueSet;
							break;
						}
					}
				}
			}
			return cardinalityContext;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001E91C File Offset: 0x0001CB1C
		public static Cardinality GetCardinality<T>(IList<T> path) where T : IPathItem
		{
			for (int i = 0; i < path.Count; i++)
			{
				T t = path[i];
				if (t.Cardinality == Cardinality.Many)
				{
					return Cardinality.Many;
				}
			}
			return Cardinality.One;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001E958 File Offset: 0x0001CB58
		public static Optionality GetOptionality<T>(IList<T> path) where T : IPathItem
		{
			for (int i = 0; i < path.Count; i++)
			{
				T t = path[i];
				if (t.Optionality == Optionality.Optional)
				{
					return Optionality.Optional;
				}
			}
			return Optionality.Required;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001E990 File Offset: 0x0001CB90
		public static int GetLeadingScalarLength<T>(IList<T> path) where T : IPathItem
		{
			for (int i = 0; i < path.Count; i++)
			{
				T t = path[i];
				if (t.Cardinality != Cardinality.One)
				{
					return i;
				}
			}
			return path.Count;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
		public static int GetMatchingSegmentLength<T>(int startAt, IList<T> path1, IList<T> path2) where T : IPathItem
		{
			if (startAt < 0 || startAt > path1.Count)
			{
				throw new ArgumentOutOfRangeException("startAt");
			}
			int num = 0;
			while (startAt + num < path1.Count && num < path2.Count)
			{
				T t = path1[startAt + num];
				if (!t.Equals(path2[num]))
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001EA37 File Offset: 0x0001CC37
		public static bool StartsWith<T>(IList<T> path1, IList<T> path2) where T : IPathItem
		{
			return PathAlgorithms.GetMatchingSegmentLength<T>(0, path1, path2) == path2.Count;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001EA49 File Offset: 0x0001CC49
		public static bool EndsWith<T>(IList<T> path1, IList<T> path2) where T : IPathItem
		{
			return path1.Count >= path2.Count && PathAlgorithms.GetMatchingSegmentLength<T>(path1.Count - path2.Count, path1, path2) == path2.Count;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001EA78 File Offset: 0x0001CC78
		public static int GetHashCode<T>(IList<T> path) where T : IPathItem
		{
			int num = 0;
			for (int i = 0; i < path.Count; i++)
			{
				T t = path[i];
				int hashCode = t.GetHashCode();
				int num2 = i % 32;
				num ^= (hashCode << num2) | (hashCode >> 32 - num2);
			}
			return num;
		}
	}
}
