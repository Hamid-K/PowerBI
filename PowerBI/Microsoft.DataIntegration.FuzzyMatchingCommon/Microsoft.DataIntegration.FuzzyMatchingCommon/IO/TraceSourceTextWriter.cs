using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.IO
{
	// Token: 0x02000033 RID: 51
	public class TraceSourceTextWriter : TextWriter
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0001124D File Offset: 0x0000F44D
		public TraceSourceTextWriter(TraceSource traceSource, TraceEventType traceEventType)
			: base(CultureInfo.InvariantCulture)
		{
			this.TraceSource = traceSource;
			this.TraceEventType = traceEventType;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00011273 File Offset: 0x0000F473
		public override Encoding Encoding
		{
			get
			{
				return Encoding.Default;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0001127C File Offset: 0x0000F47C
		public override void Flush()
		{
			base.Flush();
			if (this.m_sbBuffer.Length > 0)
			{
				StringBuilder sbBuffer = this.m_sbBuffer;
				lock (sbBuffer)
				{
					this.TraceSource.TraceEvent(this.TraceEventType, 0, this.m_sbBuffer.ToString());
					this.m_sbBuffer = new StringBuilder();
				}
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000112EC File Offset: 0x0000F4EC
		private bool IsFlushCharacter(char c)
		{
			return this.m_sbBuffer.Length > 10 || c == '\n' || c == '.' || c == ':' || c == ' ';
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00011318 File Offset: 0x0000F518
		public override void Write(char c)
		{
			StringBuilder sbBuffer = this.m_sbBuffer;
			lock (sbBuffer)
			{
				this.m_sbBuffer.Append(c);
			}
			if (this.IsFlushCharacter(c))
			{
				this.Flush();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00011368 File Offset: 0x0000F568
		public override void Write(char[] buffer, int index, int count)
		{
			if (count > 0)
			{
				if (count == 1)
				{
					this.Write(buffer[index]);
					return;
				}
				this.Write(new string(buffer, index, count));
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0001138C File Offset: 0x0000F58C
		public override void Write(string str)
		{
			if (str.Length > 0)
			{
				StringBuilder sbBuffer = this.m_sbBuffer;
				lock (sbBuffer)
				{
					this.m_sbBuffer.Append(str);
				}
			}
			if (this.IsFlushCharacter(str.get_Chars(str.Length - 1)))
			{
				this.Flush();
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000113F4 File Offset: 0x0000F5F4
		public override void WriteLine()
		{
			StringBuilder sbBuffer = this.m_sbBuffer;
			lock (sbBuffer)
			{
				this.m_sbBuffer.AppendLine();
			}
			this.Flush();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0001143C File Offset: 0x0000F63C
		public override void WriteLine(string str)
		{
			StringBuilder sbBuffer = this.m_sbBuffer;
			lock (sbBuffer)
			{
				this.m_sbBuffer.AppendLine(str);
			}
			this.Flush();
		}

		// Token: 0x04000037 RID: 55
		private TraceSource TraceSource;

		// Token: 0x04000038 RID: 56
		private TraceEventType TraceEventType;

		// Token: 0x04000039 RID: 57
		private StringBuilder m_sbBuffer = new StringBuilder();
	}
}
