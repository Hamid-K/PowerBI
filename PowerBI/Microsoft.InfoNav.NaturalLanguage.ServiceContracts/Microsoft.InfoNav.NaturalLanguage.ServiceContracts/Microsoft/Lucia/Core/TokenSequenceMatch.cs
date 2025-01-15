using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200013B RID: 315
	public class TokenSequenceMatch<T>
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0000AE24 File Offset: 0x00009024
		public TokenSequenceMatch(T data, TokenSequenceMatchType matchType, IEnumerable<TokenMatch> tokenMatches, IEnumerable<TextSegment> matchedSegments, string sequenceText, double? weight, double score)
		{
			this.Data = data;
			this.MatchType = matchType;
			this.TokenMatches = tokenMatches.AsReadOnlyCollection<TokenMatch>();
			this.MatchedSegments = matchedSegments.AsReadOnlyList<TextSegment>();
			this.SequenceText = sequenceText;
			this.Weight = weight;
			this.Score = score;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000AE76 File Offset: 0x00009076
		public T Data { get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000AE7E File Offset: 0x0000907E
		public TokenSequenceMatchType MatchType { get; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000AE86 File Offset: 0x00009086
		public ReadOnlyCollection<TokenMatch> TokenMatches { get; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000AE8E File Offset: 0x0000908E
		public IReadOnlyList<TextSegment> MatchedSegments { get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000AE96 File Offset: 0x00009096
		public string SequenceText { get; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000AE9E File Offset: 0x0000909E
		public double? Weight { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000AEA6 File Offset: 0x000090A6
		public double Score { get; }

		// Token: 0x0600062F RID: 1583 RVA: 0x0000AEAE File Offset: 0x000090AE
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public virtual string ToString(bool verbose)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append(this.Data);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.SequenceText);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.MatchType);
			if (verbose)
			{
				stringBuilder.Append(" [");
				if (this.TokenMatches.Count > 0)
				{
					int num = 0;
					stringBuilder.Append(this.TokenMatches[0].TokenIndex);
					int i = 1;
					while (i < this.TokenMatches.Count)
					{
						if (i == this.TokenMatches.Count - 1 || this.TokenMatches[i].TokenIndex + 1 != this.TokenMatches[i + 1].TokenIndex)
						{
							if (i > num)
							{
								stringBuilder.Append("-");
								stringBuilder.Append(this.TokenMatches[i].TokenIndex);
							}
							if (i == this.TokenMatches.Count - 1)
							{
								break;
							}
							stringBuilder.Append(", ");
							stringBuilder.Append(this.TokenMatches[++i].TokenIndex);
							num = i;
						}
						else
						{
							i++;
						}
					}
					stringBuilder.Append("]");
				}
				if (this.MatchedSegments.Count > 0)
				{
					stringBuilder.Append(StringUtil.FormatInvariant(" [{0}]", string.Join<TextSegment>(",", this.MatchedSegments)));
				}
			}
			if (this.Score != 1.0 || this.Weight != null)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.Score);
			}
			if (this.Weight != null)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.Weight);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public TokenSequenceMatch<T> With(string text, double score, T data, IEnumerable<TextSegment> matchedSegments)
		{
			return new TokenSequenceMatch<T>(data, this.MatchType, this.TokenMatches, matchedSegments, text, this.Weight, score);
		}
	}
}
