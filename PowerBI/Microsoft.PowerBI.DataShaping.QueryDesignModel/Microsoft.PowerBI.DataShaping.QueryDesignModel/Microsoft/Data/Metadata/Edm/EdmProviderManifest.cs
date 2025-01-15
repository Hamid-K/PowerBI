using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Xml;
using Microsoft.Data.Common;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000A6 RID: 166
	internal class EdmProviderManifest : DbProviderManifest
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x0001B6E0 File Offset: 0x000198E0
		private EdmProviderManifest()
		{
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001B6E8 File Offset: 0x000198E8
		internal static EdmProviderManifest Instance
		{
			get
			{
				return EdmProviderManifest._instance;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0001B6EF File Offset: 0x000198EF
		public override string NamespaceName
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001B6F6 File Offset: 0x000198F6
		internal string Token
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001B6FD File Offset: 0x000198FD
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			this.InitializeCanonicalFunctions();
			return this._functions;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001B70C File Offset: 0x0001990C
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType type)
		{
			this.InitializeFacetDescriptions();
			ReadOnlyCollection<FacetDescription> readOnlyCollection = null;
			if (this._facetDescriptions.TryGetValue(type as PrimitiveType, out readOnlyCollection))
			{
				return readOnlyCollection;
			}
			return Helper.EmptyFacetDescriptionEnumerable;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001B73D File Offset: 0x0001993D
		public PrimitiveType GetPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes[(int)primitiveTypeKind];
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001B754 File Offset: 0x00019954
		private void InitializePrimitiveTypes()
		{
			if (this._primitiveTypes != null)
			{
				return;
			}
			PrimitiveType[] array = new PrimitiveType[15];
			array[0] = new PrimitiveType();
			array[1] = new PrimitiveType();
			array[2] = new PrimitiveType();
			array[3] = new PrimitiveType();
			array[4] = new PrimitiveType();
			array[5] = new PrimitiveType();
			array[7] = new PrimitiveType();
			array[6] = new PrimitiveType();
			array[9] = new PrimitiveType();
			array[10] = new PrimitiveType();
			array[11] = new PrimitiveType();
			array[8] = new PrimitiveType();
			array[12] = new PrimitiveType();
			array[13] = new PrimitiveType();
			array[14] = new PrimitiveType();
			this.InitializePrimitiveType(array[0], PrimitiveTypeKind.Binary, "Binary", typeof(byte[]));
			this.InitializePrimitiveType(array[1], PrimitiveTypeKind.Boolean, "Boolean", typeof(bool));
			this.InitializePrimitiveType(array[2], PrimitiveTypeKind.Byte, "Byte", typeof(byte));
			this.InitializePrimitiveType(array[3], PrimitiveTypeKind.DateTime, "DateTime", typeof(DateTime));
			this.InitializePrimitiveType(array[4], PrimitiveTypeKind.Decimal, "Decimal", typeof(decimal));
			this.InitializePrimitiveType(array[5], PrimitiveTypeKind.Double, "Double", typeof(double));
			this.InitializePrimitiveType(array[7], PrimitiveTypeKind.Single, "Single", typeof(float));
			this.InitializePrimitiveType(array[6], PrimitiveTypeKind.Guid, "Guid", typeof(Guid));
			this.InitializePrimitiveType(array[9], PrimitiveTypeKind.Int16, "Int16", typeof(short));
			this.InitializePrimitiveType(array[10], PrimitiveTypeKind.Int32, "Int32", typeof(int));
			this.InitializePrimitiveType(array[11], PrimitiveTypeKind.Int64, "Int64", typeof(long));
			this.InitializePrimitiveType(array[8], PrimitiveTypeKind.SByte, "SByte", typeof(sbyte));
			this.InitializePrimitiveType(array[12], PrimitiveTypeKind.String, "String", typeof(string));
			this.InitializePrimitiveType(array[13], PrimitiveTypeKind.Time, "Time", typeof(TimeSpan));
			this.InitializePrimitiveType(array[14], PrimitiveTypeKind.DateTimeOffset, "DateTimeOffset", typeof(DateTimeOffset));
			foreach (PrimitiveType primitiveType in array)
			{
				primitiveType.ProviderManifest = this;
				primitiveType.SetReadOnly();
			}
			ReadOnlyCollection<PrimitiveType> readOnlyCollection = new ReadOnlyCollection<PrimitiveType>(array);
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>>(ref this._primitiveTypes, readOnlyCollection, null);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001B9A7 File Offset: 0x00019BA7
		private void InitializePrimitiveType(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, string name, Type clrType)
		{
			EdmType.Initialize(primitiveType, name, "Edm", DataSpace.CSpace, true, null);
			PrimitiveType.Initialize(primitiveType, primitiveTypeKind, true, this);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		private void InitializeFacetDescriptions()
		{
			if (this._facetDescriptions != null)
			{
				return;
			}
			this.InitializePrimitiveTypes();
			Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> dictionary = new Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>();
			FacetDescription[] array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.String);
			PrimitiveType primitiveType = this._primitiveTypes[12];
			dictionary.Add(primitiveType, Array.AsReadOnly<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Binary);
			primitiveType = this._primitiveTypes[0];
			dictionary.Add(primitiveType, Array.AsReadOnly<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTime);
			primitiveType = this._primitiveTypes[3];
			dictionary.Add(primitiveType, Array.AsReadOnly<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Time);
			primitiveType = this._primitiveTypes[13];
			dictionary.Add(primitiveType, Array.AsReadOnly<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTimeOffset);
			primitiveType = this._primitiveTypes[14];
			dictionary.Add(primitiveType, Array.AsReadOnly<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Decimal);
			primitiveType = this._primitiveTypes[4];
			dictionary.Add(primitiveType, Array.AsReadOnly<FacetDescription>(array));
			Interlocked.CompareExchange<Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>>(ref this._facetDescriptions, dictionary, null);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001BAC0 File Offset: 0x00019CC0
		internal static FacetDescription[] GetInitialFacetDescriptions(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return new FacetDescription[]
				{
					new FacetDescription("MaxLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), null),
					new FacetDescription("FixedLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null)
				};
			case PrimitiveTypeKind.Boolean:
			case PrimitiveTypeKind.Byte:
				break;
			case PrimitiveTypeKind.DateTime:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), null)
				};
			case PrimitiveTypeKind.Decimal:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(1), new int?(255), null),
					new FacetDescription("Scale", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), null)
				};
			default:
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.String:
					return new FacetDescription[]
					{
						new FacetDescription("MaxLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), null),
						new FacetDescription("Unicode", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null),
						new FacetDescription("FixedLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null)
					};
				case PrimitiveTypeKind.Time:
					return new FacetDescription[]
					{
						new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), TypeUsage.DefaultDateTimePrecisionFacetValue)
					};
				case PrimitiveTypeKind.DateTimeOffset:
					return new FacetDescription[]
					{
						new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), TypeUsage.DefaultDateTimePrecisionFacetValue)
					};
				}
				break;
			}
			return null;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		private void InitializeCanonicalFunctions()
		{
			if (this._functions != null)
			{
				return;
			}
			List<EdmFunction> list = new List<EdmFunction>();
			this.InitializePrimitiveTypes();
			TypeUsage[] parameterTypeUsages = new TypeUsage[15];
			Func<PrimitiveTypeKind, string, FunctionParameter> func = (PrimitiveTypeKind ptk, string name) => new FunctionParameter(name, parameterTypeUsages[(int)ptk], ParameterMode.In);
			Func<PrimitiveTypeKind, string, FunctionParameter> func2 = (PrimitiveTypeKind ptk, string name) => new FunctionParameter(name, parameterTypeUsages[(int)ptk], ParameterMode.In);
			Func<PrimitiveTypeKind, string, FunctionParameter> func3 = (PrimitiveTypeKind ptk, string name) => new FunctionParameter(name, parameterTypeUsages[(int)ptk], ParameterMode.In);
			Func<PrimitiveTypeKind, string, FunctionParameter> func4 = (PrimitiveTypeKind ptk, string name) => new FunctionParameter(name, parameterTypeUsages[(int)ptk], ParameterMode.In);
			Func<PrimitiveTypeKind, string, FunctionParameter> func5 = (PrimitiveTypeKind ptk, string name) => new FunctionParameter(name, parameterTypeUsages[(int)ptk], ParameterMode.In);
			Func<PrimitiveTypeKind, string, FunctionParameter> func6 = (PrimitiveTypeKind ptk, string name) => new FunctionParameter(name, parameterTypeUsages[(int)ptk], ParameterMode.In);
			Func<PrimitiveTypeKind, string, FunctionParameter> func7 = (PrimitiveTypeKind ptk, string name) => new FunctionParameter(name, parameterTypeUsages[(int)ptk], ParameterMode.In);
			Func<PrimitiveTypeKind, FunctionParameter> func8 = (PrimitiveTypeKind ptk) => new FunctionParameter("ReturnType", parameterTypeUsages[(int)ptk], ParameterMode.ReturnValue);
			Func<PrimitiveTypeKind, FunctionParameter> func9 = (PrimitiveTypeKind ptk) => new FunctionParameter("collection", TypeUsage.Create(parameterTypeUsages[(int)ptk].EdmType.GetCollectionType()), ParameterMode.In);
			for (int i = 0; i < 15; i++)
			{
				parameterTypeUsages[i] = TypeUsage.Create(this._primitiveTypes[i]);
			}
			string[] array = new string[] { "Max", "Min" };
			PrimitiveTypeKind[] array2 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.DateTime,
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.SByte,
				PrimitiveTypeKind.Single,
				PrimitiveTypeKind.String,
				PrimitiveTypeKind.Binary,
				PrimitiveTypeKind.Time,
				PrimitiveTypeKind.DateTimeOffset
			};
			foreach (string text in array)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind in array2)
				{
					EdmFunction edmFunction = EdmProviderManifest.CreateAggregateCannonicalFunction(text, func8(primitiveTypeKind), func9(primitiveTypeKind));
					list.Add(edmFunction);
				}
			}
			string[] array5 = new string[] { "Avg", "Sum" };
			PrimitiveTypeKind[] array6 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			foreach (string text2 in array5)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind2 in array6)
				{
					EdmFunction edmFunction2 = EdmProviderManifest.CreateAggregateCannonicalFunction(text2, func8(primitiveTypeKind2), func9(primitiveTypeKind2));
					list.Add(edmFunction2);
				}
			}
			string[] array7 = new string[] { "StDev", "StDevP", "Var", "VarP" };
			PrimitiveTypeKind[] array8 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			foreach (string text3 in array7)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind3 in array8)
				{
					EdmFunction edmFunction3 = EdmProviderManifest.CreateAggregateCannonicalFunction(text3, func8(PrimitiveTypeKind.Double), func9(primitiveTypeKind3));
					list.Add(edmFunction3);
				}
			}
			for (int l = 0; l < 15; l++)
			{
				PrimitiveTypeKind primitiveTypeKind4 = (PrimitiveTypeKind)l;
				EdmFunction edmFunction4 = EdmProviderManifest.CreateAggregateCannonicalFunction("Count", func8(PrimitiveTypeKind.Int32), func9(primitiveTypeKind4));
				list.Add(edmFunction4);
			}
			for (int m = 0; m < 15; m++)
			{
				PrimitiveTypeKind primitiveTypeKind5 = (PrimitiveTypeKind)m;
				EdmFunction edmFunction5 = EdmProviderManifest.CreateAggregateCannonicalFunction("BigCount", func8(PrimitiveTypeKind.Int64), func9(primitiveTypeKind5));
				list.Add(edmFunction5);
			}
			list.Add(EdmProviderManifest.CreateCannonicalFunction("Trim", func8(PrimitiveTypeKind.String), new FunctionParameter[] { func(PrimitiveTypeKind.String, "stringArgument") }));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("RTrim", func8(PrimitiveTypeKind.String), new FunctionParameter[] { func(PrimitiveTypeKind.String, "stringArgument") }));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("LTrim", func8(PrimitiveTypeKind.String), new FunctionParameter[] { func(PrimitiveTypeKind.String, "stringArgument") }));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("Concat", func8(PrimitiveTypeKind.String), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.String, "string1"),
				func2(PrimitiveTypeKind.String, "string2")
			}));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("Length", func8(PrimitiveTypeKind.Int32), new FunctionParameter[] { func(PrimitiveTypeKind.String, "stringArgument") }));
			PrimitiveTypeKind[] array9 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.SByte
			};
			foreach (PrimitiveTypeKind primitiveTypeKind6 in array9)
			{
				list.Add(EdmProviderManifest.CreateCannonicalFunction("Substring", func8(PrimitiveTypeKind.String), new FunctionParameter[]
				{
					func(PrimitiveTypeKind.String, "stringArgument"),
					func2(primitiveTypeKind6, "start"),
					func3(primitiveTypeKind6, "length")
				}));
			}
			foreach (PrimitiveTypeKind primitiveTypeKind7 in array9)
			{
				list.Add(EdmProviderManifest.CreateCannonicalFunction("Left", func8(PrimitiveTypeKind.String), new FunctionParameter[]
				{
					func(PrimitiveTypeKind.String, "stringArgument"),
					func2(primitiveTypeKind7, "length")
				}));
			}
			foreach (PrimitiveTypeKind primitiveTypeKind8 in array9)
			{
				list.Add(EdmProviderManifest.CreateCannonicalFunction("Right", func8(PrimitiveTypeKind.String), new FunctionParameter[]
				{
					func(PrimitiveTypeKind.String, "stringArgument"),
					func2(primitiveTypeKind8, "length")
				}));
			}
			list.Add(EdmProviderManifest.CreateCannonicalFunction("Replace", func8(PrimitiveTypeKind.String), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.String, "stringArgument"),
				func2(PrimitiveTypeKind.String, "toReplace"),
				func3(PrimitiveTypeKind.String, "replacement")
			}));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("IndexOf", func8(PrimitiveTypeKind.Int32), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.String, "searchString"),
				func2(PrimitiveTypeKind.String, "stringToFind")
			}));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("ToUpper", func8(PrimitiveTypeKind.String), new FunctionParameter[] { func(PrimitiveTypeKind.String, "stringArgument") }));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("ToLower", func8(PrimitiveTypeKind.String), new FunctionParameter[] { func(PrimitiveTypeKind.String, "stringArgument") }));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("Reverse", func8(PrimitiveTypeKind.String), new FunctionParameter[] { func(PrimitiveTypeKind.String, "stringArgument") }));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("Contains", func8(PrimitiveTypeKind.Boolean), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.String, "searchedString"),
				func2(PrimitiveTypeKind.String, "searchedForString")
			}));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("StartsWith", func8(PrimitiveTypeKind.Boolean), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.String, "stringArgument"),
				func2(PrimitiveTypeKind.String, "prefix")
			}));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("EndsWith", func8(PrimitiveTypeKind.Boolean), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.String, "stringArgument"),
				func2(PrimitiveTypeKind.String, "suffix")
			}));
			string[] array10 = new string[] { "Year", "Month", "Day", "DayOfYear" };
			PrimitiveTypeKind[] array11 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.DateTimeOffset,
				PrimitiveTypeKind.DateTime
			};
			foreach (string text4 in array10)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind9 in array11)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text4, func8(PrimitiveTypeKind.Int32), new FunctionParameter[] { func(primitiveTypeKind9, "dateValue") }));
				}
			}
			string[] array12 = new string[] { "Hour", "Minute", "Second", "Millisecond" };
			PrimitiveTypeKind[] array13 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.DateTimeOffset,
				PrimitiveTypeKind.DateTime,
				PrimitiveTypeKind.Time
			};
			foreach (string text5 in array12)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind10 in array13)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text5, func8(PrimitiveTypeKind.Int32), new FunctionParameter[] { func(primitiveTypeKind10, "timeValue") }));
				}
			}
			EdmFunction edmFunction6 = EdmProviderManifest.CreateCannonicalFunction("CurrentDateTime", func8(PrimitiveTypeKind.DateTime), Array.Empty<FunctionParameter>());
			list.Add(edmFunction6);
			EdmFunction edmFunction7 = EdmProviderManifest.CreateCannonicalFunction("CurrentDateTimeOffset", func8(PrimitiveTypeKind.DateTimeOffset), Array.Empty<FunctionParameter>());
			list.Add(edmFunction7);
			list.Add(EdmProviderManifest.CreateCannonicalFunction("GetTotalOffsetMinutes", func8(PrimitiveTypeKind.Int32), new FunctionParameter[] { func(PrimitiveTypeKind.DateTimeOffset, "dateTimeOffsetArgument") }));
			edmFunction6 = EdmProviderManifest.CreateCannonicalFunction("CurrentUtcDateTime", func8(PrimitiveTypeKind.DateTime), Array.Empty<FunctionParameter>());
			list.Add(edmFunction6);
			foreach (PrimitiveTypeKind primitiveTypeKind11 in array11)
			{
				list.Add(EdmProviderManifest.CreateCannonicalFunction("TruncateTime", func8(primitiveTypeKind11), new FunctionParameter[] { func(primitiveTypeKind11, "dateValue") }));
			}
			list.Add(EdmProviderManifest.CreateCannonicalFunction("CreateDateTime", func8(PrimitiveTypeKind.DateTime), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.Int32, "year"),
				func2(PrimitiveTypeKind.Int32, "month"),
				func3(PrimitiveTypeKind.Int32, "day"),
				func4(PrimitiveTypeKind.Int32, "hour"),
				func5(PrimitiveTypeKind.Int32, "minute"),
				func6(PrimitiveTypeKind.Double, "second")
			}));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("CreateDateTimeOffset", func8(PrimitiveTypeKind.DateTimeOffset), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.Int32, "year"),
				func2(PrimitiveTypeKind.Int32, "month"),
				func3(PrimitiveTypeKind.Int32, "day"),
				func4(PrimitiveTypeKind.Int32, "hour"),
				func5(PrimitiveTypeKind.Int32, "minute"),
				func6(PrimitiveTypeKind.Double, "second"),
				func7(PrimitiveTypeKind.Int32, "timeZoneOffset")
			}));
			list.Add(EdmProviderManifest.CreateCannonicalFunction("CreateTime", func8(PrimitiveTypeKind.Time), new FunctionParameter[]
			{
				func(PrimitiveTypeKind.Int32, "hour"),
				func2(PrimitiveTypeKind.Int32, "minute"),
				func3(PrimitiveTypeKind.Double, "second")
			}));
			foreach (string text6 in new string[] { "AddYears", "AddMonths", "AddDays" })
			{
				foreach (PrimitiveTypeKind primitiveTypeKind12 in array11)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text6, func8(primitiveTypeKind12), new FunctionParameter[]
					{
						func(primitiveTypeKind12, "dateValue"),
						func2(PrimitiveTypeKind.Int32, "addValue")
					}));
				}
			}
			foreach (string text7 in new string[] { "AddHours", "AddMinutes", "AddSeconds", "AddMilliseconds", "AddMicroseconds", "AddNanoseconds" })
			{
				foreach (PrimitiveTypeKind primitiveTypeKind13 in array13)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text7, func8(primitiveTypeKind13), new FunctionParameter[]
					{
						func(primitiveTypeKind13, "timeValue"),
						func2(PrimitiveTypeKind.Int32, "addValue")
					}));
				}
			}
			foreach (string text8 in new string[] { "DiffYears", "DiffMonths", "DiffDays" })
			{
				foreach (PrimitiveTypeKind primitiveTypeKind14 in array11)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text8, func8(PrimitiveTypeKind.Int32), new FunctionParameter[]
					{
						func(primitiveTypeKind14, "dateValue1"),
						func2(primitiveTypeKind14, "dateValue2")
					}));
				}
			}
			foreach (string text9 in new string[] { "DiffHours", "DiffMinutes", "DiffSeconds", "DiffMilliseconds", "DiffMicroseconds", "DiffNanoseconds" })
			{
				foreach (PrimitiveTypeKind primitiveTypeKind15 in array13)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text9, func8(PrimitiveTypeKind.Int32), new FunctionParameter[]
					{
						func(primitiveTypeKind15, "timeValue1"),
						func2(primitiveTypeKind15, "timeValue2")
					}));
				}
			}
			string[] array14 = new string[] { "Round", "Floor", "Ceiling" };
			PrimitiveTypeKind[] array15 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Single,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			foreach (string text10 in array14)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind16 in array15)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text10, func8(primitiveTypeKind16), new FunctionParameter[] { func(primitiveTypeKind16, "value") }));
				}
			}
			string[] array16 = new string[] { "Round", "Truncate" };
			PrimitiveTypeKind[] array17 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			foreach (string text11 in array16)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind17 in array17)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text11, func8(primitiveTypeKind17), new FunctionParameter[]
					{
						func(primitiveTypeKind17, "value"),
						func2(PrimitiveTypeKind.Int32, "digits")
					}));
				}
			}
			foreach (PrimitiveTypeKind primitiveTypeKind18 in new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.Single
			})
			{
				list.Add(EdmProviderManifest.CreateCannonicalFunction("Abs", func8(primitiveTypeKind18), new FunctionParameter[] { func(primitiveTypeKind18, "value") }));
			}
			PrimitiveTypeKind[] array18 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			PrimitiveTypeKind[] array19 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int64
			};
			foreach (PrimitiveTypeKind primitiveTypeKind19 in array18)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind20 in array19)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction("Power", func8(primitiveTypeKind19), new FunctionParameter[]
					{
						func(primitiveTypeKind19, "baseArgument"),
						func2(primitiveTypeKind20, "exponent")
					}));
				}
			}
			string[] array21 = new string[] { "BitwiseAnd", "BitwiseOr", "BitwiseXor" };
			PrimitiveTypeKind[] array22 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.Byte
			};
			foreach (string text12 in array21)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind21 in array22)
				{
					list.Add(EdmProviderManifest.CreateCannonicalFunction(text12, func8(primitiveTypeKind21), new FunctionParameter[]
					{
						func(primitiveTypeKind21, "value1"),
						func2(primitiveTypeKind21, "value2")
					}));
				}
			}
			foreach (PrimitiveTypeKind primitiveTypeKind22 in array22)
			{
				list.Add(EdmProviderManifest.CreateCannonicalFunction("BitwiseNot", func8(primitiveTypeKind22), new FunctionParameter[] { func(primitiveTypeKind22, "value") }));
			}
			list.Add(EdmProviderManifest.CreateCannonicalFunction("NewGuid", func8(PrimitiveTypeKind.Guid), Array.Empty<FunctionParameter>()));
			foreach (EdmFunction edmFunction8 in list)
			{
				edmFunction8.SetReadOnly();
			}
			ReadOnlyCollection<EdmFunction> readOnlyCollection = new ReadOnlyCollection<EdmFunction>(list);
			Interlocked.CompareExchange<ReadOnlyCollection<EdmFunction>>(ref this._functions, readOnlyCollection, null);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001CD98 File Offset: 0x0001AF98
		private static EdmFunction CreateAggregateCannonicalFunction(string name, FunctionParameter returnParameter, FunctionParameter parameter)
		{
			return new EdmFunction(name, "Edm", DataSpace.CSpace, new EdmFunctionPayload
			{
				IsAggregate = new bool?(true),
				IsBuiltIn = new bool?(true),
				ReturnParameter = returnParameter,
				Parameters = new FunctionParameter[] { parameter }
			});
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		private static EdmFunction CreateCannonicalFunction(string name, FunctionParameter returnParameter, params FunctionParameter[] parameters)
		{
			return new EdmFunction(name, "Edm", DataSpace.CSpace, new EdmFunctionPayload
			{
				IsBuiltIn = new bool?(true),
				ReturnParameter = returnParameter,
				Parameters = parameters
			});
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001CE2F File Offset: 0x0001B02F
		internal ReadOnlyCollection<PrimitiveType> GetPromotionTypes(PrimitiveType primitiveType)
		{
			this.InitializePromotableTypes();
			return this._promotionTypes[(int)primitiveType.PrimitiveTypeKind];
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001CE44 File Offset: 0x0001B044
		private void InitializePromotableTypes()
		{
			if (this._promotionTypes != null)
			{
				return;
			}
			ReadOnlyCollection<PrimitiveType>[] array = new ReadOnlyCollection<PrimitiveType>[15];
			for (int i = 0; i < 15; i++)
			{
				array[i] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[] { this._primitiveTypes[i] });
			}
			array[2] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[2],
				this._primitiveTypes[9],
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[9] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[9],
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[10] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[11] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[7] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>[]>(ref this._promotionTypes, array, null);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001D054 File Offset: 0x0001B254
		internal TypeUsage GetCanonicalModelTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			if (EdmProviderManifest._canonicalModelTypes == null)
			{
				this.InitializeCanonicalModelTypes();
			}
			return EdmProviderManifest._canonicalModelTypes[(int)primitiveTypeKind];
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001D06C File Offset: 0x0001B26C
		private void InitializeCanonicalModelTypes()
		{
			this.InitializePrimitiveTypes();
			TypeUsage[] array = new TypeUsage[15];
			for (int i = 0; i < 15; i++)
			{
				TypeUsage typeUsage = TypeUsage.CreateDefaultTypeUsage(this._primitiveTypes[i]);
				array[i] = typeUsage;
			}
			Interlocked.CompareExchange<TypeUsage[]>(ref EdmProviderManifest._canonicalModelTypes, array, null);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001D0B7 File Offset: 0x0001B2B7
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001D0C5 File Offset: 0x0001B2C5
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		internal TypeUsage ForgetScalarConstraints(TypeUsage type)
		{
			PrimitiveType primitiveType = type.EdmType as PrimitiveType;
			if (primitiveType != null)
			{
				return this.GetCanonicalModelTypeUsage(primitiveType.PrimitiveTypeKind);
			}
			return type;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001D0FE File Offset: 0x0001B2FE
		protected override XmlReader GetDbInformation(string informationType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000898 RID: 2200
		internal const string ConcurrencyModeFacetName = "ConcurrencyMode";

		// Token: 0x04000899 RID: 2201
		internal const string StoreGeneratedPatternFacetName = "StoreGeneratedPattern";

		// Token: 0x0400089A RID: 2202
		private Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> _facetDescriptions;

		// Token: 0x0400089B RID: 2203
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x0400089C RID: 2204
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x0400089D RID: 2205
		private static EdmProviderManifest _instance = new EdmProviderManifest();

		// Token: 0x0400089E RID: 2206
		private ReadOnlyCollection<PrimitiveType>[] _promotionTypes;

		// Token: 0x0400089F RID: 2207
		private static TypeUsage[] _canonicalModelTypes;

		// Token: 0x040008A0 RID: 2208
		internal const byte MaximumDecimalPrecision = 255;

		// Token: 0x040008A1 RID: 2209
		internal const byte MaximumDateTimePrecision = 255;
	}
}
