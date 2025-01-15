using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Lineage;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C1D RID: 3101
	internal sealed class ExcelModule : Module
	{
		// Token: 0x0600547A RID: 21626 RVA: 0x00121B52 File Offset: 0x0011FD52
		public static ValueException GetExcelError(TextValue excelErrorMessage)
		{
			return ValueException.NewDataFormatError<Message1>(Strings.InvalidCellValue(excelErrorMessage), Value.Null, null);
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x00121B65 File Offset: 0x0011FD65
		public override string Name
		{
			get
			{
				return "Excel";
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x0600547C RID: 21628 RVA: 0x00121B6C File Offset: 0x0011FD6C
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Excel.Workbook";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "Excel.ShapeTable";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x00121BA8 File Offset: 0x0011FDA8
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ExcelModule.WorkbookFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException();
				}
				return FoldableFunctionValue.New(new ExcelModule.ShapeTableFunctionValue(hostEnvironment));
			});
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x00121BDC File Offset: 0x0011FDDC
		public static bool IsShapingFunction(FunctionValue function, out FunctionValue innerFunction)
		{
			FoldableFunctionValue foldableFunctionValue = function as FoldableFunctionValue;
			if (foldableFunctionValue != null && foldableFunctionValue.Function.FunctionIdentity is ExcelModule.ShapeTableFunctionValue)
			{
				innerFunction = foldableFunctionValue.Function;
				return true;
			}
			innerFunction = null;
			return false;
		}

		// Token: 0x04002EC0 RID: 11968
		public const string DataSourceNameString = "Excel Workbook";

		// Token: 0x04002EC1 RID: 11969
		public const string ExcelWorkbook = "Excel.Workbook";

		// Token: 0x04002EC2 RID: 11970
		public const string ExcelShapeTable = "Excel.ShapeTable";

		// Token: 0x04002EC3 RID: 11971
		private Keys exportKeys;

		// Token: 0x02000C1E RID: 3102
		private enum Exports
		{
			// Token: 0x04002EC5 RID: 11973
			ExcelWorkbook,
			// Token: 0x04002EC6 RID: 11974
			ExcelShapeTable,
			// Token: 0x04002EC7 RID: 11975
			Count
		}

		// Token: 0x02000C1F RID: 3103
		private sealed class WorkbookFunctionValue : NativeFunctionValue3<TableValue, BinaryValue, Value, Value>
		{
			// Token: 0x06005480 RID: 21632 RVA: 0x00121C14 File Offset: 0x0011FE14
			public WorkbookFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "workbook", TypeValue.Binary, "useHeaders", TypeValue.Any, "delayTypes", NullableTypeValue.Logical)
			{
				this.host = host;
			}

			// Token: 0x06005481 RID: 21633 RVA: 0x00121C54 File Offset: 0x0011FE54
			public override TableValue TypedInvoke(BinaryValue workbook, Value useHeaders, Value delayTypes)
			{
				Value @null = Value.Null;
				if (useHeaders.IsRecord && delayTypes.IsNull)
				{
					ExcelModule.WorkbookFunctionValue.GetParameters(useHeaders.AsRecord, out useHeaders, out delayTypes, out @null);
				}
				bool flag = !useHeaders.IsNull && useHeaders.AsBoolean;
				bool flag2 = delayTypes.IsNull || !delayTypes.AsBoolean;
				bool flag3 = !@null.IsNull && @null.AsBoolean;
				ExcelFile excelFile = new ExcelFile(this.host, workbook);
				this.HandleClassification(excelFile);
				try
				{
					return new ExcelReaderOpenXml(this.host, excelFile, flag, flag2, flag3, false).ReadTables();
				}
				catch (OpenXmlPackageException ex)
				{
				}
				catch (FileFormatException ex)
				{
				}
				Exception ex;
				this.TraceException("ExcelReaderOpenXml", ex, flag, flag2);
				TableValue tableValue;
				try
				{
					if (!@null.IsNull)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.InvalidFileTypeForInferSheetDimensionsParameter, null, null);
					}
					tableValue = ExcelReaderAce.ReadTables(this.host, excelFile, flag);
				}
				catch (Exception ex2)
				{
					Value value;
					if (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex2) && workbook.TryGetMetaField("Content.Type", out value) && value.IsText && ExcelModule.WorkbookFunctionValue.ShouldUseExcelReaderOpenXml(value.AsString))
					{
						this.TraceException("ExcelReaderAce", ex2, flag, flag2);
						throw ValueException.NewDataFormatError(ex.Message, workbook, ex);
					}
					throw;
				}
				return tableValue;
			}

			// Token: 0x06005482 RID: 21634 RVA: 0x00121DB0 File Offset: 0x0011FFB0
			private void HandleClassification(ExcelFile file)
			{
				IMipService ipService = this.host.QueryService<IMipService>();
				IPersistentCache persistentCache = this.host.QueryService<ICacheSets>().Metadata.PersistentCache;
				string text = PersistentCacheKey.ExcelWorkbook.Qualify(file.CacheKey, "InformationProtection.3");
				FileProtectionInformation fileProtectionInformation = null;
				Stream stream;
				if (persistentCache.TryGetValue(text, out stream))
				{
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						fileProtectionInformation = FileProtectionInformationSerialization.ReadFileProtectionInformation(binaryReader);
					}
					stream.Dispose();
				}
				else if (ipService != null)
				{
					using (file.Lock())
					{
						using (Stream stream2 = file.Open())
						{
							fileProtectionInformation = ipService.GetInfo(file.Resource, stream2, file.FileExtension);
						}
					}
					stream = persistentCache.BeginAdd();
					using (BinaryWriter binaryWriter = new BinaryWriter(stream))
					{
						FileProtectionInformationSerialization.WriteFileProtectionInformation(binaryWriter, fileProtectionInformation);
						persistentCache.EndAdd(text, stream).Dispose();
					}
				}
				RecordValue recordValue = null;
				if (fileProtectionInformation != null)
				{
					ProtectionInformation protectionInformation = fileProtectionInformation.ProtectionInformation;
					if (((protectionInformation != null) ? protectionInformation.Id : null) != null)
					{
						recordValue = InformationProtectionTraits.CreateTrait(fileProtectionInformation.ProtectionInformation);
					}
				}
				if (fileProtectionInformation != null && fileProtectionInformation.Encrypted)
				{
					if (file.Resource == null)
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.EncryptedWorkbook_NotSupported, file.Content, null);
					}
					file.Decrypt((Stream s) => ipService.GetDecryptedStream(file.Resource, s, file.FileExtension));
				}
				ITraitTrackingService traitTrackingService = this.host.QueryService<ITraitTrackingService>();
				if (recordValue != null && traitTrackingService != null)
				{
					traitTrackingService.AddTrait(recordValue);
				}
			}

			// Token: 0x06005483 RID: 21635 RVA: 0x00121FB0 File Offset: 0x001201B0
			private static void GetParameters(RecordValue options, out Value useHeaders, out Value delayTypes, out Value inferSheetDimensions)
			{
				HashSet<string> hashSet = new HashSet<string>(options.Keys);
				ExcelModule.WorkbookFunctionValue.GetParameter(options, hashSet, "UseHeaders", Value.Null, out useHeaders);
				ExcelModule.WorkbookFunctionValue.GetParameter(options, hashSet, "DelayTypes", Value.Null, out delayTypes);
				ExcelModule.WorkbookFunctionValue.GetParameter(options, hashSet, "InferSheetDimensions", Value.Null, out inferSheetDimensions);
				if (hashSet.Count != 0)
				{
					string text = hashSet.First<string>();
					throw ValueException.NewExpressionError<Message1>(Strings.InvalidExcelWorkbookParameter(text), options[text], null);
				}
			}

			// Token: 0x06005484 RID: 21636 RVA: 0x000EFA64 File Offset: 0x000EDC64
			private static void GetParameter(RecordValue options, HashSet<string> keys, string key, Value defaultValue, out Value value)
			{
				if (options.TryGetValue(key, out value))
				{
					keys.Remove(key);
					return;
				}
				value = defaultValue;
			}

			// Token: 0x06005485 RID: 21637 RVA: 0x00122022 File Offset: 0x00120222
			private static bool ShouldUseExcelReaderOpenXml(string contentType)
			{
				return contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || contentType == "application/vnd.ms-excel.sheet.macroEnabled.12";
			}

			// Token: 0x06005486 RID: 21638 RVA: 0x00122040 File Offset: 0x00120240
			private void TraceException(string entrySuffix, Exception e, bool useFirstRowAsHeader, bool inferTypes)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/Excel.Workbook/" + entrySuffix, TraceEventType.Information, null))
				{
					hostTrace.Add(e, TraceEventType.Warning, true);
					hostTrace.Add("useFirstRowHeader", useFirstRowAsHeader, false);
					hostTrace.Add("inferTypes", inferTypes, false);
				}
			}

			// Token: 0x04002EC8 RID: 11976
			private readonly IEngineHost host;
		}

		// Token: 0x02000C21 RID: 3105
		private sealed class ShapeTableFunctionValue : NativeFunctionValue2<Value, TableValue, Value>
		{
			// Token: 0x06005489 RID: 21641 RVA: 0x001220D4 File Offset: 0x001202D4
			public ShapeTableFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, 1, "table", TypeValue.Table, "options", ExcelModule.ShapeTableFunctionValue.optionsType)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x0600548A RID: 21642 RVA: 0x00122100 File Offset: 0x00120300
			public override Value TypedInvoke(TableValue table, Value options)
			{
				ExcelModule.ShapeTableFunctionValue.TransformBuilder transformBuilder = ExcelModule.ShapeTableFunctionValue.TransformBuilder.New(this.engineHost, options);
				RecordValue fields = table.Type.AsTableType.ItemType.Fields;
				List<string> list = new List<string>(fields.Keys.Length);
				for (int i = 0; i < fields.Keys.Length; i++)
				{
					if (transformBuilder.RemoveTopLevelType(fields[i]["Type"].AsType))
					{
						list.Add(fields.Keys[i]);
					}
				}
				if (list.Count > 0)
				{
					table = table.RemoveColumns(ListValue.New(list.ToArray()), MissingFieldMode.Ignore);
				}
				return transformBuilder.ShapeTable(table);
			}

			// Token: 0x0600548B RID: 21643 RVA: 0x001221AC File Offset: 0x001203AC
			private static bool TryConvertStringList(Value value, out object result)
			{
				if (value.IsNull)
				{
					result = null;
					return true;
				}
				IList<string> list;
				if (value.IsList && value.AsList.TryGetStringList(100, out list))
				{
					result = list;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x04002ECB RID: 11979
			private static readonly TypeValue nullableStringList = ListTypeValue.New(TypeValue.Text).Nullable;

			// Token: 0x04002ECC RID: 11980
			private static readonly OptionRecordDefinition optionRecord = new OptionRecordDefinition(new OptionItem[]
			{
				new OptionItem("MaxDepth", NullableTypeValue.Int32, NumberValue.Zero, OptionItemOption.None, null, null),
				new OptionItem("AllowedTypeNames", ExcelModule.ShapeTableFunctionValue.nullableStringList, Value.Null, OptionItemOption.None, new TryConvertOption(ExcelModule.ShapeTableFunctionValue.TryConvertStringList), null),
				new OptionItem("ApiVersion", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null)
			});

			// Token: 0x04002ECD RID: 11981
			private static readonly TypeValue optionsType = ExcelModule.ShapeTableFunctionValue.optionRecord.CreateRecordType().Nullable;

			// Token: 0x04002ECE RID: 11982
			private readonly IEngineHost engineHost;

			// Token: 0x02000C22 RID: 3106
			private abstract class TransformBuilder
			{
				// Token: 0x0600548D RID: 21645 RVA: 0x00122288 File Offset: 0x00120488
				protected TransformBuilder(IEngineHost engineHost, OptionsRecord optionsRecord, bool unsupportedDisplayAsNull, bool allowedOptionalFields)
				{
					this.maxDepth = optionsRecord.GetInt32("MaxDepth", 100);
					this.displayNameFunctionValue = new ExcelModule.ShapeTableFunctionValue.TransformBuilder.DisplayNameFunctionValue(engineHost, unsupportedDisplayAsNull || this.maxDepth > 0);
					this.allowedOptionalFields = allowedOptionalFields;
					this.allowedTypeNames = new HashSet<string>();
					Value value;
					if (optionsRecord.TryGetValue("AllowedTypeNames", out value) && !value.IsNull)
					{
						foreach (IValueReference valueReference in value.AsList)
						{
							this.allowedTypeNames.Add(valueReference.Value.AsString);
						}
					}
				}

				// Token: 0x0600548E RID: 21646 RVA: 0x00122344 File Offset: 0x00120544
				public static ExcelModule.ShapeTableFunctionValue.TransformBuilder New(IEngineHost engineHost, Value options)
				{
					OptionsRecord optionsRecord = ExcelModule.ShapeTableFunctionValue.optionRecord.CreateOptions("Excel.ShapeTable", options);
					string text;
					if (!optionsRecord.TryGetString("ApiVersion", out text) || text == "ExcelDesktop.0")
					{
						return new ExcelModule.ShapeTableFunctionValue.TransformBuilderDesktop(engineHost, optionsRecord);
					}
					if (text == "ExcelOnline.0")
					{
						return new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo(engineHost, optionsRecord);
					}
					throw ValueException.NewExpressionError<Message2>(Strings.InvalidValueForOption(text, "ApiVersion"), RecordValue.New(Keys.New("DataSourceName", "ApiVersion"), new Value[]
					{
						TextValue.New("Excel.ShapeTable"),
						ListValue.New(new Value[]
						{
							TextValue.New("ExcelDesktop.0"),
							TextValue.New("ExcelOnline.0")
						})
					}), null);
				}

				// Token: 0x170019CE RID: 6606
				// (get) Token: 0x0600548F RID: 21647 RVA: 0x001223FB File Offset: 0x001205FB
				public FunctionValue DisplayFunctionValue
				{
					get
					{
						return this.displayNameFunctionValue;
					}
				}

				// Token: 0x170019CF RID: 6607
				// (get) Token: 0x06005490 RID: 21648 RVA: 0x00122403 File Offset: 0x00120603
				public int MaxDepth
				{
					get
					{
						return this.maxDepth;
					}
				}

				// Token: 0x06005491 RID: 21649 RVA: 0x0012240C File Offset: 0x0012060C
				public bool RemoveTopLevelType(TypeValue type)
				{
					ValueKind typeKind = type.TypeKind;
					if (typeKind == ValueKind.Any)
					{
						return false;
					}
					if (typeKind != ValueKind.Record)
					{
						TextValue textValue;
						return !ExcelModule.ShapeTableFunctionValue.TransformBuilder.AllowedType(type.TypeKind, out textValue);
					}
					Value value;
					return !this.IsEntityType(type.AsRecordType, out value);
				}

				// Token: 0x06005492 RID: 21650 RVA: 0x00122450 File Offset: 0x00120650
				protected bool IsEntityType(RecordTypeValue recordType, out Value typeName)
				{
					typeName = null;
					return !recordType.AsRecordType.Open && recordType.MetaValue.TryGetValue("Documentation.TypeName", out typeName) && typeName.IsText && this.allowedTypeNames.Contains(typeName.AsString) && (this.allowedOptionalFields || !ExcelModule.ShapeTableFunctionValue.TransformBuilder.AnyAreOptional(recordType.Fields));
				}

				// Token: 0x06005493 RID: 21651
				public abstract Value ShapeTable(TableValue table);

				// Token: 0x06005494 RID: 21652 RVA: 0x001224B8 File Offset: 0x001206B8
				protected static string UniqueColumnName(Keys existingColumns)
				{
					int num = 1;
					string text;
					for (;;)
					{
						text = "Column" + num.ToString(CultureInfo.InvariantCulture);
						if (!existingColumns.Contains(text))
						{
							break;
						}
						num++;
					}
					return text;
				}

				// Token: 0x06005495 RID: 21653 RVA: 0x001224F0 File Offset: 0x001206F0
				protected static bool AllowedType(ValueKind kind, out TextValue asText)
				{
					asText = null;
					switch (kind)
					{
					case ValueKind.None:
						asText = NoneValue.Placeholder;
						return false;
					case ValueKind.Any:
						asText = AnyValue.Placeholder;
						return false;
					case ValueKind.Null:
					case ValueKind.Time:
					case ValueKind.Date:
					case ValueKind.DateTime:
					case ValueKind.DateTimeZone:
					case ValueKind.Duration:
					case ValueKind.Number:
					case ValueKind.Logical:
					case ValueKind.Text:
						return true;
					case ValueKind.Binary:
						asText = BinaryValue.Placeholder;
						return false;
					case ValueKind.List:
						asText = ListValue.Placeholder;
						return false;
					case ValueKind.Record:
						asText = RecordValue.Placeholder;
						return false;
					case ValueKind.Table:
						asText = TableValue.Placeholder;
						return false;
					case ValueKind.Function:
						asText = FunctionValue.Placeholder;
						return false;
					case ValueKind.Type:
						asText = TypeValue.Placeholder;
						return false;
					case ValueKind.Action:
						asText = ActionValue.Placeholder;
						return false;
					default:
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					}
				}

				// Token: 0x06005496 RID: 21654 RVA: 0x001225B8 File Offset: 0x001207B8
				private static bool AnyAreOptional(RecordValue fields)
				{
					for (int i = 0; i < fields.Count; i++)
					{
						if (fields[i].AsRecord["Optional"].AsBoolean)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x04002ECF RID: 11983
				private readonly FunctionValue displayNameFunctionValue;

				// Token: 0x04002ED0 RID: 11984
				private readonly int maxDepth;

				// Token: 0x04002ED1 RID: 11985
				private readonly HashSet<string> allowedTypeNames;

				// Token: 0x04002ED2 RID: 11986
				private readonly bool allowedOptionalFields;

				// Token: 0x02000C23 RID: 3107
				private sealed class DisplayNameFunctionValue : NativeFunctionValue1<Value, Value>
				{
					// Token: 0x06005497 RID: 21655 RVA: 0x001225F6 File Offset: 0x001207F6
					public DisplayNameFunctionValue(IEngineHost engineHost, bool unsupportedAsNull)
						: base(TypeValue.Any, 1, "value", TypeValue.Any)
					{
						this.textFrom = new Library.Text.FromFunctionValue(engineHost, null);
						this.unsupportedAsNull = unsupportedAsNull;
					}

					// Token: 0x06005498 RID: 21656 RVA: 0x00122624 File Offset: 0x00120824
					public override Value TypedInvoke(Value value)
					{
						TextValue textValue = null;
						Value value2;
						if (value.TryGetMetaField("Documentation.DisplayName", out value2) && ExcelModule.ShapeTableFunctionValue.TransformBuilder.AllowedType(value2.Kind, out textValue))
						{
							return this.textFrom.TypedInvoke(value2, Value.Null);
						}
						if (!this.unsupportedAsNull && textValue != null)
						{
							return textValue;
						}
						return Value.Null;
					}

					// Token: 0x04002ED3 RID: 11987
					private readonly Library.Text.FromFunctionValue textFrom;

					// Token: 0x04002ED4 RID: 11988
					private readonly bool unsupportedAsNull;
				}
			}

			// Token: 0x02000C24 RID: 3108
			private sealed class TransformBuilderDesktop : ExcelModule.ShapeTableFunctionValue.TransformBuilder
			{
				// Token: 0x06005499 RID: 21657 RVA: 0x00122675 File Offset: 0x00120875
				public TransformBuilderDesktop(IEngineHost engineHost, OptionsRecord optionsRecord)
					: base(engineHost, optionsRecord, true, true)
				{
				}

				// Token: 0x0600549A RID: 21658 RVA: 0x00122684 File Offset: 0x00120884
				public override Value ShapeTable(TableValue table)
				{
					FunctionValue functionValue;
					RecordTypeValue recordTypeValue = this.ComputeEntityType(table.Type.AsTableType.ItemType, 0, out functionValue);
					return LanguageLibrary.List.Transform.Invoke(TableModule.Table.ToRecords.Invoke(table), functionValue).AsList.ToTable(TableTypeValue.New(recordTypeValue));
				}

				// Token: 0x0600549B RID: 21659 RVA: 0x001226D4 File Offset: 0x001208D4
				private RecordTypeValue ComputeEntityType(RecordTypeValue recordType, int depth, out FunctionValue transform)
				{
					RecordBuilder recordBuilder = new RecordBuilder(recordType.Fields.Count);
					Dictionary<string, FunctionValue> dictionary = new Dictionary<string, FunctionValue>(recordType.Fields.Count);
					bool flag = false;
					ListValue listValue = null;
					for (int i = 0; i < recordType.Fields.Count; i++)
					{
						string text = recordType.Fields.Keys[i];
						TypeValue asType = recordType.Fields[i]["Type"].AsType;
						bool boolean = recordType.Fields[i]["Optional"].AsLogical.Boolean;
						FunctionValue functionValue;
						TypeValue typeValue = this.ComputeFieldType(asType, depth + 1, out functionValue);
						if (typeValue.TypeKind == ValueKind.None && asType.TypeKind == ValueKind.Any)
						{
							typeValue = asType.SubtractMetaValue.AsType;
						}
						if (typeValue.TypeKind != ValueKind.None)
						{
							if (text.Length == 0)
							{
								text = ExcelModule.ShapeTableFunctionValue.TransformBuilder.UniqueColumnName(recordType.FieldKeys);
								listValue = ListValue.New(new Value[]
								{
									TextValue.Empty,
									TextValue.New(text)
								});
							}
							recordBuilder.Add(text, RecordTypeAlgebra.NewField(typeValue, boolean), TypeValue.Record);
							dictionary.Add(text, functionValue);
							flag = flag || boolean;
						}
					}
					RecordValue recordValue = recordBuilder.ToRecord();
					Keys keys = (flag ? null : recordValue.Keys);
					transform = new ExcelModule.ShapeTableFunctionValue.TransformBuilderDesktop.ShapeExcelTypeFunctionValue((depth > 0) ? base.DisplayFunctionValue : null, dictionary, keys, listValue);
					return RecordTypeValue.New(recordValue);
				}

				// Token: 0x0600549C RID: 21660 RVA: 0x0012284C File Offset: 0x00120A4C
				private TypeValue ComputeFieldType(TypeValue type, int depth, out FunctionValue transform)
				{
					Value value;
					if (type.TypeKind == ValueKind.Record && base.IsEntityType(type.AsRecordType, out value))
					{
						if (depth > base.MaxDepth)
						{
							transform = base.DisplayFunctionValue;
							return NullableTypeValue.Text;
						}
						TypeValue typeValue = this.ComputeEntityType(type.AsRecordType, depth, out transform);
						if (type.IsNullable)
						{
							typeValue = typeValue.Nullable;
						}
						return typeValue;
					}
					else
					{
						transform = ExcelModule.ShapeTableFunctionValue.TransformBuilderDesktop.shapeSimpleValue;
						TextValue textValue;
						if (!ExcelModule.ShapeTableFunctionValue.TransformBuilder.AllowedType(type.TypeKind, out textValue))
						{
							return TypeValue.None;
						}
						return type.SubtractMetaValue.AsType;
					}
				}

				// Token: 0x0600549D RID: 21661 RVA: 0x001228D4 File Offset: 0x00120AD4
				private static Value RemoveTypeMetadata(Value value)
				{
					TypeValue type = value.Type;
					if (type.MetaValue.Count != 0)
					{
						return value.ReplaceType(type.SubtractMetaValue.AsType);
					}
					return value;
				}

				// Token: 0x04002ED5 RID: 11989
				public const string ApiVersion = "ExcelDesktop.0";

				// Token: 0x04002ED6 RID: 11990
				private static readonly Keys typeNameKeys = Keys.New("Documentation.TypeName");

				// Token: 0x04002ED7 RID: 11991
				private static readonly Keys displayNameKeys = Keys.New("Documentation.DisplayName");

				// Token: 0x04002ED8 RID: 11992
				private static readonly FunctionValue shapeSimpleValue = new ExcelModule.ShapeTableFunctionValue.TransformBuilderDesktop.SimpleValueFunctionValue();

				// Token: 0x02000C25 RID: 3109
				private sealed class ShapeExcelTypeFunctionValue : NativeFunctionValue1<Value, Value>
				{
					// Token: 0x0600549F RID: 21663 RVA: 0x00122932 File Offset: 0x00120B32
					public ShapeExcelTypeFunctionValue(FunctionValue displayNameFunctionValue, Dictionary<string, FunctionValue> fieldTransforms, Keys fixedKeys, ListValue renames)
						: base(NullableTypeValue.Record, 1, "value", NullableTypeValue.Record)
					{
						this.displayNameFunctionValue = displayNameFunctionValue;
						this.fieldTransforms = fieldTransforms;
						this.fixedKeys = fixedKeys;
						this.renames = renames;
					}

					// Token: 0x060054A0 RID: 21664 RVA: 0x00122968 File Offset: 0x00120B68
					public override Value TypedInvoke(Value value)
					{
						Value value2;
						if (value.IsNull)
						{
							value2 = value.SubtractMetaValue;
						}
						else
						{
							RecordValue record = value.AsRecord;
							if (this.renames != null)
							{
								record = Library.Record.RenameFields.Invoke(record, this.renames, Library.MissingField.Ignore).AsRecord;
							}
							Keys keys = this.fixedKeys;
							if (keys == null)
							{
								keys = Keys.New(record.Type.AsRecordType.FieldKeys.Where(new Func<string, bool>(this.fieldTransforms.ContainsKey)).ToArray<string>());
							}
							value2 = RecordValue.New(keys, (int i) => this.fieldTransforms[keys[i]].Invoke(record[keys[i]]));
						}
						if (this.displayNameFunctionValue != null)
						{
							value2 = value2.NewMeta(RecordValue.New(ExcelModule.ShapeTableFunctionValue.TransformBuilderDesktop.displayNameKeys, new Value[] { this.displayNameFunctionValue.Invoke(value) }));
						}
						return value2;
					}

					// Token: 0x04002ED9 RID: 11993
					private readonly FunctionValue displayNameFunctionValue;

					// Token: 0x04002EDA RID: 11994
					private readonly Dictionary<string, FunctionValue> fieldTransforms;

					// Token: 0x04002EDB RID: 11995
					private readonly Keys fixedKeys;

					// Token: 0x04002EDC RID: 11996
					private readonly ListValue renames;
				}

				// Token: 0x02000C27 RID: 3111
				private sealed class SimpleValueFunctionValue : NativeFunctionValue1<Value, Value>
				{
					// Token: 0x060054A3 RID: 21667 RVA: 0x000F3192 File Offset: 0x000F1392
					public SimpleValueFunctionValue()
						: base(TypeValue.Any, 1, "value", TypeValue.Any)
					{
					}

					// Token: 0x060054A4 RID: 21668 RVA: 0x00122AA4 File Offset: 0x00120CA4
					public override Value TypedInvoke(Value value)
					{
						TextValue textValue;
						if (!ExcelModule.ShapeTableFunctionValue.TransformBuilder.AllowedType(value.Kind, out textValue))
						{
							return textValue;
						}
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderDesktop.RemoveTypeMetadata(value.SubtractMetaValue);
					}
				}
			}

			// Token: 0x02000C28 RID: 3112
			private sealed class TransformBuilderXlo : ExcelModule.ShapeTableFunctionValue.TransformBuilder
			{
				// Token: 0x060054A5 RID: 21669 RVA: 0x00122AD0 File Offset: 0x00120CD0
				static TransformBuilderXlo()
				{
					for (int i = 0; i < 23; i++)
					{
						ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[i] = NumberValue.New(i);
					}
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.titleRecord = RecordValue.New(ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.valueKeys, new Value[]
					{
						TextValue.New("Title"),
						ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[1],
						LogicalValue.True
					});
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[1] = (ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[2] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => value.AsText));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[3] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue(delegate(Value value)
					{
						TimeSpan asClrTimeSpan = value.AsTime.AsClrTimeSpan;
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.TimeSpanToValue(asClrTimeSpan - new TimeSpan((long)asClrTimeSpan.Days * 864000000000L));
					});
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[4] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.DateTimeToValue(value.AsDate.AsClrDateTime));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[5] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.DateTimeToValue(value.AsDateTime.AsClrDateTime));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[6] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.DateTimeToValue(value.AsDateTimeZone.AsClrDateTimeOffset.DateTime));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[7] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.TimeSpanToValue(value.AsDuration.AsTimeSpan));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[8] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => value.AsLogical);
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[10] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => NumberValue.New((int)value.AsNumber.ToByte()));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[11] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => NumberValue.New((int)value.AsNumber.ToInt8()));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[12] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => NumberValue.New((int)value.AsNumber.ToInt16()));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[13] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => NumberValue.New(value.AsNumber.ToInt32()));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[14] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => NumberValue.New(value.AsNumber.ToInt64()));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[15] = (ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[16] = (ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[17] = (ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[18] = (ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[19] = (ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[22] = (ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[9] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => NumberValue.New(value.AsNumber.ToDouble()))))))));
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[0] = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue((Value value) => ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueAny(value));
				}

				// Token: 0x060054A6 RID: 21670 RVA: 0x00122D6D File Offset: 0x00120F6D
				public TransformBuilderXlo(IEngineHost engineHost, OptionsRecord optionsRecord)
					: base(engineHost, optionsRecord, false, false)
				{
				}

				// Token: 0x060054A7 RID: 21671 RVA: 0x00122D7C File Offset: 0x00120F7C
				public override Value ShapeTable(TableValue table)
				{
					FunctionValue functionValue;
					ListValue listValue = this.ComputeEntityType(table.Type.AsTableType.ItemType, 0, out functionValue);
					Value value = LanguageLibrary.List.Transform.Invoke(TableModule.Table.ToRecords.Invoke(table), functionValue);
					return RecordValue.New(ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.tableKeys, new Value[]
					{
						TextValue.New("ExcelOnline.0"),
						listValue,
						value.AsList
					});
				}

				// Token: 0x060054A8 RID: 21672 RVA: 0x00122DE4 File Offset: 0x00120FE4
				private ListValue ComputeEntityType(RecordTypeValue recordType, int depth, out FunctionValue transform)
				{
					List<RecordValue> list = new List<RecordValue>((depth == 0) ? recordType.Fields.Count : (recordType.Fields.Count + 1));
					List<string> list2 = new List<string>(recordType.Fields.Count);
					List<FunctionValue> list3 = new List<FunctionValue>(recordType.Fields.Count);
					if (depth > 0)
					{
						list.Add(ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.titleRecord);
					}
					for (int i = 0; i < recordType.Fields.Count; i++)
					{
						string text = recordType.Fields.Keys[i];
						TypeValue asType = recordType.Fields[i]["Type"].AsType;
						FunctionValue functionValue;
						ListValue listValue;
						ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType mashupOleDbColumnType = this.ComputeFieldType(asType, depth + 1, out functionValue, out listValue);
						if (mashupOleDbColumnType != ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Error)
						{
							list2.Add(text);
							list3.Add(functionValue);
							if (text.Length == 0)
							{
								text = ExcelModule.ShapeTableFunctionValue.TransformBuilder.UniqueColumnName(recordType.FieldKeys);
							}
							list.Add((listValue != null) ? RecordValue.New(ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.entityValueKeys, new Value[]
							{
								TextValue.New(text),
								ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[21],
								LogicalValue.True,
								listValue
							}) : RecordValue.New(ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.valueKeys, new Value[]
							{
								TextValue.New(text),
								ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[(int)mashupOleDbColumnType],
								LogicalValue.True
							}));
						}
					}
					transform = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ShapeExcelTypeFunctionValue((depth > 0) ? base.DisplayFunctionValue : null, list3, list2);
					Value[] array = list.ToArray();
					return ListValue.New(array);
				}

				// Token: 0x060054A9 RID: 21673 RVA: 0x00122F60 File Offset: 0x00121160
				private static Value DateTimeToValue(DateTime value)
				{
					double? num = OleDbConvert.SafeToOADate(value);
					if (num == null)
					{
						return Value.Null;
					}
					return NumberValue.New(num.Value);
				}

				// Token: 0x060054AA RID: 21674 RVA: 0x00122F8F File Offset: 0x0012118F
				private static Value TimeSpanToValue(TimeSpan value)
				{
					return NumberValue.New(OleDbConvert.GetVariantValue(value));
				}

				// Token: 0x060054AB RID: 21675 RVA: 0x00122F9C File Offset: 0x0012119C
				private ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType ComputeFieldType(TypeValue type, int depth, out FunctionValue transform, out ListValue subColumns)
				{
					subColumns = null;
					Value value;
					if (type.TypeKind == ValueKind.Record && base.IsEntityType(type.AsRecordType, out value))
					{
						if (depth > base.MaxDepth)
						{
							transform = base.DisplayFunctionValue;
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Text;
						}
						subColumns = this.ComputeEntityType(type.AsRecordType, depth, out transform);
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Entity;
					}
					else
					{
						TextValue textValue;
						if (type.TypeKind == ValueKind.Null || (type.TypeKind != ValueKind.Any && !ExcelModule.ShapeTableFunctionValue.TransformBuilder.AllowedType(type.TypeKind, out textValue)))
						{
							transform = null;
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Error;
						}
						ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType mashupTypeFromType = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.GetMashupTypeFromType(type.NonNullable);
						transform = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[(int)mashupTypeFromType];
						if (type.IsNullable)
						{
							transform = new ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.NullableFunctionValue(transform);
						}
						return mashupTypeFromType;
					}
				}

				// Token: 0x060054AC RID: 21676 RVA: 0x0012303C File Offset: 0x0012123C
				private static ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType GetMashupTypeFromType(TypeValue type)
				{
					switch (type.TypeKind)
					{
					case ValueKind.Any:
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Any;
					case ValueKind.Time:
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Time;
					case ValueKind.Date:
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Date;
					case ValueKind.DateTime:
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Datetime;
					case ValueKind.DateTimeZone:
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Datetimezone;
					case ValueKind.Duration:
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Duration;
					case ValueKind.Number:
						if (type.Equals(TypeValue.Int16))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Int16;
						}
						if (type.Equals(TypeValue.Int32))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Int32;
						}
						if (type.Equals(TypeValue.Int64))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Int64;
						}
						if (type.Equals(TypeValue.Int8))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Int8;
						}
						if (type.Equals(TypeValue.Byte))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Byte;
						}
						if (type.Equals(TypeValue.Single))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Single;
						}
						if (type.Equals(TypeValue.Decimal))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Decimal;
						}
						if (type.Equals(TypeValue.Double))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Double;
						}
						if (type.Equals(TypeValue.Currency))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Currency;
						}
						if (type.Equals(TypeValue.Percentage))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Percentage;
						}
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Number;
					case ValueKind.Logical:
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Logical;
					case ValueKind.Text:
						if (type.Equals(TypeValue.Character))
						{
							return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Character;
						}
						return ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Text;
					}
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}

				// Token: 0x060054AD RID: 21677 RVA: 0x00123158 File Offset: 0x00121358
				private static Value ConvertValueAny(Value value)
				{
					if (value.IsNull)
					{
						return value.As(TypeValue.Any);
					}
					TextValue textValue;
					if (!ExcelModule.ShapeTableFunctionValue.TransformBuilder.AllowedType(value.Kind, out textValue))
					{
						return ListValue.New(new Value[]
						{
							ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[1],
							textValue
						});
					}
					ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType mashupOleDbColumnType;
					switch (value.Kind)
					{
					case ValueKind.Time:
						mashupOleDbColumnType = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Time;
						break;
					case ValueKind.Date:
						mashupOleDbColumnType = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Date;
						break;
					case ValueKind.DateTime:
						mashupOleDbColumnType = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Datetime;
						break;
					case ValueKind.DateTimeZone:
						mashupOleDbColumnType = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Datetimezone;
						break;
					case ValueKind.Duration:
						mashupOleDbColumnType = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.MashupOleDbColumnType.Duration;
						break;
					case ValueKind.Number:
						switch (value.AsNumber.NumberKind)
						{
						case Microsoft.Mashup.Engine1.Runtime.NumberKind.Int32:
							return ListValue.New(new Value[]
							{
								ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[13],
								value
							});
						case Microsoft.Mashup.Engine1.Runtime.NumberKind.Double:
							return ListValue.New(new Value[]
							{
								ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[16],
								value
							});
						case Microsoft.Mashup.Engine1.Runtime.NumberKind.Decimal:
							return ListValue.New(new Value[]
							{
								ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[17],
								NumberValue.New(value.AsNumber.ToDouble())
							});
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						break;
					case ValueKind.Logical:
						return ListValue.New(new Value[]
						{
							ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[8],
							value
						});
					case ValueKind.Text:
						return ListValue.New(new Value[]
						{
							ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[1],
							value
						});
					default:
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					}
					Value value2 = ((ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ConvertValueFunctionValue)ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.conversions[(int)mashupOleDbColumnType]).Function(value);
					if (!value2.IsNull)
					{
						return ListValue.New(new Value[]
						{
							ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[(int)mashupOleDbColumnType],
							value2
						});
					}
					return value2;
				}

				// Token: 0x04002EE0 RID: 12000
				public const string ApiVersion = "ExcelOnline.0";

				// Token: 0x04002EE1 RID: 12001
				private static readonly FunctionValue[] conversions = new FunctionValue[23];

				// Token: 0x04002EE2 RID: 12002
				private static readonly NumberValue[] types = new NumberValue[23];

				// Token: 0x04002EE3 RID: 12003
				private static readonly Keys entityValueKeys = Keys.New("Name", "Type", "IsNullable", "ColumnsInfo");

				// Token: 0x04002EE4 RID: 12004
				private static readonly Keys valueKeys = Keys.New("Name", "Type", "IsNullable");

				// Token: 0x04002EE5 RID: 12005
				private static readonly Keys tableKeys = Keys.New("ApiVersion", "ColumnsInfo", "Rows");

				// Token: 0x04002EE6 RID: 12006
				private static readonly RecordValue titleRecord;

				// Token: 0x02000C29 RID: 3113
				private enum MashupOleDbColumnType
				{
					// Token: 0x04002EE8 RID: 12008
					Error = -1,
					// Token: 0x04002EE9 RID: 12009
					Any,
					// Token: 0x04002EEA RID: 12010
					Text,
					// Token: 0x04002EEB RID: 12011
					Character,
					// Token: 0x04002EEC RID: 12012
					Time,
					// Token: 0x04002EED RID: 12013
					Date,
					// Token: 0x04002EEE RID: 12014
					Datetime,
					// Token: 0x04002EEF RID: 12015
					Datetimezone,
					// Token: 0x04002EF0 RID: 12016
					Duration,
					// Token: 0x04002EF1 RID: 12017
					Logical,
					// Token: 0x04002EF2 RID: 12018
					Number,
					// Token: 0x04002EF3 RID: 12019
					Byte,
					// Token: 0x04002EF4 RID: 12020
					Int8,
					// Token: 0x04002EF5 RID: 12021
					Int16,
					// Token: 0x04002EF6 RID: 12022
					Int32,
					// Token: 0x04002EF7 RID: 12023
					Int64,
					// Token: 0x04002EF8 RID: 12024
					Single,
					// Token: 0x04002EF9 RID: 12025
					Double,
					// Token: 0x04002EFA RID: 12026
					Decimal,
					// Token: 0x04002EFB RID: 12027
					Currency,
					// Token: 0x04002EFC RID: 12028
					Percentage,
					// Token: 0x04002EFD RID: 12029
					Unknown,
					// Token: 0x04002EFE RID: 12030
					Entity,
					// Token: 0x04002EFF RID: 12031
					Numeric,
					// Token: 0x04002F00 RID: 12032
					Max
				}

				// Token: 0x02000C2A RID: 3114
				private sealed class ShapeExcelTypeFunctionValue : NativeFunctionValue1<Value, Value>
				{
					// Token: 0x060054AE RID: 21678 RVA: 0x0012330A File Offset: 0x0012150A
					public ShapeExcelTypeFunctionValue(FunctionValue displayNameFunctionValue, List<FunctionValue> transforms, List<string> keys)
						: base(NullableTypeValue.List, 1, "value", NullableTypeValue.Record)
					{
						this.displayNameFunctionValue = displayNameFunctionValue;
						this.transforms = transforms;
						this.keys = keys;
					}

					// Token: 0x060054AF RID: 21679 RVA: 0x00123338 File Offset: 0x00121538
					public override Value TypedInvoke(Value value)
					{
						if (value.IsNull)
						{
							return value.SubtractMetaValue;
						}
						RecordValue asRecord = value.AsRecord;
						if (this.displayNameFunctionValue != null)
						{
							int count = this.keys.Count;
							IValueReference[] array = new IValueReference[count + 2];
							array[0] = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.types[21];
							array[1] = this.displayNameFunctionValue.Invoke(value);
							for (int i = 0; i < count; i++)
							{
								array[i + 2] = this.transforms[i].Invoke(asRecord[this.keys[i]]);
							}
							return ListValue.New(array);
						}
						int count2 = this.keys.Count;
						IValueReference[] array2 = new IValueReference[count2];
						for (int j = 0; j < count2; j++)
						{
							try
							{
								array2[j] = this.transforms[j].Invoke(asRecord[this.keys[j]]);
							}
							catch (ValueException)
							{
								array2[j] = ExcelModule.ShapeTableFunctionValue.TransformBuilderXlo.ShapeExcelTypeFunctionValue.errorValue;
							}
						}
						return ListValue.New(array2);
					}

					// Token: 0x04002F01 RID: 12033
					private static readonly Value errorValue = ListValue.New(new Value[] { NumberValue.New(-1) });

					// Token: 0x04002F02 RID: 12034
					private readonly FunctionValue displayNameFunctionValue;

					// Token: 0x04002F03 RID: 12035
					private readonly List<FunctionValue> transforms;

					// Token: 0x04002F04 RID: 12036
					private readonly List<string> keys;
				}

				// Token: 0x02000C2B RID: 3115
				private sealed class ConvertValueFunctionValue : NativeFunctionValue1<Value, Value>
				{
					// Token: 0x060054B1 RID: 21681 RVA: 0x00123463 File Offset: 0x00121663
					public ConvertValueFunctionValue(Func<Value, Value> convert)
						: base(TypeValue.Any, 1, "value", TypeValue.Any)
					{
						this.convert = convert;
					}

					// Token: 0x170019D0 RID: 6608
					// (get) Token: 0x060054B2 RID: 21682 RVA: 0x00123482 File Offset: 0x00121682
					public Func<Value, Value> Function
					{
						get
						{
							return this.convert;
						}
					}

					// Token: 0x060054B3 RID: 21683 RVA: 0x0012348A File Offset: 0x0012168A
					public override Value TypedInvoke(Value value)
					{
						return this.convert(value);
					}

					// Token: 0x04002F05 RID: 12037
					private readonly Func<Value, Value> convert;
				}

				// Token: 0x02000C2C RID: 3116
				private sealed class NullableFunctionValue : NativeFunctionValue1<Value, Value>
				{
					// Token: 0x060054B4 RID: 21684 RVA: 0x00123498 File Offset: 0x00121698
					public NullableFunctionValue(FunctionValue function)
						: base(TypeValue.Any, 1, "value", TypeValue.Any)
					{
						this.function = function;
					}

					// Token: 0x060054B5 RID: 21685 RVA: 0x001234B7 File Offset: 0x001216B7
					public override Value TypedInvoke(Value value)
					{
						if (!value.IsNull)
						{
							return this.function.Invoke(value);
						}
						return Value.Null;
					}

					// Token: 0x04002F06 RID: 12038
					private readonly FunctionValue function;
				}
			}
		}
	}
}
