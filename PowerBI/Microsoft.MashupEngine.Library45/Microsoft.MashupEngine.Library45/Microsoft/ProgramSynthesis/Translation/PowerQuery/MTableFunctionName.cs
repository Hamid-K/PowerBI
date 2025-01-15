using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x02000328 RID: 808
	public readonly struct MTableFunctionName
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x00034A38 File Offset: 0x00032C38
		public MTableFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00034A41 File Offset: 0x00032C41
		public string Name { get; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00034A49 File Offset: 0x00032C49
		public string QualifiedName
		{
			get
			{
				return "Table." + this.Name;
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00034A5B File Offset: 0x00032C5B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00034A63 File Offset: 0x00032C63
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00034A70 File Offset: 0x00032C70
		public override bool Equals(object obj)
		{
			return obj is MTableFunctionName && ((MTableFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008A3 RID: 2211
		public static readonly MTableFunctionName AddColumn = new MTableFunctionName("AddColumn");

		// Token: 0x040008A4 RID: 2212
		public static readonly MTableFunctionName AddIndexColumn = new MTableFunctionName("AddIndexColumn");

		// Token: 0x040008A5 RID: 2213
		public static readonly MTableFunctionName CombineColumns = new MTableFunctionName("CombineColumns");

		// Token: 0x040008A6 RID: 2214
		public static readonly MTableFunctionName Distinct = new MTableFunctionName("Distinct");

		// Token: 0x040008A7 RID: 2215
		public static readonly MTableFunctionName ExpandListColumn = new MTableFunctionName("ExpandListColumn");

		// Token: 0x040008A8 RID: 2216
		public static readonly MTableFunctionName ExpandRecordColumn = new MTableFunctionName("ExpandRecordColumn");

		// Token: 0x040008A9 RID: 2217
		public static readonly MTableFunctionName FillDown = new MTableFunctionName("FillDown");

		// Token: 0x040008AA RID: 2218
		public static readonly MTableFunctionName Group = new MTableFunctionName("Group");

		// Token: 0x040008AB RID: 2219
		public static readonly MTableFunctionName FromList = new MTableFunctionName("FromList");

		// Token: 0x040008AC RID: 2220
		public static readonly MTableFunctionName FromRecords = new MTableFunctionName("FromRecords");

		// Token: 0x040008AD RID: 2221
		public static readonly MTableFunctionName FromRows = new MTableFunctionName("FromRows");

		// Token: 0x040008AE RID: 2222
		public static readonly MTableFunctionName Partition = new MTableFunctionName("Partition");

		// Token: 0x040008AF RID: 2223
		public static readonly MTableFunctionName PromoteHeaders = new MTableFunctionName("PromoteHeaders");

		// Token: 0x040008B0 RID: 2224
		public static readonly MTableFunctionName RemoveColumns = new MTableFunctionName("RemoveColumns");

		// Token: 0x040008B1 RID: 2225
		public static readonly MTableFunctionName RemoveLastN = new MTableFunctionName("RemoveLastN");

		// Token: 0x040008B2 RID: 2226
		public static readonly MTableFunctionName RenameColumns = new MTableFunctionName("RenameColumns");

		// Token: 0x040008B3 RID: 2227
		public static readonly MTableFunctionName ReplaceValue = new MTableFunctionName("ReplaceValue");

		// Token: 0x040008B4 RID: 2228
		public static readonly MTableFunctionName ReverseRows = new MTableFunctionName("ReverseRows");

		// Token: 0x040008B5 RID: 2229
		public static readonly MTableFunctionName SelectColumns = new MTableFunctionName("SelectColumns");

		// Token: 0x040008B6 RID: 2230
		public static readonly MTableFunctionName SelectRows = new MTableFunctionName("SelectRows");

		// Token: 0x040008B7 RID: 2231
		public static readonly MTableFunctionName Skip = new MTableFunctionName("Skip");

		// Token: 0x040008B8 RID: 2232
		public static readonly MTableFunctionName SplitColumn = new MTableFunctionName("SplitColumn");

		// Token: 0x040008B9 RID: 2233
		public static readonly MTableFunctionName TransformColumns = new MTableFunctionName("TransformColumns");

		// Token: 0x040008BA RID: 2234
		public static readonly MTableFunctionName Transpose = new MTableFunctionName("Transpose");
	}
}
