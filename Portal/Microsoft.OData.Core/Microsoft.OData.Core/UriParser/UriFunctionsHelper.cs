using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001D8 RID: 472
	internal static class UriFunctionsHelper
	{
		// Token: 0x06001546 RID: 5446 RVA: 0x0003DB58 File Offset: 0x0003BD58
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
