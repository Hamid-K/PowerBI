using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000051 RID: 81
	internal static class Util
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		private static Func<IValue, string> JsonWriter
		{
			get
			{
				if (Util.jsonWriter == null)
				{
					object obj = Util.syncRoot;
					lock (obj)
					{
						if (Util.jsonWriter == null)
						{
							IEngine engine = MashupEngines.Version1;
							IEngineHost engineHost = MashupProviderFactory.MakeEngineHost();
							IValue jsonFromValue = engine.Function(engineHost, FunctionHandle.JsonFromValue);
							IValue textFromBinary = engine.Function(engineHost, FunctionHandle.TextFromBinary);
							Util.jsonWriter = (IValue v) => engine.Invoke(textFromBinary, new IValue[] { engine.Invoke(jsonFromValue, new IValue[] { v }) }).AsString;
						}
					}
				}
				return Util.jsonWriter;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000E744 File Offset: 0x0000C944
		private static Func<string, IValue> JsonParser
		{
			get
			{
				if (Util.jsonParser == null)
				{
					object obj = Util.syncRoot;
					lock (obj)
					{
						if (Util.jsonParser == null)
						{
							IEngine engine = MashupEngines.Version1;
							IEngineHost engineHost = MashupProviderFactory.MakeEngineHost();
							IValue jsonDocument = engine.GetLibrary(engineHost, null)["Json.Document"];
							Util.jsonParser = (string v) => engine.Invoke(jsonDocument, new IValue[] { engine.Text(v) });
						}
					}
				}
				return Util.jsonParser;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		public static bool IsSet(CommandBehavior commandBehavior, CommandBehavior flag)
		{
			return (commandBehavior & flag) == flag;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public static bool IsSet(MashupCommandBehavior commandBehavior, MashupCommandBehavior flag)
		{
			return (commandBehavior & flag) == flag;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		public static bool AreEqual<TKey, TValue>(IDictionary<TKey, TValue> dictionary1, IDictionary<TKey, TValue> dictionary2)
		{
			if (dictionary1 == dictionary2)
			{
				return true;
			}
			if (dictionary1.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary1)
			{
				TValue tvalue;
				if (!dictionary2.TryGetValue(keyValuePair.Key, out tvalue))
				{
					return false;
				}
				if (keyValuePair.Value != null || tvalue != null)
				{
					if (keyValuePair.Value == null || tvalue == null)
					{
						return false;
					}
					TValue value = keyValuePair.Value;
					if (!value.Equals(tvalue))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		public static bool TryGetRecord(IExpression expression, out IRecordValue record)
		{
			ExpressionKind kind = expression.Kind;
			if (kind != ExpressionKind.Constant)
			{
				if (kind == ExpressionKind.Record)
				{
					IRecordExpression recordExpression = (IRecordExpression)expression;
					IEngine version = MashupEngines.Version1;
					List<string> list = new List<string>(recordExpression.Members.Count);
					List<IValue> list2 = new List<IValue>(recordExpression.Members.Count);
					foreach (VariableInitializer variableInitializer in recordExpression.Members)
					{
						IValue value = null;
						ExpressionKind kind2 = variableInitializer.Value.Kind;
						if (kind2 != ExpressionKind.Constant)
						{
							if (kind2 == ExpressionKind.Record)
							{
								IRecordValue recordValue;
								if (Util.TryGetRecord(variableInitializer.Value, out recordValue))
								{
									value = recordValue;
								}
							}
						}
						else
						{
							value = ((IConstantExpression2)variableInitializer.Value).Value;
						}
						if (value != null)
						{
							list.Add(variableInitializer.Name);
							list2.Add(value);
						}
					}
					record = version.Record(version.Keys(list.ToArray()), list2.ToArray());
					return true;
				}
			}
			else
			{
				IValue value = ((IConstantExpression2)expression).Value;
				if (value.IsRecord)
				{
					record = value.AsRecord;
					return true;
				}
			}
			record = null;
			return false;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public static DataSource[] DeserializeDataSourcesArray(SerializationInfo info)
		{
			int @int = info.GetInt32("DataSourcesCount");
			DataSource[] array = new DataSource[@int];
			for (int i = 0; i < @int; i++)
			{
				string @string = info.GetString(Util.GenerateKey("DataSourceKind", i));
				string string2 = info.GetString(Util.GenerateKey("DataSourcePath", i));
				array[i] = ((@string != null && string2 != null) ? new DataSource(@string, string2) : null);
			}
			return array;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000EA44 File Offset: 0x0000CC44
		public static void Serialize(SerializationInfo info, DataSource[] dataSources)
		{
			info.AddValue("DataSourcesCount", dataSources.Length);
			for (int i = 0; i < dataSources.Length; i++)
			{
				info.AddValue(Util.GenerateKey("DataSourceKind", i), (dataSources[i] == null) ? null : dataSources[i].Kind);
				info.AddValue(Util.GenerateKey("DataSourcePath", i), (dataSources[i] == null) ? null : dataSources[i].Path);
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000EAAF File Offset: 0x0000CCAF
		private static string GenerateKey(string prefix, int index)
		{
			return prefix + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000EAC3 File Offset: 0x0000CCC3
		public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> list, T item)
		{
			return list.Concat(new T[] { item });
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000EAD9 File Offset: 0x0000CCD9
		public static IDocument ParseText(this IEngine engine, SegmentedString text)
		{
			return engine.Parse(engine.Tokenize(text), new TextDocumentHost(text), new Action<IError>(Util.ThrowParseError));
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000EAFC File Offset: 0x0000CCFC
		public static ISectionDocument[] ParseSectionDocuments(this IEngine engine, string text)
		{
			IDocument[] array = engine.ParseMany(engine.Tokenize(text), new TextDocumentHost(text), new Action<IError>(Util.ThrowParseError));
			ISectionDocument[] array2 = new ISectionDocument[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				ISectionDocument sectionDocument = array[i] as ISectionDocument;
				if (sectionDocument == null)
				{
					throw new NotSupportedException(ProviderErrorStrings.SectionOnlySupported);
				}
				array2[i] = sectionDocument;
			}
			return array2;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000EB5C File Offset: 0x0000CD5C
		public static void ThrowParseError(IError error)
		{
			throw new MashupExpressionException(ProviderErrorStrings.InvalidSyntax(error.Message, error.Location.Range.Start, error.Location.Range.End));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000EBA9 File Offset: 0x0000CDA9
		public static void SwallowError(IError error)
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		public static string NormalizeToKnownValue(string[] knownValues, string value)
		{
			string text;
			if (Util.TryNormalizeToKnownValue(knownValues, value, out text))
			{
				return text;
			}
			return value;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		public static bool TryNormalizeToKnownValue(string[] knownValues, string value, out string normalizedValue)
		{
			normalizedValue = knownValues.FirstOrDefault((string k) => k.Equals(value, StringComparison.OrdinalIgnoreCase));
			return normalizedValue != null;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000EBFB File Offset: 0x0000CDFB
		public static DataSource[] ToDataSources(IEnumerable<IResource> dataSources)
		{
			return dataSources.Select((IResource resource) => new DataSource(resource)).ToArray<DataSource>();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000EC27 File Offset: 0x0000CE27
		public static string ToJsonText(IValue value)
		{
			return Util.JsonWriter(value);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public static bool TryConvertJson(IValue value, out string json)
		{
			bool flag;
			try
			{
				json = Util.ToJsonText(value);
				flag = true;
			}
			catch (ValueException2)
			{
				json = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000EC68 File Offset: 0x0000CE68
		public static bool TryParseJson(string json, out IValue value)
		{
			bool flag;
			try
			{
				value = Util.JsonParser(json);
				flag = true;
			}
			catch (ValueException2)
			{
				value = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		public static bool TryConvertJsonToM(string json, out string m)
		{
			bool flag;
			try
			{
				ISourceBuilder sourceBuilder = MashupEngines.Version1.CreateSourceBuilder();
				Util.AddValue(sourceBuilder, Util.JsonParser(json));
				m = sourceBuilder.ToSource();
				flag = true;
			}
			catch (ValueException2)
			{
				m = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		private static void AddValue(ISourceBuilder builder, IValue value)
		{
			if (value.IsList)
			{
				Util.AddListValue(builder, value.AsList);
				return;
			}
			if (value.IsRecord)
			{
				Util.AddRecordValue(builder, value.AsRecord);
				return;
			}
			builder.Primitive(value);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000ED24 File Offset: 0x0000CF24
		private static void AddListValue(ISourceBuilder builder, IListValue list)
		{
			builder.BeginList();
			foreach (IValueReference2 valueReference in list)
			{
				Util.AddValue(builder, valueReference.Value);
			}
			builder.EndList();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000ED80 File Offset: 0x0000CF80
		private static void AddRecordValue(ISourceBuilder builder, IRecordValue record)
		{
			builder.BeginRecord();
			for (int i = 0; i < record.Keys.Length; i++)
			{
				builder.AddField(record.Keys[i]);
				Util.AddValue(builder, record[i]);
			}
			builder.EndRecord();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		public static byte[] SerializeDataTable(DataTable dataTable)
		{
			if (dataTable == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			IDataReader dataReader = dataTable.CreateDataReader();
			OledbDataWriter oledbDataWriter = new OledbDataWriter(memoryStream, TableSchema.FromDataReader(dataReader), null);
			object[] array = new object[dataTable.Columns.Count];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					array[j] = Util.MapObject(-1, dataTable.Rows[i][j]);
				}
				oledbDataWriter.WriteRecord(array);
			}
			oledbDataWriter.WriteEnd();
			oledbDataWriter.Flush();
			return memoryStream.ToArray();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000EE88 File Offset: 0x0000D088
		public static object[] MapParameters(DbParameterCollection parameters)
		{
			object[] array = null;
			if (parameters != null)
			{
				array = new object[parameters.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Util.MapObject(i, parameters[i].Value);
				}
			}
			return array;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000EECC File Offset: 0x0000D0CC
		private static object MapObject(int parameter, object obj)
		{
			ProviderContext instance = AdoNetProviderContext.Instance;
			MashupResource.ParameterErrors errors = new MashupResource.ParameterErrors(instance, parameter);
			if (obj is Date)
			{
				return new Date(((Date)obj).DateTime);
			}
			if (obj is Time)
			{
				return new Time(((Time)obj).TimeSpan);
			}
			if (obj is IDataReader)
			{
				IDataReader dataReader = new NonDisposableDataReader((IDataReader)obj);
				obj = new Func<IDataReader>(() => dataReader);
			}
			if (obj is Stream)
			{
				Stream stream = new NonDisposableStream((Stream)obj);
				obj = new Func<Stream>(() => stream);
			}
			Delegate @delegate = obj as Delegate;
			if (@delegate != null && @delegate.Method.GetParameters().Length == 0 && @delegate.Method.ReturnType != null)
			{
				if (typeof(IDataReader).IsAssignableFrom(@delegate.Method.ReturnType))
				{
					Func<IDataReader> func = (Func<IDataReader>)@delegate;
					Func<IDataReader> getUniqueDataReader = new Func<IDataReader>(new EnsureUniqueGetValue<IDataReader>(func).GetValue);
					Func<IPageReader> getPageReader = () => new DataReaderPageReader(getUniqueDataReader(), new DataReaderPageReader.ExceptionPropertyGetter(MashupResource.TryGetPropertiesFromException));
					Func<IPageReader> getErrorPageReader = () => new ErrorTranslatingPageReader(getPageReader(), new Func<Exception, Exception>(errors.TranslateException));
					Func<IPageReader> func2 = () => MashupTypesPageReader.New(getErrorPageReader());
					return errors.TranslateExceptions<IPageReader>(func2);
				}
				if (typeof(Stream).IsAssignableFrom(@delegate.Method.ReturnType))
				{
					Func<Stream> func3 = (Func<Stream>)@delegate;
					Func<Stream> getUniqueStream = new Func<Stream>(new EnsureUniqueGetValue<Stream>(func3).GetValue);
					Func<Stream> func4 = () => new ErrorTranslatingStream(getUniqueStream(), new Func<Exception, Exception>(errors.TranslateException));
					return errors.TranslateExceptions<Stream>(func4);
				}
			}
			return obj;
		}

		// Token: 0x040001DC RID: 476
		private const string DataSourcesCountKey = "DataSourcesCount";

		// Token: 0x040001DD RID: 477
		private const string DataSourceKindPrefix = "DataSourceKind";

		// Token: 0x040001DE RID: 478
		private const string DataSourcePathPrefix = "DataSourcePath";

		// Token: 0x040001DF RID: 479
		private static object syncRoot = new object();

		// Token: 0x040001E0 RID: 480
		private static Func<IValue, string> jsonWriter;

		// Token: 0x040001E1 RID: 481
		private static Func<string, IValue> jsonParser;
	}
}
