using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200000E RID: 14
	internal sealed class ODataNotificationWriter : TextWriter
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002FC3 File Offset: 0x000011C3
		internal ODataNotificationWriter(TextWriter textWriter, IODataStreamListener listener)
			: base(CultureInfo.InvariantCulture)
		{
			this.textWriter = textWriter;
			this.listener = listener;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002FDE File Offset: 0x000011DE
		public override Encoding Encoding
		{
			get
			{
				return this.textWriter.Encoding;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002FEB File Offset: 0x000011EB
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002FF8 File Offset: 0x000011F8
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003006 File Offset: 0x00001206
		public override IFormatProvider FormatProvider
		{
			get
			{
				return this.textWriter.FormatProvider;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003013 File Offset: 0x00001213
		public override void Flush()
		{
			this.textWriter.Flush();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003020 File Offset: 0x00001220
		public override int GetHashCode()
		{
			return this.textWriter.GetHashCode();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000302D File Offset: 0x0000122D
		public override string ToString()
		{
			return this.textWriter.ToString();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000303A File Offset: 0x0000123A
		public override void Write(char value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003048 File Offset: 0x00001248
		public override void Write(bool value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003056 File Offset: 0x00001256
		public override void Write(string value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003064 File Offset: 0x00001264
		public override void Write(char[] buffer)
		{
			this.textWriter.Write(buffer);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003072 File Offset: 0x00001272
		public override void Write(char[] buffer, int index, int count)
		{
			this.textWriter.Write(buffer, index, count);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003082 File Offset: 0x00001282
		public override void Write(string format, params object[] arg)
		{
			this.textWriter.Write(format, arg);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003091 File Offset: 0x00001291
		public override void Write(decimal value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000309F File Offset: 0x0000129F
		public override void Write(object value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000030AD File Offset: 0x000012AD
		public override void Write(double value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000030BB File Offset: 0x000012BB
		public override void Write(float value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000030C9 File Offset: 0x000012C9
		public override void Write(int value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000030D7 File Offset: 0x000012D7
		public override void Write(long value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000030E5 File Offset: 0x000012E5
		public override void Write(uint value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000030F3 File Offset: 0x000012F3
		public override void Write(ulong value)
		{
			this.textWriter.Write(value);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003101 File Offset: 0x00001301
		public override void WriteLine()
		{
			this.textWriter.WriteLine();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000310E File Offset: 0x0000130E
		public override void WriteLine(bool value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000311C File Offset: 0x0000131C
		public override void WriteLine(char value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000312A File Offset: 0x0000132A
		public override void WriteLine(char[] buffer)
		{
			this.textWriter.WriteLine(buffer);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003138 File Offset: 0x00001338
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.textWriter.WriteLine(buffer, index, count);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003148 File Offset: 0x00001348
		public override void WriteLine(decimal value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003156 File Offset: 0x00001356
		public override void WriteLine(double value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003164 File Offset: 0x00001364
		public override void WriteLine(float value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003172 File Offset: 0x00001372
		public override void WriteLine(int value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003180 File Offset: 0x00001380
		public override void WriteLine(long value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000318E File Offset: 0x0000138E
		public override void WriteLine(object value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000319C File Offset: 0x0000139C
		public override void WriteLine(string format, params object[] arg)
		{
			this.textWriter.WriteLine(format, arg);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000031AB File Offset: 0x000013AB
		public override void WriteLine(string value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000031B9 File Offset: 0x000013B9
		public override void WriteLine(uint value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000031C7 File Offset: 0x000013C7
		public override void WriteLine(ulong value)
		{
			this.textWriter.WriteLine(value);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000031D5 File Offset: 0x000013D5
		public override Task FlushAsync()
		{
			return this.textWriter.FlushAsync();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000031E2 File Offset: 0x000013E2
		public override Task WriteAsync(char value)
		{
			return this.textWriter.WriteAsync(value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000031F0 File Offset: 0x000013F0
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			return this.textWriter.WriteAsync(buffer, index, count);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003200 File Offset: 0x00001400
		public override Task WriteAsync(string value)
		{
			return this.textWriter.WriteAsync(value);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000320E File Offset: 0x0000140E
		public override Task WriteLineAsync()
		{
			return this.textWriter.WriteLineAsync();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000321B File Offset: 0x0000141B
		public override Task WriteLineAsync(char value)
		{
			return this.textWriter.WriteLineAsync(value);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003229 File Offset: 0x00001429
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			return this.textWriter.WriteLineAsync(buffer, index, count);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003239 File Offset: 0x00001439
		public override Task WriteLineAsync(string value)
		{
			return this.textWriter.WriteLineAsync(value);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003247 File Offset: 0x00001447
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.StreamDisposed();
				this.listener = null;
			}
			this.textWriter.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000021 RID: 33
		private readonly TextWriter textWriter;

		// Token: 0x04000022 RID: 34
		private IODataStreamListener listener;
	}
}
