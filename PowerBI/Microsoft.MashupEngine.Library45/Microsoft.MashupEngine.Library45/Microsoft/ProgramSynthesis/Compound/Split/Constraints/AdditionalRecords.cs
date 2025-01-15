using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009E6 RID: 2534
	public class AdditionalRecords : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D35 RID: 15669 RVA: 0x000BFF30 File Offset: 0x000BE130
		[JsonConstructor]
		public AdditionalRecords(params StringRegion[] records)
		{
			if (records == null)
			{
				throw new ArgumentNullException("records");
			}
			this.Records = records;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000BFF4E File Offset: 0x000BE14E
		public AdditionalRecords(params string[] records)
		{
			StringRegion[] array;
			if (records == null)
			{
				array = null;
			}
			else
			{
				Func<string, StringRegion> func;
				if ((func = AdditionalRecords.<>O.<0>__CreateStringRegion) == null)
				{
					func = (AdditionalRecords.<>O.<0>__CreateStringRegion = new Func<string, StringRegion>(Session.CreateStringRegion));
				}
				array = records.Select(func).ToArray<StringRegion>();
			}
			this..ctor(array);
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000BFF82 File Offset: 0x000BE182
		public IReadOnlyCollection<StringRegion> Records { get; }

		// Token: 0x06003D38 RID: 15672 RVA: 0x000BFF8A File Offset: 0x000BE18A
		public void SetOptions(Options options)
		{
			options.AdditionalRecords.AddRange(this.Records);
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000BFFA0 File Offset: 0x000BE1A0
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
			AdditionalRecords additionalRecords = other as AdditionalRecords;
			return additionalRecords != null && this.Records.SequenceEqual(additionalRecords.Records);
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000BFFDC File Offset: 0x000BE1DC
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			Program splitProgram = program as Program;
			return splitProgram != null && (this.Records.Count == 0 || this.Records.All((StringRegion r) => splitProgram.RunTextProg(r).All((SplitCell cell) => cell != null)));
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000C002B File Offset: 0x000BE22B
		public override int GetHashCode()
		{
			return this.Records.OrderIndependentHashCode((StringRegion rec) => rec.GetHashCode());
		}

		// Token: 0x020009E7 RID: 2535
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001CB3 RID: 7347
			public static Func<string, StringRegion> <0>__CreateStringRegion;
		}
	}
}
