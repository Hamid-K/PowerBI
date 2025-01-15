using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Normalization;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001049 RID: 4169
	internal class DbCommandTypeMap
	{
		// Token: 0x06006CDA RID: 27866 RVA: 0x0017683E File Offset: 0x00174A3E
		private DbCommandTypeMap(Dictionary<ValueKind, Type> types = null, bool requiresContext = false)
		{
			this.types = types;
			this.requiresEngineContext = requiresContext;
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x00176854 File Offset: 0x00174A54
		public static bool TryFromValue(Value value, out DbCommandTypeMap map)
		{
			if (value.IsNull)
			{
				map = DbCommandTypeMap.Default;
				return true;
			}
			Dictionary<ValueKind, Type> dictionary = new Dictionary<ValueKind, Type>(value.AsList.Count);
			bool flag = false;
			foreach (IValueReference valueReference in value.AsList)
			{
				ListValue asList = valueReference.Value.AsList;
				Type type;
				bool flag2;
				if (asList.Count != 2 || !asList[0].IsType || !asList[1].IsText || dictionary.TryGetValue(asList[0].AsType.TypeKind, out type) || !DbCommandTypeMap.TryConvertType(asList[0].AsType.TypeKind, asList[1].AsString, out type, out flag2))
				{
					map = null;
					return false;
				}
				dictionary.Add(asList[0].AsType.TypeKind, type);
				flag = flag || flag2;
			}
			map = new DbCommandTypeMap(dictionary, flag);
			return true;
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x00176970 File Offset: 0x00174B70
		public DbCommand AddParameters(DbCommand command, Value parameters)
		{
			if (parameters.IsNull)
			{
				return command;
			}
			if (parameters.IsList)
			{
				command = new DbCommandTypeMap.CommandWithParameters(command, this.requiresEngineContext);
				this.AddParameterList(command, parameters.AsList);
				return command;
			}
			if (parameters.IsRecord)
			{
				command = new DbCommandTypeMap.CommandWithParameters(command, this.requiresEngineContext);
				this.AddParameterRecord(command, parameters.AsRecord);
				return command;
			}
			command.Dispose();
			throw ValueException.NewExpressionError<Message0>(Strings.AdoDotNetParametersMustBeRecordOrList, parameters, null);
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x001769E4 File Offset: 0x00174BE4
		private void AddParameterList(DbCommand command, ListValue list)
		{
			foreach (IValueReference valueReference in list)
			{
				DbParameter dbParameter = command.CreateParameter();
				this.SetParameter(dbParameter, valueReference.Value);
				command.Parameters.Add(dbParameter);
			}
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x00176A48 File Offset: 0x00174C48
		private void AddParameterRecord(DbCommand command, RecordValue record)
		{
			foreach (NamedValue namedValue in record.GetFields())
			{
				DbParameter dbParameter = command.CreateParameter();
				dbParameter.ParameterName = namedValue.Key;
				this.SetParameter(dbParameter, namedValue.Value);
				command.Parameters.Add(dbParameter);
			}
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x00176AC8 File Offset: 0x00174CC8
		private void SetParameter(DbParameter param, Value value)
		{
			DbType? dbType = null;
			if (value.IsRecord)
			{
				RecordValue asRecord = value.AsRecord;
				Value value2;
				if (asRecord.TryGetValue("IsNullable", out value2))
				{
					param.IsNullable = value2.AsBoolean;
				}
				if (asRecord.TryGetValue("Size", out value2))
				{
					param.Size = value2.AsInteger32;
				}
				if (asRecord.TryGetValue("Direction", out value2))
				{
					param.Direction = (ParameterDirection)value2.AsInteger32;
					if (param.Direction != ParameterDirection.Input)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.AdoDotNetParameterDirectionNotSupported(param.Direction), asRecord, null);
					}
				}
				if (asRecord.TryGetValue("DbType", out value2))
				{
					dbType = new DbType?((DbType)value2.AsInteger32);
				}
				if (!asRecord.TryGetValue("Value", out value2))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.AdoDotNetNoParameterValue, asRecord, null);
				}
				value = value2;
			}
			bool flag = dbType == null;
			param.Value = this.MapValue(value, ref dbType);
			if (dbType != null)
			{
				try
				{
					param.DbType = dbType.Value;
				}
				catch (Exception ex)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex) || flag)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x00176BF0 File Offset: 0x00174DF0
		private object MapValue(Value value, ref DbType? dbType)
		{
			Type type;
			if (this.types != null && this.types.TryGetValue(value.Kind, out type))
			{
				dbType = new DbType?(dbType ?? DbData.MapToDbType(value.Type.NonNullable, DbType.Object));
				return value.ToOleDb(type);
			}
			switch (value.Kind)
			{
			case ValueKind.Null:
				return DBNull.Value;
			case ValueKind.Time:
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.Time));
				return value.AsTime.AsClrTimeSpan;
			case ValueKind.Date:
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.Date));
				return value.AsDate.AsClrDateTime;
			case ValueKind.DateTime:
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.DateTime));
				return value.AsDateTime.AsClrDateTime;
			case ValueKind.DateTimeZone:
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.DateTimeOffset));
				return value.AsDateTimeZone.AsClrDateTimeOffset;
			case ValueKind.Duration:
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.Time));
				return value.AsDuration.AsClrTimeSpan;
			case ValueKind.Number:
				if (dbType == null)
				{
					dbType = new DbType?(DbType.Double);
					return value.AsNumber.AsDouble;
				}
				return DbCommandTypeMap.MapNumberValue(value.AsNumber, dbType.Value);
			case ValueKind.Logical:
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.Boolean));
				return value.AsBoolean;
			case ValueKind.Text:
				if (dbType == null)
				{
					dbType = new DbType?(DbType.String);
					return value.AsString;
				}
				return DbCommandTypeMap.MapTextValue(value.AsText, dbType.Value);
			case ValueKind.Binary:
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.Binary));
				return value.AsBinary.AsBytes;
			case ValueKind.List:
			case ValueKind.Record:
			case ValueKind.Table:
			{
				TableValue asTable = NormalizationModule.TableFromValue.Invoke(value).AsTable;
				TableDataMapper tableDataMapper = ((this.types != null) ? new DbCommandTypeMap.CommandDataMapper(this.types) : TableDataMapper.Instance);
				IDataReader dataReader = new TableDataReader(asTable.Type.AsTableType, new TableValueDataReader(asTable, true), tableDataMapper);
				dbType = new DbType?(dbType.GetValueOrDefault(DbType.Object));
				return new DataReaderDbDataReader(new DbCommandTypeMap.TypeFixingDataReader(dataReader.WithTableSchema()));
			}
			case ValueKind.Function:
				return new Func<object[], object>((object[] args) => this.InvokeFunction(value.AsFunction, args));
			default:
				throw ValueException.NewExpressionError<Message1>(Strings.AdoDotNetInvalidParameterType(value.Kind), value, null);
			}
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x00176EFC File Offset: 0x001750FC
		private static bool TryConvertType(ValueKind typeKind, string clrType, out Type type, out bool requiresContext)
		{
			requiresContext = false;
			if (typeKind == ValueKind.Binary)
			{
				if (clrType == "System.IO.Stream")
				{
					type = typeof(Stream);
					requiresContext = true;
					return true;
				}
				if (clrType == "System.Byte[]")
				{
					type = typeof(byte[]);
					return true;
				}
			}
			type = null;
			return false;
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x00176F50 File Offset: 0x00175150
		private static object MapTextValue(TextValue text, DbType dbType)
		{
			if (dbType == DbType.Guid)
			{
				try
				{
					return new Guid(text.AsString);
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			return text.AsString;
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x00176FA0 File Offset: 0x001751A0
		private static object MapNumberValue(NumberValue number, DbType dbType)
		{
			try
			{
				switch (dbType)
				{
				case DbType.Byte:
					return number.ToByte();
				case DbType.Currency:
				case DbType.Decimal:
				case DbType.VarNumeric:
					return number.ToDecimal();
				case DbType.Double:
					return number.ToDouble();
				case DbType.Int16:
					return number.ToInt16();
				case DbType.Int32:
					return number.ToInt32();
				case DbType.Int64:
					return number.ToInt64();
				case DbType.SByte:
					return number.ToInt8();
				case DbType.Single:
					return (float)number.ToDouble();
				case DbType.UInt16:
					return (ushort)number.ToInt32();
				case DbType.UInt32:
					return (uint)number.ToInt64();
				case DbType.UInt64:
					if (number.Equals(number.Truncate(0)))
					{
						return (ulong)number.ToDecimal();
					}
					break;
				}
			}
			catch (ValueException)
			{
			}
			catch (OverflowException)
			{
			}
			return number.ToDouble();
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x00177110 File Offset: 0x00175310
		private object InvokeFunction(FunctionValue function, object[] arguments)
		{
			Value[] array = new Value[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				array[i] = ValueMarshaller.MarshalFromClr(arguments[i]);
			}
			Value value = function.Invoke(array);
			DbType? dbType = null;
			return this.MapValue(value, ref dbType);
		}

		// Token: 0x04003C74 RID: 15476
		public static DbCommandTypeMap Default = new DbCommandTypeMap(null, false);

		// Token: 0x04003C75 RID: 15477
		private readonly Dictionary<ValueKind, Type> types;

		// Token: 0x04003C76 RID: 15478
		private bool requiresEngineContext;

		// Token: 0x0200104A RID: 4170
		private sealed class CommandDataMapper : TableDataMapper
		{
			// Token: 0x06006CE6 RID: 27878 RVA: 0x00177166 File Offset: 0x00175366
			public CommandDataMapper(Dictionary<ValueKind, Type> types)
			{
				this.types = types;
			}

			// Token: 0x06006CE7 RID: 27879 RVA: 0x00177178 File Offset: 0x00175378
			protected override Type GetType(TypeValue type)
			{
				Type type2;
				if (this.types.TryGetValue(type.TypeKind, out type2))
				{
					return type2;
				}
				return base.GetType(type);
			}

			// Token: 0x06006CE8 RID: 27880 RVA: 0x001771A4 File Offset: 0x001753A4
			public override object ConvertValue(Value value, SchemaColumn column)
			{
				Type dataType;
				if (column.DataType != typeof(object) || !this.types.TryGetValue(value.Kind, out dataType))
				{
					dataType = column.DataType;
				}
				return value.ToOleDb(dataType);
			}

			// Token: 0x04003C77 RID: 15479
			private readonly Dictionary<ValueKind, Type> types;
		}

		// Token: 0x0200104B RID: 4171
		private sealed class TypeFixingDataReader : DelegatingDataReaderWithTableSchema
		{
			// Token: 0x06006CE9 RID: 27881 RVA: 0x001771EC File Offset: 0x001753EC
			public TypeFixingDataReader(IDataReaderWithTableSchema reader)
				: base(reader)
			{
				this.fieldCount = reader.FieldCount;
				for (int i = 0; i < this.fieldCount; i++)
				{
					Type fieldType = reader.GetFieldType(i);
					if (DbCommandTypeMap.TypeFixingDataReader.TryAdjustType(ref fieldType) || fieldType == typeof(object))
					{
						if (this.checkValue == null)
						{
							this.checkValue = new bool[this.fieldCount];
						}
						this.checkValue[i] = true;
					}
				}
			}

			// Token: 0x17001EF5 RID: 7925
			// (get) Token: 0x06006CEA RID: 27882 RVA: 0x00177264 File Offset: 0x00175464
			public override TableSchema Schema
			{
				get
				{
					TableSchema tableSchema = base.Schema.Copy();
					foreach (SchemaColumn schemaColumn in tableSchema)
					{
						Type dataType = schemaColumn.DataType;
						if (DbCommandTypeMap.TypeFixingDataReader.TryAdjustType(ref dataType))
						{
							schemaColumn.DataType = dataType;
						}
					}
					return tableSchema;
				}
			}

			// Token: 0x06006CEB RID: 27883 RVA: 0x001772CC File Offset: 0x001754CC
			public override Type GetFieldType(int i)
			{
				Type fieldType = base.GetFieldType(i);
				DbCommandTypeMap.TypeFixingDataReader.TryAdjustType(ref fieldType);
				return fieldType;
			}

			// Token: 0x06006CEC RID: 27884 RVA: 0x001772EC File Offset: 0x001754EC
			public override object GetValue(int ordinal)
			{
				object obj = base.GetValue(ordinal);
				if (this.checkValue != null && this.checkValue[ordinal])
				{
					if (obj is Date)
					{
						obj = ((Date)obj).DateTime;
					}
					else if (obj is Time)
					{
						obj = ((Time)obj).TimeSpan;
					}
					else if (obj is Currency)
					{
						obj = ((Currency)obj).Value;
					}
				}
				return obj;
			}

			// Token: 0x06006CED RID: 27885 RVA: 0x0017736C File Offset: 0x0017556C
			public override int GetValues(object[] values)
			{
				if (this.checkValue == null)
				{
					return base.GetValues(values);
				}
				int num = Math.Min(this.fieldCount, values.Length);
				for (int i = 0; i < num; i++)
				{
					values[i] = this.GetValue(i);
				}
				return num;
			}

			// Token: 0x06006CEE RID: 27886 RVA: 0x001773B0 File Offset: 0x001755B0
			private static bool TryAdjustType(ref Type type)
			{
				if (type == typeof(Date))
				{
					type = typeof(DateTime);
				}
				else if (type == typeof(Time))
				{
					type = typeof(TimeSpan);
				}
				else
				{
					if (!(type == typeof(Currency)))
					{
						return false;
					}
					type = typeof(decimal);
				}
				return true;
			}

			// Token: 0x04003C78 RID: 15480
			private readonly int fieldCount;

			// Token: 0x04003C79 RID: 15481
			private readonly bool[] checkValue;
		}

		// Token: 0x0200104C RID: 4172
		private sealed class CommandWithParameters : DelegatingDbCommand
		{
			// Token: 0x06006CEF RID: 27887 RVA: 0x00177423 File Offset: 0x00175623
			public CommandWithParameters(DbCommand command, bool requiresContext)
				: base(command)
			{
				this.disposables = new List<IDisposable>();
				this.requiresContext = requiresContext;
			}

			// Token: 0x06006CF0 RID: 27888 RVA: 0x00177440 File Offset: 0x00175640
			protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
			{
				IDisposable disposable2;
				DbDataReader dbDataReader;
				using (this.SetupExecution(out disposable2))
				{
					try
					{
						dbDataReader = base.ExecuteDbDataReader(behavior).EnterEngineContext<DbDataReader>().WithTableSchema()
							.AfterDispose(new Action(disposable2.Dispose));
					}
					catch
					{
						disposable2.Dispose();
						throw;
					}
				}
				return dbDataReader;
			}

			// Token: 0x06006CF1 RID: 27889 RVA: 0x001774AC File Offset: 0x001756AC
			public override object ExecuteScalar()
			{
				IDisposable disposable2;
				object obj;
				using (this.SetupExecution(out disposable2))
				{
					using (disposable2)
					{
						obj = base.ExecuteScalar();
					}
				}
				return obj;
			}

			// Token: 0x06006CF2 RID: 27890 RVA: 0x00177500 File Offset: 0x00175700
			public override int ExecuteNonQuery()
			{
				IDisposable disposable2;
				int num;
				using (this.SetupExecution(out disposable2))
				{
					using (disposable2)
					{
						num = base.ExecuteNonQuery();
					}
				}
				return num;
			}

			// Token: 0x06006CF3 RID: 27891 RVA: 0x00177554 File Offset: 0x00175754
			protected override void Dispose(bool disposing)
			{
				List<IDisposable> list = this.disposables;
				IDisposable[] array;
				lock (list)
				{
					array = this.disposables.ToArray();
					this.disposables.Clear();
				}
				IDisposable[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].Dispose();
				}
				base.Parameters.Clear();
				base.Dispose(disposing);
			}

			// Token: 0x06006CF4 RID: 27892 RVA: 0x001775D4 File Offset: 0x001757D4
			private IDisposable SetupExecution(out IDisposable disableContext)
			{
				foreach (object obj in base.Parameters)
				{
					object value = ((DbParameter)obj).Value;
					Type type = ((value != null) ? value.GetType() : null);
					if (type == typeof(Stream) || type == typeof(Func<object[], object>))
					{
						this.requiresContext = true;
					}
				}
				if (this.requiresContext)
				{
					EngineContext.Enable();
				}
				foreach (object obj2 in base.Parameters)
				{
					DbParameter dbParameter = (DbParameter)obj2;
					dbParameter.Value = this.MarshalValue(dbParameter.Value);
				}
				disableContext = new ActionOnDispose(delegate
				{
					if (this.requiresContext)
					{
						EngineContext.Disable();
					}
				});
				return EngineContext.Leave();
			}

			// Token: 0x06006CF5 RID: 27893 RVA: 0x001776E0 File Offset: 0x001758E0
			private object MarshalValue(object value)
			{
				IDisposable disposable = value as IDisposable;
				if (disposable != null)
				{
					List<IDisposable> list = this.disposables;
					lock (list)
					{
						this.disposables.Add(disposable);
					}
				}
				if (value is Stream)
				{
					value = ((Stream)value).LeaveEngineContext<Stream>();
				}
				else if (value is DbDataReader)
				{
					value = ((DbDataReader)value).LeaveEngineContext<DbDataReader>();
				}
				else if (value is TextReader)
				{
					value = ((TextReader)value).LeaveEngineContext<TextReader>();
				}
				else if (value is Func<object[], object>)
				{
					DbCommandTypeMap.CommandWithParameters.FunctionInvoker functionInvoker = new DbCommandTypeMap.CommandWithParameters.FunctionInvoker(this, (Func<object[], object>)value);
					List<IDisposable> list = this.disposables;
					lock (list)
					{
						this.disposables.Add(functionInvoker);
					}
					value = new Func<object[], object>(functionInvoker.InvokeUnlessDisposed);
				}
				return value;
			}

			// Token: 0x04003C7A RID: 15482
			private readonly List<IDisposable> disposables;

			// Token: 0x04003C7B RID: 15483
			private bool requiresContext;

			// Token: 0x0200104D RID: 4173
			private sealed class FunctionInvoker : IDisposable
			{
				// Token: 0x06006CF7 RID: 27895 RVA: 0x001777DF File Offset: 0x001759DF
				public FunctionInvoker(DbCommandTypeMap.CommandWithParameters command, Func<object[], object> function)
				{
					this.syncRoot = new object();
					this.command = command;
					this.function = function.LeaveEngineContext<Func<object[], object>>();
				}

				// Token: 0x06006CF8 RID: 27896 RVA: 0x00177808 File Offset: 0x00175A08
				public object InvokeUnlessDisposed(object[] args)
				{
					object obj = this.syncRoot;
					Func<object[], object> func;
					lock (obj)
					{
						func = this.function;
					}
					if (func == null)
					{
						throw new ObjectDisposedException("M delegate");
					}
					return this.command.MarshalValue(func(args));
				}

				// Token: 0x06006CF9 RID: 27897 RVA: 0x0017786C File Offset: 0x00175A6C
				public void Dispose()
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.function = null;
					}
				}

				// Token: 0x04003C7C RID: 15484
				private readonly object syncRoot;

				// Token: 0x04003C7D RID: 15485
				private readonly DbCommandTypeMap.CommandWithParameters command;

				// Token: 0x04003C7E RID: 15486
				private Func<object[], object> function;
			}
		}
	}
}
