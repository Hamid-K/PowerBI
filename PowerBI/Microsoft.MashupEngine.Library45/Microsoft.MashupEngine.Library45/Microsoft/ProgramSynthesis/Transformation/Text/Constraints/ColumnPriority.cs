using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DDD RID: 7645
	public class ColumnPriority : Constraint<IRow, object>, IEquatable<ColumnPriority>
	{
		// Token: 0x0601004B RID: 65611 RVA: 0x00370BEC File Offset: 0x0036EDEC
		public ColumnPriority(IEnumerable<IEnumerable<string>> priority)
		{
			IImmutableList<IImmutableSet<string>> immutableList;
			if ((immutableList = priority as IImmutableList<IImmutableSet<string>>) == null)
			{
				immutableList = priority.Select((IEnumerable<string> el) => (el as IImmutableSet<string>) ?? el.ToImmutableHashSet<string>()).ToImmutableList<IImmutableSet<string>>();
			}
			this._priority = immutableList;
		}

		// Token: 0x17002A8A RID: 10890
		// (get) Token: 0x0601004C RID: 65612 RVA: 0x00370C39 File Offset: 0x0036EE39
		public IEnumerable<IEnumerable<string>> Priority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x0601004D RID: 65613 RVA: 0x00370C44 File Offset: 0x0036EE44
		public bool Equals(ColumnPriority other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this._priority.Count == other._priority.Count)
			{
				return this._priority.Zip(other._priority, (IImmutableSet<string> a, IImmutableSet<string> b) => a.SetEquals(b)).All((bool b) => b);
			}
			return false;
		}

		// Token: 0x0601004E RID: 65614 RVA: 0x00370CCA File Offset: 0x0036EECA
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return other is ColumnPriority && !other.Equals(this);
		}

		// Token: 0x0601004F RID: 65615 RVA: 0x00370CE0 File Offset: 0x0036EEE0
		public override bool Valid(Program<IRow, object> program)
		{
			Program program2 = program as Program;
			return program2 != null && program2.ColumnsUsed.ConvertToHashSet<string>().IsSubsetOf(this._priority.Union<string>());
		}

		// Token: 0x06010050 RID: 65616 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<IRow, object> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06010051 RID: 65617 RVA: 0x00370D08 File Offset: 0x0036EF08
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ColumnPriority)other)));
		}

		// Token: 0x06010052 RID: 65618 RVA: 0x00370D36 File Offset: 0x0036EF36
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				this._hashCode = new int?(this._priority.OrderIndependentHashCode(ValueEquality.Comparer));
			}
			return this._hashCode.Value;
		}

		// Token: 0x0400606E RID: 24686
		private readonly IImmutableList<IImmutableSet<string>> _priority;

		// Token: 0x0400606F RID: 24687
		private int? _hashCode;
	}
}
