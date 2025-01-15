using System;
using System.IO;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.IO.Zlib;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x02000309 RID: 777
	public abstract class Compression
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600115D RID: 4445
		public abstract CompressionKind Kind { get; }

		// Token: 0x0600115E RID: 4446 RVA: 0x0005FEBA File Offset: 0x0005E0BA
		public virtual Stream Open(Stream stream)
		{
			return this.Kind.CompressStream(stream);
		}

		// Token: 0x0200030A RID: 778
		public sealed class NoneImpl : Compression
		{
			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06001160 RID: 4448 RVA: 0x0005FED0 File Offset: 0x0005E0D0
			public override CompressionKind Kind
			{
				get
				{
					return CompressionKind.None;
				}
			}
		}

		// Token: 0x0200030B RID: 779
		public sealed class ZlibImpl : Compression
		{
			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06001162 RID: 4450 RVA: 0x0005FEDB File Offset: 0x0005E0DB
			public override CompressionKind Kind
			{
				get
				{
					if (!this._isDeflate)
					{
						return CompressionKind.Zlib;
					}
					return CompressionKind.Deflate;
				}
			}

			// Token: 0x06001163 RID: 4451 RVA: 0x0005FEE8 File Offset: 0x0005E0E8
			private ZlibImpl(Compression.ZlibImpl.ArgumentsBase args, bool isDeflate)
			{
				Contracts.CheckUserArg(args.compressionLevel == null || (0 <= args.compressionLevel && args.compressionLevel <= 9), "compressionLevel", "Must be in range 0 to 9 or null");
				Contracts.CheckUserArg(8 <= args.windowBits && args.windowBits <= 15, "windowBits", "Must be in range 8 to 15");
				Contracts.CheckUserArg(1 <= args.memoryLevel && args.memoryLevel <= 9, "memoryLevel", "Must be in range 1 to 9");
				Contracts.CheckUserArg(Enum.IsDefined(typeof(Constants.Strategy), args.strategy), "strategy", "Value was not defined");
				if (args.compressionLevel == null)
				{
					this._level = Constants.Level.DefaultCompression;
				}
				else
				{
					this._level = (Constants.Level)args.compressionLevel.Value;
				}
				this._windowBits = args.windowBits;
				this._isDeflate = isDeflate;
				this._memoryLevel = args.memoryLevel;
				this._strategy = args.strategy;
			}

			// Token: 0x06001164 RID: 4452 RVA: 0x00060021 File Offset: 0x0005E221
			public ZlibImpl(Compression.ZlibImpl.DeflateArguments args)
				: this(args, true)
			{
			}

			// Token: 0x06001165 RID: 4453 RVA: 0x0006002B File Offset: 0x0005E22B
			public ZlibImpl(Compression.ZlibImpl.ZlibArguments args)
				: this(args, false)
			{
			}

			// Token: 0x06001166 RID: 4454 RVA: 0x00060035 File Offset: 0x0005E235
			public override Stream Open(Stream stream)
			{
				return new ZDeflateStream(stream, this._level, this._strategy, this._memoryLevel, !this._isDeflate, this._windowBits);
			}

			// Token: 0x040009DA RID: 2522
			private readonly int _windowBits;

			// Token: 0x040009DB RID: 2523
			private readonly Constants.Level _level;

			// Token: 0x040009DC RID: 2524
			private readonly bool _isDeflate;

			// Token: 0x040009DD RID: 2525
			private readonly int _memoryLevel;

			// Token: 0x040009DE RID: 2526
			private readonly Constants.Strategy _strategy;

			// Token: 0x0200030C RID: 780
			public abstract class ArgumentsBase
			{
				// Token: 0x040009DF RID: 2527
				[Argument(0, HelpText = "Level of compression from 0 to 9", ShortName = "c")]
				public int? compressionLevel = new int?(9);

				// Token: 0x040009E0 RID: 2528
				[Argument(0, HelpText = "Window bits from 8 to 15, higher values enable more useful run length encodings", ShortName = "w")]
				public int windowBits = 15;

				// Token: 0x040009E1 RID: 2529
				[Argument(0, HelpText = "Level of memory from 1 to 9, with higher values using more memory but enabling better, faster compression", ShortName = "m")]
				public int memoryLevel = 9;

				// Token: 0x040009E2 RID: 2530
				[Argument(0, HelpText = "Compression strategy to employ", ShortName = "s")]
				public Constants.Strategy strategy;
			}

			// Token: 0x0200030D RID: 781
			public sealed class DeflateArguments : Compression.ZlibImpl.ArgumentsBase
			{
			}

			// Token: 0x0200030E RID: 782
			public sealed class ZlibArguments : Compression.ZlibImpl.ArgumentsBase
			{
			}
		}
	}
}
