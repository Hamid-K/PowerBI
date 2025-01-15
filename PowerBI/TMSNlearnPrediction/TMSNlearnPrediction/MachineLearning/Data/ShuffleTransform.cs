using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000229 RID: 553
	public sealed class ShuffleTransform : RowToRowTransformBase
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x000439FF File Offset: 0x00041BFF
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("SHUFFLET", 65538U, 65538U, 65538U, "ShuffleTrans", null);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00043A20 File Offset: 0x00041C20
		public ShuffleTransform(ShuffleTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "Shuffle", input)
		{
			Contracts.CheckValue<ShuffleTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, args.poolRows > 0, "poolRows", "pool size must be positive");
			this._poolRows = args.poolRows;
			this._poolOnly = args.poolOnly;
			this._forceShuffle = args.forceShuffle;
			this._forceShuffleSource = args.forceShuffleSource ?? (!this._poolOnly && this._forceShuffle);
			Contracts.CheckUserArg(this._host, !this._poolOnly || !this._forceShuffleSource, "forceShuffleSource", "Cannot set both poolOnly and forceShuffleSource");
			if (this._forceShuffle || this._forceShuffleSource)
			{
				this._forceShuffleSeed = args.forceShuffleSeed ?? this._host.Rand.NextSigned();
			}
			this._subsetInput = ShuffleTransform.SelectCachableColumns(input, env);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00043B34 File Offset: 0x00041D34
		private ShuffleTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			this._poolRows = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._poolRows > 0);
			this._poolOnly = Utils.ReadBoolByte(ctx.Reader);
			this._forceShuffle = Utils.ReadBoolByte(ctx.Reader);
			this._forceShuffleSource = Utils.ReadBoolByte(ctx.Reader);
			Contracts.CheckDecode(this._host, !this._poolOnly || !this._forceShuffleSource);
			if (this._forceShuffle || this._forceShuffleSource)
			{
				this._forceShuffleSeed = ctx.Reader.ReadInt32();
			}
			this._subsetInput = ShuffleTransform.SelectCachableColumns(input, host);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00043C10 File Offset: 0x00041E10
		public static ShuffleTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Shuffle");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(ShuffleTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<ShuffleTransform>(h, "Loading Model", (IChannel ch) => new ShuffleTransform(ctx, h, input));
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00043CA8 File Offset: 0x00041EA8
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ShuffleTransform.GetVersionInfo());
			ctx.Writer.Write(this._poolRows);
			Utils.WriteBoolByte(ctx.Writer, this._poolOnly);
			Utils.WriteBoolByte(ctx.Writer, this._forceShuffle);
			Utils.WriteBoolByte(ctx.Writer, this._forceShuffleSource);
			if (this._forceShuffle || this._forceShuffleSource)
			{
				ctx.Writer.Write(this._forceShuffleSeed);
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00043D3C File Offset: 0x00041F3C
		private static IDataView SelectCachableColumns(IDataView data, IHostEnvironment env)
		{
			List<int> list = null;
			ISchema schema = data.Schema;
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				ColumnType columnType = schema.GetColumnType(i);
				if (!columnType.IsCachable())
				{
					Utils.Add<int>(ref list, i);
				}
			}
			if (Utils.Size<int>(list) == 0)
			{
				return data;
			}
			return new ChooseColumnsByIndexTransform(new ChooseColumnsByIndexTransform.Arguments
			{
				drop = true,
				index = list.ToArray()
			}, env, data);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00043DAC File Offset: 0x00041FAC
		internal static bool CanShuffleAll(ISchema schema)
		{
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				ColumnType columnType = schema.GetColumnType(i);
				if (!columnType.IsCachable())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00043DE0 File Offset: 0x00041FE0
		internal static IRowCursor GetShuffledCursor(IChannelProvider provider, int poolRows, IRowCursor cursor, IRandom rand)
		{
			Contracts.CheckValue<IChannelProvider>(provider, "provider");
			Contracts.CheckParam(provider, poolRows > 0, "poolSize", "Pool size cannot be negative");
			Contracts.CheckValue<IRowCursor>(provider, cursor, "cursor");
			Contracts.CheckParam(provider, ShuffleTransform.CanShuffleAll(cursor.Schema), "cursor", "Cannot shuffle a cursor with some uncachable columns");
			Contracts.CheckValue<IRandom>(provider, rand, "rand");
			if (poolRows == 1)
			{
				return cursor;
			}
			return new ShuffleTransform.RowCursor(provider, poolRows, cursor, rand);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00043E4E File Offset: 0x0004204E
		public override bool CanShuffle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00043E51 File Offset: 0x00042051
		public override ISchema Schema
		{
			get
			{
				return this._subsetInput.Schema;
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00043E5E File Offset: 0x0004205E
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return new bool?(false);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00043E68 File Offset: 0x00042068
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			bool flag = this._forceShuffle || rand != null;
			bool flag2 = this._forceShuffleSource || (!this._poolOnly && rand != null);
			IRandom random = rand ?? ((flag || flag2) ? RandomUtils.Create(this._forceShuffleSeed) : null);
			if (flag)
			{
				rand = random;
			}
			IRandom random2 = (flag2 ? RandomUtils.Create(random) : null);
			IRowCursor rowCursor = this._subsetInput.GetRowCursor(predicate, random2);
			if (rand == null || this._poolRows == 1)
			{
				return rowCursor;
			}
			return new ShuffleTransform.RowCursor(this._host, this._poolRows, rowCursor, rand);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00043F08 File Offset: 0x00042108
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursorCore(predicate, rand) };
		}

		// Token: 0x040006C6 RID: 1734
		internal const string Summary = "Reorders rows in the dataset by pseudo-random shuffling.";

		// Token: 0x040006C7 RID: 1735
		public const string LoaderSignature = "ShuffleTrans";

		// Token: 0x040006C8 RID: 1736
		private const string RegistrationName = "Shuffle";

		// Token: 0x040006C9 RID: 1737
		private readonly int _poolRows;

		// Token: 0x040006CA RID: 1738
		private readonly bool _poolOnly;

		// Token: 0x040006CB RID: 1739
		private readonly bool _forceShuffle;

		// Token: 0x040006CC RID: 1740
		private readonly bool _forceShuffleSource;

		// Token: 0x040006CD RID: 1741
		private readonly int _forceShuffleSeed;

		// Token: 0x040006CE RID: 1742
		private readonly IDataView _subsetInput;

		// Token: 0x0200022A RID: 554
		public sealed class Arguments
		{
			// Token: 0x040006CF RID: 1743
			[Argument(4, HelpText = "The pool will have this many rows", ShortName = "rows")]
			public int poolRows = 1000;

			// Token: 0x040006D0 RID: 1744
			[Argument(4, HelpText = "If true, the transform will not attempt to shuffle the input cursor but only shuffle based on the pool. This parameter has no effect if the input data was not itself shufflable.", ShortName = "po")]
			public bool poolOnly;

			// Token: 0x040006D1 RID: 1745
			[Argument(4, HelpText = "If true, the transform will always provide a shuffled view.", ShortName = "force")]
			public bool forceShuffle;

			// Token: 0x040006D2 RID: 1746
			[Argument(4, HelpText = "If true, the transform will always shuffle the input. The default value is the same as forceShuffle.", ShortName = "forceSource")]
			public bool? forceShuffleSource;

			// Token: 0x040006D3 RID: 1747
			[Argument(4, HelpText = "The random seed to use for forced shuffling.", ShortName = "seed")]
			public int? forceShuffleSeed;
		}

		// Token: 0x0200022B RID: 555
		private sealed class RowCursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00043F50 File Offset: 0x00042150
			public ISchema Schema
			{
				get
				{
					return this._input.Schema;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00043F5D File Offset: 0x0004215D
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06000C83 RID: 3203 RVA: 0x00043F64 File Offset: 0x00042164
			public RowCursor(IChannelProvider provider, int poolRows, IRowCursor input, IRandom rand)
				: base(provider)
			{
				this._poolRows = poolRows;
				this._input = input;
				this._rand = rand;
				this._pipeIndices = Utils.GetIdentityPermutation(this._poolRows - 1 + 48);
				int columnCount = this.Schema.ColumnCount;
				int num = 0;
				this._colToActivesIndex = new int[columnCount];
				for (int i = 0; i < columnCount; i++)
				{
					int[] colToActivesIndex = this._colToActivesIndex;
					int num2 = i;
					int num3;
					if (!this._input.IsColumnActive(i))
					{
						num3 = -1;
					}
					else
					{
						num = (num3 = num) + 1;
					}
					colToActivesIndex[num2] = num3;
				}
				this._pipes = new ShuffleTransform.RowCursor.ShufflePipe[num + 1];
				this._getters = new Delegate[num];
				for (int j = 0; j < columnCount; j++)
				{
					int num4 = this._colToActivesIndex[j];
					if (num4 >= 0)
					{
						this._pipes[num4] = ShuffleTransform.RowCursor.ShufflePipe.Create(this._pipeIndices.Length, input.Schema.GetColumnType(j), RowCursorUtils.GetGetterAsDelegate(input, j));
						this._getters[num4] = this.CreateGetterDelegate(j);
					}
				}
				ShuffleTransform.RowCursor.ShufflePipe shufflePipe = (this._pipes[num] = ShuffleTransform.RowCursor.ShufflePipe.Create(this._pipeIndices.Length, NumberType.UG, input.GetIdGetter()));
				this._idGetter = this.CreateGetterDelegate<UInt128>(shufflePipe);
				this._pipeIndex = (this._circularIndex = this._pipeIndices.Length - 1);
				this._deadCount = -1;
				this._liveCount = 1;
				this._toConsume = new BufferBlock<int>();
				this._toProduce = new BufferBlock<int>();
				ShuffleTransform.RowCursor.PostAssert<int>(this._toProduce, this._poolRows - 1 + 16);
				for (int k = 1; k < 3; k++)
				{
					ShuffleTransform.RowCursor.PostAssert<int>(this._toProduce, 16);
				}
				this._producerTask = this.LoopProducerWorker();
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x00044109 File Offset: 0x00042309
			public override void Dispose()
			{
				if (this._producerTask.Status == TaskStatus.Running)
				{
					DataflowBlock.Post<int>(this._toProduce, 0);
					this._producerTask.Wait();
				}
				base.Dispose();
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x00044137 File Offset: 0x00042337
			public static void PostAssert<T>(ITargetBlock<T> target, T item)
			{
				DataflowBlock.Post<T>(target, item);
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x00044141 File Offset: 0x00042341
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return this._idGetter;
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x00044384 File Offset: 0x00042584
			private async Task LoopProducerWorker()
			{
				try
				{
					int circularIndex = 0;
					int numRows;
					for (;;)
					{
						int requested = await DataflowBlock.ReceiveAsync<int>(this._toProduce);
						if (requested == 0)
						{
							break;
						}
						numRows = 0;
						while (numRows < requested && this._input.MoveNext())
						{
							int num = this._pipeIndices[circularIndex++];
							for (int i = 0; i < this._pipes.Length; i++)
							{
								this._pipes[i].Fill(num);
							}
							if (circularIndex == this._pipeIndices.Length)
							{
								circularIndex = 0;
							}
							numRows++;
						}
						ShuffleTransform.RowCursor.PostAssert<int>(this._toConsume, numRows);
						if (numRows < requested)
						{
							goto Block_6;
						}
					}
					return;
					Block_6:
					if (numRows > 0)
					{
						ShuffleTransform.RowCursor.PostAssert<int>(this._toConsume, 0);
					}
				}
				catch (Exception ex)
				{
					this._producerTaskException = ex;
					ShuffleTransform.RowCursor.PostAssert<int>(this._toConsume, 0);
				}
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x000443CC File Offset: 0x000425CC
			protected override bool MoveNextCore()
			{
				if (++this._circularIndex == this._pipeIndices.Length)
				{
					this._circularIndex = 0;
				}
				this._liveCount--;
				if (++this._deadCount >= 16 && !this._doneConsuming)
				{
					ShuffleTransform.RowCursor.PostAssert<int>(this._toProduce, this._deadCount);
					this._deadCount = 0;
				}
				while (this._liveCount < this._poolRows && !this._doneConsuming)
				{
					int num = DataflowBlock.Receive<int>(this._toConsume);
					if (num == 0)
					{
						if (this._producerTaskException != null)
						{
							throw Contracts.Except(this._ch, this._producerTaskException, "Shuffle input cursor reader failed with an exception");
						}
						this._doneConsuming = true;
						break;
					}
					else
					{
						this._liveCount += num;
					}
				}
				if (this._liveCount == 0)
				{
					return false;
				}
				int num2 = (this._rand.Next(Math.Min(this._liveCount, this._poolRows)) + this._circularIndex) % this._pipeIndices.Length;
				this._pipeIndex = this._pipeIndices[num2];
				this._pipeIndices[num2] = this._pipeIndices[this._circularIndex];
				this._pipeIndices[this._circularIndex] = this._pipeIndex;
				return true;
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x00044509 File Offset: 0x00042709
			public bool IsColumnActive(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._colToActivesIndex.Length, "col");
				return this._input.IsColumnActive(col);
			}

			// Token: 0x06000C8A RID: 3210 RVA: 0x0004453C File Offset: 0x0004273C
			private Delegate CreateGetterDelegate(int col)
			{
				Func<int, Delegate> func = new Func<int, Delegate>(this.CreateGetterDelegate<int>);
				return Utils.MarshalInvoke<int, Delegate>(func, this.Schema.GetColumnType(col).RawType, col);
			}

			// Token: 0x06000C8B RID: 3211 RVA: 0x0004456E File Offset: 0x0004276E
			private Delegate CreateGetterDelegate<TValue>(int col)
			{
				return this.CreateGetterDelegate<TValue>(this._pipes[this._colToActivesIndex[col]]);
			}

			// Token: 0x06000C8C RID: 3212 RVA: 0x000445A8 File Offset: 0x000427A8
			private ValueGetter<TValue> CreateGetterDelegate<TValue>(ShuffleTransform.RowCursor.ShufflePipe pipe)
			{
				ShuffleTransform.RowCursor.ShufflePipe<TValue> pipe2 = (ShuffleTransform.RowCursor.ShufflePipe<TValue>)pipe;
				return delegate(ref TValue value)
				{
					pipe2.Fetch(this._pipeIndex, ref value);
				};
			}

			// Token: 0x06000C8D RID: 3213 RVA: 0x000445DC File Offset: 0x000427DC
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._colToActivesIndex.Length, "col");
				Contracts.CheckParam(this._ch, this._colToActivesIndex[col] >= 0, "col", "requested column not active");
				ValueGetter<TValue> valueGetter = this._getters[this._colToActivesIndex[col]] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x040006D4 RID: 1748
			private const int _blockSize = 16;

			// Token: 0x040006D5 RID: 1749
			private const int _bufferDepth = 3;

			// Token: 0x040006D6 RID: 1750
			private readonly int _poolRows;

			// Token: 0x040006D7 RID: 1751
			private readonly IRowCursor _input;

			// Token: 0x040006D8 RID: 1752
			private readonly IRandom _rand;

			// Token: 0x040006D9 RID: 1753
			private readonly int[] _pipeIndices;

			// Token: 0x040006DA RID: 1754
			private readonly ShuffleTransform.RowCursor.ShufflePipe[] _pipes;

			// Token: 0x040006DB RID: 1755
			private readonly Delegate[] _getters;

			// Token: 0x040006DC RID: 1756
			private readonly ValueGetter<UInt128> _idGetter;

			// Token: 0x040006DD RID: 1757
			private int _circularIndex;

			// Token: 0x040006DE RID: 1758
			private int _pipeIndex;

			// Token: 0x040006DF RID: 1759
			private int _deadCount;

			// Token: 0x040006E0 RID: 1760
			private int _liveCount;

			// Token: 0x040006E1 RID: 1761
			private bool _doneConsuming;

			// Token: 0x040006E2 RID: 1762
			private readonly BufferBlock<int> _toProduce;

			// Token: 0x040006E3 RID: 1763
			private readonly BufferBlock<int> _toConsume;

			// Token: 0x040006E4 RID: 1764
			private readonly Task _producerTask;

			// Token: 0x040006E5 RID: 1765
			private Exception _producerTaskException;

			// Token: 0x040006E6 RID: 1766
			private readonly int[] _colToActivesIndex;

			// Token: 0x0200022C RID: 556
			private enum ExtraIndex
			{
				// Token: 0x040006E8 RID: 1768
				Id,
				// Token: 0x040006E9 RID: 1769
				_Lim
			}

			// Token: 0x0200022D RID: 557
			private abstract class ShufflePipe
			{
				// Token: 0x06000C8E RID: 3214 RVA: 0x00044670 File Offset: 0x00042870
				public static ShuffleTransform.RowCursor.ShufflePipe Create(int bufferSize, ColumnType type, Delegate getter)
				{
					Type type2;
					if (type.IsVector)
					{
						type2 = typeof(ShuffleTransform.RowCursor.ShufflePipe.ImplVec<>).MakeGenericType(new Type[] { type.ItemType.RawType });
					}
					else
					{
						type2 = typeof(ShuffleTransform.RowCursor.ShufflePipe.ImplOne<>).MakeGenericType(new Type[] { type.RawType });
					}
					if (ShuffleTransform.RowCursor.ShufflePipe._pipeConstructorTypes == null)
					{
						Interlocked.CompareExchange<Type[]>(ref ShuffleTransform.RowCursor.ShufflePipe._pipeConstructorTypes, new Type[]
						{
							typeof(int),
							typeof(Delegate)
						}, null);
					}
					ConstructorInfo constructor = type2.GetConstructor(ShuffleTransform.RowCursor.ShufflePipe._pipeConstructorTypes);
					return (ShuffleTransform.RowCursor.ShufflePipe)constructor.Invoke(new object[] { bufferSize, getter });
				}

				// Token: 0x06000C8F RID: 3215
				public abstract void Fill(int idx);

				// Token: 0x040006EA RID: 1770
				private static volatile Type[] _pipeConstructorTypes;

				// Token: 0x0200022F RID: 559
				private sealed class ImplVec<T> : ShuffleTransform.RowCursor.ShufflePipe<VBuffer<T>>
				{
					// Token: 0x06000C95 RID: 3221 RVA: 0x00044791 File Offset: 0x00042991
					public ImplVec(int bufferSize, Delegate getter)
						: base(bufferSize, getter)
					{
					}

					// Token: 0x06000C96 RID: 3222 RVA: 0x0004479B File Offset: 0x0004299B
					protected override void Copy(ref VBuffer<T> src, ref VBuffer<T> dst)
					{
						src.CopyTo(ref dst);
					}
				}

				// Token: 0x02000230 RID: 560
				private sealed class ImplOne<T> : ShuffleTransform.RowCursor.ShufflePipe<T>
				{
					// Token: 0x06000C97 RID: 3223 RVA: 0x000447A4 File Offset: 0x000429A4
					public ImplOne(int bufferSize, Delegate getter)
						: base(bufferSize, getter)
					{
					}

					// Token: 0x06000C98 RID: 3224 RVA: 0x000447AE File Offset: 0x000429AE
					protected override void Copy(ref T src, ref T dst)
					{
						dst = src;
					}
				}
			}

			// Token: 0x0200022E RID: 558
			private abstract class ShufflePipe<T> : ShuffleTransform.RowCursor.ShufflePipe
			{
				// Token: 0x06000C91 RID: 3217 RVA: 0x00044743 File Offset: 0x00042943
				public ShufflePipe(int bufferSize, Delegate getter)
				{
					this._getter = (ValueGetter<T>)getter;
					this._buffer = new T[bufferSize];
				}

				// Token: 0x06000C92 RID: 3218 RVA: 0x00044763 File Offset: 0x00042963
				public override void Fill(int idx)
				{
					this._getter.Invoke(ref this._buffer[idx]);
				}

				// Token: 0x06000C93 RID: 3219 RVA: 0x0004477C File Offset: 0x0004297C
				public void Fetch(int idx, ref T value)
				{
					this.Copy(ref this._buffer[idx], ref value);
				}

				// Token: 0x06000C94 RID: 3220
				protected abstract void Copy(ref T src, ref T dst);

				// Token: 0x040006EB RID: 1771
				private readonly ValueGetter<T> _getter;

				// Token: 0x040006EC RID: 1772
				protected readonly T[] _buffer;
			}
		}
	}
}
