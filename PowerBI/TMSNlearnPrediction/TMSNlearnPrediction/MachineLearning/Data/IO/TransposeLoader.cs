using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020000CF RID: 207
	public sealed class TransposeLoader : IDataLoader, ICanSaveModel, ITransposeDataView, IDataView, ISchematized
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x000181B4 File Offset: 0x000163B4
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("XPSLOADR", 65537U, 65537U, 65537U, "TransposeLoader", null);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000181D5 File Offset: 0x000163D5
		public ISchema Schema
		{
			get
			{
				return this._schemaEntry.GetView().Schema;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000181E7 File Offset: 0x000163E7
		public ITransposeSchema TransposeSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000181F0 File Offset: 0x000163F0
		private bool HasRowData
		{
			get
			{
				return this._header.RowCount == this._schemaEntry.GetView().GetRowCount(true);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00018230 File Offset: 0x00016430
		public bool CanShuffle
		{
			get
			{
				IDataView view = this._schemaEntry.GetView();
				return this._header.RowCount == view.GetRowCount(true) && view.CanShuffle;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001827C File Offset: 0x0001647C
		public TransposeLoader(TransposeLoader.Arguments args, IHostEnvironment env, IMultiStreamSource file)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("TransposeLoader");
			Contracts.CheckValue<TransposeLoader.Arguments>(this._host, args, "args");
			Contracts.CheckValue<IMultiStreamSource>(this._host, file, "file");
			Contracts.Check(this._host, file.Count == 1, "Transposed loader accepts a single file only");
			this._threads = args.threads ?? 0;
			if (this._threads < 0)
			{
				this._threads = 0;
			}
			this._file = file;
			using (Stream stream = this._file.Open(0))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					this._header = this.InitHeader(binaryReader);
					Utils.Seek(binaryReader, this._header.SubIdvTableOffset);
					this._schemaEntry = new TransposeLoader.SubIdvEntry.SchemaSubIdv(this, binaryReader);
					this._entries = new TransposeLoader.SubIdvEntry.TransposedSubIdv[this._header.ColumnCount];
					for (int i = 0; i < this._entries.Length; i++)
					{
						this._entries[i] = new TransposeLoader.SubIdvEntry.TransposedSubIdv(this, binaryReader, i);
					}
					this._schema = new TransposeLoader.SchemaImpl(this);
					if (!this.HasRowData)
					{
						this._colTransposers = new Transposer[this._header.ColumnCount];
						this._colTransposersLock = new object();
					}
				}
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00018400 File Offset: 0x00016600
		private TransposeLoader(ModelLoadContext ctx, IHost host, IMultiStreamSource file)
		{
			Contracts.CheckValue<IHost>(host, "host");
			this._host = host;
			Contracts.CheckValue<IMultiStreamSource>(this._host, file, "file");
			Contracts.Check(this._host, file.Count == 1, "Transposed loader accepts a single file only");
			this._threads = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._threads >= 0);
			this._file = file;
			using (Stream stream = this._file.Open(0))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					this._header = this.InitHeader(binaryReader);
					Utils.Seek(binaryReader, this._header.SubIdvTableOffset);
					this._schemaEntry = new TransposeLoader.SubIdvEntry.SchemaSubIdv(this, binaryReader);
					this._entries = new TransposeLoader.SubIdvEntry.TransposedSubIdv[this._header.ColumnCount];
					for (int i = 0; i < this._entries.Length; i++)
					{
						this._entries[i] = new TransposeLoader.SubIdvEntry.TransposedSubIdv(this, binaryReader, i);
					}
					this._schema = new TransposeLoader.SchemaImpl(this);
					if (!this.HasRowData)
					{
						this._colTransposers = new Transposer[this._header.ColumnCount];
						this._colTransposersLock = new object();
					}
				}
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00018560 File Offset: 0x00016760
		private TransposeLoader(ModelLoadContext ctx, IHost host, IDataView schemaView)
		{
			Contracts.CheckValue<IHost>(host, "host");
			this._host = host;
			Contracts.CheckValue<IDataView>(this._host, schemaView, "schemaView");
			this._threads = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._threads >= 0);
			this._header = new TransposeLoader.Header
			{
				ColumnCount = schemaView.Schema.ColumnCount
			};
			this._schemaEntry = new TransposeLoader.SubIdvEntry.SchemaSubIdv(this, schemaView);
			this._entries = new TransposeLoader.SubIdvEntry.TransposedSubIdv[this._header.ColumnCount];
			for (int i = 0; i < this._entries.Length; i++)
			{
				this._entries[i] = new TransposeLoader.SubIdvEntry.TransposedSubIdv(this, i);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00018714 File Offset: 0x00016914
		public static TransposeLoader Create(ModelLoadContext ctx, IHostEnvironment env, IMultiStreamSource files)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("TransposeLoader");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(TransposeLoader.GetVersionInfo());
			Contracts.CheckValue<IMultiStreamSource>(h, files, "files");
			return HostExtensions.Apply<TransposeLoader>(h, "Loading Model", delegate(IChannel ch)
			{
				if (files.Count == 0)
				{
					BinaryLoader schemaView = null;
					if (ctx.TryLoadBinaryStream("Schema.idv", delegate(BinaryReader r)
					{
						schemaView = new BinaryLoader(new BinaryLoader.Arguments(), h, HybridMemoryStream.CreateCache(r.BaseStream, 1073741824), false);
					}))
					{
						Contracts.CheckDecode(h, schemaView.GetRowCount(true) == 0L, "Internal schema IDV expected to have no rows, but this was not the case");
						return new TransposeLoader(ctx, h, schemaView);
					}
				}
				return new TransposeLoader(ctx, h, files);
			});
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000187AC File Offset: 0x000169AC
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(TransposeLoader.GetVersionInfo());
			ctx.Writer.Write(this._threads);
			TransposeLoader.SaveSchema(this._host, ctx, this.Schema);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00018830 File Offset: 0x00016A30
		private static void SaveSchema(IHostEnvironment env, ModelSaveContext ctx, ISchema schema)
		{
			EmptyDataView noRows = new EmptyDataView(env, schema);
			BinarySaver saver = new BinarySaver(new BinarySaver.Arguments
			{
				silent = true
			}, env);
			ctx.SaveBinaryStream("Schema.idv", delegate(BinaryWriter w)
			{
				saver.SaveData(w.BaseStream, noRows, Utils.GetIdentityPermutation(schema.ColumnCount));
			});
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00018890 File Offset: 0x00016A90
		private unsafe TransposeLoader.Header InitHeader(BinaryReader reader)
		{
			byte[] array = new byte[256];
			int num = reader.Read(array, 0, 256);
			if (num != 256)
			{
				throw Contracts.ExceptDecode(this._host, "Read only {0} bytes in file, expected header size of {1}", new object[] { num, 256 });
			}
			TransposeLoader.Header header;
			Marshal.Copy(array, 0, (IntPtr)((void*)(&header)), 256);
			Contracts.CheckDecode(this._host, header.Signature == 6216168450219266136UL, "This does not appear to be a transposed dataview file");
			if (header.CompatibleVersion > header.Version)
			{
				throw Contracts.ExceptDecode(this._host, "Compatibility version {0} cannot be greater than file version {1}", new object[]
				{
					TransposeLoader.Header.VersionToString(header.CompatibleVersion),
					TransposeLoader.Header.VersionToString(header.Version)
				});
			}
			if (header.Version < 281479271743489UL)
			{
				throw Contracts.ExceptDecode(this._host, "Unexpected version {0} encountered, earliest expected here was {1}", new object[]
				{
					TransposeLoader.Header.VersionToString(header.Version),
					TransposeLoader.Header.VersionToString(281479271743489UL)
				});
			}
			if (header.CompatibleVersion > 281479271743489UL)
			{
				throw Contracts.Except(this._host, "Cannot read version {0} data, latest that can be handled is {1}", new object[]
				{
					TransposeLoader.Header.VersionToString(header.CompatibleVersion),
					TransposeLoader.Header.VersionToString(281479271743489UL)
				});
			}
			Contracts.CheckDecode(this._host, header.RowCount >= 0L, "Row count cannot be negative");
			Contracts.CheckDecode(this._host, header.ColumnCount >= 0, "Column count cannot be negative");
			if (header.ColumnCount != 0 && header.SubIdvTableOffset < 256L)
			{
				throw Contracts.ExceptDecode(this._host, "Table of contents offset {0} less than header size, impossible", new object[] { header.SubIdvTableOffset });
			}
			if (header.TailOffset < 256L)
			{
				throw Contracts.ExceptDecode(this._host, "Tail offset {0} less than header size, impossible", new object[] { header.TailOffset });
			}
			Utils.Seek(reader, header.TailOffset);
			ulong num2 = reader.ReadUInt64();
			Contracts.CheckDecode(this._host, num2 == 6363673492537492566UL, "Incorrect tail signature");
			return header;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00018AF9 File Offset: 0x00016CF9
		public long? GetRowCount(bool lazy = true)
		{
			return new long?(this._header.RowCount);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00018B0B File Offset: 0x00016D0B
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			if (this.HasRowData)
			{
				return this._schemaEntry.GetView().GetRowCursor(predicate, rand);
			}
			return new TransposeLoader.Cursor(this, predicate);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00018B40 File Offset: 0x00016D40
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			if (this.HasRowData)
			{
				return this._schemaEntry.GetView().GetRowCursorSet(ref consolidator, predicate, n, rand);
			}
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursor(predicate, rand) };
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00018B98 File Offset: 0x00016D98
		public ISlotCursor GetSlotCursor(int col)
		{
			Contracts.CheckParam(this._host, 0 <= col && col < this._header.ColumnCount, "col");
			IDataView viewOrNull = this._entries[col].GetViewOrNull();
			if (viewOrNull == null)
			{
				throw Contracts.ExceptParam(this._host, "col", "Bad call to GetSlotCursor on untransposable column '{0}'", new object[] { this.Schema.GetColumnName(col) });
			}
			Contracts.CheckParam(this._host, 0 <= col && col < this._header.ColumnCount, "col");
			ColumnType itemType = this.TransposeSchema.GetSlotType(col).ItemType;
			IRowCursor rowCursor = viewOrNull.GetRowCursor((int c) => true, null);
			ISlotCursor slotCursor;
			try
			{
				slotCursor = Utils.MarshalInvoke<IRowCursor, ISlotCursor>(new Func<IRowCursor, ISlotCursor>(this.GetSlotCursorCore<int>), itemType.RawType, rowCursor);
			}
			catch (Exception)
			{
				if (rowCursor != null)
				{
					rowCursor.Dispose();
				}
				throw;
			}
			return slotCursor;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00018CA0 File Offset: 0x00016EA0
		private ISlotCursor GetSlotCursorCore<T>(IRowCursor inputCursor)
		{
			return new TransposeLoader.SlotCursor<T>(this, inputCursor);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00018CAC File Offset: 0x00016EAC
		private Transposer EnsureAndGetTransposer(int col)
		{
			if (this._colTransposers[col] == null)
			{
				lock (this._colTransposersLock)
				{
					if (this._colTransposers[col] == null)
					{
						IDataView viewOrNull = this._entries[col].GetViewOrNull();
						Transposer[] colTransposers = this._colTransposers;
						IHostEnvironment host = this._host;
						IDataView dataView = viewOrNull;
						bool flag2 = false;
						int[] array = new int[1];
						colTransposers[col] = Transposer.Create(host, dataView, flag2, array);
					}
				}
			}
			return this._colTransposers[col];
		}

		// Token: 0x040001CC RID: 460
		private const ulong ReaderFirstVersion = 281479271743489UL;

		// Token: 0x040001CD RID: 461
		private const ulong ReaderVersion = 281479271743489UL;

		// Token: 0x040001CE RID: 462
		internal const string Summary = "Loads a binary transposed data file.";

		// Token: 0x040001CF RID: 463
		internal const string LoadName = "TransposeLoader";

		// Token: 0x040001D0 RID: 464
		private readonly int _threads;

		// Token: 0x040001D1 RID: 465
		private readonly IMultiStreamSource _file;

		// Token: 0x040001D2 RID: 466
		private readonly IHost _host;

		// Token: 0x040001D3 RID: 467
		private readonly TransposeLoader.Header _header;

		// Token: 0x040001D4 RID: 468
		private readonly TransposeLoader.SchemaImpl _schema;

		// Token: 0x040001D5 RID: 469
		private readonly TransposeLoader.SubIdvEntry.SchemaSubIdv _schemaEntry;

		// Token: 0x040001D6 RID: 470
		private readonly TransposeLoader.SubIdvEntry.TransposedSubIdv[] _entries;

		// Token: 0x040001D7 RID: 471
		private readonly Transposer[] _colTransposers;

		// Token: 0x040001D8 RID: 472
		private readonly object _colTransposersLock;

		// Token: 0x020000D0 RID: 208
		public sealed class Arguments
		{
			// Token: 0x040001DA RID: 474
			[Argument(4, HelpText = "The number of worker decompressor threads to use", ShortName = "t")]
			public int? threads;
		}

		// Token: 0x020000D1 RID: 209
		[StructLayout(LayoutKind.Explicit, Size = 256)]
		public struct Header
		{
			// Token: 0x06000467 RID: 1127 RVA: 0x00018D38 File Offset: 0x00016F38
			internal static string VersionToString(ulong v)
			{
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					(v >> 48) & 65535UL,
					(v >> 32) & 65535UL,
					(v >> 16) & 65535UL,
					v & 65535UL
				});
			}

			// Token: 0x040001DB RID: 475
			public const int HeaderSize = 256;

			// Token: 0x040001DC RID: 476
			public const ulong SignatureValue = 6216168450219266136UL;

			// Token: 0x040001DD RID: 477
			public const ulong TailSignatureValue = 6363673492537492566UL;

			// Token: 0x040001DE RID: 478
			public const ulong WriterVersion = 281479271743489UL;

			// Token: 0x040001DF RID: 479
			public const ulong CanBeReadByVersion = 281479271743489UL;

			// Token: 0x040001E0 RID: 480
			[FieldOffset(0)]
			public ulong Signature;

			// Token: 0x040001E1 RID: 481
			[FieldOffset(8)]
			public ulong Version;

			// Token: 0x040001E2 RID: 482
			[FieldOffset(16)]
			public ulong CompatibleVersion;

			// Token: 0x040001E3 RID: 483
			[FieldOffset(24)]
			public long SubIdvTableOffset;

			// Token: 0x040001E4 RID: 484
			[FieldOffset(32)]
			public long TailOffset;

			// Token: 0x040001E5 RID: 485
			[FieldOffset(40)]
			public long RowCount;

			// Token: 0x040001E6 RID: 486
			[FieldOffset(48)]
			public int ColumnCount;
		}

		// Token: 0x020000D2 RID: 210
		private abstract class SubIdvEntry
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000468 RID: 1128 RVA: 0x00018DA0 File Offset: 0x00016FA0
			public bool HasDataView
			{
				get
				{
					return this._view != null || this._offset > 0L;
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000469 RID: 1129 RVA: 0x00018DB6 File Offset: 0x00016FB6
			private IHost Host
			{
				get
				{
					return this._parent._host;
				}
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x00018DC4 File Offset: 0x00016FC4
			private SubIdvEntry(TransposeLoader parent, BinaryReader reader)
			{
				this._parent = parent;
				this._offset = reader.ReadInt64();
				Contracts.CheckDecode(this.Host, this._offset == 0L || (256L <= this._offset && this._offset <= this._parent._header.TailOffset));
				this._length = reader.ReadInt64();
				Contracts.CheckDecode(this.Host, 0L <= this._length && this._offset <= this._parent._header.TailOffset - this._length);
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x00018E74 File Offset: 0x00017074
			private SubIdvEntry(TransposeLoader parent)
			{
				this._parent = parent;
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x00018E84 File Offset: 0x00017084
			public IDataView GetViewOrNull()
			{
				if (this._view == null && this._offset > 0L)
				{
					Stream stream = this._parent._file.Open(0);
					stream.Seek(this._offset, SeekOrigin.Begin);
					Contracts.Check(stream.Position == this._offset, "Unexpected position on substream");
					SubsetStream subsetStream = new SubsetStream(stream, new long?(this._length));
					BinaryLoader.Arguments arguments = new BinaryLoader.Arguments();
					if (this._parent._threads > 0)
					{
						arguments.threads = new int?(this._parent._threads);
					}
					BinaryLoader binaryLoader = new BinaryLoader(arguments, this.Host, subsetStream, false);
					IDataView dataView = Interlocked.CompareExchange<IDataView>(ref this._view, binaryLoader, null);
					if (dataView == binaryLoader)
					{
						this.VerifyView(dataView);
					}
				}
				return this._view;
			}

			// Token: 0x0600046D RID: 1133
			protected abstract void VerifyView(IDataView view);

			// Token: 0x040001E7 RID: 487
			private readonly TransposeLoader _parent;

			// Token: 0x040001E8 RID: 488
			private readonly long _offset;

			// Token: 0x040001E9 RID: 489
			private readonly long _length;

			// Token: 0x040001EA RID: 490
			private IDataView _view;

			// Token: 0x020000D3 RID: 211
			public sealed class SchemaSubIdv : TransposeLoader.SubIdvEntry
			{
				// Token: 0x0600046E RID: 1134 RVA: 0x00018F50 File Offset: 0x00017150
				public IDataView GetView()
				{
					return base.GetViewOrNull();
				}

				// Token: 0x0600046F RID: 1135 RVA: 0x00018F65 File Offset: 0x00017165
				public SchemaSubIdv(TransposeLoader parent, BinaryReader reader)
					: base(parent, reader)
				{
					Contracts.CheckDecode(base.Host, base.HasDataView);
				}

				// Token: 0x06000470 RID: 1136 RVA: 0x00018F80 File Offset: 0x00017180
				public SchemaSubIdv(TransposeLoader parent, IDataView view)
					: base(parent)
				{
					this._view = view;
				}

				// Token: 0x06000471 RID: 1137 RVA: 0x00018F90 File Offset: 0x00017190
				protected override void VerifyView(IDataView view)
				{
					long value = view.GetRowCount(true).Value;
					Contracts.CheckDecode(base.Host, value == 0L || this._parent._header.RowCount == value);
					ISchema schema = view.Schema;
					Contracts.CheckDecode(base.Host, schema.ColumnCount == this._parent._header.ColumnCount);
				}
			}

			// Token: 0x020000D4 RID: 212
			public sealed class TransposedSubIdv : TransposeLoader.SubIdvEntry
			{
				// Token: 0x06000472 RID: 1138 RVA: 0x00018FFD File Offset: 0x000171FD
				public TransposedSubIdv(TransposeLoader parent, BinaryReader reader, int col)
					: base(parent, reader)
				{
					this._col = col;
					Contracts.CheckDecode(base.Host, base.HasDataView || parent.HasRowData);
				}

				// Token: 0x06000473 RID: 1139 RVA: 0x0001902A File Offset: 0x0001722A
				public TransposedSubIdv(TransposeLoader parent, int col)
					: base(parent)
				{
					this._col = col;
				}

				// Token: 0x06000474 RID: 1140 RVA: 0x0001903C File Offset: 0x0001723C
				protected override void VerifyView(IDataView view)
				{
					ISchema schema = view.Schema;
					Contracts.CheckDecode(base.Host, schema.ColumnCount == 1);
					ColumnType columnType = schema.GetColumnType(0);
					Contracts.CheckDecode(base.Host, columnType.IsVector);
					Contracts.CheckDecode(base.Host, (long)columnType.ValueCount == this._parent._header.RowCount);
					long value = view.GetRowCount(true).Value;
					ColumnType columnType2 = this._parent.Schema.GetColumnType(this._col);
					Contracts.CheckDecode(base.Host, (long)columnType2.ValueCount == value);
					Contracts.CheckDecode(base.Host, columnType2.ItemType.Equals(columnType.ItemType));
				}

				// Token: 0x040001EB RID: 491
				private readonly int _col;
			}
		}

		// Token: 0x020000D5 RID: 213
		private sealed class SchemaImpl : ITransposeSchema, ISchema
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000475 RID: 1141 RVA: 0x000190FC File Offset: 0x000172FC
			private ISchema Schema
			{
				get
				{
					return this._parent.Schema;
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000476 RID: 1142 RVA: 0x00019109 File Offset: 0x00017309
			private IHost Host
			{
				get
				{
					return this._parent._host;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000477 RID: 1143 RVA: 0x00019116 File Offset: 0x00017316
			public int ColumnCount
			{
				get
				{
					return this.Schema.ColumnCount;
				}
			}

			// Token: 0x06000478 RID: 1144 RVA: 0x00019123 File Offset: 0x00017323
			public SchemaImpl(TransposeLoader parent)
			{
				this._parent = parent;
				ISchema schema = parent._schemaEntry.GetView().Schema;
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x00019143 File Offset: 0x00017343
			public string GetColumnName(int col)
			{
				return this.Schema.GetColumnName(col);
			}

			// Token: 0x0600047A RID: 1146 RVA: 0x00019151 File Offset: 0x00017351
			public bool TryGetColumnIndex(string name, out int col)
			{
				return this.Schema.TryGetColumnIndex(name, ref col);
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x00019160 File Offset: 0x00017360
			public ColumnType GetColumnType(int col)
			{
				return this.Schema.GetColumnType(col);
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x0001916E File Offset: 0x0001736E
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				return this.Schema.GetMetadataTypeOrNull(kind, col);
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x0001917D File Offset: 0x0001737D
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				return this.Schema.GetMetadataTypes(col);
			}

			// Token: 0x0600047E RID: 1150 RVA: 0x0001918B File Offset: 0x0001738B
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				this.Schema.GetMetadata<TValue>(kind, col, ref value);
			}

			// Token: 0x0600047F RID: 1151 RVA: 0x0001919C File Offset: 0x0001739C
			public VectorType GetSlotType(int col)
			{
				Contracts.CheckParam(this.Host, 0 <= col && col < this.ColumnCount, "col");
				IDataView viewOrNull = this._parent._entries[col].GetViewOrNull();
				if (viewOrNull == null)
				{
					return null;
				}
				return viewOrNull.Schema.GetColumnType(0).AsVector;
			}

			// Token: 0x040001EC RID: 492
			private readonly TransposeLoader _parent;
		}

		// Token: 0x020000D7 RID: 215
		private sealed class SlotCursor<T> : SynchronizedCursorBase<IRowCursor>, ISlotCursor, ICursor, ICounted, IDisposable
		{
			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000482 RID: 1154 RVA: 0x000191F2 File Offset: 0x000173F2
			private IHost Host
			{
				get
				{
					return this._parent._host;
				}
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x000191FF File Offset: 0x000173FF
			public SlotCursor(TransposeLoader parent, IRowCursor cursor)
				: base(parent._host, cursor)
			{
				this._parent = parent;
				this._getter = base.Input.GetGetter<VBuffer<T>>(0);
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x00019228 File Offset: 0x00017428
			public VectorType GetSlotType()
			{
				return base.Input.Schema.GetColumnType(0).AsVector;
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x00019250 File Offset: 0x00017450
			public ValueGetter<VBuffer<TValue>> GetGetter<TValue>()
			{
				ValueGetter<VBuffer<TValue>> valueGetter = this._getter as ValueGetter<VBuffer<TValue>>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x040001ED RID: 493
			private readonly TransposeLoader _parent;

			// Token: 0x040001EE RID: 494
			private readonly ValueGetter<VBuffer<T>> _getter;
		}

		// Token: 0x020000D8 RID: 216
		private sealed class Cursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000486 RID: 1158 RVA: 0x00019293 File Offset: 0x00017493
			public ISchema Schema
			{
				get
				{
					return this._parent.Schema;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000487 RID: 1159 RVA: 0x000192A0 File Offset: 0x000174A0
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x000192A4 File Offset: 0x000174A4
			public Cursor(TransposeLoader parent, Func<int, bool> pred)
				: base(parent._host)
			{
				this._parent = parent;
				Utils.BuildSubsetMaps(this._parent._header.ColumnCount, pred, ref this._actives, ref this._colToActivesIndex);
				this._transCursors = new ICursor[this._actives.Length];
				this._getters = new Delegate[this._actives.Length];
				for (int i = 0; i < this._actives.Length; i++)
				{
					this.Init(this._actives[i]);
				}
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x00019330 File Offset: 0x00017530
			public override void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					for (int i = 0; i < this._transCursors.Length; i++)
					{
						this._transCursors[i].Dispose();
					}
					base.Dispose();
				}
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x00019374 File Offset: 0x00017574
			private void Init(int col)
			{
				ColumnType columnType = this.Schema.GetColumnType(col);
				Action<int> action = new Action<int>(this.InitOne<int>);
				if (columnType.IsVector)
				{
					action = new Action<int>(this.InitVec<int>);
				}
				MethodInfo methodInfo = action.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.ItemType.RawType });
				methodInfo.Invoke(this, new object[] { col });
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x0001941C File Offset: 0x0001761C
			private void InitOne<T>(int col)
			{
				this.Schema.GetColumnType(col);
				Transposer transposer = this._parent.EnsureAndGetTransposer(col);
				ISlotCursor slotCursor = transposer.GetSlotCursor(0);
				ValueGetter<VBuffer<T>> getter = slotCursor.GetGetter<T>();
				VBuffer<T> buff = default(VBuffer<T>);
				ValueGetter<T> valueGetter = delegate(ref T value)
				{
					getter.Invoke(ref buff);
					buff.GetItemOrDefault(0, ref value);
				};
				int num = this._colToActivesIndex[col];
				this._getters[num] = valueGetter;
				this._transCursors[num] = slotCursor;
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x00019498 File Offset: 0x00017698
			private void InitVec<T>(int col)
			{
				this.Schema.GetColumnType(col);
				Transposer transposer = this._parent.EnsureAndGetTransposer(col);
				ISlotCursor slotCursor = transposer.GetSlotCursor(0);
				ValueGetter<VBuffer<T>> getter = slotCursor.GetGetter<T>();
				int num = this._colToActivesIndex[col];
				this._getters[num] = getter;
				this._transCursors[num] = slotCursor;
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x00019514 File Offset: 0x00017714
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
					val = new UInt128((ulong)base.Position, 0UL);
				};
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x00019524 File Offset: 0x00017724
			protected override bool MoveNextCore()
			{
				bool flag = base.Position < this._parent._header.RowCount - 1L;
				for (int i = 0; i < this._transCursors.Length; i++)
				{
					this._transCursors[i].MoveNext();
				}
				return flag;
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x00019570 File Offset: 0x00017770
			protected override bool MoveManyCore(long count)
			{
				bool flag = base.Position < this._parent._header.RowCount - count;
				for (int i = 0; i < this._transCursors.Length; i++)
				{
					this._transCursors[i].MoveMany(count);
				}
				return flag;
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x000195BB File Offset: 0x000177BB
			public bool IsColumnActive(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col <= this._colToActivesIndex.Length, "col");
				return this._colToActivesIndex[col] >= 0;
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x000195F0 File Offset: 0x000177F0
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col <= this._colToActivesIndex.Length, "col");
				Contracts.CheckParam(this._ch, this.IsColumnActive(col), "col", "requested column not active");
				ValueGetter<TValue> valueGetter = this._getters[this._colToActivesIndex[col]] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x040001EF RID: 495
			private readonly TransposeLoader _parent;

			// Token: 0x040001F0 RID: 496
			private readonly int[] _actives;

			// Token: 0x040001F1 RID: 497
			private readonly int[] _colToActivesIndex;

			// Token: 0x040001F2 RID: 498
			private readonly ICursor[] _transCursors;

			// Token: 0x040001F3 RID: 499
			private readonly Delegate[] _getters;

			// Token: 0x040001F4 RID: 500
			private bool _disposed;
		}
	}
}
