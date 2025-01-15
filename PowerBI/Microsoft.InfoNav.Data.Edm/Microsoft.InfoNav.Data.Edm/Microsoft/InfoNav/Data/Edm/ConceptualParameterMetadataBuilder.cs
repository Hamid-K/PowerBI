using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200001F RID: 31
	internal static class ConceptualParameterMetadataBuilder
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00004B38 File Offset: 0x00002D38
		internal static ConceptualParameterMetadata BuildConceptualParameter(IReadOnlyList<ConceptualMParameter> mappedMParametersList, ParameterMetadata parameterMetadata)
		{
			bool flag = parameterMetadata == null || parameterMetadata.ParameterKind == ParameterKind.MParameters;
			bool flag2 = mappedMParametersList != null && mappedMParametersList.Count > 0;
			if (flag2 && flag)
			{
				bool flag3 = parameterMetadata != null && parameterMetadata.SupportsMultipleValues;
				string text = ((parameterMetadata != null) ? parameterMetadata.SelectAllValue : null);
				return new ConceptualParameterMetadata(mappedMParametersList, flag3, text);
			}
			if (flag2 && !flag)
			{
				throw Contract.ExceptNotSupported("Only one kind of parameter {WhatIf, MParameter, Field} is supported.");
			}
			if (parameterMetadata != null)
			{
				return new ConceptualParameterMetadata(parameterMetadata.ParameterKind);
			}
			return null;
		}

		// Token: 0x0400014C RID: 332
		internal const string ParameterMetadata = "ParameterMetadata";
	}
}
