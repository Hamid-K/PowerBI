using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000B9 RID: 185
	internal class TdsParserStateObject
	{
		// Token: 0x06000CEB RID: 3307 RVA: 0x0002774C File Offset: 0x0002594C
		internal TdsParserStateObject(TdsParser parser)
		{
			this._parser = parser;
			this._onTimeoutAsync = new TimerCallback(this.OnTimeoutAsync);
			this.SetPacketSize(4096);
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = new LastIOTimer();
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002783D File Offset: 0x00025A3D
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00027845 File Offset: 0x00025A45
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x0002784D File Offset: 0x00025A4D
		internal bool BcpLock
		{
			get
			{
				return this._bcpLock;
			}
			set
			{
				this._bcpLock = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00027858 File Offset: 0x00025A58
		internal bool IsOrphaned
		{
			get
			{
				object obj;
				bool flag = this._owner.TryGetTarget(out obj);
				return this._activateCount != 0 && !flag;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00027884 File Offset: 0x00025A84
		internal object Owner
		{
			set
			{
				SqlDataReader sqlDataReader = value as SqlDataReader;
				if (sqlDataReader != null)
				{
					this._readerState = sqlDataReader._sharedState;
				}
				else
				{
					this._readerState = null;
				}
				this._owner.SetTarget(value);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000278BC File Offset: 0x00025ABC
		internal bool HasOwner
		{
			get
			{
				object obj;
				return this._owner.TryGetTarget(out obj);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000278D6 File Offset: 0x00025AD6
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000278DE File Offset: 0x00025ADE
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x000278E6 File Offset: 0x00025AE6
		internal SniContext SniContext
		{
			get
			{
				return this._sniContext;
			}
			set
			{
				this._sniContext = value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000278EF File Offset: 0x00025AEF
		internal bool TimeoutHasExpired
		{
			get
			{
				return TdsParserStaticMethods.TimeoutHasExpired(this._timeoutTime);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000278FC File Offset: 0x00025AFC
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00027925 File Offset: 0x00025B25
		internal long TimeoutTime
		{
			get
			{
				if (this._timeoutMilliseconds != 0L)
				{
					this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
					this._timeoutMilliseconds = 0L;
				}
				return this._timeoutTime;
			}
			set
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = value;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00027938 File Offset: 0x00025B38
		internal int GetTimeoutRemaining()
		{
			int num;
			if (this._timeoutMilliseconds != 0L)
			{
				num = (int)Math.Min(2147483647L, this._timeoutMilliseconds);
				this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
				this._timeoutMilliseconds = 0L;
			}
			else
			{
				num = TdsParserStaticMethods.GetTimeoutMilliseconds(this._timeoutTime);
			}
			return num;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00027988 File Offset: 0x00025B88
		internal bool TryStartNewRow(bool isNullCompressed, int nullBitmapColumnsCount = 0)
		{
			TdsParserStateObject.StateSnapshot snapshot = this._snapshot;
			if (snapshot != null)
			{
				snapshot.CloneNullBitmapInfo();
			}
			if (isNullCompressed)
			{
				if (!this._nullBitmapInfo.TryInitialize(this, nullBitmapColumnsCount))
				{
					return false;
				}
			}
			else
			{
				this._nullBitmapInfo.Clean();
			}
			return true;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000279BC File Offset: 0x00025BBC
		internal bool IsRowTokenReady()
		{
			int num = Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed) - 1;
			if (num > 0)
			{
				if (this._inBuff[this._inBytesUsed] == 209)
				{
					return true;
				}
				if (this._inBuff[this._inBytesUsed] == 210)
				{
					int num2 = 1 + (this._cleanupMetaData.Length + 7) / 8;
					return num2 <= num;
				}
			}
			return false;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00027A2D File Offset: 0x00025C2D
		internal bool IsNullCompressionBitSet(int columnOrdinal)
		{
			return this._nullBitmapInfo.IsGuaranteedNull(columnOrdinal);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00027A3C File Offset: 0x00025C3C
		internal void Activate(object owner)
		{
			this.Owner = owner;
			int num = Interlocked.Increment(ref this._activateCount);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00027A5C File Offset: 0x00025C5C
		internal void CancelRequest()
		{
			this.ResetBuffer();
			this.ResetPacketCounters();
			if (!this._bulkCopyWriteTimeout)
			{
				this.SendAttention(false, false);
				this.Parser.ProcessPendingAck(this);
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00027A88 File Offset: 0x00025C88
		public void CheckSetResetConnectionState(uint error, CallbackType callbackType)
		{
			if (this._fResetEventOwned)
			{
				if (callbackType == CallbackType.Read && error == 0U)
				{
					this._parser._fResetConnection = false;
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
				if (error != 0U)
				{
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00027AFE File Offset: 0x00025CFE
		internal void CloseSession()
		{
			this.ResetCancelAndProcessAttention();
			this.Parser.PutSession(this);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00027B14 File Offset: 0x00025D14
		internal bool Deactivate()
		{
			bool flag = false;
			try
			{
				TdsParserState state = this.Parser.State;
				if (state != TdsParserState.Broken && state != TdsParserState.Closed)
				{
					if (this.HasPendingData)
					{
						this.Parser.DrainData(this);
					}
					if (this.HasOpenResult)
					{
						this.DecrementOpenResultCount();
					}
					this.ResetCancelAndProcessAttention();
					flag = true;
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(ex);
			}
			return flag;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00027B88 File Offset: 0x00025D88
		internal void RemoveOwner()
		{
			if (this._parser.MARSOn)
			{
				int num = Interlocked.Decrement(ref this._activateCount);
			}
			this.Owner = null;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00027BB8 File Offset: 0x00025DB8
		internal void DecrementOpenResultCount()
		{
			if (this._executedUnderTransaction == null)
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("TdsParserStateObject.DecrementOpenResultCount | INFO | State Object Id {0}, Processing Attention.", this._objectID);
				this._parser.DecrementNonTransactedOpenResultCount();
			}
			else
			{
				this._executedUnderTransaction.DecrementAndObtainOpenResultCount();
				this._executedUnderTransaction = null;
			}
			this.HasOpenResult = false;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00027C0C File Offset: 0x00025E0C
		internal void DisposeCounters()
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				this._networkPacketTimeout = null;
				networkPacketTimeout.Dispose();
			}
			if (Volatile.Read(ref this._readingCount) > 0)
			{
				SpinWait.SpinUntil(() => Volatile.Read(ref this._readingCount) == 0);
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00027C4F File Offset: 0x00025E4F
		internal int IncrementAndObtainOpenResultCount(SqlInternalTransaction transaction)
		{
			this.HasOpenResult = true;
			if (transaction == null)
			{
				return this._parser.IncrementNonTransactedOpenResultCount();
			}
			this._executedUnderTransaction = transaction;
			return transaction.IncrementAndObtainOpenResultCount();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00027C74 File Offset: 0x00025E74
		internal void SetTimeoutSeconds(int timeout)
		{
			this.SetTimeoutMilliseconds((long)timeout * 1000L);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00027C85 File Offset: 0x00025E85
		internal void SetTimeoutMilliseconds(long timeout)
		{
			if (timeout <= 0L)
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = long.MaxValue;
				return;
			}
			this._timeoutMilliseconds = timeout;
			this._timeoutTime = 0L;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00027CB3 File Offset: 0x00025EB3
		internal void ThrowExceptionAndWarning(bool callerHasConnectionLock = false, bool asyncClose = false)
		{
			this._parser.ThrowExceptionAndWarning(this, callerHasConnectionLock, asyncClose);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00027CC4 File Offset: 0x00025EC4
		internal Task ExecuteFlush()
		{
			Task task2;
			lock (this)
			{
				if (this._cancelled && 1 == this._outputPacketNumber)
				{
					this.ResetBuffer();
					this._cancelled = false;
					throw SQL.OperationCancelled();
				}
				Task task = this.WritePacket(1, false);
				if (task == null)
				{
					this.HasPendingData = true;
					this._messageStatus = 0;
					task2 = null;
				}
				else
				{
					task2 = AsyncHelper.CreateContinuationTaskWithState(task, this, delegate(object state)
					{
						TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)state;
						tdsParserStateObject.HasPendingData = true;
						tdsParserStateObject._messageStatus = 0;
					}, null);
				}
			}
			return task2;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00027D64 File Offset: 0x00025F64
		internal bool TryProcessHeader()
		{
			if (this._partialHeaderBytesRead > 0 || this._inBytesUsed + this._inputHeaderLen > this._inBytesRead)
			{
				for (;;)
				{
					int num = Math.Min(this._inBytesRead - this._inBytesUsed, this._inputHeaderLen - this._partialHeaderBytesRead);
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, this._partialHeaderBuffer, this._partialHeaderBytesRead, num);
					this._partialHeaderBytesRead += num;
					this._inBytesUsed += num;
					if (this._partialHeaderBytesRead == this._inputHeaderLen)
					{
						this._partialHeaderBytesRead = 0;
						this._inBytesPacket = (((int)this._partialHeaderBuffer[2] << 8) | (int)this._partialHeaderBuffer[3]) - this._inputHeaderLen;
						this._messageStatus = this._partialHeaderBuffer[1];
						this._spid = ((int)this._partialHeaderBuffer[4] << 8) | (int)this._partialHeaderBuffer[5];
						SqlClientEventSource log = SqlClientEventSource.Log;
						string text = "TdsParserStateObject.TryProcessHeader | ADV | State Object Id {0}, Client Connection Id {1}, Server process Id (SPID) {2}";
						int objectID = this._objectID;
						TdsParser parser = this._parser;
						Guid? guid;
						if (parser == null)
						{
							guid = null;
						}
						else
						{
							SqlInternalConnectionTds connection = parser.Connection;
							guid = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null);
						}
						log.TryAdvancedTraceEvent<int, Guid?, int>(text, objectID, guid, this._spid);
					}
					else
					{
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							break;
						}
						if (!this.TryReadNetworkPacket())
						{
							return false;
						}
						if (this.IsTimeoutStateExpired)
						{
							goto Block_7;
						}
					}
					if (this._partialHeaderBytesRead == 0)
					{
						goto Block_8;
					}
				}
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_7:
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_8:;
			}
			else
			{
				this._messageStatus = this._inBuff[this._inBytesUsed + 1];
				this._inBytesPacket = (((int)this._inBuff[this._inBytesUsed + 2] << 8) | (int)this._inBuff[this._inBytesUsed + 2 + 1]) - this._inputHeaderLen;
				this._spid = ((int)this._inBuff[this._inBytesUsed + 4] << 8) | (int)this._inBuff[this._inBytesUsed + 4 + 1];
				this._inBytesUsed += this._inputHeaderLen;
			}
			if (this._inBytesPacket < 0)
			{
				throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
			}
			return true;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00027F80 File Offset: 0x00026180
		internal bool TryPrepareBuffer()
		{
			if (this._inBytesPacket == 0 && this._inBytesUsed < this._inBytesRead && !this.TryProcessHeader())
			{
				return false;
			}
			if (this._inBytesUsed == this._inBytesRead)
			{
				if (this._inBytesPacket > 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
				}
				else if (this._inBytesPacket == 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
					if (!this.TryProcessHeader())
					{
						return false;
					}
					if (this._inBytesUsed == this._inBytesRead && !this.TryReadNetworkPacket())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00028003 File Offset: 0x00026203
		internal void ResetBuffer()
		{
			this._outBytesUsed = this._outputHeaderLen;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00028011 File Offset: 0x00026211
		internal void ResetPacketCounters()
		{
			this._outputPacketNumber = 1;
			this._outputPacketCount = 0U;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00028024 File Offset: 0x00026224
		internal bool SetPacketSize(int size)
		{
			if (size > 32768)
			{
				throw SQL.InvalidPacketSize();
			}
			if (this._inBuff == null || this._inBuff.Length != size)
			{
				if (this._inBuff == null)
				{
					this._inBuff = new byte[size];
					this._inBytesRead = 0;
					this._inBytesUsed = 0;
				}
				else if (size != this._inBuff.Length)
				{
					if (this._inBytesRead > this._inBytesUsed)
					{
						byte[] inBuff = this._inBuff;
						this._inBuff = new byte[size];
						int num = this._inBytesRead - this._inBytesUsed;
						if (inBuff.Length < this._inBytesUsed + num || this._inBuff.Length < num)
						{
							string text = string.Concat(new string[]
							{
								StringsHelper.GetString(Strings.SQL_InvalidInternalPacketSize, Array.Empty<object>()),
								" ",
								inBuff.Length.ToString(),
								", ",
								this._inBytesUsed.ToString(),
								", ",
								num.ToString(),
								", ",
								this._inBuff.Length.ToString()
							});
							throw SQL.InvalidInternalPacketSize(text);
						}
						Buffer.BlockCopy(inBuff, this._inBytesUsed, this._inBuff, 0, num);
						this._inBytesRead -= this._inBytesUsed;
						this._inBytesUsed = 0;
					}
					else
					{
						this._inBuff = new byte[size];
						this._inBytesRead = 0;
						this._inBytesUsed = 0;
					}
				}
				this._outBuff = new byte[size];
				this._outBytesUsed = this._outputHeaderLen;
				return true;
			}
			return false;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000281B8 File Offset: 0x000263B8
		internal TdsParserStateObject(TdsParser parser, SNIHandle physicalConnection, bool async)
		{
			this._parser = parser;
			this.SniContext = SniContext.Snix_GetMarsSession;
			this.SetPacketSize(this._parser._physicalStateObj._outBuff.Length);
			SNINativeMethodWrapper.ConsumerInfo consumerInfo = this.CreateConsumerInfo(async);
			SQLDNSInfo sqldnsinfo;
			SQLFallbackDNSCache.Instance.GetDNSInfo(this._parser.FQDNforDNSCache, out sqldnsinfo);
			this._sessionHandle = new SNIHandle(consumerInfo, physicalConnection, this._parser.Connection.ConnectionOptions.IPAddressPreference, sqldnsinfo);
			if (this._sessionHandle.Status != 0U)
			{
				this.AddError(parser.ProcessSNIError(this));
				this.ThrowExceptionAndWarning(false, false);
			}
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = parser._physicalStateObj._lastSuccessfulIOTimer;
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00028317 File Offset: 0x00026517
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0002831F File Offset: 0x0002651F
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00028327 File Offset: 0x00026527
		internal bool HasOpenResult
		{
			get
			{
				return this._hasOpenResult;
			}
			set
			{
				this._hasOpenResult = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00028330 File Offset: 0x00026530
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00028338 File Offset: 0x00026538
		internal bool HasPendingData
		{
			get
			{
				return this._pendingData;
			}
			set
			{
				this._pendingData = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00028341 File Offset: 0x00026541
		internal uint Status
		{
			get
			{
				if (this._sessionHandle != null)
				{
					return this._sessionHandle.Status;
				}
				return uint.MaxValue;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00028358 File Offset: 0x00026558
		internal void Cancel(int objectID)
		{
			bool flag = false;
			try
			{
				while (!flag && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
				{
					Monitor.TryEnter(this, 100, ref flag);
					if (flag && !this._cancelled && objectID == this._allowObjectID && objectID != -1)
					{
						this._cancelled = true;
						if (this._pendingData && !this._attentionSent)
						{
							bool flag2 = false;
							while (!flag2 && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
							{
								try
								{
									this._parser.Connection._parserLock.Wait(false, 100, ref flag2);
									if (flag2)
									{
										this._parser.Connection.ThreadHasParserLockForClose = true;
										this.SendAttention(false, false);
									}
								}
								finally
								{
									if (flag2)
									{
										if (this._parser.Connection.ThreadHasParserLockForClose)
										{
											this._parser.Connection.ThreadHasParserLockForClose = false;
										}
										this._parser.Connection._parserLock.Release();
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000284B8 File Offset: 0x000266B8
		private void ResetCancelAndProcessAttention()
		{
			lock (this)
			{
				this._cancelled = false;
				this._allowObjectID = -1;
				if (this._attentionSent)
				{
					this.Parser.ProcessPendingAck(this);
				}
				this.SetTimeoutStateStopped();
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002851C File Offset: 0x0002671C
		private SNINativeMethodWrapper.ConsumerInfo CreateConsumerInfo(bool async)
		{
			SNINativeMethodWrapper.ConsumerInfo consumerInfo = default(SNINativeMethodWrapper.ConsumerInfo);
			consumerInfo.defaultBufferSize = this._outBuff.Length;
			if (async)
			{
				consumerInfo.readDelegate = SNILoadHandle.SingletonInstance.ReadAsyncCallbackDispatcher;
				consumerInfo.writeDelegate = SNILoadHandle.SingletonInstance.WriteAsyncCallbackDispatcher;
				this._gcHandle = GCHandle.Alloc(this, GCHandleType.Normal);
				consumerInfo.key = (IntPtr)this._gcHandle;
			}
			return consumerInfo;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00028588 File Offset: 0x00026788
		internal void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, byte[] spnBuffer, bool flushCache, bool async, bool fParallel, TransparentNetworkResolutionState transparentNetworkResolutionState, int totalTimeout, SqlConnectionIPAddressPreference ipPreference, string cachedFQDN, string hostNameInCertificate = "")
		{
			SNINativeMethodWrapper.ConsumerInfo consumerInfo = this.CreateConsumerInfo(async);
			long num;
			if (9223372036854775807L == timerExpire)
			{
				num = 2147483647L;
			}
			else
			{
				num = ADP.TimerRemainingMilliseconds(timerExpire);
				if (num > 2147483647L)
				{
					num = 2147483647L;
				}
				else if (0L > num)
				{
					num = 0L;
				}
			}
			SQLDNSInfo sqldnsinfo;
			SQLFallbackDNSCache.Instance.GetDNSInfo(cachedFQDN, out sqldnsinfo);
			this._sessionHandle = new SNIHandle(consumerInfo, serverName, spnBuffer, ignoreSniOpenTimeout, checked((int)num), out instanceName, flushCache, !async, fParallel, transparentNetworkResolutionState, totalTimeout, ipPreference, sqldnsinfo, hostNameInCertificate);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002860C File Offset: 0x0002680C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int DecrementPendingCallbacks(bool release)
		{
			int num = Interlocked.Decrement(ref this._pendingCallbacks);
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParserStateObject.DecrementPendingCallbacks|ADV> {0}, after decrementing _pendingCallbacks: {1}", this.ObjectID, this._pendingCallbacks);
			if ((num == 0 || release) && this._gcHandle.IsAllocated)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.TdsParserStateObject.DecrementPendingCallbacks|ADV> {0}, FREEING HANDLE!", this.ObjectID);
				this._gcHandle.Free();
			}
			return num;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00028678 File Offset: 0x00026878
		internal void Dispose()
		{
			SafeHandle sniPacket = this._sniPacket;
			SafeHandle sessionHandle = this._sessionHandle;
			SafeHandle sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
			this.DisposeCounters();
			if (sessionHandle != null || sniPacket != null)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (sniPacket != null)
					{
						sniPacket.Dispose();
					}
					if (sniAsyncAttnPacket != null)
					{
						sniAsyncAttnPacket.Dispose();
					}
					if (sessionHandle != null)
					{
						sessionHandle.Dispose();
						this.DecrementPendingCallbacks(true);
					}
				}
			}
			if (this._writePacketCache != null)
			{
				object writePacketLockObject = this._writePacketLockObject;
				lock (writePacketLockObject)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						this._writePacketCache.Dispose();
					}
				}
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00028748 File Offset: 0x00026948
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int IncrementPendingCallbacks()
		{
			int num = Interlocked.Increment(ref this._pendingCallbacks);
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParserStateObject.IncrementPendingCallbacks|ADV> {0}, after incrementing _pendingCallbacks: {1}", this.ObjectID, this._pendingCallbacks);
			return num;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002877D File Offset: 0x0002697D
		internal void StartSession(int objectID)
		{
			this._allowObjectID = objectID;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00028788 File Offset: 0x00026988
		internal bool TryPeekByte(out byte value)
		{
			if (!this.TryReadByte(out value))
			{
				return false;
			}
			this._inBytesPacket++;
			this._inBytesUsed--;
			return true;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000287B4 File Offset: 0x000269B4
		public bool TryReadByteArray(Span<byte> buff, int len)
		{
			int num;
			return this.TryReadByteArray(buff, len, out num);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000287CC File Offset: 0x000269CC
		public bool TryReadByteArray(Span<byte> buff, int len, out int totalRead)
		{
			totalRead = 0;
			while (len > 0)
			{
				if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
				{
					return false;
				}
				int num = Math.Min(len, Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed));
				if (!buff.IsEmpty)
				{
					ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(this._inBuff, this._inBytesUsed, num);
					Span<byte> span = buff.Slice(totalRead, num);
					readOnlySpan.CopyTo(span);
				}
				totalRead += num;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
				len -= num;
			}
			return true;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00028880 File Offset: 0x00026A80
		internal bool TryReadByte(out byte value)
		{
			value = 0;
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				return false;
			}
			this._inBytesPacket--;
			byte[] inBuff = this._inBuff;
			int inBytesUsed = this._inBytesUsed;
			this._inBytesUsed = inBytesUsed + 1;
			value = inBuff[inBytesUsed];
			return true;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000288DC File Offset: 0x00026ADC
		internal bool TryReadChar(out char value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 2))
				{
					value = '\0';
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (char)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00028960 File Offset: 0x00026B60
		internal bool TryReadInt16(out short value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (short)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000289E4 File Offset: 0x00026BE4
		internal bool TryReadInt32(out int value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 4))
			{
				value = 0;
				return false;
			}
			value = BitConverter.ToInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00028A64 File Offset: 0x00026C64
		internal bool TryReadInt64(out long value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0L;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToInt64(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			int num;
			if (!this.TryReadByteArray(MemoryExtensions.AsSpan<byte>(this._bTmp, this._bTmpRead), 8 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0L;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToInt64(this._bTmp, 0);
			return true;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00028B38 File Offset: 0x00026D38
		internal bool TryReadUInt16(out ushort value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (ushort)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00028BBC File Offset: 0x00026DBC
		internal bool TryReadUInt32(out uint value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0U;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToUInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			int num;
			if (!this.TryReadByteArray(MemoryExtensions.AsSpan<byte>(this._bTmp, this._bTmpRead), 4 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0U;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToUInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00028C8C File Offset: 0x00026E8C
		internal bool TryReadSingle(out float value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToSingle(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 4))
			{
				value = 0f;
				return false;
			}
			value = BitConverter.ToSingle(this._bTmp, 0);
			return true;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00028D10 File Offset: 0x00026F10
		internal bool TryReadDouble(out double value)
		{
			if (this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToDouble(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 8))
			{
				value = 0.0;
				return false;
			}
			value = BitConverter.ToDouble(this._bTmp, 0);
			return true;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00028D98 File Offset: 0x00026F98
		internal bool TryReadString(int length, out string value)
		{
			int num = length << 1;
			int num2 = 0;
			byte[] array;
			if (this._inBytesUsed + num > this._inBytesRead || this._inBytesPacket < num)
			{
				if (this._bTmp == null || this._bTmp.Length < num)
				{
					this._bTmp = new byte[num];
				}
				if (!this.TryReadByteArray(this._bTmp, num))
				{
					value = null;
					return false;
				}
				array = this._bTmp;
			}
			else
			{
				array = this._inBuff;
				num2 = this._inBytesUsed;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
			}
			value = Encoding.Unicode.GetString(array, num2, num);
			return true;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00028E40 File Offset: 0x00027040
		internal bool TryReadStringWithEncoding(int length, Encoding encoding, bool isPlp, out string value)
		{
			if (encoding == null)
			{
				if (isPlp)
				{
					ulong num;
					if (!this._parser.TrySkipPlpValue((ulong)((long)length), this, out num))
					{
						value = null;
						return false;
					}
				}
				else if (!this.TrySkipBytes(length))
				{
					value = null;
					return false;
				}
				this._parser.ThrowUnsupportedCollationEncountered(this);
			}
			byte[] array = null;
			int num2 = 0;
			if (isPlp)
			{
				if (!this.TryReadPlpBytes(ref array, 0, 2147483647, out length))
				{
					value = null;
					return false;
				}
			}
			else if (this._inBytesUsed + length > this._inBytesRead || this._inBytesPacket < length)
			{
				if (this._bTmp == null || this._bTmp.Length < length)
				{
					this._bTmp = new byte[length];
				}
				if (!this.TryReadByteArray(this._bTmp, length))
				{
					value = null;
					return false;
				}
				array = this._bTmp;
			}
			else
			{
				array = this._inBuff;
				num2 = this._inBytesUsed;
				this._inBytesUsed += length;
				this._inBytesPacket -= length;
			}
			value = encoding.GetString(array, num2, length);
			return true;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00028F3C File Offset: 0x0002713C
		internal ulong ReadPlpLength(bool returnPlpNullIfNull)
		{
			ulong num;
			if (!this.TryReadPlpLength(returnPlpNullIfNull, out num))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00028F60 File Offset: 0x00027160
		internal bool TryReadPlpLength(bool returnPlpNullIfNull, out ulong lengthLeft)
		{
			bool flag = false;
			if (this._longlen == 0UL)
			{
				long num;
				if (!this.TryReadInt64(out num))
				{
					lengthLeft = 0UL;
					return false;
				}
				this._longlen = (ulong)num;
			}
			if (this._longlen == 18446744073709551615UL)
			{
				this._longlen = 0UL;
				this._longlenleft = 0UL;
				flag = true;
			}
			else
			{
				uint num2;
				if (!this.TryReadUInt32(out num2))
				{
					lengthLeft = 0UL;
					return false;
				}
				if (num2 == 0U)
				{
					this._longlenleft = 0UL;
					this._longlen = 0UL;
				}
				else
				{
					this._longlenleft = (ulong)num2;
				}
			}
			if (flag && returnPlpNullIfNull)
			{
				lengthLeft = ulong.MaxValue;
				return true;
			}
			lengthLeft = this._longlenleft;
			return true;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00028FF0 File Offset: 0x000271F0
		internal int ReadPlpBytesChunk(byte[] buff, int offset, int len)
		{
			int num = (int)Math.Min(this._longlenleft, (ulong)((long)len));
			int num2;
			bool flag = this.TryReadByteArray(MemoryExtensions.AsSpan<byte>(buff, offset), num, out num2);
			this._longlenleft -= (ulong)((long)num);
			if (!flag)
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num2;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00029038 File Offset: 0x00027238
		internal bool TryReadPlpBytes(ref byte[] buff, int offst, int len, out int totalBytesRead)
		{
			if (this._longlen == 0UL)
			{
				if (buff == null)
				{
					buff = new byte[0];
				}
				totalBytesRead = 0;
				return true;
			}
			int i = len;
			if (buff == null && this._longlen != 18446744073709551614UL)
			{
				buff = new byte[Math.Min((int)this._longlen, len)];
			}
			if (this._longlenleft == 0UL)
			{
				ulong num;
				if (!this.TryReadPlpLength(false, out num))
				{
					totalBytesRead = 0;
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					totalBytesRead = 0;
					return true;
				}
			}
			if (buff == null)
			{
				buff = new byte[this._longlenleft];
			}
			totalBytesRead = 0;
			while (i > 0)
			{
				int num2 = (int)Math.Min(this._longlenleft, (ulong)((long)i));
				if (buff.Length < offst + num2)
				{
					byte[] array = new byte[offst + num2];
					Buffer.BlockCopy(buff, 0, array, 0, offst);
					buff = array;
				}
				int num3;
				bool flag = this.TryReadByteArray(MemoryExtensions.AsSpan<byte>(buff, offst), num2, out num3);
				i -= num3;
				offst += num3;
				totalBytesRead += num3;
				this._longlenleft -= (ulong)((long)num3);
				if (!flag)
				{
					return false;
				}
				ulong num;
				if (this._longlenleft == 0UL && !this.TryReadPlpLength(false, out num))
				{
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00029158 File Offset: 0x00027358
		internal bool TrySkipLongBytes(long num)
		{
			while (num > 0L)
			{
				int num2 = (int)Math.Min(2147483647L, num);
				if (!this.TryReadByteArray(Span<byte>.Empty, num2))
				{
					return false;
				}
				num -= (long)num2;
			}
			return true;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00029191 File Offset: 0x00027391
		internal bool TrySkipBytes(int num)
		{
			return this.TryReadByteArray(Span<byte>.Empty, num);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0002919F File Offset: 0x0002739F
		internal void SetSnapshot()
		{
			this._snapshot = new TdsParserStateObject.StateSnapshot(this);
			this._snapshot.Snap();
			this._snapshotReplay = false;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000291BF File Offset: 0x000273BF
		internal void ResetSnapshot()
		{
			this._snapshot = null;
			this._snapshotReplay = false;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000291D0 File Offset: 0x000273D0
		internal bool TryReadNetworkPacket()
		{
			if (this._snapshot != null)
			{
				if (this._snapshotReplay && this._snapshot.Replay())
				{
					SqlClientEventSource.Log.TryTraceEvent<string>("<sc.TdsParser.ReadNetworkPacket|{0}|ADV> Async packet replay{0}", "INFO");
					return true;
				}
				this._inBuff = new byte[this._inBuff.Length];
			}
			if (this._syncOverAsync)
			{
				this.ReadSniSyncOverAsync();
				return true;
			}
			this.ReadSni(new TaskCompletionSource<object>());
			return false;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0002923F File Offset: 0x0002743F
		internal void PrepareReplaySnapshot()
		{
			this._networkPacketTaskSource = null;
			this._snapshot.PrepareReplay();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00029254 File Offset: 0x00027454
		internal void ReadSniSyncOverAsync()
		{
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			IntPtr zero = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag = false;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				flag = true;
				SNIHandle handle = this.Handle;
				if (handle == null)
				{
					throw ADP.ClosedConnectionError();
				}
				uint num = SNINativeMethodWrapper.SNIReadSyncOverAsync(handle, ref zero, this.GetTimeoutRemaining());
				Interlocked.Decrement(ref this._readingCount);
				flag = false;
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(num, CallbackType.Read);
				}
				if (num == 0U)
				{
					this.ProcessSniPacket(zero, 0U);
				}
				else
				{
					this.ReadSniError(this, num);
				}
			}
			finally
			{
				if (flag)
				{
					Interlocked.Decrement(ref this._readingCount);
				}
				if (zero != IntPtr.Zero)
				{
					SNINativeMethodWrapper.SNIPacketRelease(zero);
				}
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002932C File Offset: 0x0002752C
		internal void OnConnectionClosed()
		{
			this.Parser.State = TdsParserState.Broken;
			this.Parser.Connection.BreakConnection();
			Thread.MemoryBarrier();
			TaskCompletionSource<object> taskCompletionSource = this._networkPacketTaskSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			taskCompletionSource = this._writeCompletionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00029392 File Offset: 0x00027592
		public void SetTimeoutStateStopped()
		{
			Interlocked.Exchange(ref this._timeoutState, 0);
			this._timeoutIdentityValue = 0;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000293AC File Offset: 0x000275AC
		public bool IsTimeoutStateExpired
		{
			get
			{
				int timeoutState = this._timeoutState;
				return timeoutState == 2 || timeoutState == 3;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000293CC File Offset: 0x000275CC
		private void OnTimeoutAsync(object state)
		{
			if (this._enforceTimeoutDelay)
			{
				Thread.Sleep(this._enforcedTimeoutDelayInMilliSeconds);
			}
			int timeoutIdentityValue = this._timeoutIdentityValue;
			TdsParserStateObject.TimeoutState timeoutState = (TdsParserStateObject.TimeoutState)state;
			if (timeoutState.IdentityValue == this._timeoutIdentityValue)
			{
				this.OnTimeoutCore(1, 2, false);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00029416 File Offset: 0x00027616
		private bool OnTimeoutSync(bool asyncClose = false)
		{
			return this.OnTimeoutCore(1, 3, asyncClose);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00029424 File Offset: 0x00027624
		private bool OnTimeoutCore(int expectedState, int targetState, bool asyncClose = false)
		{
			bool flag = false;
			if (Interlocked.CompareExchange(ref this._timeoutState, targetState, expectedState) == expectedState)
			{
				flag = true;
				lock (this)
				{
					if (!this._attentionSent)
					{
						this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
						TaskCompletionSource<object> source = this._networkPacketTaskSource;
						if (this._parser.Connection.IsInPool)
						{
							this._parser.State = TdsParserState.Broken;
							this._parser.Connection.BreakConnection();
							if (source != null)
							{
								source.TrySetCanceled();
							}
						}
						else if (this._parser.State == TdsParserState.OpenLoggedIn)
						{
							try
							{
								this.SendAttention(true, asyncClose);
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								if (source != null)
								{
									source.TrySetCanceled();
								}
							}
						}
						if (source != null)
						{
							Task.Delay(5000).ContinueWith(delegate(Task _)
							{
								if (!source.Task.IsCompleted)
								{
									int num = this.IncrementPendingCallbacks();
									RuntimeHelpers.PrepareConstrainedRegions();
									try
									{
										if (num == 3 && !source.Task.IsCompleted)
										{
											bool flag3 = false;
											try
											{
												this.CheckThrowSNIException();
											}
											catch (Exception ex2)
											{
												if (source.TrySetException(ex2))
												{
													flag3 = true;
												}
											}
											this._parser.State = TdsParserState.Broken;
											this._parser.Connection.BreakConnection();
											if (!flag3)
											{
												source.TrySetCanceled();
											}
										}
									}
									finally
									{
										this.DecrementPendingCallbacks(false);
									}
								}
							});
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002959C File Offset: 0x0002779C
		internal void ReadSni(TaskCompletionSource<object> completion)
		{
			this._networkPacketTaskSource = completion;
			Thread.MemoryBarrier();
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (Interlocked.CompareExchange(ref this._timeoutState, 1, 0) == 0)
				{
					this._timeoutIdentityValue = Interlocked.Increment(ref this._timeoutIdentitySource);
				}
				Timer networkPacketTimeout = this._networkPacketTimeout;
				if (networkPacketTimeout != null)
				{
					networkPacketTimeout.Dispose();
				}
				this._networkPacketTimeout = new Timer(new TimerCallback(this.OnTimeoutAsync), new TdsParserStateObject.TimeoutState(this._timeoutIdentityValue), -1, -1);
				int timeoutRemaining = this.GetTimeoutRemaining();
				if (timeoutRemaining > 0)
				{
					this.ChangeNetworkPacketTimeout(timeoutRemaining, -1);
				}
				SNIHandle snihandle = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					Interlocked.Increment(ref this._readingCount);
					snihandle = this.Handle;
					if (snihandle != null)
					{
						this.IncrementPendingCallbacks();
						num = SNINativeMethodWrapper.SNIReadAsync(snihandle, ref zero);
						if (num != 0U && 997U != num)
						{
							this.DecrementPendingCallbacks(false);
						}
					}
					Interlocked.Decrement(ref this._readingCount);
				}
				if (snihandle == null)
				{
					throw ADP.ClosedConnectionError();
				}
				if (num == 0U)
				{
					this.ReadAsyncCallback(ADP.s_ptrZero, zero, 0U);
				}
				else if (997U != num)
				{
					this.ReadSniError(this, num);
					this._networkPacketTaskSource.TrySetResult(null);
					this.SetTimeoutStateStopped();
					this.ChangeNetworkPacketTimeout(-1, -1);
				}
				else if (timeoutRemaining == 0)
				{
					this.ChangeNetworkPacketTimeout(-1, -1);
					this.OnTimeoutSync(false);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					SNINativeMethodWrapper.SNIPacketRelease(zero);
				}
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002974C File Offset: 0x0002794C
		internal bool IsConnectionAlive(bool throwOnException)
		{
			bool flag = true;
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value > 50000L)
			{
				if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					flag = false;
					if (throwOnException)
					{
						throw SQL.ConnectionDoomed();
					}
				}
				else if (this._pendingCallbacks <= 1 && (this._parser.Connection == null || this._parser.Connection.IsInPool))
				{
					IntPtr zero = IntPtr.Zero;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this.SniContext = SniContext.Snix_Connect;
						uint num = SNINativeMethodWrapper.SNICheckConnection(this.Handle);
						if (num != 0U && num != 258U)
						{
							SqlClientEventSource.Log.TryTraceEvent<int>("<sc.TdsParser.IsConnectionAlive|Info> received error {0} on idle connection", (int)num);
							flag = false;
							if (throwOnException)
							{
								this.AddError(this._parser.ProcessSNIError(this));
								this.ThrowExceptionAndWarning(false, false);
							}
						}
						else
						{
							this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
						}
					}
					finally
					{
						if (zero != IntPtr.Zero)
						{
							SNINativeMethodWrapper.SNIPacketRelease(zero);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00029878 File Offset: 0x00027A78
		internal bool ValidateSNIConnection()
		{
			if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				return false;
			}
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value <= 50000L)
			{
				return true;
			}
			uint num = 0U;
			this.SniContext = SniContext.Snix_Connect;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				SNIHandle handle = this.Handle;
				if (handle != null)
				{
					num = SNINativeMethodWrapper.SNICheckConnection(handle);
				}
			}
			finally
			{
				Interlocked.Decrement(ref this._readingCount);
			}
			return num == 0U || num == 258U;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00029920 File Offset: 0x00027B20
		private void ReadSniError(TdsParserStateObject stateObj, uint error)
		{
			if (258U == error)
			{
				bool flag = false;
				if (this.IsTimeoutStateExpired)
				{
					flag = true;
				}
				else
				{
					stateObj.SetTimeoutStateStopped();
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					if (!stateObj._attentionSent)
					{
						if (stateObj.Parser.State == TdsParserState.OpenLoggedIn)
						{
							stateObj.SendAttention(true, false);
							IntPtr zero = IntPtr.Zero;
							RuntimeHelpers.PrepareConstrainedRegions();
							bool flag2 = false;
							try
							{
								Interlocked.Increment(ref this._readingCount);
								flag2 = true;
								SNIHandle handle = this.Handle;
								if (handle == null)
								{
									throw ADP.ClosedConnectionError();
								}
								error = SNINativeMethodWrapper.SNIReadSyncOverAsync(handle, ref zero, stateObj.GetTimeoutRemaining());
								Interlocked.Decrement(ref this._readingCount);
								flag2 = false;
								if (error == 0U)
								{
									stateObj.ProcessSniPacket(zero, 0U);
									return;
								}
								flag = true;
								goto IL_016C;
							}
							finally
							{
								if (flag2)
								{
									Interlocked.Decrement(ref this._readingCount);
								}
								if (zero != IntPtr.Zero)
								{
									SNINativeMethodWrapper.SNIPacketRelease(zero);
								}
							}
						}
						if (this._parser._loginWithFailover)
						{
							this._parser.Disconnect();
						}
						else if (this._parser.State == TdsParserState.OpenNotLoggedIn && (this._parser.Connection.ConnectionOptions.MultiSubnetFailover || this._parser.Connection.ConnectionOptions.TransparentNetworkIPResolution))
						{
							this._parser.Disconnect();
						}
						else
						{
							flag = true;
						}
					}
				}
				IL_016C:
				if (flag)
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
				}
			}
			else
			{
				this.AddError(this._parser.ProcessSNIError(stateObj));
			}
			this.ThrowExceptionAndWarning(false, false);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00029AE4 File Offset: 0x00027CE4
		public void ProcessSniPacket(IntPtr packet, uint error)
		{
			if (error != 0U)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				this.AddError(this._parser.ProcessSNIError(this));
				return;
			}
			else
			{
				uint num = 0U;
				if (SNINativeMethodWrapper.SNIPacketGetData(packet, this._inBuff, ref num) != 0U)
				{
					throw SQL.ParsingError(ParsingErrorState.ProcessSniPacketFailed);
				}
				if ((long)this._inBuff.Length < (long)((ulong)num))
				{
					throw SQL.InvalidInternalPacketSize(StringsHelper.GetString(Strings.SqlMisc_InvalidArraySizeMessage, Array.Empty<object>()));
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
				this._inBytesRead = (int)num;
				this._inBytesUsed = 0;
				if (this._snapshot != null)
				{
					this._snapshot.PushBuffer(this._inBuff, this._inBytesRead);
					if (this._snapshotReplay)
					{
						this._snapshot.Replay();
					}
				}
				this.SniReadStatisticsAndTracing();
				SqlClientEventSource.Log.TryAdvancedTraceBinEvent<int, byte[], ushort>("TdsParser.ReadNetworkPacketAsyncCallback | INFO | ADV | State Object Id {0}, Packet read. In Buffer {1}, In Bytes Read: {2}", this.ObjectID, this._inBuff, (ushort)this._inBytesRead);
				return;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00029BE8 File Offset: 0x00027DE8
		private void ChangeNetworkPacketTimeout(int dueTime, int period)
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				try
				{
					networkPacketTimeout.Change(dueTime, period);
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00029C20 File Offset: 0x00027E20
		public void ReadAsyncCallback(IntPtr key, IntPtr packet, uint error)
		{
			TaskCompletionSource<object> source = this._networkPacketTaskSource;
			if (source == null && this._parser._pMarsPhysicalConObj == this)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag = true;
			try
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(error, CallbackType.Read);
				}
				this.ChangeNetworkPacketTimeout(-1, -1);
				if (this.TimeoutHasExpired)
				{
					this.OnTimeoutSync(true);
				}
				int num = Interlocked.CompareExchange(ref this._timeoutState, 0, 1);
				if (this._timeoutState != 1)
				{
					this._timeoutIdentityValue = 0;
				}
				this.ProcessSniPacket(packet, error);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				int num2 = this.DecrementPendingCallbacks(false);
				if (flag && source != null && num2 < 2)
				{
					if (error == 0U)
					{
						if (this._executionContext != null)
						{
							ExecutionContext.Run(this._executionContext, delegate(object state)
							{
								source.TrySetResult(null);
							}, null);
						}
						else
						{
							source.TrySetResult(null);
						}
					}
					else if (this._executionContext != null)
					{
						ExecutionContext.Run(this._executionContext, delegate(object state)
						{
							this.ReadAsyncCallbackCaptureException(source);
						}, null);
					}
					else
					{
						this.ReadAsyncCallbackCaptureException(source);
					}
				}
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00029D60 File Offset: 0x00027F60
		private void ReadAsyncCallbackCaptureException(TaskCompletionSource<object> source)
		{
			bool flag = false;
			try
			{
				if (this._hasErrorOrWarning)
				{
					this.ThrowExceptionAndWarning(false, true);
				}
				else if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			catch (Exception ex)
			{
				if (source.TrySetException(ex))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Task.Factory.StartNew(delegate
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
					source.TrySetCanceled();
				});
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00029DF8 File Offset: 0x00027FF8
		public void WriteAsyncCallback(IntPtr key, IntPtr packet, uint sniError)
		{
			this.RemovePacketFromPendingList(packet);
			try
			{
				if (sniError != 0U)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.TdsParser.WriteAsyncCallback|Info> write async returned error code {0}", (int)sniError);
					try
					{
						this.AddError(this._parser.ProcessSNIError(this));
						this.ThrowExceptionAndWarning(false, true);
						goto IL_00A9;
					}
					catch (Exception ex)
					{
						TaskCompletionSource<object> taskCompletionSource = this._writeCompletionSource;
						if (taskCompletionSource != null)
						{
							taskCompletionSource.TrySetException(ex);
						}
						else
						{
							this._delayedWriteAsyncCallbackException = ex;
							Thread.MemoryBarrier();
							taskCompletionSource = this._writeCompletionSource;
							if (taskCompletionSource != null)
							{
								Exception ex2 = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
								if (ex2 != null)
								{
									taskCompletionSource.TrySetException(ex2);
								}
							}
						}
						return;
					}
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncWriteCount);
			}
			IL_00A9:
			TaskCompletionSource<object> writeCompletionSource = this._writeCompletionSource;
			if (this._asyncWriteCount == 0 && writeCompletionSource != null)
			{
				writeCompletionSource.TrySetResult(null);
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00029EE8 File Offset: 0x000280E8
		internal void WriteSecureString(SecureString secureString)
		{
			int num = ((this._securePasswords[0] != null) ? 1 : 0);
			this._securePasswords[num] = secureString;
			this._securePasswordOffsetsInBuffer[num] = this._outBytesUsed;
			int num2 = secureString.Length * 2;
			this._outBytesUsed += num2;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00029F30 File Offset: 0x00028130
		internal void ResetSecurePasswordsInfomation()
		{
			for (int i = 0; i < this._securePasswords.Length; i++)
			{
				this._securePasswords[i] = null;
				this._securePasswordOffsetsInBuffer[i] = 0;
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00029F64 File Offset: 0x00028164
		internal Task WaitForAccumulatedWrites()
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0)
			{
				return null;
			}
			this._writeCompletionSource = new TaskCompletionSource<object>();
			Task task = this._writeCompletionSource.Task;
			Thread.MemoryBarrier();
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
			{
				task = null;
			}
			return task;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002A000 File Offset: 0x00028200
		internal void WriteByte(byte b)
		{
			if (this._outBytesUsed == this._outBuff.Length)
			{
				this.WritePacket(0, true);
			}
			byte[] outBuff = this._outBuff;
			int outBytesUsed = this._outBytesUsed;
			this._outBytesUsed = outBytesUsed + 1;
			outBuff[outBytesUsed] = b;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002A040 File Offset: 0x00028240
		internal Task WriteByteArray(byte[] b, int len, int offsetBuffer, bool canAccumulate = true, TaskCompletionSource<object> completion = null)
		{
			Task task3;
			try
			{
				bool asyncWrite = this._parser._asyncWrite;
				int num = offsetBuffer;
				while (this._outBytesUsed + len > this._outBuff.Length)
				{
					int num2 = this._outBuff.Length - this._outBytesUsed;
					Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, num2);
					num += num2;
					this._outBytesUsed += num2;
					len -= num2;
					Task task = this.WritePacket(0, canAccumulate);
					if (task != null)
					{
						Task task2 = null;
						if (completion == null)
						{
							completion = new TaskCompletionSource<object>();
							task2 = completion.Task;
						}
						this.WriteByteArraySetupContinuation(b, len, completion, num, task);
						return task2;
					}
					if (len <= 0)
					{
						IL_00BC:
						if (completion != null)
						{
							completion.SetResult(null);
						}
						return null;
					}
				}
				Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, len);
				this._outBytesUsed += len;
				goto IL_00BC;
			}
			catch (Exception ex)
			{
				if (completion == null)
				{
					throw;
				}
				completion.SetException(ex);
				task3 = null;
			}
			return task3;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002A144 File Offset: 0x00028344
		private void WriteByteArraySetupContinuation(byte[] b, int len, TaskCompletionSource<object> completion, int offset, Task packetTask)
		{
			AsyncHelper.ContinueTask(packetTask, completion, delegate
			{
				this.WriteByteArray(b, len, offset, false, completion);
			}, null, null, null, this._parser.Connection, null);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002A1A4 File Offset: 0x000283A4
		internal Task WritePacket(byte flushMode, bool canAccumulate = false)
		{
			TdsParserState state = this._parser.State;
			if (state == TdsParserState.Closed || state == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			if ((state == TdsParserState.OpenLoggedIn && !this._bulkCopyOpperationInProgress && this._outBytesUsed == this._outputHeaderLen + BitConverter.ToInt32(this._outBuff, this._outputHeaderLen) && this._outputPacketCount == 0U) || (this._outBytesUsed == this._outputHeaderLen && this._outputPacketCount == 0U))
			{
				return null;
			}
			byte outputPacketNumber = this._outputPacketNumber;
			bool flag = this._cancelled && this._parser._asyncWrite;
			byte b;
			if (flag)
			{
				b = 3;
				this.ResetPacketCounters();
			}
			else if (1 == flushMode)
			{
				b = 1;
				this.ResetPacketCounters();
			}
			else if (flushMode == 0)
			{
				b = 4;
				this._outputPacketNumber += 1;
				this._outputPacketCount += 1U;
			}
			else
			{
				b = 1;
			}
			this._outBuff[0] = this._outputMessageType;
			this._outBuff[1] = b;
			this._outBuff[2] = (byte)(this._outBytesUsed >> 8);
			this._outBuff[3] = (byte)(this._outBytesUsed & 255);
			this._outBuff[4] = 0;
			this._outBuff[5] = 0;
			this._outBuff[6] = outputPacketNumber;
			this._outBuff[7] = 0;
			this._parser.CheckResetConnection(this);
			Task task = this.WriteSni(canAccumulate);
			if (flag)
			{
				task = AsyncHelper.CreateContinuationTask(task, new Action(this.CancelWritePacket), this._parser.Connection, null);
			}
			return task;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002A314 File Offset: 0x00028514
		private void CancelWritePacket()
		{
			this._parser.Connection.ThreadHasParserLockForClose = true;
			try
			{
				this.SendAttention(false, false);
				this.ResetCancelAndProcessAttention();
				throw SQL.OperationCancelled();
			}
			finally
			{
				this._parser.Connection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002A368 File Offset: 0x00028568
		private Task SNIWritePacket(SNIHandle handle, SNIPacket packet, out uint sniError, bool canAccumulate, bool callerHasConnectionLock, bool asyncClose = false)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			Task task = null;
			this._writeCompletionSource = null;
			IntPtr intPtr = IntPtr.Zero;
			bool flag = !this._parser._asyncWrite;
			if (flag && this._asyncWriteCount > 0)
			{
				Task task2 = this.WaitForAccumulatedWrites();
				if (task2 != null)
				{
					try
					{
						task2.Wait();
					}
					catch (AggregateException ex2)
					{
						throw ex2.InnerException;
					}
				}
			}
			if (!flag)
			{
				intPtr = this.AddPacketToPendingList(packet);
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				sniError = SNINativeMethodWrapper.SNIWritePacket(handle, packet, flag);
			}
			if (sniError == 997U)
			{
				Interlocked.Increment(ref this._asyncWriteCount);
				if (!canAccumulate)
				{
					this._writeCompletionSource = new TaskCompletionSource<object>();
					task = this._writeCompletionSource.Task;
					Thread.MemoryBarrier();
					ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
					if (ex != null)
					{
						throw ex;
					}
					if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
					{
						task = null;
					}
				}
			}
			else
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(sniError, CallbackType.Write);
				}
				if (sniError == 0U)
				{
					this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
					if (!flag)
					{
						this.RemovePacketFromPendingList(intPtr);
					}
				}
				else
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.TdsParser.WritePacket|Info> write async returned error code {0}", (int)sniError);
					this.AddError(this._parser.ProcessSNIError(this));
					this.ThrowExceptionAndWarning(callerHasConnectionLock, false);
				}
			}
			return task;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002A4E8 File Offset: 0x000286E8
		internal void SendAttention(bool mustTakeWriteLock = false, bool asyncClose = false)
		{
			if (!this._attentionSent)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				SNIPacket snipacket = new SNIPacket(this.Handle);
				this._sniAsyncAttnPacket = snipacket;
				SNINativeMethodWrapper.SNIPacketSetData(snipacket, SQL.AttentionHeader, 8, null, null);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._attentionSending = true;
					bool flag = false;
					if (mustTakeWriteLock && !this._parser.Connection.ThreadHasParserLockForClose)
					{
						flag = true;
						this._parser.Connection._parserLock.Wait(false);
						this._parser.Connection.ThreadHasParserLockForClose = true;
					}
					try
					{
						if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
						{
							return;
						}
						this._parser._asyncWrite = false;
						uint num;
						this.SNIWritePacket(this.Handle, snipacket, out num, false, false, asyncClose);
						SqlClientEventSource.Log.TryTraceEvent<string>("<sc.TdsParser.SendAttention|{0}> Send Attention ASync.", "Info");
					}
					finally
					{
						if (flag)
						{
							this._parser.Connection.ThreadHasParserLockForClose = false;
							this._parser.Connection._parserLock.Release();
						}
					}
					this.SetTimeoutSeconds(5);
					this._attentionSent = true;
				}
				finally
				{
					this._attentionSending = false;
				}
				SqlClientEventSource.Log.TryAdvancedTraceBinEvent<int, byte[], ushort>("TdsParser.WritePacket | INFO | ADV | State Object Id {0}, Packet sent. Out Buffer {1}, Out Bytes Used {2}", this.ObjectID, this._outBuff, (ushort)this._outBytesUsed);
				SqlClientEventSource.Log.TryTraceEvent("TdsParser.SendAttention | INFO | Attention sent to the server.");
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002A678 File Offset: 0x00028878
		private Task WriteSni(bool canAccumulate)
		{
			SNIPacket resetWritePacket = this.GetResetWritePacket();
			SNINativeMethodWrapper.SNIPacketSetData(resetWritePacket, this._outBuff, this._outBytesUsed, this._securePasswords, this._securePasswordOffsetsInBuffer);
			uint num;
			Task task = this.SNIWritePacket(this.Handle, resetWritePacket, out num, canAccumulate, true, false);
			if (this._bulkCopyOpperationInProgress && this.GetTimeoutRemaining() == 0)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					this._bulkCopyWriteTimeout = true;
					this.SendAttention(false, false);
					this._parser.ProcessPendingAck(this);
					this.ThrowExceptionAndWarning(false, false);
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
			if (this._parser.State == TdsParserState.OpenNotLoggedIn && (this._parser.EncryptionOptions & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.LOGIN)
			{
				this._parser.RemoveEncryption();
				this._parser.EncryptionOptions = EncryptionOptions.OFF | (this._parser.EncryptionOptions & (EncryptionOptions)(-64));
				this.ClearAllWritePackets();
			}
			this.SniWriteStatisticsAndTracing();
			this.ResetBuffer();
			return task;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002A7C0 File Offset: 0x000289C0
		internal SNIPacket GetResetWritePacket()
		{
			if (this._sniPacket != null)
			{
				SNINativeMethodWrapper.SNIPacketReset(this.Handle, SNINativeMethodWrapper.IOType.WRITE, this._sniPacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
			}
			else
			{
				object writePacketLockObject = this._writePacketLockObject;
				lock (writePacketLockObject)
				{
					this._sniPacket = this._writePacketCache.Take(this.Handle);
				}
			}
			return this._sniPacket;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002A834 File Offset: 0x00028A34
		internal void ClearAllWritePackets()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Dispose();
				this._sniPacket = null;
			}
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Clear();
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002A894 File Offset: 0x00028A94
		private IntPtr AddPacketToPendingList(SNIPacket packet)
		{
			this._sniPacket = null;
			IntPtr intPtr = packet.DangerousGetHandle();
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._pendingWritePackets.Add(intPtr, packet);
			}
			return intPtr;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002A8EC File Offset: 0x00028AEC
		private void RemovePacketFromPendingList(IntPtr pointer)
		{
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				SNIPacket snipacket;
				if (this._pendingWritePackets.TryGetValue(pointer, out snipacket))
				{
					this._pendingWritePackets.Remove(pointer);
					this._writePacketCache.Add(snipacket);
				}
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002A950 File Offset: 0x00028B50
		private void SniReadStatisticsAndTracing()
		{
			SqlStatistics statistics = this.Parser.Statistics;
			if (statistics != null)
			{
				if (statistics.WaitForReply)
				{
					statistics.SafeIncrement(ref statistics._serverRoundtrips);
					statistics.ReleaseAndUpdateNetworkServerTimer();
				}
				statistics.SafeAdd(ref statistics._bytesReceived, (long)this._inBytesRead);
				statistics.SafeIncrement(ref statistics._buffersReceived);
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		private void SniWriteStatisticsAndTracing()
		{
			SqlStatistics statistics = this._parser.Statistics;
			if (statistics != null)
			{
				statistics.SafeIncrement(ref statistics._buffersSent);
				statistics.SafeAdd(ref statistics._bytesSent, (long)this._outBytesUsed);
				statistics.RequestNetworkServerTimer();
			}
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				if (this._tracePasswordOffset != 0)
				{
					for (int i = this._tracePasswordOffset; i < this._tracePasswordOffset + this._tracePasswordLength; i++)
					{
						this._outBuff[i] = 0;
					}
					this._tracePasswordOffset = 0;
					this._tracePasswordLength = 0;
				}
				if (this._traceChangePasswordOffset != 0)
				{
					for (int j = this._traceChangePasswordOffset; j < this._traceChangePasswordOffset + this._traceChangePasswordLength; j++)
					{
						this._outBuff[j] = 0;
					}
					this._traceChangePasswordOffset = 0;
					this._traceChangePasswordLength = 0;
				}
			}
			SqlClientEventSource.Log.TryAdvancedTraceBinEvent<int, byte[], ushort>("TdsParser.WritePacket | INFO | ADV | State Object Id {0}, Packet sent. Out buffer: {1}, Out Bytes Used: {2}", this.ObjectID, this._outBuff, (ushort)this._outBytesUsed);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002AA90 File Offset: 0x00028C90
		[Conditional("DEBUG")]
		private void AssertValidState()
		{
			if (this._inBytesUsed >= 0 && this._inBytesRead >= 0)
			{
				int inBytesUsed = this._inBytesUsed;
				int inBytesRead = this._inBytesRead;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0002AAB2 File Offset: 0x00028CB2
		internal bool HasErrorOrWarning
		{
			get
			{
				return this._hasErrorOrWarning;
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002AABC File Offset: 0x00028CBC
		internal void AddError(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				this._errors.Add(error);
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0002AB24 File Offset: 0x00028D24
		internal int ErrorCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._errors != null)
					{
						num = this._errors.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002AB78 File Offset: 0x00028D78
		internal void AddWarning(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._warnings == null)
				{
					this._warnings = new SqlErrorCollection();
				}
				this._warnings.Add(error);
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		internal int WarningCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._warnings != null)
					{
						num = this._warnings.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002AC34 File Offset: 0x00028E34
		internal int PreAttentionErrorCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._preAttentionErrors != null)
					{
						num = this._preAttentionErrors.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0002AC88 File Offset: 0x00028E88
		internal int PreAttentionWarningCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._preAttentionWarnings != null)
					{
						num = this._preAttentionWarnings.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002ACDC File Offset: 0x00028EDC
		internal SqlErrorCollection GetFullErrorAndWarningCollection(out bool broken)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			broken = false;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this.AddErrorsToCollection(this._errors, ref sqlErrorCollection, ref broken);
				this.AddErrorsToCollection(this._warnings, ref sqlErrorCollection, ref broken);
				this._errors = null;
				this._warnings = null;
				this.AddErrorsToCollection(this._preAttentionErrors, ref sqlErrorCollection, ref broken);
				this.AddErrorsToCollection(this._preAttentionWarnings, ref sqlErrorCollection, ref broken);
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
			return sqlErrorCollection;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002AD80 File Offset: 0x00028F80
		private void AddErrorsToCollection(SqlErrorCollection inCollection, ref SqlErrorCollection collectionToAddTo, ref bool broken)
		{
			if (inCollection != null)
			{
				foreach (object obj in inCollection)
				{
					SqlError sqlError = (SqlError)obj;
					collectionToAddTo.Add(sqlError);
					broken |= sqlError.Class >= 20;
				}
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002ADEC File Offset: 0x00028FEC
		internal void StoreErrorAndWarningForAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this._preAttentionErrors = this._errors;
				this._preAttentionWarnings = this._warnings;
				this._errors = null;
				this._warnings = null;
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002AE54 File Offset: 0x00029054
		internal void RestoreErrorAndWarningAfterAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = (this._preAttentionErrors != null && this._preAttentionErrors.Count > 0) || (this._preAttentionWarnings != null && this._preAttentionWarnings.Count > 0);
				this._errors = this._preAttentionErrors;
				this._warnings = this._preAttentionWarnings;
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002AEEC File Offset: 0x000290EC
		internal void CheckThrowSNIException()
		{
			if (this.HasErrorOrWarning)
			{
				this.ThrowExceptionAndWarning(false, false);
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002AF00 File Offset: 0x00029100
		[Conditional("DEBUG")]
		internal void AssertStateIsClean()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State != TdsParserState.Closed)
			{
				TdsParserState state = parser.State;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002AF28 File Offset: 0x00029128
		internal void CloneCleanupAltMetaDataSetArray()
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneCleanupAltMetaDataSetArray();
			}
		}

		// Token: 0x0400057D RID: 1405
		private static int s_objectTypeCount;

		// Token: 0x0400057E RID: 1406
		internal readonly int _objectID = Interlocked.Increment(ref TdsParserStateObject.s_objectTypeCount);

		// Token: 0x0400057F RID: 1407
		private const int AttentionTimeoutSeconds = 5;

		// Token: 0x04000580 RID: 1408
		private const long CheckConnectionWindow = 50000L;

		// Token: 0x04000581 RID: 1409
		protected readonly TdsParser _parser;

		// Token: 0x04000582 RID: 1410
		private readonly WeakReference<object> _owner = new WeakReference<object>(null);

		// Token: 0x04000583 RID: 1411
		internal SqlDataReader.SharedState _readerState;

		// Token: 0x04000584 RID: 1412
		private int _activateCount;

		// Token: 0x04000585 RID: 1413
		internal readonly int _inputHeaderLen = 8;

		// Token: 0x04000586 RID: 1414
		internal readonly int _outputHeaderLen = 8;

		// Token: 0x04000587 RID: 1415
		internal byte[] _outBuff;

		// Token: 0x04000588 RID: 1416
		internal int _outBytesUsed = 8;

		// Token: 0x04000589 RID: 1417
		protected byte[] _inBuff;

		// Token: 0x0400058A RID: 1418
		internal int _inBytesUsed;

		// Token: 0x0400058B RID: 1419
		internal int _inBytesRead;

		// Token: 0x0400058C RID: 1420
		internal int _inBytesPacket;

		// Token: 0x0400058D RID: 1421
		internal int _spid;

		// Token: 0x0400058E RID: 1422
		internal byte _outputMessageType;

		// Token: 0x0400058F RID: 1423
		internal byte _messageStatus;

		// Token: 0x04000590 RID: 1424
		internal byte _outputPacketNumber = 1;

		// Token: 0x04000591 RID: 1425
		internal uint _outputPacketCount;

		// Token: 0x04000592 RID: 1426
		internal volatile bool _fResetEventOwned;

		// Token: 0x04000593 RID: 1427
		internal volatile bool _fResetConnectionSent;

		// Token: 0x04000594 RID: 1428
		internal bool _bulkCopyOpperationInProgress;

		// Token: 0x04000595 RID: 1429
		internal bool _bulkCopyWriteTimeout;

		// Token: 0x04000596 RID: 1430
		protected readonly object _writePacketLockObject = new object();

		// Token: 0x04000597 RID: 1431
		private int _pendingCallbacks;

		// Token: 0x04000598 RID: 1432
		private long _timeoutMilliseconds;

		// Token: 0x04000599 RID: 1433
		private long _timeoutTime;

		// Token: 0x0400059A RID: 1434
		private int _timeoutState;

		// Token: 0x0400059B RID: 1435
		private int _timeoutIdentitySource;

		// Token: 0x0400059C RID: 1436
		private volatile int _timeoutIdentityValue;

		// Token: 0x0400059D RID: 1437
		internal volatile bool _attentionSent;

		// Token: 0x0400059E RID: 1438
		internal volatile bool _attentionSending;

		// Token: 0x0400059F RID: 1439
		private readonly TimerCallback _onTimeoutAsync;

		// Token: 0x040005A0 RID: 1440
		internal bool _enforceTimeoutDelay;

		// Token: 0x040005A1 RID: 1441
		internal int _enforcedTimeoutDelayInMilliSeconds = 5000;

		// Token: 0x040005A2 RID: 1442
		private readonly LastIOTimer _lastSuccessfulIOTimer;

		// Token: 0x040005A3 RID: 1443
		private readonly SecureString[] _securePasswords = new SecureString[2];

		// Token: 0x040005A4 RID: 1444
		private readonly int[] _securePasswordOffsetsInBuffer = new int[2];

		// Token: 0x040005A5 RID: 1445
		private bool _cancelled;

		// Token: 0x040005A6 RID: 1446
		private const int WaitForCancellationLockPollTimeout = 100;

		// Token: 0x040005A7 RID: 1447
		internal SqlInternalTransaction _executedUnderTransaction;

		// Token: 0x040005A8 RID: 1448
		internal ulong _longlen;

		// Token: 0x040005A9 RID: 1449
		internal ulong _longlenleft;

		// Token: 0x040005AA RID: 1450
		internal int[] _decimalBits;

		// Token: 0x040005AB RID: 1451
		internal byte[] _bTmp = new byte[12];

		// Token: 0x040005AC RID: 1452
		internal int _bTmpRead;

		// Token: 0x040005AD RID: 1453
		internal Decoder _plpdecoder;

		// Token: 0x040005AE RID: 1454
		internal bool _accumulateInfoEvents;

		// Token: 0x040005AF RID: 1455
		internal List<SqlError> _pendingInfoEvents;

		// Token: 0x040005B0 RID: 1456
		internal byte[] _bLongBytes;

		// Token: 0x040005B1 RID: 1457
		internal byte[] _bIntBytes;

		// Token: 0x040005B2 RID: 1458
		internal byte[] _bShortBytes;

		// Token: 0x040005B3 RID: 1459
		internal byte[] _bDecimalBytes;

		// Token: 0x040005B4 RID: 1460
		private readonly byte[] _partialHeaderBuffer = new byte[8];

		// Token: 0x040005B5 RID: 1461
		internal int _partialHeaderBytesRead;

		// Token: 0x040005B6 RID: 1462
		internal _SqlMetaDataSet _cleanupMetaData;

		// Token: 0x040005B7 RID: 1463
		internal _SqlMetaDataSetCollection _cleanupAltMetaDataSetArray;

		// Token: 0x040005B8 RID: 1464
		private SniContext _sniContext;

		// Token: 0x040005B9 RID: 1465
		private bool _bcpLock;

		// Token: 0x040005BA RID: 1466
		private TdsParserStateObject.NullBitmap _nullBitmapInfo;

		// Token: 0x040005BB RID: 1467
		internal TaskCompletionSource<object> _networkPacketTaskSource;

		// Token: 0x040005BC RID: 1468
		private Timer _networkPacketTimeout;

		// Token: 0x040005BD RID: 1469
		internal bool _syncOverAsync = true;

		// Token: 0x040005BE RID: 1470
		private bool _snapshotReplay;

		// Token: 0x040005BF RID: 1471
		private TdsParserStateObject.StateSnapshot _snapshot;

		// Token: 0x040005C0 RID: 1472
		internal ExecutionContext _executionContext;

		// Token: 0x040005C1 RID: 1473
		internal bool _asyncReadWithoutSnapshot;

		// Token: 0x040005C2 RID: 1474
		internal SqlErrorCollection _errors;

		// Token: 0x040005C3 RID: 1475
		internal SqlErrorCollection _warnings;

		// Token: 0x040005C4 RID: 1476
		internal object _errorAndWarningsLock = new object();

		// Token: 0x040005C5 RID: 1477
		private bool _hasErrorOrWarning;

		// Token: 0x040005C6 RID: 1478
		internal SqlErrorCollection _preAttentionErrors;

		// Token: 0x040005C7 RID: 1479
		internal SqlErrorCollection _preAttentionWarnings;

		// Token: 0x040005C8 RID: 1480
		private volatile TaskCompletionSource<object> _writeCompletionSource;

		// Token: 0x040005C9 RID: 1481
		protected volatile int _asyncWriteCount;

		// Token: 0x040005CA RID: 1482
		private volatile Exception _delayedWriteAsyncCallbackException;

		// Token: 0x040005CB RID: 1483
		private int _readingCount;

		// Token: 0x040005CC RID: 1484
		private SNIHandle _sessionHandle;

		// Token: 0x040005CD RID: 1485
		internal bool _pendingData;

		// Token: 0x040005CE RID: 1486
		internal bool _errorTokenReceived;

		// Token: 0x040005CF RID: 1487
		private SNIPacket _sniPacket;

		// Token: 0x040005D0 RID: 1488
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x040005D1 RID: 1489
		private readonly WritePacketCache _writePacketCache = new WritePacketCache();

		// Token: 0x040005D2 RID: 1490
		private readonly Dictionary<IntPtr, SNIPacket> _pendingWritePackets = new Dictionary<IntPtr, SNIPacket>();

		// Token: 0x040005D3 RID: 1491
		private GCHandle _gcHandle;

		// Token: 0x040005D4 RID: 1492
		internal bool _attentionReceived;

		// Token: 0x040005D5 RID: 1493
		private volatile int _allowObjectID;

		// Token: 0x040005D6 RID: 1494
		internal bool _hasOpenResult;

		// Token: 0x040005D7 RID: 1495
		internal int _tracePasswordOffset;

		// Token: 0x040005D8 RID: 1496
		internal int _tracePasswordLength;

		// Token: 0x040005D9 RID: 1497
		internal int _traceChangePasswordOffset;

		// Token: 0x040005DA RID: 1498
		internal int _traceChangePasswordLength;

		// Token: 0x040005DB RID: 1499
		internal bool _receivedColMetaData;

		// Token: 0x020001E4 RID: 484
		[Flags]
		internal enum SnapshottedStateFlags : byte
		{
			// Token: 0x04001483 RID: 5251
			None = 0,
			// Token: 0x04001484 RID: 5252
			PendingData = 2,
			// Token: 0x04001485 RID: 5253
			OpenResult = 4,
			// Token: 0x04001486 RID: 5254
			ErrorTokenReceived = 8,
			// Token: 0x04001487 RID: 5255
			ColMetaDataReceived = 16,
			// Token: 0x04001488 RID: 5256
			AttentionReceived = 32
		}

		// Token: 0x020001E5 RID: 485
		private sealed class TimeoutState
		{
			// Token: 0x06001DDD RID: 7645 RVA: 0x0007B34D File Offset: 0x0007954D
			public TimeoutState(int value)
			{
				this._value = value;
			}

			// Token: 0x17000A32 RID: 2610
			// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0007B35C File Offset: 0x0007955C
			public int IdentityValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04001489 RID: 5257
			public const int Stopped = 0;

			// Token: 0x0400148A RID: 5258
			public const int Running = 1;

			// Token: 0x0400148B RID: 5259
			public const int ExpiredAsync = 2;

			// Token: 0x0400148C RID: 5260
			public const int ExpiredSync = 3;

			// Token: 0x0400148D RID: 5261
			private readonly int _value;
		}

		// Token: 0x020001E6 RID: 486
		private struct NullBitmap
		{
			// Token: 0x06001DDF RID: 7647 RVA: 0x0007B364 File Offset: 0x00079564
			internal bool ReferenceEquals(TdsParserStateObject.NullBitmap obj)
			{
				return this._nullBitmap == obj._nullBitmap;
			}

			// Token: 0x06001DE0 RID: 7648 RVA: 0x0007B374 File Offset: 0x00079574
			internal TdsParserStateObject.NullBitmap Clone()
			{
				return new TdsParserStateObject.NullBitmap
				{
					_nullBitmap = ((this._nullBitmap == null) ? null : ((byte[])this._nullBitmap.Clone())),
					_columnsCount = this._columnsCount
				};
			}

			// Token: 0x06001DE1 RID: 7649 RVA: 0x0007B3B9 File Offset: 0x000795B9
			internal void Clean()
			{
				this._columnsCount = 0;
			}

			// Token: 0x06001DE2 RID: 7650 RVA: 0x0007B3C4 File Offset: 0x000795C4
			internal bool IsGuaranteedNull(int columnOrdinal)
			{
				if (this._columnsCount == 0)
				{
					return false;
				}
				byte b = (byte)(1 << (columnOrdinal & 7));
				byte b2 = this._nullBitmap[columnOrdinal >> 3];
				return (b & b2) > 0;
			}

			// Token: 0x06001DE3 RID: 7651 RVA: 0x0007B3F8 File Offset: 0x000795F8
			internal bool TryInitialize(TdsParserStateObject stateObj, int columnsCount)
			{
				this._columnsCount = columnsCount;
				int num = (columnsCount + 7) / 8;
				if (this._nullBitmap == null || this._nullBitmap.Length != num)
				{
					this._nullBitmap = new byte[num];
				}
				if (!stateObj.TryReadByteArray(this._nullBitmap, this._nullBitmap.Length))
				{
					return false;
				}
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("TdsParserStateObject.NullBitmap.Initialize | INFO | ADV | State Object Id {0}, NBCROW bitmap received, column count = {1}", stateObj.ObjectID, columnsCount);
				SqlClientEventSource.Log.TryAdvancedTraceBinEvent<int, byte[], ushort>("TdsParserStateObject.NullBitmap.Initialize | INFO | ADV | State Object Id {0}, NBCROW bitmap data. Null Bitmap {1}, Null bitmap length: {2}", stateObj.ObjectID, this._nullBitmap, (ushort)this._nullBitmap.Length);
				return true;
			}

			// Token: 0x0400148E RID: 5262
			private byte[] _nullBitmap;

			// Token: 0x0400148F RID: 5263
			private int _columnsCount;
		}

		// Token: 0x020001E7 RID: 487
		private class PacketData
		{
			// Token: 0x04001490 RID: 5264
			public byte[] Buffer;

			// Token: 0x04001491 RID: 5265
			public int Read;
		}

		// Token: 0x020001E8 RID: 488
		private class StateSnapshot
		{
			// Token: 0x06001DE5 RID: 7653 RVA: 0x0007B489 File Offset: 0x00079689
			public StateSnapshot(TdsParserStateObject state)
			{
				this._snapshotInBuffs = new List<TdsParserStateObject.PacketData>();
				this._stateObj = state;
			}

			// Token: 0x06001DE6 RID: 7654 RVA: 0x0007B4A3 File Offset: 0x000796A3
			internal void CloneNullBitmapInfo()
			{
				if (this._stateObj._nullBitmapInfo.ReferenceEquals(this._snapshotNullBitmapInfo))
				{
					this._stateObj._nullBitmapInfo = this._stateObj._nullBitmapInfo.Clone();
				}
			}

			// Token: 0x06001DE7 RID: 7655 RVA: 0x0007B4D8 File Offset: 0x000796D8
			internal void CloneCleanupAltMetaDataSetArray()
			{
				if (this._stateObj._cleanupAltMetaDataSetArray != null && this._snapshotCleanupAltMetaDataSetArray == this._stateObj._cleanupAltMetaDataSetArray)
				{
					this._stateObj._cleanupAltMetaDataSetArray = (_SqlMetaDataSetCollection)this._stateObj._cleanupAltMetaDataSetArray.Clone();
				}
			}

			// Token: 0x06001DE8 RID: 7656 RVA: 0x0007B528 File Offset: 0x00079728
			internal void PushBuffer(byte[] buffer, int read)
			{
				TdsParserStateObject.PacketData packetData = new TdsParserStateObject.PacketData();
				packetData.Buffer = buffer;
				packetData.Read = read;
				this._snapshotInBuffs.Add(packetData);
			}

			// Token: 0x06001DE9 RID: 7657 RVA: 0x0007B558 File Offset: 0x00079758
			internal bool Replay()
			{
				if (this._snapshotInBuffCurrent < this._snapshotInBuffs.Count)
				{
					TdsParserStateObject.PacketData packetData = this._snapshotInBuffs[this._snapshotInBuffCurrent];
					this._stateObj._inBuff = packetData.Buffer;
					this._stateObj._inBytesUsed = 0;
					this._stateObj._inBytesRead = packetData.Read;
					this._snapshotInBuffCurrent++;
					return true;
				}
				return false;
			}

			// Token: 0x06001DEA RID: 7658 RVA: 0x0007B5CC File Offset: 0x000797CC
			internal void Snap()
			{
				this._snapshotInBuffs.Clear();
				this._snapshotInBuffCurrent = 0;
				this._snapshotInBytesUsed = this._stateObj._inBytesUsed;
				this._snapshotInBytesPacket = this._stateObj._inBytesPacket;
				this._snapshotPendingData = this._stateObj._pendingData;
				this._snapshotErrorTokenReceived = this._stateObj._errorTokenReceived;
				this._snapshotMessageStatus = this._stateObj._messageStatus;
				this._snapshotNullBitmapInfo = this._stateObj._nullBitmapInfo;
				this._snapshotLongLen = this._stateObj._longlen;
				this._snapshotLongLenLeft = this._stateObj._longlenleft;
				this._snapshotCleanupMetaData = this._stateObj._cleanupMetaData;
				this._snapshotCleanupAltMetaDataSetArray = this._stateObj._cleanupAltMetaDataSetArray;
				this._snapshotHasOpenResult = this._stateObj._hasOpenResult;
				this._snapshotReceivedColumnMetadata = this._stateObj._receivedColMetaData;
				this._snapshotAttentionReceived = this._stateObj._attentionReceived;
				this.PushBuffer(this._stateObj._inBuff, this._stateObj._inBytesRead);
			}

			// Token: 0x06001DEB RID: 7659 RVA: 0x0007B6E4 File Offset: 0x000798E4
			internal void ResetSnapshotState()
			{
				this._snapshotInBuffCurrent = 0;
				this.Replay();
				this._stateObj._inBytesUsed = this._snapshotInBytesUsed;
				this._stateObj._inBytesPacket = this._snapshotInBytesPacket;
				this._stateObj._pendingData = this._snapshotPendingData;
				this._stateObj._errorTokenReceived = this._snapshotErrorTokenReceived;
				this._stateObj._messageStatus = this._snapshotMessageStatus;
				this._stateObj._nullBitmapInfo = this._snapshotNullBitmapInfo;
				this._stateObj._cleanupMetaData = this._snapshotCleanupMetaData;
				this._stateObj._cleanupAltMetaDataSetArray = this._snapshotCleanupAltMetaDataSetArray;
				if (!this._stateObj._hasOpenResult && this._snapshotHasOpenResult)
				{
					this._stateObj.IncrementAndObtainOpenResultCount(this._stateObj._executedUnderTransaction);
				}
				else if (this._stateObj._hasOpenResult && !this._snapshotHasOpenResult)
				{
					this._stateObj.DecrementOpenResultCount();
				}
				this._stateObj._receivedColMetaData = this._snapshotReceivedColumnMetadata;
				this._stateObj._attentionReceived = this._snapshotAttentionReceived;
				this._stateObj._bTmpRead = 0;
				this._stateObj._partialHeaderBytesRead = 0;
				this._stateObj._longlen = this._snapshotLongLen;
				this._stateObj._longlenleft = this._snapshotLongLenLeft;
				this._stateObj._snapshotReplay = true;
			}

			// Token: 0x06001DEC RID: 7660 RVA: 0x0007B83D File Offset: 0x00079A3D
			internal void PrepareReplay()
			{
				this.ResetSnapshotState();
			}

			// Token: 0x04001492 RID: 5266
			private List<TdsParserStateObject.PacketData> _snapshotInBuffs;

			// Token: 0x04001493 RID: 5267
			private int _snapshotInBuffCurrent;

			// Token: 0x04001494 RID: 5268
			private int _snapshotInBytesUsed;

			// Token: 0x04001495 RID: 5269
			private int _snapshotInBytesPacket;

			// Token: 0x04001496 RID: 5270
			private bool _snapshotPendingData;

			// Token: 0x04001497 RID: 5271
			private bool _snapshotErrorTokenReceived;

			// Token: 0x04001498 RID: 5272
			private bool _snapshotHasOpenResult;

			// Token: 0x04001499 RID: 5273
			private bool _snapshotReceivedColumnMetadata;

			// Token: 0x0400149A RID: 5274
			private bool _snapshotAttentionReceived;

			// Token: 0x0400149B RID: 5275
			private byte _snapshotMessageStatus;

			// Token: 0x0400149C RID: 5276
			private TdsParserStateObject.NullBitmap _snapshotNullBitmapInfo;

			// Token: 0x0400149D RID: 5277
			private ulong _snapshotLongLen;

			// Token: 0x0400149E RID: 5278
			private ulong _snapshotLongLenLeft;

			// Token: 0x0400149F RID: 5279
			private _SqlMetaDataSet _snapshotCleanupMetaData;

			// Token: 0x040014A0 RID: 5280
			private _SqlMetaDataSetCollection _snapshotCleanupAltMetaDataSetArray;

			// Token: 0x040014A1 RID: 5281
			private readonly TdsParserStateObject _stateObj;
		}
	}
}
