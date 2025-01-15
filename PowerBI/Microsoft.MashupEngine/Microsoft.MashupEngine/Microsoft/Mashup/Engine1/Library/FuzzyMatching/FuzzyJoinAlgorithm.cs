using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B39 RID: 2873
	public class FuzzyJoinAlgorithm
	{
		// Token: 0x06004FC8 RID: 20424 RVA: 0x0010B37E File Offset: 0x0010957E
		public bool Supports(TableTypeAlgebra.JoinKind joinKind)
		{
			if (joinKind > TableTypeAlgebra.JoinKind.RightAnti)
			{
				if (joinKind - TableTypeAlgebra.JoinKind.LeftSemi > 1)
				{
				}
				return false;
			}
			return true;
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x0010B38F File Offset: 0x0010958F
		public IEnumerable<IValueReference> Join(IEngineHost host, FuzzyJoinParameters parameters)
		{
			return new FuzzyJoinAlgorithm.FuzzyJoinEnumerable(host, parameters);
		}

		// Token: 0x04002AC3 RID: 10947
		public static readonly FuzzyJoinAlgorithm Fuzzy = new FuzzyJoinAlgorithm();

		// Token: 0x02000B3A RID: 2874
		private class FuzzyJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06004FCC RID: 20428 RVA: 0x0010B3A4 File Offset: 0x001095A4
			public FuzzyJoinEnumerable(IEngineHost host, FuzzyJoinParameters parameters)
			{
				this.host = host;
				this.parameters = parameters;
			}

			// Token: 0x06004FCD RID: 20429 RVA: 0x0010B3BA File Offset: 0x001095BA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06004FCE RID: 20430 RVA: 0x0010B3C2 File Offset: 0x001095C2
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new FuzzyJoinAlgorithm.FuzzyJoin(this.host, this.parameters).ProduceMatches();
			}

			// Token: 0x04002AC4 RID: 10948
			private readonly IEngineHost host;

			// Token: 0x04002AC5 RID: 10949
			private readonly FuzzyJoinParameters parameters;
		}

		// Token: 0x02000B3B RID: 2875
		private class FuzzyJoin
		{
			// Token: 0x06004FCF RID: 20431 RVA: 0x0010B3DA File Offset: 0x001095DA
			public FuzzyJoin(IEngineHost host, FuzzyJoinParameters parameters)
			{
				this.host = host;
				this.parameters = parameters;
				this.fuzzyMatcher = FuzzyMatcher.New(this.parameters.JoinKind, false);
			}

			// Token: 0x06004FD0 RID: 20432 RVA: 0x0010B407 File Offset: 0x00109607
			public IEnumerator<IValueReference> ProduceMatches()
			{
				return this.fuzzyMatcher.GetJoinValueReferenceEnumerator(this.host, this.parameters);
			}

			// Token: 0x04002AC6 RID: 10950
			private readonly IEngineHost host;

			// Token: 0x04002AC7 RID: 10951
			private readonly FuzzyJoinParameters parameters;

			// Token: 0x04002AC8 RID: 10952
			private readonly FuzzyMatcher fuzzyMatcher;
		}
	}
}
