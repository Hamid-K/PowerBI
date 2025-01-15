using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build
{
	// Token: 0x02000BED RID: 3053
	public static class Cluster
	{
		// Token: 0x06004E31 RID: 20017 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x000F81BF File Offset: 0x000F63BF
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<tableBounds>>> ClusterOnInput(this ProgramSetBuilder<tableBounds> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<tableBounds>>(Cluster.CastValue<BoundsOnPdfPage>(kvp.Key), ProgramSetBuilder<tableBounds>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x000F81F1 File Offset: 0x000F63F1
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<selectedBounds>>> ClusterOnInput(this ProgramSetBuilder<selectedBounds> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<selectedBounds>>(Cluster.CastValue<BoundsOnPdfPage>(kvp.Key), ProgramSetBuilder<selectedBounds>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x000F8223 File Offset: 0x000F6423
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<expandedBounds>>> ClusterOnInput(this ProgramSetBuilder<expandedBounds> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<expandedBounds>>(Cluster.CastValue<BoundsOnPdfPage>(kvp.Key), ProgramSetBuilder<expandedBounds>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x000F8255 File Offset: 0x000F6455
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<beforeRelativeBounds>>> ClusterOnInput(this ProgramSetBuilder<beforeRelativeBounds> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<beforeRelativeBounds>>(Cluster.CastValue<BoundsOnPdfPage>(kvp.Key), ProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x000F8287 File Offset: 0x000F6487
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<fixedBounds>>> ClusterOnInput(this ProgramSetBuilder<fixedBounds> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<fixedBounds>>(Cluster.CastValue<BoundsOnPdfPage>(kvp.Key), ProgramSetBuilder<fixedBounds>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x000F82B9 File Offset: 0x000F64B9
		public static IEnumerable<KeyValuePair<Optional<Axis>, ProgramSetBuilder<axis>>> ClusterOnInput(this ProgramSetBuilder<axis> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Axis>, ProgramSetBuilder<axis>>(Cluster.CastValue<Axis>(kvp.Key), ProgramSetBuilder<axis>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x000F82EB File Offset: 0x000F64EB
		public static IEnumerable<KeyValuePair<Optional<Direction>, ProgramSetBuilder<dir>>> ClusterOnInput(this ProgramSetBuilder<dir> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Direction>, ProgramSetBuilder<dir>>(Cluster.CastValue<Direction>(kvp.Key), ProgramSetBuilder<dir>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x000F831D File Offset: 0x000F651D
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x000F834F File Offset: 0x000F654F
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<tolerance>>> ClusterOnInput(this ProgramSetBuilder<tolerance> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<tolerance>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<tolerance>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x000F8381 File Offset: 0x000F6581
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<BoundsOnPdfPage>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x000F83B3 File Offset: 0x000F65B3
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<_LetB1>>> ClusterOnInput(this ProgramSetBuilder<_LetB1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<BoundsOnPdfPage>, ProgramSetBuilder<_LetB1>>(Cluster.CastValue<BoundsOnPdfPage>(kvp.Key), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x000F83E5 File Offset: 0x000F65E5
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<tableBounds>>> ClusterOnInputTuple(this ProgramSetBuilder<tableBounds> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<BoundsOnPdfPage>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<BoundsOnPdfPage>>(Cluster.CastValue<BoundsOnPdfPage>));
				}
				return new KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<tableBounds>>(key.Select(func).ToArray<Optional<BoundsOnPdfPage>>(), ProgramSetBuilder<tableBounds>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x000F8417 File Offset: 0x000F6617
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<selectedBounds>>> ClusterOnInputTuple(this ProgramSetBuilder<selectedBounds> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<BoundsOnPdfPage>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<BoundsOnPdfPage>>(Cluster.CastValue<BoundsOnPdfPage>));
				}
				return new KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<selectedBounds>>(key.Select(func).ToArray<Optional<BoundsOnPdfPage>>(), ProgramSetBuilder<selectedBounds>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x000F8449 File Offset: 0x000F6649
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<expandedBounds>>> ClusterOnInputTuple(this ProgramSetBuilder<expandedBounds> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<BoundsOnPdfPage>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<BoundsOnPdfPage>>(Cluster.CastValue<BoundsOnPdfPage>));
				}
				return new KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<expandedBounds>>(key.Select(func).ToArray<Optional<BoundsOnPdfPage>>(), ProgramSetBuilder<expandedBounds>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x000F847B File Offset: 0x000F667B
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<beforeRelativeBounds>>> ClusterOnInputTuple(this ProgramSetBuilder<beforeRelativeBounds> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<BoundsOnPdfPage>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<BoundsOnPdfPage>>(Cluster.CastValue<BoundsOnPdfPage>));
				}
				return new KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<beforeRelativeBounds>>(key.Select(func).ToArray<Optional<BoundsOnPdfPage>>(), ProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x000F84AD File Offset: 0x000F66AD
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<fixedBounds>>> ClusterOnInputTuple(this ProgramSetBuilder<fixedBounds> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<BoundsOnPdfPage>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<BoundsOnPdfPage>>(Cluster.CastValue<BoundsOnPdfPage>));
				}
				return new KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<fixedBounds>>(key.Select(func).ToArray<Optional<BoundsOnPdfPage>>(), ProgramSetBuilder<fixedBounds>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x000F84DF File Offset: 0x000F66DF
		public static IEnumerable<KeyValuePair<Optional<Axis>[], ProgramSetBuilder<axis>>> ClusterOnInputTuple(this ProgramSetBuilder<axis> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Axis>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<Axis>>(Cluster.CastValue<Axis>));
				}
				return new KeyValuePair<Optional<Axis>[], ProgramSetBuilder<axis>>(key.Select(func).ToArray<Optional<Axis>>(), ProgramSetBuilder<axis>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x000F8511 File Offset: 0x000F6711
		public static IEnumerable<KeyValuePair<Optional<Direction>[], ProgramSetBuilder<dir>>> ClusterOnInputTuple(this ProgramSetBuilder<dir> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Direction>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Direction>>(Cluster.CastValue<Direction>));
				}
				return new KeyValuePair<Optional<Direction>[], ProgramSetBuilder<dir>>(key.Select(func).ToArray<Optional<Direction>>(), ProgramSetBuilder<dir>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x000F8543 File Offset: 0x000F6743
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>> ClusterOnInputTuple(this ProgramSetBuilder<k> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x000F8575 File Offset: 0x000F6775
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<tolerance>>> ClusterOnInputTuple(this ProgramSetBuilder<tolerance> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<tolerance>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<tolerance>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x000F85A7 File Offset: 0x000F67A7
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<BoundsOnPdfPage>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<BoundsOnPdfPage>>(Cluster.CastValue<BoundsOnPdfPage>));
				}
				return new KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<BoundsOnPdfPage>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x000F85D9 File Offset: 0x000F67D9
		public static IEnumerable<KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<_LetB1>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<BoundsOnPdfPage>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<BoundsOnPdfPage>>(Cluster.CastValue<BoundsOnPdfPage>));
				}
				return new KeyValuePair<Optional<BoundsOnPdfPage>[], ProgramSetBuilder<_LetB1>>(key.Select(func).ToArray<Optional<BoundsOnPdfPage>>(), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02000BEE RID: 3054
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040022FD RID: 8957
			public static Func<object, Optional<BoundsOnPdfPage>> <0>__CastValue;

			// Token: 0x040022FE RID: 8958
			public static Func<object, Optional<Axis>> <1>__CastValue;

			// Token: 0x040022FF RID: 8959
			public static Func<object, Optional<Direction>> <2>__CastValue;

			// Token: 0x04002300 RID: 8960
			public static Func<object, Optional<int>> <3>__CastValue;
		}
	}
}
