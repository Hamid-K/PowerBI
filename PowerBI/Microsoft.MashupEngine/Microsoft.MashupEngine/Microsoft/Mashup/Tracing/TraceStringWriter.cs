using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020B6 RID: 8374
	internal class TraceStringWriter : TextWriter
	{
		// Token: 0x0600CCFF RID: 52479 RVA: 0x0028C3F4 File Offset: 0x0028A5F4
		public TraceStringWriter(Trace trace, int? maxLength)
			: base(CultureInfo.InvariantCulture)
		{
			this.trace = trace;
			this.writer = trace.Writer.BeginString(CultureInfo.InvariantCulture);
			this.remainingWrites = maxLength;
		}

		// Token: 0x17003152 RID: 12626
		// (get) Token: 0x0600CD00 RID: 52480 RVA: 0x00246711 File Offset: 0x00244911
		public override Encoding Encoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x0600CD01 RID: 52481 RVA: 0x0028C428 File Offset: 0x0028A628
		public override void Write(char value)
		{
			if (this.remainingWrites != null)
			{
				int? num = this.remainingWrites;
				int num2 = 0;
				if (!((num.GetValueOrDefault() > num2) & (num != null)))
				{
					return;
				}
			}
			this.writer.Write(value);
			this.remainingWrites--;
		}

		// Token: 0x0600CD02 RID: 52482 RVA: 0x0028C49C File Offset: 0x0028A69C
		public void BeginPii()
		{
			if (this.remainingWrites != null)
			{
				int? num = this.remainingWrites;
				int num2 = 0;
				if (!((num.GetValueOrDefault() > num2) & (num != null)))
				{
					return;
				}
			}
			this.writer.Flush();
			this.piiOffset = new int?(this.trace.Writer.Length);
		}

		// Token: 0x0600CD03 RID: 52483 RVA: 0x0028C4FC File Offset: 0x0028A6FC
		public void EndPii()
		{
			if (this.piiOffset != null)
			{
				this.writer.Flush();
				this.trace.MarkPii(this.piiOffset.Value, this.trace.Writer.Length - this.piiOffset.Value, "[Hidden]");
				this.piiOffset = null;
			}
		}

		// Token: 0x0600CD04 RID: 52484 RVA: 0x00246746 File Offset: 0x00244946
		public override void Close()
		{
			base.Close();
			this.Dispose(true);
		}

		// Token: 0x0600CD05 RID: 52485 RVA: 0x0028C564 File Offset: 0x0028A764
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.writer != null)
			{
				this.writer.Dispose();
			}
			this.writer = null;
			this.trace = null;
		}

		// Token: 0x040067C8 RID: 26568
		private TextWriter writer;

		// Token: 0x040067C9 RID: 26569
		private Trace trace;

		// Token: 0x040067CA RID: 26570
		private int? piiOffset;

		// Token: 0x040067CB RID: 26571
		private int? remainingWrites;
	}
}
