using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Common;
using Microsoft.HostIntegration.PerformanceCounters.Drda;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007C3 RID: 1987
	public sealed class DdmReader
	{
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x000D3BB2 File Offset: 0x000D1DB2
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x000D3BBA File Offset: 0x000D1DBA
		public bool IsManagedMsDrda
		{
			get
			{
				return this._isManagedMsDrda;
			}
			set
			{
				this._isManagedMsDrda = value;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x000D3BC3 File Offset: 0x000D1DC3
		// (set) Token: 0x06003F22 RID: 16162 RVA: 0x000D3BCB File Offset: 0x000D1DCB
		public ICcsidManager CcsidManager
		{
			get
			{
				return this._ccsidManager;
			}
			set
			{
				this._ccsidManager = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x000D3BD4 File Offset: 0x000D1DD4
		public Converter Converter
		{
			get
			{
				return this._converter;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x000D3BDC File Offset: 0x000D1DDC
		// (set) Token: 0x06003F25 RID: 16165 RVA: 0x000D3BE4 File Offset: 0x000D1DE4
		public Ccsid Ccsid
		{
			get
			{
				return this._ccsid;
			}
			set
			{
				this._ccsid = value;
				this._converter.SetCodePage(this._ccsid);
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000D3BFE File Offset: 0x000D1DFE
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x000D3C06 File Offset: 0x000D1E06
		public bool IsReadingDDMObject
		{
			get
			{
				return this._isReadingDDMObject;
			}
			set
			{
				this._isReadingDDMObject = value;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x000D3C0F File Offset: 0x000D1E0F
		public int InputBufferLength
		{
			get
			{
				return this._inputBuffer.Count;
			}
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x000D3C1C File Offset: 0x000D1E1C
		public DdmReader(Stream stream, ICcsidManager ccsidManager, Converter converter, CommonDrdaPerformanceCountersContainer perfContainer)
			: this(stream, ccsidManager, converter, perfContainer, null)
		{
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x000D3C2C File Offset: 0x000D1E2C
		public DdmReader(Stream stream, ICcsidManager ccsidManager, Converter converter, CommonDrdaPerformanceCountersContainer perfContainer, object tracePoint)
		{
			this._stream = stream;
			this._converter = converter;
			this._ccsidManager = ccsidManager;
			this.CommonPerformanceContainer = perfContainer;
			this._tracePoint = tracePoint;
			this._inputBuffer = new Buffer(stream, this.CommonPerformanceContainer, this._tracePoint);
			this._dssOriginalPosition = this._inputBuffer.Position;
			this._dssOriginalLength = 0;
			this._ddmCollectionLenStack = new long[10];
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x000D3CC0 File Offset: 0x000D1EC0
		public void Reset(Stream stream)
		{
			this._stream = stream;
			if (this._inputBuffer.Bytes != null)
			{
				ByteArrayPool.Put(this._inputBuffer.Bytes);
			}
			this._inputBuffer = new Buffer(stream, this.CommonPerformanceContainer, this._tracePoint);
			this._ddmCollectionLenStack = new long[10];
			this._ddmObjectLength = 0L;
			this._dssLength = 0;
			this._dssIsContinued = false;
			this._terminateOnError = false;
			this._dssChainedWithSameID = false;
			this._dssChainedWithDiffID = false;
			this._dssCorrelationID = -1;
			this._previousCorrelationID = -1;
			this._dssOriginalPosition = this._inputBuffer.Position;
			this._dssOriginalLength = 0;
			this._enddianType = EndianType.LittleEndian;
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x000D3D6E File Offset: 0x000D1F6E
		public int DssLength
		{
			get
			{
				return this._dssLength;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x000D3D76 File Offset: 0x000D1F76
		public int DssOriginalPosition
		{
			get
			{
				return this._dssOriginalPosition;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x000D3D7E File Offset: 0x000D1F7E
		public int DssOriginalLength
		{
			get
			{
				return this._dssOriginalLength;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003F2F RID: 16175 RVA: 0x000D3D86 File Offset: 0x000D1F86
		public int Position
		{
			get
			{
				return this._inputBuffer.Position;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x000D3D93 File Offset: 0x000D1F93
		public bool IsDdmObjectAvailable
		{
			get
			{
				return this._inputBuffer.IsDataAvailable(3);
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x000D3DA1 File Offset: 0x000D1FA1
		public byte DssFormat
		{
			get
			{
				return this._dssFormat;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x000D3DA9 File Offset: 0x000D1FA9
		// (set) Token: 0x06003F33 RID: 16179 RVA: 0x000D3DB1 File Offset: 0x000D1FB1
		public EndianType EndianType
		{
			get
			{
				return this._enddianType;
			}
			set
			{
				this._enddianType = value;
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000D3DBC File Offset: 0x000D1FBC
		public int ReadDss()
		{
			return this.ReadDssAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x000D3DE4 File Offset: 0x000D1FE4
		public async Task<int> ReadDssAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.ReadDssLengthAsync(isAsync, cancellationToken);
			await this.ReadDssIdAsync(isAsync, cancellationToken);
			await this.ReadDssFormatByteAsync(isAsync, cancellationToken);
			return await this.ReadCorrelationIDAsync(isAsync, cancellationToken);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x000D3E39 File Offset: 0x000D2039
		public IEnumerable<ObjectInfo> ReadDssObjects()
		{
			do
			{
				this.ReadDss();
				CodePoint result = this.ReadDdmObjectLengthAndCodePointAsync(false, CancellationToken.None).GetAwaiter().GetResult();
				yield return new ObjectInfo(result, this._ddmObjectLength, this._dssCorrelationID);
			}
			while (this.DssChained);
			yield break;
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x000D3E49 File Offset: 0x000D2049
		public IEnumerable<Task<ObjectInfo>> ReadDssObjectsAsync(CancellationToken cancellationToken)
		{
			DdmReader.<>c__DisplayClass60_0 CS$<>8__locals1 = new DdmReader.<>c__DisplayClass60_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			do
			{
				Func<Task<ObjectInfo>> func;
				if ((func = CS$<>8__locals1.<>9__0) == null)
				{
					func = (CS$<>8__locals1.<>9__0 = delegate
					{
						DdmReader.<>c__DisplayClass60_0.<<ReadDssObjectsAsync>b__0>d <<ReadDssObjectsAsync>b__0>d;
						<<ReadDssObjectsAsync>b__0>d.<>4__this = CS$<>8__locals1;
						<<ReadDssObjectsAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ObjectInfo>.Create();
						<<ReadDssObjectsAsync>b__0>d.<>1__state = -1;
						AsyncTaskMethodBuilder<ObjectInfo> <>t__builder = <<ReadDssObjectsAsync>b__0>d.<>t__builder;
						<>t__builder.Start<DdmReader.<>c__DisplayClass60_0.<<ReadDssObjectsAsync>b__0>d>(ref <<ReadDssObjectsAsync>b__0>d);
						return <<ReadDssObjectsAsync>b__0>d.<>t__builder.Task;
					});
				}
				Task<ObjectInfo> task = Task.Run<ObjectInfo>(func, CS$<>8__locals1.cancellationToken);
				yield return task;
			}
			while (this.DssChained);
			yield break;
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x000D3E60 File Offset: 0x000D2060
		public IEnumerable<ObjectInfo> ReadDdmObjects()
		{
			this.ValidateDdmCollectionStack();
			long[] ddmCollectionLenStack = this._ddmCollectionLenStack;
			int num = this._topDdmCollectionStack + 1;
			this._topDdmCollectionStack = num;
			ddmCollectionLenStack[num] = this._ddmObjectLength;
			this._ddmObjectLength = 0L;
			while (this._topDdmCollectionStack != -1)
			{
				if (this._ddmCollectionLenStack[this._topDdmCollectionStack] <= 0L)
				{
					this._topDdmCollectionStack--;
					break;
				}
				CodePoint result = this.ReadDdmObjectLengthAndCodePointAsync(false, CancellationToken.None).GetAwaiter().GetResult();
				yield return new ObjectInfo(result, this._ddmObjectLength, this._dssCorrelationID);
			}
			if (this._topDdmCollectionStack == -1 && !this._isManagedMsDrda)
			{
				while (this._dssChainedWithSameID)
				{
					this.ReadDss();
					while (!this.DssEmpty())
					{
						CodePoint result2 = this.ReadDdmObjectLengthAndCodePointAsync(false, CancellationToken.None).GetAwaiter().GetResult();
						yield return new ObjectInfo(result2, this._ddmObjectLength, this._dssCorrelationID);
					}
				}
			}
			yield break;
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x000D3E70 File Offset: 0x000D2070
		public IEnumerable<Task<ObjectInfo>> ReadDdmObjectsAsync(CancellationToken cancellationToken)
		{
			DdmReader.<>c__DisplayClass62_0 CS$<>8__locals1 = new DdmReader.<>c__DisplayClass62_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			this.ValidateDdmCollectionStack();
			long[] ddmCollectionLenStack = this._ddmCollectionLenStack;
			int num = this._topDdmCollectionStack + 1;
			this._topDdmCollectionStack = num;
			ddmCollectionLenStack[num] = this._ddmObjectLength;
			this._ddmObjectLength = 0L;
			while (this._topDdmCollectionStack != -1)
			{
				if (this._ddmCollectionLenStack[this._topDdmCollectionStack] <= 0L)
				{
					this._topDdmCollectionStack--;
					break;
				}
				Func<Task<ObjectInfo>> func;
				if ((func = CS$<>8__locals1.<>9__0) == null)
				{
					func = (CS$<>8__locals1.<>9__0 = delegate
					{
						DdmReader.<>c__DisplayClass62_0.<<ReadDdmObjectsAsync>b__0>d <<ReadDdmObjectsAsync>b__0>d;
						<<ReadDdmObjectsAsync>b__0>d.<>4__this = CS$<>8__locals1;
						<<ReadDdmObjectsAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ObjectInfo>.Create();
						<<ReadDdmObjectsAsync>b__0>d.<>1__state = -1;
						AsyncTaskMethodBuilder<ObjectInfo> <>t__builder = <<ReadDdmObjectsAsync>b__0>d.<>t__builder;
						<>t__builder.Start<DdmReader.<>c__DisplayClass62_0.<<ReadDdmObjectsAsync>b__0>d>(ref <<ReadDdmObjectsAsync>b__0>d);
						return <<ReadDdmObjectsAsync>b__0>d.<>t__builder.Task;
					});
				}
				Task<ObjectInfo> task = Task.Run<ObjectInfo>(func, CS$<>8__locals1.cancellationToken);
				yield return task;
			}
			if (this._topDdmCollectionStack == -1 && !this._isManagedMsDrda)
			{
				while (this._dssChainedWithSameID)
				{
					Func<Task<ObjectInfo>> func2;
					if ((func2 = CS$<>8__locals1.<>9__1) == null)
					{
						func2 = (CS$<>8__locals1.<>9__1 = delegate
						{
							DdmReader.<>c__DisplayClass62_0.<<ReadDdmObjectsAsync>b__1>d <<ReadDdmObjectsAsync>b__1>d;
							<<ReadDdmObjectsAsync>b__1>d.<>4__this = CS$<>8__locals1;
							<<ReadDdmObjectsAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<ObjectInfo>.Create();
							<<ReadDdmObjectsAsync>b__1>d.<>1__state = -1;
							AsyncTaskMethodBuilder<ObjectInfo> <>t__builder2 = <<ReadDdmObjectsAsync>b__1>d.<>t__builder;
							<>t__builder2.Start<DdmReader.<>c__DisplayClass62_0.<<ReadDdmObjectsAsync>b__1>d>(ref <<ReadDdmObjectsAsync>b__1>d);
							return <<ReadDdmObjectsAsync>b__1>d.<>t__builder.Task;
						});
					}
					Task<ObjectInfo> task2 = Task.Run<ObjectInfo>(func2, CS$<>8__locals1.cancellationToken);
					yield return task2;
					while (!this.DssEmpty())
					{
						Func<Task<ObjectInfo>> func3;
						if ((func3 = CS$<>8__locals1.<>9__2) == null)
						{
							func3 = (CS$<>8__locals1.<>9__2 = delegate
							{
								DdmReader.<>c__DisplayClass62_0.<<ReadDdmObjectsAsync>b__2>d <<ReadDdmObjectsAsync>b__2>d;
								<<ReadDdmObjectsAsync>b__2>d.<>4__this = CS$<>8__locals1;
								<<ReadDdmObjectsAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<ObjectInfo>.Create();
								<<ReadDdmObjectsAsync>b__2>d.<>1__state = -1;
								AsyncTaskMethodBuilder<ObjectInfo> <>t__builder3 = <<ReadDdmObjectsAsync>b__2>d.<>t__builder;
								<>t__builder3.Start<DdmReader.<>c__DisplayClass62_0.<<ReadDdmObjectsAsync>b__2>d>(ref <<ReadDdmObjectsAsync>b__2>d);
								return <<ReadDdmObjectsAsync>b__2>d.<>t__builder.Task;
							});
						}
						task2 = Task.Run<ObjectInfo>(func3, CS$<>8__locals1.cancellationToken);
						yield return task2;
					}
				}
			}
			yield break;
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x000D3E87 File Offset: 0x000D2087
		private void ValidateDdmCollectionStack()
		{
			while (this._topDdmCollectionStack >= 0)
			{
				if (this._ddmCollectionLenStack[this._topDdmCollectionStack] > 0L)
				{
					return;
				}
				this._ddmCollectionLenStack[this._topDdmCollectionStack] = 0L;
				this._topDdmCollectionStack--;
			}
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x000D3EC4 File Offset: 0x000D20C4
		public void SkipBytes(int len)
		{
			this.SkipBytesAsync(len, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000D3EEC File Offset: 0x000D20EC
		public async Task SkipBytesAsync(int len, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(len, true, isAsync, cancellationToken);
			this._inputBuffer.Position += len;
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000D3F49 File Offset: 0x000D2149
		public void SkipCurrentDdmObject()
		{
			this.SkipBytes((int)this._ddmObjectLength);
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000D3F58 File Offset: 0x000D2158
		public Task SkipCurrentDdmObjectAsync(bool isAsync, CancellationToken cancellationToken)
		{
			return this.SkipBytesAsync((int)this._ddmObjectLength, isAsync, cancellationToken);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000D3F6C File Offset: 0x000D216C
		public void SkipDss()
		{
			this.SkipDssAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000D3F94 File Offset: 0x000D2194
		public async Task SkipDssAsync(bool isAsync, CancellationToken cancellationToken)
		{
			while (this._dssIsContinued)
			{
				await this.SkipBytesAsync(this._dssLength, isAsync, cancellationToken);
				await this.ReadDssContinuationHeaderAsync(isAsync, cancellationToken);
			}
			await this.SkipBytesAsync(this._dssLength, isAsync, cancellationToken);
			this._topDdmCollectionStack = -1;
			this._ddmObjectLength = 0L;
			this._dssLength = 0;
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000D3FEC File Offset: 0x000D21EC
		public void SkipRemainder(bool onlySkipSameIds, bool keepDssCorrelationID)
		{
			int dssCorrelationID = this._dssCorrelationID;
			this.SkipRemainderAsync(onlySkipSameIds, false, CancellationToken.None).GetAwaiter().GetResult();
			if (keepDssCorrelationID)
			{
				this._dssCorrelationID = dssCorrelationID;
			}
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000D4024 File Offset: 0x000D2224
		public void SkipRemainder(bool onlySkipSameIds)
		{
			this.SkipRemainderAsync(onlySkipSameIds, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x000D404C File Offset: 0x000D224C
		public async Task SkipRemainderAsync(bool onlySkipSameIds, bool isAsync, CancellationToken cancellationToken)
		{
			await this.SkipDssAsync(isAsync, cancellationToken);
			while (this._dssChainedWithSameID || (!onlySkipSameIds && this._dssChainedWithDiffID))
			{
				await this.ReadDssAsync(isAsync, cancellationToken);
				await this.SkipDssAsync(isAsync, cancellationToken);
			}
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000D40A9 File Offset: 0x000D22A9
		public string ConvertBytes(byte[] abyte0)
		{
			return this._ccsidManager.GetString(abyte0, 0, abyte0.Length);
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000D40BC File Offset: 0x000D22BC
		public void Close()
		{
			try
			{
				if (this._stream != null)
				{
					this._stream.Close();
				}
			}
			finally
			{
				this._inputBuffer.Dispose();
			}
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000D40FC File Offset: 0x000D22FC
		public byte GetCurrentChainState()
		{
			if (!this._dssChainedWithSameID && !this._dssChainedWithDiffID)
			{
				return 0;
			}
			if (this._dssChainedWithSameID)
			{
				return 80;
			}
			return 64;
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000D411D File Offset: 0x000D231D
		public bool DssEmpty()
		{
			return this._dssLength == 0;
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000D4128 File Offset: 0x000D2328
		public long DdmObjectLength
		{
			get
			{
				return this._ddmObjectLength;
			}
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000D4130 File Offset: 0x000D2330
		public bool HasMoreDdmObjectData()
		{
			return this._ddmObjectLength > 0L;
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000D413C File Offset: 0x000D233C
		public bool DssChained
		{
			get
			{
				return this._dssChainedWithSameID || this._dssChainedWithDiffID;
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000D414E File Offset: 0x000D234E
		public bool TerminateChainOnError
		{
			get
			{
				return this._terminateOnError;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000D4156 File Offset: 0x000D2356
		public int DssCorrelationID
		{
			get
			{
				return this._dssCorrelationID;
			}
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x000D4160 File Offset: 0x000D2360
		public byte ReadByte()
		{
			return this.ReadByteAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x000D4188 File Offset: 0x000D2388
		public async Task<byte> ReadByteAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(1, true, isAsync, cancellationToken);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int position = inputBuffer2.Position;
			inputBuffer2.Position = position + 1;
			return inputBuffer[position];
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x000D41DD File Offset: 0x000D23DD
		public void UnreadBytes(int length)
		{
			this._inputBuffer.Position -= length;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x000D41F4 File Offset: 0x000D23F4
		public int ReadUnsignedByte()
		{
			return this.ReadUnsignedByteAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000D421C File Offset: 0x000D241C
		public async Task<int> ReadUnsignedByteAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(1, true, isAsync, cancellationToken);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int position = inputBuffer2.Position;
			inputBuffer2.Position = position + 1;
			return (int)(inputBuffer[position] & byte.MaxValue);
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x000D4274 File Offset: 0x000D2474
		public bool ReadBoolean()
		{
			return this.ReadBooleanAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000D429C File Offset: 0x000D249C
		public async Task<bool> ReadBooleanAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(1, true, isAsync, cancellationToken);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int position = inputBuffer2.Position;
			inputBuffer2.Position = position + 1;
			return inputBuffer[position] > 0;
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000D42F1 File Offset: 0x000D24F1
		public short ReadInt16()
		{
			return this.ReadInt16(this._enddianType);
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x000D42FF File Offset: 0x000D24FF
		public Task<short> ReadInt16Async(bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadInt16Async(this._enddianType, isAsync, cancellationToken);
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x000D4310 File Offset: 0x000D2510
		public int ReadNetworkShort()
		{
			return this.ReadNetworkShortAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000D4338 File Offset: 0x000D2538
		public async Task<int> ReadNetworkShortAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(2, true, isAsync, cancellationToken);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int num = inputBuffer2.Position;
			inputBuffer2.Position = num + 1;
			int num2 = (int)(inputBuffer[num] & byte.MaxValue) << 8;
			Buffer inputBuffer3 = this._inputBuffer;
			Buffer inputBuffer4 = this._inputBuffer;
			num = inputBuffer4.Position;
			inputBuffer4.Position = num + 1;
			return num2 + (int)(inputBuffer3[num] & byte.MaxValue);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000D4390 File Offset: 0x000D2590
		public int ReadNetworkInt()
		{
			return this.ReadNetworkIntAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000D43B8 File Offset: 0x000D25B8
		public async Task<int> ReadNetworkIntAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(4, true, isAsync, cancellationToken);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int num = inputBuffer2.Position;
			inputBuffer2.Position = num + 1;
			int num2 = (int)(inputBuffer[num] & byte.MaxValue) << 24;
			Buffer inputBuffer3 = this._inputBuffer;
			Buffer inputBuffer4 = this._inputBuffer;
			num = inputBuffer4.Position;
			inputBuffer4.Position = num + 1;
			int num3 = num2 + ((int)(inputBuffer3[num] & byte.MaxValue) << 16);
			Buffer inputBuffer5 = this._inputBuffer;
			Buffer inputBuffer6 = this._inputBuffer;
			num = inputBuffer6.Position;
			inputBuffer6.Position = num + 1;
			int num4 = num3 + ((int)(inputBuffer5[num] & byte.MaxValue) << 8);
			Buffer inputBuffer7 = this._inputBuffer;
			Buffer inputBuffer8 = this._inputBuffer;
			num = inputBuffer8.Position;
			inputBuffer8.Position = num + 1;
			return num4 + (int)(inputBuffer7[num] & byte.MaxValue);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000D4410 File Offset: 0x000D2610
		public ushort ReadUInt16(EndianType type)
		{
			return this.ReadUInt16Async(type, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000D4438 File Offset: 0x000D2638
		public async Task<ushort> ReadUInt16Async(EndianType type, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(2, true, isAsync, cancellationToken);
			ushort num = Converter.ToUInt16(this._inputBuffer.Bytes, this._inputBuffer.Position, type);
			this._inputBuffer.Position += 2;
			return num;
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x000D4498 File Offset: 0x000D2698
		public short ReadInt16(EndianType type)
		{
			return this.ReadInt16Async(type, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x000D44C0 File Offset: 0x000D26C0
		public async Task<short> ReadInt16Async(EndianType type, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(2, true, isAsync, cancellationToken);
			short num = Converter.ToInt16(this._inputBuffer.Bytes, this._inputBuffer.Position, type);
			this._inputBuffer.Position += 2;
			return num;
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000D451D File Offset: 0x000D271D
		public int ReadInt32()
		{
			return this.ReadInt32(this._enddianType);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x000D452B File Offset: 0x000D272B
		public Task<int> ReadInt32Async(bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadInt32Async(this._enddianType, isAsync, cancellationToken);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x000D453C File Offset: 0x000D273C
		public int ReadInt32(EndianType type)
		{
			return this.ReadInt32Async(type, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x000D4564 File Offset: 0x000D2764
		public async Task<int> ReadInt32Async(EndianType type, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(4, true, isAsync, cancellationToken);
			int num = Converter.ToInt32(this._inputBuffer.Bytes, this._inputBuffer.Position, type);
			this._inputBuffer.Position += 4;
			return num;
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000D45C1 File Offset: 0x000D27C1
		public long ReadInt48()
		{
			return this.ReadInt48(this._enddianType);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000D45CF File Offset: 0x000D27CF
		public Task<long> ReadInt48Async(bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadInt48Async(this._enddianType, isAsync, cancellationToken);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000D45E0 File Offset: 0x000D27E0
		public long ReadInt48(EndianType type)
		{
			return this.ReadInt48Async(type, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000D4608 File Offset: 0x000D2808
		public async Task<long> ReadInt48Async(EndianType type, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(6, true, isAsync, cancellationToken);
			long num = Converter.ToInt48(this._inputBuffer.Bytes, this._inputBuffer.Position, type);
			this._inputBuffer.Position += 6;
			return num;
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000D4665 File Offset: 0x000D2865
		public long ReadInt64()
		{
			return this.ReadInt64(this._enddianType);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000D4673 File Offset: 0x000D2873
		public Task<long> ReadInt64Async(bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadInt64Async(this._enddianType, isAsync, cancellationToken);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x000D4684 File Offset: 0x000D2884
		public long ReadInt64(EndianType type)
		{
			return this.ReadInt64Async(type, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x000D46AC File Offset: 0x000D28AC
		public async Task<long> ReadInt64Async(EndianType type, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(8, true, isAsync, cancellationToken);
			long num = Converter.ToInt64(this._inputBuffer.Bytes, this._inputBuffer.Position, type);
			this._inputBuffer.Position += 8;
			return num;
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x000D4709 File Offset: 0x000D2909
		public string ReadString()
		{
			return this.ReadString((int)this._ddmObjectLength);
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x000D4718 File Offset: 0x000D2918
		public Task<string> ReadStringAsync(bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadStringAsync((int)this._ddmObjectLength, isAsync, cancellationToken);
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000D472C File Offset: 0x000D292C
		public string ReadString(int size)
		{
			return this.ReadStringAsync(size, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x000D4754 File Offset: 0x000D2954
		public async Task<string> ReadStringAsync(int size, bool isAsync, CancellationToken cancellationToken)
		{
			string text;
			if (this._ccsid != null && this._ccsid._ccsidmbc > 0)
			{
				text = await this.ReadStringAsync(size, this._ccsid._ccsidmbc, isAsync, cancellationToken);
			}
			else
			{
				await this.EnsureDataAvailableInBufferAsync(size, true, isAsync, cancellationToken);
				string @string = this._ccsidManager.GetString(this._inputBuffer.Bytes, this._inputBuffer.Position, size);
				this._inputBuffer.Position += size;
				text = @string;
			}
			return text;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000D47B1 File Offset: 0x000D29B1
		public string ReadString(int size, int encoding)
		{
			return this.ReadString(size, encoding, 48);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x000D47BD File Offset: 0x000D29BD
		public Task<string> ReadStringAsync(int size, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadStringAsync(size, encoding, 48, isAsync, cancellationToken);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000D47CC File Offset: 0x000D29CC
		public string ReadString(int size, int encoding, int drdaType)
		{
			return this.ReadStringAsync(size, encoding, drdaType, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000D47F8 File Offset: 0x000D29F8
		public async Task<string> ReadStringAsync(int size, int encoding, int drdaType, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(size, true, isAsync, cancellationToken);
			string text = null;
			this._converter.UnpackString(this._inputBuffer.Bytes, this._inputBuffer.Position, size, encoding, ref text, true, (DrdaTypes)drdaType);
			this._inputBuffer.Position += size;
			return text;
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000D4868 File Offset: 0x000D2A68
		public async Task<string> ReadStringXMLAsync(int size, int encoding, int drdaType, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(size, true, isAsync, cancellationToken);
			string text = null;
			this._converter.UnpackStringXML(this._inputBuffer.Bytes, this._inputBuffer.Position, size, encoding, ref text, true, (DrdaTypes)drdaType);
			this._inputBuffer.Position += size;
			return text;
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x000D48D8 File Offset: 0x000D2AD8
		public string ReadExtString(int size, int encoding, int drdaType)
		{
			return this.ReadExtStringAsync(size, encoding, drdaType, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000D4904 File Offset: 0x000D2B04
		public async Task<string> ReadExtStringAsync(int size, int encoding, int drdaType, bool isAsync, CancellationToken cancellationToken)
		{
			string strVal = null;
			byte[] array = await this.ReadExtDataAsync(size, isAsync, cancellationToken);
			if (size < 0 && array != null)
			{
				size = array.Length;
			}
			this._converter.UnpackString(array, 0, size, encoding, ref strVal, true, (DrdaTypes)drdaType);
			return strVal;
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000D4974 File Offset: 0x000D2B74
		public async Task<string> ReadExtDSStringAsync(int size, int encoding, int drdaType, bool isAsync, CancellationToken cancellationToken)
		{
			string strVal = null;
			byte[] array = await this.ReadExtDataAsync(size, isAsync, cancellationToken);
			this.SwapBytesForPackedString(ref array);
			this._converter.UnpackDSString(array, 0, size, encoding, ref strVal, (DrdaTypes)drdaType);
			return strVal;
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000D49E4 File Offset: 0x000D2BE4
		public string ReadLDString(int encoding)
		{
			return this.ReadLDStringAsync(encoding, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000D4A0C File Offset: 0x000D2C0C
		public async Task<string> ReadLDStringAsync(int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			int num = (int)(await this.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			return await this.ReadStringAsync(num, encoding, isAsync, cancellationToken);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000D4A69 File Offset: 0x000D2C69
		public string ReadLDString(ref int readLen, int encoding)
		{
			readLen = (int)this.ReadInt16(EndianType.BigEndian);
			return this.ReadString(readLen, encoding);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000D4A7D File Offset: 0x000D2C7D
		public byte[] ReadBytes()
		{
			return this.ReadBytes((int)this.DdmObjectLength);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000D4A8C File Offset: 0x000D2C8C
		public Task<byte[]> ReadBytesAsync(bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadBytesAsync((int)this.DdmObjectLength, isAsync, cancellationToken);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000D4AA0 File Offset: 0x000D2CA0
		public async Task<byte[]> ReadBytesAsync(int prefixSpace, int size, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(size, true, isAsync, cancellationToken);
			byte[] array = new byte[size + prefixSpace];
			Buffer.BlockCopy(this._inputBuffer.Bytes, this._inputBuffer.Position, array, prefixSpace, size);
			this._inputBuffer.Position += size;
			return array;
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x000D4B08 File Offset: 0x000D2D08
		public byte[] ReadExtData(int size)
		{
			return this.GetExtDataAsync((long)size, false, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x000D4B31 File Offset: 0x000D2D31
		public Task<byte[]> ReadExtDataAsync(int size, bool isAsync, CancellationToken cancellationToken)
		{
			return this.GetExtDataAsync((long)size, false, isAsync, cancellationToken);
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000D4B40 File Offset: 0x000D2D40
		public byte[] GetBytes(int position, int size)
		{
			byte[] array = new byte[size];
			Buffer.BlockCopy(this._inputBuffer.Bytes, position, array, 0, size);
			return array;
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000D4B6C File Offset: 0x000D2D6C
		public byte[] GetBytes(int size)
		{
			byte[] array = new byte[size];
			Buffer.BlockCopy(this._inputBuffer.Bytes, this._inputBuffer.Position, array, 0, size);
			return array;
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000D4BA0 File Offset: 0x000D2DA0
		public byte[] GetAllBytes()
		{
			byte[] array = new byte[this._inputBuffer.Count];
			Buffer.BlockCopy(this._inputBuffer.Bytes, 0, array, 0, this._inputBuffer.Count);
			return array;
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000D4BE0 File Offset: 0x000D2DE0
		public byte[] ReadBytes(int size)
		{
			return this.ReadBytesAsync(size, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000D4C08 File Offset: 0x000D2E08
		public async Task<byte[]> ReadBytesAsync(int size, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(size, true, isAsync, cancellationToken);
			byte[] array = new byte[size];
			Buffer.BlockCopy(this._inputBuffer.Bytes, this._inputBuffer.Position, array, 0, size);
			this._inputBuffer.Position += size;
			return array;
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000D4C68 File Offset: 0x000D2E68
		private async Task<int> ReadDssLengthAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._inputBuffer.EnsureDataAvailableAsync(2, isAsync, cancellationToken);
			if (!this._isReadingDDMObject)
			{
				this._dssOriginalPosition = this._inputBuffer.Position;
			}
			this._dssLength = (int)Converter.ToInt16(this._inputBuffer.Bytes, this._inputBuffer.Position);
			if (!this._isReadingDDMObject)
			{
				this._dssOriginalLength = this._dssLength;
			}
			else
			{
				this._dssOriginalLength += this._dssLength;
			}
			this._inputBuffer.Position += 2;
			if ((this._dssLength & 32768) == 32768)
			{
				this._dssLength = 32767;
				this._dssIsContinued = true;
			}
			else
			{
				if (this._dssLength < 6)
				{
					DrdaException.ThrowSyntaxrm(SyntaxErrorCode.DssLengthLessThan6);
				}
				this._dssIsContinued = false;
			}
			this._dssLength -= 2;
			return this._dssLength;
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x000D4CC0 File Offset: 0x000D2EC0
		private async Task ReadDssIdAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._inputBuffer.EnsureDataAvailableAsync(1, isAsync, cancellationToken);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int position = inputBuffer2.Position;
			inputBuffer2.Position = position + 1;
			if ((inputBuffer[position] & 255) != 208)
			{
				DrdaException.ThrowSyntaxrm(SyntaxErrorCode.C_ByteNotD0);
			}
			this._dssLength--;
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x000D4D18 File Offset: 0x000D2F18
		private async Task ReadDssFormatByteAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._inputBuffer.EnsureDataAvailableAsync(1, isAsync, cancellationToken);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int position = inputBuffer2.Position;
			inputBuffer2.Position = position + 1;
			this._dssFormat = inputBuffer[position];
			if ((this._dssFormat & 15) != 1 && (this._dssFormat & 15) != 2 && (this._dssFormat & 15) != 3 && (this._dssFormat & 15) != 5)
			{
				DrdaException.ThrowSyntaxrm(SyntaxErrorCode.F_ByteNotSupported);
			}
			if ((this._dssFormat & 64) == 64)
			{
				if ((this._dssFormat & 80) == 80)
				{
					this._dssChainedWithSameID = true;
					this._dssChainedWithDiffID = false;
				}
				else
				{
					this._dssChainedWithSameID = false;
					this._dssChainedWithDiffID = true;
				}
				if ((this._dssFormat & 32) == 32)
				{
					this._terminateOnError = false;
				}
				else
				{
					this._terminateOnError = true;
				}
			}
			else
			{
				if ((this._dssFormat & 80) == 80)
				{
					DrdaException.ThrowSyntaxrm(SyntaxErrorCode.ChainOffSameNextCorrelator);
				}
				if ((this._dssFormat & 32) == 32)
				{
					DrdaException.ThrowSyntaxrm(SyntaxErrorCode.ChainOffErrorContinue);
				}
				this._dssChainedWithSameID = false;
				this._dssChainedWithDiffID = false;
			}
			this._dssLength--;
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x000D4D70 File Offset: 0x000D2F70
		private async Task<int> ReadCorrelationIDAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._inputBuffer.EnsureDataAvailableAsync(2, isAsync, cancellationToken);
			this._dssCorrelationID = (int)Converter.ToInt16(this._inputBuffer.Bytes, this._inputBuffer.Position);
			this._inputBuffer.Position += 2;
			if (this._dssChainedWithSameID && this._previousCorrelationID != -1 && this._dssCorrelationID != this._previousCorrelationID)
			{
				DrdaException.ThrowSyntaxrm(SyntaxErrorCode.ChainOffErrorContinue);
			}
			if (this._dssChainedWithSameID)
			{
				this._previousCorrelationID = this._dssCorrelationID;
			}
			else
			{
				this._previousCorrelationID = -1;
			}
			this._dssLength -= 2;
			return this._dssCorrelationID;
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x000D4DC8 File Offset: 0x000D2FC8
		public async Task<CodePoint> ReadDdmObjectLengthAndCodePointAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(4, false, isAsync, cancellationToken);
			this._ddmObjectLength = (long)Converter.ToInt16(this._inputBuffer.Bytes, this._inputBuffer.Position);
			this._inputBuffer.Position += 2;
			CodePoint cp = (CodePoint)Converter.ToUInt16(this._inputBuffer.Bytes, this._inputBuffer.Position, EndianType.BigEndian);
			this._inputBuffer.Position += 2;
			if ((this._ddmObjectLength & 32768L) == 32768L)
			{
				this._ddmObjectLength &= 32767L;
				int numberOfBytesinExtendedLength = (int)this._ddmObjectLength - 4;
				int adjustSize = 0;
				await this.EnsureDataAvailableInBufferAsync(numberOfBytesinExtendedLength, false, isAsync, cancellationToken);
				switch (numberOfBytesinExtendedLength)
				{
				case 0:
					this._ddmObjectLength = 0L;
					adjustSize = 4;
					goto IL_02A7;
				case 4:
					this._ddmObjectLength = (long)Converter.ToInt32(this._inputBuffer.Bytes, this._inputBuffer.Position);
					this._inputBuffer.Position += 4;
					adjustSize = 8;
					goto IL_02A7;
				case 6:
					this._ddmObjectLength = Converter.ToInt48(this._inputBuffer.Bytes, this._inputBuffer.Position);
					this._inputBuffer.Position += 6;
					adjustSize = 10;
					goto IL_02A7;
				case 8:
					this._ddmObjectLength = Converter.ToInt64(this._inputBuffer.Bytes, this._inputBuffer.Position);
					this._inputBuffer.Position += 8;
					adjustSize = 12;
					goto IL_02A7;
				}
				DrdaException.ThrowSyntaxrm(SyntaxErrorCode.WrongExtendedLength);
				IL_02A7:
				this._dssLength -= adjustSize;
				for (int i = 0; i <= this._topDdmCollectionStack; i++)
				{
					this._ddmCollectionLenStack[i] -= (long)adjustSize;
				}
			}
			else
			{
				if (this._ddmObjectLength < 4L)
				{
					DrdaException.ThrowSyntaxrm(SyntaxErrorCode.ObjectLengthLessThan4);
				}
				this.AdjustLengths(4);
			}
			return cp;
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x000D4E20 File Offset: 0x000D3020
		private async Task ReadDssContinuationHeaderAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._inputBuffer.EnsureDataAvailableAsync(2, isAsync, cancellationToken);
			this._dssLength = (int)Converter.ToInt16(this._inputBuffer.Bytes, this._inputBuffer.Position);
			this._inputBuffer.Position += 2;
			if ((this._dssLength & 32768) == 32768)
			{
				this._dssLength = 32767;
				this._dssIsContinued = true;
			}
			else
			{
				this._dssIsContinued = false;
			}
			if (this._dssLength <= 2)
			{
				DrdaException.ThrowSyntaxrm(SyntaxErrorCode.DssContLessOrEqual_2);
			}
			this._dssLength -= 2;
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000D4E78 File Offset: 0x000D3078
		private async Task<bool> IsEXTDTANullAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._inputBuffer.EnsureDataAvailableAsync(1, isAsync, cancellationToken);
			this.AdjustLengths(1);
			Buffer inputBuffer = this._inputBuffer;
			Buffer inputBuffer2 = this._inputBuffer;
			int position = inputBuffer2.Position;
			inputBuffer2.Position = position + 1;
			return inputBuffer[position] > 0;
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000D4ED0 File Offset: 0x000D30D0
		private async Task<byte[]> GetExtDataAsync(long requiredLength, bool checkIfAvailable, bool isAsync, CancellationToken cancellationToken)
		{
			MemoryStream byteArrayStream;
			if (requiredLength != -1L)
			{
				byteArrayStream = new MemoryStream((int)requiredLength);
			}
			else
			{
				byteArrayStream = new MemoryStream();
				requiredLength = long.MaxValue;
			}
			bool flag = checkIfAvailable;
			if (flag)
			{
				flag = await this.IsEXTDTANullAsync(isAsync, cancellationToken);
			}
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				int copySize = (int)Math.Min((long)this._dssLength, requiredLength);
				bool readHeader;
				do
				{
					readHeader = this._dssIsContinued;
					await this._inputBuffer.EnsureDataAvailableAsync(copySize, isAsync, cancellationToken);
					this.AdjustLengths(copySize);
					byteArrayStream.Write(this._inputBuffer.Bytes, this._inputBuffer.Position, copySize);
					this._inputBuffer.Position += copySize;
					requiredLength -= (long)copySize;
					if (readHeader)
					{
						await this.ReadDssContinuationHeaderAsync(isAsync, cancellationToken);
					}
					copySize = (int)Math.Min((long)this._dssLength, requiredLength);
				}
				while (readHeader && requiredLength > 0L);
				array = byteArrayStream.ToArray();
			}
			return array;
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000D4F38 File Offset: 0x000D3138
		private async Task EnsureDataAvailableInBufferAsync(int required, bool adjustLength, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._inputBuffer.IsMemoryBuffer)
			{
				if (this._inputBuffer.Count - this._inputBuffer.Position < required)
				{
					DrdaException.BadObjectLength(CodePoint.QRYDTA);
				}
			}
			else
			{
				await this._inputBuffer.EnsureDataAvailableAsync(required, isAsync, cancellationToken);
				if (this._dssIsContinued && required > this._dssLength)
				{
					this.RemoveContinuationHeaders((required - this._dssLength) / 32767 + 1);
				}
				if (adjustLength)
				{
					this.AdjustLengths(required);
				}
			}
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x000D4FA0 File Offset: 0x000D31A0
		internal void AdjustLengths(int len)
		{
			this._ddmObjectLength -= (long)len;
			for (int i = 0; i <= this._topDdmCollectionStack; i++)
			{
				this._ddmCollectionLenStack[i] -= (long)len;
			}
			this._dssLength -= len;
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x000D4FF0 File Offset: 0x000D31F0
		private void RemoveContinuationHeaders(int numberOfDssContinuationHeaders)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < numberOfDssContinuationHeaders; i++)
			{
				if (i == 0)
				{
					num = this._inputBuffer.Position + this._dssLength;
				}
				else
				{
					num += 32767;
				}
			}
			for (int j = 0; j < numberOfDssContinuationHeaders; j++)
			{
				int num4 = (int)Converter.ToInt16(this._inputBuffer.Bytes, num);
				if (j == 0)
				{
					if ((num4 & 32768) == 32768)
					{
						num4 = 32767;
						this._dssIsContinued = true;
					}
					else
					{
						this._dssIsContinued = false;
					}
					num2 = 2;
				}
				else
				{
					if ((num4 & 32768) == 32768)
					{
						num4 = 32767;
					}
					else
					{
						DrdaException.ThrowSyntaxrm(SyntaxErrorCode.DssLengthByteNumberMismatch);
					}
					num2 += 2;
				}
				if (num4 <= 2)
				{
					DrdaException.ThrowSyntaxrm(SyntaxErrorCode.DssContLessOrEqual_2);
				}
				num3 += num4 - 2;
				int num5;
				if (j != numberOfDssContinuationHeaders - 1)
				{
					num5 = 32767;
				}
				else
				{
					num5 = this._dssLength;
				}
				num -= num5 - 2;
				Buffer.BlockCopy(this._inputBuffer.Bytes, num - num2, this._inputBuffer.Bytes, num, num5);
			}
			this._inputBuffer.Position = num;
			this._dssLength += num3;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x000D5118 File Offset: 0x000D3318
		public decimal ReadDecimal(int length, int precision, int scale)
		{
			return this.ReadDecimalAsync(length, precision, scale, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000D5144 File Offset: 0x000D3344
		public async Task<decimal> ReadDecimalAsync(int length, int precision, int scale, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(length, isAsync, cancellationToken);
			return this._converter.UnpackDecimal(array, precision, scale);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000D51B4 File Offset: 0x000D33B4
		public async Task<decimal> ReadZonedDecimalAsync(int length, int precision, int scale, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(length, isAsync, cancellationToken);
			return this._converter.UnpackZonedDecimal(array, precision, scale);
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000D5224 File Offset: 0x000D3424
		public async Task<decimal> ReadDecimalFloatAsync(int length, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(length, isAsync, cancellationToken);
			return this._converter.UnpackDecimalFloat(array);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000D5284 File Offset: 0x000D3484
		public TimeSpan ReadTime(int codePage)
		{
			return this.ReadTimeAsync(codePage, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000D52AC File Offset: 0x000D34AC
		public async Task<TimeSpan> ReadTimeAsync(int codePage, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(8, isAsync, cancellationToken);
			return this._converter.UnpackTime(array, codePage);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000D530C File Offset: 0x000D350C
		public async Task<string> ReadTimeDateTimeAsCharAsync(int codePage, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(8, isAsync, cancellationToken);
			return this._converter.UnpackDateDateTimeAsChar(array, codePage);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000D536C File Offset: 0x000D356C
		public DateTime ReadDate(int codePage)
		{
			return this.ReadDateAsync(codePage, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000D5394 File Offset: 0x000D3594
		public async Task<DateTime> ReadDateAsync(int codePage, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(10, isAsync, cancellationToken);
			return this._converter.UnpackDate(array, codePage);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000D53F4 File Offset: 0x000D35F4
		public async Task<string> ReadDateDateTimeAsCharAsync(int codePage, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(10, isAsync, cancellationToken);
			return this._converter.UnpackDateDateTimeAsChar(array, codePage);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000D5454 File Offset: 0x000D3654
		public DateTime ReadTimestamp(int codePage, ref bool succeed)
		{
			Tuple<DateTime, bool> result = this.ReadTimestampAsync(codePage, succeed, false, CancellationToken.None).GetAwaiter().GetResult();
			succeed = result.Item2;
			return result.Item1;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000D548C File Offset: 0x000D368C
		public async Task<Tuple<DateTime, bool>> ReadTimestampAsync(int codePage, bool succeed, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(26, isAsync, cancellationToken);
			bool flag = succeed;
			return new Tuple<DateTime, bool>(this._converter.UnpackTimestamp(array, codePage, ref flag), flag);
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000D54F4 File Offset: 0x000D36F4
		public DateTime ReadTimestamp(int codePage)
		{
			return this.ReadTimestampAsync(codePage, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000D551C File Offset: 0x000D371C
		public async Task<DateTime> ReadTimestampAsync(int codePage, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(26, isAsync, cancellationToken);
			return this._converter.UnpackTimestamp(array, codePage);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000D557C File Offset: 0x000D377C
		public async Task<DateTime> ReadTimestampAsync(int codePage, int length, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(length, isAsync, cancellationToken);
			return this._converter.UnpackTimestamp(array, codePage);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000D55E4 File Offset: 0x000D37E4
		public async Task<string> ReadTimestampDateTimeAsCharAsync(int codePage, int length, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(length, isAsync, cancellationToken);
			return this._converter.UnpackTimestampDateTimeAsChar(array, codePage);
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000D564C File Offset: 0x000D384C
		public object ReadFloat(int length, string typDefname)
		{
			return this.ReadFloatAsync(length, typDefname, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000D5674 File Offset: 0x000D3874
		public async Task<object> ReadFloatAsync(int length, string typDefname, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(length, isAsync, cancellationToken);
			return this._converter.UnpackFloat(array, typDefname);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000D56DC File Offset: 0x000D38DC
		public object ReadDouble(int length, string typDefname)
		{
			return this.ReadDoubleAsync(length, typDefname, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000D5704 File Offset: 0x000D3904
		public async Task<object> ReadDoubleAsync(int length, string typDefname, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await this.ReadBytesAsync(length, isAsync, cancellationToken);
			return this._converter.UnpackDouble(array, typDefname);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000D576C File Offset: 0x000D396C
		public string ReadDSString(int byteCount, int encoding, int drdaType, EndianType endian)
		{
			return this.ReadDSStringAsync(byteCount, encoding, drdaType, endian, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000D5798 File Offset: 0x000D3998
		public async Task<string> ReadDSStringAsync(int byteCount, int encoding, int drdaType, EndianType endian, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnsureDataAvailableInBufferAsync(byteCount, true, isAsync, cancellationToken);
			string text = null;
			byte[] array = new byte[byteCount];
			Buffer.BlockCopy(this._inputBuffer.Bytes, this._inputBuffer.Position, array, 0, byteCount);
			if (endian == EndianType.LittleEndian)
			{
				this.SwapBytesForPackedString(ref array);
			}
			this._converter.UnpackDSString(array, 0, byteCount, encoding, ref text, (DrdaTypes)drdaType);
			this._inputBuffer.Position += byteCount;
			return text;
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000D5810 File Offset: 0x000D3A10
		public string ReadDSString(int byteCount, int encoding, int drdaType)
		{
			return this.ReadDSString(byteCount, encoding, drdaType, EndianType.BigEndian);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000D581C File Offset: 0x000D3A1C
		public Task<string> ReadDSStringAsync(int byteCount, int encoding, int drdaType, bool isAsync, CancellationToken cancellationToken)
		{
			return this.ReadDSStringAsync(byteCount, encoding, drdaType, EndianType.BigEndian, isAsync, cancellationToken);
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000D582C File Offset: 0x000D3A2C
		private void SwapBytesForPackedString(ref byte[] bytes)
		{
			if (bytes.Length % 2 != 0)
			{
				throw new Exception("String bytes not right");
			}
			for (int i = 0; i < bytes.Length; i += 2)
			{
				byte b = bytes[i];
				bytes[i] = bytes[i + 1];
				bytes[i + 1] = b;
			}
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000D5874 File Offset: 0x000D3A74
		public byte[] ReadLDBytes(ref int readLen)
		{
			readLen = 0;
			Tuple<byte[], int> result = this.ReadLDBytesAsync(false, CancellationToken.None).GetAwaiter().GetResult();
			if (result == null)
			{
				return null;
			}
			readLen = result.Item2;
			return result.Item1;
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000D58B4 File Offset: 0x000D3AB4
		public async Task<Tuple<byte[], int>> ReadLDBytesAsync(bool isAsync, CancellationToken cancellationToken)
		{
			short num = await this.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
			int readLen = (int)num;
			Tuple<byte[], int> tuple;
			if (readLen > 0)
			{
				TaskAwaiter<byte[]> taskAwaiter = this.ReadBytesAsync(readLen, isAsync, cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<byte[]> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<byte[]>);
				}
				tuple = new Tuple<byte[], int>(taskAwaiter.GetResult(), readLen);
			}
			else
			{
				tuple = null;
			}
			return tuple;
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000D590C File Offset: 0x000D3B0C
		public object ReadLDStringDBCS(ref int readLen, int encoding, int drdaType, EndianType endianType)
		{
			readLen = 0;
			Tuple<object, int> result = this.ReadLDStringDBCSAsync(encoding, drdaType, endianType, false, CancellationToken.None).GetAwaiter().GetResult();
			if (result == null)
			{
				return string.Empty;
			}
			readLen = result.Item2;
			return result.Item1;
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000D5954 File Offset: 0x000D3B54
		public async Task<Tuple<object, int>> ReadLDStringDBCSAsync(int encoding, int drdaType, EndianType endianType, bool isAsync, CancellationToken cancellationToken)
		{
			short num = await this.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
			int readLen = (int)num;
			Tuple<object, int> tuple;
			if (readLen > 0)
			{
				TaskAwaiter<string> taskAwaiter = this.ReadDSStringAsync(readLen * 2, encoding, drdaType, endianType, isAsync, cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<string> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<string>);
				}
				tuple = new Tuple<object, int>(taskAwaiter.GetResult(), readLen);
			}
			else
			{
				tuple = null;
			}
			return tuple;
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000D59C4 File Offset: 0x000D3BC4
		public CodePoint PeekCodePoint()
		{
			BitConverter.ToInt16(this.GetBytes(10), 8);
			CodePoint codePoint = CodePoint.UNKNOWN;
			if (Enum.TryParse<CodePoint>(codePoint.ToString(), out codePoint))
			{
				return codePoint;
			}
			return CodePoint.UNKNOWN;
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000D59FC File Offset: 0x000D3BFC
		public CodePoint MoveToNextDdm()
		{
			return this.MoveToNextDdmAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000D5A24 File Offset: 0x000D3C24
		public async Task<CodePoint> MoveToNextDdmAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._inputBuffer.EnsureDataAvailableAsync(3, isAsync, cancellationToken);
			if (this.GetBytes(3)[2] == 208)
			{
				if (!isAsync)
				{
					IEnumerator<ObjectInfo> enumerator = this.ReadDssObjects().GetEnumerator();
					enumerator.MoveNext();
					ObjectInfo objectInfo = enumerator.Current;
					return objectInfo.Codepoint;
				}
				IEnumerator<Task<ObjectInfo>> taskEnumerator = this.ReadDssObjectsAsync(cancellationToken).GetEnumerator();
				if (taskEnumerator.MoveNext())
				{
					await taskEnumerator.Current;
					return taskEnumerator.Current.Result.Codepoint;
				}
				taskEnumerator = null;
			}
			return CodePoint.UNKNOWN;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000D5A79 File Offset: 0x000D3C79
		public void SetMemeoryBuffer(byte[] memoryBuffer)
		{
			this._inputBuffer.SetMemoryBuffer(memoryBuffer);
		}

		// Token: 0x04002BB8 RID: 11192
		private Buffer _inputBuffer;

		// Token: 0x04002BB9 RID: 11193
		private long[] _ddmCollectionLenStack;

		// Token: 0x04002BBA RID: 11194
		private int _topDdmCollectionStack = -1;

		// Token: 0x04002BBB RID: 11195
		private long _ddmObjectLength;

		// Token: 0x04002BBC RID: 11196
		private int _dssLength;

		// Token: 0x04002BBD RID: 11197
		private int _dssOriginalPosition;

		// Token: 0x04002BBE RID: 11198
		private int _dssOriginalLength;

		// Token: 0x04002BBF RID: 11199
		private bool _dssIsContinued;

		// Token: 0x04002BC0 RID: 11200
		private bool _terminateOnError;

		// Token: 0x04002BC1 RID: 11201
		private bool _dssChainedWithSameID;

		// Token: 0x04002BC2 RID: 11202
		private bool _dssChainedWithDiffID;

		// Token: 0x04002BC3 RID: 11203
		private int _dssCorrelationID = -1;

		// Token: 0x04002BC4 RID: 11204
		private int _previousCorrelationID = -1;

		// Token: 0x04002BC5 RID: 11205
		private byte _dssFormat;

		// Token: 0x04002BC6 RID: 11206
		private ICcsidManager _ccsidManager;

		// Token: 0x04002BC7 RID: 11207
		private Ccsid _ccsid;

		// Token: 0x04002BC8 RID: 11208
		private Stream _stream;

		// Token: 0x04002BC9 RID: 11209
		private bool _isReadingDDMObject;

		// Token: 0x04002BCA RID: 11210
		private Converter _converter;

		// Token: 0x04002BCB RID: 11211
		private bool _isManagedMsDrda;

		// Token: 0x04002BCC RID: 11212
		private object _tracePoint;

		// Token: 0x04002BCD RID: 11213
		private EndianType _enddianType = EndianType.LittleEndian;

		// Token: 0x04002BCE RID: 11214
		private CommonDrdaPerformanceCountersContainer CommonPerformanceContainer;
	}
}
