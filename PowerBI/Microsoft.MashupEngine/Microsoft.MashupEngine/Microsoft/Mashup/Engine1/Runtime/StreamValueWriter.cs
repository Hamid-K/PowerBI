using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200161D RID: 5661
	public class StreamValueWriter : BinaryWriter, IValueWriter
	{
		// Token: 0x06008E58 RID: 36440 RVA: 0x001DB608 File Offset: 0x001D9808
		public StreamValueWriter(Stream stream)
			: base(stream)
		{
			this.stream = stream;
			this.state = new ValueReaderWriterState();
		}

		// Token: 0x06008E59 RID: 36441 RVA: 0x001DB623 File Offset: 0x001D9823
		protected override void Dispose(bool disposing)
		{
			if (this.state != null)
			{
				base.Dispose(disposing);
				this.state = null;
			}
		}

		// Token: 0x06008E5A RID: 36442 RVA: 0x001DB63B File Offset: 0x001D983B
		public void WriteStartValue(ValueKind kind, ValueFlags flags)
		{
			this.WriteTag(kind, flags);
		}

		// Token: 0x06008E5B RID: 36443 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteEndValue()
		{
		}

		// Token: 0x06008E5C RID: 36444 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteNull()
		{
		}

		// Token: 0x06008E5D RID: 36445 RVA: 0x001DB645 File Offset: 0x001D9845
		public void WriteTime(TimeSpan time)
		{
			this.Write(time.Ticks);
		}

		// Token: 0x06008E5E RID: 36446 RVA: 0x001DB654 File Offset: 0x001D9854
		public void WriteDate(DateTime date)
		{
			this.Write(date.Ticks);
		}

		// Token: 0x06008E5F RID: 36447 RVA: 0x001DB654 File Offset: 0x001D9854
		public void WriteDateTime(DateTime dateTime)
		{
			this.Write(dateTime.Ticks);
		}

		// Token: 0x06008E60 RID: 36448 RVA: 0x001DB664 File Offset: 0x001D9864
		public void WriteDateTimeZone(DateTimeOffset dateTimeZone)
		{
			this.Write(dateTimeZone.Ticks);
			this.Write(dateTimeZone.Offset.Ticks);
		}

		// Token: 0x06008E61 RID: 36449 RVA: 0x001DB645 File Offset: 0x001D9845
		public void WriteDuration(TimeSpan duration)
		{
			this.Write(duration.Ticks);
		}

		// Token: 0x06008E62 RID: 36450 RVA: 0x00174D51 File Offset: 0x00172F51
		public void WriteNumberKind(NumberKind numberKind)
		{
			this.Write((byte)numberKind);
		}

		// Token: 0x06008E63 RID: 36451 RVA: 0x001DB693 File Offset: 0x001D9893
		public void WriteNumber(int number)
		{
			this.Write(number);
		}

		// Token: 0x06008E64 RID: 36452 RVA: 0x001DB69C File Offset: 0x001D989C
		public void WriteNumber(decimal number)
		{
			this.Write(number);
		}

		// Token: 0x06008E65 RID: 36453 RVA: 0x001DB6A5 File Offset: 0x001D98A5
		public void WriteNumber(double number)
		{
			this.Write(number);
		}

		// Token: 0x06008E66 RID: 36454 RVA: 0x001DB6AE File Offset: 0x001D98AE
		public void WriteLogical(bool logical)
		{
			this.Write(logical);
		}

		// Token: 0x06008E67 RID: 36455 RVA: 0x001DB6B7 File Offset: 0x001D98B7
		public void WriteText(string text)
		{
			this.Write(text);
		}

		// Token: 0x06008E68 RID: 36456 RVA: 0x001DB6C0 File Offset: 0x001D98C0
		public void WriteStartBinary()
		{
			long num = this.PrepareOffset();
			this.state.StartBinary(num);
		}

		// Token: 0x06008E69 RID: 36457 RVA: 0x001DB6E0 File Offset: 0x001D98E0
		public void WriteBinary(byte[] buffer)
		{
			this.Write(buffer);
		}

		// Token: 0x06008E6A RID: 36458 RVA: 0x001DB6E9 File Offset: 0x001D98E9
		public void WriteBinary(byte[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
		}

		// Token: 0x06008E6B RID: 36459 RVA: 0x001DB6F4 File Offset: 0x001D98F4
		public void WriteEndBinary()
		{
			this.PatchOffset(this.state.EndBinary());
		}

		// Token: 0x06008E6C RID: 36460 RVA: 0x001DB708 File Offset: 0x001D9908
		public void WriteStartList()
		{
			long num = this.PrepareOffset();
			this.state.StartList(num);
		}

		// Token: 0x06008E6D RID: 36461 RVA: 0x001DB728 File Offset: 0x001D9928
		public void WriteEndList()
		{
			this.PatchOffset(this.state.EndList());
		}

		// Token: 0x06008E6E RID: 36462 RVA: 0x001DB73B File Offset: 0x001D993B
		public void WriteStartRecord(Keys fieldNames)
		{
			this.WriteKeys(fieldNames);
		}

		// Token: 0x06008E6F RID: 36463 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteEndRecord()
		{
		}

		// Token: 0x06008E70 RID: 36464 RVA: 0x001DB744 File Offset: 0x001D9944
		public void WriteStartTable(Keys columnNames)
		{
			long num = this.PrepareOffset();
			this.WriteKeys(columnNames);
			this.state.StartTable(num, columnNames.Length);
		}

		// Token: 0x06008E71 RID: 36465 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteStartRow()
		{
		}

		// Token: 0x06008E72 RID: 36466 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteEndRow()
		{
		}

		// Token: 0x06008E73 RID: 36467 RVA: 0x001DB771 File Offset: 0x001D9971
		public void WriteEndTable()
		{
			this.PatchOffset(this.state.EndTable());
		}

		// Token: 0x06008E74 RID: 36468 RVA: 0x001DB784 File Offset: 0x001D9984
		public void WriteFunction(Keys paramNames, int minParams)
		{
			this.WriteKeys(paramNames);
			this.Write(minParams);
		}

		// Token: 0x06008E75 RID: 36469 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteAction()
		{
		}

		// Token: 0x06008E76 RID: 36470 RVA: 0x001DB794 File Offset: 0x001D9994
		public void WriteStartType(ValueKind kind, bool nullable)
		{
			this.Write((byte)(kind + 128));
			this.Write(nullable);
		}

		// Token: 0x06008E77 RID: 36471 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteEndType()
		{
		}

		// Token: 0x06008E78 RID: 36472 RVA: 0x001DB7AB File Offset: 0x001D99AB
		public void WriteStartRecordType(Keys keys, bool open)
		{
			this.WriteKeys(keys);
			this.Write(open);
		}

		// Token: 0x06008E79 RID: 36473 RVA: 0x001DB6AE File Offset: 0x001D98AE
		public void WriteFieldType(bool optional)
		{
			this.Write(optional);
		}

		// Token: 0x06008E7A RID: 36474 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteEndRecordType()
		{
		}

		// Token: 0x06008E7B RID: 36475 RVA: 0x001DB7BB File Offset: 0x001D99BB
		public void WriteTableType(TableKey[] keys)
		{
			this.WriteTableKeys(keys);
		}

		// Token: 0x06008E7C RID: 36476 RVA: 0x001DB784 File Offset: 0x001D9984
		public void WriteStartFunctionType(Keys paramNames, int minParams)
		{
			this.WriteKeys(paramNames);
			this.Write(minParams);
		}

		// Token: 0x06008E7D RID: 36477 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteEndFunctionType()
		{
		}

		// Token: 0x06008E7E RID: 36478 RVA: 0x001DB693 File Offset: 0x001D9893
		public void WriteReference(int key)
		{
			this.Write(key);
		}

		// Token: 0x06008E7F RID: 36479 RVA: 0x001DB7C4 File Offset: 0x001D99C4
		private void WriteTag(ValueKind kind, ValueFlags flags)
		{
			ValueFlags valueFlags = (ValueFlags)(kind + 8) | flags;
			this.Write((byte)valueFlags);
		}

		// Token: 0x06008E80 RID: 36480 RVA: 0x001DB7E0 File Offset: 0x001D99E0
		private void WriteKeys(Keys keys)
		{
			this.Write(keys.Length);
			for (int i = 0; i < keys.Length; i++)
			{
				this.Write(keys[i]);
			}
		}

		// Token: 0x06008E81 RID: 36481 RVA: 0x001DB818 File Offset: 0x001D9A18
		private void WriteTableKey(TableKey key)
		{
			this.Write(key.Primary);
			int[] columns = key.Columns;
			this.Write(columns.Length);
			for (int i = 0; i < columns.Length; i++)
			{
				this.Write(columns[i]);
			}
		}

		// Token: 0x06008E82 RID: 36482 RVA: 0x001DB858 File Offset: 0x001D9A58
		private void WriteTableKeys(TableKey[] keys)
		{
			this.Write(keys.Length);
			for (int i = 0; i < keys.Length; i++)
			{
				this.WriteTableKey(keys[i]);
			}
		}

		// Token: 0x06008E83 RID: 36483 RVA: 0x001DB885 File Offset: 0x001D9A85
		private long PrepareOffset()
		{
			long position = this.stream.Position;
			this.Write(0L);
			return position;
		}

		// Token: 0x06008E84 RID: 36484 RVA: 0x001DB89C File Offset: 0x001D9A9C
		private void PatchOffset(long start)
		{
			long position = this.stream.Position;
			this.stream.Position = start;
			this.Write(position - start);
			this.stream.Position = position;
		}

		// Token: 0x04004D58 RID: 19800
		private Stream stream;

		// Token: 0x04004D59 RID: 19801
		private ValueReaderWriterState state;
	}
}
