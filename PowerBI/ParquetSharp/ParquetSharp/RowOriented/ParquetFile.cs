using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using ParquetSharp.IO;

namespace ParquetSharp.RowOriented
{
	// Token: 0x0200009E RID: 158
	[NullableContext(1)]
	[Nullable(0)]
	public static class ParquetFile
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x000100FC File Offset: 0x0000E2FC
		public static ParquetRowReader<TTuple> CreateRowReader<[Nullable(2)] TTuple>(string path)
		{
			MappedField[] fieldsAndProperties = ParquetFile.GetFieldsAndProperties(typeof(TTuple));
			ParquetRowReader<TTuple>.ReadAction orCreateReadDelegate = ParquetFile.GetOrCreateReadDelegate<TTuple>(fieldsAndProperties);
			return new ParquetRowReader<TTuple>(path, orCreateReadDelegate, fieldsAndProperties);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001012C File Offset: 0x0000E32C
		public static ParquetRowReader<TTuple> CreateRowReader<[Nullable(2)] TTuple>(string path, ReaderProperties readerProperties)
		{
			MappedField[] fieldsAndProperties = ParquetFile.GetFieldsAndProperties(typeof(TTuple));
			ParquetRowReader<TTuple>.ReadAction orCreateReadDelegate = ParquetFile.GetOrCreateReadDelegate<TTuple>(fieldsAndProperties);
			return new ParquetRowReader<TTuple>(path, readerProperties, orCreateReadDelegate, fieldsAndProperties);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00010160 File Offset: 0x0000E360
		public static ParquetRowReader<TTuple> CreateRowReader<[Nullable(2)] TTuple>(string path, bool memoryMap)
		{
			MappedField[] fieldsAndProperties = ParquetFile.GetFieldsAndProperties(typeof(TTuple));
			ParquetRowReader<TTuple>.ReadAction orCreateReadDelegate = ParquetFile.GetOrCreateReadDelegate<TTuple>(fieldsAndProperties);
			return new ParquetRowReader<TTuple>(path, memoryMap, orCreateReadDelegate, fieldsAndProperties);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00010194 File Offset: 0x0000E394
		public static ParquetRowReader<TTuple> CreateRowReader<[Nullable(2)] TTuple>(string path, bool memoryMap, ReaderProperties readerProperties)
		{
			MappedField[] fieldsAndProperties = ParquetFile.GetFieldsAndProperties(typeof(TTuple));
			ParquetRowReader<TTuple>.ReadAction orCreateReadDelegate = ParquetFile.GetOrCreateReadDelegate<TTuple>(fieldsAndProperties);
			return new ParquetRowReader<TTuple>(path, memoryMap, readerProperties, orCreateReadDelegate, fieldsAndProperties);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000101C8 File Offset: 0x0000E3C8
		public static ParquetRowReader<TTuple> CreateRowReader<[Nullable(2)] TTuple>(RandomAccessFile randomAccessFile)
		{
			MappedField[] fieldsAndProperties = ParquetFile.GetFieldsAndProperties(typeof(TTuple));
			ParquetRowReader<TTuple>.ReadAction orCreateReadDelegate = ParquetFile.GetOrCreateReadDelegate<TTuple>(fieldsAndProperties);
			return new ParquetRowReader<TTuple>(randomAccessFile, orCreateReadDelegate, fieldsAndProperties);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000101F8 File Offset: 0x0000E3F8
		public static ParquetRowReader<TTuple> CreateRowReader<[Nullable(2)] TTuple>(RandomAccessFile randomAccessFile, ReaderProperties readerProperties)
		{
			MappedField[] fieldsAndProperties = ParquetFile.GetFieldsAndProperties(typeof(TTuple));
			ParquetRowReader<TTuple>.ReadAction orCreateReadDelegate = ParquetFile.GetOrCreateReadDelegate<TTuple>(fieldsAndProperties);
			return new ParquetRowReader<TTuple>(randomAccessFile, readerProperties, orCreateReadDelegate, fieldsAndProperties);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001022C File Offset: 0x0000E42C
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(string path, [Nullable(new byte[] { 2, 1 })] string[] columnNames = null, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columnNames);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(path, item, compression, keyValueMetadata, item2);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001025C File Offset: 0x0000E45C
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(string path, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1 })] string[] columnNames = null, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columnNames);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(path, item, writerProperties, keyValueMetadata, item2);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001028C File Offset: 0x0000E48C
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(OutputStream outputStream, [Nullable(new byte[] { 2, 1 })] string[] columnNames = null, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columnNames);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(outputStream, item, compression, keyValueMetadata, item2);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000102BC File Offset: 0x0000E4BC
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(OutputStream outputStream, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1 })] string[] columnNames = null, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columnNames);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(outputStream, item, writerProperties, keyValueMetadata, item2);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000102EC File Offset: 0x0000E4EC
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(string path, Column[] columns, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columns);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(path, item, compression, keyValueMetadata, item2);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001031C File Offset: 0x0000E51C
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(string path, WriterProperties writerProperties, Column[] columns, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columns);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(path, item, writerProperties, keyValueMetadata, item2);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001034C File Offset: 0x0000E54C
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(OutputStream outputStream, Column[] columns, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columns);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(outputStream, item, compression, keyValueMetadata, item2);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001037C File Offset: 0x0000E57C
		public static ParquetRowWriter<TTuple> CreateRowWriter<[Nullable(2)] TTuple>(OutputStream outputStream, WriterProperties writerProperties, Column[] columns, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> orCreateWriteDelegate = ParquetFile.GetOrCreateWriteDelegate<TTuple>(columns);
			Column[] item = orCreateWriteDelegate.Item1;
			ParquetRowWriter<TTuple>.WriteAction item2 = orCreateWriteDelegate.Item2;
			return new ParquetRowWriter<TTuple>(outputStream, item, writerProperties, keyValueMetadata, item2);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000103AC File Offset: 0x0000E5AC
		private static ParquetRowReader<TTuple>.ReadAction GetOrCreateReadDelegate<[Nullable(2)] TTuple>(MappedField[] fields)
		{
			return (ParquetRowReader<TTuple>.ReadAction)ParquetFile.ReadDelegatesCache.GetOrAdd(typeof(TTuple), (Type k) => ParquetFile.CreateReadDelegate<TTuple>(fields));
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000103F0 File Offset: 0x0000E5F0
		[NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "columns", "writeDelegate" })]
		[return: Nullable(new byte[] { 0, 1, 1, 1, 1 })]
		private static global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> GetOrCreateWriteDelegate<TTuple>([Nullable(new byte[] { 2, 1 })] string[] columnNames)
		{
			global::System.ValueTuple<MappedField[], Delegate> orAdd = ParquetFile.WriteDelegates.GetOrAdd(typeof(TTuple), delegate(Type k)
			{
				global::System.ValueTuple<MappedField[], ParquetRowWriter<TTuple>.WriteAction> valueTuple = ParquetFile.CreateWriteDelegate<TTuple>();
				return new global::System.ValueTuple<MappedField[], Delegate>(valueTuple.Item1, valueTuple.Item2);
			});
			MappedField[] item = orAdd.Item1;
			Delegate item2 = orAdd.Item2;
			Column[] array = item.Select(new Func<MappedField, Column>(ParquetFile.GetColumn)).ToArray<Column>();
			if (columnNames != null)
			{
				if (columnNames.Length != array.Length)
				{
					throw new ArgumentException(string.Format("The length of column names ({0}) does not mach the number of ", columnNames.Length) + string.Format("public fields and properties ({0})", array.Length), "columnNames");
				}
				array = array.Select((Column c, int i) => new Column(c.LogicalSystemType, columnNames[i], c.LogicalTypeOverride, c.Length)).ToArray<Column>();
			}
			return new global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction>(array, (ParquetRowWriter<TTuple>.WriteAction)item2);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000104E4 File Offset: 0x0000E6E4
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "columns", "writeDelegate" })]
		[return: Nullable(new byte[] { 0, 1, 1, 1, 1 })]
		private static global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction> GetOrCreateWriteDelegate<[Nullable(2)] TTuple>(Column[] columns)
		{
			global::System.ValueTuple<MappedField[], Delegate> orAdd = ParquetFile.WriteDelegates.GetOrAdd(typeof(TTuple), delegate(Type k)
			{
				global::System.ValueTuple<MappedField[], ParquetRowWriter<TTuple>.WriteAction> valueTuple = ParquetFile.CreateWriteDelegate<TTuple>();
				return new global::System.ValueTuple<MappedField[], Delegate>(valueTuple.Item1, valueTuple.Item2);
			});
			MappedField[] item = orAdd.Item1;
			Delegate item2 = orAdd.Item2;
			if (columns.Length != item.Length)
			{
				throw new ArgumentException(string.Format("The number of columns specified ({0}) does not mach the number of public ", columns.Length) + string.Format("fields and properties ({0})", item.Length), "columns");
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i].LogicalSystemType != item[i].Type)
				{
					throw new ArgumentException(string.Format("Expected a system type of '{0}' for column {1} ({2}) ", item[i].Type, i, columns[i].Name) + string.Format("but received '{0}'", columns[i].LogicalSystemType), "columns");
				}
			}
			return new global::System.ValueTuple<Column[], ParquetRowWriter<TTuple>.WriteAction>(columns, (ParquetRowWriter<TTuple>.WriteAction)item2);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00010604 File Offset: 0x0000E804
		private static ParquetRowReader<TTuple>.ReadAction CreateReadDelegate<[Nullable(2)] TTuple>(MappedField[] fields)
		{
			ParameterExpression reader = Expression.Parameter(typeof(ParquetRowReader<TTuple>), "reader");
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TTuple[]), "tuples");
			ParameterExpression length = Expression.Parameter(typeof(int), "length");
			ConstructorInfo constructorInfo = typeof(TTuple).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, fields.Select((MappedField f) => f.Type).ToArray<Type>(), null);
			if (constructorInfo == null && !ParquetFile.IsMemberInitializable(typeof(TTuple), fields))
			{
				constructorInfo = typeof(TTuple).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, fields.Select((MappedField f) => f.Type).ToArray<Type>(), null);
			}
			ParameterExpression[] buffers = fields.Select((MappedField f) => Expression.Variable(f.Type.MakeArrayType(), "buffer_" + f.Name)).ToArray<ParameterExpression>();
			Expression[] array = fields.Select((MappedField f, int i) => Expression.Assign(buffers[i], Expression.NewArrayBounds(f.Type, new Expression[] { length }))).ToArray<Expression>();
			MethodCallExpression[] array2 = buffers.Select((ParameterExpression buffer, int i) => Expression.Call(reader, ParquetFile.GetReadMethod<TTuple>(fields[i].Type), Expression.Constant(i), buffer, length)).ToArray<MethodCallExpression>();
			ParameterExpression index = Expression.Variable(typeof(int), "index");
			Expression expression = ParquetFile.For(index, Expression.Constant(0), Expression.NotEqual(index, length), Expression.PreIncrementAssign(index), Expression.Assign(Expression.ArrayAccess(parameterExpression, new Expression[] { index }), (constructorInfo == null) ? Expression.MemberInit(Expression.New(typeof(TTuple)), fields.Select((MappedField f, int i) => Expression.Bind(f.Info, Expression.ArrayAccess(buffers[i], new Expression[] { index })))) : Expression.New(constructorInfo, fields.Select((MappedField f, int i) => Expression.ArrayAccess(buffers[i], new Expression[] { index })))));
			Expression<ParquetRowReader<TTuple>.ReadAction> expression2 = Expression.Lambda<ParquetRowReader<TTuple>.ReadAction>(Expression.Block(buffers, array.Concat(array2).Concat(new Expression[] { expression })), new ParameterExpression[] { reader, parameterExpression, length });
			Action<Expression> onReadExpressionCreated = ParquetFile.OnReadExpressionCreated;
			if (onReadExpressionCreated != null)
			{
				onReadExpressionCreated(expression2);
			}
			return expression2.Compile();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000108B8 File Offset: 0x0000EAB8
		[NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "fields", "writeDelegate" })]
		[return: Nullable(new byte[] { 0, 1, 1, 1 })]
		private static global::System.ValueTuple<MappedField[], ParquetRowWriter<TTuple>.WriteAction> CreateWriteDelegate<TTuple>()
		{
			MappedField[] fieldsAndProperties = ParquetFile.GetFieldsAndProperties(typeof(TTuple));
			ParameterExpression writer = Expression.Parameter(typeof(ParquetRowWriter<TTuple>), "writer");
			ParameterExpression tuples = Expression.Parameter(typeof(TTuple[]), "tuples");
			ParameterExpression length = Expression.Parameter(typeof(int), "length");
			Expression<ParquetRowWriter<TTuple>.WriteAction> expression = Expression.Lambda<ParquetRowWriter<TTuple>.WriteAction>(Expression.Block(fieldsAndProperties.Select(delegate(MappedField f)
			{
				Type type = f.Type.MakeArrayType();
				ParameterExpression parameterExpression = Expression.Variable(type, "buffer_" + f.Name);
				BinaryExpression binaryExpression = Expression.Assign(parameterExpression, Expression.NewArrayBounds(f.Type, new Expression[] { length }));
				BinaryExpression binaryExpression2 = Expression.Assign(parameterExpression, Expression.Constant(null, type));
				ParameterExpression parameterExpression2 = Expression.Variable(typeof(int), "index");
				Expression expression2 = ParquetFile.For(parameterExpression2, Expression.Constant(0), Expression.NotEqual(parameterExpression2, length), Expression.PreIncrementAssign(parameterExpression2), Expression.Assign(Expression.ArrayAccess(parameterExpression, new Expression[] { parameterExpression2 }), Expression.PropertyOrField(Expression.ArrayAccess(tuples, new Expression[] { parameterExpression2 }), f.Name)));
				MethodCallExpression methodCallExpression = Expression.Call(writer, ParquetFile.GetWriteMethod<TTuple>(parameterExpression.Type.GetElementType()), parameterExpression, length);
				return Expression.Block(new ParameterExpression[] { parameterExpression, parameterExpression2 }, new Expression[] { binaryExpression, expression2, methodCallExpression, binaryExpression2 });
			})), new ParameterExpression[] { writer, tuples, length });
			Action<Expression> onWriteExpressionCreated = ParquetFile.OnWriteExpressionCreated;
			if (onWriteExpressionCreated != null)
			{
				onWriteExpressionCreated(expression);
			}
			return new global::System.ValueTuple<MappedField[], ParquetRowWriter<TTuple>.WriteAction>(fieldsAndProperties, expression.Compile());
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00010990 File Offset: 0x0000EB90
		private static MethodInfo GetReadMethod<[Nullable(2)] TTuple>(Type type)
		{
			MethodInfo method = typeof(ParquetRowReader<TTuple>).GetMethod("ReadColumn", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new ArgumentException("could not find a ParquetReader generic read method");
			}
			return method.MakeGenericMethod(new Type[] { type });
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000109D0 File Offset: 0x0000EBD0
		private static MethodInfo GetWriteMethod<[Nullable(2)] TTuple>(Type type)
		{
			MethodInfo method = typeof(ParquetRowWriter<TTuple>).GetMethod("WriteColumn", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new ArgumentException("could not find a ParquetWriter generic writer method");
			}
			return method.MakeGenericMethod(new Type[] { type });
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00010A10 File Offset: 0x0000EC10
		private static Expression For(ParameterExpression loopVar, Expression initValue, Expression condition, Expression increment, Expression loopContent)
		{
			BinaryExpression binaryExpression = Expression.Assign(loopVar, initValue);
			LabelTarget labelTarget = Expression.Label("LoopBreak");
			return Expression.Block(new ParameterExpression[] { loopVar }, new Expression[]
			{
				binaryExpression,
				Expression.Loop(Expression.IfThenElse(condition, Expression.Block(loopContent, increment), Expression.Break(labelTarget)), labelTarget)
			});
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00010A6C File Offset: 0x0000EC6C
		private static MappedField[] GetFieldsAndProperties(Type type)
		{
			List<MappedField> list = new List<MappedField>();
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(global::System.ValueTuple<, , , , , , , >))
			{
				throw new ArgumentException("System.ValueTuple TTuple types beyond 7 in length are not supported");
			}
			foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags))
			{
				MapToColumnAttribute customAttribute = fieldInfo.GetCustomAttribute<MapToColumnAttribute>();
				string text = ((customAttribute != null) ? customAttribute.ColumnName : null);
				if (fieldInfo.IsPublic || text != null)
				{
					list.Add(new MappedField(fieldInfo.Name, text, fieldInfo.FieldType, fieldInfo));
				}
			}
			foreach (PropertyInfo propertyInfo in type.GetProperties(bindingFlags))
			{
				MapToColumnAttribute customAttribute2 = propertyInfo.GetCustomAttribute<MapToColumnAttribute>();
				string text2 = ((customAttribute2 != null) ? customAttribute2.ColumnName : null);
				MethodInfo getMethod = propertyInfo.GetMethod;
				if ((getMethod != null && getMethod.IsPublic) || text2 != null)
				{
					list.Add(new MappedField(propertyInfo.Name, text2, propertyInfo.PropertyType, propertyInfo));
				}
			}
			if (list.Count == 0)
			{
				throw new ArgumentException(string.Format("Type '{0}' does not have any public fields or properties to map to Parquet columns, ", type) + "or any private fields or properties annotated with 'MapToColumnAttribute'", "type");
			}
			return list.OrderBy((MappedField x) => x.Info.MetadataToken).ToArray<MappedField>();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00010C00 File Offset: 0x0000EE00
		private static Column GetColumn(MappedField field)
		{
			bool flag = field.Type == typeof(decimal) || field.Type == typeof(decimal?);
			ParquetDecimalScaleAttribute parquetDecimalScaleAttribute = field.Info.GetCustomAttributes(typeof(ParquetDecimalScaleAttribute)).Cast<ParquetDecimalScaleAttribute>().SingleOrDefault<ParquetDecimalScaleAttribute>();
			if (!flag && parquetDecimalScaleAttribute != null)
			{
				throw new ArgumentException("field '" + field.Name + "' has a ParquetDecimalScaleAttribute despite not being a decimal type");
			}
			if (flag && parquetDecimalScaleAttribute == null)
			{
				throw new ArgumentException("field '" + field.Name + "' has no ParquetDecimalScaleAttribute despite being a decimal type");
			}
			return new Column(field.Type, field.MappedColumn ?? field.Name, flag ? LogicalType.Decimal(29, parquetDecimalScaleAttribute.Scale) : null);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00010CEC File Offset: 0x0000EEEC
		private static bool IsMemberInitializable(Type type, MappedField[] fields)
		{
			if (!type.IsValueType && type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Array.Empty<Type>(), null) == null)
			{
				return false;
			}
			foreach (MappedField mappedField in fields)
			{
				MemberTypes memberType = mappedField.Info.MemberType;
				if (memberType == MemberTypes.Field && !((FieldInfo)mappedField.Info).IsPublic)
				{
					return false;
				}
				if (memberType == MemberTypes.Property)
				{
					MethodInfo setMethod = ((PropertyInfo)mappedField.Info).SetMethod;
					if (setMethod == null || !setMethod.IsPublic)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000161 RID: 353
		[Nullable(new byte[] { 2, 1 })]
		public static Action<Expression> OnReadExpressionCreated;

		// Token: 0x04000162 RID: 354
		[Nullable(new byte[] { 2, 1 })]
		public static Action<Expression> OnWriteExpressionCreated;

		// Token: 0x04000163 RID: 355
		private static readonly ConcurrentDictionary<Type, Delegate> ReadDelegatesCache = new ConcurrentDictionary<Type, Delegate>();

		// Token: 0x04000164 RID: 356
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "fields", "writeDelegate" })]
		[Nullable(new byte[] { 1, 1, 0, 1, 1 })]
		private static readonly ConcurrentDictionary<Type, global::System.ValueTuple<MappedField[], Delegate>> WriteDelegates = new ConcurrentDictionary<Type, global::System.ValueTuple<MappedField[], Delegate>>();
	}
}
