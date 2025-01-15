using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql
{
	// Token: 0x02000016 RID: 22
	public class SqlSQDataReader : IDataReaderExtension, IDataReader, IDisposable
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005E74 File Offset: 0x00004074
		internal SqlSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
		{
			this.m_targetDataReader = targetDataReader;
			this.m_traceLog = traceLog;
			this.m_schema = new SqlSQDataReader.FieldInfo[compiledSql.Schema.Count];
			this.InitSchema(compiledSql, dataIsCaseSensitive, out this.m_maxAggregationFields, out this.m_aggregationFieldCountTargetIndex, out this.m_aggregationFieldCountConvertValueType);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005ED1 File Offset: 0x000040D1
		string IDataReader.GetName(int fieldIndex)
		{
			this.CheckFieldIndex(fieldIndex);
			return this.m_schema[fieldIndex].ExpressionName;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005EE8 File Offset: 0x000040E8
		int IDataReader.GetOrdinal(string fieldName)
		{
			int num;
			if (this.m_expressionNameToFieldIndex.TryGetValue(fieldName, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005F08 File Offset: 0x00004108
		int IDataReader.FieldCount
		{
			get
			{
				return this.m_schema.Length;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005F12 File Offset: 0x00004112
		bool IDataReader.Read()
		{
			return this.m_targetDataReader.Read();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005F1F File Offset: 0x0000411F
		Type IDataReader.GetFieldType(int fieldIndex)
		{
			this.CheckFieldIndex(fieldIndex);
			return this.m_schema[fieldIndex].SystemType;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005F38 File Offset: 0x00004138
		object IDataReader.GetValue(int fieldIndex)
		{
			this.CheckFieldIndex(fieldIndex);
			SqlSQDataReader.FieldInfo fieldInfo = this.m_schema[fieldIndex];
			if (fieldInfo.ModelType != DataType.EntityKey)
			{
				object obj = this.m_targetDataReader.GetValue(fieldInfo.TargetIndex);
				if (fieldInfo.ConvertValueType != null)
				{
					obj = fieldInfo.ConvertValueType(obj);
				}
				return obj;
			}
			if (fieldInfo.SystemType != SqlSQDataReader.EntityKeyTargetSystemType)
			{
				throw SQEAssert.AssertFalseAndThrow("Entity key value type mismatch.", Array.Empty<object>());
			}
			if (fieldInfo.EntityKeyParts != null)
			{
				bool flag = true;
				for (int i = 0; i < fieldInfo.EntityKeyParts.Length; i++)
				{
					object obj2 = this.m_targetDataReader.GetValue(fieldInfo.EntityKeyParts[i].TargetIndex);
					if (obj2 == null || Convert.IsDBNull(obj2))
					{
						obj2 = null;
					}
					else
					{
						flag = false;
					}
					fieldInfo.EntityKeyPartValues[i] = obj2;
				}
				if (flag)
				{
					return DBNull.Value;
				}
				return fieldInfo.EntityKeyBuilder.CreateKeyAsBase64String(fieldInfo.EntityKeyPartValues, fieldInfo.EntityKeyPartTypes);
			}
			else
			{
				object value = this.m_targetDataReader.GetValue(fieldInfo.TargetIndex);
				if (value == null || Convert.IsDBNull(value))
				{
					return DBNull.Value;
				}
				return value.ToString();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006050 File Offset: 0x00004250
		bool IDataReaderExtension.IsAggregationField(int index)
		{
			this.CheckFieldIndex(index);
			SqlSQDataReader.FieldInfo fieldInfo = this.m_schema[index];
			int aggregationFlagTargetIndex = fieldInfo.AggregationFlagTargetIndex;
			if (aggregationFlagTargetIndex >= 0)
			{
				object obj = this.m_targetDataReader.GetValue(aggregationFlagTargetIndex);
				if (fieldInfo.AggregationFlagConvertValueType != null)
				{
					obj = fieldInfo.AggregationFlagConvertValueType(obj);
				}
				return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
			}
			return false;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000060A7 File Offset: 0x000042A7
		bool IDataReaderExtension.IsAggregateRow
		{
			get
			{
				return this.m_aggregationFieldCountTargetIndex >= 0 && this.m_maxAggregationFields > ((IDataReaderExtension)this).AggregationFieldCount;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000060C4 File Offset: 0x000042C4
		int IDataReaderExtension.AggregationFieldCount
		{
			get
			{
				if (this.m_aggregationFieldCountTargetIndex >= 0)
				{
					object obj = this.m_targetDataReader.GetValue(this.m_aggregationFieldCountTargetIndex);
					if (this.m_aggregationFieldCountConvertValueType != null)
					{
						obj = this.m_aggregationFieldCountConvertValueType(obj);
					}
					return (int)obj;
				}
				return 0;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000610C File Offset: 0x0000430C
		void IDisposable.Dispose()
		{
			if (this.m_targetDataReader != null)
			{
				this.m_targetDataReader.Dispose();
			}
			if (this.m_schema != null)
			{
				for (int i = 0; i < this.m_schema.Length; i++)
				{
					SqlSQDataReader.FieldInfo fieldInfo = this.m_schema[i];
					if (fieldInfo.EntityKeyParts != null)
					{
						((IDisposable)fieldInfo.EntityKeyBuilder).Dispose();
					}
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006163 File Offset: 0x00004363
		internal virtual bool IsAllowedTypeMismatch(DataType modelDataType, Type targetSystemType, out Converter<object, object> convertValueType, out Type newTargetSystemType)
		{
			convertValueType = null;
			newTargetSystemType = null;
			return false;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006170 File Offset: 0x00004370
		private void InitSchema(CompiledSql compiledSql, bool? dataIsCaseSensitive, out int maxAggregationFields, out int aggregationFieldCountTargetIndex, out Converter<object, object> aggregationFieldCountConvertValueType)
		{
			int num = 0;
			foreach (CompiledSql.ExpressionSchema expressionSchema in compiledSql.Schema)
			{
				if (expressionSchema.ColumnAliases.Length == 0)
				{
					throw SQEAssert.AssertFalseAndThrow("Found expression with no sql columns mapping.", Array.Empty<object>());
				}
				string name = expressionSchema.Expression.Name;
				ResultType resultType = expressionSchema.Expression.GetResultType();
				DataType dataType = resultType.DataType;
				SqlSQDataReader.FieldInfo fieldInfo;
				if (resultType.EntityKeyTarget != null)
				{
					if (dataType != DataType.EntityKey && dataType != DataType.Null)
					{
						throw SQEAssert.AssertFalseAndThrow("Invalid data type for a non-null EntityKeyTarget: {0}.", new object[] { dataType });
					}
					SqlSQDataReader.FieldInfo[] array = new SqlSQDataReader.FieldInfo[expressionSchema.ColumnAliases.Length];
					Type[] entityKeyPartTypes = QueryPlanBuilder.GetEntityKeyPartTypes(resultType.EntityKeyTarget);
					for (int i = 0; i < expressionSchema.ColumnAliases.Length; i++)
					{
						array[i] = new SqlSQDataReader.FieldInfo(this.GetTargetOrdinal(expressionSchema.ColumnAliases[i]), name, entityKeyPartTypes[i], dataType, null);
					}
					bool flag = true;
					if (dataIsCaseSensitive != null && dataIsCaseSensitive.Value)
					{
						flag = false;
					}
					fieldInfo = new SqlSQDataReader.FieldInfo(array[0].TargetIndex, array, flag, name, SqlSQDataReader.EntityKeyTargetSystemType, dataType);
				}
				else
				{
					Converter<object, object> converter = null;
					if (expressionSchema.ColumnAliases.Length != 1)
					{
						throw SQEAssert.AssertFalseAndThrow("Found a non-EntityKey expression that is mapped on multiple sql columns.", Array.Empty<object>());
					}
					string text = expressionSchema.ColumnAliases[0];
					int targetOrdinal = this.GetTargetOrdinal(text);
					Type type = this.m_targetDataReader.GetFieldType(targetOrdinal);
					if (dataType != DataType.Null)
					{
						if (dataType != DataTypeMapper.TranslateClrType(type))
						{
							if (dataType != DataType.EntityKey || !(type == SqlSQDataReader.EntityKeyTargetSystemType))
							{
								this.ProcessTypeMismatch(dataType, type, text, name, out converter, out type);
							}
						}
						else if (dataType == DataType.DateTime && type == typeof(DateTime))
						{
							converter = new Converter<object, object>(SqlSQDataReader.ConvertDateTimeToUnspecifiedKind);
						}
						else if (dataType == DataType.String && type == typeof(Guid))
						{
							converter = new Converter<object, object>(SqlSQDataReader.ConvertGuidToString);
							type = typeof(string);
						}
						else if (dataType == DataType.DateTime && type == typeof(DateTimeOffset))
						{
							converter = new Converter<object, object>(SqlSQDataReader.ConvertDateTimeOffsetToDateTimeUTC);
							type = typeof(DateTime);
						}
					}
					fieldInfo = new SqlSQDataReader.FieldInfo(targetOrdinal, name, type, dataType, converter);
				}
				string text2;
				if (compiledSql.AggregationFlags.TryGetValue(name, out text2))
				{
					fieldInfo.AggregationFlagTargetIndex = this.GetTargetOrdinal(text2);
					Type fieldType = this.m_targetDataReader.GetFieldType(fieldInfo.AggregationFlagTargetIndex);
					if (DataType.Boolean != DataTypeMapper.TranslateClrType(fieldType))
					{
						Converter<object, object> converter2;
						this.ProcessTypeMismatch(DataType.Boolean, fieldType, text2, name, out converter2, out fieldType);
						fieldInfo.AggregationFlagConvertValueType = converter2;
					}
				}
				this.m_expressionNameToFieldIndex.Add(name, num);
				this.m_schema[num] = fieldInfo;
				num++;
			}
			maxAggregationFields = compiledSql.AggregationFlags.Count;
			if (compiledSql.AggregationFieldCountColumnName != null)
			{
				aggregationFieldCountTargetIndex = this.GetTargetOrdinal(compiledSql.AggregationFieldCountColumnName);
				aggregationFieldCountConvertValueType = null;
				Type fieldType2 = this.m_targetDataReader.GetFieldType(aggregationFieldCountTargetIndex);
				if (DataType.Integer != DataTypeMapper.TranslateClrType(fieldType2))
				{
					this.ProcessTypeMismatch(DataType.Integer, fieldType2, compiledSql.AggregationFieldCountColumnName, "aggregationFieldCount", out aggregationFieldCountConvertValueType, out fieldType2);
					return;
				}
			}
			else
			{
				if (maxAggregationFields > 0)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				aggregationFieldCountTargetIndex = -1;
				aggregationFieldCountConvertValueType = null;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006518 File Offset: 0x00004718
		private static object ConvertDateTimeToUnspecifiedKind(object objValue)
		{
			if (!(objValue is DateTime))
			{
				return objValue;
			}
			DateTime dateTime = (DateTime)objValue;
			if (dateTime.Kind != DateTimeKind.Unspecified)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
			}
			return objValue;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000654D File Offset: 0x0000474D
		private static object ConvertGuidToString(object objValue)
		{
			if (objValue is Guid)
			{
				return objValue.ToString();
			}
			return objValue;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006560 File Offset: 0x00004760
		private static object ConvertDateTimeOffsetToDateTimeUTC(object objValue)
		{
			if (objValue is DateTimeOffset)
			{
				return ((DateTimeOffset)objValue).UtcDateTime;
			}
			return objValue;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000658C File Offset: 0x0000478C
		private void ProcessTypeMismatch(DataType modelDataType, Type targetSystemType, string targetColumnName, string expressionName, out Converter<object, object> convertValueType, out Type newTargetSystemType)
		{
			if (!this.IsAllowedTypeMismatch(modelDataType, targetSystemType, out convertValueType, out newTargetSystemType))
			{
				if (this.m_traceLog != null && this.m_traceLog.TraceError)
				{
					string text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Type mismatch of returned database column '{0}' for expression '{1}'. Expected model type is {2}, actual system type is {3}.", new object[]
					{
						targetColumnName,
						expressionName,
						modelDataType.ToString(),
						targetSystemType.ToString()
					});
					this.m_traceLog.WriteTrace(text, TraceLevel.Error);
				}
				convertValueType = null;
				newTargetSystemType = targetSystemType;
			}
			if (newTargetSystemType == null || (targetSystemType != newTargetSystemType && convertValueType == null))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006628 File Offset: 0x00004828
		private int GetTargetOrdinal(string columnName)
		{
			int num = -1;
			try
			{
				num = this.m_targetDataReader.GetOrdinal(columnName);
			}
			catch (Exception ex)
			{
				if (this.m_traceLog != null && this.m_traceLog.TraceError)
				{
					this.m_traceLog.WriteTrace(Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Failed to get ordinal number for the column {0}. {1}", new object[]
					{
						columnName,
						ex.ToString()
					}), TraceLevel.Error);
				}
			}
			if (num < 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Can not find target column: {0}.", new object[] { columnName });
			}
			return num;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000066B4 File Offset: 0x000048B4
		private void CheckFieldIndex(int fieldIndex)
		{
			if (fieldIndex < 0 || fieldIndex >= this.m_schema.Length)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("fieldIndex"));
			}
		}

		// Token: 0x0400005D RID: 93
		private readonly IDataReader m_targetDataReader;

		// Token: 0x0400005E RID: 94
		private readonly Dictionary<string, int> m_expressionNameToFieldIndex = new Dictionary<string, int>();

		// Token: 0x0400005F RID: 95
		private readonly SqlSQDataReader.FieldInfo[] m_schema;

		// Token: 0x04000060 RID: 96
		private readonly int m_aggregationFieldCountTargetIndex;

		// Token: 0x04000061 RID: 97
		private readonly Converter<object, object> m_aggregationFieldCountConvertValueType;

		// Token: 0x04000062 RID: 98
		private readonly int m_maxAggregationFields;

		// Token: 0x04000063 RID: 99
		private ITraceLog m_traceLog;

		// Token: 0x04000064 RID: 100
		private static readonly Type EntityKeyTargetSystemType = typeof(string);

		// Token: 0x020000B3 RID: 179
		private sealed class FieldInfo
		{
			// Token: 0x060006A6 RID: 1702 RVA: 0x0001AB6C File Offset: 0x00018D6C
			internal FieldInfo(int targetIndex, string expressionName, Type systemType, DataType modelType, Converter<object, object> convertValueType)
				: this(targetIndex, null, false, expressionName, systemType, modelType)
			{
				this.ConvertValueType = convertValueType;
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x0001AB84 File Offset: 0x00018D84
			internal FieldInfo(int targetIndex, SqlSQDataReader.FieldInfo[] entityKeyParts, bool forceCaseInsensitiveBase64ForEntityKeys, string expressionName, Type systemType, DataType modelType)
			{
				this.TargetIndex = targetIndex;
				this.EntityKeyParts = entityKeyParts;
				if (entityKeyParts != null)
				{
					this.EntityKeyPartTypes = new Type[entityKeyParts.Length];
					for (int i = 0; i < entityKeyParts.Length; i++)
					{
						this.EntityKeyPartTypes[i] = entityKeyParts[i].SystemType;
					}
					this.EntityKeyPartValues = new object[entityKeyParts.Length];
					this.EntityKeyBuilder = new EntityKeyBuilder();
					this.EntityKeyBuilder.GenerateCaseInsensitiveBase64Strings = forceCaseInsensitiveBase64ForEntityKeys;
				}
				this.ExpressionName = expressionName;
				this.SystemType = systemType;
				this.ModelType = modelType;
			}

			// Token: 0x0400033D RID: 829
			internal readonly int TargetIndex;

			// Token: 0x0400033E RID: 830
			internal readonly SqlSQDataReader.FieldInfo[] EntityKeyParts;

			// Token: 0x0400033F RID: 831
			internal readonly Type[] EntityKeyPartTypes;

			// Token: 0x04000340 RID: 832
			internal EntityKey ReusableEntityKey;

			// Token: 0x04000341 RID: 833
			internal readonly object[] EntityKeyPartValues;

			// Token: 0x04000342 RID: 834
			internal readonly EntityKeyBuilder EntityKeyBuilder;

			// Token: 0x04000343 RID: 835
			internal string ExpressionName;

			// Token: 0x04000344 RID: 836
			internal readonly Type SystemType;

			// Token: 0x04000345 RID: 837
			internal readonly DataType ModelType;

			// Token: 0x04000346 RID: 838
			internal readonly Converter<object, object> ConvertValueType;

			// Token: 0x04000347 RID: 839
			internal int AggregationFlagTargetIndex = -1;

			// Token: 0x04000348 RID: 840
			internal Converter<object, object> AggregationFlagConvertValueType;
		}
	}
}
