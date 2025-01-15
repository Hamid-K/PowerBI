using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Transformation.Formula;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Constraints
{
	// Token: 0x02001B59 RID: 7001
	public class AllowedOperators : Constraint<ITable<object>, ITable<object>>, IOptionConstraint<Options>
	{
		// Token: 0x0600E5D5 RID: 58837 RVA: 0x0030B32F File Offset: 0x0030952F
		public AllowedOperators(Operators allowed)
		{
			this.Allowed = allowed;
		}

		// Token: 0x17002648 RID: 9800
		// (get) Token: 0x0600E5D6 RID: 58838 RVA: 0x0030B33E File Offset: 0x0030953E
		public Operators Allowed { get; }

		// Token: 0x0600E5D7 RID: 58839 RVA: 0x0030B346 File Offset: 0x00309546
		public static AllowedOperators DefaultForTarget(TargetLanguage target)
		{
			return AllowedOperators.Defaults.MaybeGet(target).OrElseDefault<AllowedOperators>();
		}

		// Token: 0x0600E5D8 RID: 58840 RVA: 0x0030B358 File Offset: 0x00309558
		public override bool ConflictsWith(Constraint<ITable<object>, ITable<object>> other)
		{
			AllowedOperators allowedOperators = other as AllowedOperators;
			return allowedOperators != null && allowedOperators.Allowed != this.Allowed;
		}

		// Token: 0x0600E5D9 RID: 58841 RVA: 0x0030B384 File Offset: 0x00309584
		public override bool Equals(Constraint<ITable<object>, ITable<object>> other)
		{
			AllowedOperators allowedOperators = other as AllowedOperators;
			return allowedOperators != null && allowedOperators.Allowed == this.Allowed;
		}

		// Token: 0x0600E5DA RID: 58842 RVA: 0x0030B3AC File Offset: 0x003095AC
		public override int GetHashCode()
		{
			return this.Allowed.GetHashCode();
		}

		// Token: 0x0600E5DB RID: 58843 RVA: 0x0030B3CD File Offset: 0x003095CD
		public void SetOptions(Options options)
		{
			options.AllowedOperators = this.Allowed;
		}

		// Token: 0x0600E5DC RID: 58844 RVA: 0x0030B3DC File Offset: 0x003095DC
		public override bool Valid(Program<ITable<object>, ITable<object>> program)
		{
			if (this.Allowed == Operators.All)
			{
				return true;
			}
			if (!this.Allowed.HasFlag(Operators.SplitText))
			{
				if (program.ProgramNode.SubPrograms.Any((ProgramNode node) => node.Grammar == Microsoft.ProgramSynthesis.Split.Text.Language.Grammar))
				{
					return false;
				}
			}
			if (!this.Allowed.HasFlag(Operators.TransformationFormula))
			{
				if (program.ProgramNode.SubPrograms.Any((ProgramNode node) => node.Grammar == Microsoft.ProgramSynthesis.Transformation.Formula.Language.Grammar))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600E5DD RID: 58845 RVA: 0x0030B48C File Offset: 0x0030968C
		// Note: this type is marked as 'beforefieldinit'.
		static AllowedOperators()
		{
			Dictionary<TargetLanguage, Operators> dictionary = new Dictionary<TargetLanguage, Operators>();
			dictionary[TargetLanguage.Pandas] = Operators.All;
			dictionary[TargetLanguage.PowerQueryM] = ~Operators.LabelEncoding;
			AllowedOperators.Defaults = dictionary.ToDictionary((KeyValuePair<TargetLanguage, Operators> kv) => kv.Key, (KeyValuePair<TargetLanguage, Operators> kv) => new AllowedOperators(kv.Value));
		}

		// Token: 0x04005748 RID: 22344
		private static readonly IReadOnlyDictionary<TargetLanguage, AllowedOperators> Defaults;
	}
}
