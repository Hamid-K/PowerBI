using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020002B4 RID: 692
	internal static class BuiltInUriFunctions
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x00050E79 File Offset: 0x0004F079
		internal static bool TryGetBuiltInFunction(string name, out FunctionSignatureWithReturnType[] signatures)
		{
			return BuiltInUriFunctions.builtInFunctions.TryGetValue(name, ref signatures);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00050E88 File Offset: 0x0004F088
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

		// Token: 0x060017D4 RID: 6100 RVA: 0x000510AC File Offset: 0x0004F2AC
		private static Dictionary<string, FunctionSignatureWithReturnType[]> InitializeBuiltInFunctions()
		{
			Dictionary<string, FunctionSignatureWithReturnType[]> dictionary = new Dictionary<string, FunctionSignatureWithReturnType[]>(StringComparer.Ordinal);
			BuiltInUriFunctions.CreateStringFunctions(dictionary);
			BuiltInUriFunctions.CreateSpatialFunctions(dictionary);
			BuiltInUriFunctions.CreateDateTimeFunctions(dictionary);
			BuiltInUriFunctions.CreateMathFunctions(dictionary);
			return dictionary;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000510E0 File Offset: 0x0004F2E0
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

		// Token: 0x060017D6 RID: 6102 RVA: 0x000515B4 File Offset: 0x0004F7B4
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

		// Token: 0x060017D7 RID: 6103 RVA: 0x000516F0 File Offset: 0x0004F8F0
		private static FunctionSignatureWithReturnType[] CreateDateTimeFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType2 };
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00051890 File Offset: 0x0004FA90
		private static IEnumerable<FunctionSignatureWithReturnType> CreateDurationFunctionSignatures()
		{
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false) });
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true) });
			yield break;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000518A8 File Offset: 0x0004FAA8
		private static FunctionSignatureWithReturnType[] CreateVoidReturnDateTimeOffset()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false), new IEdmTypeReference[0])
			};
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000518D8 File Offset: 0x0004FAD8
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnDate()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDate(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDate(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00051944 File Offset: 0x0004FB44
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000519B0 File Offset: 0x0004FBB0
		private static FunctionSignatureWithReturnType[] CreateDateTimeOffsetReturnTimeOfDay()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) })
			};
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00051A20 File Offset: 0x0004FC20
		private static FunctionSignatureWithReturnType[] CreateDurationReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true) })
			};
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00051A8C File Offset: 0x0004FC8C
		private static FunctionSignatureWithReturnType[] CreateDateReturnInt()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDate(false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDate(true) })
			};
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00051AF4 File Offset: 0x0004FCF4
		private static FunctionSignatureWithReturnType[] CreateTimeOfDayReturnInt()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true) })
			};
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00051B60 File Offset: 0x0004FD60
		private static FunctionSignatureWithReturnType[] CreateTimeOfDayReturnDecimal()
		{
			return new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true) })
			};
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00051BCB File Offset: 0x0004FDCB
		private static void CreateMathFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			functions.Add("round", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
			functions.Add("floor", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
			functions.Add("ceiling", BuiltInUriFunctions.CreateMathFunctionSignatureArray());
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00051C00 File Offset: 0x0004FE00
		private static FunctionSignatureWithReturnType[] CreateMathFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(true) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType3 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType4 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType3, functionSignatureWithReturnType2, functionSignatureWithReturnType4 };
		}

		// Token: 0x04000A3B RID: 2619
		private static readonly Dictionary<string, FunctionSignatureWithReturnType[]> builtInFunctions = BuiltInUriFunctions.InitializeBuiltInFunctions();
	}
}
