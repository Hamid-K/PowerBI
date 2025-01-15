using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000DB RID: 219
	internal class JsonWriter : TextWriter
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x0002472C File Offset: 0x0002372C
		public JsonWriter(Stream outStream, bool prettyPrint)
		{
			this.outStream = outStream;
			this.prettyPrint = prettyPrint;
			this.shouldCloseStream = false;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00024783 File Offset: 0x00023783
		protected override void Dispose(bool disposing)
		{
			if (this.shouldCloseStream)
			{
				this.outStream.Flush();
				this.outStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000247AC File Offset: 0x000237AC
		public override void Write(char value)
		{
			if (value == '\r' && this.writingStringValue)
			{
				this.WriteInternal('\\');
				this.WriteInternal('r');
				return;
			}
			if (value == '\n' && this.writingStringValue)
			{
				this.WriteInternal('\\');
				this.WriteInternal('n');
				return;
			}
			if (value == '\t' && this.writingStringValue)
			{
				this.WriteInternal('\\');
				this.WriteInternal('t');
				return;
			}
			if (value == '"' || value == '\\')
			{
				this.WriteInternal('\\');
			}
			this.WriteInternal(value);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002482C File Offset: 0x0002382C
		public void PushObjectClosure()
		{
			this.AddingValue();
			this.closures.Push('}');
			this.closureHasMembers.Push(false);
			this.WriteInternal('{');
			this.WriteIndentation();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002485B File Offset: 0x0002385B
		public void PushArrayClosure()
		{
			this.AddingValue();
			this.closures.Push(']');
			this.closureHasMembers.Push(false);
			this.WriteInternal('[');
			this.WriteIndentation();
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002488C File Offset: 0x0002388C
		public void PopClosure()
		{
			char c = this.closures.Pop();
			this.closureHasMembers.Pop();
			this.WriteIndentation();
			this.WriteInternal(c);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000248BE File Offset: 0x000238BE
		public void WriteQuote()
		{
			this.WriteInternal('"');
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000248C8 File Offset: 0x000238C8
		public void WriteKey(string key)
		{
			if (this.closureHasMembers.Peek())
			{
				this.WriteInternal(',');
				this.WriteIndentation();
			}
			this.WriteQuote();
			this.Write(key);
			this.WriteQuote();
			if (this.prettyPrint)
			{
				this.Write(" : ");
				return;
			}
			this.WriteInternal(':');
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002491F File Offset: 0x0002391F
		public void WriteValue(string value)
		{
			this.AddingValue();
			this.WriteQuote();
			this.writingStringValue = true;
			this.Write(value);
			this.writingStringValue = false;
			this.WriteQuote();
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00024948 File Offset: 0x00023948
		public void WriteValue(bool value)
		{
			this.AddingValue();
			this.Write(value.ToString().ToLowerInvariant());
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00024962 File Offset: 0x00023962
		public void WriteValue(long value)
		{
			this.AddingValue();
			this.Write(value);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00024971 File Offset: 0x00023971
		public void WriteValue(int value)
		{
			this.AddingValue();
			this.Write(value);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00024980 File Offset: 0x00023980
		public void WriteValue(Enum value)
		{
			this.WriteValue(EwsUtilities.SerializeEnum(value));
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002498E File Offset: 0x0002398E
		public void WriteValue(DateTime value)
		{
			this.WriteValue(EwsUtilities.DateTimeToXSDateTime(value));
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002499C File Offset: 0x0002399C
		public void WriteValue(float value)
		{
			this.AddingValue();
			this.Write(value);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000249AB File Offset: 0x000239AB
		public void WriteValue(double value)
		{
			this.AddingValue();
			this.Write(value);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000249BA File Offset: 0x000239BA
		public void WriteNullValue()
		{
			this.AddingValue();
			this.Write("null");
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000249D0 File Offset: 0x000239D0
		private void WriteInternal(char value)
		{
			this.singleCharBuffer[0] = value;
			int bytes = this.Encoding.GetBytes(this.singleCharBuffer, 0, 1, this.smallBuffer, 0);
			this.outStream.Write(this.smallBuffer, 0, bytes);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00024A14 File Offset: 0x00023A14
		private void WriteIndentation()
		{
			if (this.prettyPrint)
			{
				this.WriteInternal('\r');
				this.WriteInternal('\n');
				for (int i = this.closures.Count - 1; i >= 0; i--)
				{
					this.Write("  ");
				}
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00024A5C File Offset: 0x00023A5C
		private void AddingValue()
		{
			if (this.closures.Count > 0)
			{
				if (this.closures.Peek() == ']' && this.closureHasMembers.Peek())
				{
					this.WriteInternal(',');
					this.WriteIndentation();
				}
				if (!this.closureHasMembers.Peek())
				{
					this.closureHasMembers.Pop();
					this.closureHasMembers.Push(true);
				}
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00024AC6 File Offset: 0x00023AC6
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00024ACD File Offset: 0x00023ACD
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x00024AD5 File Offset: 0x00023AD5
		public bool ShouldCloseStream
		{
			get
			{
				return this.shouldCloseStream;
			}
			set
			{
				this.shouldCloseStream = value;
			}
		}

		// Token: 0x04000348 RID: 840
		private const string Indentation = "  ";

		// Token: 0x04000349 RID: 841
		private Stream outStream;

		// Token: 0x0400034A RID: 842
		private bool shouldCloseStream;

		// Token: 0x0400034B RID: 843
		private bool prettyPrint;

		// Token: 0x0400034C RID: 844
		private bool writingStringValue;

		// Token: 0x0400034D RID: 845
		private byte[] smallBuffer = new byte[10];

		// Token: 0x0400034E RID: 846
		private char[] singleCharBuffer = new char[1];

		// Token: 0x0400034F RID: 847
		private Stack<char> closures = new Stack<char>();

		// Token: 0x04000350 RID: 848
		private Stack<bool> closureHasMembers = new Stack<bool>();
	}
}
