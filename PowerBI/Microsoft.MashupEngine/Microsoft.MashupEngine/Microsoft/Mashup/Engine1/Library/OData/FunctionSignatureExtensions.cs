using System;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000723 RID: 1827
	internal static class FunctionSignatureExtensions
	{
		// Token: 0x06003665 RID: 13925 RVA: 0x000AD63C File Offset: 0x000AB83C
		public static string CreateSignature(this FunctionTypeValue type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("function");
			stringBuilder.Append(" (");
			for (int i = 0; i < type.ParameterCount; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				int length = stringBuilder.Length;
				if (i >= type.Min)
				{
					stringBuilder.Append("optional ");
				}
				string text = type.ParameterName(i);
				stringBuilder.Append(text);
				FunctionSignatureExtensions.AppendType(stringBuilder, type.ParameterType(i));
			}
			stringBuilder.Append(')');
			FunctionSignatureExtensions.AppendType(stringBuilder, type.ReturnType);
			return stringBuilder.ToString();
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000AD6DC File Offset: 0x000AB8DC
		private static void AppendType(StringBuilder builder, TypeValue type)
		{
			if (!type.Equals(TypeHandle.Any) || !type.IsNullable)
			{
				builder.Append(" as ");
				builder.Append(type.ToSource());
			}
		}
	}
}
