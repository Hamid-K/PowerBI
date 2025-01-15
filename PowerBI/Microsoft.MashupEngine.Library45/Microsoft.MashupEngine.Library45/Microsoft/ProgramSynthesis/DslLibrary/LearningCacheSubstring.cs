using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007EE RID: 2030
	public abstract class LearningCacheSubstring : Substring
	{
		// Token: 0x06002B44 RID: 11076 RVA: 0x00078DD9 File Offset: 0x00076FD9
		protected LearningCacheSubstring(string s, uint start, uint end, StringLearningCache cache)
			: base(s, start, end)
		{
			this.Cache = cache;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x00078DEC File Offset: 0x00076FEC
		protected LearningCacheSubstring(LearningCacheSubstring other)
			: this(other.Source, other.Start, other.End, other.Cache.CloneWithCurrentCacheState())
		{
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00078E11 File Offset: 0x00077011
		protected LearningCacheSubstring(string s, IReadOnlyDictionary<string, Token> tokens)
			: base(s, 0U, (uint)s.Length)
		{
			if (tokens != null && tokens.Count > 0)
			{
				this.Cache = new StringLearningCache(s, tokens);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x00078E3A File Offset: 0x0007703A
		[IgnoreDataMember]
		public StringLearningCache Cache { get; }
	}
}
