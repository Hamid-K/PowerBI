using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000664 RID: 1636
	internal sealed class ParserOptions
	{
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x00118855 File Offset: 0x00116A55
		internal StringComparer NameComparer
		{
			get
			{
				if (!this.NameComparisonCaseInsensitive)
				{
					return StringComparer.Ordinal;
				}
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06004E13 RID: 19987 RVA: 0x0011886A File Offset: 0x00116A6A
		internal bool NameComparisonCaseInsensitive
		{
			get
			{
				return this.ParserCompilationMode != ParserOptions.CompilationMode.RestrictedViewGenerationMode;
			}
		}

		// Token: 0x04001C57 RID: 7255
		internal ParserOptions.CompilationMode ParserCompilationMode;

		// Token: 0x02000C73 RID: 3187
		internal enum CompilationMode
		{
			// Token: 0x04003129 RID: 12585
			NormalMode,
			// Token: 0x0400312A RID: 12586
			RestrictedViewGenerationMode,
			// Token: 0x0400312B RID: 12587
			UserViewGenerationMode
		}
	}
}
