using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200161C RID: 5660
	public class StreamValueReader : BinaryReader, IValueReader
	{
		// Token: 0x06008E27 RID: 36391 RVA: 0x001DB28C File Offset: 0x001D948C
		public StreamValueReader(Stream stream)
			: base(stream)
		{
			this.state = new ValueReaderWriterState();
		}

		// Token: 0x06008E28 RID: 36392 RVA: 0x001DB2A0 File Offset: 0x001D94A0
		protected override void Dispose(bool disposing)
		{
			if (this.state != null)
			{
				base.Dispose(disposing);
				this.state = null;
			}
		}

		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x06008E29 RID: 36393 RVA: 0x001DB2B8 File Offset: 0x001D94B8
		public bool EndOfBinary
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x06008E2A RID: 36394 RVA: 0x001DB2B8 File Offset: 0x001D94B8
		public bool EndOfList
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17002553 RID: 9555
		// (get) Token: 0x06008E2B RID: 36395 RVA: 0x001DB2B8 File Offset: 0x001D94B8
		public bool EndOfTable
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x06008E2C RID: 36396 RVA: 0x001DB2C0 File Offset: 0x001D94C0
		public void ReadStartValue(out ValueKind kind, out ValueFlags flags)
		{
			this.ReadTag(out kind, out flags);
		}

		// Token: 0x06008E2D RID: 36397 RVA: 0x001DB2CA File Offset: 0x001D94CA
		public void ReadEndValue()
		{
			this.end = this.BaseStream.Position >= this.state.Continuation;
		}

		// Token: 0x06008E2E RID: 36398 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadNull()
		{
		}

		// Token: 0x06008E2F RID: 36399 RVA: 0x001DB2ED File Offset: 0x001D94ED
		public TimeSpan ReadTime()
		{
			return new TimeSpan(this.ReadInt64());
		}

		// Token: 0x06008E30 RID: 36400 RVA: 0x001DB2FA File Offset: 0x001D94FA
		public DateTime ReadDate()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x06008E31 RID: 36401 RVA: 0x001DB2FA File Offset: 0x001D94FA
		public DateTime ReadDateTime()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x06008E32 RID: 36402 RVA: 0x001DB307 File Offset: 0x001D9507
		public DateTimeOffset ReadDateTimeZone()
		{
			return new DateTimeOffset(this.ReadInt64(), new TimeSpan(this.ReadInt64()));
		}

		// Token: 0x06008E33 RID: 36403 RVA: 0x001DB2ED File Offset: 0x001D94ED
		public TimeSpan ReadDuration()
		{
			return new TimeSpan(this.ReadInt64());
		}

		// Token: 0x06008E34 RID: 36404 RVA: 0x00174979 File Offset: 0x00172B79
		public NumberKind ReadNumberKind()
		{
			return (NumberKind)this.ReadByte();
		}

		// Token: 0x06008E35 RID: 36405 RVA: 0x001DB31F File Offset: 0x001D951F
		public int ReadInt32Number()
		{
			return this.ReadInt32();
		}

		// Token: 0x06008E36 RID: 36406 RVA: 0x001DB327 File Offset: 0x001D9527
		public decimal ReadDecimalNumber()
		{
			return this.ReadDecimal();
		}

		// Token: 0x06008E37 RID: 36407 RVA: 0x001DB32F File Offset: 0x001D952F
		public double ReadDoubleNumber()
		{
			return this.ReadDouble();
		}

		// Token: 0x06008E38 RID: 36408 RVA: 0x001DB337 File Offset: 0x001D9537
		public bool ReadLogical()
		{
			return this.ReadBoolean();
		}

		// Token: 0x06008E39 RID: 36409 RVA: 0x001DB33F File Offset: 0x001D953F
		public string ReadText()
		{
			return this.ReadString();
		}

		// Token: 0x06008E3A RID: 36410 RVA: 0x001DB348 File Offset: 0x001D9548
		public long ReadStartBinary()
		{
			long num = this.ReadContinuation();
			this.end = num == this.BaseStream.Position;
			this.state.StartBinary(num);
			return num;
		}

		// Token: 0x06008E3B RID: 36411 RVA: 0x001DB37D File Offset: 0x001D957D
		public byte[] ReadBinary(int count)
		{
			byte[] array = this.ReadBytes(count);
			this.end = this.BaseStream.Position >= this.state.Continuation;
			return array;
		}

		// Token: 0x06008E3C RID: 36412 RVA: 0x001DB3A7 File Offset: 0x001D95A7
		public int ReadBinary(byte[] buffer, int index, int count)
		{
			int num = this.BaseStream.Read(buffer, index, count);
			this.end = this.BaseStream.Position >= this.state.Continuation;
			return num;
		}

		// Token: 0x06008E3D RID: 36413 RVA: 0x001DB3D8 File Offset: 0x001D95D8
		public void ReadEndBinary()
		{
			this.SkipToContinuation(this.state.EndBinary());
		}

		// Token: 0x06008E3E RID: 36414 RVA: 0x001DB3EC File Offset: 0x001D95EC
		public long ReadStartList()
		{
			long num = this.ReadContinuation();
			this.end = num == this.BaseStream.Position;
			this.state.StartList(num);
			return num;
		}

		// Token: 0x06008E3F RID: 36415 RVA: 0x001DB421 File Offset: 0x001D9621
		public void ReadEndList()
		{
			this.SkipToContinuation(this.state.EndList());
		}

		// Token: 0x06008E40 RID: 36416 RVA: 0x001DB434 File Offset: 0x001D9634
		public void ReadStartRecord(out Keys fieldNames)
		{
			fieldNames = this.ReadKeys();
		}

		// Token: 0x06008E41 RID: 36417 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadEndRecord()
		{
		}

		// Token: 0x06008E42 RID: 36418 RVA: 0x001DB440 File Offset: 0x001D9640
		public long ReadStartTable(out Keys columnNames)
		{
			long num = this.ReadContinuation();
			columnNames = this.ReadKeys();
			this.end = num == this.BaseStream.Position;
			this.state.StartTable(num, columnNames.Length);
			return num;
		}

		// Token: 0x06008E43 RID: 36419 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadStartRow()
		{
		}

		// Token: 0x06008E44 RID: 36420 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadEndRow()
		{
		}

		// Token: 0x06008E45 RID: 36421 RVA: 0x001DB484 File Offset: 0x001D9684
		public void ReadEndTable()
		{
			this.SkipToContinuation(this.state.EndTable());
		}

		// Token: 0x06008E46 RID: 36422 RVA: 0x001DB497 File Offset: 0x001D9697
		public void ReadFunction(out Keys paramNames, out int minParams)
		{
			paramNames = this.ReadKeys();
			minParams = this.ReadInt32();
		}

		// Token: 0x06008E47 RID: 36423 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadAction()
		{
		}

		// Token: 0x06008E48 RID: 36424 RVA: 0x001DB4A9 File Offset: 0x001D96A9
		public void ReadStartType(out ValueKind kind, out bool nullable)
		{
			kind = (ValueKind)(this.ReadByte() - 128);
			nullable = this.ReadBoolean();
		}

		// Token: 0x06008E49 RID: 36425 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadEndType()
		{
		}

		// Token: 0x06008E4A RID: 36426 RVA: 0x001DB4C1 File Offset: 0x001D96C1
		public void ReadStartRecordType(out Keys fieldNames, out bool open)
		{
			fieldNames = this.ReadKeys();
			open = this.ReadBoolean();
		}

		// Token: 0x06008E4B RID: 36427 RVA: 0x001DB4D3 File Offset: 0x001D96D3
		public void ReadFieldType(out bool optional)
		{
			optional = this.ReadBoolean();
		}

		// Token: 0x06008E4C RID: 36428 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadEndRecordType()
		{
		}

		// Token: 0x06008E4D RID: 36429 RVA: 0x001DB4DD File Offset: 0x001D96DD
		public void ReadTableType(out TableKey[] keys)
		{
			keys = this.ReadTableKeys();
		}

		// Token: 0x06008E4E RID: 36430 RVA: 0x001DB497 File Offset: 0x001D9697
		public void ReadStartFunctionType(out Keys paramNames, out int minParams)
		{
			paramNames = this.ReadKeys();
			minParams = this.ReadInt32();
		}

		// Token: 0x06008E4F RID: 36431 RVA: 0x0000336E File Offset: 0x0000156E
		public void ReadEndFunctionType()
		{
		}

		// Token: 0x06008E50 RID: 36432 RVA: 0x001DB31F File Offset: 0x001D951F
		public int ReadReference()
		{
			return this.ReadInt32();
		}

		// Token: 0x06008E51 RID: 36433 RVA: 0x001DB4E8 File Offset: 0x001D96E8
		private void ReadTag(out ValueKind kind, out ValueFlags flags)
		{
			ValueFlags valueFlags = (ValueFlags)this.ReadByte();
			kind = (ValueKind)((valueFlags & ~(ValueFlags.HasMeta | ValueFlags.HasType)) - 8);
			flags = valueFlags & ValueFlags.All;
		}

		// Token: 0x06008E52 RID: 36434 RVA: 0x001DB510 File Offset: 0x001D9710
		private Keys ReadKeys()
		{
			int num = this.ReadInt32();
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ReadString();
			}
			return Keys.New(array);
		}

		// Token: 0x06008E53 RID: 36435 RVA: 0x001DB548 File Offset: 0x001D9748
		private TableKey ReadTableKey()
		{
			bool flag = this.ReadBoolean();
			int[] array = new int[this.ReadInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ReadInt32();
			}
			return new TableKey(array, flag);
		}

		// Token: 0x06008E54 RID: 36436 RVA: 0x001DB588 File Offset: 0x001D9788
		private TableKey[] ReadTableKeys()
		{
			long num = (long)this.ReadInt32();
			if (num == 0L)
			{
				return StreamValueReader.emptyTableKeys;
			}
			TableKey[] array = new TableKey[num];
			int num2 = 0;
			while ((long)num2 < num)
			{
				array[num2] = this.ReadTableKey();
				num2++;
			}
			return array;
		}

		// Token: 0x06008E55 RID: 36437 RVA: 0x001DB5C5 File Offset: 0x001D97C5
		private long ReadContinuation()
		{
			return this.BaseStream.Position + this.ReadInt64();
		}

		// Token: 0x06008E56 RID: 36438 RVA: 0x001DB5D9 File Offset: 0x001D97D9
		private void SkipToContinuation(long continuation)
		{
			if (continuation < this.BaseStream.Position)
			{
				throw new InvalidOperationException();
			}
			this.BaseStream.Position = continuation;
		}

		// Token: 0x04004D55 RID: 19797
		private static TableKey[] emptyTableKeys = new TableKey[0];

		// Token: 0x04004D56 RID: 19798
		private ValueReaderWriterState state;

		// Token: 0x04004D57 RID: 19799
		private bool end;
	}
}
