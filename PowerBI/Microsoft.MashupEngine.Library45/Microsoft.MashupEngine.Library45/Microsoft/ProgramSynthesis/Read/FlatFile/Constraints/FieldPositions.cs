using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012DF RID: 4831
	public class FieldPositions : FixedWidthConstraint
	{
		// Token: 0x060091B8 RID: 37304 RVA: 0x001EB279 File Offset: 0x001E9479
		public FieldPositions(IEnumerable<Record<int, int?>> fieldPositions)
		{
			if (fieldPositions == null)
			{
				throw new ArgumentNullException("fieldPositions");
			}
			this.FieldPositionList = fieldPositions.ToList<Record<int, int?>>();
			if (this.FieldPositionList.Count == 0)
			{
				throw new ArgumentException("fieldPositions");
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x060091B9 RID: 37305 RVA: 0x001EB2B3 File Offset: 0x001E94B3
		public IReadOnlyList<Record<int, int?>> FieldPositionList { get; }

		// Token: 0x060091BA RID: 37306 RVA: 0x001EB2BB File Offset: 0x001E94BB
		public override void SetOptions(Options options)
		{
			base.SetOptions(options);
			options.FieldPositions = this.FieldPositionList;
		}

		// Token: 0x060091BB RID: 37307 RVA: 0x001EB2D0 File Offset: 0x001E94D0
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			if (!base.ConflictsWith(other))
			{
				FieldPositions fieldPositions = other as FieldPositions;
				return fieldPositions != null && !FieldPositions.EqualLists(this.FieldPositionList, fieldPositions.FieldPositionList);
			}
			return true;
		}

		// Token: 0x060091BC RID: 37308 RVA: 0x001EB308 File Offset: 0x001E9508
		public override bool Valid(Program<string, ITable<string>> program)
		{
			FwProgram fwProgram = program as FwProgram;
			return fwProgram != null && FieldPositions.EqualLists(this.FieldPositionList, fwProgram.FieldPositions);
		}

		// Token: 0x060091BD RID: 37309 RVA: 0x001EB334 File Offset: 0x001E9534
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			FieldPositions fieldPositions = other as FieldPositions;
			return fieldPositions != null && FieldPositions.EqualLists(this.FieldPositionList, fieldPositions.FieldPositionList);
		}

		// Token: 0x060091BE RID: 37310 RVA: 0x001EB35E File Offset: 0x001E955E
		public override int GetHashCode()
		{
			return this.FieldPositionList.OrderDependentHashCode<Record<int, int?>>();
		}

		// Token: 0x060091BF RID: 37311 RVA: 0x001EB36C File Offset: 0x001E956C
		private static bool EqualLists(IReadOnlyList<Record<int, int?>> l1, IReadOnlyList<Record<int, int?>> l2)
		{
			if (l1 != null && l2 != null && l1.Count == l2.Count)
			{
				return l1.Zip(l2, delegate(Record<int, int?> x, Record<int, int?> y)
				{
					if (x.Item1 == y.Item1)
					{
						int? item = x.Item2;
						int? item2 = y.Item2;
						return (item.GetValueOrDefault() == item2.GetValueOrDefault()) & (item != null == (item2 != null));
					}
					return false;
				}).All((bool x) => x);
			}
			return false;
		}
	}
}
