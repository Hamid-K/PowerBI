using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build
{
	// Token: 0x02000FFA RID: 4090
	public static class Cluster
	{
		// Token: 0x06007766 RID: 30566 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06007767 RID: 30567 RVA: 0x0019BDB7 File Offset: 0x00199FB7
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<WebRegion>>, ProgramSetBuilder<resultSequence>>> ClusterOnInput(this ProgramSetBuilder<resultSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<WebRegion>>, ProgramSetBuilder<resultSequence>>(Cluster.CastValue<IEnumerable<WebRegion>>(kvp.Key), ProgramSetBuilder<resultSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007768 RID: 30568 RVA: 0x0019BDE9 File Offset: 0x00199FE9
		public static IEnumerable<KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<resultRegion>>> ClusterOnInput(this ProgramSetBuilder<resultRegion> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<resultRegion>>(Cluster.CastValue<WebRegion>(kvp.Key), ProgramSetBuilder<resultRegion>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007769 RID: 30569 RVA: 0x0019BE1B File Offset: 0x0019A01B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<WebRegion>>, ProgramSetBuilder<subNodeSequence>>> ClusterOnInput(this ProgramSetBuilder<subNodeSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<WebRegion>>, ProgramSetBuilder<subNodeSequence>>(Cluster.CastValue<IEnumerable<WebRegion>>(kvp.Key), ProgramSetBuilder<subNodeSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600776A RID: 30570 RVA: 0x0019BE4D File Offset: 0x0019A04D
		public static IEnumerable<KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<subNode>>> ClusterOnInput(this ProgramSetBuilder<subNode> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<subNode>>(Cluster.CastValue<WebRegion>(kvp.Key), ProgramSetBuilder<subNode>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600776B RID: 30571 RVA: 0x0019BE7F File Offset: 0x0019A07F
		public static IEnumerable<KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<mapNodeInSequence>>> ClusterOnInput(this ProgramSetBuilder<mapNodeInSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<mapNodeInSequence>>(Cluster.CastValue<WebRegion>(kvp.Key), ProgramSetBuilder<mapNodeInSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600776C RID: 30572 RVA: 0x0019BEB1 File Offset: 0x0019A0B1
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<WebRegion>>, ProgramSetBuilder<regionSequence>>> ClusterOnInput(this ProgramSetBuilder<regionSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<WebRegion>>, ProgramSetBuilder<regionSequence>>(Cluster.CastValue<IEnumerable<WebRegion>>(kvp.Key), ProgramSetBuilder<regionSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600776D RID: 30573 RVA: 0x0019BEE3 File Offset: 0x0019A0E3
		public static IEnumerable<KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<region>>> ClusterOnInput(this ProgramSetBuilder<region> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<region>>(Cluster.CastValue<WebRegion>(kvp.Key), ProgramSetBuilder<region>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600776E RID: 30574 RVA: 0x0019BF15 File Offset: 0x0019A115
		public static IEnumerable<KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<mapRegionInSequence>>> ClusterOnInput(this ProgramSetBuilder<mapRegionInSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<mapRegionInSequence>>(Cluster.CastValue<WebRegion>(kvp.Key), ProgramSetBuilder<mapRegionInSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600776F RID: 30575 RVA: 0x0019BF47 File Offset: 0x0019A147
		public static IEnumerable<KeyValuePair<Optional<IDomNode>, ProgramSetBuilder<beginNode>>> ClusterOnInput(this ProgramSetBuilder<beginNode> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IDomNode>, ProgramSetBuilder<beginNode>>(Cluster.CastValue<IDomNode>(kvp.Key), ProgramSetBuilder<beginNode>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007770 RID: 30576 RVA: 0x0019BF79 File Offset: 0x0019A179
		public static IEnumerable<KeyValuePair<Optional<IDomNode>, ProgramSetBuilder<endNode>>> ClusterOnInput(this ProgramSetBuilder<endNode> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IDomNode>, ProgramSetBuilder<endNode>>(Cluster.CastValue<IDomNode>(kvp.Key), ProgramSetBuilder<endNode>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007771 RID: 30577 RVA: 0x0019BFAB File Offset: 0x0019A1AB
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection>>> ClusterOnInput(this ProgramSetBuilder<selection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x0019BFDD File Offset: 0x0019A1DD
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection>>> ClusterOnInput(this ProgramSetBuilder<filterSelection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<filterSelection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007773 RID: 30579 RVA: 0x0019C00F File Offset: 0x0019A20F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selectionEnd>>> ClusterOnInput(this ProgramSetBuilder<selectionEnd> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selectionEnd>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selectionEnd>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007774 RID: 30580 RVA: 0x0019C041 File Offset: 0x0019A241
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<regionStartSiblings>>> ClusterOnInput(this ProgramSetBuilder<regionStartSiblings> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<regionStartSiblings>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<regionStartSiblings>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007775 RID: 30581 RVA: 0x0019C073 File Offset: 0x0019A273
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection2>>> ClusterOnInput(this ProgramSetBuilder<selection2> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection2>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection2>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007776 RID: 30582 RVA: 0x0019C0A5 File Offset: 0x0019A2A5
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection3>>> ClusterOnInput(this ProgramSetBuilder<selection3> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection3>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection3>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007777 RID: 30583 RVA: 0x0019C0D7 File Offset: 0x0019A2D7
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection2>>> ClusterOnInput(this ProgramSetBuilder<filterSelection2> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection2>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<filterSelection2>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x0019C109 File Offset: 0x0019A309
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection4>>> ClusterOnInput(this ProgramSetBuilder<selection4> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection4>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection4>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007779 RID: 30585 RVA: 0x0019C13B File Offset: 0x0019A33B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection5>>> ClusterOnInput(this ProgramSetBuilder<selection5> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection5>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection5>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600777A RID: 30586 RVA: 0x0019C16D File Offset: 0x0019A36D
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection3>>> ClusterOnInput(this ProgramSetBuilder<filterSelection3> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection3>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<filterSelection3>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600777B RID: 30587 RVA: 0x0019C19F File Offset: 0x0019A39F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection6>>> ClusterOnInput(this ProgramSetBuilder<selection6> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection6>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection6>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600777C RID: 30588 RVA: 0x0019C1D1 File Offset: 0x0019A3D1
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection7>>> ClusterOnInput(this ProgramSetBuilder<selection7> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection7>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection7>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x0019C203 File Offset: 0x0019A403
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection4>>> ClusterOnInput(this ProgramSetBuilder<filterSelection4> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection4>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<filterSelection4>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600777E RID: 30590 RVA: 0x0019C235 File Offset: 0x0019A435
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection8>>> ClusterOnInput(this ProgramSetBuilder<selection8> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection8>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection8>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600777F RID: 30591 RVA: 0x0019C267 File Offset: 0x0019A467
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection9>>> ClusterOnInput(this ProgramSetBuilder<selection9> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection9>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection9>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x0019C299 File Offset: 0x0019A499
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection5>>> ClusterOnInput(this ProgramSetBuilder<filterSelection5> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<filterSelection5>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<filterSelection5>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007781 RID: 30593 RVA: 0x0019C2CB File Offset: 0x0019A4CB
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection10>>> ClusterOnInput(this ProgramSetBuilder<selection10> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IDomNode>>, ProgramSetBuilder<selection10>>(Cluster.CastValue<IEnumerable<IDomNode>>(kvp.Key), ProgramSetBuilder<selection10>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007782 RID: 30594 RVA: 0x0019C2FD File Offset: 0x0019A4FD
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<leafFExpr>>> ClusterOnInput(this ProgramSetBuilder<leafFExpr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<leafFExpr>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<leafFExpr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007783 RID: 30595 RVA: 0x0019C32F File Offset: 0x0019A52F
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<leafAtom>>> ClusterOnInput(this ProgramSetBuilder<leafAtom> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<leafAtom>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<leafAtom>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007784 RID: 30596 RVA: 0x0019C361 File Offset: 0x0019A561
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<atomExpr>>> ClusterOnInput(this ProgramSetBuilder<atomExpr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<atomExpr>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<atomExpr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007785 RID: 30597 RVA: 0x0019C393 File Offset: 0x0019A593
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<literalExpr>>> ClusterOnInput(this ProgramSetBuilder<literalExpr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<literalExpr>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<literalExpr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007786 RID: 30598 RVA: 0x0019C3C5 File Offset: 0x0019A5C5
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<fexpr>>> ClusterOnInput(this ProgramSetBuilder<fexpr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<fexpr>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<fexpr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007787 RID: 30599 RVA: 0x0019C3F7 File Offset: 0x0019A5F7
		public static IEnumerable<KeyValuePair<Optional<string[]>, ProgramSetBuilder<resultFields>>> ClusterOnInput(this ProgramSetBuilder<resultFields> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string[]>, ProgramSetBuilder<resultFields>>(Cluster.CastValue<string[]>(kvp.Key), ProgramSetBuilder<resultFields>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007788 RID: 30600 RVA: 0x0019C429 File Offset: 0x0019A629
		public static IEnumerable<KeyValuePair<Optional<string[]>, ProgramSetBuilder<singletonField>>> ClusterOnInput(this ProgramSetBuilder<singletonField> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string[]>, ProgramSetBuilder<singletonField>>(Cluster.CastValue<string[]>(kvp.Key), ProgramSetBuilder<singletonField>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007789 RID: 30601 RVA: 0x0019C45B File Offset: 0x0019A65B
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<fieldSubstring>>> ClusterOnInput(this ProgramSetBuilder<fieldSubstring> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<fieldSubstring>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<fieldSubstring>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600778A RID: 30602 RVA: 0x0019C48D File Offset: 0x0019A68D
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<y>>> ClusterOnInput(this ProgramSetBuilder<y> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<y>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<y>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600778B RID: 30603 RVA: 0x0019C4BF File Offset: 0x0019A6BF
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<selectSubstring>>> ClusterOnInput(this ProgramSetBuilder<selectSubstring> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<selectSubstring>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<selectSubstring>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600778C RID: 30604 RVA: 0x0019C4F1 File Offset: 0x0019A6F1
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring[]>, ProgramSetBuilder<substringDisj>>> ClusterOnInput(this ProgramSetBuilder<substringDisj> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring[]>, ProgramSetBuilder<substringDisj>>(Cluster.CastValue<ValueSubstring[]>(kvp.Key), ProgramSetBuilder<substringDisj>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600778D RID: 30605 RVA: 0x0019C523 File Offset: 0x0019A723
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<substring>>> ClusterOnInput(this ProgramSetBuilder<substring> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<substring>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<substring>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600778E RID: 30606 RVA: 0x0019C555 File Offset: 0x0019A755
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>, ProgramSetBuilder<resultTable>>> ClusterOnInput(this ProgramSetBuilder<resultTable> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>, ProgramSetBuilder<resultTable>>(Cluster.CastValue<IEnumerable<IEnumerable<WebRegion>>>(kvp.Key), ProgramSetBuilder<resultTable>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600778F RID: 30607 RVA: 0x0019C587 File Offset: 0x0019A787
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>, ProgramSetBuilder<columnSelectors>>> ClusterOnInput(this ProgramSetBuilder<columnSelectors> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>, ProgramSetBuilder<columnSelectors>>(Cluster.CastValue<IEnumerable<IEnumerable<WebRegion>>>(kvp.Key), ProgramSetBuilder<columnSelectors>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007790 RID: 30608 RVA: 0x0019C5B9 File Offset: 0x0019A7B9
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<name>>> ClusterOnInput(this ProgramSetBuilder<name> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<name>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<name>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x0019C5EB File Offset: 0x0019A7EB
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<value>>> ClusterOnInput(this ProgramSetBuilder<value> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<value>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<value>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x0019C61D File Offset: 0x0019A81D
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<cssSelector>>> ClusterOnInput(this ProgramSetBuilder<cssSelector> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<cssSelector>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<cssSelector>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x0019C64F File Offset: 0x0019A84F
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<className>>> ClusterOnInput(this ProgramSetBuilder<className> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<className>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<className>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007794 RID: 30612 RVA: 0x0019C681 File Offset: 0x0019A881
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<idName>>> ClusterOnInput(this ProgramSetBuilder<idName> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<idName>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<idName>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x0019C6B3 File Offset: 0x0019A8B3
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<nodeName>>> ClusterOnInput(this ProgramSetBuilder<nodeName> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<nodeName>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<nodeName>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x0019C6E5 File Offset: 0x0019A8E5
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<propName>>> ClusterOnInput(this ProgramSetBuilder<propName> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<propName>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<propName>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x0019C717 File Offset: 0x0019A917
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<idx1>>> ClusterOnInput(this ProgramSetBuilder<idx1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<idx1>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<idx1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x0019C749 File Offset: 0x0019A949
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<idx2>>> ClusterOnInput(this ProgramSetBuilder<idx2> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<idx2>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<idx2>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x0019C77B File Offset: 0x0019A97B
		public static IEnumerable<KeyValuePair<Optional<string[]>, ProgramSetBuilder<names>>> ClusterOnInput(this ProgramSetBuilder<names> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string[]>, ProgramSetBuilder<names>>(Cluster.CastValue<string[]>(kvp.Key), ProgramSetBuilder<names>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x0019C7AD File Offset: 0x0019A9AD
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<count>>> ClusterOnInput(this ProgramSetBuilder<count> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<count>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<count>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600779B RID: 30619 RVA: 0x0019C7DF File Offset: 0x0019A9DF
		public static IEnumerable<KeyValuePair<Optional<string[]>, ProgramSetBuilder<substringFeatureNames>>> ClusterOnInput(this ProgramSetBuilder<substringFeatureNames> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string[]>, ProgramSetBuilder<substringFeatureNames>>(Cluster.CastValue<string[]>(kvp.Key), ProgramSetBuilder<substringFeatureNames>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600779C RID: 30620 RVA: 0x0019C811 File Offset: 0x0019AA11
		public static IEnumerable<KeyValuePair<Optional<int[]>, ProgramSetBuilder<substringFeatureValues>>> ClusterOnInput(this ProgramSetBuilder<substringFeatureValues> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int[]>, ProgramSetBuilder<substringFeatureValues>>(Cluster.CastValue<int[]>(kvp.Key), ProgramSetBuilder<substringFeatureValues>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600779D RID: 30621 RVA: 0x0019C843 File Offset: 0x0019AA43
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600779E RID: 30622 RVA: 0x0019C875 File Offset: 0x0019AA75
		public static IEnumerable<KeyValuePair<Optional<EntityDetector[]>, ProgramSetBuilder<entityObjs>>> ClusterOnInput(this ProgramSetBuilder<entityObjs> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<EntityDetector[]>, ProgramSetBuilder<entityObjs>>(Cluster.CastValue<EntityDetector[]>(kvp.Key), ProgramSetBuilder<entityObjs>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600779F RID: 30623 RVA: 0x0019C8A7 File Offset: 0x0019AAA7
		public static IEnumerable<KeyValuePair<Optional<KeyDirections>, ProgramSetBuilder<direction>>> ClusterOnInput(this ProgramSetBuilder<direction> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<KeyDirections>, ProgramSetBuilder<direction>>(Cluster.CastValue<KeyDirections>(kvp.Key), ProgramSetBuilder<direction>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A0 RID: 30624 RVA: 0x0019C8D9 File Offset: 0x0019AAD9
		public static IEnumerable<KeyValuePair<Optional<NodeCollection>, ProgramSetBuilder<nodeCollection>>> ClusterOnInput(this ProgramSetBuilder<nodeCollection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<NodeCollection>, ProgramSetBuilder<nodeCollection>>(Cluster.CastValue<NodeCollection>(kvp.Key), ProgramSetBuilder<nodeCollection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A1 RID: 30625 RVA: 0x0019C90B File Offset: 0x0019AB0B
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_NthChild>>> ClusterOnInput(this ProgramSetBuilder<gen_NthChild> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_NthChild>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_NthChild>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A2 RID: 30626 RVA: 0x0019C93D File Offset: 0x0019AB3D
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_NthLastChild>>> ClusterOnInput(this ProgramSetBuilder<gen_NthLastChild> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_NthLastChild>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_NthLastChild>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A3 RID: 30627 RVA: 0x0019C96F File Offset: 0x0019AB6F
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_Class>>> ClusterOnInput(this ProgramSetBuilder<gen_Class> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_Class>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_Class>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A4 RID: 30628 RVA: 0x0019C9A1 File Offset: 0x0019ABA1
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_ID>>> ClusterOnInput(this ProgramSetBuilder<gen_ID> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_ID>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_ID>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x0019C9D3 File Offset: 0x0019ABD3
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_NodeName>>> ClusterOnInput(this ProgramSetBuilder<gen_NodeName> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_NodeName>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_NodeName>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A6 RID: 30630 RVA: 0x0019CA05 File Offset: 0x0019AC05
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_ItemProp>>> ClusterOnInput(this ProgramSetBuilder<gen_ItemProp> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_ItemProp>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_ItemProp>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A7 RID: 30631 RVA: 0x0019CA37 File Offset: 0x0019AC37
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<obj>>> ClusterOnInput(this ProgramSetBuilder<obj> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<obj>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<obj>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A8 RID: 30632 RVA: 0x0019CA69 File Offset: 0x0019AC69
		public static IEnumerable<KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<WebRegion>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<WebRegion>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x0019CA9B File Offset: 0x0019AC9B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<WebRegion>>[], ProgramSetBuilder<resultSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<resultSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<WebRegion>>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<IEnumerable<WebRegion>>>(Cluster.CastValue<IEnumerable<WebRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<WebRegion>>[], ProgramSetBuilder<resultSequence>>(key.Select(func).ToArray<Optional<IEnumerable<WebRegion>>>(), ProgramSetBuilder<resultSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077AA RID: 30634 RVA: 0x0019CACD File Offset: 0x0019ACCD
		public static IEnumerable<KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<resultRegion>>> ClusterOnInputTuple(this ProgramSetBuilder<resultRegion> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<WebRegion>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<WebRegion>>(Cluster.CastValue<WebRegion>));
				}
				return new KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<resultRegion>>(key.Select(func).ToArray<Optional<WebRegion>>(), ProgramSetBuilder<resultRegion>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077AB RID: 30635 RVA: 0x0019CAFF File Offset: 0x0019ACFF
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<WebRegion>>[], ProgramSetBuilder<subNodeSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<subNodeSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<WebRegion>>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<IEnumerable<WebRegion>>>(Cluster.CastValue<IEnumerable<WebRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<WebRegion>>[], ProgramSetBuilder<subNodeSequence>>(key.Select(func).ToArray<Optional<IEnumerable<WebRegion>>>(), ProgramSetBuilder<subNodeSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x0019CB31 File Offset: 0x0019AD31
		public static IEnumerable<KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<subNode>>> ClusterOnInputTuple(this ProgramSetBuilder<subNode> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<WebRegion>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<WebRegion>>(Cluster.CastValue<WebRegion>));
				}
				return new KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<subNode>>(key.Select(func).ToArray<Optional<WebRegion>>(), ProgramSetBuilder<subNode>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077AD RID: 30637 RVA: 0x0019CB63 File Offset: 0x0019AD63
		public static IEnumerable<KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<mapNodeInSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<mapNodeInSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<WebRegion>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<WebRegion>>(Cluster.CastValue<WebRegion>));
				}
				return new KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<mapNodeInSequence>>(key.Select(func).ToArray<Optional<WebRegion>>(), ProgramSetBuilder<mapNodeInSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x0019CB95 File Offset: 0x0019AD95
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<WebRegion>>[], ProgramSetBuilder<regionSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<regionSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<WebRegion>>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<IEnumerable<WebRegion>>>(Cluster.CastValue<IEnumerable<WebRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<WebRegion>>[], ProgramSetBuilder<regionSequence>>(key.Select(func).ToArray<Optional<IEnumerable<WebRegion>>>(), ProgramSetBuilder<regionSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x0019CBC7 File Offset: 0x0019ADC7
		public static IEnumerable<KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<region>>> ClusterOnInputTuple(this ProgramSetBuilder<region> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<WebRegion>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<WebRegion>>(Cluster.CastValue<WebRegion>));
				}
				return new KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<region>>(key.Select(func).ToArray<Optional<WebRegion>>(), ProgramSetBuilder<region>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x0019CBF9 File Offset: 0x0019ADF9
		public static IEnumerable<KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<mapRegionInSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<mapRegionInSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<WebRegion>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<WebRegion>>(Cluster.CastValue<WebRegion>));
				}
				return new KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<mapRegionInSequence>>(key.Select(func).ToArray<Optional<WebRegion>>(), ProgramSetBuilder<mapRegionInSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B1 RID: 30641 RVA: 0x0019CC2B File Offset: 0x0019AE2B
		public static IEnumerable<KeyValuePair<Optional<IDomNode>[], ProgramSetBuilder<beginNode>>> ClusterOnInputTuple(this ProgramSetBuilder<beginNode> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IDomNode>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<IDomNode>>(Cluster.CastValue<IDomNode>));
				}
				return new KeyValuePair<Optional<IDomNode>[], ProgramSetBuilder<beginNode>>(key.Select(func).ToArray<Optional<IDomNode>>(), ProgramSetBuilder<beginNode>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B2 RID: 30642 RVA: 0x0019CC5D File Offset: 0x0019AE5D
		public static IEnumerable<KeyValuePair<Optional<IDomNode>[], ProgramSetBuilder<endNode>>> ClusterOnInputTuple(this ProgramSetBuilder<endNode> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IDomNode>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<IDomNode>>(Cluster.CastValue<IDomNode>));
				}
				return new KeyValuePair<Optional<IDomNode>[], ProgramSetBuilder<endNode>>(key.Select(func).ToArray<Optional<IDomNode>>(), ProgramSetBuilder<endNode>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B3 RID: 30643 RVA: 0x0019CC8F File Offset: 0x0019AE8F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection>>> ClusterOnInputTuple(this ProgramSetBuilder<selection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B4 RID: 30644 RVA: 0x0019CCC1 File Offset: 0x0019AEC1
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection>>> ClusterOnInputTuple(this ProgramSetBuilder<filterSelection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<filterSelection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x0019CCF3 File Offset: 0x0019AEF3
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selectionEnd>>> ClusterOnInputTuple(this ProgramSetBuilder<selectionEnd> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selectionEnd>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selectionEnd>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B6 RID: 30646 RVA: 0x0019CD25 File Offset: 0x0019AF25
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<regionStartSiblings>>> ClusterOnInputTuple(this ProgramSetBuilder<regionStartSiblings> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<regionStartSiblings>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<regionStartSiblings>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x0019CD57 File Offset: 0x0019AF57
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection2>>> ClusterOnInputTuple(this ProgramSetBuilder<selection2> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection2>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection2>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x0019CD89 File Offset: 0x0019AF89
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection3>>> ClusterOnInputTuple(this ProgramSetBuilder<selection3> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection3>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection3>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x0019CDBB File Offset: 0x0019AFBB
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection2>>> ClusterOnInputTuple(this ProgramSetBuilder<filterSelection2> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection2>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<filterSelection2>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x0019CDED File Offset: 0x0019AFED
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection4>>> ClusterOnInputTuple(this ProgramSetBuilder<selection4> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection4>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection4>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x0019CE1F File Offset: 0x0019B01F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection5>>> ClusterOnInputTuple(this ProgramSetBuilder<selection5> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection5>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection5>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077BC RID: 30652 RVA: 0x0019CE51 File Offset: 0x0019B051
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection3>>> ClusterOnInputTuple(this ProgramSetBuilder<filterSelection3> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection3>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<filterSelection3>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077BD RID: 30653 RVA: 0x0019CE83 File Offset: 0x0019B083
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection6>>> ClusterOnInputTuple(this ProgramSetBuilder<selection6> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection6>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection6>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077BE RID: 30654 RVA: 0x0019CEB5 File Offset: 0x0019B0B5
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection7>>> ClusterOnInputTuple(this ProgramSetBuilder<selection7> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection7>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection7>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077BF RID: 30655 RVA: 0x0019CEE7 File Offset: 0x0019B0E7
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection4>>> ClusterOnInputTuple(this ProgramSetBuilder<filterSelection4> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection4>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<filterSelection4>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C0 RID: 30656 RVA: 0x0019CF19 File Offset: 0x0019B119
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection8>>> ClusterOnInputTuple(this ProgramSetBuilder<selection8> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection8>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection8>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x0019CF4B File Offset: 0x0019B14B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection9>>> ClusterOnInputTuple(this ProgramSetBuilder<selection9> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection9>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection9>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C2 RID: 30658 RVA: 0x0019CF7D File Offset: 0x0019B17D
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection5>>> ClusterOnInputTuple(this ProgramSetBuilder<filterSelection5> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<filterSelection5>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<filterSelection5>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C3 RID: 30659 RVA: 0x0019CFAF File Offset: 0x0019B1AF
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection10>>> ClusterOnInputTuple(this ProgramSetBuilder<selection10> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IDomNode>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<IDomNode>>>(Cluster.CastValue<IEnumerable<IDomNode>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IDomNode>>[], ProgramSetBuilder<selection10>>(key.Select(func).ToArray<Optional<IEnumerable<IDomNode>>>(), ProgramSetBuilder<selection10>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C4 RID: 30660 RVA: 0x0019CFE1 File Offset: 0x0019B1E1
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<leafFExpr>>> ClusterOnInputTuple(this ProgramSetBuilder<leafFExpr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<leafFExpr>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<leafFExpr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C5 RID: 30661 RVA: 0x0019D013 File Offset: 0x0019B213
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<leafAtom>>> ClusterOnInputTuple(this ProgramSetBuilder<leafAtom> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<leafAtom>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<leafAtom>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x0019D045 File Offset: 0x0019B245
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<atomExpr>>> ClusterOnInputTuple(this ProgramSetBuilder<atomExpr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<atomExpr>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<atomExpr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C7 RID: 30663 RVA: 0x0019D077 File Offset: 0x0019B277
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<literalExpr>>> ClusterOnInputTuple(this ProgramSetBuilder<literalExpr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<literalExpr>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<literalExpr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C8 RID: 30664 RVA: 0x0019D0A9 File Offset: 0x0019B2A9
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<fexpr>>> ClusterOnInputTuple(this ProgramSetBuilder<fexpr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<fexpr>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<fexpr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077C9 RID: 30665 RVA: 0x0019D0DB File Offset: 0x0019B2DB
		public static IEnumerable<KeyValuePair<Optional<string[]>[], ProgramSetBuilder<resultFields>>> ClusterOnInputTuple(this ProgramSetBuilder<resultFields> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string[]>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string[]>>(Cluster.CastValue<string[]>));
				}
				return new KeyValuePair<Optional<string[]>[], ProgramSetBuilder<resultFields>>(key.Select(func).ToArray<Optional<string[]>>(), ProgramSetBuilder<resultFields>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077CA RID: 30666 RVA: 0x0019D10D File Offset: 0x0019B30D
		public static IEnumerable<KeyValuePair<Optional<string[]>[], ProgramSetBuilder<singletonField>>> ClusterOnInputTuple(this ProgramSetBuilder<singletonField> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string[]>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string[]>>(Cluster.CastValue<string[]>));
				}
				return new KeyValuePair<Optional<string[]>[], ProgramSetBuilder<singletonField>>(key.Select(func).ToArray<Optional<string[]>>(), ProgramSetBuilder<singletonField>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077CB RID: 30667 RVA: 0x0019D13F File Offset: 0x0019B33F
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<fieldSubstring>>> ClusterOnInputTuple(this ProgramSetBuilder<fieldSubstring> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<fieldSubstring>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<fieldSubstring>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077CC RID: 30668 RVA: 0x0019D171 File Offset: 0x0019B371
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<y>>> ClusterOnInputTuple(this ProgramSetBuilder<y> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<y>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<y>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077CD RID: 30669 RVA: 0x0019D1A3 File Offset: 0x0019B3A3
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<selectSubstring>>> ClusterOnInputTuple(this ProgramSetBuilder<selectSubstring> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<selectSubstring>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<selectSubstring>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077CE RID: 30670 RVA: 0x0019D1D5 File Offset: 0x0019B3D5
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring[]>[], ProgramSetBuilder<substringDisj>>> ClusterOnInputTuple(this ProgramSetBuilder<substringDisj> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring[]>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<ValueSubstring[]>>(Cluster.CastValue<ValueSubstring[]>));
				}
				return new KeyValuePair<Optional<ValueSubstring[]>[], ProgramSetBuilder<substringDisj>>(key.Select(func).ToArray<Optional<ValueSubstring[]>>(), ProgramSetBuilder<substringDisj>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077CF RID: 30671 RVA: 0x0019D207 File Offset: 0x0019B407
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<substring>>> ClusterOnInputTuple(this ProgramSetBuilder<substring> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<substring>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<substring>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x0019D239 File Offset: 0x0019B439
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>[], ProgramSetBuilder<resultTable>>> ClusterOnInputTuple(this ProgramSetBuilder<resultTable> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IEnumerable<WebRegion>>>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<IEnumerable<IEnumerable<WebRegion>>>>(Cluster.CastValue<IEnumerable<IEnumerable<WebRegion>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>[], ProgramSetBuilder<resultTable>>(key.Select(func).ToArray<Optional<IEnumerable<IEnumerable<WebRegion>>>>(), ProgramSetBuilder<resultTable>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x0019D26B File Offset: 0x0019B46B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>[], ProgramSetBuilder<columnSelectors>>> ClusterOnInputTuple(this ProgramSetBuilder<columnSelectors> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IEnumerable<WebRegion>>>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<IEnumerable<IEnumerable<WebRegion>>>>(Cluster.CastValue<IEnumerable<IEnumerable<WebRegion>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IEnumerable<WebRegion>>>[], ProgramSetBuilder<columnSelectors>>(key.Select(func).ToArray<Optional<IEnumerable<IEnumerable<WebRegion>>>>(), ProgramSetBuilder<columnSelectors>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D2 RID: 30674 RVA: 0x0019D29D File Offset: 0x0019B49D
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<name>>> ClusterOnInputTuple(this ProgramSetBuilder<name> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<name>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<name>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x0019D2CF File Offset: 0x0019B4CF
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<value>>> ClusterOnInputTuple(this ProgramSetBuilder<value> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<value>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<value>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x0019D301 File Offset: 0x0019B501
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<cssSelector>>> ClusterOnInputTuple(this ProgramSetBuilder<cssSelector> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<cssSelector>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<cssSelector>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D5 RID: 30677 RVA: 0x0019D333 File Offset: 0x0019B533
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<className>>> ClusterOnInputTuple(this ProgramSetBuilder<className> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<className>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<className>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D6 RID: 30678 RVA: 0x0019D365 File Offset: 0x0019B565
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<idName>>> ClusterOnInputTuple(this ProgramSetBuilder<idName> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<idName>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<idName>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D7 RID: 30679 RVA: 0x0019D397 File Offset: 0x0019B597
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<nodeName>>> ClusterOnInputTuple(this ProgramSetBuilder<nodeName> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<nodeName>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<nodeName>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D8 RID: 30680 RVA: 0x0019D3C9 File Offset: 0x0019B5C9
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<propName>>> ClusterOnInputTuple(this ProgramSetBuilder<propName> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<propName>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<propName>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077D9 RID: 30681 RVA: 0x0019D3FB File Offset: 0x0019B5FB
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<idx1>>> ClusterOnInputTuple(this ProgramSetBuilder<idx1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<idx1>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<idx1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077DA RID: 30682 RVA: 0x0019D42D File Offset: 0x0019B62D
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<idx2>>> ClusterOnInputTuple(this ProgramSetBuilder<idx2> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<idx2>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<idx2>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077DB RID: 30683 RVA: 0x0019D45F File Offset: 0x0019B65F
		public static IEnumerable<KeyValuePair<Optional<string[]>[], ProgramSetBuilder<names>>> ClusterOnInputTuple(this ProgramSetBuilder<names> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string[]>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string[]>>(Cluster.CastValue<string[]>));
				}
				return new KeyValuePair<Optional<string[]>[], ProgramSetBuilder<names>>(key.Select(func).ToArray<Optional<string[]>>(), ProgramSetBuilder<names>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077DC RID: 30684 RVA: 0x0019D491 File Offset: 0x0019B691
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<count>>> ClusterOnInputTuple(this ProgramSetBuilder<count> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<count>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<count>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x0019D4C3 File Offset: 0x0019B6C3
		public static IEnumerable<KeyValuePair<Optional<string[]>[], ProgramSetBuilder<substringFeatureNames>>> ClusterOnInputTuple(this ProgramSetBuilder<substringFeatureNames> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string[]>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string[]>>(Cluster.CastValue<string[]>));
				}
				return new KeyValuePair<Optional<string[]>[], ProgramSetBuilder<substringFeatureNames>>(key.Select(func).ToArray<Optional<string[]>>(), ProgramSetBuilder<substringFeatureNames>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x0019D4F5 File Offset: 0x0019B6F5
		public static IEnumerable<KeyValuePair<Optional<int[]>[], ProgramSetBuilder<substringFeatureValues>>> ClusterOnInputTuple(this ProgramSetBuilder<substringFeatureValues> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int[]>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<int[]>>(Cluster.CastValue<int[]>));
				}
				return new KeyValuePair<Optional<int[]>[], ProgramSetBuilder<substringFeatureValues>>(key.Select(func).ToArray<Optional<int[]>>(), ProgramSetBuilder<substringFeatureValues>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077DF RID: 30687 RVA: 0x0019D527 File Offset: 0x0019B727
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>> ClusterOnInputTuple(this ProgramSetBuilder<k> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E0 RID: 30688 RVA: 0x0019D559 File Offset: 0x0019B759
		public static IEnumerable<KeyValuePair<Optional<EntityDetector[]>[], ProgramSetBuilder<entityObjs>>> ClusterOnInputTuple(this ProgramSetBuilder<entityObjs> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<EntityDetector[]>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<EntityDetector[]>>(Cluster.CastValue<EntityDetector[]>));
				}
				return new KeyValuePair<Optional<EntityDetector[]>[], ProgramSetBuilder<entityObjs>>(key.Select(func).ToArray<Optional<EntityDetector[]>>(), ProgramSetBuilder<entityObjs>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E1 RID: 30689 RVA: 0x0019D58B File Offset: 0x0019B78B
		public static IEnumerable<KeyValuePair<Optional<KeyDirections>[], ProgramSetBuilder<direction>>> ClusterOnInputTuple(this ProgramSetBuilder<direction> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<KeyDirections>> func;
				if ((func = Cluster.<>O.<13>__CastValue) == null)
				{
					func = (Cluster.<>O.<13>__CastValue = new Func<object, Optional<KeyDirections>>(Cluster.CastValue<KeyDirections>));
				}
				return new KeyValuePair<Optional<KeyDirections>[], ProgramSetBuilder<direction>>(key.Select(func).ToArray<Optional<KeyDirections>>(), ProgramSetBuilder<direction>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E2 RID: 30690 RVA: 0x0019D5BD File Offset: 0x0019B7BD
		public static IEnumerable<KeyValuePair<Optional<NodeCollection>[], ProgramSetBuilder<nodeCollection>>> ClusterOnInputTuple(this ProgramSetBuilder<nodeCollection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<NodeCollection>> func;
				if ((func = Cluster.<>O.<14>__CastValue) == null)
				{
					func = (Cluster.<>O.<14>__CastValue = new Func<object, Optional<NodeCollection>>(Cluster.CastValue<NodeCollection>));
				}
				return new KeyValuePair<Optional<NodeCollection>[], ProgramSetBuilder<nodeCollection>>(key.Select(func).ToArray<Optional<NodeCollection>>(), ProgramSetBuilder<nodeCollection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E3 RID: 30691 RVA: 0x0019D5EF File Offset: 0x0019B7EF
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_NthChild>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_NthChild> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_NthChild>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_NthChild>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E4 RID: 30692 RVA: 0x0019D621 File Offset: 0x0019B821
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_NthLastChild>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_NthLastChild> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_NthLastChild>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_NthLastChild>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E5 RID: 30693 RVA: 0x0019D653 File Offset: 0x0019B853
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_Class>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_Class> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_Class>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_Class>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E6 RID: 30694 RVA: 0x0019D685 File Offset: 0x0019B885
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_ID>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_ID> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_ID>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_ID>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E7 RID: 30695 RVA: 0x0019D6B7 File Offset: 0x0019B8B7
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_NodeName>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_NodeName> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_NodeName>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_NodeName>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x0019D6E9 File Offset: 0x0019B8E9
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_ItemProp>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_ItemProp> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_ItemProp>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_ItemProp>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077E9 RID: 30697 RVA: 0x0019D71B File Offset: 0x0019B91B
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<obj>>> ClusterOnInputTuple(this ProgramSetBuilder<obj> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<obj>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<obj>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x0019D74D File Offset: 0x0019B94D
		public static IEnumerable<KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<WebRegion>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<WebRegion>>(Cluster.CastValue<WebRegion>));
				}
				return new KeyValuePair<Optional<WebRegion>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<WebRegion>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02000FFB RID: 4091
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003281 RID: 12929
			public static Func<object, Optional<IEnumerable<WebRegion>>> <0>__CastValue;

			// Token: 0x04003282 RID: 12930
			public static Func<object, Optional<WebRegion>> <1>__CastValue;

			// Token: 0x04003283 RID: 12931
			public static Func<object, Optional<IDomNode>> <2>__CastValue;

			// Token: 0x04003284 RID: 12932
			public static Func<object, Optional<IEnumerable<IDomNode>>> <3>__CastValue;

			// Token: 0x04003285 RID: 12933
			public static Func<object, Optional<bool>> <4>__CastValue;

			// Token: 0x04003286 RID: 12934
			public static Func<object, Optional<string[]>> <5>__CastValue;

			// Token: 0x04003287 RID: 12935
			public static Func<object, Optional<ValueSubstring>> <6>__CastValue;

			// Token: 0x04003288 RID: 12936
			public static Func<object, Optional<ValueSubstring[]>> <7>__CastValue;

			// Token: 0x04003289 RID: 12937
			public static Func<object, Optional<IEnumerable<IEnumerable<WebRegion>>>> <8>__CastValue;

			// Token: 0x0400328A RID: 12938
			public static Func<object, Optional<string>> <9>__CastValue;

			// Token: 0x0400328B RID: 12939
			public static Func<object, Optional<int>> <10>__CastValue;

			// Token: 0x0400328C RID: 12940
			public static Func<object, Optional<int[]>> <11>__CastValue;

			// Token: 0x0400328D RID: 12941
			public static Func<object, Optional<EntityDetector[]>> <12>__CastValue;

			// Token: 0x0400328E RID: 12942
			public static Func<object, Optional<KeyDirections>> <13>__CastValue;

			// Token: 0x0400328F RID: 12943
			public static Func<object, Optional<NodeCollection>> <14>__CastValue;

			// Token: 0x04003290 RID: 12944
			public static Func<object, Optional<object>> <15>__CastValue;
		}
	}
}
