using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Hosting.Tracing
{
	// Token: 0x0200000C RID: 12
	internal class DualWriter : TextWriter
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000030E0 File Offset: 0x000012E0
		internal DualWriter(TextWriter writer2)
			: base(writer2.FormatProvider)
		{
			this.Writer2 = writer2;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000030F5 File Offset: 0x000012F5
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000030FD File Offset: 0x000012FD
		private TextWriter Writer2 { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003106 File Offset: 0x00001306
		public override Encoding Encoding
		{
			get
			{
				return this.Writer2.Encoding;
			}
		}

		// Token: 0x06000043 RID: 67
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern void OutputDebugString(string message);

		// Token: 0x06000044 RID: 68 RVA: 0x00003113 File Offset: 0x00001313
		public override void Close()
		{
			this.Writer2.Close();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003120 File Offset: 0x00001320
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Writer2.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003137 File Offset: 0x00001337
		private static void InternalWrite(string message)
		{
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
				return;
			}
			if (!DualWriter.IsMono)
			{
				DualWriter.OutputDebugString(message ?? string.Empty);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000315F File Offset: 0x0000135F
		public override void Write(char value)
		{
			DualWriter.InternalWrite(value.ToString());
			this.Writer2.Write(value);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003179 File Offset: 0x00001379
		public override void Write(char[] buffer)
		{
			DualWriter.InternalWrite(new string(buffer));
			this.Writer2.Write(buffer);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003192 File Offset: 0x00001392
		public override void Write(string value)
		{
			DualWriter.InternalWrite(value);
			this.Writer2.Write(value);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031A6 File Offset: 0x000013A6
		public override void Write(char[] buffer, int index, int count)
		{
			DualWriter.InternalWrite(new string(buffer, index, count));
			this.Writer2.Write(buffer, index, count);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031C3 File Offset: 0x000013C3
		public override void Flush()
		{
			this.Writer2.Flush();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031D0 File Offset: 0x000013D0
		public override Task FlushAsync()
		{
			return this.Writer2.FlushAsync();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031DD File Offset: 0x000013DD
		public override Task WriteAsync(char value)
		{
			DualWriter.InternalWrite(value.ToString());
			return this.Writer2.WriteAsync(value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000031F7 File Offset: 0x000013F7
		public override Task WriteAsync(string value)
		{
			DualWriter.InternalWrite(value);
			return this.Writer2.WriteAsync(value);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000320B File Offset: 0x0000140B
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			DualWriter.InternalWrite(new string(buffer, index, count));
			return this.Writer2.WriteAsync(buffer, index, count);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003228 File Offset: 0x00001428
		public override Task WriteLineAsync()
		{
			DualWriter.InternalWrite(Environment.NewLine);
			return this.Writer2.WriteLineAsync();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000323F File Offset: 0x0000143F
		public override Task WriteLineAsync(char value)
		{
			DualWriter.InternalWrite(value.ToString() + Environment.NewLine);
			return this.Writer2.WriteLineAsync(value);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003263 File Offset: 0x00001463
		public override Task WriteLineAsync(string value)
		{
			DualWriter.InternalWrite(value + Environment.NewLine);
			return this.Writer2.WriteLineAsync(value);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003281 File Offset: 0x00001481
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			DualWriter.InternalWrite(new string(buffer, index, count) + Environment.NewLine);
			return this.Writer2.WriteLineAsync(buffer, index, count);
		}

		// Token: 0x0400002A RID: 42
		private static readonly bool IsMono = Type.GetType("Mono.Runtime") != null;
	}
}
