using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000138 RID: 312
	internal static class BuiltInUriFunctions
	{
		// Token: 0x06001059 RID: 4185 RVA: 0x0002BAEF File Offset: 0x00029CEF
		internal static bool TryGetBuiltInFunction(string name, out FunctionSignatureWithReturnType[] signatures)
		{
			return BuiltInUriFunctions.builtInFunctions.TryGetValue(name, out signatures);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0002BB00 File Offset: 0x00029D00
		internal static void CreateSpatialFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			FunctionSignatureWithReturnType[] array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true)
				})
			};
			functions.Add("geo.distance", array);
			array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPolygon, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPolygon, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPolygon, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPolygon, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true)
				})
			};
			functions.Add("geo.intersects", array);
			array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryLineString, true) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyLineString, true) })
			};
			functions.Add("geo.length", array);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002BCF0 File Offset: 0x00029EF0
		private static Dictionary<string, FunctionSignatureWithReturnType[]> InitializeBuiltInFunctions()
		{
			Dictionary<string, FunctionSignatureWithReturnType[]> dictionary = new Dictionary<string, FunctionSignatureWithReturnType[]>(StringComparer.Ordinal);
			BuiltInUriFunctions.CreateStringFunctions(dictionary);
			BuiltInUriFunctions.CreateSpatialFunctions(dictionary);
			BuiltInUriFunctions.CreateDateTimeFunctions(dictionary);
			BuiltInUriFunctions.CreateMathFunctions(dictionary);
			return dictionary;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002BD24 File Offset: 0x00029F24
		private static void CreateStringFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("endswith", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("indexof", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("replace", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("startswith", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("tolower", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("toupper", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("trim", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			FunctionSignatureWithReturnType[] array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false),
					EdmCoreModel.Instance.GetInt32(true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true),
					EdmCoreModel.Instance.GetInt32(true)
				})
			};
			functions.Add("substring", array);
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("contains", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("concat", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("length", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002C160 File Offset: 0x0002A360
		private static void CreateDateTimeFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			FunctionSignatureWithReturnType[] array = BuiltInUriFunctions.CreateDateTimeFunctionSignatureArray();
			FunctionSignatureWithReturnType[] array2 = BuiltInUriFunctions.CreateDateTimeOffsetReturnDate();
			FunctionSignatureWithReturnType[] array3 = BuiltInUriFunctions.CreateDateTimeOffsetReturnDecimal();
			FunctionSignatureWithReturnType[] array4 = BuiltInUriFunctions.CreateDateTimeOffsetReturnTimeOfDay();
			FunctionSignatureWithReturnType[] array5 = array.Concat(BuiltInUriFunctions.CreateDurationFunctionSignatures()).ToArray<FunctionSignatureWithReturnType>();
			FunctionSignatureWithReturnType[] array6 = BuiltInUriFunctions.CreateVoidReturnDateTimeOffset();
			FunctionSignatureWithReturnType[] array7 = BuiltInUriFunctions.CreateDurationReturnDecimal();
			FunctionSignatureWithReturnType[] array8 = BuiltInUriFunctions.CreateDateReturnInt();
			FunctionSignatureWithReturnType[] array9 = BuiltInUriFunctions.CreateTimeOfDayReturnInt();
			FunctionSignatureWithReturnType[] array10 = BuiltInUriFunctions.CreateTimeOfDayReturnDecimal();
			FunctionSignatureWithReturnType[] array11 = array.Concat(array8).ToArray<FunctionSignatureWithReturnType>();
			FunctionSignatureWithReturnType[] array12 = array5.Concat(array9).ToArray<FunctionSignatureWithReturnType>();
			FunctionSignatureWithReturnType[] array13 = array3.Concat(array10).ToArray<FunctionSignatureWithReturnType>();
			functions.Add("year", array11);
			functions.Add("month", array11);
			functions.Add("day", array11);
			functions.Add("hour", array12);
			functions.Add("minute", array12);
			functions.Add("second", array12);
			functions.Add("fractionalseconds", array13);
			functions.Add("totaloffsetminutes", array);
			functions.Add("now", array6);
			functions.Add("maxdatetime", array6);
			functions.Add("mindatetime", array6);
			functions.Add("totalseconds", array7);
			functions.Add("date", array2);
			functions.Add("time", array4);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002C29C File Offset: 0x0002A49C
		private static FunctionSignatureWithReturnType[] CreateDateTimeFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType2 };
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0002C303 File Offset: 0x0002A503
		private static IEnumerable<FunctionSignatureWithReturnType> CreateDurationFunctionSignatures()
		{
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false) });
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true) });
			yield break;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002C30C File Offset: 0x0002A50C
		private static FunctionSignatureWithReturnType[] CreateVoidReturnDateTimeOffset()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false), new IEdmTypeReference[0])
			};
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002C330 File Offset: 0x0002A530
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnDate()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDate(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDate(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002C394 File Offset: 0x0002A594
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002C3F8 File Offset: 0x0002A5F8
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnTimeOfDay()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0002C460 File Offset: 0x0002A660
		private static FunctionSignatureWithReturnType[] CreateDurationReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true) })
			};
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0002C4C8 File Offset: 0x0002A6C8
		private static FunctionSignatureWithReturnType[] CreateDateReturnInt()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDate(false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDate(true) })
			};
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0002C52C File Offset: 0x0002A72C
		private static FunctionSignatureWithReturnType[] CreateTimeOfDayReturnInt()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true) })
			};
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0002C594 File Offset: 0x0002A794
		private static FunctionSignatureWithReturnType[] CreateTimeOfDayReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true) })
			};
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0002C5F9 File Offset: 0x0002A7F9
		private static void CreateMathFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			functions.Add("round", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
			functions.Add("floor", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
			functions.Add("ceiling", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0002C62C File Offset: 0x0002A82C
		private static FunctionSignatureWithReturnType[] CreateMathFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(true) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType3 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType4 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType3, functionSignatureWithReturnType2, functionSignatureWithReturnType4 };
		}

		// Token: 0x040007B2 RID: 1970
		private static readonly Dictionary<string, FunctionSignatureWithReturnType[]> builtInFunctions = BuiltInUriFunctions.InitializeBuiltInFunctions();
	}
}
