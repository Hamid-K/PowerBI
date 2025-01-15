using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Parquet.Interop;
using Microsoft.Mashup.Engine1.Library.Parquet.Schema;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F48 RID: 8008
	internal class ParquetRecordReader : IDisposable
	{
		// Token: 0x06010D2B RID: 68907 RVA: 0x0039F298 File Offset: 0x0039D498
		private ParquetRecordReader(ParquetRowGroupsEnumerator rowGroupsEnumerator, ParquetRecordReader.SchemaInfo info, IDictionary<ParquetRecordReader.ReaderPosition, int> transitionMap, RowCount skipCount, int bufferSize)
		{
			this.rowGroupsEnumerator = rowGroupsEnumerator;
			this.fields = info.Root.SchemaElement.PrimitiveElements;
			this.info = info;
			this.transitionMap = transitionMap;
			this.columnEnumerators = new ParquetRecordReader.ColumnEnumerator[this.fields.Count];
			this.fieldElements = new ParquetRecordReader.IElementInfo[this.fields.Count];
			this.skipCount = skipCount;
			for (int i = 0; i < this.fields.Count; i++)
			{
				this.columnEnumerators[i] = ParquetRecordReader.ColumnEnumerator.Create(this.fields[i], bufferSize);
				this.fieldElements[i] = info.Element(this.fields[i]);
			}
		}

		// Token: 0x06010D2C RID: 68908 RVA: 0x0039F358 File Offset: 0x0039D558
		public static ParquetRecordReader New(StreamOwningParquetFileReader fileReader, SchemaElement schema, int[] columnSelection, RowCount skipCount)
		{
			IEnumerable<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>> enumerable = schema.PrimitiveElements.SelectMany((PrimitiveSchemaElement element1) => schema.PrimitiveElements.Select((PrimitiveSchemaElement element2) => new Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>(element1, element2)));
			ParquetRecordReader.SchemaInfo schemaInfo = ParquetRecordReader.SchemaInfo.Process(schema, enumerable);
			IDictionary<ParquetRecordReader.ReaderPosition, int> dictionary = ParquetRecordReader.CreateTransitionMap(schema.PrimitiveElements, schemaInfo);
			return new ParquetRecordReader(new ParquetRowGroupsEnumerator(fileReader, columnSelection), schemaInfo, dictionary, skipCount, ParquetRecordReader.BufferSize(schemaInfo.Root.SchemaElement.PrimitiveElements.Select((PrimitiveSchemaElement element) => element.TypeMap)));
		}

		// Token: 0x17002C8B RID: 11403
		// (get) Token: 0x06010D2D RID: 68909 RVA: 0x0039F3F6 File Offset: 0x0039D5F6
		public string CurrentFieldName
		{
			get
			{
				return this.currentElement.Name;
			}
		}

		// Token: 0x17002C8C RID: 11404
		// (get) Token: 0x06010D2E RID: 68910 RVA: 0x0039F403 File Offset: 0x0039D603
		public Value CurrentFieldValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x17002C8D RID: 11405
		// (get) Token: 0x06010D2F RID: 68911 RVA: 0x0039F40B File Offset: 0x0039D60B
		public SchemaElement CurrentSchemaElement
		{
			get
			{
				return this.currentElement.SchemaElement;
			}
		}

		// Token: 0x17002C8E RID: 11406
		// (get) Token: 0x06010D30 RID: 68912 RVA: 0x0039F418 File Offset: 0x0039D618
		public ParquetRecordReaderState State
		{
			get
			{
				if (this.end)
				{
					return ParquetRecordReaderState.End;
				}
				if (this.currentStateEnumerator == null)
				{
					return ParquetRecordReaderState.Start;
				}
				return this.currentStateEnumerator.Current;
			}
		}

		// Token: 0x06010D31 RID: 68913 RVA: 0x0039F43C File Offset: 0x0039D63C
		public void Read()
		{
			if (this.end)
			{
				return;
			}
			while (this.currentStateEnumerator == null || !this.currentStateEnumerator.MoveNext())
			{
				if (this.currentStateEnumerator != null)
				{
					this.currentStateEnumerator.Dispose();
					this.currentStateEnumerator = null;
				}
				while (!this.columnEnumerators[0].HasData)
				{
					if (!this.rowGroupsEnumerator.MoveNext(this.skipCount, out this.skipCount))
					{
						this.end = true;
						return;
					}
					for (int i = 0; i < this.columnEnumerators.Length; i++)
					{
						this.columnEnumerators[i].ColumnReader = this.rowGroupsEnumerator.CurrentColumnReaders[i];
						this.columnEnumerators[i].MoveNext();
					}
				}
				if (!this.skipCount.IsZero)
				{
					for (long num = this.skipCount.Value; num > 0L; num -= 1L)
					{
						using (IEnumerator<ParquetRecordReaderState> enumerator = this.ReadRecord(this.columnEnumerators))
						{
							while (enumerator.MoveNext())
							{
							}
						}
					}
					this.skipCount = RowCount.Zero;
				}
				this.currentStateEnumerator = this.ReadRecord(this.columnEnumerators);
			}
		}

		// Token: 0x06010D32 RID: 68914 RVA: 0x0039F56C File Offset: 0x0039D76C
		public void Dispose()
		{
			if (this.currentStateEnumerator != null)
			{
				this.currentStateEnumerator.Dispose();
				this.currentStateEnumerator = null;
				this.end = true;
			}
			ParquetRecordReader.ColumnEnumerator[] array = this.columnEnumerators;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Dispose();
			}
			this.rowGroupsEnumerator.Dispose();
		}

		// Token: 0x06010D33 RID: 68915 RVA: 0x0039F5C2 File Offset: 0x0039D7C2
		private IEnumerator<ParquetRecordReaderState> ReadRecord(ParquetRecordReader.ColumnEnumerator[] columnEnumerators)
		{
			this.currentElement = this.info.Root;
			yield return ParquetRecordReaderState.RecordStart;
			this.currentField = 0;
			int nextField = 0;
			bool repeat = false;
			ParquetRecordReader.ColumnEnumerator currentColumnEnumerator = columnEnumerators[nextField];
			IEnumerator<ParquetRecordReaderState> moveToDefinitionLevelEnumerator;
			while (currentColumnEnumerator.HasData)
			{
				if (repeat && this.currentElement.SchemaElement is GroupSchemaElement)
				{
					yield return ParquetRecordReaderState.NestedRecordStart;
				}
				repeat = false;
				ParquetRecordReader.ColumnElement element = currentColumnEnumerator.Current;
				ParquetRecordReader.IElementInfo elementInfo = this.fieldElements[nextField].AncestorAtDefinitionLevel(element.DefinitionLevel);
				using (IEnumerator<ParquetRecordReaderState> moveToDefinitionLevelEnumerator = this.MoveToLevel(elementInfo.TreeLevel, nextField))
				{
					while (moveToDefinitionLevelEnumerator.MoveNext())
					{
						ParquetRecordReaderState parquetRecordReaderState = moveToDefinitionLevelEnumerator.Current;
						yield return parquetRecordReaderState;
					}
				}
				moveToDefinitionLevelEnumerator = null;
				if (!element.Value.IsNull)
				{
					this.currentValue = element.Value;
					yield return ParquetRecordReaderState.Primitive;
				}
				currentColumnEnumerator.MoveNext();
				short num = (currentColumnEnumerator.HasData ? currentColumnEnumerator.Current.RepetitionLevel : 0);
				ParquetRecordReader.ReaderPosition readerPosition = new ParquetRecordReader.ReaderPosition(nextField, num);
				nextField = this.transitionMap[readerPosition];
				if (nextField >= this.fields.Count)
				{
					break;
				}
				currentColumnEnumerator = columnEnumerators[nextField];
				repeat = nextField <= readerPosition.Field;
				short num2 = (repeat ? this.currentElement.AncestorAtRepetitionLevel(num).TreeLevel : this.fieldElements[nextField].TreeLevel);
				using (IEnumerator<ParquetRecordReaderState> moveToDefinitionLevelEnumerator = this.ReturnToLevel((int)num2))
				{
					while (moveToDefinitionLevelEnumerator.MoveNext())
					{
						ParquetRecordReaderState parquetRecordReaderState2 = moveToDefinitionLevelEnumerator.Current;
						yield return parquetRecordReaderState2;
					}
				}
				moveToDefinitionLevelEnumerator = null;
				if (repeat && this.currentElement.SchemaElement is GroupSchemaElement)
				{
					yield return ParquetRecordReaderState.NestedRecordEnd;
				}
				element = default(ParquetRecordReader.ColumnElement);
			}
			using (IEnumerator<ParquetRecordReaderState> moveToDefinitionLevelEnumerator = this.ReturnToLevel(0))
			{
				while (moveToDefinitionLevelEnumerator.MoveNext())
				{
					ParquetRecordReaderState parquetRecordReaderState3 = moveToDefinitionLevelEnumerator.Current;
					yield return parquetRecordReaderState3;
				}
			}
			moveToDefinitionLevelEnumerator = null;
			yield return ParquetRecordReaderState.RecordEnd;
			yield break;
			yield break;
		}

		// Token: 0x06010D34 RID: 68916 RVA: 0x0039F5D8 File Offset: 0x0039D7D8
		private IEnumerator<ParquetRecordReaderState> MoveToLevel(short newLevel, int nextField)
		{
			ParquetRecordReader.IElementInfo elementInfo = this.fieldElements[this.currentField];
			ParquetRecordReader.IElementInfo nextFieldElement = this.fieldElements[nextField];
			ParquetRecordReader.IElementInfo elementInfo2 = this.info.LowestCommonAncestor(elementInfo.SchemaElement, nextFieldElement.SchemaElement);
			using (IEnumerator<ParquetRecordReaderState> enumerator = this.ReturnToLevel((int)elementInfo2.TreeLevel))
			{
				while (enumerator.MoveNext())
				{
					ParquetRecordReaderState parquetRecordReaderState = enumerator.Current;
					yield return parquetRecordReaderState;
				}
			}
			IEnumerator<ParquetRecordReaderState> enumerator = null;
			ParquetRecordReader.IElementInfo[] ancestors = nextFieldElement.Ancestors;
			this.currentField = nextField;
			int offset = (int)(this.currentElement.TreeLevel + 1);
			while (offset < ancestors.Length && ancestors[offset].TreeLevel <= newLevel)
			{
				this.currentElement = ancestors[offset];
				if (this.currentElement.Repetition == Repetition.Repeated)
				{
					yield return ParquetRecordReaderState.NestedListStart;
				}
				if (this.currentElement.SchemaElement is GroupSchemaElement)
				{
					yield return ParquetRecordReaderState.NestedRecordStart;
				}
				int num = offset;
				offset = num + 1;
			}
			yield break;
			yield break;
		}

		// Token: 0x06010D35 RID: 68917 RVA: 0x0039F5F5 File Offset: 0x0039D7F5
		private IEnumerator<ParquetRecordReaderState> ReturnToLevel(int newLevel)
		{
			while ((int)this.currentElement.TreeLevel > newLevel)
			{
				if (this.currentElement.SchemaElement is GroupSchemaElement)
				{
					yield return ParquetRecordReaderState.NestedRecordEnd;
				}
				if (this.currentElement.Repetition == Repetition.Repeated)
				{
					yield return ParquetRecordReaderState.NestedListEnd;
				}
				this.currentElement = this.currentElement.Parent;
			}
			yield break;
		}

		// Token: 0x06010D36 RID: 68918 RVA: 0x0039F60C File Offset: 0x0039D80C
		private static ColumnDescriptor[] GetColumns(SchemaDescriptor schemaDescriptor, int[] columnSelection)
		{
			ColumnDescriptor[] array = new ColumnDescriptor[columnSelection.Length];
			for (int i = 0; i < columnSelection.Length; i++)
			{
				ColumnDescriptor columnDescriptor = schemaDescriptor.Column(columnSelection[i]);
				array[i] = columnDescriptor;
			}
			return array;
		}

		// Token: 0x06010D37 RID: 68919 RVA: 0x0039F640 File Offset: 0x0039D840
		private static IDictionary<ParquetRecordReader.ReaderPosition, int> CreateTransitionMap(IList<PrimitiveSchemaElement> fields, ParquetRecordReader.SchemaInfo info)
		{
			Dictionary<ParquetRecordReader.ReaderPosition, int> dictionary = new Dictionary<ParquetRecordReader.ReaderPosition, int>();
			for (int i = 0; i < fields.Count; i++)
			{
				SchemaElement schemaElement = fields[i];
				short repetitionLevel = schemaElement.RepetitionLevel;
				int num = i + 1;
				short num2 = ((num < fields.Count) ? info.CommonRepetitionLevel(schemaElement, fields[num]) : 0);
				for (int j = i; j >= 0; j--)
				{
					SchemaElement schemaElement2 = fields[j];
					if (schemaElement2.RepetitionLevel > num2)
					{
						short num3 = info.CommonRepetitionLevel(schemaElement2, schemaElement);
						dictionary[new ParquetRecordReader.ReaderPosition(i, num3)] = j;
					}
				}
				short num4;
				for (num4 = 0; num4 <= num2; num4 += 1)
				{
					dictionary[new ParquetRecordReader.ReaderPosition(i, num4)] = num;
				}
				int num5 = i;
				while (num4 <= repetitionLevel)
				{
					int num6;
					if (!dictionary.TryGetValue(new ParquetRecordReader.ReaderPosition(i, num4), out num6))
					{
						dictionary.Add(new ParquetRecordReader.ReaderPosition(i, num4), num5);
					}
					else
					{
						num5 = num6;
					}
					num4 += 1;
				}
			}
			return dictionary;
		}

		// Token: 0x06010D38 RID: 68920 RVA: 0x0039F738 File Offset: 0x0039D938
		private static int BufferSize(IEnumerable<ParquetTypeMap> typeMaps)
		{
			int num = 0;
			foreach (ParquetTypeMap parquetTypeMap in typeMaps)
			{
				num += ParquetRecordReader.EstimateColumnSize((ParquetPrimitiveTypeMap)parquetTypeMap);
			}
			return Math.Max(Math.Min(262144 / Math.Max(num, 1), 4096), 1);
		}

		// Token: 0x06010D39 RID: 68921 RVA: 0x0039F7A8 File Offset: 0x0039D9A8
		private static int EstimateColumnSize(ParquetPrimitiveTypeMap typeMap)
		{
			switch (typeMap.PhysicalType)
			{
			case PhysicalType.Boolean:
				return 1;
			case PhysicalType.Int32:
				return 4;
			case PhysicalType.Int64:
				return 8;
			case PhysicalType.Int96:
				return 12;
			case PhysicalType.Float:
				return 4;
			case PhysicalType.Double:
				return 8;
			case PhysicalType.ByteArray:
				if (typeMap.LogicalTypeType != LogicalTypeEnum.String)
				{
					return 8192;
				}
				return 256;
			case PhysicalType.FixedLenByteArray:
				return Math.Min(typeMap.TypeLength.Value, 1073741823);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x040064E8 RID: 25832
		private const int TextLengthEstimate = 128;

		// Token: 0x040064E9 RID: 25833
		private const int BinaryLengthEstimate = 8192;

		// Token: 0x040064EA RID: 25834
		private const int MaxBufferedBytes = 262144;

		// Token: 0x040064EB RID: 25835
		private const int MaxRowsPerBuffer = 4096;

		// Token: 0x040064EC RID: 25836
		private readonly ParquetRowGroupsEnumerator rowGroupsEnumerator;

		// Token: 0x040064ED RID: 25837
		private readonly IList<PrimitiveSchemaElement> fields;

		// Token: 0x040064EE RID: 25838
		private readonly ParquetRecordReader.SchemaInfo info;

		// Token: 0x040064EF RID: 25839
		private readonly IDictionary<ParquetRecordReader.ReaderPosition, int> transitionMap;

		// Token: 0x040064F0 RID: 25840
		private readonly ParquetRecordReader.ColumnEnumerator[] columnEnumerators;

		// Token: 0x040064F1 RID: 25841
		private readonly ParquetRecordReader.IElementInfo[] fieldElements;

		// Token: 0x040064F2 RID: 25842
		private IEnumerator<ParquetRecordReaderState> currentStateEnumerator;

		// Token: 0x040064F3 RID: 25843
		private int currentField;

		// Token: 0x040064F4 RID: 25844
		private ParquetRecordReader.IElementInfo currentElement;

		// Token: 0x040064F5 RID: 25845
		private Value currentValue;

		// Token: 0x040064F6 RID: 25846
		private bool end;

		// Token: 0x040064F7 RID: 25847
		private RowCount skipCount;

		// Token: 0x02001F49 RID: 8009
		private abstract class ColumnEnumerator : IEnumerator<ParquetRecordReader.ColumnElement>, IDisposable, IEnumerator
		{
			// Token: 0x06010D3A RID: 68922 RVA: 0x0039F824 File Offset: 0x0039DA24
			public static ParquetRecordReader.ColumnEnumerator Create(PrimitiveSchemaElement schemaElement, int bufferSize)
			{
				switch (schemaElement.PhysicalType)
				{
				case PhysicalType.Boolean:
					return ParquetRecordReader.ColumnEnumerator<bool>.Create(schemaElement, bufferSize);
				case PhysicalType.Int32:
					return ParquetRecordReader.ColumnEnumerator<int>.Create(schemaElement, bufferSize);
				case PhysicalType.Int64:
					return ParquetRecordReader.ColumnEnumerator<long>.Create(schemaElement, bufferSize);
				case PhysicalType.Int96:
					return ParquetRecordReader.ColumnEnumerator<Int96>.Create(schemaElement, bufferSize);
				case PhysicalType.Float:
					return ParquetRecordReader.ColumnEnumerator<float>.Create(schemaElement, bufferSize);
				case PhysicalType.Double:
					return ParquetRecordReader.ColumnEnumerator<double>.Create(schemaElement, bufferSize);
				case PhysicalType.ByteArray:
					return ParquetRecordReader.ColumnEnumerator<ByteArray>.Create(schemaElement, bufferSize);
				case PhysicalType.FixedLenByteArray:
					return ParquetRecordReader.ColumnEnumerator<FixedLenByteArray>.Create(schemaElement, bufferSize);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17002C8F RID: 11407
			// (get) Token: 0x06010D3B RID: 68923 RVA: 0x0039F8A5 File Offset: 0x0039DAA5
			public bool HasData
			{
				get
				{
					return this.currentColumnEnumerator != null;
				}
			}

			// Token: 0x17002C90 RID: 11408
			// (get) Token: 0x06010D3C RID: 68924 RVA: 0x0039F8B0 File Offset: 0x0039DAB0
			public ParquetRecordReader.ColumnElement Current
			{
				get
				{
					return this.currentColumnEnumerator.Current;
				}
			}

			// Token: 0x17002C91 RID: 11409
			// (get) Token: 0x06010D3D RID: 68925 RVA: 0x0039F8BD File Offset: 0x0039DABD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17002C92 RID: 11410
			// (set) Token: 0x06010D3E RID: 68926
			public abstract ColumnReader ColumnReader { set; }

			// Token: 0x06010D3F RID: 68927 RVA: 0x0039F8CA File Offset: 0x0039DACA
			public void Dispose()
			{
				if (this.currentColumnEnumerator != null)
				{
					this.currentColumnEnumerator.Dispose();
					this.currentColumnEnumerator = null;
				}
			}

			// Token: 0x06010D40 RID: 68928 RVA: 0x0039F8E6 File Offset: 0x0039DAE6
			public bool MoveNext()
			{
				if (!this.currentColumnEnumerator.MoveNext())
				{
					this.Dispose();
					return false;
				}
				return true;
			}

			// Token: 0x06010D41 RID: 68929 RVA: 0x001D2D64 File Offset: 0x001D0F64
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x040064F8 RID: 25848
			protected IEnumerator<ParquetRecordReader.ColumnElement> currentColumnEnumerator;
		}

		// Token: 0x02001F4A RID: 8010
		private sealed class ColumnEnumerator<T> : ParquetRecordReader.ColumnEnumerator
		{
			// Token: 0x06010D43 RID: 68931 RVA: 0x0039F8FE File Offset: 0x0039DAFE
			private ColumnEnumerator(PrimitiveSchemaElement schemaElement, ParquetPrimitiveTypeMap<T> typeMap, int bufferSize)
			{
				this.buffer = ParquetColumnBuffer.CreateDense<T>(schemaElement.DefinitionLevel, schemaElement.RepetitionLevel, bufferSize);
				this.typeMap = typeMap;
			}

			// Token: 0x06010D44 RID: 68932 RVA: 0x0039F925 File Offset: 0x0039DB25
			public new static ParquetRecordReader.ColumnEnumerator<T> Create(PrimitiveSchemaElement schemaElement, int bufferSize)
			{
				return new ParquetRecordReader.ColumnEnumerator<T>(schemaElement, (ParquetPrimitiveTypeMap<T>)schemaElement.TypeMap, bufferSize);
			}

			// Token: 0x17002C93 RID: 11411
			// (set) Token: 0x06010D45 RID: 68933 RVA: 0x0039F939 File Offset: 0x0039DB39
			public override ColumnReader ColumnReader
			{
				set
				{
					base.Dispose();
					this.currentColumnEnumerator = ParquetRecordReader.ColumnEnumerator<T>.GetColumnElements(value, this.buffer, this.typeMap);
				}
			}

			// Token: 0x06010D46 RID: 68934 RVA: 0x0039F959 File Offset: 0x0039DB59
			private static IEnumerator<ParquetRecordReader.ColumnElement> GetColumnElements(ColumnReader columnReader, IParquetColumnBuffer<T> buffer, ParquetPrimitiveTypeMap<T> typeMap)
			{
				while (columnReader.HasNext)
				{
					buffer.Clear();
					buffer.Read(columnReader, int.MaxValue);
					int columnValuesOffset = 0;
					int num3;
					for (int columnLevelsOffset = 0; columnLevelsOffset < buffer.LevelsCount; columnLevelsOffset = num3 + 1)
					{
						short num = ((buffer.DefinitionLevels == null) ? 0 : buffer.DefinitionLevels[columnLevelsOffset]);
						short num2 = ((buffer.RepetitionLevels == null) ? 0 : buffer.RepetitionLevels[columnLevelsOffset]);
						if (num < buffer.MaxDefinitionLevel)
						{
							yield return new ParquetRecordReader.ColumnElement(Value.Null, num, num2);
						}
						else
						{
							yield return new ParquetRecordReader.ColumnElement(typeMap.ToValue(buffer.Values[columnValuesOffset]), num, num2);
							num3 = columnValuesOffset;
							columnValuesOffset = num3 + 1;
						}
						num3 = columnLevelsOffset;
					}
				}
				yield break;
			}

			// Token: 0x040064F9 RID: 25849
			private readonly IParquetColumnBuffer<T> buffer;

			// Token: 0x040064FA RID: 25850
			private readonly ParquetPrimitiveTypeMap<T> typeMap;
		}

		// Token: 0x02001F4C RID: 8012
		private struct ColumnElement
		{
			// Token: 0x06010D4D RID: 68941 RVA: 0x0039FB03 File Offset: 0x0039DD03
			public ColumnElement(Value value, short definitionLevel, short repetitionLevel)
			{
				this.Value = value;
				this.DefinitionLevel = definitionLevel;
				this.RepetitionLevel = repetitionLevel;
			}

			// Token: 0x04006502 RID: 25858
			public readonly Value Value;

			// Token: 0x04006503 RID: 25859
			public readonly short DefinitionLevel;

			// Token: 0x04006504 RID: 25860
			public readonly short RepetitionLevel;
		}

		// Token: 0x02001F4D RID: 8013
		private struct ReaderPosition : IEquatable<ParquetRecordReader.ReaderPosition>
		{
			// Token: 0x06010D4E RID: 68942 RVA: 0x0039FB1A File Offset: 0x0039DD1A
			public ReaderPosition(int field, short level)
			{
				this.Field = field;
				this.Level = level;
			}

			// Token: 0x06010D4F RID: 68943 RVA: 0x0039FB2A File Offset: 0x0039DD2A
			public bool Equals(ParquetRecordReader.ReaderPosition other)
			{
				return this.Field == other.Field && this.Level == other.Level;
			}

			// Token: 0x06010D50 RID: 68944 RVA: 0x0039FB4A File Offset: 0x0039DD4A
			public override bool Equals(object obj)
			{
				return obj is ParquetRecordReader.ReaderPosition && this.Equals((ParquetRecordReader.ReaderPosition)obj);
			}

			// Token: 0x06010D51 RID: 68945 RVA: 0x0039FB62 File Offset: 0x0039DD62
			public override int GetHashCode()
			{
				return this.Field.GetHashCode() ^ this.Level.GetHashCode();
			}

			// Token: 0x04006505 RID: 25861
			public int Field;

			// Token: 0x04006506 RID: 25862
			public short Level;
		}

		// Token: 0x02001F4E RID: 8014
		private interface IElementInfo
		{
			// Token: 0x17002C96 RID: 11414
			// (get) Token: 0x06010D52 RID: 68946
			string Name { get; }

			// Token: 0x17002C97 RID: 11415
			// (get) Token: 0x06010D53 RID: 68947
			Repetition Repetition { get; }

			// Token: 0x17002C98 RID: 11416
			// (get) Token: 0x06010D54 RID: 68948
			SchemaElement SchemaElement { get; }

			// Token: 0x17002C99 RID: 11417
			// (get) Token: 0x06010D55 RID: 68949
			ParquetRecordReader.IElementInfo Parent { get; }

			// Token: 0x17002C9A RID: 11418
			// (get) Token: 0x06010D56 RID: 68950
			ParquetRecordReader.IElementInfo[] Ancestors { get; }

			// Token: 0x17002C9B RID: 11419
			// (get) Token: 0x06010D57 RID: 68951
			short TreeLevel { get; }

			// Token: 0x17002C9C RID: 11420
			// (get) Token: 0x06010D58 RID: 68952
			short DefinitionLevel { get; }

			// Token: 0x17002C9D RID: 11421
			// (get) Token: 0x06010D59 RID: 68953
			short RepetitionLevel { get; }

			// Token: 0x06010D5A RID: 68954
			ParquetRecordReader.IElementInfo AncestorAtDefinitionLevel(short definitionLevel);

			// Token: 0x06010D5B RID: 68955
			ParquetRecordReader.IElementInfo AncestorAtRepetitionLevel(short repetitionLevel);
		}

		// Token: 0x02001F4F RID: 8015
		private class SchemaInfo
		{
			// Token: 0x06010D5C RID: 68956 RVA: 0x0039FB7B File Offset: 0x0039DD7B
			private SchemaInfo(IEnumerable<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>> lowestCommonAncestorsAsks)
			{
				this.elements = new Dictionary<SchemaElement, ParquetRecordReader.SchemaInfo.ElementInfo>();
				this.currentAncestors = new List<ParquetRecordReader.IElementInfo>();
				this.lowestCommonAncestorsAsks = lowestCommonAncestorsAsks;
				this.lowestCommonAncestors = new Dictionary<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>, ParquetRecordReader.IElementInfo>(ParquetRecordReader.SchemaInfo.UnorderedComparer);
			}

			// Token: 0x06010D5D RID: 68957 RVA: 0x0039FBB0 File Offset: 0x0039DDB0
			public static ParquetRecordReader.SchemaInfo Process(SchemaElement schema, IEnumerable<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>> lowestCommonAncestorsAsks)
			{
				ParquetRecordReader.SchemaInfo schemaInfo = new ParquetRecordReader.SchemaInfo(lowestCommonAncestorsAsks);
				ParquetRecordReader.IElementInfo elementInfo = schemaInfo.Process(schema.Name, schema);
				schemaInfo.root = elementInfo;
				return schemaInfo;
			}

			// Token: 0x17002C9E RID: 11422
			// (get) Token: 0x06010D5E RID: 68958 RVA: 0x0039FBD8 File Offset: 0x0039DDD8
			public ParquetRecordReader.IElementInfo Root
			{
				get
				{
					return this.root;
				}
			}

			// Token: 0x06010D5F RID: 68959 RVA: 0x0039FBE0 File Offset: 0x0039DDE0
			public ParquetRecordReader.IElementInfo LowestCommonAncestor(SchemaElement element1, SchemaElement element2)
			{
				return this.lowestCommonAncestors[new Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>(element1, element2)];
			}

			// Token: 0x06010D60 RID: 68960 RVA: 0x0039FBF4 File Offset: 0x0039DDF4
			public ParquetRecordReader.IElementInfo Element(SchemaElement element)
			{
				return this.elements[element];
			}

			// Token: 0x06010D61 RID: 68961 RVA: 0x0039FC02 File Offset: 0x0039DE02
			public short CommonRepetitionLevel(SchemaElement element1, SchemaElement element2)
			{
				return this.LowestCommonAncestor(element1, element2).RepetitionLevel;
			}

			// Token: 0x06010D62 RID: 68962 RVA: 0x0039FC14 File Offset: 0x0039DE14
			private ParquetRecordReader.SchemaInfo.ElementInfo Process(string name, SchemaElement element)
			{
				ParquetRecordReader.SchemaInfo.ElementInfo elementInfo = this.MakeSet(name, element);
				elementInfo.LCAAncestor = elementInfo;
				GroupSchemaElement groupSchemaElement = element as GroupSchemaElement;
				if (groupSchemaElement != null)
				{
					this.currentAncestors.Add(elementInfo);
					for (int i = 0; i < groupSchemaElement.Fields.Length; i++)
					{
						string text = groupSchemaElement.FieldKeys[i];
						SchemaElement schemaElement = groupSchemaElement.Fields[i];
						ParquetRecordReader.SchemaInfo.ElementInfo elementInfo2 = this.Process(text, schemaElement);
						elementInfo.Union(elementInfo2);
						elementInfo.Find().LCAAncestor = elementInfo;
					}
					this.currentAncestors.RemoveAt(this.currentAncestors.Count - 1);
				}
				elementInfo.LCAVisited = true;
				foreach (Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement> valueTuple in this.lowestCommonAncestorsAsks)
				{
					ParquetRecordReader.SchemaInfo.ElementInfo elementInfo3 = null;
					if (valueTuple.Item1 == elementInfo.SchemaElement)
					{
						elementInfo3 = this.Info(valueTuple.Item2);
					}
					else if (valueTuple.Item2 == elementInfo.SchemaElement)
					{
						elementInfo3 = this.Info(valueTuple.Item1);
					}
					if (elementInfo3 != null && elementInfo3.LCAVisited)
					{
						this.lowestCommonAncestors[valueTuple] = elementInfo3.Find().LCAAncestor;
					}
				}
				return elementInfo;
			}

			// Token: 0x06010D63 RID: 68963 RVA: 0x0039FD58 File Offset: 0x0039DF58
			private ParquetRecordReader.SchemaInfo.ElementInfo MakeSet(string name, SchemaElement element)
			{
				ParquetRecordReader.SchemaInfo.ElementInfo elementInfo = new ParquetRecordReader.SchemaInfo.ElementInfo(name, element, this.currentAncestors);
				elementInfo.MakeSet();
				this.elements.Add(elementInfo.SchemaElement, elementInfo);
				return elementInfo;
			}

			// Token: 0x06010D64 RID: 68964 RVA: 0x0039FD8C File Offset: 0x0039DF8C
			private ParquetRecordReader.SchemaInfo.ElementInfo Info(SchemaElement element)
			{
				return this.elements.GetValueOrDefault(element, null);
			}

			// Token: 0x04006507 RID: 25863
			private static readonly IEqualityComparer<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>> UnorderedComparer = new ParquetRecordReader.SchemaInfo.UnorderedPairComparer<SchemaElement>(null);

			// Token: 0x04006508 RID: 25864
			private readonly Dictionary<SchemaElement, ParquetRecordReader.SchemaInfo.ElementInfo> elements;

			// Token: 0x04006509 RID: 25865
			private readonly IEnumerable<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>> lowestCommonAncestorsAsks;

			// Token: 0x0400650A RID: 25866
			private readonly Dictionary<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<SchemaElement, SchemaElement>, ParquetRecordReader.IElementInfo> lowestCommonAncestors;

			// Token: 0x0400650B RID: 25867
			private ParquetRecordReader.IElementInfo root;

			// Token: 0x0400650C RID: 25868
			private List<ParquetRecordReader.IElementInfo> currentAncestors;

			// Token: 0x02001F50 RID: 8016
			private class ElementInfo : ParquetRecordReader.IElementInfo
			{
				// Token: 0x06010D66 RID: 68966 RVA: 0x0039FDA8 File Offset: 0x0039DFA8
				public ElementInfo(string name, SchemaElement element, IList<ParquetRecordReader.IElementInfo> ancestors)
				{
					this.name = name;
					this.element = element;
					this.parent = ((ancestors.Count > 0) ? ((ParquetRecordReader.SchemaInfo.ElementInfo)ancestors[ancestors.Count - 1]) : null);
					this.ancestors = new ParquetRecordReader.IElementInfo[ancestors.Count + 1];
					ancestors.CopyTo(this.ancestors, 0);
					this.ancestors[this.ancestors.Length - 1] = this;
					this.fullDefinitionLevelsMap = new ParquetRecordReader.IElementInfo[(int)(this.DefinitionLevel + 1)];
					this.fullRepetitionLevelsMap = new ParquetRecordReader.IElementInfo[(int)(this.RepetitionLevel + 1)];
					if (this.parent != null)
					{
						Array.Copy(this.parent.fullDefinitionLevelsMap, this.fullDefinitionLevelsMap, this.parent.fullDefinitionLevelsMap.Length);
						Array.Copy(this.parent.fullRepetitionLevelsMap, this.fullRepetitionLevelsMap, this.parent.fullRepetitionLevelsMap.Length);
					}
					this.fullDefinitionLevelsMap[(int)this.DefinitionLevel] = this;
					if (this.parent == null || this.Repetition == Repetition.Repeated)
					{
						this.fullRepetitionLevelsMap[(int)this.RepetitionLevel] = this;
					}
				}

				// Token: 0x17002C9F RID: 11423
				// (get) Token: 0x06010D67 RID: 68967 RVA: 0x0039FEC1 File Offset: 0x0039E0C1
				public string Name
				{
					get
					{
						return this.name;
					}
				}

				// Token: 0x17002CA0 RID: 11424
				// (get) Token: 0x06010D68 RID: 68968 RVA: 0x0039FEC9 File Offset: 0x0039E0C9
				public Repetition Repetition
				{
					get
					{
						return this.SchemaElement.Repetition;
					}
				}

				// Token: 0x17002CA1 RID: 11425
				// (get) Token: 0x06010D69 RID: 68969 RVA: 0x0039FED6 File Offset: 0x0039E0D6
				public SchemaElement SchemaElement
				{
					get
					{
						return this.element;
					}
				}

				// Token: 0x17002CA2 RID: 11426
				// (get) Token: 0x06010D6A RID: 68970 RVA: 0x0039FEDE File Offset: 0x0039E0DE
				public ParquetRecordReader.IElementInfo[] Ancestors
				{
					get
					{
						return this.ancestors;
					}
				}

				// Token: 0x17002CA3 RID: 11427
				// (get) Token: 0x06010D6B RID: 68971 RVA: 0x0039FEE6 File Offset: 0x0039E0E6
				public ParquetRecordReader.IElementInfo Parent
				{
					get
					{
						return this.parent;
					}
				}

				// Token: 0x17002CA4 RID: 11428
				// (get) Token: 0x06010D6C RID: 68972 RVA: 0x0039FEEE File Offset: 0x0039E0EE
				public short TreeLevel
				{
					get
					{
						return (short)(this.ancestors.Length - 1);
					}
				}

				// Token: 0x17002CA5 RID: 11429
				// (get) Token: 0x06010D6D RID: 68973 RVA: 0x0039FEFB File Offset: 0x0039E0FB
				public short DefinitionLevel
				{
					get
					{
						return this.SchemaElement.DefinitionLevel;
					}
				}

				// Token: 0x17002CA6 RID: 11430
				// (get) Token: 0x06010D6E RID: 68974 RVA: 0x0039FF08 File Offset: 0x0039E108
				public short RepetitionLevel
				{
					get
					{
						return this.SchemaElement.RepetitionLevel;
					}
				}

				// Token: 0x17002CA7 RID: 11431
				// (get) Token: 0x06010D6F RID: 68975 RVA: 0x0039FF15 File Offset: 0x0039E115
				// (set) Token: 0x06010D70 RID: 68976 RVA: 0x0039FF1D File Offset: 0x0039E11D
				public bool LCAVisited
				{
					get
					{
						return this.lcaVisited;
					}
					set
					{
						this.lcaVisited = value;
					}
				}

				// Token: 0x17002CA8 RID: 11432
				// (get) Token: 0x06010D71 RID: 68977 RVA: 0x0039FF26 File Offset: 0x0039E126
				// (set) Token: 0x06010D72 RID: 68978 RVA: 0x0039FF2E File Offset: 0x0039E12E
				public ParquetRecordReader.SchemaInfo.ElementInfo LCAAncestor
				{
					get
					{
						return this.lcaAncestor;
					}
					set
					{
						this.lcaAncestor = value;
					}
				}

				// Token: 0x06010D73 RID: 68979 RVA: 0x0039FF37 File Offset: 0x0039E137
				public void MakeSet()
				{
					this.unionFindParent = this;
					this.unionFindRank = 0;
				}

				// Token: 0x06010D74 RID: 68980 RVA: 0x0039FF47 File Offset: 0x0039E147
				public ParquetRecordReader.SchemaInfo.ElementInfo Find()
				{
					if (this.unionFindParent != this)
					{
						this.unionFindParent = this.unionFindParent.Find();
					}
					return this.unionFindParent;
				}

				// Token: 0x06010D75 RID: 68981 RVA: 0x0039FF69 File Offset: 0x0039E169
				public void Union(ParquetRecordReader.SchemaInfo.ElementInfo other)
				{
					this.Find().Link(other.Find());
				}

				// Token: 0x06010D76 RID: 68982 RVA: 0x0039FF7C File Offset: 0x0039E17C
				public ParquetRecordReader.IElementInfo AncestorAtDefinitionLevel(short definitionLevel)
				{
					return this.fullDefinitionLevelsMap[(int)definitionLevel];
				}

				// Token: 0x06010D77 RID: 68983 RVA: 0x0039FF86 File Offset: 0x0039E186
				public ParquetRecordReader.IElementInfo AncestorAtRepetitionLevel(short repetitionLevel)
				{
					return this.fullRepetitionLevelsMap[(int)repetitionLevel];
				}

				// Token: 0x06010D78 RID: 68984 RVA: 0x0039FF90 File Offset: 0x0039E190
				private void Link(ParquetRecordReader.SchemaInfo.ElementInfo other)
				{
					if (this.unionFindRank > other.unionFindRank)
					{
						other.unionFindParent = this;
						return;
					}
					if (this.unionFindRank < other.unionFindRank)
					{
						this.unionFindParent = other;
						return;
					}
					other.unionFindParent = this;
					this.unionFindRank++;
				}

				// Token: 0x0400650D RID: 25869
				private readonly string name;

				// Token: 0x0400650E RID: 25870
				private readonly SchemaElement element;

				// Token: 0x0400650F RID: 25871
				private readonly ParquetRecordReader.SchemaInfo.ElementInfo parent;

				// Token: 0x04006510 RID: 25872
				private readonly ParquetRecordReader.IElementInfo[] ancestors;

				// Token: 0x04006511 RID: 25873
				private readonly ParquetRecordReader.IElementInfo[] fullDefinitionLevelsMap;

				// Token: 0x04006512 RID: 25874
				private readonly ParquetRecordReader.IElementInfo[] fullRepetitionLevelsMap;

				// Token: 0x04006513 RID: 25875
				private bool lcaVisited;

				// Token: 0x04006514 RID: 25876
				private ParquetRecordReader.SchemaInfo.ElementInfo lcaAncestor;

				// Token: 0x04006515 RID: 25877
				private ParquetRecordReader.SchemaInfo.ElementInfo unionFindParent;

				// Token: 0x04006516 RID: 25878
				private int unionFindRank;
			}

			// Token: 0x02001F51 RID: 8017
			private class UnorderedPairComparer<T> : IEqualityComparer<Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<T, T>>
			{
				// Token: 0x06010D79 RID: 68985 RVA: 0x0039FFDE File Offset: 0x0039E1DE
				public UnorderedPairComparer(IEqualityComparer<T> equalityComparer = null)
				{
					this.equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
				}

				// Token: 0x06010D7A RID: 68986 RVA: 0x0039FFF8 File Offset: 0x0039E1F8
				public bool Equals(Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<T, T> x, Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<T, T> y)
				{
					return (this.equalityComparer.Equals(x.Item1, y.Item1) && this.equalityComparer.Equals(x.Item2, y.Item2)) || (this.equalityComparer.Equals(x.Item1, y.Item2) && this.equalityComparer.Equals(x.Item2, y.Item1));
				}

				// Token: 0x06010D7B RID: 68987 RVA: 0x003A0073 File Offset: 0x0039E273
				public int GetHashCode(Microsoft.Mashup.Engine1.Library.Parquet.Schema.ValueTuple<T, T> obj)
				{
					return this.equalityComparer.GetHashCode(obj.Item1) ^ this.equalityComparer.GetHashCode(obj.Item2);
				}

				// Token: 0x04006517 RID: 25879
				private readonly IEqualityComparer<T> equalityComparer;
			}
		}
	}
}
