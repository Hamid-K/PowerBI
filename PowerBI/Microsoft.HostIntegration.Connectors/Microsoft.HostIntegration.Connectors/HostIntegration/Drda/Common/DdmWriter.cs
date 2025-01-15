using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Common;
using Microsoft.HostIntegration.PerformanceCounters;
using Microsoft.HostIntegration.PerformanceCounters.Drda;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007FE RID: 2046
	public sealed class DdmWriter
	{
		// Token: 0x0600403D RID: 16445 RVA: 0x000DA596 File Offset: 0x000D8796
		public DdmWriter(Stream stream, ICcsidManager ccsidManager, Converter converter, CommonDrdaPerformanceCountersContainer perfContainer)
			: this(stream, ccsidManager, converter, perfContainer, 0)
		{
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000DA5A4 File Offset: 0x000D87A4
		public DdmWriter(Stream stream, ICcsidManager ccsidManager, Converter converter, CommonDrdaPerformanceCountersContainer perfContainer, int id)
			: this(stream, ccsidManager, converter, perfContainer, id, null)
		{
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000DA5B4 File Offset: 0x000D87B4
		public DdmWriter(Stream stream, ICcsidManager ccsidManager, Converter converter, CommonDrdaPerformanceCountersContainer perfContainer, int id, object tracePoint)
		{
			this._stream = stream;
			this._converter = converter;
			this._markStack = new Stack<int>(10);
			this._outputBuffer = ByteArrayPool.Get();
			this._workingBuffer = ByteArrayPool.Get();
			this._ccsidManager = ccsidManager;
			this._prevHeaderLocation = -1;
			this._previousCorrelationId = -1;
			this._previousChainByte = 0;
			this._isContinuationDss = false;
			this._lastDssBeforeMark = -1;
			this._sessionId = id;
			this._tracePoint = tracePoint;
			if (perfContainer != null)
			{
				this.CreatePerformanceCounters(perfContainer);
			}
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000DA64C File Offset: 0x000D884C
		public void Reset(Stream stream)
		{
			this._stream = stream;
			if (this._outputBuffer != null)
			{
				ByteArrayPool.Put(this._outputBuffer);
			}
			if (this._workingBuffer != null)
			{
				ByteArrayPool.Put(this._workingBuffer);
			}
			this._outputBuffer = ByteArrayPool.Get();
			this._workingBuffer = ByteArrayPool.Get();
			this.Reset();
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x000DA6A4 File Offset: 0x000D88A4
		private void CreatePerformanceCounters(CommonDrdaPerformanceCountersContainer perfContainer)
		{
			try
			{
				object obj = DdmWriter.perfmonLockObject;
				lock (obj)
				{
					if (DdmWriter.CommonPerformanceContainer == null)
					{
						DdmWriter.CommonPerformanceContainer = perfContainer;
						DdmWriter.bytesSentPerSecond = DdmWriter.CommonPerformanceContainer.GetPerformanceCounter(CommonDrdaPerformanceCounter.BytesSentPerSecond) as PerSecondCounter;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x000DA710 File Offset: 0x000D8910
		public Converter Converter
		{
			get
			{
				return this._converter;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x000DA718 File Offset: 0x000D8918
		// (set) Token: 0x06004044 RID: 16452 RVA: 0x000DA720 File Offset: 0x000D8920
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

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x000DA73A File Offset: 0x000D893A
		// (set) Token: 0x06004046 RID: 16454 RVA: 0x000DA742 File Offset: 0x000D8942
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

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000DA74B File Offset: 0x000D894B
		// (set) Token: 0x06004048 RID: 16456 RVA: 0x000DA753 File Offset: 0x000D8953
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

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x000DA75C File Offset: 0x000D895C
		public int CorrelationID
		{
			get
			{
				return this._correlationID;
			}
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x000DA764 File Offset: 0x000D8964
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
				ByteArrayPool.Put(this._outputBuffer);
				ByteArrayPool.Put(this._workingBuffer);
				this._outputBuffer = null;
				this._workingBuffer = null;
			}
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000DA7BC File Offset: 0x000D89BC
		public void WriteBeginDdm(CodePoint cp)
		{
			this._markStack.Push(this._offset);
			this.EnsureLength(4);
			this._offset += 2;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x000DA814 File Offset: 0x000D8A14
		public void WriteEndDdm()
		{
			int num = this._markStack.Pop();
			int num2 = this._offset - num;
			int num3 = this.CalculateBytesInExtendedLength((long)num2);
			if (num3 != 0)
			{
				this.EnsureLength(num3);
				int num4 = num2 - 4;
				int num5 = num + 4;
				Buffer.BlockCopy(this._outputBuffer, num5, this._outputBuffer, num5 + num3, num4);
				Converter.IntToBytes((long)num4, this._outputBuffer, num3, num5, EndianType.BigEndian);
				this._offset += num3;
				num2 = num3 + 4;
				num2 |= 32768;
			}
			Converter.IntToBytes((long)num2, this._outputBuffer, 2, num, EndianType.BigEndian);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x000DA8A8 File Offset: 0x000D8AA8
		public void WriteEndDss(byte chainByte)
		{
			this.WriteEndDss(true);
			byte[] outputBuffer = this._outputBuffer;
			int num = this._dssLengthOffset + 3;
			outputBuffer[num] &= 15;
			byte[] outputBuffer2 = this._outputBuffer;
			int num2 = this._dssLengthOffset + 3;
			outputBuffer2[num2] |= chainByte;
			this._previousChainByte = chainByte;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x000DA8F6 File Offset: 0x000D8AF6
		public void WriteEndDss()
		{
			this.WriteEndDss(true);
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x000DA8FF File Offset: 0x000D8AFF
		public void WriteEndDss(bool final)
		{
			if (final)
			{
				this.FinalizeDssLength();
			}
			if (this._isContinuationDss)
			{
				this._isContinuationDss = false;
				return;
			}
			this._previousCorrelationId = this._correlationID;
			this._prevHeaderLocation = this._dssLengthOffset;
			this._previousChainByte = 80;
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000DA93A File Offset: 0x000D8B3A
		public void WriteEndDdmAndDss()
		{
			this.WriteEndDdm();
			this.WriteEndDss();
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x000DA948 File Offset: 0x000D8B48
		public void CreateDssRequest()
		{
			this.BeginDss(1, true);
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000DA952 File Offset: 0x000D8B52
		public void CreateDssReply()
		{
			this.BeginDss(2, true);
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x000DA95C File Offset: 0x000D8B5C
		public void CreateDssObject()
		{
			this.BeginDss(3, true);
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x000DA966 File Offset: 0x000D8B66
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06004055 RID: 16469 RVA: 0x000DA96E File Offset: 0x000D8B6E
		public int DssLengthOffset
		{
			get
			{
				return this._dssLengthOffset;
			}
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000DA976 File Offset: 0x000D8B76
		public void CreateDssObject(int corrId)
		{
			this.BeginDss(corrId, 3, true);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x000DA981 File Offset: 0x000D8B81
		public void CreateDssReply(int corrId)
		{
			this.BeginDss(corrId, 2, true);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000DA98C File Offset: 0x000D8B8C
		public void CreateDssRequest(int corrId)
		{
			this.BeginDss(corrId, 1, true);
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000DA997 File Offset: 0x000D8B97
		public void BeginDss(int dssType, bool checkLength)
		{
			this.BeginDss(-1, dssType, checkLength);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x000DA9A4 File Offset: 0x000D8BA4
		private void BeginDss(int corrId, int dssType, bool checkLength)
		{
			this._dssLengthOffset = this._offset;
			if (checkLength)
			{
				this.EnsureLength(6);
			}
			this._offset += 2;
			this._outputBuffer[this._offset] = 208;
			this._outputBuffer[this._offset + 1] = (byte)dssType;
			byte[] outputBuffer = this._outputBuffer;
			int num = this._offset + 1;
			outputBuffer[num] |= 80;
			if (corrId == -1)
			{
				this._correlationID = this.GetCorrelationID();
			}
			else
			{
				this._correlationID = this.GetCorrelationID(corrId);
			}
			Converter.IntToBytes((long)this._correlationID, this._outputBuffer, 2, this._offset + 2, EndianType.BigEndian);
			this._offset += 4;
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x000DAA5C File Offset: 0x000D8C5C
		public void BeginDss(bool chainedToNextStructure, int dssType)
		{
			this.BeginDss(dssType, false);
			this._outputBuffer[this._dssLengthOffset] = byte.MaxValue;
			this._outputBuffer[this._dssLengthOffset + 1] = byte.MaxValue;
			if (chainedToNextStructure)
			{
				dssType |= 80;
			}
			this._outputBuffer[this._dssLengthOffset + 3] = (byte)(dssType & 255);
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x000DAAB7 File Offset: 0x000D8CB7
		public void WriteBool(bool ival)
		{
			this.WriteByte(ival ? 1 : 0);
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x000DAAC8 File Offset: 0x000D8CC8
		public void WriteByte(int i)
		{
			this.EnsureLength(1);
			byte[] outputBuffer = this._outputBuffer;
			int offset = this._offset;
			this._offset = offset + 1;
			outputBuffer[offset] = (byte)(i & 255);
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x000DAAFC File Offset: 0x000D8CFC
		public void WriteInt16(int val)
		{
			this.WriteInt16(val, this._enddianType);
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000DAB0B File Offset: 0x000D8D0B
		public void WriteInt16(int val, EndianType endian)
		{
			this.EnsureLength(2);
			Converter.IntToBytes((long)val, this._outputBuffer, 2, this._offset, endian);
			this._offset += 2;
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000DAB37 File Offset: 0x000D8D37
		public void WriteInt32(int val)
		{
			this.WriteInt32(val, this._enddianType);
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x000DAB46 File Offset: 0x000D8D46
		public void WriteInt32(int val, EndianType endian)
		{
			this.EnsureLength(4);
			Converter.IntToBytes((long)val, this._outputBuffer, 4, this._offset, endian);
			this._offset += 4;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000DAB72 File Offset: 0x000D8D72
		public void WriteInt64(long val)
		{
			this.WriteInt64(val, this._enddianType);
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000DAB81 File Offset: 0x000D8D81
		public void WriteInt64(long val, EndianType endian)
		{
			this.EnsureLength(8);
			Converter.IntToBytes(val, this._outputBuffer, 8, this._offset, endian);
			this._offset += 8;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000DABAC File Offset: 0x000D8DAC
		public void WriteCodePointAnd2Bytes(CodePoint cp, int val, EndianType endianType)
		{
			this.EnsureLength(4);
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, endianType);
			this._offset += 2;
			Converter.IntToBytes((long)val, this._outputBuffer, 2, this._offset, endianType);
			this._offset += 2;
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000DAC06 File Offset: 0x000D8E06
		public void WriteLDString(string s)
		{
			this.WriteLDString(s, 48, string.IsNullOrEmpty(s) ? 0 : s.Length);
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000DAC24 File Offset: 0x000D8E24
		public void WriteLDString(string s, int drdaType, int length, int codepageOverride)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.WriteInt16(0, EndianType.BigEndian);
				return;
			}
			this.EnsureWorkingBuffer(s.Length);
			int num = 0;
			this._converter.PackString(s, codepageOverride, this._workingBuffer, ref num, 0, (DrdaTypes)drdaType);
			int num2 = Math.Min(32700, num);
			if (num2 != num)
			{
				while ((this._workingBuffer[num2 - 1] & 192) == 128)
				{
					num2--;
					num2--;
				}
			}
			if (num2 < length)
			{
				this.WriteInt16(num2, EndianType.BigEndian);
				this.WriteBytes(this._workingBuffer, num2);
				return;
			}
			this.WriteInt16(length, EndianType.BigEndian);
			this.WriteBytes(this._workingBuffer, length);
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000DACC8 File Offset: 0x000D8EC8
		private void EnsureWorkingBuffer(int length)
		{
			if (length < this._workingBuffer.Length)
			{
				return;
			}
			this._workingBuffer = new byte[length];
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000DACE2 File Offset: 0x000D8EE2
		public void WriteLDString(string s, int drdaType, int length)
		{
			this.WriteLDString(s, drdaType, length, (this._ccsid == null) ? 1208 : this._ccsid._ccsidsbc);
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x000DAD07 File Offset: 0x000D8F07
		public void WriteLDStringDBCS(string s, int drdaType)
		{
			this.WriteLDStringDBCS(s, this._enddianType, drdaType);
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x000DAD18 File Offset: 0x000D8F18
		public void WriteLDStringDBCS(string s, EndianType endian, int drdaType)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.WriteInt16(0, EndianType.BigEndian);
				return;
			}
			this.EnsureWorkingBuffer(s.Length * 2);
			int num = 0;
			this._converter.PackString(s, (this._ccsid == null) ? 1200 : this._ccsid._ccsiddbc, this._workingBuffer, ref num, 0, (DrdaTypes)drdaType);
			this.WriteInt16(s.Length, EndianType.BigEndian);
			if (endian == EndianType.LittleEndian)
			{
				this.SwapBytesForPackedString(ref this._workingBuffer, s.Length * 2);
			}
			this.WriteBytes(this._workingBuffer, s.Length * 2);
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x000DADB0 File Offset: 0x000D8FB0
		private void SwapBytesForPackedString(ref byte[] bytes, int length)
		{
			if (length % 2 != 0)
			{
				throw new Exception("String bytes not right");
			}
			for (int i = 0; i < length; i += 2)
			{
				byte b = bytes[i];
				bytes[i] = bytes[i + 1];
				bytes[i + 1] = b;
			}
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x000DADF1 File Offset: 0x000D8FF1
		public void WriteStringDBCS(string s, int drdaType)
		{
			this.WriteStringDBCS(s, this._enddianType, drdaType);
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000DAE01 File Offset: 0x000D9001
		public void WriteStringDBCS(string s, EndianType endian, int drdaType)
		{
			this.WriteStringDBCS(s, endian, drdaType, (this._ccsid == null) ? 1200 : this._ccsid._ccsiddbc);
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x000DAE28 File Offset: 0x000D9028
		public void WriteStringDBCS(string s, EndianType endian, int drdaType, int overrideCcsid)
		{
			if (string.IsNullOrEmpty(s))
			{
				return;
			}
			this.EnsureWorkingBuffer(s.Length * 2);
			int num = 0;
			this._converter.PackString(s, overrideCcsid, this._workingBuffer, ref num, 0, (DrdaTypes)drdaType);
			if (endian == EndianType.LittleEndian)
			{
				this.SwapBytesForPackedString(ref this._workingBuffer, s.Length * 2);
			}
			this.WriteBytes(this._workingBuffer, s.Length * 2);
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x000DAE92 File Offset: 0x000D9092
		public void WriteStringSBCS(string s, int drdaType)
		{
			this.WriteStringSBCS(s, drdaType, (this._ccsid == null) ? 1208 : this._ccsid._ccsidsbc);
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x000DAEB8 File Offset: 0x000D90B8
		public void WriteStringSBCS(string s, int drdaType, int overrideCcsid)
		{
			if (string.IsNullOrEmpty(s))
			{
				return;
			}
			this.EnsureWorkingBuffer(s.Length);
			int num = 0;
			this._converter.PackString(s, overrideCcsid, this._workingBuffer, ref num, 0, (DrdaTypes)drdaType);
			this.WriteBytes(this._workingBuffer, s.Length);
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x000DAF05 File Offset: 0x000D9105
		public void WriteStringSBCS(string s)
		{
			this.WriteStringSBCS(s, 48);
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x000DAF10 File Offset: 0x000D9110
		public int GenerateStringMBCS(string s)
		{
			return this.GenerateStringMBCS(s, (this._ccsid == null) ? 1208 : this._ccsid._ccsidmbc);
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000DAF34 File Offset: 0x000D9134
		public int GenerateStringMBCS(string s, int overrideEncoding)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0;
			}
			int num = this._workingBuffer.Length / s.Length;
			if (num < 2)
			{
				num = 2;
			}
			int num2 = 0;
			do
			{
				this.EnsureWorkingBuffer(s.Length * num);
				num2 = 0;
				try
				{
					this._converter.PackStringAsIs(s, overrideEncoding, this._workingBuffer, ref num2, DrdaTypes.DRDA_TYPE_CHAR);
					break;
				}
				catch (CustomHISException ex)
				{
					if (num >= 4)
					{
						throw ex;
					}
				}
				num++;
			}
			while (num <= 4);
			return num2;
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x000DAFB0 File Offset: 0x000D91B0
		public void WriteScalar1Byte(CodePoint cp, int val)
		{
			this.EnsureLength(5);
			byte[] outputBuffer = this._outputBuffer;
			int num = this._offset;
			this._offset = num + 1;
			outputBuffer[num] = 0;
			byte[] outputBuffer2 = this._outputBuffer;
			num = this._offset;
			this._offset = num + 1;
			outputBuffer2[num] = 5;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			byte[] outputBuffer3 = this._outputBuffer;
			num = this._offset;
			this._offset = num + 1;
			outputBuffer3[num] = (byte)(val & 255);
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x000DB03C File Offset: 0x000D923C
		public void WriteScalar2Bytes(CodePoint cp, int val)
		{
			this.EnsureLength(6);
			byte[] outputBuffer = this._outputBuffer;
			int num = this._offset;
			this._offset = num + 1;
			outputBuffer[num] = 0;
			byte[] outputBuffer2 = this._outputBuffer;
			num = this._offset;
			this._offset = num + 1;
			outputBuffer2[num] = 6;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes((long)val, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x000DB0C8 File Offset: 0x000D92C8
		public void WriteScalar4Bytes(CodePoint cp, int val)
		{
			this.EnsureLength(8);
			byte[] outputBuffer = this._outputBuffer;
			int num = this._offset;
			this._offset = num + 1;
			outputBuffer[num] = 0;
			byte[] outputBuffer2 = this._outputBuffer;
			num = this._offset;
			this._offset = num + 1;
			outputBuffer2[num] = 8;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes((long)val, this._outputBuffer, 4, this._offset, EndianType.BigEndian);
			this._offset += 4;
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000DB154 File Offset: 0x000D9354
		public void WriteScalar8Bytes(CodePoint cp, long val)
		{
			this.EnsureLength(12);
			byte[] outputBuffer = this._outputBuffer;
			int num = this._offset;
			this._offset = num + 1;
			outputBuffer[num] = 0;
			byte[] outputBuffer2 = this._outputBuffer;
			num = this._offset;
			this._offset = num + 1;
			outputBuffer2[num] = 12;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes(val, this._outputBuffer, 8, this._offset, EndianType.BigEndian);
			this._offset += 8;
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000DB1E1 File Offset: 0x000D93E1
		public void WriteScalar2Bytes(int val)
		{
			this.EnsureLength(2);
			Converter.IntToBytes((long)val, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000DB210 File Offset: 0x000D9410
		public void WriteScalarHeader(CodePoint cp, int dataLength)
		{
			this.EnsureLength(dataLength + 4);
			Converter.IntToBytes((long)(dataLength + 4), this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x000DB270 File Offset: 0x000D9470
		public void WriteScalarBytes(CodePoint cp, byte[] buf)
		{
			int num = buf.Length;
			this.EnsureLength(num + 4);
			Converter.IntToBytes((long)(num + 4), this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Buffer.BlockCopy(buf, 0, this._outputBuffer, this._offset, num);
			this._offset += num;
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000DB2F4 File Offset: 0x000D94F4
		public void WriteScalarBytes(CodePoint cp, byte[] buf, int bufLength)
		{
			this.EnsureLength(bufLength + 4);
			Converter.IntToBytes((long)(bufLength + 4), this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Buffer.BlockCopy(buf, 0, this._outputBuffer, this._offset, bufLength);
			this._offset += bufLength;
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x000DB374 File Offset: 0x000D9574
		public void WriteScalarPaddedBytes(CodePoint cp, byte[] buf, int paddedLength, byte padByte)
		{
			int num = buf.Length;
			this.EnsureLength(paddedLength + 4);
			Converter.IntToBytes((long)(paddedLength + 4), this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Buffer.BlockCopy(buf, 0, this._outputBuffer, this._offset, num);
			this._offset += num;
			int num2 = paddedLength - num;
			DdmWriter.Fill(this._outputBuffer, this._offset, this._offset + num2, padByte);
			this._offset += num2;
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000DB42C File Offset: 0x000D962C
		public void WriteScalarPaddedString(string s, int paddedLength)
		{
			this.EnsureWorkingBuffer((s.Length < paddedLength) ? paddedLength : s.Length);
			int num = 0;
			this.Converter.PackString(s, (this._ccsid == null) ? 1208 : this._ccsid._ccsidsbc, this._workingBuffer, ref num, (s.Length < paddedLength) ? paddedLength : s.Length, DrdaTypes.DRDA_TYPE_CHAR);
			this.WriteBytes(this._workingBuffer, num);
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000DB4A4 File Offset: 0x000D96A4
		public void WriteScalarPaddedString(string s, int paddedLength, int encoding)
		{
			this.EnsureWorkingBuffer((s.Length < paddedLength) ? paddedLength : s.Length);
			int num = 0;
			this.Converter.PackString(s, encoding, this._workingBuffer, ref num, (s.Length < paddedLength) ? paddedLength : s.Length, DrdaTypes.DRDA_TYPE_CHAR);
			this.WriteBytes(this._workingBuffer, num);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000DB504 File Offset: 0x000D9704
		public void WriteScalarString(CodePoint cp, string s, int encoding)
		{
			this.EnsureWorkingBuffer(s.Length);
			int num = 0;
			this.Converter.PackString(s, encoding, this._workingBuffer, ref num, s.Length, DrdaTypes.DRDA_TYPE_CHAR);
			this.WriteScalarBytes(cp, this._workingBuffer, num);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000DB54C File Offset: 0x000D974C
		public void WriteScalarPaddedString(CodePoint cp, string s, int paddedLength)
		{
			this.EnsureWorkingBuffer((s.Length < paddedLength) ? paddedLength : s.Length);
			int num = 0;
			this.Converter.PackString(s, (this._ccsid == null) ? 1208 : this._ccsid._ccsidsbc, this._workingBuffer, ref num, (s.Length < paddedLength) ? paddedLength : s.Length, DrdaTypes.DRDA_TYPE_CHAR);
			this.WriteScalarBytes(cp, this._workingBuffer, num);
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000DB5C4 File Offset: 0x000D97C4
		public void WriteScalarPaddedBytes(byte[] buf, int paddedLength, byte padByte)
		{
			int num = buf.Length;
			int num2 = paddedLength - num;
			this.EnsureLength(paddedLength);
			Buffer.BlockCopy(buf, 0, this._outputBuffer, this._offset, num);
			this._offset += num;
			DdmWriter.Fill(this._outputBuffer, this._offset, this._offset + num2, padByte);
			this._offset += num2;
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000DB630 File Offset: 0x000D9830
		public void WriteStringSBCS(string s, int length, int drdaType, int codepageOverride)
		{
			if (s == null)
			{
				return;
			}
			this.EnsureWorkingBuffer(s.Length);
			int num = 0;
			this._converter.PackString(s, codepageOverride, this._workingBuffer, ref num, length, (DrdaTypes)drdaType);
			if (num > length)
			{
				this.WriteBytes(this._workingBuffer, length);
				return;
			}
			this.WriteBytes(this._workingBuffer, num);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000DB686 File Offset: 0x000D9886
		public void WriteBytes(byte[] bytes, int len)
		{
			this.WriteBytes(bytes, 0, len);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000DB691 File Offset: 0x000D9891
		public void WriteBytes(byte[] bytes, int start, int length)
		{
			this.EnsureLength(length);
			Buffer.BlockCopy(bytes, start, this._outputBuffer, this._offset, length);
			this._offset += length;
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000DB6BC File Offset: 0x000D98BC
		public void WriteBytes(int length)
		{
			this.EnsureLength(length);
			Buffer.BlockCopy(this._workingBuffer, 0, this._outputBuffer, this._offset, length);
			this._offset += length;
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x000DB6EC File Offset: 0x000D98EC
		public void WriteDecimal(decimal value, int precision, int scale)
		{
			this._converter.PackDecimal(value, this._workingBuffer, precision, scale);
			this.WriteBytes(this._workingBuffer, precision / 2 + 1);
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x000DB713 File Offset: 0x000D9913
		public void WriteBytes(byte[] bytes)
		{
			this.WriteBytes(bytes, bytes.Length);
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x000DB71F File Offset: 0x000D991F
		public void WriteLDBytes(byte[] bytes)
		{
			this.WriteInt16(bytes.Length, EndianType.BigEndian);
			this.WriteBytes(bytes, bytes.Length);
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000DB738 File Offset: 0x000D9938
		public void WriteScalar(CodePoint cp, string s)
		{
			int length = s.Length;
			this.EnsureLength(length * 2 + 4);
			Converter.IntToBytes((long)(length + 4), this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			Converter.IntToBytes((long)cp, this._outputBuffer, 2, this._offset, EndianType.BigEndian);
			this._offset += 2;
			this._offset = this._ccsidManager.GetBytes(s, this._outputBuffer, this._offset);
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000DB7C0 File Offset: 0x000D99C0
		public void Flush(byte[] bytes)
		{
			this.FlushAsync(bytes, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x000DB7E8 File Offset: 0x000D99E8
		public async Task FlushAsync(byte[] bytes, bool isAsync, CancellationToken cancellationToken)
		{
			try
			{
				this.LogDataStream(bytes, bytes.Length);
				if (isAsync)
				{
					await this._stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
					await this._stream.FlushAsync(cancellationToken);
				}
				else
				{
					this._stream.Write(bytes, 0, bytes.Length);
					this._stream.Flush();
				}
				if (DdmWriter.bytesSentPerSecond != null)
				{
					DdmWriter.bytesSentPerSecond.IncrementBy(bytes.Length);
				}
			}
			finally
			{
				this.Reset();
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x000DB848 File Offset: 0x000D9A48
		public void Flush()
		{
			this.FlushAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x000DB870 File Offset: 0x000D9A70
		public async Task FlushAsync(bool isAsync, CancellationToken cancellationToken)
		{
			try
			{
				this.LogDataStream(this._outputBuffer, this._offset);
				if (isAsync)
				{
					await this._stream.WriteAsync(this._outputBuffer, 0, this._offset, cancellationToken);
					await this._stream.FlushAsync(cancellationToken);
				}
				else
				{
					this._stream.Write(this._outputBuffer, 0, this._offset);
					this._stream.Flush();
				}
				if (DdmWriter.bytesSentPerSecond != null)
				{
					DdmWriter.bytesSentPerSecond.IncrementBy(this._offset);
				}
			}
			finally
			{
				this.Reset();
			}
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x000DB8C8 File Offset: 0x000D9AC8
		private void LogDataStream(byte[] data, int length)
		{
			if (Logger.maxTracingLevel == 6)
			{
				bool flag = true;
				DrdaCommonTracePoint drdaCommonTracePoint = this._tracePoint as DrdaCommonTracePoint;
				if (drdaCommonTracePoint != null && !drdaCommonTracePoint.IsEnabled(TraceFlags.Data))
				{
					flag = false;
				}
				if (flag)
				{
					int num = 0;
					while (num + 9 < length)
					{
						int num2 = (int)data[num] * 256 + (int)data[num + 1];
						if (num2 < 1)
						{
							break;
						}
						StringBuilder stringBuilder = new StringBuilder("SEND BUFFER: ");
						stringBuilder.Append(Enum.GetName(typeof(CodePoint), (int)data[num + 8] * 256 + (int)data[num + 9]));
						Logger.DataStream(this._tracePoint, this._sessionId, stringBuilder, data, num, num2);
						num += num2;
					}
				}
			}
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x000DB974 File Offset: 0x000D9B74
		public void WriteEndChain(byte chainByte)
		{
			this.WriteEndChainAsync(chainByte, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x000DB99C File Offset: 0x000D9B9C
		public async Task WriteEndChainAsync(byte chainByte, bool isAsync, CancellationToken cancellationToken)
		{
			await this.WriteEndChainAsync(chainByte, true, isAsync, cancellationToken);
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000DB9FC File Offset: 0x000D9BFC
		public async Task WriteEndChainAsync(byte chainByte, bool flush, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._prevHeaderLocation != -1)
			{
				byte[] outputBuffer = this._outputBuffer;
				int num = this._prevHeaderLocation + 3;
				outputBuffer[num] &= 15;
				byte[] outputBuffer2 = this._outputBuffer;
				int num2 = this._prevHeaderLocation + 3;
				outputBuffer2[num2] |= chainByte;
			}
			this._previousChainByte = chainByte;
			if ((chainByte & 64) != 64)
			{
				this.ResetChainState();
				if (flush && this._offset != 0)
				{
					try
					{
						await this.FlushAsync(isAsync, cancellationToken);
					}
					catch (Exception ex)
					{
						Logger.LogException(this._tracePoint, 0, "EdmWriter::WriteEndChain ", ex);
						DrdaException.CommunicationFailure(ex);
					}
				}
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x000DBA62 File Offset: 0x000D9C62
		public int DssLength
		{
			get
			{
				return this._offset - this._dssLengthOffset;
			}
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x000DBA71 File Offset: 0x000D9C71
		public int MarkDssClearPoint()
		{
			this._lastDssBeforeMark = this._prevHeaderLocation;
			return this._offset;
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x000DBA88 File Offset: 0x000D9C88
		public void ClearDSSesBackToMark(int i)
		{
			this._offset = i;
			if (this._lastDssBeforeMark == -1)
			{
				this._nextCorrelationID = 1;
				return;
			}
			this._nextCorrelationID = ((int)(this._outputBuffer[this._lastDssBeforeMark + 4] & byte.MaxValue) << 8) + (int)(this._outputBuffer[this._lastDssBeforeMark + 5] & byte.MaxValue);
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000DBAE0 File Offset: 0x000D9CE0
		public byte[] CopyDssDataToEnd(int start)
		{
			int num = this._offset - start;
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._outputBuffer, start, array, 0, num);
			return array;
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x000DBB0D File Offset: 0x000D9D0D
		public void UpdateDssOffset(int newOffset)
		{
			this._offset = newOffset;
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000DBB18 File Offset: 0x000D9D18
		private void EnsureLength(int i)
		{
			i += this._offset;
			if (i > this._outputBuffer.Length)
			{
				byte[] array = new byte[Math.Max(this._outputBuffer.Length, i)];
				Buffer.BlockCopy(this._outputBuffer, 0, array, 0, this._offset);
				this._outputBuffer = array;
			}
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x000DBB69 File Offset: 0x000D9D69
		public int GetCorrelationID()
		{
			return this.GetCorrelationID(-1);
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x000DBB74 File Offset: 0x000D9D74
		private int GetCorrelationID(int dssCorrId)
		{
			if (dssCorrId > 0)
			{
				return dssCorrId;
			}
			int num;
			if (this._previousCorrelationId != -1)
			{
				if (this._previousChainByte == 80)
				{
					num = this._previousCorrelationId;
				}
				else if (dssCorrId == 0)
				{
					num = 0;
					this._nextCorrelationID = 0;
				}
				else
				{
					int num2 = this._nextCorrelationID;
					this._nextCorrelationID = num2 + 1;
					num = num2;
				}
			}
			else if (dssCorrId == 0)
			{
				num = 0;
				this._nextCorrelationID = 0;
			}
			else
			{
				int num2 = this._nextCorrelationID;
				this._nextCorrelationID = num2 + 1;
				num = num2;
			}
			return num;
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x000DBBE6 File Offset: 0x000D9DE6
		private int CalculateBytesInExtendedLength(long ddmLen)
		{
			if (ddmLen <= 32767L)
			{
				return 0;
			}
			if (ddmLen <= (long)((ulong)(-1)))
			{
				return 4;
			}
			if (ddmLen <= 281474976710655L)
			{
				return 6;
			}
			if (ddmLen <= 9223372036854775807L)
			{
				return 8;
			}
			return 0;
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000DBC18 File Offset: 0x000D9E18
		private void FinalizeDssLength()
		{
			int num = this._offset - this._dssLengthOffset;
			int num2 = num - 32767;
			if (num2 > 0)
			{
				int num3 = num2 / 32765;
				if (num2 % 32765 != 0)
				{
					num3++;
				}
				int num4 = this._offset - 1;
				int num5 = num3 * 2;
				this.EnsureLength(num5);
				this._offset += num5;
				bool flag = true;
				do
				{
					int num6 = num2 % 32765;
					if (num6 == 0)
					{
						num6 = 32765;
					}
					int num7 = num4 - num6 + 1;
					Buffer.BlockCopy(this._outputBuffer, num7, this._outputBuffer, num7 + num5, num6);
					num4 -= num6;
					int num8 = num6 + 2;
					if (flag)
					{
						flag = false;
					}
					else if (num8 == 32767)
					{
						num8 |= 32768;
					}
					int num9 = num4 + num5 - 1;
					Converter.IntToBytes((long)num8, this._outputBuffer, 2, num9, EndianType.BigEndian);
					num2 -= num6;
					num5 -= 2;
				}
				while (num2 > 0);
				num = 65535;
			}
			Converter.IntToBytes((long)num, this._outputBuffer, 2, this._dssLengthOffset, EndianType.BigEndian);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x000DBD23 File Offset: 0x000D9F23
		private void ResetChainState()
		{
			this._prevHeaderLocation = -1;
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x000DBD2C File Offset: 0x000D9F2C
		public void Reset()
		{
			this._prevHeaderLocation = -1;
			this._previousCorrelationId = -1;
			this._previousChainByte = 0;
			this._isContinuationDss = false;
			this._lastDssBeforeMark = -1;
			this._offset = 0;
			this._markStack.Clear();
			this._dssLengthOffset = 0;
			this._nextCorrelationID = 1;
			this._correlationID = -1;
			this._enddianType = EndianType.LittleEndian;
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x000DBD8C File Offset: 0x000D9F8C
		private static void Fill(Array array, int fromindex, int toindex, object val)
		{
			if (array == null)
			{
				throw new NullReferenceException("Array parameter cannot be null");
			}
			object obj = val;
			Type elementType = array.GetType().GetElementType();
			if (elementType != val.GetType())
			{
				obj = Convert.ChangeType(val, elementType);
			}
			if (array.Length == 0)
			{
				throw new NullReferenceException();
			}
			if (fromindex > toindex)
			{
				throw new ArgumentException();
			}
			if (fromindex < 0 || array.Length < toindex)
			{
				throw new IndexOutOfRangeException();
			}
			int num;
			if (fromindex <= 0)
			{
				num = fromindex;
			}
			else
			{
				fromindex = (num = fromindex) - 1;
			}
			for (int i = num; i < toindex; i++)
			{
				array.SetValue(obj, i);
			}
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000DBE16 File Offset: 0x000DA016
		public void WriteTime(TimeSpan ival, int codePage)
		{
			this._converter.PackTime(ival, codePage, this._workingBuffer);
			this.WriteBytes(this._workingBuffer, 8);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x000DBE38 File Offset: 0x000DA038
		public void WriteDate(DateTime ival, int codePage)
		{
			this._converter.PackDate(ival, codePage, this._workingBuffer);
			this.WriteBytes(this._workingBuffer, 10);
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000DBE5B File Offset: 0x000DA05B
		public void WriteTimestamp(DateTime ival, int codePage)
		{
			this._converter.PackTimestamp(ival, codePage, this._workingBuffer);
			this.WriteBytes(this._workingBuffer, 26);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x000DBE7E File Offset: 0x000DA07E
		public void WriteDouble(double ival, string typDefnam)
		{
			this._converter.PackDouble(ival, this._workingBuffer, typDefnam);
			this.WriteBytes(this._workingBuffer, 8);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x000DBEA0 File Offset: 0x000DA0A0
		public void WriteFloat(float ival, string typDefnam)
		{
			this._converter.PackFloat(ival, this._workingBuffer, typDefnam);
			this.WriteBytes(this._workingBuffer, 4);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000DBEC2 File Offset: 0x000DA0C2
		public void WriteDecimalFloat(decimal ival, int length)
		{
			this._converter.PackDecimalFloat(ival, this._workingBuffer, length);
			this.WriteBytes(this._workingBuffer, length);
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060040A5 RID: 16549 RVA: 0x000DBEE4 File Offset: 0x000DA0E4
		public int DateMaskLength
		{
			get
			{
				return this._converter.DateMaskLength;
			}
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x000DBEF1 File Offset: 0x000DA0F1
		public void WriteStringSBCSFixedLength(string s, ushort paramLenNumBytes, int encoding)
		{
			if (s.Length > (int)paramLenNumBytes)
			{
				s = s.Substring(0, (int)paramLenNumBytes);
			}
			else
			{
				s = s.PadRight((int)paramLenNumBytes);
			}
			if (encoding > -1)
			{
				this.WriteStringSBCS(s, 48, encoding);
				return;
			}
			this.WriteStringSBCS(s, 48);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x000DBF29 File Offset: 0x000DA129
		public void WriteStringDBCSFixedLength(string s, EndianType endianTpe, int drdaType, ushort paramLenNumBytes, int encoding)
		{
			if (s.Length > (int)paramLenNumBytes)
			{
				s = s.Substring(0, (int)paramLenNumBytes);
			}
			else
			{
				s = s.PadRight((int)paramLenNumBytes);
			}
			if (encoding > -1)
			{
				this.WriteStringDBCS(s, endianTpe, drdaType, encoding);
				return;
			}
			this.WriteStringDBCS(s, endianTpe, drdaType);
		}

		// Token: 0x04002D6B RID: 11627
		private byte[] _outputBuffer;

		// Token: 0x04002D6C RID: 11628
		private byte[] _workingBuffer;

		// Token: 0x04002D6D RID: 11629
		private Stack<int> _markStack;

		// Token: 0x04002D6E RID: 11630
		private ICcsidManager _ccsidManager;

		// Token: 0x04002D6F RID: 11631
		private Stream _stream;

		// Token: 0x04002D70 RID: 11632
		private int _offset;

		// Token: 0x04002D71 RID: 11633
		private int _dssLengthOffset;

		// Token: 0x04002D72 RID: 11634
		private int _correlationID;

		// Token: 0x04002D73 RID: 11635
		private int _nextCorrelationID = 1;

		// Token: 0x04002D74 RID: 11636
		private int _prevHeaderLocation;

		// Token: 0x04002D75 RID: 11637
		private int _previousCorrelationId;

		// Token: 0x04002D76 RID: 11638
		private byte _previousChainByte;

		// Token: 0x04002D77 RID: 11639
		private bool _isContinuationDss;

		// Token: 0x04002D78 RID: 11640
		private int _lastDssBeforeMark;

		// Token: 0x04002D79 RID: 11641
		private Converter _converter;

		// Token: 0x04002D7A RID: 11642
		private Ccsid _ccsid;

		// Token: 0x04002D7B RID: 11643
		private int _sessionId;

		// Token: 0x04002D7C RID: 11644
		private object _tracePoint;

		// Token: 0x04002D7D RID: 11645
		private EndianType _enddianType = EndianType.LittleEndian;

		// Token: 0x04002D7E RID: 11646
		private static object perfmonLockObject = new object();

		// Token: 0x04002D7F RID: 11647
		private static CommonDrdaPerformanceCountersContainer CommonPerformanceContainer;

		// Token: 0x04002D80 RID: 11648
		private static PerSecondCounter bytesSentPerSecond;
	}
}
