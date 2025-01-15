using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text
{
	// Token: 0x02000EF4 RID: 3828
	public class Example : ConstraintOnInput<StringRegion, ITable<StringRegion>>
	{
		// Token: 0x06006822 RID: 26658 RVA: 0x001533F8 File Offset: 0x001515F8
		public Example(StringRegion input, ITable<ExampleCell> output)
			: base(input, false)
		{
			List<List<ExampleCell>> list = output.Select((IEnumerable<ExampleCell> row) => row.Select(delegate(ExampleCell cell)
			{
				string text = Example.NormalizeNewLines(cell.Value);
				return new ExampleCell((text != null) ? text.Trim() : null, cell.IsUserSpecified);
			}).ToList<ExampleCell>()).ToList<List<ExampleCell>>();
			ITable<ExampleCell> table = new Table<ExampleCell>(output.ColumnNames, list, null);
			this.Output = table;
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x0015344D File Offset: 0x0015164D
		public Example(string input, ITable<ExampleCell> output)
			: this(Semantics.CreateStringRegion(Example.NormalizeNewLines(input)), output)
		{
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06006824 RID: 26660 RVA: 0x00153461 File Offset: 0x00151661
		public ITable<ExampleCell> Output { get; }

		// Token: 0x06006825 RID: 26661 RVA: 0x0015346C File Offset: 0x0015166C
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			Example example = other as Example;
			return example != null && base.Input.Equals(example.Input) && this.Output.Equals(example.Output);
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x001534AC File Offset: 0x001516AC
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			Example example = other as Example;
			if (example == null)
			{
				return false;
			}
			if (other == this)
			{
				return false;
			}
			if (!object.Equals(base.Input, example.Input))
			{
				return false;
			}
			if (this.Output.ColumnNames.ZipWith(example.Output.ColumnNames).Any2((string colName, string otherColName) => colName != otherColName))
			{
				return true;
			}
			return this.Output.Rows.ZipWith(example.Output.Rows).Any2((IEnumerable<ExampleCell> row, IEnumerable<ExampleCell> otherRow) => row.ZipWith(otherRow).Any2((ExampleCell cell, ExampleCell otherCell) => cell.IsUserSpecified && otherCell.IsUserSpecified && !object.Equals(cell.Value, otherCell.Value)));
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x00153564 File Offset: 0x00151764
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			ITable<StringRegion> table = program.Run(base.Input);
			if (table == null || !this.Output.ColumnNames.SequenceEqual(table.ColumnNames.Take(this.Output.ColumnNames.Count<string>())))
			{
				return false;
			}
			int num = this.Output.Rows.Count<IEnumerable<ExampleCell>>();
			int num2 = table.Rows.Count<IEnumerable<StringRegion>>();
			for (int i = 0; i < Math.Min(num, num2); i++)
			{
				IEnumerable<ExampleCell> enumerable = this.Output.Rows.ElementAt(i);
				IEnumerable<StringRegion> enumerable2 = table.Rows.ElementAt(i);
				if (!enumerable.ZipWith(enumerable2).All2(new Func<ExampleCell, StringRegion, bool>(Example.<Valid>g__IsNullAndWhiteSpaceOrEquals|8_0)))
				{
					return false;
				}
			}
			if (num > num2)
			{
				if (this.Output.Rows.Skip(num2).Any((IEnumerable<ExampleCell> row) => row.Any((ExampleCell cell) => cell.IsUserSpecified)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x00153658 File Offset: 0x00151858
		public override int GetHashCode()
		{
			return base.Input.GetHashCode() + (11 ^ this.Output.GetHashCode());
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x00153674 File Offset: 0x00151874
		internal static string NormalizeNewLines(string s)
		{
			if (s != null)
			{
				return Example.NewLineRegex.Replace(s, "\n");
			}
			return null;
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x0015369E File Offset: 0x0015189E
		[CompilerGenerated]
		internal static bool <Valid>g__IsNullAndWhiteSpaceOrEquals|8_0(ExampleCell exampleCell, StringRegion outputCell)
		{
			return !exampleCell.IsUserSpecified || string.IsNullOrWhiteSpace(exampleCell.Value) || exampleCell.Value.Equals((outputCell != null) ? outputCell.Value : null);
		}

		// Token: 0x04002E25 RID: 11813
		private static readonly Regex NewLineRegex = new Regex("\r\n|\n|\r", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
	}
}
