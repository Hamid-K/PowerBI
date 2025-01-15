using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020002C2 RID: 706
	internal static class UriFunctionsHelper
	{
		// Token: 0x06001854 RID: 6228 RVA: 0x00053150 File Offset: 0x00051350
		public static string BuildFunctionSignatureListDescription(string name, IEnumerable<FunctionSignatureWithReturnType> signatures)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (FunctionSignatureWithReturnType functionSignatureWithReturnType in signatures)
			{
				stringBuilder.Append(text);
				text = "; ";
				string text2 = "";
				stringBuilder.Append(name);
				stringBuilder.Append('(');
				foreach (IEdmTypeReference edmTypeReference in functionSignatureWithReturnType.ArgumentTypes)
				{
					stringBuilder.Append(text2);
					text2 = ", ";
					if (edmTypeReference.IsODataPrimitiveTypeKind() && edmTypeReference.IsNullable)
					{
						stringBuilder.Append(edmTypeReference.FullName());
						stringBuilder.Append(" Nullable=true");
					}
					else
					{
						stringBuilder.Append(edmTypeReference.FullName());
					}
				}
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}
	}
}
