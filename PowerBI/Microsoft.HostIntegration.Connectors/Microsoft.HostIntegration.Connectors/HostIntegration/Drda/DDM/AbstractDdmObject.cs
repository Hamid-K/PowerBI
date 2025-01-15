using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000868 RID: 2152
	public abstract class AbstractDdmObject : IDdmObject
	{
		// Token: 0x06004445 RID: 17477 RVA: 0x000E56EC File Offset: 0x000E38EC
		protected AbstractDdmObject(IDatabase dataBase)
			: this()
		{
			this._database = dataBase;
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x000E56FB File Offset: 0x000E38FB
		protected AbstractDdmObject(IDatabase dataBase, int sqlamLevel)
			: this(dataBase)
		{
			this._sqlamLevel = sqlamLevel;
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x000E570B File Offset: 0x000E390B
		protected AbstractDdmObject(IDatabase dataBase, int sqlamLevel, string accrdbTypedefname)
			: this(dataBase, sqlamLevel)
		{
			this._accrdbTypedefname = accrdbTypedefname;
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x000E571C File Offset: 0x000E391C
		protected int DatabaseSessionId
		{
			get
			{
				if (this._database != null)
				{
					return this._database.SessionID;
				}
				return 0;
			}
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x000E5733 File Offset: 0x000E3933
		public AbstractDdmObject()
		{
			this.AutoFlush = true;
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x000E5742 File Offset: 0x000E3942
		// (set) Token: 0x0600444B RID: 17483 RVA: 0x000E574A File Offset: 0x000E394A
		public long Cmdsrcid
		{
			get
			{
				return this._cmdsrcid;
			}
			set
			{
				this._cmdsrcid = value;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x000E5753 File Offset: 0x000E3953
		// (set) Token: 0x0600444D RID: 17485 RVA: 0x000E575B File Offset: 0x000E395B
		public object TracePoint
		{
			get
			{
				return this._tracePoint;
			}
			set
			{
				this._tracePoint = value;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x000E5764 File Offset: 0x000E3964
		// (set) Token: 0x0600444F RID: 17487 RVA: 0x000E576C File Offset: 0x000E396C
		public DrdaFlavor DrdaFlavor
		{
			get
			{
				return this._flavor;
			}
			set
			{
				this._flavor = value;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x000E5775 File Offset: 0x000E3975
		// (set) Token: 0x06004451 RID: 17489 RVA: 0x000E577D File Offset: 0x000E397D
		public Action<AbstractDdmObject> Initializer
		{
			get
			{
				return this._initializer;
			}
			set
			{
				this._initializer = value;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x000E5786 File Offset: 0x000E3986
		// (set) Token: 0x06004453 RID: 17491 RVA: 0x000E578E File Offset: 0x000E398E
		public bool AutoFlush { get; set; }

		// Token: 0x06004454 RID: 17492 RVA: 0x000E5797 File Offset: 0x000E3997
		public void SetSqlAmLevel(int sqlAmLevel)
		{
			this._sqlamLevel = sqlAmLevel;
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x000E57A0 File Offset: 0x000E39A0
		public virtual void Reset()
		{
			this._cmdsrcid = 0L;
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x000E57AC File Offset: 0x000E39AC
		public virtual void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x000E57D3 File Offset: 0x000E39D3
		public virtual Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			throw new NotImplementedException("The ReadAsync() method is not implemented.");
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x000E57D3 File Offset: 0x000E39D3
		public virtual Task ReadAsync(DdmReader reader, bool isAsync, bool useAccelerator, CancellationToken cancellationToken)
		{
			throw new NotImplementedException("The ReadAsync() method is not implemented.");
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x000E57DF File Offset: 0x000E39DF
		public virtual void Write(DdmWriter writer)
		{
			throw new NotImplementedException("The Write() method is not supported.");
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x000E57EB File Offset: 0x000E39EB
		public void WriteRequestDss(DdmReader reader, DdmWriter writer)
		{
			this.WriteRequestDss(reader, writer, true);
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x000E57F6 File Offset: 0x000E39F6
		public Task WriteRequestDssAsync(DdmReader reader, DdmWriter writer, bool isAsync, CancellationToken cancellationToken)
		{
			return this.WriteRequestDssAsync(reader, writer, true, isAsync, cancellationToken);
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x000E5804 File Offset: 0x000E3A04
		public void WriteRequestDss(DdmReader reader, DdmWriter writer, bool endChain)
		{
			this.WriteRequestDssAsync(reader, writer, endChain, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x000E5830 File Offset: 0x000E3A30
		public async Task WriteRequestDssAsync(DdmReader reader, DdmWriter writer, bool endChain, bool isAsync, CancellationToken cancellationToken)
		{
			writer.CreateDssRequest(reader.DssCorrelationID);
			this.Write(writer);
			writer.WriteEndDss();
			if (endChain)
			{
				await writer.WriteEndChainAsync(reader.GetCurrentChainState(), this.AutoFlush, isAsync, cancellationToken);
			}
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x000E58A0 File Offset: 0x000E3AA0
		public async Task WriteRequestDssAsync(DdmWriter writer, int correlationId, byte chainState, bool isAsync, CancellationToken cancellationToken)
		{
			writer.CreateDssRequest(correlationId);
			this.Write(writer);
			writer.WriteEndDss();
			await writer.WriteEndChainAsync(chainState, this.AutoFlush, isAsync, cancellationToken);
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x000E590F File Offset: 0x000E3B0F
		public void WriteReplyDss(DdmReader reader, DdmWriter writer)
		{
			this.WriteReplyDss(reader, writer, true);
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x000E591A File Offset: 0x000E3B1A
		public Task WriteReplyDssAsync(DdmReader reader, DdmWriter writer, bool isAsync, CancellationToken cancellationToken)
		{
			return this.WriteReplyDssAsync(reader, writer, true, isAsync, cancellationToken);
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x000E5928 File Offset: 0x000E3B28
		public void WriteReplyDss(DdmReader reader, DdmWriter writer, bool endChain)
		{
			this.WriteReplyDssAsync(reader, writer, endChain, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x000E5954 File Offset: 0x000E3B54
		public async Task WriteReplyDssAsync(DdmReader reader, DdmWriter writer, bool endChain, bool isAsync, CancellationToken cancellationToken)
		{
			writer.CreateDssReply(reader.DssCorrelationID);
			this.Write(writer);
			writer.WriteEndDss();
			if (endChain)
			{
				await writer.WriteEndChainAsync(reader.GetCurrentChainState(), this.AutoFlush, isAsync, cancellationToken);
			}
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x000E59C3 File Offset: 0x000E3BC3
		public void WriteObjectDss(DdmReader reader, DdmWriter writer)
		{
			this.WriteObjectDss(reader, writer, true);
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x000E59CE File Offset: 0x000E3BCE
		public Task WriteObjectDssAsync(DdmReader reader, DdmWriter writer, bool isAsync, CancellationToken cancellationToken)
		{
			return this.WriteObjectDssAsync(reader, writer, true, isAsync, cancellationToken);
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x000E59DC File Offset: 0x000E3BDC
		public void WriteObjectDss(DdmReader reader, DdmWriter writer, bool endChain)
		{
			this.WriteObjectDss(reader, writer, endChain, reader.DssCorrelationID);
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x000E59ED File Offset: 0x000E3BED
		public Task WriteObjectDssAsync(DdmReader reader, DdmWriter writer, bool endChain, bool isAsync, CancellationToken cancellationToken)
		{
			return this.WriteObjectDssAsync(reader, writer, endChain, reader.DssCorrelationID, isAsync, cancellationToken);
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x000E5A02 File Offset: 0x000E3C02
		public void WriteObjectDss(DdmReader reader, DdmWriter writer, int corrid)
		{
			this.WriteObjectDss(reader, writer, true, corrid);
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x000E5A0E File Offset: 0x000E3C0E
		public Task WriteObjectDssAsync(DdmReader reader, DdmWriter writer, int corrid, bool isAsync, CancellationToken cancellationToken)
		{
			return this.WriteObjectDssAsync(reader, writer, true, corrid, isAsync, cancellationToken);
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x000E5A20 File Offset: 0x000E3C20
		public void WriteObjectDss(DdmReader reader, DdmWriter writer, bool endChain, int corrid)
		{
			this.WriteObjectDssAsync(reader, writer, endChain, corrid, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x000E5A4C File Offset: 0x000E3C4C
		public async Task WriteObjectDssAsync(DdmReader reader, DdmWriter writer, bool endChain, int corrid, bool isAsync, CancellationToken cancellationToken)
		{
			writer.CreateDssObject(corrid);
			this.Write(writer);
			writer.WriteEndDss();
			if (endChain)
			{
				await writer.WriteEndChainAsync(reader.GetCurrentChainState(), this.AutoFlush, isAsync, cancellationToken);
			}
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x000E5AC4 File Offset: 0x000E3CC4
		public async Task WriteObjectDssAsync(DdmWriter writer, int corrid, byte chainState, bool isAsync, CancellationToken cancellationToken)
		{
			writer.CreateDssObject(corrid);
			this.Write(writer);
			writer.WriteEndDss();
			await writer.WriteEndChainAsync(chainState, isAsync, this.AutoFlush, cancellationToken);
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x000E5B34 File Offset: 0x000E3D34
		protected static void CheckLength(DdmReader reader, CodePoint cp, int len)
		{
			long ddmObjectLength = reader.DdmObjectLength;
			if (ddmObjectLength < (long)len)
			{
				DrdaException.BadObjectLength(cp);
				return;
			}
			if (ddmObjectLength > (long)len)
			{
				DrdaException.TooBig(cp);
			}
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x000E5B60 File Offset: 0x000E3D60
		protected string CodePointToString(CodePoint cp, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (args != null)
			{
				for (int i = 0; i < args.Length; i++)
				{
					stringBuilder.Append(args[i]);
				}
			}
			stringBuilder.Append(cp);
			return stringBuilder.ToString();
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x000E5BA1 File Offset: 0x000E3DA1
		protected void LogCodePoint(CodePoint cp)
		{
			Logger.Verbose(this._tracePoint, this.DatabaseSessionId, this.CodePointToString(cp, new object[] { "  Reading CodePoint:" }), Array.Empty<object>());
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x000E5BCE File Offset: 0x000E3DCE
		protected Task<string> ParseEncodedStringAsync(DdmReader reader, Ccsid ccsid, int sqlamLevel, bool isAsync, CancellationToken cancellationToken)
		{
			if (sqlamLevel < 7)
			{
				return this.ParseVCMorVCSAsync(reader, ccsid, isAsync, cancellationToken);
			}
			return this.ParseNOCMorNOCSAsync(reader, ccsid, isAsync, cancellationToken);
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x000E5BF0 File Offset: 0x000E3DF0
		protected async Task<string> ParseVCMorVCSAsync(DdmReader reader, Ccsid ccsid, bool isAsync, CancellationToken cancellationToken)
		{
			string strVal = null;
			int num = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			if (num > 0)
			{
				strVal = await reader.ReadStringAsync(num, ccsid._ccsidmbc, 60, isAsync, cancellationToken);
			}
			int num2 = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			if (num2 == 0 && this._accrdbTypedefname == "QTDSQL400" && this._sqlamLevel <= 6)
			{
				num2 = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			}
			if (num2 > 0)
			{
				strVal = await reader.ReadStringAsync(num2, ccsid._ccsidsbc, 48, isAsync, cancellationToken);
			}
			return strVal;
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x000E5C58 File Offset: 0x000E3E58
		protected async Task<string> ParseNOCMorNOCSAsync(DdmReader reader, Ccsid ccsid, bool isAsync, CancellationToken cancellationToken)
		{
			int num = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
			string strVal = null;
			if (num != 255)
			{
				int num2 = await reader.ReadNetworkIntAsync(isAsync, cancellationToken);
				if (num2 != 0)
				{
					strVal = await reader.ReadStringAsync(num2, ccsid._ccsidmbc, 60, isAsync, cancellationToken);
				}
			}
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				int num2 = await reader.ReadNetworkIntAsync(isAsync, cancellationToken);
				if (num2 != 0)
				{
					strVal = await reader.ReadStringAsync(num2, ccsid._ccsidsbc, 48, isAsync, cancellationToken);
				}
			}
			return strVal;
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x000E5CB8 File Offset: 0x000E3EB8
		protected async Task<string> ParseNVCMorNVCSAsync(DdmReader reader, Ccsid ccsid, bool isAsync, CancellationToken cancellationToken)
		{
			int num = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
			string strVal = null;
			if (num != 255)
			{
				int num2 = await reader.ReadNetworkShortAsync(isAsync, cancellationToken);
				if (num2 != 0)
				{
					strVal = await reader.ReadStringAsync(num2, ccsid._ccsidmbc, 60, isAsync, cancellationToken);
				}
			}
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				int num2 = await reader.ReadNetworkShortAsync(isAsync, cancellationToken);
				if (num2 != 0)
				{
					strVal = await reader.ReadStringAsync(num2, ccsid._ccsidsbc, 48, isAsync, cancellationToken);
				}
			}
			return strVal;
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x000E5D18 File Offset: 0x000E3F18
		protected void WriteNOCMorNOCS(DdmWriter writer, string s)
		{
			if (s == null)
			{
				writer.WriteByte(255);
				writer.WriteByte(255);
				return;
			}
			writer.WriteByte(0);
			int num = writer.GenerateStringMBCS(s);
			writer.WriteInt32(num, EndianType.BigEndian);
			if (num > 0)
			{
				writer.WriteBytes(num);
			}
			writer.WriteByte(255);
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x000E5D6C File Offset: 0x000E3F6C
		protected void WriteVCMorVCS(DdmWriter writer, string s)
		{
			if (s == null)
			{
				writer.WriteInt16(0);
				writer.WriteInt16(0);
				return;
			}
			writer.WriteLDString(s);
			if (this._accrdbTypedefname != "QTDSQL370")
			{
				writer.WriteInt16(0);
			}
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x000E5DA0 File Offset: 0x000E3FA0
		protected string FixParenthis(string s)
		{
			if (s.Contains("{"))
			{
				s = s.Replace("{", "{{");
			}
			if (s.Contains("}"))
			{
				s = s.Replace("}", "}}");
			}
			return s;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x000E5DEC File Offset: 0x000E3FEC
		protected void InitializeCodepoint(AbstractDdmObject codepoint)
		{
			if (this._initializer != null)
			{
				this._initializer(codepoint);
			}
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000E5E02 File Offset: 0x000E4002
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x000E5E0A File Offset: 0x000E400A
		public void WriteCommandSourceId(DdmWriter writer)
		{
			if (this._cmdsrcid > 0L)
			{
				writer.WriteScalar8Bytes(CodePoint.CMDSRCID, this._cmdsrcid);
			}
		}

		// Token: 0x04002FDE RID: 12254
		protected string _accrdbTypedefname;

		// Token: 0x04002FDF RID: 12255
		private const byte NULL_VALUE = 255;

		// Token: 0x04002FE0 RID: 12256
		protected IDatabase _database;

		// Token: 0x04002FE1 RID: 12257
		protected int _sqlamLevel;

		// Token: 0x04002FE2 RID: 12258
		protected long _cmdsrcid;

		// Token: 0x04002FE3 RID: 12259
		protected object _tracePoint;

		// Token: 0x04002FE4 RID: 12260
		protected DrdaFlavor _flavor;

		// Token: 0x04002FE5 RID: 12261
		protected Action<AbstractDdmObject> _initializer;
	}
}
