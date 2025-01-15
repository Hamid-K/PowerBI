using System;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatching;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B3F RID: 2879
	internal class FuzzyJoinRequest
	{
		// Token: 0x06004FF5 RID: 20469 RVA: 0x0010BCB8 File Offset: 0x00109EB8
		protected FuzzyJoinRequest(DataTable referenceDataTable, DataTable transformationDataTable, FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters, FuzzyLookupEntry.JoinType fuzzyJoinType)
		{
			this.referenceDataTable = referenceDataTable;
			this.transformationDataTable = transformationDataTable;
			this.fuzzyJoinParameters = fuzzyJoinParameters;
			this.fuzzyJoinType = fuzzyJoinType;
		}

		// Token: 0x04002AE6 RID: 10982
		protected readonly DataTable referenceDataTable;

		// Token: 0x04002AE7 RID: 10983
		protected readonly DataTable transformationDataTable;

		// Token: 0x04002AE8 RID: 10984
		protected readonly FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters;

		// Token: 0x04002AE9 RID: 10985
		protected readonly FuzzyLookupEntry.JoinType fuzzyJoinType;
	}
}
