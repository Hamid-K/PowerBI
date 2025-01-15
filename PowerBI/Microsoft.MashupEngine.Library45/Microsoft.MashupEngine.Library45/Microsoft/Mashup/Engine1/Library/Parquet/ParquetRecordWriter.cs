using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Parquet.Interop;
using Microsoft.Mashup.Engine1.Library.Parquet.Schema;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F59 RID: 8025
	internal class ParquetRecordWriter : IDisposable
	{
		// Token: 0x06010D99 RID: 69017 RVA: 0x003A08A4 File Offset: 0x0039EAA4
		public ParquetRecordWriter(SchemaElement rootSchemaElement)
		{
			this.rootBuilder = (ParquetRecordWriter.GroupColumnBuilder)ParquetRecordWriter.ColumnBuilder.Create(rootSchemaElement);
			this.primitiveBuilders = this.rootBuilder.GetPrimitiveColumnBuilders().ToList<ParquetRecordWriter.PrimitiveColumnBuilder>();
		}

		// Token: 0x06010D9A RID: 69018 RVA: 0x003A08D4 File Offset: 0x0039EAD4
		public void Write(RowGroupWriter writer, bool isBufferedWriter = false)
		{
			int num = 0;
			foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveBuilders)
			{
				if (isBufferedWriter)
				{
					using (ColumnWriter columnWriter = writer.Column(num))
					{
						primitiveColumnBuilder.Write(columnWriter);
					}
					num++;
				}
				else
				{
					using (ColumnWriter columnWriter2 = writer.NextColumn())
					{
						primitiveColumnBuilder.Write(columnWriter2);
					}
				}
			}
		}

		// Token: 0x06010D9B RID: 69019 RVA: 0x003A0978 File Offset: 0x0039EB78
		public void Clear()
		{
			foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveBuilders)
			{
				primitiveColumnBuilder.Clear();
			}
		}

		// Token: 0x06010D9C RID: 69020 RVA: 0x003A09C4 File Offset: 0x0039EBC4
		public void Add(RecordValue record)
		{
			this.AddRecord(record, this.rootBuilder, 0);
		}

		// Token: 0x06010D9D RID: 69021 RVA: 0x003A09D4 File Offset: 0x0039EBD4
		public void Add(IPage page, int rowCount)
		{
			int num = 0;
			foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveBuilders)
			{
				primitiveColumnBuilder.AddColumn(page.GetColumn(num), rowCount);
				num++;
			}
		}

		// Token: 0x06010D9E RID: 69022 RVA: 0x003A0A2C File Offset: 0x0039EC2C
		public void Commit()
		{
			this.rootBuilder.Commit();
		}

		// Token: 0x06010D9F RID: 69023 RVA: 0x003A0A39 File Offset: 0x0039EC39
		public void Rollback()
		{
			this.rootBuilder.Rollback();
		}

		// Token: 0x06010DA0 RID: 69024 RVA: 0x003A0A46 File Offset: 0x0039EC46
		public long EstimateSize()
		{
			return this.rootBuilder.EstimateSize();
		}

		// Token: 0x06010DA1 RID: 69025 RVA: 0x003A0A53 File Offset: 0x0039EC53
		public void Dispose()
		{
			this.rootBuilder.Dispose();
		}

		// Token: 0x06010DA2 RID: 69026 RVA: 0x003A0A60 File Offset: 0x0039EC60
		private void AddValue(Value value, ParquetRecordWriter.ColumnBuilder builder, short repetitionLevel)
		{
			if (builder.SchemaElement.Repetition == Repetition.Optional && value.IsNull)
			{
				builder.AddLevels(builder.NullDefinitionLevel, repetitionLevel);
				return;
			}
			if (builder.SchemaElement.Repetition == Repetition.Repeated)
			{
				this.AddList(value, builder, repetitionLevel);
				return;
			}
			this.AddRequiredValue(value, builder, repetitionLevel);
		}

		// Token: 0x06010DA3 RID: 69027 RVA: 0x003A0AB4 File Offset: 0x0039ECB4
		private void AddRequiredValue(Value value, ParquetRecordWriter.ColumnBuilder builder, short repetitionLevel)
		{
			ParquetRecordWriter.GroupColumnBuilder groupColumnBuilder = builder as ParquetRecordWriter.GroupColumnBuilder;
			if (groupColumnBuilder != null)
			{
				RecordValue recordValue = (RecordValue)builder.SchemaElement.FromValue(null, value);
				this.AddRecord(recordValue, groupColumnBuilder, repetitionLevel);
				return;
			}
			this.AddPrimitive(value, (ParquetRecordWriter.PrimitiveColumnBuilder)builder, repetitionLevel);
		}

		// Token: 0x06010DA4 RID: 69028 RVA: 0x003A0AF8 File Offset: 0x0039ECF8
		private void AddList(Value listOrTable, ParquetRecordWriter.ColumnBuilder builder, short repetitionLevel)
		{
			IEnumerable<IValueReference> enumerable;
			if (listOrTable.IsTable)
			{
				enumerable = listOrTable.AsTable;
			}
			else
			{
				if (!listOrTable.IsList)
				{
					throw new InvalidOperationException();
				}
				enumerable = listOrTable.AsList;
			}
			using (IEnumerator<IValueReference> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					this.AddRequiredValue(enumerator.Current.Value, builder, repetitionLevel);
					repetitionLevel = builder.SchemaElement.RepetitionLevel;
					while (enumerator.MoveNext())
					{
						IValueReference valueReference = enumerator.Current;
						this.AddRequiredValue(valueReference.Value, builder, repetitionLevel);
					}
				}
				else
				{
					builder.AddLevels(builder.NullDefinitionLevel, repetitionLevel);
				}
			}
		}

		// Token: 0x06010DA5 RID: 69029 RVA: 0x003A0BA8 File Offset: 0x0039EDA8
		private void AddRecord(RecordValue record, ParquetRecordWriter.GroupColumnBuilder builder, short repetitionLevel)
		{
			Keys keys = record.Keys;
			for (int i = 0; i < record.Count; i++)
			{
				ParquetRecordWriter.ColumnBuilder child = builder.GetChild(keys[i]);
				this.AddValue(record[i], child, repetitionLevel);
			}
		}

		// Token: 0x06010DA6 RID: 69030 RVA: 0x003A0BEA File Offset: 0x0039EDEA
		private void AddPrimitive(Value value, ParquetRecordWriter.PrimitiveColumnBuilder builder, short repetitionLevel)
		{
			builder.AddLevels(builder.NonNullDefinitionLevel, repetitionLevel);
			builder.AddValue(value);
		}

		// Token: 0x0400653C RID: 25916
		public const int BytesPerMegabyte = 1048576;

		// Token: 0x0400653D RID: 25917
		public const long NativeMemoryToLeave = 16777216L;

		// Token: 0x0400653E RID: 25918
		public const int PageSize = 4096;

		// Token: 0x0400653F RID: 25919
		private const int ManagedMegabytesToLeave = 16;

		// Token: 0x04006540 RID: 25920
		private readonly ParquetRecordWriter.GroupColumnBuilder rootBuilder;

		// Token: 0x04006541 RID: 25921
		private readonly IEnumerable<ParquetRecordWriter.PrimitiveColumnBuilder> primitiveBuilders;

		// Token: 0x02001F5A RID: 8026
		private abstract class ColumnBuilder : IDisposable
		{
			// Token: 0x06010DA7 RID: 69031 RVA: 0x003A0C00 File Offset: 0x0039EE00
			protected ColumnBuilder(SchemaElement schemaElement)
			{
				this.schemaElement = schemaElement;
				SchemaElement parent = schemaElement.Parent;
				this.NullDefinitionLevel = ((parent != null) ? parent.DefinitionLevel : 0);
				this.NonNullDefinitionLevel = schemaElement.DefinitionLevel;
			}

			// Token: 0x17002CAF RID: 11439
			// (get) Token: 0x06010DA8 RID: 69032 RVA: 0x003A0C33 File Offset: 0x0039EE33
			public short NullDefinitionLevel { get; }

			// Token: 0x17002CB0 RID: 11440
			// (get) Token: 0x06010DA9 RID: 69033 RVA: 0x003A0C3B File Offset: 0x0039EE3B
			public short NonNullDefinitionLevel { get; }

			// Token: 0x06010DAA RID: 69034 RVA: 0x003A0C44 File Offset: 0x0039EE44
			public static ParquetRecordWriter.ColumnBuilder Create(SchemaElement schemaElement)
			{
				GroupSchemaElement groupSchemaElement = schemaElement as GroupSchemaElement;
				if (groupSchemaElement != null)
				{
					return new ParquetRecordWriter.GroupColumnBuilder(groupSchemaElement);
				}
				PrimitiveSchemaElement primitiveSchemaElement = schemaElement as PrimitiveSchemaElement;
				if (primitiveSchemaElement != null)
				{
					return ParquetRecordWriter.ColumnBuilder.Create(primitiveSchemaElement);
				}
				throw new InvalidOperationException();
			}

			// Token: 0x06010DAB RID: 69035 RVA: 0x003A0C78 File Offset: 0x0039EE78
			public static ParquetRecordWriter.ColumnBuilder Create(PrimitiveSchemaElement schemaElement)
			{
				switch (schemaElement.PhysicalType)
				{
				case PhysicalType.Boolean:
					return ParquetRecordWriter.PrimitiveColumnBuilder<bool>.Create(schemaElement);
				case PhysicalType.Int32:
					return ParquetRecordWriter.PrimitiveColumnBuilder<int>.Create(schemaElement);
				case PhysicalType.Int64:
					return ParquetRecordWriter.PrimitiveColumnBuilder<long>.Create(schemaElement);
				case PhysicalType.Int96:
					return ParquetRecordWriter.PrimitiveColumnBuilder<Int96>.Create(schemaElement);
				case PhysicalType.Float:
					return ParquetRecordWriter.PrimitiveColumnBuilder<float>.Create(schemaElement);
				case PhysicalType.Double:
					return ParquetRecordWriter.PrimitiveColumnBuilder<double>.Create(schemaElement);
				case PhysicalType.ByteArray:
					return ParquetRecordWriter.PrimitiveColumnBuilder<ByteArray>.Create(schemaElement);
				case PhysicalType.FixedLenByteArray:
					return ParquetRecordWriter.PrimitiveColumnBuilder<FixedLenByteArray>.Create(schemaElement);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17002CB1 RID: 11441
			// (get) Token: 0x06010DAC RID: 69036 RVA: 0x003A0CF1 File Offset: 0x0039EEF1
			public SchemaElement SchemaElement
			{
				get
				{
					return this.schemaElement;
				}
			}

			// Token: 0x06010DAD RID: 69037
			public abstract void AddLevels(short definitionLevel, short repetitionLevel);

			// Token: 0x06010DAE RID: 69038
			public abstract void Commit();

			// Token: 0x06010DAF RID: 69039
			public abstract void Rollback();

			// Token: 0x06010DB0 RID: 69040
			public abstract long EstimateSize();

			// Token: 0x06010DB1 RID: 69041
			public abstract IEnumerable<ParquetRecordWriter.PrimitiveColumnBuilder> GetPrimitiveColumnBuilders();

			// Token: 0x06010DB2 RID: 69042
			public abstract void Clear();

			// Token: 0x06010DB3 RID: 69043
			public abstract void Dispose();

			// Token: 0x04006542 RID: 25922
			private readonly SchemaElement schemaElement;
		}

		// Token: 0x02001F5B RID: 8027
		private sealed class GroupColumnBuilder : ParquetRecordWriter.ColumnBuilder
		{
			// Token: 0x06010DB4 RID: 69044 RVA: 0x003A0CFC File Offset: 0x0039EEFC
			public GroupColumnBuilder(GroupSchemaElement groupSchemaElement)
				: base(groupSchemaElement)
			{
				this.childColumnBuilders = new Dictionary<string, ParquetRecordWriter.ColumnBuilder>();
				this.primitiveColumnBuilders = new List<ParquetRecordWriter.PrimitiveColumnBuilder>();
				for (int i = 0; i < groupSchemaElement.Fields.Length; i++)
				{
					ParquetRecordWriter.ColumnBuilder columnBuilder = ParquetRecordWriter.ColumnBuilder.Create(groupSchemaElement.Fields[i]);
					this.childColumnBuilders.Add(groupSchemaElement.FieldKeys[i], columnBuilder);
					this.primitiveColumnBuilders.AddRange(columnBuilder.GetPrimitiveColumnBuilders());
				}
			}

			// Token: 0x06010DB5 RID: 69045 RVA: 0x003A0D70 File Offset: 0x0039EF70
			public ParquetRecordWriter.ColumnBuilder GetChild(string name)
			{
				return this.childColumnBuilders[name];
			}

			// Token: 0x06010DB6 RID: 69046 RVA: 0x003A0D80 File Offset: 0x0039EF80
			public override void AddLevels(short definitionLevel, short repetitionLevel)
			{
				foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveColumnBuilders)
				{
					primitiveColumnBuilder.AddLevels(definitionLevel, repetitionLevel);
				}
			}

			// Token: 0x06010DB7 RID: 69047 RVA: 0x003A0DD4 File Offset: 0x0039EFD4
			public override void Commit()
			{
				foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveColumnBuilders)
				{
					primitiveColumnBuilder.Commit();
				}
			}

			// Token: 0x06010DB8 RID: 69048 RVA: 0x003A0E24 File Offset: 0x0039F024
			public override void Rollback()
			{
				foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveColumnBuilders)
				{
					primitiveColumnBuilder.Rollback();
				}
			}

			// Token: 0x06010DB9 RID: 69049 RVA: 0x003A0E74 File Offset: 0x0039F074
			public override long EstimateSize()
			{
				return this.primitiveColumnBuilders.Sum((ParquetRecordWriter.PrimitiveColumnBuilder primitive) => primitive.EstimateSize());
			}

			// Token: 0x06010DBA RID: 69050 RVA: 0x003A0EA0 File Offset: 0x0039F0A0
			public override IEnumerable<ParquetRecordWriter.PrimitiveColumnBuilder> GetPrimitiveColumnBuilders()
			{
				return this.primitiveColumnBuilders;
			}

			// Token: 0x06010DBB RID: 69051 RVA: 0x003A0EA8 File Offset: 0x0039F0A8
			public override void Clear()
			{
				foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveColumnBuilders)
				{
					primitiveColumnBuilder.Clear();
				}
			}

			// Token: 0x06010DBC RID: 69052 RVA: 0x003A0EF8 File Offset: 0x0039F0F8
			public override void Dispose()
			{
				foreach (ParquetRecordWriter.PrimitiveColumnBuilder primitiveColumnBuilder in this.primitiveColumnBuilders)
				{
					primitiveColumnBuilder.Dispose();
				}
			}

			// Token: 0x04006545 RID: 25925
			private readonly Dictionary<string, ParquetRecordWriter.ColumnBuilder> childColumnBuilders;

			// Token: 0x04006546 RID: 25926
			private readonly List<ParquetRecordWriter.PrimitiveColumnBuilder> primitiveColumnBuilders;
		}

		// Token: 0x02001F5D RID: 8029
		private abstract class PrimitiveColumnBuilder : ParquetRecordWriter.ColumnBuilder
		{
			// Token: 0x06010DC0 RID: 69056 RVA: 0x003A0F5C File Offset: 0x0039F15C
			protected PrimitiveColumnBuilder(PrimitiveSchemaElement primitiveSchemaElement)
				: base(primitiveSchemaElement)
			{
			}

			// Token: 0x06010DC1 RID: 69057
			public abstract void AddValue(Value value);

			// Token: 0x06010DC2 RID: 69058
			public abstract void AddColumn(IColumn column, int rowCount);

			// Token: 0x06010DC3 RID: 69059
			public abstract void Write(ColumnWriter writer);

			// Token: 0x06010DC4 RID: 69060 RVA: 0x003A0F65 File Offset: 0x0039F165
			public override IEnumerable<ParquetRecordWriter.PrimitiveColumnBuilder> GetPrimitiveColumnBuilders()
			{
				yield return this;
				yield break;
			}
		}

		// Token: 0x02001F5F RID: 8031
		private abstract class PrimitiveColumnBuilder<T> : ParquetRecordWriter.PrimitiveColumnBuilder
		{
			// Token: 0x06010DCD RID: 69069 RVA: 0x003A1028 File Offset: 0x0039F228
			protected PrimitiveColumnBuilder(PrimitiveSchemaElement schemaElement, ParquetPrimitiveTypeMap<T> typeMap)
				: base(schemaElement)
			{
				this.typeMap = typeMap;
				this.buffer = ParquetColumnBuffer.CreateDense<T>(schemaElement.DefinitionLevel, schemaElement.RepetitionLevel, 4096);
				this.uncommittedBuffers = new List<IParquetColumnBuffer<T>>(0);
				this.committedBuffers = new List<IParquetColumnBuffer<T>>(0);
				this.pool = new List<IParquetColumnBuffer<T>>(0);
				this.nextCommittedBufferToWrite = 0;
				this.Rollback();
				PhysicalType physicalType = typeMap.PhysicalType;
				if (physicalType - PhysicalType.ByteArray <= 1)
				{
					this.allocator = new ParquetRecordWriter.PrimitiveColumnBuilder<T>.Allocator(new ByteBuffer(8192, 16));
					return;
				}
				this.allocator = null;
			}

			// Token: 0x06010DCE RID: 69070 RVA: 0x003A10BC File Offset: 0x0039F2BC
			public new static ParquetRecordWriter.ColumnBuilder Create(PrimitiveSchemaElement schemaElement)
			{
				ParquetPrimitiveTypeMap<T> parquetPrimitiveTypeMap = (ParquetPrimitiveTypeMap<T>)schemaElement.TypeMap;
				if (schemaElement.DefinitionLevel == 0 && schemaElement.RepetitionLevel == 0)
				{
					return new ParquetRecordWriter.PrimitiveColumnBuilder<T>.NoLevelsPrimitiveColumnBuilder(schemaElement, parquetPrimitiveTypeMap);
				}
				return new ParquetRecordWriter.PrimitiveColumnBuilder<T>.WithLevelsPrimitiveColumnBuilder(schemaElement, parquetPrimitiveTypeMap);
			}

			// Token: 0x06010DCF RID: 69071 RVA: 0x003A10F4 File Offset: 0x0039F2F4
			public override void AddLevels(short definitionLevel, short repetitionLevel)
			{
				if (this.buffer.DefinitionLevels != null)
				{
					if (this.levelsNextIndex >= this.buffer.DefinitionLevels.Length)
					{
						this.Grow();
					}
					this.buffer.DefinitionLevels[this.levelsNextIndex] = definitionLevel;
				}
				if (this.buffer.RepetitionLevels != null)
				{
					if (this.levelsNextIndex >= this.buffer.RepetitionLevels.Length)
					{
						this.Grow();
					}
					this.buffer.RepetitionLevels[this.levelsNextIndex] = repetitionLevel;
				}
				this.levelsNextIndex++;
			}

			// Token: 0x06010DD0 RID: 69072 RVA: 0x003A1188 File Offset: 0x0039F388
			public override void AddValue(Value value)
			{
				if (this.valuesNextIndex >= this.buffer.Values.Length)
				{
					this.Grow();
				}
				if (value.IsNull && !base.SchemaElement.TypeValue.IsNullable)
				{
					throw ValueException.NewDataFormatError<Message0>(Resources.NullInNotNullColumn, Value.Null, null);
				}
				this.buffer.Values[this.valuesNextIndex] = this.typeMap.FromValue(this.allocator, value);
				this.valuesNextIndex++;
			}

			// Token: 0x06010DD1 RID: 69073 RVA: 0x003A1214 File Offset: 0x0039F414
			public override void Commit()
			{
				ParquetRecordWriter.PrimitiveColumnBuilder<T>.SafeBufferListReserve(this.committedBuffers, this.uncommittedBuffers.Count);
				this.committedBuffers.AddRange(this.uncommittedBuffers);
				this.committedBufferEstimatedSize += this.uncommittedBufferEstimatedSize;
				this.uncommittedBuffers.Clear();
				this.uncommittedBufferEstimatedSize = 0L;
				this.CommitCurrentBuffer();
			}

			// Token: 0x06010DD2 RID: 69074 RVA: 0x003A1274 File Offset: 0x0039F474
			public override void Rollback()
			{
				ParquetRecordWriter.PrimitiveColumnBuilder<T>.SafeBufferListReserve(this.pool, this.uncommittedBuffers.Count);
				foreach (IParquetColumnBuffer<T> parquetColumnBuffer in this.uncommittedBuffers)
				{
					parquetColumnBuffer.Clear();
					this.pool.Add(parquetColumnBuffer);
				}
				this.uncommittedBuffers.Clear();
				this.uncommittedBufferEstimatedSize = 0L;
				this.valuesNextIndex = this.buffer.ValuesOffset + this.buffer.ValuesCount;
				this.levelsNextIndex = this.buffer.LevelsOffset + this.buffer.LevelsCount;
			}

			// Token: 0x06010DD3 RID: 69075 RVA: 0x003A1338 File Offset: 0x0039F538
			public override long EstimateSize()
			{
				ParquetRecordWriter.PrimitiveColumnBuilder<T>.Allocator allocator = this.allocator;
				long num = ((allocator != null) ? allocator.AllocatedBytes : 0L);
				num += this.committedBufferEstimatedSize;
				num += this.uncommittedBufferEstimatedSize;
				if (this.buffer.DefinitionLevels != null)
				{
					num += (long)(this.levelsNextIndex * 2);
				}
				if (this.buffer.RepetitionLevels != null)
				{
					num += (long)(this.levelsNextIndex * 2);
				}
				return num + (long)(this.valuesNextIndex * ParquetRecordWriter.PrimitiveColumnBuilder<T>.valueSize);
			}

			// Token: 0x06010DD4 RID: 69076 RVA: 0x003A13B0 File Offset: 0x0039F5B0
			public override void Write(ColumnWriter writer)
			{
				while (this.nextCommittedBufferToWrite < this.committedBuffers.Count)
				{
					this.committedBuffers[this.nextCommittedBufferToWrite].Write(writer);
					this.nextCommittedBufferToWrite++;
				}
				this.buffer.Write(writer);
				this.nextCommittedBufferToWrite = this.committedBuffers.Count;
			}

			// Token: 0x06010DD5 RID: 69077 RVA: 0x003A1414 File Offset: 0x0039F614
			public override void Clear()
			{
				this.buffer.Clear();
				this.Rollback();
				ParquetRecordWriter.PrimitiveColumnBuilder<T>.SafeBufferListReserve(this.pool, this.committedBuffers.Count);
				foreach (IParquetColumnBuffer<T> parquetColumnBuffer in this.committedBuffers)
				{
					parquetColumnBuffer.Clear();
					this.pool.Add(parquetColumnBuffer);
				}
				this.committedBuffers.Clear();
				this.committedBufferEstimatedSize = 0L;
				this.nextCommittedBufferToWrite = 0;
				if (this.allocator != null)
				{
					this.allocator.Clear();
				}
			}

			// Token: 0x06010DD6 RID: 69078 RVA: 0x003A14C8 File Offset: 0x0039F6C8
			public override void Dispose()
			{
				if (this.allocator != null)
				{
					this.allocator.Dispose();
				}
				this.buffer = null;
				this.committedBuffers.Clear();
				this.committedBuffers = null;
				this.committedBufferEstimatedSize = 0L;
				this.nextCommittedBufferToWrite = 0;
				this.uncommittedBuffers.Clear();
				this.uncommittedBuffers = null;
				this.uncommittedBufferEstimatedSize = 0L;
				this.pool.Clear();
				this.pool = null;
			}

			// Token: 0x17002CB4 RID: 11444
			// (get) Token: 0x06010DD7 RID: 69079 RVA: 0x003A153C File Offset: 0x0039F73C
			private int CurrentBufferUncommittedValuesCount
			{
				get
				{
					return this.valuesNextIndex - (this.buffer.ValuesOffset + this.buffer.ValuesCount);
				}
			}

			// Token: 0x17002CB5 RID: 11445
			// (get) Token: 0x06010DD8 RID: 69080 RVA: 0x003A155C File Offset: 0x0039F75C
			private int CurrentBufferUncommittedLevelsCount
			{
				get
				{
					if (this.buffer.DefinitionLevels == null && this.buffer.RepetitionLevels == null)
					{
						return this.CurrentBufferUncommittedValuesCount;
					}
					return this.levelsNextIndex - (this.buffer.LevelsOffset + this.buffer.LevelsCount);
				}
			}

			// Token: 0x06010DD9 RID: 69081 RVA: 0x003A15A8 File Offset: 0x0039F7A8
			private void CommitCurrentBuffer()
			{
				if (this.buffer.DefinitionLevels != null || this.buffer.RepetitionLevels != null)
				{
					this.buffer.LevelsCount = this.levelsNextIndex - this.buffer.LevelsOffset;
				}
				this.buffer.ValuesCount = this.valuesNextIndex - this.buffer.ValuesOffset;
			}

			// Token: 0x06010DDA RID: 69082 RVA: 0x003A160C File Offset: 0x0039F80C
			private void Grow()
			{
				IParquetColumnBuffer<T> newBuffer = this.GetNewBuffer();
				int num = this.buffer.ValuesOffset + this.buffer.ValuesCount;
				int num2 = this.buffer.LevelsOffset + this.buffer.LevelsCount;
				if (num == 0 && num2 == 0)
				{
					this.CommitCurrentBuffer();
					ParquetRecordWriter.PrimitiveColumnBuilder<T>.SafeBufferListAdd(this.uncommittedBuffers, this.buffer);
					this.uncommittedBufferEstimatedSize += ParquetRecordWriter.PrimitiveColumnBuilder<T>.EstimateCommittedSizeOfBuffer(this.buffer);
					this.valuesNextIndex = 0;
					this.levelsNextIndex = 0;
					this.buffer = newBuffer;
					return;
				}
				Array.Copy(this.buffer.Values, num, newBuffer.Values, 0, this.CurrentBufferUncommittedValuesCount);
				if (this.buffer.DefinitionLevels != null)
				{
					Array.Copy(this.buffer.DefinitionLevels, num2, newBuffer.DefinitionLevels, 0, this.CurrentBufferUncommittedLevelsCount);
				}
				if (this.buffer.RepetitionLevels != null)
				{
					Array.Copy(this.buffer.RepetitionLevels, num2, newBuffer.RepetitionLevels, 0, this.CurrentBufferUncommittedLevelsCount);
				}
				ParquetRecordWriter.PrimitiveColumnBuilder<T>.SafeBufferListAdd(this.committedBuffers, this.buffer);
				this.committedBufferEstimatedSize += ParquetRecordWriter.PrimitiveColumnBuilder<T>.EstimateCommittedSizeOfBuffer(this.buffer);
				this.valuesNextIndex = this.CurrentBufferUncommittedValuesCount;
				this.levelsNextIndex = this.CurrentBufferUncommittedLevelsCount;
				this.buffer = newBuffer;
			}

			// Token: 0x06010DDB RID: 69083 RVA: 0x003A1758 File Offset: 0x0039F958
			private IParquetColumnBuffer<T> GetNewBuffer()
			{
				if (this.pool.Count == 0)
				{
					long num = (long)(4096 * ParquetRecordWriter.PrimitiveColumnBuilder<T>.valueSize);
					if (this.buffer.DefinitionLevels != null)
					{
						num += 8192L;
					}
					if (this.buffer.RepetitionLevels != null)
					{
						num += 8192L;
					}
					ParquetRecordWriter.PrimitiveColumnBuilder<T>.SafeBufferListReserve(this.pool, this.pool.Capacity + 1);
					using (ParquetRecordWriter.PrimitiveColumnBuilder<T>.TestAvailableManagedMemory(num))
					{
						return ParquetColumnBuffer.CreateDense<T>(this.buffer.MaxDefinitionLevel, this.buffer.MaxRepetitionLevel, 4096, 4096);
					}
				}
				int num2 = this.pool.Count - 1;
				IParquetColumnBuffer<T> parquetColumnBuffer = this.pool[num2];
				this.pool.RemoveAt(num2);
				return parquetColumnBuffer;
			}

			// Token: 0x06010DDC RID: 69084 RVA: 0x003A1838 File Offset: 0x0039FA38
			private static long EstimateCommittedSizeOfBuffer(IParquetColumnBuffer<T> buffer)
			{
				long num = 0L;
				if (buffer.DefinitionLevels != null)
				{
					num += (long)((buffer.LevelsOffset + buffer.LevelsCount) * 2);
				}
				if (buffer.RepetitionLevels != null)
				{
					num += (long)((buffer.LevelsOffset + buffer.LevelsCount) * 2);
				}
				return num + (long)((buffer.LevelsOffset + buffer.LevelsCount) * ParquetRecordWriter.PrimitiveColumnBuilder<T>.valueSize);
			}

			// Token: 0x06010DDD RID: 69085 RVA: 0x003A1896 File Offset: 0x0039FA96
			private static MemoryFailPoint TestAvailableManagedMemory(long bytesNeeded)
			{
				if (!MemoryPool.GetDefaultMemoryPool().TestAvailableMemory(16777216L))
				{
					throw new InsufficientMemoryException();
				}
				return new MemoryFailPoint((int)(1L + (bytesNeeded - 1L) / 1048576L) + 16);
			}

			// Token: 0x06010DDE RID: 69086 RVA: 0x003A18C7 File Offset: 0x0039FAC7
			private static void SafeBufferListAdd(List<IParquetColumnBuffer<T>> list, IParquetColumnBuffer<T> bufferToAdd)
			{
				ParquetRecordWriter.PrimitiveColumnBuilder<T>.SafeBufferListReserve(list, 1);
				list.Add(bufferToAdd);
			}

			// Token: 0x06010DDF RID: 69087 RVA: 0x003A18D8 File Offset: 0x0039FAD8
			private static void SafeBufferListReserve(List<IParquetColumnBuffer<T>> list, int itemsToAdd)
			{
				int num = list.Count + itemsToAdd;
				if (num > list.Capacity)
				{
					if (list.Capacity > 0 || num > 4096)
					{
						num = (1 + (num - 1) / 4096) * 4096;
					}
					using (ParquetRecordWriter.PrimitiveColumnBuilder<T>.TestAvailableManagedMemory((long)(num * ParquetRecordWriter.PrimitiveColumnBuilder<T>.ptrSize)))
					{
						list.Capacity = num;
					}
				}
			}

			// Token: 0x0400654D RID: 25933
			private const int maxLength = 1073741823;

			// Token: 0x0400654E RID: 25934
			private static readonly int valueSize = Marshal.SizeOf(typeof(T));

			// Token: 0x0400654F RID: 25935
			private static int ptrSize = Marshal.SizeOf(typeof(IntPtr));

			// Token: 0x04006550 RID: 25936
			private readonly ParquetPrimitiveTypeMap<T> typeMap;

			// Token: 0x04006551 RID: 25937
			private readonly ParquetRecordWriter.PrimitiveColumnBuilder<T>.Allocator allocator;

			// Token: 0x04006552 RID: 25938
			private IParquetColumnBuffer<T> buffer;

			// Token: 0x04006553 RID: 25939
			private List<IParquetColumnBuffer<T>> uncommittedBuffers;

			// Token: 0x04006554 RID: 25940
			private List<IParquetColumnBuffer<T>> committedBuffers;

			// Token: 0x04006555 RID: 25941
			private List<IParquetColumnBuffer<T>> pool;

			// Token: 0x04006556 RID: 25942
			private long committedBufferEstimatedSize;

			// Token: 0x04006557 RID: 25943
			private long uncommittedBufferEstimatedSize;

			// Token: 0x04006558 RID: 25944
			private int nextCommittedBufferToWrite;

			// Token: 0x04006559 RID: 25945
			private int valuesNextIndex;

			// Token: 0x0400655A RID: 25946
			private int levelsNextIndex;

			// Token: 0x02001F60 RID: 8032
			private sealed class Allocator : IAllocator, IDisposable
			{
				// Token: 0x06010DE1 RID: 69089 RVA: 0x003A1976 File Offset: 0x0039FB76
				public Allocator(ByteBuffer buffer)
				{
					this.buffer = buffer;
					this.allocatedBytes = 0L;
				}

				// Token: 0x17002CB6 RID: 11446
				// (get) Token: 0x06010DE2 RID: 69090 RVA: 0x003A198D File Offset: 0x0039FB8D
				public long AllocatedBytes
				{
					get
					{
						return this.allocatedBytes;
					}
				}

				// Token: 0x06010DE3 RID: 69091 RVA: 0x003A1995 File Offset: 0x0039FB95
				public ByteArray Allocate(int length)
				{
					ByteArray byteArray = this.buffer.Allocate(length);
					this.allocatedBytes += (long)length;
					return byteArray;
				}

				// Token: 0x06010DE4 RID: 69092 RVA: 0x003A19B2 File Offset: 0x0039FBB2
				public void Clear()
				{
					this.buffer.FreeAllAndReuse();
					this.allocatedBytes = 0L;
				}

				// Token: 0x06010DE5 RID: 69093 RVA: 0x003A19C7 File Offset: 0x0039FBC7
				public void Dispose()
				{
					this.buffer.Dispose();
					this.allocatedBytes = 0L;
				}

				// Token: 0x0400655B RID: 25947
				private readonly ByteBuffer buffer;

				// Token: 0x0400655C RID: 25948
				private long allocatedBytes;
			}

			// Token: 0x02001F61 RID: 8033
			private sealed class NoLevelsPrimitiveColumnBuilder : ParquetRecordWriter.PrimitiveColumnBuilder<T>
			{
				// Token: 0x06010DE6 RID: 69094 RVA: 0x003A19DC File Offset: 0x0039FBDC
				public NoLevelsPrimitiveColumnBuilder(PrimitiveSchemaElement schemaElement, ParquetPrimitiveTypeMap<T> typeMap)
					: base(schemaElement, typeMap)
				{
				}

				// Token: 0x06010DE7 RID: 69095 RVA: 0x0000CC37 File Offset: 0x0000AE37
				public override void AddLevels(short definitionLevel, short repetitionLevel)
				{
				}

				// Token: 0x06010DE8 RID: 69096 RVA: 0x003A19E8 File Offset: 0x0039FBE8
				public override void AddColumn(IColumn column, int rowCount)
				{
					while (this.valuesNextIndex + rowCount - 1 >= this.buffer.Values.Length)
					{
						base.Grow();
					}
					for (int i = 0; i < rowCount; i++)
					{
						this.buffer.Values[this.valuesNextIndex] = this.typeMap.GetFromColumn(this.allocator, column, i);
						this.valuesNextIndex++;
					}
				}
			}

			// Token: 0x02001F62 RID: 8034
			private sealed class WithLevelsPrimitiveColumnBuilder : ParquetRecordWriter.PrimitiveColumnBuilder<T>
			{
				// Token: 0x06010DE9 RID: 69097 RVA: 0x003A19DC File Offset: 0x0039FBDC
				public WithLevelsPrimitiveColumnBuilder(PrimitiveSchemaElement schemaElement, ParquetPrimitiveTypeMap<T> typeMap)
					: base(schemaElement, typeMap)
				{
				}

				// Token: 0x06010DEA RID: 69098 RVA: 0x003A1A5C File Offset: 0x0039FC5C
				public override void AddColumn(IColumn column, int rowCount)
				{
					while (this.levelsNextIndex + rowCount - 1 >= this.buffer.DefinitionLevels.Length)
					{
						base.Grow();
					}
					while (this.valuesNextIndex + rowCount - 1 >= this.buffer.Values.Length)
					{
						base.Grow();
					}
					for (int i = 0; i < rowCount; i++)
					{
						if (column.IsNull(i))
						{
							this.buffer.DefinitionLevels[this.levelsNextIndex] = base.NullDefinitionLevel;
						}
						else
						{
							this.buffer.DefinitionLevels[this.levelsNextIndex] = base.NonNullDefinitionLevel;
							this.buffer.Values[this.valuesNextIndex] = this.typeMap.GetFromColumn(this.allocator, column, i);
							this.valuesNextIndex++;
						}
						this.levelsNextIndex++;
					}
				}
			}
		}
	}
}
