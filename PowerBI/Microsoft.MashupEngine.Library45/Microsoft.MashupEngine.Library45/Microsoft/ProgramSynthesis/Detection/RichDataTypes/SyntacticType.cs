using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000AAC RID: 2732
	public abstract class SyntacticType : IEquatable<SyntacticType>
	{
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x000D75AF File Offset: 0x000D57AF
		public DataKind BaseKind { get; }

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x000D75B7 File Offset: 0x000D57B7
		public Optional<string> NaValue { get; }

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x000D75BF File Offset: 0x000D57BF
		public IReadOnlyDictionary<string, string> Substitutions { get; }

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x000D75C7 File Offset: 0x000D57C7
		public IEnumerable<string> Examples
		{
			get
			{
				return this._examples;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x000D75CF File Offset: 0x000D57CF
		public IReadOnlyDictionary<string, string> NonEmptySubstitutions { get; }

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x000D75D7 File Offset: 0x000D57D7
		public IReadOnlyDictionary<string, string> EmptySubstitutions { get; }

		// Token: 0x060044B6 RID: 17590
		public abstract Optional<string> Canonicalize(string value);

		// Token: 0x060044B7 RID: 17591
		public abstract bool IsValid(string value);

		// Token: 0x060044B8 RID: 17592
		public abstract bool Equals(SyntacticType other);

		// Token: 0x060044B9 RID: 17593 RVA: 0x000D75E0 File Offset: 0x000D57E0
		protected SyntacticType(DataKind kind, Dictionary<string, string> substitutions, string naValue = null)
		{
			this.BaseKind = kind;
			this.NaValue = naValue.SomeIfNotNull<string>();
			IReadOnlyDictionary<string, string> readOnlyDictionary;
			if (substitutions == null)
			{
				readOnlyDictionary = null;
			}
			else
			{
				readOnlyDictionary = substitutions.Where((KeyValuePair<string, string> kvp) => kvp.Key != kvp.Value).ToDictionary<string, string>();
			}
			this.Substitutions = readOnlyDictionary ?? new Dictionary<string, string>();
			this.NonEmptySubstitutions = this.Substitutions.Where((KeyValuePair<string, string> kvp) => !string.IsNullOrEmpty(kvp.Value)).ToDictionary<string, string>();
			this.EmptySubstitutions = this.Substitutions.Where((KeyValuePair<string, string> kvp) => string.IsNullOrEmpty(kvp.Value)).ToDictionary<string, string>();
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x000D76BA File Offset: 0x000D58BA
		public virtual DataKind Kind
		{
			get
			{
				return this.BaseKind;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x000D76C4 File Offset: 0x000D58C4
		public bool IsNaValue
		{
			get
			{
				return this.NaValue.HasValue;
			}
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000D76DF File Offset: 0x000D58DF
		internal void AddExample(string example)
		{
			if (this._examples.Count < 5)
			{
				this._examples.Add(example);
			}
		}

		// Token: 0x04001F5D RID: 8029
		private const int MaxExamples = 5;

		// Token: 0x04001F61 RID: 8033
		private readonly SortedSet<string> _examples = new SortedSet<string>();
	}
}
