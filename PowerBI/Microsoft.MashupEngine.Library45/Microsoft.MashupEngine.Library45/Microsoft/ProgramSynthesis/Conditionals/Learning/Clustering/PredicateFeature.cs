using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Conditionals.Build;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering
{
	// Token: 0x02000A6C RID: 2668
	public abstract class PredicateFeature
	{
		// Token: 0x06004226 RID: 16934 RVA: 0x000CEEC9 File Offset: 0x000CD0C9
		protected PredicateFeature(PredicateType type, bool negated, int k)
		{
			this.Type = type;
			this.Negated = negated;
			this.K = k;
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06004227 RID: 16935 RVA: 0x000CEEE6 File Offset: 0x000CD0E6
		public int K { get; }

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x000CEEEE File Offset: 0x000CD0EE
		public bool Negated { get; }

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x000CEEF8 File Offset: 0x000CD0F8
		public double Score
		{
			get
			{
				double num = this._score.GetValueOrDefault();
				if (this._score == null)
				{
					num = (this.Negated ? (2.0 * this.CalculateScore()) : this.CalculateScore());
					this._score = new double?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x000CEF4E File Offset: 0x000CD14E
		public PredicateType Type { get; }

		// Token: 0x0600422B RID: 16939
		public abstract pred GenerateProgramNode(GrammarBuilders build);

		// Token: 0x0600422C RID: 16940 RVA: 0x000CEF58 File Offset: 0x000CD158
		public static HashSet<PredicateFeature> GetFeaturesFor(LearningCacheSubstring input, bool supportRegex)
		{
			if (string.IsNullOrWhiteSpace((input != null) ? input.Value : null))
			{
				return new HashSet<PredicateFeature>
				{
					new BasicPredicateFeature(PredicateType.IsNullOrWhiteSpace, false, 1),
					(input == null || input.Value == null) ? new BasicPredicateFeature(PredicateType.IsNull, false, 1) : new BasicPredicateFeature(PredicateType.IsWhiteSpace, false, 1)
				};
			}
			HashSet<PredicateFeature> hashSet = new HashSet<PredicateFeature>();
			char c3 = input.Value[0];
			if (char.IsDigit(c3))
			{
				hashSet.Add(new BasicPredicateFeature(PredicateType.StartsWithDigit, false, 1));
			}
			else if (char.IsLetter(c3))
			{
				hashSet.Add(new BasicPredicateFeature(PredicateType.StartsWithLetter, false, 1));
			}
			else if (PredicateFeature.<GetFeaturesFor>g__IsSymbol|14_0(c3))
			{
				hashSet.Add(new StringPredicateFeature(c3.ToString(), PredicateType.StartsWithString, false, 1));
			}
			char c2 = input.Value.Last<char>();
			if (char.IsDigit(c2))
			{
				hashSet.Add(new BasicPredicateFeature(PredicateType.EndsWithDigit, false, 1));
			}
			else if (char.IsLetter(c2))
			{
				hashSet.Add(new BasicPredicateFeature(PredicateType.EndsWithLetter, false, 1));
			}
			else if (PredicateFeature.<GetFeaturesFor>g__IsSymbol|14_0(c2))
			{
				hashSet.Add(new StringPredicateFeature(c2.ToString(), PredicateType.EndsWithString, false, 1));
			}
			hashSet.AddRange(from c in input.Value
				where PredicateFeature.<GetFeaturesFor>g__IsSymbol|14_0(c)
				group c by c into grp
				select new StringPredicateFeature(grp.Key.ToString(), PredicateType.ContainsString, false, grp.Count<char>()));
			if (supportRegex)
			{
				hashSet.AddRange(from re in RegularExpression.LearnFullMatches(input, 5, 0)
					where re.Count > 0
					orderby re.Score descending
					select new RegexPredicateFeature(re, PredicateType.Matches, false, 1));
				hashSet.AddRange(from re in RegularExpression.LearnRightMatches(input, 0U, 3, 0)
					where re.Count > 0
					orderby re.Score descending
					select new RegexPredicateFeature(re, PredicateType.StartsWith, false, 1));
				hashSet.AddRange(from re in RegularExpression.LearnLeftMatches(input, input.Length, 3, 0)
					where re.Count > 0
					orderby re.Score descending
					select new RegexPredicateFeature(re, PredicateType.EndsWith, false, 1));
			}
			return hashSet;
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000CF264 File Offset: 0x000CD464
		public override int GetHashCode()
		{
			return (int)((((this.Type * (PredicateType)6599) ^ (PredicateType)this.Negated.GetHashCode()) * (PredicateType)1873) ^ (PredicateType)this.K.GetHashCode());
		}

		// Token: 0x0600422E RID: 16942
		public abstract PredicateFeature Negate();

		// Token: 0x0600422F RID: 16943
		protected abstract double CalculateScore();

		// Token: 0x06004230 RID: 16944 RVA: 0x000CF2A1 File Offset: 0x000CD4A1
		[CompilerGenerated]
		internal static bool <GetFeaturesFor>g__IsSymbol|14_0(char c)
		{
			return !char.IsWhiteSpace(c) && !char.IsLetterOrDigit(c);
		}

		// Token: 0x04001DC5 RID: 7621
		private double? _score;
	}
}
