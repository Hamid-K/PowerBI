using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.Compound.Split.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F7 RID: 2551
	public class IgnoreSplitRecords : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x000C06FB File Offset: 0x000BE8FB
		public IgnoreSplitRecords(params SequenceExample[] examples)
		{
			this.Examples = examples;
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000C070A File Offset: 0x000BE90A
		public IReadOnlyList<SequenceExample> Examples { get; }

		// Token: 0x06003D99 RID: 15769 RVA: 0x000C0712 File Offset: 0x000BE912
		public void SetOptions(Options options)
		{
			options.IgnoreSplitRecords = true;
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000C071C File Offset: 0x000BE91C
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			IgnoreSplitRecords ignoreSplitRecords = other as IgnoreSplitRecords;
			return ignoreSplitRecords != null && this.Examples.SequenceEqual(ignoreSplitRecords.Examples);
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x000C0758 File Offset: 0x000BE958
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			foreach (SequenceExample sequenceExample in this.Examples)
			{
				StringRegion stringRegion = Session.CreateStringRegion(string.Concat(sequenceExample.Input.Select((StringRegion sr) => sr.Value)));
				IReadOnlyList<IEnumerable<StringRegion>> readOnlyList = program.Run(stringRegion).Rows.ToList<IEnumerable<StringRegion>>();
				if (readOnlyList.Any((IEnumerable<StringRegion> row) => row.Count<StringRegion>() != 1))
				{
					return false;
				}
				IEnumerable<string> enumerable = readOnlyList.Select((IEnumerable<StringRegion> row) => row.Single<StringRegion>().Value);
				if (!(from sr in Semantics.MergeRecordLines(sequenceExample.Output)
					select sr.Value).SequenceEqual(enumerable.Take(sequenceExample.Output.Count<IEnumerable<StringRegion>>())))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x000C08A0 File Offset: 0x000BEAA0
		public override int GetHashCode()
		{
			return this.Examples.GetHashCode();
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x000C08AD File Offset: 0x000BEAAD
		public override string ToString()
		{
			return "IgnoreSplitRecords(examples: " + string.Join<SequenceExample>("; ", this.Examples) + ")";
		}
	}
}
