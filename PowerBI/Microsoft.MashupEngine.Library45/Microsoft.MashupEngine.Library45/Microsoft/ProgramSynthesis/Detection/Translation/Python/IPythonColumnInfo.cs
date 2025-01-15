using System;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B19 RID: 2841
	public interface IPythonColumnInfo : IColumnInfo
	{
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x060046FA RID: 18170
		bool ColumnNameIsInt { get; }

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x060046FB RID: 18171
		bool FixPandasNaNBug { get; }

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x060046FC RID: 18172
		string ColumnNameLiteral { get; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060046FD RID: 18173
		string ColumnNameForIdentifier { get; }
	}
}
