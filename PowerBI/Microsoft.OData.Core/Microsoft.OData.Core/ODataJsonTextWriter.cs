using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Buffers;
using Microsoft.OData.Json;

namespace Microsoft.OData
{
	// Token: 0x02000008 RID: 8
	internal sealed class ODataJsonTextWriter : TextWriter
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002859 File Offset: 0x00000A59
		internal ODataJsonTextWriter(TextWriter textWriter, ref char[] buffer, ICharArrayPool bufferPool)
			: base(CultureInfo.InvariantCulture)
		{
			ExceptionUtils.CheckArgumentNotNull<TextWriter>(textWriter, "textWriter");
			this.textWriter = textWriter;
			this.streamingBuffer = buffer;
			this.bufferPool = bufferPool;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000288F File Offset: 0x00000A8F
		public override Encoding Encoding
		{
			get
			{
				return this.textWriter.Encoding;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000289C File Offset: 0x00000A9C
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000028A9 File Offset: 0x00000AA9
		public override string NewLine
		{
			get
			{
				return this.textWriter.NewLine;
			}
			set
			{
				this.textWriter.NewLine = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028B7 File Offset: 0x00000AB7
		public override IFormatProvider FormatProvider
		{
			get
			{
				return this.textWriter.FormatProvider;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028C4 File Offset: 0x00000AC4
		public override void Flush()
		{
			this.textWriter.Flush();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028D1 File Offset: 0x00000AD1
		public override int GetHashCode()
		{
			return this.textWriter.GetHashCode();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028DE File Offset: 0x00000ADE
		public override string ToString()
		{
			return this.textWriter.ToString();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028EB File Offset: 0x00000AEB
		public override void Write(char value)
		{
			this.WriteEscapedCharValue(value);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028F4 File Offset: 0x00000AF4
		public override void Write(bool value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002902 File Offset: 0x00000B02
		public override void Write(string value)
		{
			this.WriteEscapedStringValue(value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000290B File Offset: 0x00000B0B
		public override void Write(char[] buffer)
		{
			this.WriteEscapedCharArray(buffer, 0, buffer.Length);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002918 File Offset: 0x00000B18
		public override void Write(char[] buffer, int index, int count)
		{
			this.WriteEscapedCharArray(buffer, index, count);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002923 File Offset: 0x00000B23
		public override void Write(string format, params object[] arg)
		{
			this.WriteEscapedStringValue(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002938 File Offset: 0x00000B38
		public override void Write(decimal value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002946 File Offset: 0x00000B46
		public override void Write(object value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002954 File Offset: 0x00000B54
		public override void Write(double value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002962 File Offset: 0x00000B62
		public override void Write(float value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002970 File Offset: 0x00000B70
		public override void Write(int value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000297E File Offset: 0x00000B7E
		public override void Write(long value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000298C File Offset: 0x00000B8C
		public override void Write(uint value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000299A File Offset: 0x00000B9A
		public override void Write(ulong value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029A8 File Offset: 0x00000BA8
		public override void WriteLine()
		{
			this.textWriter.WriteLine();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029B5 File Offset: 0x00000BB5
		public override void WriteLine(bool value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029C3 File Offset: 0x00000BC3
		public override void WriteLine(char value)
		{
			this.Write(value);
			this.textWriter.WriteLine();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029D7 File Offset: 0x00000BD7
		public override void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.textWriter.WriteLine();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029EB File Offset: 0x00000BEB
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.textWriter.WriteLine();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A01 File Offset: 0x00000C01
		public override void WriteLine(decimal value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A0F File Offset: 0x00000C0F
		public override void WriteLine(double value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A1D File Offset: 0x00000C1D
		public override void WriteLine(float value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A2B File Offset: 0x00000C2B
		public override void WriteLine(int value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A39 File Offset: 0x00000C39
		public override void WriteLine(long value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A47 File Offset: 0x00000C47
		public override void WriteLine(object value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A55 File Offset: 0x00000C55
		public override void WriteLine(string format, params object[] arg)
		{
			this.Write(format, arg);
			this.textWriter.WriteLine();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A6A File Offset: 0x00000C6A
		public override void WriteLine(string value)
		{
			this.Write(value);
			this.textWriter.WriteLine();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A7E File Offset: 0x00000C7E
		public override void WriteLine(uint value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A8C File Offset: 0x00000C8C
		public override void WriteLine(ulong value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A9A File Offset: 0x00000C9A
		public override Task FlushAsync()
		{
			return this.textWriter.FlushAsync();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public override Task WriteAsync(char value)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.Write(value);
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002ADC File Offset: 0x00000CDC
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.Write(buffer, index, count);
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B1C File Offset: 0x00000D1C
		public override Task WriteAsync(string value)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.Write(value);
			});
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B4E File Offset: 0x00000D4E
		public override Task WriteLineAsync()
		{
			return this.textWriter.WriteLineAsync();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B5C File Offset: 0x00000D5C
		public override Task WriteLineAsync(char value)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteLine(value);
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B90 File Offset: 0x00000D90
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteLine(buffer, index, count);
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public override Task WriteLineAsync(string value)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteLine(value);
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002C02 File Offset: 0x00000E02
		protected override void Dispose(bool disposing)
		{
			this.textWriter.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C16 File Offset: 0x00000E16
		private void WriteEscapedCharValue(char value)
		{
			JsonValueUtils.WriteValue(this.textWriter, value, this.escapeOption);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C2A File Offset: 0x00000E2A
		private void WriteEscapedStringValue(string value)
		{
			JsonValueUtils.WriteEscapedJsonStringValue(this.textWriter, value, this.escapeOption, ref this.streamingBuffer, this.bufferPool);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C4A File Offset: 0x00000E4A
		private void WriteEscapedCharArray(char[] value, int offset, int count)
		{
			JsonValueUtils.WriteEscapedCharArray(this.textWriter, value, offset, count, this.escapeOption, ref this.streamingBuffer, this.bufferPool);
		}

		// Token: 0x04000013 RID: 19
		private readonly TextWriter textWriter;

		// Token: 0x04000014 RID: 20
		private readonly ODataStringEscapeOption escapeOption = ODataStringEscapeOption.EscapeOnlyControls;

		// Token: 0x04000015 RID: 21
		private char[] streamingBuffer;

		// Token: 0x04000016 RID: 22
		private ICharArrayPool bufferPool;
	}
}
