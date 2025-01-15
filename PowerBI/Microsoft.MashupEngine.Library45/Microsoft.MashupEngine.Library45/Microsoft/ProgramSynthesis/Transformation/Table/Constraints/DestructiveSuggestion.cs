using System;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Constraints
{
	// Token: 0x02001B5B RID: 7003
	public class DestructiveSuggestion : Constraint<ITable<object>, ITable<object>>, IOptionConstraint<Options>
	{
		// Token: 0x0600E5E4 RID: 58852 RVA: 0x0030B51A File Offset: 0x0030971A
		public DestructiveSuggestion(bool isAllowed)
		{
			this.IsAllowed = isAllowed;
		}

		// Token: 0x17002649 RID: 9801
		// (get) Token: 0x0600E5E5 RID: 58853 RVA: 0x0030B529 File Offset: 0x00309729
		public bool IsAllowed { get; }

		// Token: 0x1700264A RID: 9802
		// (get) Token: 0x0600E5E6 RID: 58854 RVA: 0x0030B531 File Offset: 0x00309731
		public static DestructiveSuggestion Allow { get; } = new DestructiveSuggestion(true);

		// Token: 0x1700264B RID: 9803
		// (get) Token: 0x0600E5E7 RID: 58855 RVA: 0x0030B538 File Offset: 0x00309738
		public static DestructiveSuggestion Disallow { get; } = new DestructiveSuggestion(false);

		// Token: 0x0600E5E8 RID: 58856 RVA: 0x0030B53F File Offset: 0x0030973F
		public void SetOptions(Options options)
		{
			options.DestructiveSuggestion = this.IsAllowed;
		}

		// Token: 0x0600E5E9 RID: 58857 RVA: 0x0030B550 File Offset: 0x00309750
		public override bool ConflictsWith(Constraint<ITable<object>, ITable<object>> other)
		{
			DestructiveSuggestion destructiveSuggestion = other as DestructiveSuggestion;
			return destructiveSuggestion != null && destructiveSuggestion.IsAllowed != this.IsAllowed;
		}

		// Token: 0x0600E5EA RID: 58858 RVA: 0x0030B57A File Offset: 0x0030977A
		public override bool Equals(Constraint<ITable<object>, ITable<object>> other)
		{
			return other == this;
		}

		// Token: 0x0600E5EB RID: 58859 RVA: 0x0030B583 File Offset: 0x00309783
		public override int GetHashCode()
		{
			return 9302013;
		}

		// Token: 0x0600E5EC RID: 58860 RVA: 0x0030B58C File Offset: 0x0030978C
		public override bool Valid(Program<ITable<object>, ITable<object>> program)
		{
			if (this.IsAllowed)
			{
				return true;
			}
			Program program2 = program as Program;
			return program2 == null || program2.IsNonDestructiveTransformation || this.IsAllowed;
		}
	}
}
