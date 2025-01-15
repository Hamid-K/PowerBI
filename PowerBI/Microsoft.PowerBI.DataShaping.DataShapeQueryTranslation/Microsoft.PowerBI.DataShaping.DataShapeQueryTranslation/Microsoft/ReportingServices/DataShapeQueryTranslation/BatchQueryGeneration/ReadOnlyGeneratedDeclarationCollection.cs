using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000155 RID: 341
	internal sealed class ReadOnlyGeneratedDeclarationCollection : GeneratedDeclarationCollection
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x000341AA File Offset: 0x000323AA
		internal ReadOnlyGeneratedDeclarationCollection(Dictionary<string, GeneratedTableDeclaration> declarations, Dictionary<string, List<GeneratedTableDeclaration>> multiTableDeclarations, Dictionary<string, GeneratedScalarDeclaration> scalarDeclarations, Dictionary<string, GeneratedEntityDeclaration> entityDeclarations, Dictionary<string, List<GeneratedTableDeclaration>> reconciledDeclarationNames)
			: base(declarations, multiTableDeclarations, scalarDeclarations, entityDeclarations, reconciledDeclarationNames)
		{
		}
	}
}
