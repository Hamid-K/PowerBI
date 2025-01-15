using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019C RID: 412
	internal static class BuiltInUriFunctions
	{
		// Token: 0x060010BD RID: 4285 RVA: 0x0002DE21 File Offset: 0x0002C021
		internal static bool TryGetBuiltInFunction(string name, out FunctionSignatureWithReturnType[] signatures)
		{
			return BuiltInUriFunctions.builtInFunctions.TryGetValue(name, ref signatures);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0002DE30 File Offset: 0x0002C030
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

		// Token: 0x060010BF RID: 4287 RVA: 0x0002E020 File Offset: 0x0002C220
		private static Dictionary<string, FunctionSignatureWithReturnType[]> InitializeBuiltInFunctions()
		{
			Dictionary<string, FunctionSignatureWithReturnType[]> dictionary = new Dictionary<string, FunctionSignatureWithReturnType[]>(StringComparer.Ordinal);
			BuiltInUriFunctions.CreateStringFunctions(dictionary);
			BuiltInUriFunctions.CreateSpatialFunctions(dictionary);
			BuiltInUriFunctions.CreateDateTimeFunctions(dictionary);
			BuiltInUriFunctions.CreateMathFunctions(dictionary);
			return dictionary;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0002E054 File Offset: 0x0002C254
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

		// Token: 0x060010C1 RID: 4289 RVA: 0x0002E490 File Offset: 0x0002C690
		private static void CreateDateTimeFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			FunctionSignatureWithReturnType[] array = BuiltInUriFunctions.CreateDateTimeFunctionSignatureArray();
			FunctionSignatureWithReturnType[] array2 = BuiltInUriFunctions.CreateDateTimeOffsetReturnDate();
			FunctionSignatureWithReturnType[] array3 = BuiltInUriFunctions.CreateDateTimeOffsetReturnDecimal();
			FunctionSignatureWithReturnType[] array4 = BuiltInUriFunctions.CreateDateTimeOffsetReturnTimeOfDay();
			FunctionSignatureWithReturnType[] array5 = Enumerable.ToArray<FunctionSignatureWithReturnType>(Enumerable.Concat<FunctionSignatureWithReturnType>(array, BuiltInUriFunctions.CreateDurationFunctionSignatures()));
			FunctionSignatureWithReturnType[] array6 = BuiltInUriFunctions.CreateVoidReturnDateTimeOffset();
			FunctionSignatureWithReturnType[] array7 = BuiltInUriFunctions.CreateDurationReturnDecimal();
			FunctionSignatureWithReturnType[] array8 = BuiltInUriFunctions.CreateDateReturnInt();
			FunctionSignatureWithReturnType[] array9 = BuiltInUriFunctions.CreateTimeOfDayReturnInt();
			FunctionSignatureWithReturnType[] array10 = BuiltInUriFunctions.CreateTimeOfDayReturnDecimal();
			FunctionSignatureWithReturnType[] array11 = Enumerable.ToArray<FunctionSignatureWithReturnType>(Enumerable.Concat<FunctionSignatureWithReturnType>(array, array8));
			FunctionSignatureWithReturnType[] array12 = Enumerable.ToArray<FunctionSignatureWithReturnType>(Enumerable.Concat<FunctionSignatureWithReturnType>(array5, array9));
			FunctionSignatureWithReturnType[] array13 = Enumerable.ToArray<FunctionSignatureWithReturnType>(Enumerable.Concat<FunctionSignatureWithReturnType>(array3, array10));
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

		// Token: 0x060010C2 RID: 4290 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		private static FunctionSignatureWithReturnType[] CreateDateTimeFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType2 };
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0002E633 File Offset: 0x0002C833
		private static IEnumerable<FunctionSignatureWithReturnType> CreateDurationFunctionSignatures()
		{
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false) });
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true) });
			yield break;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0002E63C File Offset: 0x0002C83C
		private static FunctionSignatureWithReturnType[] CreateVoidReturnDateTimeOffset()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false), new IEdmTypeReference[0])
			};
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002E660 File Offset: 0x0002C860
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnDate()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDate(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDate(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002E6C4 File Offset: 0x0002C8C4
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002E728 File Offset: 0x0002C928
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnTimeOfDay()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0002E790 File Offset: 0x0002C990
		private static FunctionSignatureWithReturnType[] CreateDurationReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true) })
			};
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0002E7F8 File Offset: 0x0002C9F8
		private static FunctionSignatureWithReturnType[] CreateDateReturnInt()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDate(false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDate(true) })
			};
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0002E85C File Offset: 0x0002CA5C
		private static FunctionSignatureWithReturnType[] CreateTimeOfDayReturnInt()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true) })
			};
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0002E8C4 File Offset: 0x0002CAC4
		private static FunctionSignatureWithReturnType[] CreateTimeOfDayReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true) })
			};
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0002E929 File Offset: 0x0002CB29
		private static void CreateMathFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			functions.Add("round", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
			functions.Add("floor", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
			functions.Add("ceiling", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0002E95C File Offset: 0x0002CB5C
		private static FunctionSignatureWithReturnType[] CreateMathFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(true) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType3 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType4 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType3, functionSignatureWithReturnType2, functionSignatureWithReturnType4 };
		}

		// Token: 0x040008AF RID: 2223
		private static readonly Dictionary<string, FunctionSignatureWithReturnType[]> builtInFunctions = BuiltInUriFunctions.InitializeBuiltInFunctions();
	}
}
