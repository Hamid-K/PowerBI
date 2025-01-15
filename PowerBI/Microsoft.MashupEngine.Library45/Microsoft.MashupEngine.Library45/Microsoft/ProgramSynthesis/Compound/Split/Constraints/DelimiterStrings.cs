using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009ED RID: 2541
	public class DelimiterStrings : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>, IEquatable<DelimiterStrings>
	{
		// Token: 0x06003D51 RID: 15697 RVA: 0x000C01DB File Offset: 0x000BE3DB
		[JsonConstructor]
		public DelimiterStrings(IEnumerable<string> delimiters)
		{
			this.Delimiters = new HashSet<string>(delimiters);
			this._textConstraint = new DelimiterStringsConstraint(this.Delimiters);
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000C0200 File Offset: 0x000BE400
		public DelimiterStrings(params string[] delimiterStrings)
			: this(delimiterStrings)
		{
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x000C0209 File Offset: 0x000BE409
		public HashSet<string> Delimiters { get; }

		// Token: 0x06003D54 RID: 15700 RVA: 0x000C0211 File Offset: 0x000BE411
		public bool Equals(DelimiterStrings other)
		{
			return other != null && (this == other || this.Delimiters.SetEquals(other.Delimiters));
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000C022F File Offset: 0x000BE42F
		public void SetOptions(Options options)
		{
			options.TextConstraints.Add(this._textConstraint);
			options.DelimiterStringsProvided = true;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000C024C File Offset: 0x000BE44C
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			Program program2 = program as Program;
			SplitProgram splitProgram = ((program2 != null) ? program2.SplitTextProgram : null);
			return splitProgram != null && this._textConstraint.Valid(splitProgram);
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000C0283 File Offset: 0x000BE483
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is FixedWidth || ((other is SimpleDelimiter || other is SimpleDelimiterOrFixedWidth) && this.Delimiters.Count > 1) || (other is DelimiterStrings && !this.Equals(other));
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000C02C1 File Offset: 0x000BE4C1
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((DelimiterConstraint)obj)));
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000C02EF File Offset: 0x000BE4EF
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			this._hashCode = new int?(this.Delimiters.OrderIndependentHashCode<string>());
			return this._hashCode.Value;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000C032C File Offset: 0x000BE52C
		public override string ToString()
		{
			return "DelimiterStrings({" + string.Join(", ", this.Delimiters.Select((string str) => str.ToLiteral(null))) + "})";
		}

		// Token: 0x04001CBA RID: 7354
		private readonly DelimiterStringsConstraint _textConstraint;

		// Token: 0x04001CBB RID: 7355
		private int? _hashCode;
	}
}
