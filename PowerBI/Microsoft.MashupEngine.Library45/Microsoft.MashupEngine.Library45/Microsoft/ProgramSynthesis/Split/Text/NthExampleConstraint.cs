using System;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F3 RID: 4851
	public class NthExampleConstraint : ConstraintOnInput<StringRegion, SplitCell[]>
	{
		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06009234 RID: 37428 RVA: 0x001EC2BD File Offset: 0x001EA4BD
		public string ExampleValue { get; }

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06009235 RID: 37429 RVA: 0x001EC2C5 File Offset: 0x001EA4C5
		public int SplitIndex { get; }

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06009236 RID: 37430 RVA: 0x001EC2CD File Offset: 0x001EA4CD
		public string InputString
		{
			get
			{
				return base.Input.Value;
			}
		}

		// Token: 0x06009237 RID: 37431 RVA: 0x001EC2DA File Offset: 0x001EA4DA
		public NthExampleConstraint(string inputString, int splitIndex, string exampleValue)
			: base(SplitSession.CreateStringRegion(inputString), false)
		{
			this.SplitIndex = splitIndex;
			this.ExampleValue = exampleValue;
			if (inputString.IndexOf(exampleValue, StringComparison.Ordinal) < 0)
			{
				throw new Exception("The example value is not a substring of the input");
			}
		}

		// Token: 0x06009238 RID: 37432 RVA: 0x001EC310 File Offset: 0x001EA510
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			string text = this.ExampleValue ?? string.Empty;
			SplitCell splitCell = program.Run(base.Input).ElementAtOrDefault(this.SplitIndex);
			string text2;
			if (splitCell == null)
			{
				text2 = null;
			}
			else
			{
				StringRegion cellValue = splitCell.CellValue;
				text2 = ((cellValue != null) ? cellValue.Value : null);
			}
			return text == (text2 ?? string.Empty);
		}

		// Token: 0x06009239 RID: 37433 RVA: 0x001EC36C File Offset: 0x001EA56C
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			NthExampleConstraint nthExampleConstraint = other as NthExampleConstraint;
			return nthExampleConstraint != null && nthExampleConstraint.InputString == this.InputString && nthExampleConstraint.SplitIndex == this.SplitIndex && nthExampleConstraint.ExampleValue != this.ExampleValue;
		}

		// Token: 0x0600923A RID: 37434 RVA: 0x001EC3C0 File Offset: 0x001EA5C0
		public bool Equals(NthExampleConstraint other)
		{
			return other != null && (this == other || (this.InputString == other.InputString && this.SplitIndex == other.SplitIndex && this.ExampleValue == other.ExampleValue));
		}

		// Token: 0x0600923B RID: 37435 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600923C RID: 37436 RVA: 0x001EC40C File Offset: 0x001EA60C
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((NthExampleConstraint)obj)));
		}

		// Token: 0x0600923D RID: 37437 RVA: 0x001EC43C File Offset: 0x001EA63C
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int num = 17;
			num = num * 23 + this.InputString.GetHashCode();
			num = num * 23 + this.SplitIndex.GetHashCode();
			num = num * 23 + this.ExampleValue.GetHashCode();
			this._hashCode = new int?(num);
			return num;
		}

		// Token: 0x0600923E RID: 37438 RVA: 0x001EC4A8 File Offset: 0x001EA6A8
		public override string ToString()
		{
			return string.Format("{0}({1} is column {2} of {3})", new object[]
			{
				"NthExampleConstraint",
				this.ExampleValue.ToLiteral(null),
				this.SplitIndex,
				this.InputString.ToLiteral(null)
			});
		}

		// Token: 0x04003BFC RID: 15356
		private int? _hashCode;
	}
}
