using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B21 RID: 2849
	public class PythonColumnInfo : IPythonColumnInfo<PythonColumnInfo>, IPythonColumnInfo, IColumnInfo, IColumnInfo<PythonColumnInfo>, IEquatable<PythonColumnInfo>
	{
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x000DEC82 File Offset: 0x000DCE82
		public string ColumnName { get; }

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004722 RID: 18210 RVA: 0x000DEC8A File Offset: 0x000DCE8A
		public bool ColumnNameIsInt { get; }

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x000DEC92 File Offset: 0x000DCE92
		public bool FixPandasNaNBug { get; }

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004724 RID: 18212 RVA: 0x000DEC9A File Offset: 0x000DCE9A
		public bool UseColumnForLearning { get; }

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x000DECA2 File Offset: 0x000DCEA2
		public IEnumerable<string> Data { get; }

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x000DECAA File Offset: 0x000DCEAA
		public long? TrueLength { get; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x000DECB2 File Offset: 0x000DCEB2
		public string ColumnNameLiteral
		{
			get
			{
				if (!this.ColumnNameIsInt)
				{
					return this.ColumnName.ToPythonLiteral();
				}
				return this.ColumnName;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x000DECCE File Offset: 0x000DCECE
		public string ColumnNameForIdentifier
		{
			get
			{
				return PythonNameUtils.NearestValidIdentifier(this.ColumnName);
			}
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x000DECDB File Offset: 0x000DCEDB
		public PythonColumnInfo(string columnName, IEnumerable<string> data, long? trueLength = null, bool columnNameIsInt = false, bool fixPandasNaNBug = false, bool useColumnForLearning = true)
		{
			this.ColumnName = columnName;
			this.Data = data;
			this.TrueLength = trueLength;
			this.ColumnNameIsInt = columnNameIsInt;
			this.FixPandasNaNBug = fixPandasNaNBug;
			this.UseColumnForLearning = useColumnForLearning;
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void ResetDataStream()
		{
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x000DED10 File Offset: 0x000DCF10
		public bool Equals(PythonColumnInfo other)
		{
			return other != null && (this == other || string.Equals(this.ColumnName, other.ColumnName));
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x000DED2E File Offset: 0x000DCF2E
		public override int GetHashCode()
		{
			return this.ColumnName.GetHashCode();
		}
	}
}
