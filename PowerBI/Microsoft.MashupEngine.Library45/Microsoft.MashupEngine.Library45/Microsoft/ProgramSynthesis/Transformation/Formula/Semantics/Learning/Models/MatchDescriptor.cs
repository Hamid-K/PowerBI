using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D7 RID: 5847
	public class MatchDescriptor : IEquatable<MatchDescriptor>
	{
		// Token: 0x0600C30C RID: 49932 RVA: 0x002A033C File Offset: 0x0029E53C
		private MatchDescriptor(string pattern, MatchName name = MatchName.Unassigned)
		{
			this.Pattern = pattern;
			this.Name = name;
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x0600C30D RID: 49933 RVA: 0x002A0352 File Offset: 0x0029E552
		public MatchName Name { get; }

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x0600C30E RID: 49934 RVA: 0x002A035A File Offset: 0x0029E55A
		public string Pattern { get; }

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x0600C30F RID: 49935 RVA: 0x002A0364 File Offset: 0x0029E564
		public Regex Regex
		{
			get
			{
				Regex regex;
				if ((regex = this._regex) == null)
				{
					regex = (this._regex = this.Pattern.ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant));
				}
				return regex;
			}
		}

		// Token: 0x0600C310 RID: 49936 RVA: 0x002A0394 File Offset: 0x0029E594
		public bool Equals(MatchDescriptor other)
		{
			return other != null && this.ToEqualString() == other.ToEqualString();
		}

		// Token: 0x0600C311 RID: 49937 RVA: 0x002A03B2 File Offset: 0x0029E5B2
		public override bool Equals(object other)
		{
			return this.Equals(other as MatchDescriptor);
		}

		// Token: 0x0600C312 RID: 49938 RVA: 0x002A03C0 File Offset: 0x0029E5C0
		public override int GetHashCode()
		{
			return this.ToEqualString().GetHashCode();
		}

		// Token: 0x0600C313 RID: 49939 RVA: 0x002A03CD File Offset: 0x0029E5CD
		public static bool operator ==(MatchDescriptor left, MatchDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C314 RID: 49940 RVA: 0x002A03E3 File Offset: 0x0029E5E3
		public static bool operator !=(MatchDescriptor left, MatchDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C315 RID: 49941 RVA: 0x002A03F0 File Offset: 0x0029E5F0
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("/{0}/", this.Regex));
			}
			return text;
		}

		// Token: 0x0600C316 RID: 49942 RVA: 0x002A0420 File Offset: 0x0029E620
		private string ToEqualString()
		{
			string text;
			if ((text = this._toEqualString) == null)
			{
				text = (this._toEqualString = string.Format("{0}:{1}", this.Name, this));
			}
			return text;
		}

		// Token: 0x04004BD9 RID: 19417
		internal static readonly IReadOnlyList<MatchDescriptor> Descriptors = new MatchDescriptor[]
		{
			new MatchDescriptor("[0-9]+", MatchName.DigitBlock),
			new MatchDescriptor("[0-9]", (MatchName)int.MinValue),
			new MatchDescriptor("\\p{Zs}* \\p{Zs}*", MatchName.Whitespace),
			new MatchDescriptor("\\p{Ll}+\\p{Zs}+", MatchName.Unassigned),
			new MatchDescriptor("\\p{L}+", MatchName.MultipleLowerChar),
			new MatchDescriptor("\\p{Lu}(\\p{Ll})+\\p{Zs}+", MatchName.Unassigned),
			new MatchDescriptor("[-.\\p{L}]+", MatchName.Unassigned),
			new MatchDescriptor("\\p{Lu}(\\p{Ll})+", MatchName.Unassigned),
			new MatchDescriptor("\\p{Lu}", MatchName.UpperChar),
			new MatchDescriptor("\\p{Ll}", MatchName.Unassigned),
			new MatchDescriptor("\\p{Zs}+\\p{Lu}", MatchName.Unassigned),
			new MatchDescriptor("\\p{L}+$", MatchName.LastWord),
			new MatchDescriptor("\\p{Zs}*\\W\\p{Zs}*", MatchName.DelimiterWithWhitespace),
			new MatchDescriptor("\\p{Ll}{2,}", MatchName.LowercaseWord),
			new MatchDescriptor("\\p{Ll}(?=[-\\p{Zs}]+|$)", MatchName.LowerCharSpaceOrEnd),
			new MatchDescriptor("\\p{Lu}\\p{Ll}", MatchName.UpperLowerChar),
			new MatchDescriptor("\\p{Zs}+\\p{Lu}\\p{Ll}", MatchName.WhitespaceUpperLowerChar),
			new MatchDescriptor("(?<=[-\\p{Zs}]+|^)\\p{Lu}\\p{Ll}", MatchName.Unassigned),
			new MatchDescriptor("(\\s?\\,|;|(\\s+\\()|\\-|\\))\\p{Zs}+", MatchName.Unassigned)
		};

		// Token: 0x04004BDA RID: 19418
		internal static readonly IReadOnlyList<MatchDescriptor> DescriptorsAscii = new MatchDescriptor[]
		{
			new MatchDescriptor("[0-9]+", MatchName.DigitBlock),
			new MatchDescriptor("[0-9]", (MatchName)int.MinValue),
			new MatchDescriptor("\\s* \\s*", MatchName.Whitespace),
			new MatchDescriptor("[a-z]+\\s+", MatchName.Unassigned),
			new MatchDescriptor("[a-zA-Z]+", MatchName.MultipleLowerChar),
			new MatchDescriptor("[A-Z][a-z]+\\s+", MatchName.Unassigned),
			new MatchDescriptor("[-.a-zA-Z]+", MatchName.Unassigned),
			new MatchDescriptor("[A-Z][a-z]+", MatchName.Unassigned),
			new MatchDescriptor("[A-Z]", MatchName.UpperChar),
			new MatchDescriptor("[a-z]", MatchName.Unassigned),
			new MatchDescriptor("\\s+[A-Z]", MatchName.Unassigned),
			new MatchDescriptor("[a-zA-Z]+$", MatchName.LastWord),
			new MatchDescriptor("\\s*\\W\\s*", MatchName.DelimiterWithWhitespace),
			new MatchDescriptor("[a-z]{2,}", MatchName.LowercaseWord),
			new MatchDescriptor("[a-z](?=[-\\s]+|$)", MatchName.LowerCharSpaceOrEnd),
			new MatchDescriptor("[A-Z][a-z]", MatchName.UpperLowerChar),
			new MatchDescriptor("\\s+[A-Z][a-z]", MatchName.WhitespaceUpperLowerChar),
			new MatchDescriptor("(?<=[-\\s]+|^)[A-Z][a-z]", MatchName.Unassigned),
			new MatchDescriptor("(\\s?\\,|;|(\\s+\\()|\\-|\\))\\s+", MatchName.Unassigned)
		};

		// Token: 0x04004BDB RID: 19419
		private Regex _regex;

		// Token: 0x04004BDC RID: 19420
		private string _toEqualString;

		// Token: 0x04004BDD RID: 19421
		private string _toString;
	}
}
