using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Constraints
{
	// Token: 0x02001B5C RID: 7004
	public class FocusedColumnNames : Constraint<ITable<object>, ITable<object>>, IOptionConstraint<Options>
	{
		// Token: 0x0600E5EE RID: 58862 RVA: 0x0030B5D8 File Offset: 0x003097D8
		public FocusedColumnNames(IEnumerable<string> columnNames)
		{
			this._columnNames = ((columnNames != null) ? columnNames.ConvertToHashSet<string>() : null);
		}

		// Token: 0x1700264C RID: 9804
		// (get) Token: 0x0600E5EF RID: 58863 RVA: 0x0030B5F2 File Offset: 0x003097F2
		public IEnumerable<string> ColumnNames
		{
			get
			{
				return this._columnNames;
			}
		}

		// Token: 0x0600E5F0 RID: 58864 RVA: 0x0030B5FC File Offset: 0x003097FC
		public override bool ConflictsWith(Constraint<ITable<object>, ITable<object>> other)
		{
			FocusedColumnNames focusedColumnNames = other as FocusedColumnNames;
			return focusedColumnNames != null && !this._columnNames.SetEquals(focusedColumnNames.ColumnNames);
		}

		// Token: 0x0600E5F1 RID: 58865 RVA: 0x0030B62C File Offset: 0x0030982C
		public override bool Equals(Constraint<ITable<object>, ITable<object>> other)
		{
			FocusedColumnNames focusedColumnNames = other as FocusedColumnNames;
			return focusedColumnNames != null && this._columnNames.SetEquals(focusedColumnNames.ColumnNames);
		}

		// Token: 0x0600E5F2 RID: 58866 RVA: 0x0030B656 File Offset: 0x00309856
		public override int GetHashCode()
		{
			return this.ColumnNames.OrderIndependentHashCode<string>();
		}

		// Token: 0x0600E5F3 RID: 58867 RVA: 0x0030B663 File Offset: 0x00309863
		public void SetOptions(Options options)
		{
			options.FocusedColumnNames = this._columnNames;
		}

		// Token: 0x0600E5F4 RID: 58868 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<ITable<object>, ITable<object>> program)
		{
			return true;
		}

		// Token: 0x0400574F RID: 22351
		private readonly HashSet<string> _columnNames;
	}
}
