using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012ED RID: 4845
	public class DelimiterStringsConstraint : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06009200 RID: 37376 RVA: 0x001EBC5A File Offset: 0x001E9E5A
		public DelimiterStringsConstraint(IEnumerable<string> delimiterStrings)
		{
			this.DelimiterStrings = new HashSet<string>(delimiterStrings);
			if (this.DelimiterStrings.Count == 0)
			{
				throw new ArgumentException("Delimiter list must not be empty", "delimiterStrings");
			}
		}

		// Token: 0x06009201 RID: 37377 RVA: 0x001EBC8B File Offset: 0x001E9E8B
		public DelimiterStringsConstraint(params string[] delimiterStrings)
			: this(delimiterStrings)
		{
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06009202 RID: 37378 RVA: 0x001EBC94 File Offset: 0x001E9E94
		public HashSet<string> DelimiterStrings { get; }

		// Token: 0x06009203 RID: 37379 RVA: 0x001EBC9C File Offset: 0x001E9E9C
		public void SetOptions(Witnesses.Options options)
		{
			options.ProvidedDelimiterStrings = this.DelimiterStrings;
		}

		// Token: 0x06009204 RID: 37380 RVA: 0x001EBCAC File Offset: 0x001E9EAC
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			GrammarBuilders build = Language.Build;
			SplitRegion splitRegion;
			if (!build.Node.IsRule.SplitRegion(program.ProgramNode, out splitRegion))
			{
				return false;
			}
			HashSet<string> hashSet = splitRegion.splitMatches.Switch<HashSet<string>>(build, delegate(splitMatches_multipleMatches multipleMatches)
			{
				IEnumerable<string> enumerable = base.<Valid>g__GetDelimiters|0(multipleMatches.multipleMatches);
				if (enumerable == null)
				{
					return null;
				}
				return enumerable.ConvertToHashSet<string>();
			}, (splitMatches_constantDelimiterMatches constDelimMatches) => constDelimMatches.constantDelimiterMatches.Switch<HashSet<string>>(build, (ConstantDelimiterWithQuoting constDelimQuoting) => new HashSet<string> { constDelimQuoting.s.Value }, (ConstantDelimiter constDelim) => new HashSet<string> { constDelim.s.Value }), (splitMatches_fixedWidthMatches fwMatches) => null);
			return hashSet != null && hashSet.SetEquals(this.DelimiterStrings);
		}

		// Token: 0x06009205 RID: 37381 RVA: 0x001EBD48 File Offset: 0x001E9F48
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			return other is FixedWidthConstraint || (other is DelimiterStringsConstraint && !this.Equals(other));
		}

		// Token: 0x06009206 RID: 37382 RVA: 0x001EBD68 File Offset: 0x001E9F68
		public bool Equals(DelimiterStringsConstraint other)
		{
			return other != null && (this == other || this.DelimiterStrings.SetEquals(other.DelimiterStrings));
		}

		// Token: 0x06009207 RID: 37383 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06009208 RID: 37384 RVA: 0x001EBD86 File Offset: 0x001E9F86
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((DelimiterStringsConstraint)obj)));
		}

		// Token: 0x06009209 RID: 37385 RVA: 0x001EBDB4 File Offset: 0x001E9FB4
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			this._hashCode = new int?(this.DelimiterStrings.OrderIndependentHashCode<string>());
			return this._hashCode.Value;
		}

		// Token: 0x0600920A RID: 37386 RVA: 0x001EBDF0 File Offset: 0x001E9FF0
		public override string ToString()
		{
			return "DelimiterStringsConstraint({" + string.Join(", ", this.DelimiterStrings.Select((string str) => str.ToLiteral(null))) + "})";
		}

		// Token: 0x04003BF1 RID: 15345
		private int? _hashCode;
	}
}
