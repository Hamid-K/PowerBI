using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x0200030F RID: 783
	public sealed class BinaryLoader : IDataLoader, IDataView, ISchematized, ICanSaveModel, IDisposable
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00060093 File Offset: 0x0005E293
		public ISchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0006009B File Offset: 0x0005E29B
		private long RowCount
		{
			get
			{
				return this._header.RowCount;
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000600A8 File Offset: 0x0005E2A8
		public long? GetRowCount(bool lazy = true)
		{
			return new long?(this.RowCount);
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x000600B5 File Offset: 0x0005E2B5
		public bool CanShuffle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000600B8 File Offset: 0x0005E2B8
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BINLOADR", 65539U, 65539U, 65537U, "BinaryLoader", null);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000600DC File Offset: 0x0005E2DC
		private BinaryLoader(BinaryLoader.Arguments args, IHost host, Stream stream, bool leaveOpen)
		{
			this._host = host;
			Contracts.CheckValue<BinaryLoader.Arguments>(this._host, args, "args");
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			Contracts.CheckParam(this._host, stream.CanRead, "stream", "input stream must be readable");
			Contracts.CheckParam(this._host, stream.CanSeek, "stream", "input stream must be seekable");
			Contracts.CheckParam(this._host, stream.Position == 0L, "stream", "input stream must be at head");
			Contracts.CheckUserArg(this._host, 0.0 <= args.poolBlocks, "shuffleBlocks", "must be non-negative");
			using (IChannel channel = this._host.Start("Initializing"))
			{
				this._stream = stream;
				this._reader = new BinaryReader(this._stream, Encoding.UTF8, leaveOpen);
				this._factory = new CodecFactory(this._host, null);
				this._header = this.InitHeader();
				this._autodeterminedThreads = args.threads == null;
				this._threads = Math.Max(1, args.threads ?? (Environment.ProcessorCount / 2));
				this._generatedRowIndexName = (string.IsNullOrWhiteSpace(args.rowIndexName) ? null : args.rowIndexName);
				this.InitTOC(channel, out this._aliveColumns, out this._deadColumns, out this._rowsPerBlock, out this._tocEndLim);
				this._schema = new BinaryLoader.SchemaImpl(this);
				this._bufferCollection = new MemoryStreamCollection();
				if (Utils.Size<BinaryLoader.TableOfContentsEntry>(this._deadColumns) > 0)
				{
					channel.Warning("BinaryLoader does not know how to interpret {0} columns", new object[] { Utils.Size<BinaryLoader.TableOfContentsEntry>(this._deadColumns) });
				}
				this._shuffleBlocks = args.poolBlocks;
				this.CalculateShufflePoolRows(channel, out this._randomShufflePoolRows);
				channel.Done();
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000602F4 File Offset: 0x0005E4F4
		public BinaryLoader(BinaryLoader.Arguments args, IHostEnvironment env, Stream stream, bool leaveOpen = true)
			: this(args, env.Register("BinaryLoader"), stream, leaveOpen)
		{
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0006030B File Offset: 0x0005E50B
		public BinaryLoader(BinaryLoader.Arguments args, IHostEnvironment env, string filename)
			: this(args, env, BinaryLoader.OpenStream(filename), false)
		{
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0006031C File Offset: 0x0005E51C
		public BinaryLoader(BinaryLoader.Arguments args, IHostEnvironment env, IMultiStreamSource file)
			: this(args, env, BinaryLoader.OpenStream(file), false)
		{
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00060330 File Offset: 0x0005E530
		private BinaryLoader(ModelLoadContext ctx, IHost host, Stream stream)
		{
			this._host = host;
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			Contracts.CheckParam(this._host, stream.CanRead, "stream", "input stream must be readable");
			Contracts.CheckParam(this._host, stream.CanSeek, "stream", "input stream must be seekable");
			Contracts.CheckParam(this._host, stream.Position == 0L, "stream", "input stream must be at head");
			using (IChannel channel = this._host.Start("Initializing"))
			{
				this._stream = stream;
				if (ctx.Header.ModelVerWritten >= 65538U)
				{
					this._threads = ctx.Reader.ReadInt32();
					Contracts.CheckDecode(channel, this._threads >= 0);
					if (this._threads == 0)
					{
						this._autodeterminedThreads = true;
						this._threads = Math.Max(1, Environment.ProcessorCount / 2);
					}
					this._generatedRowIndexName = ctx.LoadStringOrNull();
					Contracts.CheckDecode(channel, this._generatedRowIndexName == null || !string.IsNullOrWhiteSpace(this._generatedRowIndexName));
				}
				else
				{
					this._threads = Math.Max(1, Environment.ProcessorCount / 2);
					this._generatedRowIndexName = null;
				}
				if (ctx.Header.ModelVerWritten >= 65539U)
				{
					this._shuffleBlocks = ctx.Reader.ReadDouble();
					Contracts.CheckDecode(channel, 0.0 <= this._shuffleBlocks);
				}
				else
				{
					this._shuffleBlocks = 4.0;
				}
				this._reader = new BinaryReader(this._stream, Encoding.UTF8, false);
				this._factory = new CodecFactory(this._host, null);
				this._header = this.InitHeader();
				this.InitTOC(channel, out this._aliveColumns, out this._deadColumns, out this._rowsPerBlock, out this._tocEndLim);
				this._schema = new BinaryLoader.SchemaImpl(this);
				this._bufferCollection = new MemoryStreamCollection();
				if (Utils.Size<BinaryLoader.TableOfContentsEntry>(this._deadColumns) > 0)
				{
					channel.Warning("BinaryLoader does not know how to interpret {0} columns", new object[] { Utils.Size<BinaryLoader.TableOfContentsEntry>(this._deadColumns) });
				}
				this.CalculateShufflePoolRows(channel, out this._randomShufflePoolRows);
				channel.Done();
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00060664 File Offset: 0x0005E864
		public static BinaryLoader Create(ModelLoadContext ctx, IHostEnvironment env, IMultiStreamSource files)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("BinaryLoader");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(BinaryLoader.GetVersionInfo());
			Contracts.CheckValue<IMultiStreamSource>(h, files, "files");
			return HostExtensions.Apply<BinaryLoader>(h, "Loading Model", delegate(IChannel ch)
			{
				if (files.Count == 0)
				{
					BinaryLoader retVal = null;
					if (ctx.TryLoadBinaryStream("Schema.idv", delegate(BinaryReader r)
					{
						retVal = new BinaryLoader(ctx, h, HybridMemoryStream.CreateCache(r.BaseStream, 1073741824));
					}))
					{
						Contracts.CheckDecode(h, retVal.RowCount == 0L, "Internal schema IDV expected to have no rows, but some encountered");
						return retVal;
					}
				}
				return new BinaryLoader(ctx, h, BinaryLoader.OpenStream(files));
			});
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000606FC File Offset: 0x0005E8FC
		public static BinaryLoader Create(ModelLoadContext ctx, IHostEnvironment env, Stream stream)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("BinaryLoader");
			return new BinaryLoader(ctx, host, HybridMemoryStream.CreateCache(stream, 1073741824));
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00060732 File Offset: 0x0005E932
		private static Stream OpenStream(IMultiStreamSource files)
		{
			Contracts.CheckValue<IMultiStreamSource>(files, "files");
			Contracts.CheckParam(files.Count == 1, "files", "binary loader must be created with one file");
			return files.Open(0);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00060760 File Offset: 0x0005E960
		private static Stream OpenStream(string filename)
		{
			Contracts.CheckNonEmpty(filename, "filename");
			MultiFileSource multiFileSource = new MultiFileSource(filename);
			return BinaryLoader.OpenStream(multiFileSource);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00060788 File Offset: 0x0005E988
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(BinaryLoader.GetVersionInfo());
			BinaryLoader.SaveParameters(ctx, this._autodeterminedThreads ? 0 : this._threads, this._generatedRowIndexName, this._shuffleBlocks);
			int[] array;
			BinaryLoader.SaveSchema(this._host, ctx, this.Schema, out array);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000607EE File Offset: 0x0005E9EE
		private static void SaveParameters(ModelSaveContext ctx, int threads, string generatedRowIndexName, double shuffleBlocks)
		{
			ctx.Writer.Write(threads);
			ctx.SaveStringOrNull(generatedRowIndexName);
			ctx.Writer.Write(shuffleBlocks);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00060994 File Offset: 0x0005EB94
		private static void SaveSchema(IHostEnvironment env, ModelSaveContext ctx, ISchema schema, out int[] unsavableColIndices)
		{
			IHost host = env.Register("BinaryLoader");
			EmptyDataView noRows = new EmptyDataView(host, schema);
			BinarySaver saver = new BinarySaver(new BinarySaver.Arguments
			{
				silent = true
			}, env);
			var enumerable = from x in Enumerable.Range(0, schema.ColumnCount)
				select new
				{
					col = x,
					isSavable = saver.IsColumnSavable(schema.GetColumnType(x))
				};
			int[] toSave = (from x in enumerable
				where x.isSavable
				select x.col).ToArray<int>();
			unsavableColIndices = (from x in enumerable
				where !x.isSavable
				select x.col).ToArray<int>();
			ctx.SaveBinaryStream("Schema.idv", delegate(BinaryWriter w)
			{
				saver.SaveData(w.BaseStream, noRows, toSave);
			});
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00060ABC File Offset: 0x0005ECBC
		public static void SaveInstance(IHostEnvironment env, ModelSaveContext ctx, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("BinaryLoader");
			Contracts.CheckValue<ModelSaveContext>(host, ctx, "ctx");
			Contracts.CheckValue<ModelSaveContext>(host, ctx, "schema");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(BinaryLoader.GetVersionInfo());
			BinaryLoader.SaveParameters(ctx, 0, null, 4.0);
			int[] array;
			BinaryLoader.SaveSchema(env, ctx, schema, out array);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00060B24 File Offset: 0x0005ED24
		private unsafe Header InitHeader()
		{
			byte[] array = new byte[256];
			int num = this._reader.Read(array, 0, 256);
			if (num != 256)
			{
				throw Contracts.ExceptDecode(this._host, "Read only {0} bytes in file, expected header size of {1}", new object[] { num, 256 });
			}
			Header header;
			Marshal.Copy(array, 0, (IntPtr)((void*)(&header)), 256);
			Contracts.CheckDecode(this._host, header.Signature == 18672198525668675UL, "This does not appear to be a binary dataview file");
			if (header.CompatibleVersion > header.Version)
			{
				throw Contracts.ExceptDecode(this._host, "Compatibility version {0} cannot be greater than file version {1}", new object[]
				{
					Header.VersionToString(header.CompatibleVersion),
					Header.VersionToString(header.Version)
				});
			}
			if (header.Version < 281479271743490UL)
			{
				throw Contracts.ExceptDecode(this._host, "Unexpected version {0} encountered, earliest expected here was {1}", new object[]
				{
					Header.VersionToString(header.Version),
					Header.VersionToString(281479271743490UL)
				});
			}
			if (header.CompatibleVersion < 281479271743492UL)
			{
				throw Contracts.Except(this._host, "Cannot read version {0} data, earliest that can be handled is {1}", new object[]
				{
					Header.VersionToString(header.CompatibleVersion),
					Header.VersionToString(281479271743492UL)
				});
			}
			if (header.CompatibleVersion > 281479271743493UL)
			{
				throw Contracts.Except(this._host, "Cannot read version {0} data, latest that can be handled is {1}", new object[]
				{
					Header.VersionToString(header.CompatibleVersion),
					Header.VersionToString(281479271743493UL)
				});
			}
			Contracts.CheckDecode(this._host, header.RowCount >= 0L, "Row count cannot be negative");
			Contracts.CheckDecode(this._host, header.ColumnCount >= 0, "Column count cannot be negative");
			if (header.ColumnCount != 0 && header.TableOfContentsOffset < 256L)
			{
				throw Contracts.ExceptDecode(this._host, "Table of contents offset {0} less than header size, impossible", new object[] { header.TableOfContentsOffset });
			}
			if (header.TailOffset < 256L)
			{
				throw Contracts.ExceptDecode(this._host, "Tail offset {0} less than header size, impossible", new object[] { header.TailOffset });
			}
			this._stream.Seek(header.TailOffset, SeekOrigin.Begin);
			ulong num2 = this._reader.ReadUInt64();
			Contracts.CheckDecode(this._host, num2 == 4849615937778106880UL, "Incorrect tail signature");
			return header;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00060DF0 File Offset: 0x0005EFF0
		private void InitTOC(IChannel ch, out BinaryLoader.TableOfContentsEntry[] aliveColumns, out BinaryLoader.TableOfContentsEntry[] deadColumns, out int allRowsPerBlock, out long tocEndOffset)
		{
			if (this._header.ColumnCount > 0)
			{
				this._stream.Seek(this._header.TableOfContentsOffset, SeekOrigin.Begin);
			}
			List<BinaryLoader.TableOfContentsEntry> list = new List<BinaryLoader.TableOfContentsEntry>();
			List<BinaryLoader.TableOfContentsEntry> list2 = new List<BinaryLoader.TableOfContentsEntry>();
			allRowsPerBlock = 0;
			for (int i = 0; i < this._header.ColumnCount; i++)
			{
				string text = this._reader.ReadString();
				IValueCodec valueCodec;
				bool flag = this._factory.TryReadCodec(this._stream, out valueCodec);
				CompressionKind compressionKind = (CompressionKind)this._reader.ReadByte();
				bool flag2 = Enum.IsDefined(typeof(CompressionKind), compressionKind);
				int num = (int)Utils.ReadLEB128Int(this._reader);
				if (0 >= num && (num != 0 || this._header.RowCount != 0L))
				{
					throw Contracts.ExceptDecode(ch, "Bad number of rows per block {0} read", new object[] { num });
				}
				if (i == 0)
				{
					allRowsPerBlock = num;
				}
				else if (allRowsPerBlock != num)
				{
					throw Contracts.ExceptNotSupp(ch, "Different rows per block per column not supported yet, encountered {0} and {1}", new object[] { allRowsPerBlock, num });
				}
				long num2 = this._reader.ReadInt64();
				if (this._header.RowCount > 0L)
				{
					long num3 = (this._header.RowCount - 1L) / (long)num + 1L;
					Contracts.CheckDecode(ch, 256L <= num2 && num2 <= this._header.TailOffset - 16L * num3, "Lookup table offset out of range");
				}
				long num4 = this._reader.ReadInt64();
				Contracts.CheckDecode(ch, num4 == 0L || (256L <= num4 && num4 <= this._header.TailOffset), "Metadata TOC offset out of range");
				BinaryLoader.TableOfContentsEntry tableOfContentsEntry = new BinaryLoader.TableOfContentsEntry(this, i, text, valueCodec, compressionKind, num, num2, num4);
				if (flag && flag2)
				{
					list.Add(tableOfContentsEntry);
				}
				else
				{
					ch.Warning("Cannot interpret column '{0}' at index {1} because {2} unrecognized", new object[]
					{
						text,
						i,
						flag ? "compression" : (flag2 ? "codec" : "codec and compression")
					});
					list2.Add(tableOfContentsEntry);
				}
			}
			tocEndOffset = this._stream.Position;
			if (this._generatedRowIndexName != null)
			{
				ch.Trace("Creating generated column to hold row index, named '{0}'", new object[] { this._generatedRowIndexName });
				list.Add(this.CreateRowIndexEntry(this._generatedRowIndexName));
			}
			aliveColumns = list.ToArray();
			deadColumns = list2.ToArray();
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00061086 File Offset: 0x0005F286
		public void Dispose()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				this._reader.Dispose();
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000610A4 File Offset: 0x0005F2A4
		private void CalculateShufflePoolRows(IChannel ch, out int poolRows)
		{
			if (!ShuffleTransform.CanShuffleAll(this.Schema))
			{
				ch.Warning("Not adding implicit shuffle, as we did not know how to copy some types of values");
				poolRows = 0;
			}
			double num = Math.Ceiling(this._shuffleBlocks * (double)this._rowsPerBlock);
			if (num < 2.0)
			{
				ch.Trace("Not adding implicit shuffle, as it is unnecessary");
				poolRows = 0;
				return;
			}
			if (num > 268435456.0)
			{
				num = 268435456.0;
			}
			if (num > (double)this._header.RowCount)
			{
				num = (double)this._header.RowCount;
			}
			poolRows = checked((int)num);
			ch.Trace("Implicit shuffle will have pool size {0}", new object[] { poolRows });
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00061158 File Offset: 0x0005F358
		private BinaryLoader.TableOfContentsEntry CreateRowIndexEntry(string rowIndexName)
		{
			int num = ((this._header.RowCount <= 2147483647L) ? ((int)this._header.RowCount) : 0);
			KeyType keyType = new KeyType(8, 0UL, num, true);
			ValueMapper<long, ulong> valueMapper = delegate(ref long src, ref ulong dst)
			{
				dst = (ulong)(src + 1L);
			};
			return new BinaryLoader.TableOfContentsEntry(this, rowIndexName, keyType, valueMapper);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000611BC File Offset: 0x0005F3BC
		private IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			if (rand != null && this._randomShufflePoolRows > 0)
			{
				IRandom random = (((long)this._randomShufflePoolRows == this._header.RowCount) ? null : rand);
				BinaryLoader.Cursor cursor = new BinaryLoader.Cursor(this, predicate, random);
				return ShuffleTransform.GetShuffledCursor(this._host, this._randomShufflePoolRows, cursor, rand);
			}
			return new BinaryLoader.Cursor(this, predicate, rand);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00061213 File Offset: 0x0005F413
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			return this.GetRowCursorCore(predicate, rand);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00061230 File Offset: 0x0005F430
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursorCore(predicate, rand) };
		}

		// Token: 0x040009E3 RID: 2531
		private const double _defaultShuffleBlocks = 4.0;

		// Token: 0x040009E4 RID: 2532
		private const ulong ReaderVersion = 281479271743493UL;

		// Token: 0x040009E5 RID: 2533
		private const ulong MissingTextVersion = 281479271743493UL;

		// Token: 0x040009E6 RID: 2534
		private const ulong MetadataVersion = 281479271743492UL;

		// Token: 0x040009E7 RID: 2535
		private const ulong SlotNamesVersion = 281479271743491UL;

		// Token: 0x040009E8 RID: 2536
		private const ulong ReaderFirstVersion = 281479271743490UL;

		// Token: 0x040009E9 RID: 2537
		internal const string Summary = "Loads native Binary IDV data file.";

		// Token: 0x040009EA RID: 2538
		internal const string LoadName = "BinaryLoader";

		// Token: 0x040009EB RID: 2539
		internal const string LoaderSignature = "BinaryLoader";

		// Token: 0x040009EC RID: 2540
		private readonly Stream _stream;

		// Token: 0x040009ED RID: 2541
		private readonly BinaryReader _reader;

		// Token: 0x040009EE RID: 2542
		private readonly CodecFactory _factory;

		// Token: 0x040009EF RID: 2543
		private readonly Header _header;

		// Token: 0x040009F0 RID: 2544
		private readonly BinaryLoader.SchemaImpl _schema;

		// Token: 0x040009F1 RID: 2545
		private readonly bool _autodeterminedThreads;

		// Token: 0x040009F2 RID: 2546
		private readonly int _threads;

		// Token: 0x040009F3 RID: 2547
		private readonly string _generatedRowIndexName;

		// Token: 0x040009F4 RID: 2548
		private bool _disposed;

		// Token: 0x040009F5 RID: 2549
		private readonly BinaryLoader.TableOfContentsEntry[] _aliveColumns;

		// Token: 0x040009F6 RID: 2550
		private readonly BinaryLoader.TableOfContentsEntry[] _deadColumns;

		// Token: 0x040009F7 RID: 2551
		private readonly int _rowsPerBlock;

		// Token: 0x040009F8 RID: 2552
		private readonly long _tocEndLim;

		// Token: 0x040009F9 RID: 2553
		private readonly MemoryStreamCollection _bufferCollection;

		// Token: 0x040009FA RID: 2554
		private readonly IHost _host;

		// Token: 0x040009FB RID: 2555
		private readonly double _shuffleBlocks;

		// Token: 0x040009FC RID: 2556
		private readonly int _randomShufflePoolRows;

		// Token: 0x02000310 RID: 784
		public sealed class Arguments
		{
			// Token: 0x04000A02 RID: 2562
			[Argument(4, HelpText = "The number of worker decompressor threads to use", ShortName = "t")]
			public int? threads;

			// Token: 0x04000A03 RID: 2563
			[Argument(4, HelpText = "If specified, the name of a column to generate and append, providing a U8 key-value indicating the index of the row within the binary file", ShortName = "rowIndex", Hide = true)]
			public string rowIndexName;

			// Token: 0x04000A04 RID: 2564
			[Argument(4, HelpText = "When shuffling, the number of blocks worth of data to keep in the shuffle pool. Larger values will make the shuffling more random, but use more memory. Set to 0 to use only block shuffling.", ShortName = "pb")]
			public double poolBlocks = 4.0;
		}

		// Token: 0x02000311 RID: 785
		private sealed class TableOfContentsEntry
		{
			// Token: 0x170001BB RID: 443
			// (get) Token: 0x0600118A RID: 4490 RVA: 0x0006127C File Offset: 0x0005F47C
			public bool IsGenerated
			{
				get
				{
					return this.ColumnIndex == -1;
				}
			}

			// Token: 0x0600118B RID: 4491 RVA: 0x00061288 File Offset: 0x0005F488
			public TableOfContentsEntry(BinaryLoader parent, int index, string name, IValueCodec codec, CompressionKind compression, int rowsPerBlock, long lookupOffset, long metadataTocOffset)
			{
				this._parent = parent;
				this._ectx = this._parent._host;
				this.ColumnIndex = index;
				this.Name = name;
				this.Codec = codec;
				this.Type = ((this.Codec != null) ? this.Codec.Type : null);
				this.Compression = compression;
				this.RowsPerBlock = rowsPerBlock;
				this.LookupOffset = lookupOffset;
				this.MetadataTocOffset = metadataTocOffset;
				this._maxCompLen = -1;
				this._maxDecompLen = -1;
			}

			// Token: 0x0600118C RID: 4492 RVA: 0x00061318 File Offset: 0x0005F518
			public TableOfContentsEntry(BinaryLoader parent, string name, ColumnType type, Delegate valueMapper)
			{
				this._parent = parent;
				this._ectx = this._parent._host;
				this.ColumnIndex = -1;
				this.Name = name;
				this.Type = type;
				this._generatorDelegate = valueMapper;
				this._maxCompLen = 0;
				this._maxDecompLen = 0;
			}

			// Token: 0x0600118D RID: 4493 RVA: 0x00061372 File Offset: 0x0005F572
			public ValueMapper<long, T> GetValueMapper<T>()
			{
				return (ValueMapper<long, T>)this._generatorDelegate;
			}

			// Token: 0x0600118E RID: 4494 RVA: 0x00061380 File Offset: 0x0005F580
			public BlockLookup[] GetLookup()
			{
				if (this.LookupOffset > 0L && this._maxCompLen == -1)
				{
					Stream stream = this._parent._stream;
					lock (stream)
					{
						if (this._maxCompLen == -1)
						{
							long rowCount = this._parent._header.RowCount;
							if (rowCount == 0L)
							{
								return this._lookup = new BlockLookup[0];
							}
							long num = (rowCount - 1L) / (long)this.RowsPerBlock + 1L;
							if (num > 2147483647L)
							{
								throw Contracts.ExceptNotSupp(this._ectx, "This version of the software does not support {0} blocks", new object[] { num });
							}
							BlockLookup[] array = new BlockLookup[num];
							stream.Seek(this.LookupOffset, SeekOrigin.Begin);
							BinaryReader reader = this._parent._reader;
							int num2 = 0;
							int num3 = 0;
							int num4 = 0;
							while ((long)num4 < num)
							{
								long num5 = reader.ReadInt64();
								int num6 = reader.ReadInt32();
								int num7 = reader.ReadInt32();
								Contracts.CheckDecode(this._ectx, 0 <= num6, "negative compressed block length detected");
								Contracts.CheckDecode(this._ectx, 0 <= num7, "negative decompressed block length detected");
								if (num2 < num6)
								{
									num2 = num6;
								}
								if (num3 < num7)
								{
									num3 = num7;
								}
								Contracts.CheckDecode(this._ectx, 256L <= num5 && num5 <= this._parent._header.TailOffset - (long)num6, "block offset out of range");
								array[num4] = new BlockLookup(num5, num6, num7);
								num4++;
							}
							this._lookup = array;
							this._metadataTocEnd = stream.Position;
							this._maxDecompLen = num3;
							this._maxCompLen = num2;
						}
					}
				}
				return this._lookup;
			}

			// Token: 0x0600118F RID: 4495 RVA: 0x0006158C File Offset: 0x0005F78C
			public void GetMaxBlockSizes(out int compressed, out int decompressed)
			{
				if (this._maxCompLen == -1)
				{
					this.GetLookup();
				}
				compressed = this._maxCompLen;
				decompressed = this._maxDecompLen;
			}

			// Token: 0x06001190 RID: 4496 RVA: 0x000615B4 File Offset: 0x0005F7B4
			private void EnsureMetadataStructuresInitialized()
			{
				if (this.MetadataTocOffset <= 0L || this._metadataToc != null)
				{
					return;
				}
				Stream stream = this._parent._stream;
				lock (stream)
				{
					if (this._metadataToc == null)
					{
						using (IChannel channel = this._parent._host.Start("Metadata TOC Read"))
						{
							this.ReadTocMetadata(channel, stream);
							channel.Done();
						}
					}
				}
			}

			// Token: 0x06001191 RID: 4497 RVA: 0x00061654 File Offset: 0x0005F854
			private void ReadTocMetadata(IChannel ch, Stream stream)
			{
				stream.Seek(this.MetadataTocOffset, SeekOrigin.Begin);
				BinaryReader reader = this._parent._reader;
				ulong num = Utils.ReadLEB128Int(reader);
				Contracts.CheckDecode(ch, 0UL < num && num < 2147483647UL, "Bad number of metadata TOC entries read");
				List<BinaryLoader.MetadataTableOfContentsEntry> list = new List<BinaryLoader.MetadataTableOfContentsEntry>();
				List<BinaryLoader.MetadataTableOfContentsEntry> list2 = new List<BinaryLoader.MetadataTableOfContentsEntry>();
				Dictionary<string, BinaryLoader.MetadataTableOfContentsEntry> dictionary = new Dictionary<string, BinaryLoader.MetadataTableOfContentsEntry>();
				HashSet<string> hashSet = new HashSet<string>();
				for (int i = 0; i < (int)num; i++)
				{
					string text = reader.ReadString();
					Contracts.CheckDecode(ch, !string.IsNullOrEmpty(text), "Metadata kind must be non-empty string");
					Contracts.CheckDecode(ch, hashSet.Add(text), "Duplicate metadata kind read from file");
					IValueCodec valueCodec;
					bool flag = this._parent._factory.TryReadCodec(stream, out valueCodec);
					CompressionKind compressionKind = (CompressionKind)reader.ReadByte();
					bool flag2 = Enum.IsDefined(typeof(CompressionKind), compressionKind);
					long num2 = reader.ReadInt64();
					Contracts.CheckDecode(ch, 256L <= num2 && num2 <= this._parent._header.TailOffset, "Metadata block offset out of range");
					ulong num3 = Utils.ReadLEB128Int(reader);
					Contracts.CheckDecode(ch, num3 <= 9223372036854775807UL, "Metadata block size out of range");
					long num4 = (long)num3;
					Contracts.CheckDecode(ch, 0L < num4 && num4 <= this._parent._header.TailOffset - num2, "Metadata block size out of range");
					if (flag && flag2)
					{
						BinaryLoader.MetadataTableOfContentsEntry metadataTableOfContentsEntry = BinaryLoader.MetadataTableOfContentsEntry.Create(this._parent, text, valueCodec, compressionKind, num2, num4);
						list.Add(metadataTableOfContentsEntry);
						dictionary[text] = metadataTableOfContentsEntry;
					}
					else
					{
						ch.Warning("Cannot interpret metadata of kind '{0}' because {1} unrecognized", new object[] { flag ? "compression" : (flag2 ? "codec" : "codec and compression") });
						BinaryLoader.MetadataTableOfContentsEntry metadataTableOfContentsEntry2 = BinaryLoader.MetadataTableOfContentsEntry.CreateDead(this._parent, text, valueCodec, compressionKind, num2, num4);
						list2.Add(metadataTableOfContentsEntry2);
					}
				}
				this._metadataToc = list.ToArray();
				this._deadMetadataToc = list2.ToArray();
				this._metadataMap = dictionary;
				this._metadataTocEnd = stream.Position;
			}

			// Token: 0x06001192 RID: 4498 RVA: 0x0006187A File Offset: 0x0005FA7A
			public BinaryLoader.MetadataTableOfContentsEntry[] GetMetadataTOCArray()
			{
				this.EnsureMetadataStructuresInitialized();
				return this._metadataToc;
			}

			// Token: 0x06001193 RID: 4499 RVA: 0x0006188A File Offset: 0x0005FA8A
			public BinaryLoader.MetadataTableOfContentsEntry[] GetDeadMetadataTOCArray()
			{
				this.EnsureMetadataStructuresInitialized();
				return this._deadMetadataToc;
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x0006189C File Offset: 0x0005FA9C
			public BinaryLoader.MetadataTableOfContentsEntry GetMetadataTOCEntryOrNull(string kind)
			{
				this.EnsureMetadataStructuresInitialized();
				if (this._metadataMap == null)
				{
					return null;
				}
				BinaryLoader.MetadataTableOfContentsEntry metadataTableOfContentsEntry;
				this._metadataMap.TryGetValue(kind, out metadataTableOfContentsEntry);
				return metadataTableOfContentsEntry;
			}

			// Token: 0x06001195 RID: 4501 RVA: 0x000618CD File Offset: 0x0005FACD
			public long GetMetadataTocEndOffset()
			{
				this.EnsureMetadataStructuresInitialized();
				return this._metadataTocEnd;
			}

			// Token: 0x04000A05 RID: 2565
			public readonly string Name;

			// Token: 0x04000A06 RID: 2566
			public readonly IValueCodec Codec;

			// Token: 0x04000A07 RID: 2567
			public readonly ColumnType Type;

			// Token: 0x04000A08 RID: 2568
			public readonly CompressionKind Compression;

			// Token: 0x04000A09 RID: 2569
			public readonly int RowsPerBlock;

			// Token: 0x04000A0A RID: 2570
			public readonly long LookupOffset;

			// Token: 0x04000A0B RID: 2571
			public readonly long MetadataTocOffset;

			// Token: 0x04000A0C RID: 2572
			public readonly int ColumnIndex;

			// Token: 0x04000A0D RID: 2573
			private readonly Delegate _generatorDelegate;

			// Token: 0x04000A0E RID: 2574
			private readonly BinaryLoader _parent;

			// Token: 0x04000A0F RID: 2575
			private readonly IExceptionContext _ectx;

			// Token: 0x04000A10 RID: 2576
			private volatile BlockLookup[] _lookup;

			// Token: 0x04000A11 RID: 2577
			private volatile int _maxCompLen;

			// Token: 0x04000A12 RID: 2578
			private volatile int _maxDecompLen;

			// Token: 0x04000A13 RID: 2579
			private volatile BinaryLoader.MetadataTableOfContentsEntry[] _metadataToc;

			// Token: 0x04000A14 RID: 2580
			private volatile BinaryLoader.MetadataTableOfContentsEntry[] _deadMetadataToc;

			// Token: 0x04000A15 RID: 2581
			private volatile Dictionary<string, BinaryLoader.MetadataTableOfContentsEntry> _metadataMap;

			// Token: 0x04000A16 RID: 2582
			private long _metadataTocEnd;
		}

		// Token: 0x02000312 RID: 786
		private abstract class MetadataTableOfContentsEntry
		{
			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06001196 RID: 4502
			public abstract IValueCodec Codec { get; }

			// Token: 0x06001197 RID: 4503 RVA: 0x000618DB File Offset: 0x0005FADB
			protected MetadataTableOfContentsEntry(BinaryLoader parent, string kind, CompressionKind compression, long blockOffset, long blockSize)
			{
				this._parent = parent;
				this.Kind = kind;
				this.Compression = compression;
				this.BlockOffset = blockOffset;
				this.BlockSize = blockSize;
			}

			// Token: 0x06001198 RID: 4504 RVA: 0x00061908 File Offset: 0x0005FB08
			public static BinaryLoader.MetadataTableOfContentsEntry Create(BinaryLoader parent, string kind, IValueCodec codec, CompressionKind compression, long blockOffset, long blockSize)
			{
				IHost host = parent._host;
				ColumnType type = codec.Type;
				Type type2;
				if (type.IsVector)
				{
					Type rawType = type.RawType;
					Type[] genericArguments = rawType.GetGenericArguments();
					type2 = typeof(BinaryLoader.MetadataTableOfContentsEntry.ImplVec<>).MakeGenericType(genericArguments);
				}
				else
				{
					type2 = typeof(BinaryLoader.MetadataTableOfContentsEntry.ImplOne<>).MakeGenericType(new Type[] { type.RawType });
				}
				return (BinaryLoader.MetadataTableOfContentsEntry)Activator.CreateInstance(type2, new object[] { parent, kind, codec, compression, blockOffset, blockSize });
			}

			// Token: 0x06001199 RID: 4505 RVA: 0x000619B8 File Offset: 0x0005FBB8
			public static BinaryLoader.MetadataTableOfContentsEntry CreateDead(BinaryLoader parent, string kind, IValueCodec codec, CompressionKind compression, long blockOffset, long blockSize)
			{
				return new BinaryLoader.MetadataTableOfContentsEntry.ImplDead(parent, kind, codec, compression, blockOffset, blockSize);
			}

			// Token: 0x04000A17 RID: 2583
			public readonly string Kind;

			// Token: 0x04000A18 RID: 2584
			public readonly CompressionKind Compression;

			// Token: 0x04000A19 RID: 2585
			public readonly long BlockOffset;

			// Token: 0x04000A1A RID: 2586
			public readonly long BlockSize;

			// Token: 0x04000A1B RID: 2587
			protected readonly BinaryLoader _parent;

			// Token: 0x02000313 RID: 787
			private sealed class ImplDead : BinaryLoader.MetadataTableOfContentsEntry
			{
				// Token: 0x170001BD RID: 445
				// (get) Token: 0x0600119A RID: 4506 RVA: 0x000619C7 File Offset: 0x0005FBC7
				public override IValueCodec Codec
				{
					get
					{
						return this._codec;
					}
				}

				// Token: 0x0600119B RID: 4507 RVA: 0x000619CF File Offset: 0x0005FBCF
				public ImplDead(BinaryLoader parent, string kind, IValueCodec codec, CompressionKind compression, long blockOffset, long blockSize)
					: base(parent, kind, compression, blockOffset, blockSize)
				{
					this._codec = codec;
				}

				// Token: 0x04000A1C RID: 2588
				private readonly IValueCodec _codec;
			}

			// Token: 0x02000315 RID: 789
			private sealed class ImplOne<T> : BinaryLoader.MetadataTableOfContentsEntry<T>
			{
				// Token: 0x060011A0 RID: 4512 RVA: 0x00061AFC File Offset: 0x0005FCFC
				public ImplOne(BinaryLoader parent, string kind, IValueCodec<T> codec, CompressionKind compression, long blockOffset, long blockSize)
					: base(parent, kind, codec, compression, blockOffset, blockSize)
				{
				}

				// Token: 0x060011A1 RID: 4513 RVA: 0x00061B0D File Offset: 0x0005FD0D
				public override void Get(ref T value)
				{
					base.EnsureValue();
					value = this._value;
				}
			}

			// Token: 0x02000316 RID: 790
			private sealed class ImplVec<T> : BinaryLoader.MetadataTableOfContentsEntry<VBuffer<T>>
			{
				// Token: 0x060011A2 RID: 4514 RVA: 0x00061B21 File Offset: 0x0005FD21
				public ImplVec(BinaryLoader parent, string kind, IValueCodec<VBuffer<T>> codec, CompressionKind compression, long blockOffset, long blockSize)
					: base(parent, kind, codec, compression, blockOffset, blockSize)
				{
				}

				// Token: 0x060011A3 RID: 4515 RVA: 0x00061B32 File Offset: 0x0005FD32
				public override void Get(ref VBuffer<T> value)
				{
					base.EnsureValue();
					this._value.CopyTo(ref value);
				}
			}
		}

		// Token: 0x02000314 RID: 788
		private abstract class MetadataTableOfContentsEntry<T> : BinaryLoader.MetadataTableOfContentsEntry
		{
			// Token: 0x170001BE RID: 446
			// (get) Token: 0x0600119C RID: 4508 RVA: 0x000619E6 File Offset: 0x0005FBE6
			public override IValueCodec Codec
			{
				get
				{
					return this._codec;
				}
			}

			// Token: 0x0600119D RID: 4509 RVA: 0x000619EE File Offset: 0x0005FBEE
			protected MetadataTableOfContentsEntry(BinaryLoader parent, string kind, IValueCodec<T> codec, CompressionKind compression, long blockOffset, long blockSize)
				: base(parent, kind, compression, blockOffset, blockSize)
			{
				this._codec = codec;
			}

			// Token: 0x0600119E RID: 4510 RVA: 0x00061A08 File Offset: 0x0005FC08
			protected void EnsureValue()
			{
				if (!this._fetched)
				{
					Stream stream = this._parent._stream;
					lock (stream)
					{
						if (!this._fetched)
						{
							stream.Seek(this.BlockOffset, SeekOrigin.Begin);
							using (SubsetStream subsetStream = new SubsetStream(stream, new long?(this.BlockSize)))
							{
								using (Stream stream3 = this.Compression.DecompressStream(subsetStream))
								{
									using (IValueReader<T> valueReader = this._codec.OpenReader(stream3, 1))
									{
										valueReader.MoveNext();
										valueReader.Get(ref this._value);
									}
								}
							}
							this._fetched = true;
						}
					}
				}
			}

			// Token: 0x0600119F RID: 4511
			public abstract void Get(ref T value);

			// Token: 0x04000A1D RID: 2589
			private bool _fetched;

			// Token: 0x04000A1E RID: 2590
			private readonly IValueCodec<T> _codec;

			// Token: 0x04000A1F RID: 2591
			protected T _value;
		}

		// Token: 0x02000317 RID: 791
		private sealed class SchemaImpl : ISchema
		{
			// Token: 0x060011A4 RID: 4516 RVA: 0x00061B48 File Offset: 0x0005FD48
			public SchemaImpl(BinaryLoader parent)
			{
				this._ectx = parent._host;
				this._name2col = new Dictionary<string, int>();
				this._toc = parent._aliveColumns;
				for (int i = 0; i < this._toc.Length; i++)
				{
					this._name2col[this._toc[i].Name] = i;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00061BAA File Offset: 0x0005FDAA
			public int ColumnCount
			{
				get
				{
					return this._toc.Length;
				}
			}

			// Token: 0x060011A6 RID: 4518 RVA: 0x00061BB4 File Offset: 0x0005FDB4
			public bool TryGetColumnIndex(string name, out int col)
			{
				if (name == null)
				{
					col = 0;
					return false;
				}
				return this._name2col.TryGetValue(name, out col);
			}

			// Token: 0x060011A7 RID: 4519 RVA: 0x00061BCB File Offset: 0x0005FDCB
			public string GetColumnName(int col)
			{
				Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
				return this._toc[col].Name;
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x00061BFA File Offset: 0x0005FDFA
			public ColumnType GetColumnType(int col)
			{
				Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
				return this._toc[col].Type;
			}

			// Token: 0x060011A9 RID: 4521 RVA: 0x00061C44 File Offset: 0x0005FE44
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
				BinaryLoader.MetadataTableOfContentsEntry[] metadataTOCArray = this._toc[col].GetMetadataTOCArray();
				if (Utils.Size<BinaryLoader.MetadataTableOfContentsEntry>(metadataTOCArray) > 0)
				{
					return metadataTOCArray.Select((BinaryLoader.MetadataTableOfContentsEntry e) => new KeyValuePair<string, ColumnType>(e.Kind, e.Codec.Type));
				}
				return Enumerable.Empty<KeyValuePair<string, ColumnType>>();
			}

			// Token: 0x060011AA RID: 4522 RVA: 0x00061CB4 File Offset: 0x0005FEB4
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				Contracts.CheckNonEmpty(this._ectx, kind, "kind");
				Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
				BinaryLoader.MetadataTableOfContentsEntry metadataTOCEntryOrNull = this._toc[col].GetMetadataTOCEntryOrNull(kind);
				if (metadataTOCEntryOrNull != null)
				{
					return metadataTOCEntryOrNull.Codec.Type;
				}
				return null;
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x00061D14 File Offset: 0x0005FF14
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				Contracts.CheckNonEmpty(this._ectx, kind, "kind");
				Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
				BinaryLoader.MetadataTableOfContentsEntry<TValue> metadataTableOfContentsEntry = this._toc[col].GetMetadataTOCEntryOrNull(kind) as BinaryLoader.MetadataTableOfContentsEntry<TValue>;
				if (metadataTableOfContentsEntry == null)
				{
					throw MetadataUtils.ExceptGetMetadata();
				}
				metadataTableOfContentsEntry.Get(ref value);
			}

			// Token: 0x04000A20 RID: 2592
			private readonly BinaryLoader.TableOfContentsEntry[] _toc;

			// Token: 0x04000A21 RID: 2593
			private readonly Dictionary<string, int> _name2col;

			// Token: 0x04000A22 RID: 2594
			private readonly IExceptionContext _ectx;
		}

		// Token: 0x02000318 RID: 792
		private sealed class Cursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x060011AD RID: 4525 RVA: 0x00061D77 File Offset: 0x0005FF77
			public ISchema Schema
			{
				get
				{
					return this._parent.Schema;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x060011AE RID: 4526 RVA: 0x00061D84 File Offset: 0x0005FF84
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x060011AF RID: 4527 RVA: 0x00061D88 File Offset: 0x0005FF88
			public Cursor(BinaryLoader parent, Func<int, bool> predicate, IRandom rand)
				: base(parent._host)
			{
				this._parent = parent;
				BinaryLoader.SchemaImpl schema = this._parent._schema;
				this._exMarshaller = new ExceptionMarshaller();
				BinaryLoader.TableOfContentsEntry[] aliveColumns = this._parent._aliveColumns;
				int[] array;
				Utils.BuildSubsetMaps(aliveColumns.Length, predicate, ref array, ref this._colToActivesIndex);
				this._actives = new BinaryLoader.TableOfContentsEntry[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this._actives[i] = aliveColumns[array[i]];
				}
				this._lastValidCounter = this._parent._header.RowCount - 1L;
				this._pipes = new BinaryLoader.Cursor.ReadPipe[(parent.RowCount > 0L) ? this._actives.Length : 0];
				this._pipeGetters = new Delegate[this._actives.Length];
				this._rowsPerBlock = this._parent._rowsPerBlock;
				if (this._rowsPerBlock == 0)
				{
					this._rowsPerBlock = int.MaxValue;
				}
				this._rowsInLastBlock = ((this._parent.RowCount == 0L) ? 0 : ((int)(this._parent.RowCount % (long)this._rowsPerBlock)));
				if (this._rowsInLastBlock == 0)
				{
					this._rowsInLastBlock = this._rowsPerBlock;
				}
				checked
				{
					this._numBlocks = (int)((this._parent.RowCount - 1L) / unchecked((long)this._rowsPerBlock) + 1L);
					this._blockShuffleOrder = ((rand == null || this._numBlocks == 0) ? null : Utils.GetRandomPermutation(rand, this._numBlocks));
				}
				if (this._pipes.Length == 0)
				{
					for (int j = 0; j < this._pipeGetters.Length; j++)
					{
						this._pipeGetters[j] = this.GetNoRowGetter(this._actives[j].Type);
					}
					return;
				}
				int num = 2 * ((this._parent._threads + this._pipes.Length - 1) / this._pipes.Length);
				for (int k = 0; k < this._pipes.Length; k++)
				{
					this._pipes[k] = BinaryLoader.Cursor.ReadPipe.Create(this, k, num);
					this._pipeGetters[k] = this._pipes[k].GetGetter();
				}
				this._readerThread = Utils.CreateBackgroundThread(new ThreadStart(this.ReaderWorker));
				this._readerThread.Start();
				this._pipeTask = this.SetupDecompressTask();
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x00061FC0 File Offset: 0x000601C0
			public override void Dispose()
			{
				if (!this._disposed && this._readerThread != null)
				{
					this._disposed = true;
					try
					{
						bool flag;
						do
						{
							flag = false;
							for (int i = 0; i < this._pipes.Length; i++)
							{
								flag |= this._pipes[i].MoveNextCleanup();
							}
						}
						while (flag);
					}
					catch (OperationCanceledException)
					{
						this._exMarshaller.ThrowIfSet(this._ch);
					}
					finally
					{
						this._pipeTask.Wait();
						this._readerThread.Join();
					}
				}
				base.Dispose();
			}

			// Token: 0x060011B1 RID: 4529 RVA: 0x00062104 File Offset: 0x00060304
			private Task SetupDecompressTask()
			{
				Thread[] pipeWorkers = new Thread[this._parent._threads];
				long decompressSequence = -1L;
				for (int i = 0; i < pipeWorkers.Length; i++)
				{
					Thread thread = (pipeWorkers[i] = Utils.CreateBackgroundThread(delegate
					{
						try
						{
							int num2;
							do
							{
								long num = Interlocked.Increment(ref decompressSequence);
								num2 = (int)(num % (long)this._pipes.Length);
							}
							while (this._pipes[num2].DecompressOne());
						}
						catch (Exception ex)
						{
							this._exMarshaller.Set("decompressing", ex);
						}
					}));
					thread.Start();
				}
				Task task = new Task(delegate
				{
					foreach (Thread thread2 in pipeWorkers)
					{
						thread2.Join();
					}
				});
				task.Start();
				return task;
			}

			// Token: 0x060011B2 RID: 4530 RVA: 0x0006219C File Offset: 0x0006039C
			private void ReaderWorker()
			{
				try
				{
					int num;
					int num2;
					checked
					{
						num = (int)((this._parent.RowCount - 1L) / unchecked((long)this._rowsPerBlock) + 1L);
						Stream stream = this._parent._stream;
						num2 = 0;
					}
					while (num2 < num && !this._disposed)
					{
						int num3 = ((this._blockShuffleOrder == null) ? num2 : this._blockShuffleOrder[num2]);
						int num4 = ((num3 == num - 1) ? this._rowsInLastBlock : this._rowsPerBlock);
						for (int i = 0; i < this._pipes.Length; i++)
						{
							this._pipes[i].PrepAndSendCompressedBlock((long)num3, (long)num2, num4);
						}
						num2++;
					}
					for (int j = 0; j < this._pipes.Length; j++)
					{
						this._pipes[j].SendSentinelBlock((long)num2);
					}
				}
				catch (Exception ex)
				{
					this._exMarshaller.Set("reading", ex);
				}
			}

			// Token: 0x060011B3 RID: 4531 RVA: 0x00062288 File Offset: 0x00060488
			public bool IsColumnActive(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._colToActivesIndex.Length, "col");
				return this._colToActivesIndex[col] >= 0;
			}

			// Token: 0x060011B4 RID: 4532 RVA: 0x000622BC File Offset: 0x000604BC
			protected override bool MoveNextCore()
			{
				bool flag = base.Position != this._lastValidCounter;
				for (int i = 0; i < this._pipes.Length; i++)
				{
					try
					{
						this._pipes[i].MoveNext();
					}
					catch (OperationCanceledException)
					{
						this._disposed = true;
						this._pipeTask.Wait(100);
						this._readerThread.Join(100);
						this._exMarshaller.ThrowIfSet(this._ch);
						throw;
					}
				}
				if (!flag && this._pipes.Length > 0)
				{
					this._disposed = true;
					this._pipeTask.Wait();
					this._readerThread.Join();
				}
				return flag;
			}

			// Token: 0x060011B5 RID: 4533 RVA: 0x00062378 File Offset: 0x00060578
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._colToActivesIndex.Length, "col");
				Contracts.CheckParam(this._ch, this._colToActivesIndex[col] >= 0, "col", "requested column not active");
				ValueGetter<TValue> valueGetter = this._pipeGetters[this._colToActivesIndex[col]] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x060011B6 RID: 4534 RVA: 0x00062409 File Offset: 0x00060609
			private Delegate GetNoRowGetter(ColumnType type)
			{
				return Utils.MarshalInvoke<Delegate>(new Func<Delegate>(this.NoRowGetter<int>), type.RawType);
			}

			// Token: 0x060011B7 RID: 4535 RVA: 0x00062434 File Offset: 0x00060634
			private Delegate NoRowGetter<T>()
			{
				return new ValueGetter<T>(delegate(ref T value)
				{
					throw Contracts.Except(this._ch, "cursor is either not started or is ended, and cannot get values");
				});
			}

			// Token: 0x060011B8 RID: 4536 RVA: 0x0006251C File Offset: 0x0006071C
			public override ValueGetter<UInt128> GetIdGetter()
			{
				if (this._blockShuffleOrder == null)
				{
					return delegate(ref UInt128 val)
					{
						Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
						val = new UInt128((ulong)base.Position, 0UL);
					};
				}
				int num = 0;
				for (int i = 1; i < this._blockShuffleOrder.Length; i++)
				{
					if (this._blockShuffleOrder[i] > this._blockShuffleOrder[num])
					{
						num = i;
					}
				}
				int correction = this._rowsPerBlock - this._rowsInLastBlock;
				long firstPositionToCorrect = (long)num * (long)this._rowsPerBlock + (long)this._rowsInLastBlock;
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, this.IsGood, "Cannot call ID getter in current state");
					long num2 = this.Position;
					if (num2 >= firstPositionToCorrect)
					{
						num2 += (long)correction;
					}
					long num3 = (long)this._rowsPerBlock * (long)this._blockShuffleOrder[(int)(num2 / (long)this._rowsPerBlock)];
					num3 += num2 % (long)this._rowsPerBlock;
					val = new UInt128((ulong)num3, 0UL);
				};
			}

			// Token: 0x04000A24 RID: 2596
			private const string _badCursorState = "cursor is either not started or is ended, and cannot get values";

			// Token: 0x04000A25 RID: 2597
			private readonly BinaryLoader _parent;

			// Token: 0x04000A26 RID: 2598
			private readonly int[] _colToActivesIndex;

			// Token: 0x04000A27 RID: 2599
			private readonly BinaryLoader.TableOfContentsEntry[] _actives;

			// Token: 0x04000A28 RID: 2600
			private readonly int _numBlocks;

			// Token: 0x04000A29 RID: 2601
			private readonly int _rowsPerBlock;

			// Token: 0x04000A2A RID: 2602
			private readonly int _rowsInLastBlock;

			// Token: 0x04000A2B RID: 2603
			private readonly BinaryLoader.Cursor.ReadPipe[] _pipes;

			// Token: 0x04000A2C RID: 2604
			private readonly Delegate[] _pipeGetters;

			// Token: 0x04000A2D RID: 2605
			private readonly long _lastValidCounter;

			// Token: 0x04000A2E RID: 2606
			private readonly int[] _blockShuffleOrder;

			// Token: 0x04000A2F RID: 2607
			private readonly Thread _readerThread;

			// Token: 0x04000A30 RID: 2608
			private readonly Task _pipeTask;

			// Token: 0x04000A31 RID: 2609
			private readonly ExceptionMarshaller _exMarshaller;

			// Token: 0x04000A32 RID: 2610
			private volatile bool _disposed;

			// Token: 0x02000319 RID: 793
			private abstract class ReadPipe
			{
				// Token: 0x170001C2 RID: 450
				// (get) Token: 0x060011BB RID: 4539 RVA: 0x000625B2 File Offset: 0x000607B2
				protected ExceptionMarshaller ExMarshaller
				{
					get
					{
						return this._parent._exMarshaller;
					}
				}

				// Token: 0x170001C3 RID: 451
				// (get) Token: 0x060011BC RID: 4540 RVA: 0x000625BF File Offset: 0x000607BF
				protected IExceptionContext Ectx
				{
					get
					{
						return this._parent._ch;
					}
				}

				// Token: 0x060011BD RID: 4541 RVA: 0x000625CC File Offset: 0x000607CC
				public static BinaryLoader.Cursor.ReadPipe Create(BinaryLoader.Cursor parent, int columnIndex, int bufferSize)
				{
					BinaryLoader.TableOfContentsEntry tableOfContentsEntry = parent._actives[columnIndex];
					Type type = (tableOfContentsEntry.IsGenerated ? typeof(BinaryLoader.Cursor.ReadPipeGenerated<>) : typeof(BinaryLoader.Cursor.ReadPipe<>));
					type = type.MakeGenericType(new Type[] { tableOfContentsEntry.Type.RawType });
					return (BinaryLoader.Cursor.ReadPipe)Activator.CreateInstance(type, new object[] { parent, columnIndex, bufferSize });
				}

				// Token: 0x060011BE RID: 4542 RVA: 0x00062647 File Offset: 0x00060847
				protected ReadPipe(BinaryLoader.Cursor parent, int columnIndex)
				{
					this._parent = parent;
					this._columnIndex = columnIndex;
				}

				// Token: 0x060011BF RID: 4543
				public abstract void PrepAndSendCompressedBlock(long blockIndex, long blockSequence, int rowCount);

				// Token: 0x060011C0 RID: 4544
				public abstract void SendSentinelBlock(long blockSequence);

				// Token: 0x060011C1 RID: 4545
				public abstract bool DecompressOne();

				// Token: 0x060011C2 RID: 4546
				public abstract bool MoveNext();

				// Token: 0x060011C3 RID: 4547
				public abstract bool MoveNextCleanup();

				// Token: 0x060011C4 RID: 4548
				public abstract Delegate GetGetter();

				// Token: 0x04000A33 RID: 2611
				protected readonly int _columnIndex;

				// Token: 0x04000A34 RID: 2612
				protected readonly BinaryLoader.Cursor _parent;
			}

			// Token: 0x0200031A RID: 794
			private sealed class ReadPipeGenerated<T> : BinaryLoader.Cursor.ReadPipe
			{
				// Token: 0x060011C5 RID: 4549 RVA: 0x00062660 File Offset: 0x00060860
				public ReadPipeGenerated(BinaryLoader.Cursor parent, int columnIndex, int bufferSize)
					: base(parent, columnIndex)
				{
					BinaryLoader.TableOfContentsEntry tableOfContentsEntry = parent._actives[this._columnIndex];
					this._toDecompress = new BlockingCollection<BinaryLoader.Cursor.ReadPipeGenerated<T>.Block>(4);
					this._toDecompressEnumerator = this._toDecompress.GetConsumingEnumerable(base.ExMarshaller.Token).GetEnumerator();
					this._toRead = new BlockingCollection<BinaryLoader.Cursor.ReadPipeGenerated<T>.Block>(bufferSize);
					this._toReadEnumerator = this._toRead.GetConsumingEnumerable(base.ExMarshaller.Token).GetEnumerator();
					this._waiter = new OrderedWaiter(true);
					this._mapper = tableOfContentsEntry.GetValueMapper<T>();
				}

				// Token: 0x060011C6 RID: 4550 RVA: 0x000626F8 File Offset: 0x000608F8
				public override void PrepAndSendCompressedBlock(long blockIndex, long blockSequence, int rowCount)
				{
					long num = blockIndex * (long)this._parent._rowsPerBlock;
					BinaryLoader.Cursor.ReadPipeGenerated<T>.Block block = new BinaryLoader.Cursor.ReadPipeGenerated<T>.Block(blockSequence, num, num + (long)rowCount);
					this._toDecompress.Add(block, base.ExMarshaller.Token);
				}

				// Token: 0x060011C7 RID: 4551 RVA: 0x00062738 File Offset: 0x00060938
				public override void SendSentinelBlock(long blockSequence)
				{
					BinaryLoader.Cursor.ReadPipeGenerated<T>.Block block = new BinaryLoader.Cursor.ReadPipeGenerated<T>.Block(blockSequence);
					this._toDecompress.Add(block, base.ExMarshaller.Token);
					this._toDecompress.CompleteAdding();
				}

				// Token: 0x060011C8 RID: 4552 RVA: 0x00062770 File Offset: 0x00060970
				public override bool DecompressOne()
				{
					BinaryLoader.Cursor.ReadPipeGenerated<T>.Block block;
					lock (this._toDecompressEnumerator)
					{
						if (!this._toDecompressEnumerator.MoveNext())
						{
							return false;
						}
						block = this._toDecompressEnumerator.Current;
					}
					if (block.IsSentinel)
					{
						this._waiter.Wait(block.BlockSequence, base.ExMarshaller.Token);
						this._toRead.CompleteAdding();
						this._waiter.Increment();
						return true;
					}
					if (this._parent._disposed)
					{
						this._waiter.Wait(block.BlockSequence, base.ExMarshaller.Token);
						this._waiter.Increment();
						return true;
					}
					this._waiter.Wait(block.BlockSequence, base.ExMarshaller.Token);
					this._toRead.Add(block, base.ExMarshaller.Token);
					this._waiter.Increment();
					return true;
				}

				// Token: 0x060011C9 RID: 4553 RVA: 0x00062884 File Offset: 0x00060A84
				public override bool MoveNext()
				{
					if (this._remaining == 0)
					{
						if (this._curr != null)
						{
							this._curr = null;
						}
						if (!this._toReadEnumerator.MoveNext())
						{
							return false;
						}
						this._curr = this._toReadEnumerator.Current;
						this._remaining = this._curr.Rows;
					}
					this._remaining--;
					return true;
				}

				// Token: 0x060011CA RID: 4554 RVA: 0x000628E8 File Offset: 0x00060AE8
				public override bool MoveNextCleanup()
				{
					if (this._curr != null)
					{
						this._curr = null;
					}
					if (!this._toReadEnumerator.MoveNext())
					{
						return false;
					}
					this._curr = this._toReadEnumerator.Current;
					return true;
				}

				// Token: 0x060011CB RID: 4555 RVA: 0x0006291C File Offset: 0x00060B1C
				private void Get(ref T value)
				{
					Contracts.Check(base.Ectx, this._curr != null, "cursor is either not started or is ended, and cannot get values");
					long num = this._curr.RowIndexLim - (long)this._remaining - 1L;
					this._mapper.Invoke(ref num, ref value);
				}

				// Token: 0x060011CC RID: 4556 RVA: 0x0006296C File Offset: 0x00060B6C
				public override Delegate GetGetter()
				{
					return new ValueGetter<T>(this.Get);
				}

				// Token: 0x04000A35 RID: 2613
				private const int _bufferSize = 4;

				// Token: 0x04000A36 RID: 2614
				private readonly BlockingCollection<BinaryLoader.Cursor.ReadPipeGenerated<T>.Block> _toDecompress;

				// Token: 0x04000A37 RID: 2615
				private readonly IEnumerator<BinaryLoader.Cursor.ReadPipeGenerated<T>.Block> _toDecompressEnumerator;

				// Token: 0x04000A38 RID: 2616
				private readonly BlockingCollection<BinaryLoader.Cursor.ReadPipeGenerated<T>.Block> _toRead;

				// Token: 0x04000A39 RID: 2617
				private readonly IEnumerator<BinaryLoader.Cursor.ReadPipeGenerated<T>.Block> _toReadEnumerator;

				// Token: 0x04000A3A RID: 2618
				private readonly OrderedWaiter _waiter;

				// Token: 0x04000A3B RID: 2619
				private readonly ValueMapper<long, T> _mapper;

				// Token: 0x04000A3C RID: 2620
				private BinaryLoader.Cursor.ReadPipeGenerated<T>.Block _curr;

				// Token: 0x04000A3D RID: 2621
				private int _remaining;

				// Token: 0x0200031B RID: 795
				private sealed class Block
				{
					// Token: 0x170001C4 RID: 452
					// (get) Token: 0x060011CD RID: 4557 RVA: 0x00062987 File Offset: 0x00060B87
					public bool IsSentinel
					{
						get
						{
							return this.RowIndexMin == -1L;
						}
					}

					// Token: 0x170001C5 RID: 453
					// (get) Token: 0x060011CE RID: 4558 RVA: 0x00062993 File Offset: 0x00060B93
					public int Rows
					{
						get
						{
							return (int)(this.RowIndexLim - this.RowIndexMin);
						}
					}

					// Token: 0x060011CF RID: 4559 RVA: 0x000629A3 File Offset: 0x00060BA3
					public Block(long blockSequence, long min, long lim)
					{
						this.BlockSequence = blockSequence;
						this.RowIndexMin = min;
						this.RowIndexLim = lim;
					}

					// Token: 0x060011D0 RID: 4560 RVA: 0x000629C0 File Offset: 0x00060BC0
					public Block(long blockSequence)
					{
						this.BlockSequence = blockSequence;
						this.RowIndexMin = (this.RowIndexLim = -1L);
					}

					// Token: 0x04000A3E RID: 2622
					public readonly long BlockSequence;

					// Token: 0x04000A3F RID: 2623
					public readonly long RowIndexMin;

					// Token: 0x04000A40 RID: 2624
					public readonly long RowIndexLim;
				}
			}

			// Token: 0x0200031C RID: 796
			private sealed class ReadPipe<T> : BinaryLoader.Cursor.ReadPipe
			{
				// Token: 0x060011D1 RID: 4561 RVA: 0x000629EC File Offset: 0x00060BEC
				public ReadPipe(BinaryLoader.Cursor parent, int columnIndex, int bufferSize)
					: base(parent, columnIndex)
				{
					BinaryLoader.TableOfContentsEntry tableOfContentsEntry = this._parent._actives[this._columnIndex];
					this._codec = (IValueCodec<T>)tableOfContentsEntry.Codec;
					this._compression = tableOfContentsEntry.Compression;
					int num;
					int num2;
					tableOfContentsEntry.GetMaxBlockSizes(out num, out num2);
					this._compPool = parent._parent._bufferCollection.Get(num);
					this._decompPool = parent._parent._bufferCollection.Get(num2);
					this._lookup = tableOfContentsEntry.GetLookup();
					this._stream = parent._parent._stream;
					this._toDecompress = new BlockingCollection<BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock>(4);
					this._toDecompressEnumerator = this._toDecompress.GetConsumingEnumerable(base.ExMarshaller.Token).GetEnumerator();
					this._toRead = new BlockingCollection<BinaryLoader.Cursor.ReadPipe<T>.ReaderContainer>(bufferSize);
					this._toReadEnumerator = this._toRead.GetConsumingEnumerable(base.ExMarshaller.Token).GetEnumerator();
					this._waiter = new OrderedWaiter(true);
				}

				// Token: 0x060011D2 RID: 4562 RVA: 0x00062AEC File Offset: 0x00060CEC
				public override void PrepAndSendCompressedBlock(long blockIndex, long blockSequence, int rowCount)
				{
					BlockLookup blockLookup = this._lookup[(int)blockIndex];
					MemoryStream memoryStream = this._compPool.Get();
					BinaryLoader.Cursor.ReadPipe<T>.EnsureCapacity(memoryStream, blockLookup.BlockLength);
					memoryStream.SetLength((long)blockLookup.BlockLength);
					ArraySegment<byte> arraySegment;
					Utils.TryGetBuffer(memoryStream, ref arraySegment);
					lock (this._stream)
					{
						this._stream.Seek(blockLookup.BlockOffset, SeekOrigin.Begin);
						Utils.ReadBlock(this._stream, arraySegment.Array, arraySegment.Offset, arraySegment.Count);
					}
					BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock compressedBlock = new BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock(memoryStream, blockLookup.DecompressedBlockLength, blockIndex, blockSequence, rowCount);
					this._toDecompress.Add(compressedBlock, base.ExMarshaller.Token);
				}

				// Token: 0x060011D3 RID: 4563 RVA: 0x00062BCC File Offset: 0x00060DCC
				public override void SendSentinelBlock(long blockSequence)
				{
					BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock compressedBlock = new BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock(blockSequence);
					this._toDecompress.Add(compressedBlock, base.ExMarshaller.Token);
					this._toDecompress.CompleteAdding();
				}

				// Token: 0x060011D4 RID: 4564 RVA: 0x00062C04 File Offset: 0x00060E04
				private static void EnsureCapacity(MemoryStream stream, int value)
				{
					int capacity = stream.Capacity;
					if (capacity >= value)
					{
						return;
					}
					int num = value;
					if (num < 256)
					{
						num = 256;
					}
					if (num < capacity * 2)
					{
						num = capacity * 2;
					}
					if (capacity * 2 > 2147483591)
					{
						num = ((value > 2147483591) ? value : 2147483591);
					}
					stream.Capacity = num;
				}

				// Token: 0x060011D5 RID: 4565 RVA: 0x00062C5C File Offset: 0x00060E5C
				public override bool DecompressOne()
				{
					BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock compressedBlock;
					lock (this._toDecompressEnumerator)
					{
						if (!this._toDecompressEnumerator.MoveNext())
						{
							return false;
						}
						compressedBlock = this._toDecompressEnumerator.Current;
					}
					if (compressedBlock.IsSentinel)
					{
						this._waiter.Wait(compressedBlock.BlockSequence, base.ExMarshaller.Token);
						this._toRead.CompleteAdding();
						this._waiter.Increment();
						return true;
					}
					MemoryStream buffer = compressedBlock.Buffer;
					buffer.Position = 0L;
					if (this._parent._disposed)
					{
						this._compPool.Return(ref buffer);
						this._waiter.Wait(compressedBlock.BlockSequence, base.ExMarshaller.Token);
						this._waiter.Increment();
						return true;
					}
					MemoryStream memoryStream = this._decompPool.Get();
					BinaryLoader.Cursor.ReadPipe<T>.EnsureCapacity(memoryStream, compressedBlock.DecompressedLength);
					memoryStream.SetLength((long)compressedBlock.DecompressedLength);
					using (Stream stream = this._compression.DecompressStream(buffer))
					{
						ArraySegment<byte> arraySegment;
						Utils.TryGetBuffer(memoryStream, ref arraySegment);
						Utils.ReadBlock(stream, arraySegment.Array, arraySegment.Offset, arraySegment.Count);
					}
					this._compPool.Return(ref buffer);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					IValueReader<T> valueReader = this._codec.OpenReader(memoryStream, compressedBlock.Rows);
					this._waiter.Wait(compressedBlock.BlockSequence, base.ExMarshaller.Token);
					this._toRead.Add(new BinaryLoader.Cursor.ReadPipe<T>.ReaderContainer(valueReader, memoryStream, compressedBlock.Rows, compressedBlock.BlockSequence), base.ExMarshaller.Token);
					this._waiter.Increment();
					return true;
				}

				// Token: 0x060011D6 RID: 4566 RVA: 0x00062E3C File Offset: 0x0006103C
				public override bool MoveNext()
				{
					if (this._remaining == 0)
					{
						if (this._curr != null)
						{
							this._curr.Reader.Dispose();
							MemoryStream stream = this._curr.Stream;
							this._curr = null;
							this._decompPool.Return(ref stream);
						}
						if (!this._toReadEnumerator.MoveNext())
						{
							return false;
						}
						this._curr = this._toReadEnumerator.Current;
						this._remaining = this._curr.Rows;
					}
					this._curr.Reader.MoveNext();
					this._remaining--;
					return true;
				}

				// Token: 0x060011D7 RID: 4567 RVA: 0x00062EDC File Offset: 0x000610DC
				public override bool MoveNextCleanup()
				{
					if (this._curr != null)
					{
						this._curr.Reader.Dispose();
						MemoryStream stream = this._curr.Stream;
						this._curr = null;
						this._decompPool.Return(ref stream);
					}
					if (!this._toReadEnumerator.MoveNext())
					{
						return false;
					}
					this._curr = this._toReadEnumerator.Current;
					return true;
				}

				// Token: 0x060011D8 RID: 4568 RVA: 0x00062F42 File Offset: 0x00061142
				private void Get(ref T value)
				{
					Contracts.Check(this._curr != null, "cursor is either not started or is ended, and cannot get values");
					this._curr.Reader.Get(ref value);
				}

				// Token: 0x060011D9 RID: 4569 RVA: 0x00062F6C File Offset: 0x0006116C
				public override Delegate GetGetter()
				{
					return new ValueGetter<T>(this.Get);
				}

				// Token: 0x04000A41 RID: 2625
				private const int _bufferSize = 4;

				// Token: 0x04000A42 RID: 2626
				private readonly BlockLookup[] _lookup;

				// Token: 0x04000A43 RID: 2627
				private readonly Stream _stream;

				// Token: 0x04000A44 RID: 2628
				private readonly MemoryStreamPool _compPool;

				// Token: 0x04000A45 RID: 2629
				private readonly MemoryStreamPool _decompPool;

				// Token: 0x04000A46 RID: 2630
				private readonly BlockingCollection<BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock> _toDecompress;

				// Token: 0x04000A47 RID: 2631
				private readonly IEnumerator<BinaryLoader.Cursor.ReadPipe<T>.CompressedBlock> _toDecompressEnumerator;

				// Token: 0x04000A48 RID: 2632
				private readonly BlockingCollection<BinaryLoader.Cursor.ReadPipe<T>.ReaderContainer> _toRead;

				// Token: 0x04000A49 RID: 2633
				private readonly IEnumerator<BinaryLoader.Cursor.ReadPipe<T>.ReaderContainer> _toReadEnumerator;

				// Token: 0x04000A4A RID: 2634
				private readonly IValueCodec<T> _codec;

				// Token: 0x04000A4B RID: 2635
				private readonly CompressionKind _compression;

				// Token: 0x04000A4C RID: 2636
				private readonly OrderedWaiter _waiter;

				// Token: 0x04000A4D RID: 2637
				private BinaryLoader.Cursor.ReadPipe<T>.ReaderContainer _curr;

				// Token: 0x04000A4E RID: 2638
				private int _remaining;

				// Token: 0x0200031D RID: 797
				private sealed class CompressedBlock
				{
					// Token: 0x170001C6 RID: 454
					// (get) Token: 0x060011DA RID: 4570 RVA: 0x00062F87 File Offset: 0x00061187
					public bool IsSentinel
					{
						get
						{
							return this.BlockIndex == -1L;
						}
					}

					// Token: 0x060011DB RID: 4571 RVA: 0x00062F93 File Offset: 0x00061193
					public CompressedBlock(MemoryStream buffer, int decompressedLength, long blockIndex, long blockSequence, int rows)
					{
						this.Buffer = buffer;
						this.DecompressedLength = decompressedLength;
						this.BlockIndex = blockIndex;
						this.BlockSequence = blockSequence;
						this.Rows = rows;
					}

					// Token: 0x060011DC RID: 4572 RVA: 0x00062FC0 File Offset: 0x000611C0
					public CompressedBlock(long blockSequence)
					{
						this.BlockIndex = -1L;
						this.BlockSequence = blockSequence;
					}

					// Token: 0x04000A4F RID: 2639
					public readonly MemoryStream Buffer;

					// Token: 0x04000A50 RID: 2640
					public readonly int DecompressedLength;

					// Token: 0x04000A51 RID: 2641
					public readonly long BlockIndex;

					// Token: 0x04000A52 RID: 2642
					public readonly long BlockSequence;

					// Token: 0x04000A53 RID: 2643
					public readonly int Rows;
				}

				// Token: 0x0200031E RID: 798
				private sealed class ReaderContainer
				{
					// Token: 0x060011DD RID: 4573 RVA: 0x00062FD7 File Offset: 0x000611D7
					public ReaderContainer(IValueReader<T> reader, MemoryStream stream, int rows, long blockSequence)
					{
						this.Reader = reader;
						this.Stream = stream;
						this.Rows = rows;
						this.BlockSequence = blockSequence;
					}

					// Token: 0x04000A54 RID: 2644
					public readonly IValueReader<T> Reader;

					// Token: 0x04000A55 RID: 2645
					public readonly MemoryStream Stream;

					// Token: 0x04000A56 RID: 2646
					public readonly int Rows;

					// Token: 0x04000A57 RID: 2647
					public readonly long BlockSequence;
				}
			}
		}

		// Token: 0x0200031F RID: 799
		public sealed class InfoCommand : ICommand
		{
			// Token: 0x060011DE RID: 4574 RVA: 0x00062FFC File Offset: 0x000611FC
			public InfoCommand(BinaryLoader.InfoCommand.Arguments args, IHostEnvironment env)
			{
				Contracts.CheckValue<IHostEnvironment>(env, "env");
				Contracts.CheckValue<BinaryLoader.InfoCommand.Arguments>(env, args, "args");
				Contracts.CheckNonWhiteSpace(env, args.dataFile, "dataFile", "Data file must be specified");
				this._dataFile = args.dataFile;
				this._env = env.Fork(null, args.verbose, null);
			}

			// Token: 0x060011DF RID: 4575 RVA: 0x00063070 File Offset: 0x00061270
			private string VersionToString(ulong ver)
			{
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					ver >> 48,
					(ver >> 32) & 65535UL,
					(ver >> 16) & 65535UL,
					ver & 65535UL
				});
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x000630D4 File Offset: 0x000612D4
			public void Run()
			{
				IHost host = this._env.Register("IdvInfo");
				MultiFileSource multiFileSource = new MultiFileSource(this._dataFile);
				BinaryLoader.Arguments arguments = new BinaryLoader.Arguments();
				using (BinaryLoader binaryLoader = new BinaryLoader(arguments, host, multiFileSource))
				{
					using (IChannel channel = host.Start("Inspection"))
					{
						this.RunCore(channel, binaryLoader);
						channel.Done();
					}
				}
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x00063208 File Offset: 0x00061408
			private void RunCore(IChannel ch, BinaryLoader loader)
			{
				Header header = loader._header;
				long idvSize = header.TailOffset + 8L;
				if (loader._stream.Length != idvSize)
				{
					ch.Warning("Stream is {0} bytes, IDV is {1} bytes. This is legal but unusual.", new object[]
					{
						loader._stream.Length,
						idvSize
					});
				}
				ch.Info("IDV {0} (compat {1}), {2} col, {3} row, {4} bytes", new object[]
				{
					this.VersionToString(header.Version),
					this.VersionToString(header.CompatibleVersion),
					header.ColumnCount,
					header.RowCount,
					idvSize
				});
				IOrderedEnumerable<KeyValuePair<bool, BinaryLoader.TableOfContentsEntry>> orderedEnumerable = from t in loader._aliveColumns.Select((BinaryLoader.TableOfContentsEntry t) => new KeyValuePair<bool, BinaryLoader.TableOfContentsEntry>(true, t)).Concat(loader._deadColumns.Select((BinaryLoader.TableOfContentsEntry t) => new KeyValuePair<bool, BinaryLoader.TableOfContentsEntry>(false, t)))
					where !t.Value.IsGenerated
					orderby t.Value.ColumnIndex
					select t;
				long num = 0L;
				long num2 = 0L;
				long num3 = 0L;
				long num4 = 0L;
				int num5 = 0;
				foreach (KeyValuePair<bool, BinaryLoader.TableOfContentsEntry> keyValuePair in orderedEnumerable)
				{
					BinaryLoader.TableOfContentsEntry value = keyValuePair.Value;
					string text = ((value.Type == null) ? "<?>" : value.Type.ToString());
					long num6 = 0L;
					long num7 = 0L;
					BlockLookup[] lookup = value.GetLookup();
					foreach (BlockLookup blockLookup in lookup)
					{
						num7 += (long)blockLookup.BlockLength;
						num6 += (long)blockLookup.DecompressedBlockLength;
						num4 += 1L;
					}
					num += num7;
					ch.Info("Column {0} '{1}'{2} of {3} in {4} blocks of {5}", new object[]
					{
						value.ColumnIndex,
						value.Name,
						keyValuePair.Key ? "" : " (DEAD!)",
						text,
						lookup.Length,
						value.RowsPerBlock
					});
					ch.Info("  {0} compressed from {1} with {2} ({3:0.00%})", new object[]
					{
						num7,
						num6,
						value.Compression,
						((double)num7 + 0.0) / (double)num6
					});
					BinaryLoader.MetadataTableOfContentsEntry[] metadataTOCArray = value.GetMetadataTOCArray();
					BinaryLoader.MetadataTableOfContentsEntry[] deadMetadataTOCArray = value.GetDeadMetadataTOCArray();
					if (metadataTOCArray != null)
					{
						long num8 = metadataTOCArray.Sum((BinaryLoader.MetadataTableOfContentsEntry t) => t.BlockSize) + deadMetadataTOCArray.Sum((BinaryLoader.MetadataTableOfContentsEntry t) => t.BlockSize);
						long num9 = value.GetMetadataTocEndOffset() - value.MetadataTocOffset;
						string text2 = ((deadMetadataTOCArray.Length > 0) ? string.Format(" ({0} dead)", deadMetadataTOCArray.Length) : "");
						ch.Info("  {0} pieces of metadata{1} has {2} byte table, {3} byte content", new object[] { metadataTOCArray.Length, text2, num9, num8 });
						num2 += num8;
						num3 += num9;
					}
					num5++;
				}
				long num10 = num4 * 16L;
				long num11 = loader._tocEndLim - header.TableOfContentsOffset;
				long accountedSize = 0L;
				ch.Info(" ");
				Action<string, long> action = delegate(string desc, long size)
				{
					ch.Info("{0,8:0.000%} for {1} ({2} bytes)", new object[]
					{
						((double)size + 0.0) / (double)idvSize,
						desc,
						size
					});
					accountedSize += size;
				};
				action("data blocks", num);
				action("block lookup", num10);
				action("table of contents", num11);
				action("metadata contents", num2);
				action("metadata table of contents", num3);
				action("header/tail", 264L);
				if (idvSize != accountedSize)
				{
					action("unknown", idvSize - accountedSize);
				}
			}

			// Token: 0x04000A58 RID: 2648
			public const string LoadName = "IdvInfo";

			// Token: 0x04000A59 RID: 2649
			private readonly IHostEnvironment _env;

			// Token: 0x04000A5A RID: 2650
			private readonly string _dataFile;

			// Token: 0x02000320 RID: 800
			public sealed class Arguments
			{
				// Token: 0x04000A61 RID: 2657
				[DefaultArgument(0, IsInputFileName = true, HelpText = "The data file", SortOrder = 0)]
				public string dataFile;

				// Token: 0x04000A62 RID: 2658
				[Argument(0, HelpText = "Verbose?", ShortName = "v", Hide = true)]
				public bool? verbose;
			}
		}
	}
}
