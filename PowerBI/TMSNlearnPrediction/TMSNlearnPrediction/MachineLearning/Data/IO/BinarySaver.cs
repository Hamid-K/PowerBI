using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x02000321 RID: 801
	public sealed class BinarySaver : IDataSaver
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x00063700 File Offset: 0x00061900
		public BinarySaver(BinarySaver.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("BinarySaver");
			Contracts.CheckUserArg(this._host, SubComponentExtensions.IsGood(args.compression), "compression", "Must have value");
			Contracts.CheckUserArg(this._host, args.maxRowsPerBlock == null || args.maxRowsPerBlock > 0, "maxRowsPerBlock", "max rows per block must be positive");
			Contracts.CheckUserArg(this._host, args.maxBytesPerBlock == null || args.maxBytesPerBlock > 0L, "maxBytesPerBlock", "max bytes per block must be positive");
			Contracts.CheckUserArg(this._host, args.maxRowsPerBlock != null || args.maxBytesPerBlock != null, "maxBytesPerBlock", "at least one of max rows or bytes per block must have a defined value");
			this._memPool = new MemoryStreamPool();
			this._factory = new CodecFactory(this._host, this._memPool);
			this._compression = ComponentCatalog.CreateInstance<Compression, SignatureCompression>(args.compression);
			this._maxRowsPerBlock = args.maxRowsPerBlock;
			this._maxBytesPerBlock = args.maxBytesPerBlock;
			this._deterministicBlockOrder = args.deterministicBlockOrder;
			this._silent = args.silent;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00063864 File Offset: 0x00061A64
		private void CompressionWorker(BlockingCollection<BinarySaver.Block> toCompress, BlockingCollection<BinarySaver.Block> toWrite, int columns, OrderedWaiter waiter, ExceptionMarshaller exMarshaller)
		{
			try
			{
				foreach (BinarySaver.Block block in toCompress.GetConsumingEnumerable(exMarshaller.Token))
				{
					MemoryStream memoryStream = this._memPool.Get();
					int num;
					using (Stream stream = this._compression.Open(memoryStream))
					{
						MemoryStream blockData = block.BlockData;
						num = (int)blockData.Length;
						ArraySegment<byte> arraySegment;
						Utils.TryGetBuffer(blockData, ref arraySegment);
						stream.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
						this._memPool.Return(ref blockData);
					}
					if (this._deterministicBlockOrder)
					{
						waiter.Wait((long)columns * block.BlockIndex + (long)block.ColumnIndex, exMarshaller.Token);
					}
					toWrite.Add(new BinarySaver.Block(memoryStream, block.ColumnIndex, block.BlockIndex, num), exMarshaller.Token);
					if (this._deterministicBlockOrder)
					{
						waiter.Increment();
					}
				}
			}
			catch (Exception ex)
			{
				exMarshaller.Set("compressing", ex);
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000639D0 File Offset: 0x00061BD0
		private long WriteMetadata(BinaryWriter writer, ISchema schema, int col, IChannel ch)
		{
			int num = 0;
			BinarySaver.WriteMetadataCoreDelegate writeMetadataCoreDelegate = new BinarySaver.WriteMetadataCoreDelegate(this.WriteMetadataCore<int>);
			MethodInfo genericMethodDefinition = writeMetadataCoreDelegate.GetMethodInfo().GetGenericMethodDefinition();
			object[] array = new object[6];
			array[0] = writer.BaseStream;
			array[1] = schema;
			array[2] = col;
			object[] array2 = array;
			List<long> list = new List<long>();
			list.Add(writer.BaseStream.Position);
			List<Tuple<string, IValueCodec, CompressionKind>> list2 = new List<Tuple<string, IValueCodec, CompressionKind>>();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (KeyValuePair<string, ColumnType> keyValuePair in schema.GetMetadataTypes(col))
			{
				Contracts.Check(this._host, !string.IsNullOrEmpty(keyValuePair.Key), "Metadata with null or empty kind detected, disallowed");
				Contracts.Check(this._host, keyValuePair.Value != null, "Metadata with null type detected, disallowed");
				if (!hashSet.Add(keyValuePair.Key))
				{
					throw Contracts.Except(this._host, "Metadata with duplicate kind '{0}' encountered, disallowed", new object[]
					{
						keyValuePair.Key,
						schema.GetColumnName(col)
					});
				}
				array2[3] = keyValuePair.Key;
				array2[4] = keyValuePair.Value;
				IValueCodec valueCodec = (IValueCodec)genericMethodDefinition.MakeGenericMethod(new Type[] { keyValuePair.Value.RawType }).Invoke(this, array2);
				if (valueCodec == null)
				{
					ch.Warning("Could not get codec for type {0}, dropping column '{1}' index {2} metadata kind '{3}'", new object[]
					{
						keyValuePair.Value,
						schema.GetColumnName(col),
						col,
						keyValuePair.Key
					});
				}
				else
				{
					list.Add(writer.BaseStream.Position);
					Contracts.CheckIO(this._host, list[list.Count - 1] > list[list.Count - 2], "Bad offsets detected during write");
					list2.Add(Tuple.Create<string, IValueCodec, CompressionKind>(keyValuePair.Key, valueCodec, (CompressionKind)array2[5]));
					num++;
				}
			}
			if (list2.Count == 0)
			{
				Contracts.CheckIO(this._host, writer.BaseStream.Position == list[0], "unexpected offset after no writing of metadata");
				return 0L;
			}
			long num2 = list[list2.Count];
			Utils.WriteLEB128Int(writer, (ulong)((long)list2.Count));
			num2 += (long)Utils.LEB128IntLength((ulong)((long)list2.Count));
			for (int i = 0; i < list2.Count; i++)
			{
				writer.Write(list2[i].Item1);
				int byteCount = Encoding.UTF8.GetByteCount(list2[i].Item1);
				num2 += (long)(Utils.LEB128IntLength((ulong)((long)byteCount)) + byteCount);
				Contracts.CheckIO(this._host, writer.BaseStream.Position == num2, "unexpected offsets after metadata table of contents kind");
				num2 += (long)this._factory.WriteCodec(writer.BaseStream, list2[i].Item2);
				Contracts.CheckIO(this._host, writer.BaseStream.Position == num2, "unexpected offsets after metadata table of contents type description");
				writer.Write((byte)list2[i].Item3);
				num2 += 1L;
				writer.Write(list[i]);
				num2 += 8L;
				long num3 = list[i + 1] - list[i];
				Utils.WriteLEB128Int(writer, (ulong)num3);
				num2 += (long)Utils.LEB128IntLength((ulong)num3);
				Contracts.CheckIO(this._host, writer.BaseStream.Position == num2, "unexpected offsets after metadata table of contents location");
			}
			return list[list2.Count];
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00063DAC File Offset: 0x00061FAC
		private IValueCodec WriteMetadataCore<T>(Stream stream, ISchema schema, int col, string kind, ColumnType type, out CompressionKind compressionKind)
		{
			IValueCodec valueCodec;
			if (!this._factory.TryGetCodec(type, out valueCodec))
			{
				compressionKind = CompressionKind.None;
				return null;
			}
			IValueCodec<T> valueCodec2 = (IValueCodec<T>)valueCodec;
			T t = default(T);
			schema.GetMetadata<T>(kind, col, ref t);
			MemoryStream memoryStream = this._memPool.Get();
			using (IValueWriter<T> valueWriter = valueCodec2.OpenWriter(memoryStream))
			{
				valueWriter.Write(ref t);
				valueWriter.Commit();
			}
			MemoryStream memoryStream2 = this._memPool.Get();
			ArraySegment<byte> arraySegment;
			Utils.TryGetBuffer(memoryStream, ref arraySegment);
			using (Stream stream2 = this._compression.Open(memoryStream2))
			{
				stream2.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			if (memoryStream.Length <= memoryStream2.Length)
			{
				compressionKind = CompressionKind.None;
			}
			else
			{
				compressionKind = this._compression.Kind;
				Utils.TryGetBuffer(memoryStream2, ref arraySegment);
			}
			stream.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			this._memPool.Return(ref memoryStream);
			this._memPool.Return(ref memoryStream2);
			return valueCodec2;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00063EEC File Offset: 0x000620EC
		private unsafe void WriteWorker(Stream stream, BlockingCollection<BinarySaver.Block> toWrite, BinarySaver.ColumnCodec[] activeColumns, ISchema sourceSchema, int rowsPerBlock, IChannelProvider cp, ExceptionMarshaller exMarshaller)
		{
			try
			{
				using (IChannel channel = cp.Start("Write"))
				{
					List<BlockLookup>[] array = new List<BlockLookup>[activeColumns.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new List<BlockLookup>();
					}
					int[] array2 = new int[activeColumns.Length];
					Contracts.CheckIO(channel, stream.Position == 0L);
					stream.Write(new byte[256], 0, 256);
					Contracts.CheckIO(channel, stream.Position == 256L);
					long num = stream.Position;
					BlockLookup blockLookup = default(BlockLookup);
					foreach (BinarySaver.Block block in toWrite.GetConsumingEnumerable(exMarshaller.Token))
					{
						Contracts.CheckIO(channel, stream.Position == num);
						MemoryStream blockData = block.BlockData;
						ArraySegment<byte> arraySegment;
						Utils.TryGetBuffer(blockData, ref arraySegment);
						stream.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
						BlockLookup blockLookup2 = new BlockLookup(num, (int)blockData.Length, block.UncompressedLength);
						num += blockData.Length;
						this._memPool.Return(ref blockData);
						Contracts.CheckIO(channel, stream.Position == num);
						int num2 = (int)block.BlockIndex;
						List<BlockLookup> list = array[block.ColumnIndex];
						if ((long)list.Count == block.BlockIndex)
						{
							list.Add(blockLookup2);
						}
						else if ((long)list.Count < block.BlockIndex)
						{
							int num3 = (int)block.BlockIndex - list.Count;
							for (int j = 0; j < num3; j++)
							{
								list.Add(blockLookup);
							}
							array2[block.ColumnIndex] += num3;
							list.Add(blockLookup2);
						}
						else
						{
							array2[block.ColumnIndex]--;
							list[num2] = blockLookup2;
						}
					}
					long[] array3 = new long[array.Length];
					using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true))
					{
						for (int k = 0; k < array.Length; k++)
						{
							array3[k] = stream.Position;
							foreach (BlockLookup blockLookup3 in array[k])
							{
								binaryWriter.Write(blockLookup3.BlockOffset);
								binaryWriter.Write(blockLookup3.BlockLength);
								binaryWriter.Write(blockLookup3.DecompressedBlockLength);
							}
							Contracts.CheckIO(channel, stream.Position == array3[k] + (long)(16 * array[k].Count), "unexpected offsets after block lookup table write");
						}
						long[] array4 = new long[activeColumns.Length];
						for (int l = 0; l < activeColumns.Length; l++)
						{
							array4[l] = this.WriteMetadata(binaryWriter, sourceSchema, activeColumns[l].SourceIndex, channel);
						}
						long position = stream.Position;
						int num4 = 0;
						num = stream.Position;
						foreach (BinarySaver.ColumnCodec columnCodec in activeColumns)
						{
							string columnName = sourceSchema.GetColumnName(columnCodec.SourceIndex);
							binaryWriter.Write(columnName);
							int byteCount = Encoding.UTF8.GetByteCount(columnName);
							num += (long)(Utils.LEB128IntLength((ulong)byteCount) + byteCount);
							Contracts.CheckIO(channel, stream.Position == num, "unexpected offsets after table of contents name");
							num += (long)this._factory.WriteCodec(stream, columnCodec.Codec);
							Contracts.CheckIO(channel, stream.Position == num, "unexpected offsets after table of contents type description");
							binaryWriter.Write((byte)this._compression.Kind);
							num += 1L;
							Utils.WriteLEB128Int(binaryWriter, (ulong)((long)rowsPerBlock));
							num += (long)Utils.LEB128IntLength((ulong)rowsPerBlock);
							binaryWriter.Write(array3[num4]);
							num += 8L;
							binaryWriter.Write(array4[num4]);
							num += 8L;
							Contracts.CheckIO(channel, stream.Position == num, "unexpected offsets after table of contents");
							num4++;
						}
						long position2 = stream.Position;
						binaryWriter.Write(4849615937778106880UL);
						Header header = new Header
						{
							Signature = 18672198525668675UL,
							Version = 281479271743493UL,
							CompatibleVersion = 281479271743493UL,
							TableOfContentsOffset = position,
							TailOffset = position2,
							RowCount = this._rowCount,
							ColumnCount = activeColumns.Length
						};
						byte[] array5 = new byte[256];
						Marshal.Copy(new IntPtr((void*)(&header)), array5, 0, Marshal.SizeOf(typeof(Header)));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(array5);
					}
					channel.Done();
				}
			}
			catch (Exception ex)
			{
				exMarshaller.Set("writing", ex);
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0006448C File Offset: 0x0006268C
		private void FetchWorker(BlockingCollection<BinarySaver.Block> toCompress, IDataView data, BinarySaver.ColumnCodec[] activeColumns, int rowsPerBlock, Stopwatch sw, IChannel ch, ExceptionMarshaller exMarshaller)
		{
			try
			{
				HashSet<int> hashSet = new HashSet<int>(activeColumns.Select((BinarySaver.ColumnCodec col) => col.SourceIndex));
				long num = 0L;
				int num2 = rowsPerBlock;
				using (IRowCursor rowCursor = data.GetRowCursor(new Func<int, bool>(hashSet.Contains), null))
				{
					BinarySaver.WritePipe[] array = new BinarySaver.WritePipe[activeColumns.Length];
					for (int i = 0; i < activeColumns.Length; i++)
					{
						array[i] = BinarySaver.WritePipe.Create(this, rowCursor, activeColumns[i]);
					}
					for (int j = 0; j < array.Length; j++)
					{
						array[j].BeginBlock();
					}
					int num3 = 0;
					while (rowCursor.MoveNext())
					{
						for (int k = 0; k < array.Length; k++)
						{
							array[k].FetchAndWrite();
						}
						if (--num2 == 0)
						{
							for (int l = 0; l < array.Length; l++)
							{
								toCompress.Add(new BinarySaver.Block(array[l].EndBlock(), l, num, 0), exMarshaller.Token);
								array[l].BeginBlock();
							}
							num2 = rowsPerBlock;
							num += 1L;
						}
						if (!this._silent && ++num3 == 100000)
						{
							ch.Info("{0} rows in process", new object[] { (num + 1L) * (long)rowsPerBlock - (long)num2 });
							num3 = 0;
						}
					}
					if (num2 < rowsPerBlock)
					{
						for (int m = 0; m < array.Length; m++)
						{
							toCompress.Add(new BinarySaver.Block(array[m].EndBlock(), m, num, 0), exMarshaller.Token);
						}
					}
				}
				this._rowCount = (num + 1L) * (long)rowsPerBlock - (long)num2;
				toCompress.CompleteAdding();
			}
			catch (Exception ex)
			{
				exMarshaller.Set("cursoring", ex);
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00064690 File Offset: 0x00062890
		public bool IsColumnSavable(ColumnType type)
		{
			IValueCodec valueCodec;
			return this._factory.TryGetCodec(type, out valueCodec);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000647B4 File Offset: 0x000629B4
		public void SaveData(Stream stream, IDataView data, params int[] colIndices)
		{
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			Contracts.CheckValue<IDataView>(this._host, data, "data");
			Contracts.Check(this._host, stream.CanWrite, "cannot save to non-writable stream");
			Contracts.Check(this._host, stream.CanSeek, "cannot save to non-seekable stream");
			Contracts.Check(this._host, stream.Position == 0L, "stream must be positioned at head of stream");
			using (IChannel ch = this._host.Start("Saving"))
			{
				using (ExceptionMarshaller exMarshaller = new ExceptionMarshaller())
				{
					BlockingCollection<BinarySaver.Block> toWrite = new BlockingCollection<BinarySaver.Block>(16);
					BlockingCollection<BinarySaver.Block> toCompress = new BlockingCollection<BinarySaver.Block>(16);
					BinarySaver.ColumnCodec[] activeColumns = this.GetActiveColumns(data.Schema, colIndices);
					int rowsPerBlock = this.RowsPerBlockHeuristic(data, activeColumns);
					Stopwatch stopwatch = new Stopwatch();
					Task task = null;
					if (activeColumns.Length > 0)
					{
						OrderedWaiter waiter = (this._deterministicBlockOrder ? new OrderedWaiter(true) : null);
						Thread[] compressionThreads = new Thread[Environment.ProcessorCount];
						for (int i = 0; i < compressionThreads.Length; i++)
						{
							compressionThreads[i] = Utils.CreateBackgroundThread(delegate
							{
								this.CompressionWorker(toCompress, toWrite, activeColumns.Length, waiter, exMarshaller);
							});
							compressionThreads[i].Start();
						}
						task = new Task(delegate
						{
							foreach (Thread thread2 in compressionThreads)
							{
								thread2.Join();
							}
						});
						task.Start();
					}
					Thread thread = Utils.CreateBackgroundThread(delegate
					{
						this.WriteWorker(stream, toWrite, activeColumns, data.Schema, rowsPerBlock, ch, exMarshaller);
					});
					thread.Start();
					stopwatch.Start();
					this.FetchWorker(toCompress, data, activeColumns, rowsPerBlock, stopwatch, ch, exMarshaller);
					if (task != null)
					{
						task.Wait();
					}
					toWrite.CompleteAdding();
					thread.Join();
					exMarshaller.ThrowIfSet(ch);
					if (!this._silent)
					{
						ch.Info("Wrote {0} rows across {1} columns in {2}", new object[] { this._rowCount, activeColumns.Length, stopwatch.Elapsed });
					}
					ch.Done();
				}
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00064B18 File Offset: 0x00062D18
		private BinarySaver.ColumnCodec[] GetActiveColumns(ISchema schema, int[] colIndices)
		{
			BinarySaver.ColumnCodec[] array = new BinarySaver.ColumnCodec[Utils.Size<int>(colIndices)];
			if (Utils.Size<int>(colIndices) == 0)
			{
				return array;
			}
			for (int i = 0; i < colIndices.Length; i++)
			{
				ColumnType columnType = schema.GetColumnType(colIndices[i]);
				IValueCodec valueCodec;
				if (!this._factory.TryGetCodec(columnType, out valueCodec))
				{
					throw Contracts.Except(this._host, "Could not get codec for requested column {0} of type {1}", new object[]
					{
						schema.GetColumnName(i),
						columnType
					});
				}
				array[i] = new BinarySaver.ColumnCodec(colIndices[i], valueCodec);
			}
			return array;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00064BB8 File Offset: 0x00062DB8
		private int RowsPerBlockHeuristic(IDataView data, BinarySaver.ColumnCodec[] actives)
		{
			if (this._maxBytesPerBlock == null)
			{
				return this._maxRowsPerBlock.Value;
			}
			long value = this._maxBytesPerBlock.Value;
			HashSet<int> hashSet = new HashSet<int>(actives.Select((BinarySaver.ColumnCodec cc) => cc.SourceIndex));
			IRandom random = (data.CanShuffle ? new TauswortheHybrid(this._host.Rand) : null);
			BinarySaver.EstimatorDelegate estimatorDelegate = new BinarySaver.EstimatorDelegate(this.EstimatorCore<int>);
			MethodInfo genericMethodDefinition = estimatorDelegate.GetMethodInfo().GetGenericMethodDefinition();
			int num3;
			using (IRowCursor rowCursor = data.GetRowCursor(new Func<int, bool>(hashSet.Contains), random))
			{
				object[] array = new object[4];
				array[0] = rowCursor;
				object[] array2 = array;
				IValueWriter[] array3 = new IValueWriter[actives.Length];
				Func<long>[] array4 = new Func<long>[actives.Length];
				for (int i = 0; i < actives.Length; i++)
				{
					BinarySaver.ColumnCodec columnCodec = actives[i];
					array2[1] = columnCodec;
					genericMethodDefinition.MakeGenericMethod(new Type[] { columnCodec.Codec.Type.RawType }).Invoke(this, array2);
					array4[i] = (Func<long>)array2[2];
					array3[i] = (IValueWriter)array2[3];
				}
				int num = 0;
				int valueOrDefault = this._maxRowsPerBlock.GetValueOrDefault(int.MaxValue);
				while (num < valueOrDefault && rowCursor.MoveNext())
				{
					long num2 = array4.Sum((Func<long> c) => c());
					if (num2 > value)
					{
						break;
					}
					num++;
				}
				num3 = Math.Max(1, num);
			}
			return num3;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00064DD0 File Offset: 0x00062FD0
		private void EstimatorCore<T>(IRowCursor cursor, BinarySaver.ColumnCodec col, out Func<long> fetchWriteEstimator, out IValueWriter writer)
		{
			ValueGetter<T> getter = cursor.GetGetter<T>(col.SourceIndex);
			IValueCodec<T> valueCodec = col.Codec as IValueCodec<T>;
			IValueWriter<T> specificWriter = valueCodec.OpenWriter(Stream.Null);
			writer = specificWriter;
			T val = default(T);
			fetchWriteEstimator = delegate
			{
				getter.Invoke(ref val);
				specificWriter.Write(ref val);
				return specificWriter.GetCommitLengthEstimate();
			};
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00064E38 File Offset: 0x00063038
		public bool TryWriteTypeDescription(Stream stream, ColumnType type, out int bytesWritten)
		{
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			Contracts.CheckValue<ColumnType>(this._host, type, "type");
			IValueCodec valueCodec;
			if (!this._factory.TryGetCodec(type, out valueCodec))
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = this._factory.WriteCodec(stream, valueCodec);
			return true;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00064E8C File Offset: 0x0006308C
		public ColumnType LoadTypeDescriptionOrNull(Stream stream)
		{
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			IValueCodec valueCodec;
			if (!this._factory.TryReadCodec(stream, out valueCodec))
			{
				return null;
			}
			return valueCodec.Type;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00064EC4 File Offset: 0x000630C4
		public bool TryWriteTypeAndValue<T>(Stream stream, ColumnType type, ref T value, out int bytesWritten)
		{
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			Contracts.CheckValue<ColumnType>(this._host, type, "type");
			Contracts.CheckParam(this._host, value.GetType() == type.RawType, "value", "Value doesn't match type");
			IValueCodec valueCodec;
			if (!this._factory.TryGetCodec(type, out valueCodec))
			{
				bytesWritten = 0;
				return false;
			}
			IValueCodec<T> valueCodec2 = (IValueCodec<T>)valueCodec;
			bytesWritten = this._factory.WriteCodec(stream, valueCodec);
			using (IValueWriter<T> valueWriter = valueCodec2.OpenWriter(stream))
			{
				valueWriter.Write(ref value);
				bytesWritten += (int)valueWriter.GetCommitLengthEstimate();
				valueWriter.Commit();
			}
			return true;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00064F8C File Offset: 0x0006318C
		public bool TryLoadTypeAndValue(Stream stream, out ColumnType type, out object value)
		{
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			IValueCodec valueCodec;
			if (!this._factory.TryReadCodec(stream, out valueCodec))
			{
				type = null;
				value = null;
				return false;
			}
			type = valueCodec.Type;
			Func<Stream, IValueCodec<int>, object> func = new Func<Stream, IValueCodec<int>, object>(this.LoadValue<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { valueCodec.Type.RawType });
			value = methodInfo.Invoke(this, new object[] { stream, valueCodec });
			return true;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0006501C File Offset: 0x0006321C
		private object LoadValue<T>(Stream stream, IValueCodec<T> codec)
		{
			T t = default(T);
			using (IValueReader<T> valueReader = codec.OpenReader(stream, 1))
			{
				valueReader.MoveNext();
				valueReader.Get(ref t);
			}
			return t;
		}

		// Token: 0x04000A63 RID: 2659
		internal const string Summary = "Writes data into a native binary IDV file.";

		// Token: 0x04000A64 RID: 2660
		private readonly IHost _host;

		// Token: 0x04000A65 RID: 2661
		private readonly CodecFactory _factory;

		// Token: 0x04000A66 RID: 2662
		private readonly MemoryStreamPool _memPool;

		// Token: 0x04000A67 RID: 2663
		private readonly Compression _compression;

		// Token: 0x04000A68 RID: 2664
		private readonly int? _maxRowsPerBlock;

		// Token: 0x04000A69 RID: 2665
		private readonly long? _maxBytesPerBlock;

		// Token: 0x04000A6A RID: 2666
		private readonly bool _deterministicBlockOrder;

		// Token: 0x04000A6B RID: 2667
		private readonly bool _silent;

		// Token: 0x04000A6C RID: 2668
		private long _rowCount;

		// Token: 0x02000322 RID: 802
		public sealed class Arguments
		{
			// Token: 0x04000A70 RID: 2672
			[Argument(4, HelpText = "The compression scheme to use for the blocks", ShortName = "comp")]
			public SubComponent<Compression, SignatureCompression> compression = new SubComponent<Compression, SignatureCompression>("deflate");

			// Token: 0x04000A71 RID: 2673
			[Argument(4, HelpText = "The block-size heuristic will choose no more than this many rows to have per block, can be set to null to indicate that there is no inherent limit", ShortName = "rpb")]
			public int? maxRowsPerBlock = new int?(8192);

			// Token: 0x04000A72 RID: 2674
			[Argument(4, HelpText = "The block-size heuristic will attempt to have about this many bytes across all columns per block, can be set to null to accept the inidcated max-rows-per-block as the number of rows per block", ShortName = "bpb")]
			public long? maxBytesPerBlock = new long?(83886080L);

			// Token: 0x04000A73 RID: 2675
			[Argument(4, HelpText = "If true, this forces a deterministic block order during writing", ShortName = "det")]
			public bool deterministicBlockOrder;

			// Token: 0x04000A74 RID: 2676
			[Argument(4, HelpText = "Suppress any info output (not warnings or errors)", Hide = true)]
			public bool silent;
		}

		// Token: 0x02000323 RID: 803
		private struct ColumnCodec
		{
			// Token: 0x060011FD RID: 4605 RVA: 0x000650A5 File Offset: 0x000632A5
			public ColumnCodec(int sourceIndex, IValueCodec codec)
			{
				this.SourceIndex = sourceIndex;
				this.Codec = codec;
			}

			// Token: 0x04000A75 RID: 2677
			public readonly int SourceIndex;

			// Token: 0x04000A76 RID: 2678
			public readonly IValueCodec Codec;
		}

		// Token: 0x02000324 RID: 804
		private abstract class WritePipe
		{
			// Token: 0x060011FE RID: 4606 RVA: 0x000650B5 File Offset: 0x000632B5
			protected WritePipe(BinarySaver parent)
			{
				this._parent = parent;
			}

			// Token: 0x060011FF RID: 4607 RVA: 0x000650C4 File Offset: 0x000632C4
			public static BinarySaver.WritePipe Create(BinarySaver parent, IRowCursor cursor, BinarySaver.ColumnCodec col)
			{
				Type type = typeof(BinarySaver.WritePipe<>).MakeGenericType(new Type[] { col.Codec.Type.RawType });
				return (BinarySaver.WritePipe)Activator.CreateInstance(type, new object[] { parent, cursor, col });
			}

			// Token: 0x06001200 RID: 4608
			public abstract void BeginBlock();

			// Token: 0x06001201 RID: 4609
			public abstract void FetchAndWrite();

			// Token: 0x06001202 RID: 4610
			public abstract MemoryStream EndBlock();

			// Token: 0x04000A77 RID: 2679
			protected readonly BinarySaver _parent;
		}

		// Token: 0x02000325 RID: 805
		private sealed class WritePipe<T> : BinarySaver.WritePipe
		{
			// Token: 0x06001203 RID: 4611 RVA: 0x00065124 File Offset: 0x00063324
			public WritePipe(BinarySaver parent, IRowCursor cursor, BinarySaver.ColumnCodec col)
				: base(parent)
			{
				IValueCodec<T> valueCodec = col.Codec as IValueCodec<T>;
				this._codec = valueCodec;
				this._getter = cursor.GetGetter<T>(col.SourceIndex);
			}

			// Token: 0x06001204 RID: 4612 RVA: 0x0006515F File Offset: 0x0006335F
			public override void BeginBlock()
			{
				this._currentStream = this._parent._memPool.Get();
				this._writer = this._codec.OpenWriter(this._currentStream);
			}

			// Token: 0x06001205 RID: 4613 RVA: 0x0006518E File Offset: 0x0006338E
			public override void FetchAndWrite()
			{
				this._getter.Invoke(ref this._value);
				this._writer.Write(ref this._value);
			}

			// Token: 0x06001206 RID: 4614 RVA: 0x000651B4 File Offset: 0x000633B4
			public override MemoryStream EndBlock()
			{
				this._writer.Commit();
				this._writer = null;
				MemoryStream currentStream = this._currentStream;
				this._currentStream = null;
				return currentStream;
			}

			// Token: 0x04000A78 RID: 2680
			private ValueGetter<T> _getter;

			// Token: 0x04000A79 RID: 2681
			private IValueCodec<T> _codec;

			// Token: 0x04000A7A RID: 2682
			private IValueWriter<T> _writer;

			// Token: 0x04000A7B RID: 2683
			private MemoryStream _currentStream;

			// Token: 0x04000A7C RID: 2684
			private T _value;
		}

		// Token: 0x02000326 RID: 806
		private struct Block
		{
			// Token: 0x06001207 RID: 4615 RVA: 0x000651E2 File Offset: 0x000633E2
			public Block(MemoryStream data, int colindex, long blockIndex, int uncompLength = 0)
			{
				this.BlockData = data;
				this.ColumnIndex = colindex;
				this.BlockIndex = blockIndex;
				this.UncompressedLength = uncompLength;
			}

			// Token: 0x04000A7D RID: 2685
			public readonly MemoryStream BlockData;

			// Token: 0x04000A7E RID: 2686
			public readonly int UncompressedLength;

			// Token: 0x04000A7F RID: 2687
			public readonly int ColumnIndex;

			// Token: 0x04000A80 RID: 2688
			public readonly long BlockIndex;
		}

		// Token: 0x02000327 RID: 807
		// (Invoke) Token: 0x06001209 RID: 4617
		private delegate IValueCodec WriteMetadataCoreDelegate(Stream stream, ISchema schema, int col, string kind, ColumnType type, out CompressionKind compression);

		// Token: 0x02000328 RID: 808
		// (Invoke) Token: 0x0600120D RID: 4621
		private delegate void EstimatorDelegate(IRowCursor cursor, BinarySaver.ColumnCodec col, out Func<long> fetchWriteEstimator, out IValueWriter writer);
	}
}
