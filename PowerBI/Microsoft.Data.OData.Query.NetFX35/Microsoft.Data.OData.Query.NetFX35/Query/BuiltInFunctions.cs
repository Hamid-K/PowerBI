using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Experimental.OData.Metadata;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200000D RID: 13
	internal static class BuiltInFunctions
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000297C File Offset: 0x00000B7C
		internal static bool TryGetBuiltInFunction(string name, out BuiltInFunctionSignature[] signatures)
		{
			BuiltInFunctions.InitializeBuiltInFunctions();
			return BuiltInFunctions.builtInFunctions.TryGetValue(name, ref signatures);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002990 File Offset: 0x00000B90
		internal static string BuildFunctionSignatureListDescription(string name, IEnumerable<BuiltInFunctionSignature> signatures)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (BuiltInFunctionSignature builtInFunctionSignature in signatures)
			{
				stringBuilder.Append(text);
				text = "; ";
				string text2 = "";
				stringBuilder.Append(name);
				stringBuilder.Append('(');
				foreach (IEdmTypeReference edmTypeReference in builtInFunctionSignature.ArgumentTypes)
				{
					stringBuilder.Append(text2);
					text2 = ", ";
					if (edmTypeReference.IsODataPrimitiveTypeKind() && edmTypeReference.IsNullable)
					{
						stringBuilder.Append(edmTypeReference.ODataFullName());
						stringBuilder.Append(" Nullable=true");
					}
					else
					{
						stringBuilder.Append(edmTypeReference.ODataFullName());
					}
				}
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A88 File Offset: 0x00000C88
		private static void InitializeBuiltInFunctions()
		{
			if (BuiltInFunctions.builtInFunctions == null)
			{
				BuiltInFunctions.builtInFunctions = new Dictionary<string, BuiltInFunctionSignature[]>(StringComparer.Ordinal);
				BuiltInFunctions.CreateStringFunctions();
				BuiltInFunctions.CreateDateTimeFunctions();
				BuiltInFunctions.CreateMathFunctions();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002AB0 File Offset: 0x00000CB0
		private static void CreateStringFunctions()
		{
			BuiltInFunctionSignature builtInFunctionSignature = BuiltInFunctionSignature.CreateFromInstanceMethodCall("EndsWith", EdmCoreModel.Instance.GetBoolean(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			BuiltInFunctions.builtInFunctions.Add("endswith", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromInstanceMethodCall("IndexOf", EdmCoreModel.Instance.GetInt32(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			BuiltInFunctions.builtInFunctions.Add("indexof", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromInstanceMethodCall("Replace", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			BuiltInFunctions.builtInFunctions.Add("replace", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromInstanceMethodCall("StartsWith", EdmCoreModel.Instance.GetBoolean(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			BuiltInFunctions.builtInFunctions.Add("startswith", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromInstanceMethodCall("ToLower", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			BuiltInFunctions.builtInFunctions.Add("tolower", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromInstanceMethodCall("ToUpper", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			BuiltInFunctions.builtInFunctions.Add("toupper", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromInstanceMethodCall("Trim", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			BuiltInFunctions.builtInFunctions.Add("trim", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			BuiltInFunctionSignature[] array = new BuiltInFunctionSignature[]
			{
				BuiltInFunctionSignature.CreateFromInstanceMethodCall("Substring", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				BuiltInFunctionSignature.CreateFromInstanceMethodCall("Substring", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true)
				}),
				BuiltInFunctionSignature.CreateFromInstanceMethodCall("Substring", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				BuiltInFunctionSignature.CreateFromInstanceMethodCall("Substring", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				BuiltInFunctionSignature.CreateFromInstanceMethodCall("Substring", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false),
					EdmCoreModel.Instance.GetInt32(true)
				}),
				BuiltInFunctionSignature.CreateFromInstanceMethodCall("Substring", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true),
					EdmCoreModel.Instance.GetInt32(true)
				})
			};
			BuiltInFunctions.builtInFunctions.Add("substring", array);
			builtInFunctionSignature = new BuiltInFunctionSignature(new Func<Expression[], Expression>(BuiltInFunctions.BuildSubstringOfExpression), EdmCoreModel.Instance.GetBoolean(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			BuiltInFunctions.builtInFunctions.Add("substringof", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromStaticMethodCall(typeof(string), "Concat", EdmCoreModel.Instance.GetString(true), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			BuiltInFunctions.builtInFunctions.Add("concat", new BuiltInFunctionSignature[] { builtInFunctionSignature });
			builtInFunctionSignature = BuiltInFunctionSignature.CreateFromPropertyAccess("Length", EdmCoreModel.Instance.GetInt32(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			BuiltInFunctions.builtInFunctions.Add("length", new BuiltInFunctionSignature[] { builtInFunctionSignature });
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000305C File Offset: 0x0000125C
		private static Expression BuildSubstringOfExpression(Expression[] argumentExpressions)
		{
			return Expression.Call(argumentExpressions[1], BuiltInFunctions.StringContainsMethodInfo, new Expression[] { argumentExpressions[0] });
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003084 File Offset: 0x00001284
		private static void CreateDateTimeFunctions()
		{
			BuiltInFunctions.builtInFunctions.Add("year", BuiltInFunctions.CreateDateTimeFunctionSignatureArray("Year"));
			BuiltInFunctions.builtInFunctions.Add("month", BuiltInFunctions.CreateDateTimeFunctionSignatureArray("Month"));
			BuiltInFunctions.builtInFunctions.Add("day", BuiltInFunctions.CreateDateTimeFunctionSignatureArray("Day"));
			BuiltInFunctions.builtInFunctions.Add("hour", BuiltInFunctions.CreateDateTimeFunctionSignatureArray("Hour"));
			BuiltInFunctions.builtInFunctions.Add("minute", BuiltInFunctions.CreateDateTimeFunctionSignatureArray("Minute"));
			BuiltInFunctions.builtInFunctions.Add("second", BuiltInFunctions.CreateDateTimeFunctionSignatureArray("Second"));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003128 File Offset: 0x00001328
		private static BuiltInFunctionSignature[] CreateDateTimeFunctionSignatureArray(string propertyName)
		{
			BuiltInFunctionSignature builtInFunctionSignature = BuiltInFunctionSignature.CreateFromPropertyAccess(propertyName, EdmCoreModel.Instance.GetInt32(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, false) });
			BuiltInFunctionSignature builtInFunctionSignature2 = BuiltInFunctionSignature.CreateFromPropertyAccess(propertyName, EdmCoreModel.Instance.GetInt32(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, true) });
			return new BuiltInFunctionSignature[] { builtInFunctionSignature, builtInFunctionSignature2 };
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000031A8 File Offset: 0x000013A8
		private static void CreateMathFunctions()
		{
			BuiltInFunctions.builtInFunctions.Add("round", BuiltInFunctions.CreateMathFunctionSignatureArray("Round"));
			BuiltInFunctions.builtInFunctions.Add("floor", BuiltInFunctions.CreateMathFunctionSignatureArray("Floor"));
			BuiltInFunctions.builtInFunctions.Add("ceiling", BuiltInFunctions.CreateMathFunctionSignatureArray("Ceiling"));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003200 File Offset: 0x00001400
		private static BuiltInFunctionSignature[] CreateMathFunctionSignatureArray(string methodName)
		{
			BuiltInFunctionSignature builtInFunctionSignature = BuiltInFunctionSignature.CreateFromStaticMethodCall(typeof(Math), methodName, EdmCoreModel.Instance.GetDouble(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetDouble(false) });
			BuiltInFunctionSignature builtInFunctionSignature2 = BuiltInFunctionSignature.CreateFromStaticMethodCall(typeof(Math), methodName, EdmCoreModel.Instance.GetDouble(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetDouble(true) });
			BuiltInFunctionSignature builtInFunctionSignature3 = BuiltInFunctionSignature.CreateFromStaticMethodCall(typeof(Math), methodName, EdmCoreModel.Instance.GetDecimal(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetDecimal(false) });
			BuiltInFunctionSignature builtInFunctionSignature4 = BuiltInFunctionSignature.CreateFromStaticMethodCall(typeof(Math), methodName, EdmCoreModel.Instance.GetDecimal(false), EdmCoreModel.Instance, new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) });
			return new BuiltInFunctionSignature[] { builtInFunctionSignature, builtInFunctionSignature3, builtInFunctionSignature2, builtInFunctionSignature4 };
		}

		// Token: 0x0400003B RID: 59
		private static readonly MethodInfo StringContainsMethodInfo = typeof(string).GetMethod("Contains", 20, null, new Type[] { typeof(string) }, null);

		// Token: 0x0400003C RID: 60
		private static Dictionary<string, BuiltInFunctionSignature[]> builtInFunctions;
	}
}
