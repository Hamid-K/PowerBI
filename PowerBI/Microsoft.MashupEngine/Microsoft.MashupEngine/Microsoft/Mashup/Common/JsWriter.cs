using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BFA RID: 7162
	public class JsWriter
	{
		// Token: 0x0600B2C4 RID: 45764 RVA: 0x00246337 File Offset: 0x00244537
		public JsWriter()
		{
			this.builder = new StringBuilder();
			this.scopes = new List<bool>();
			this.EnterScope();
		}

		// Token: 0x17002CDB RID: 11483
		// (get) Token: 0x0600B2C5 RID: 45765 RVA: 0x0024635B File Offset: 0x0024455B
		public int Length
		{
			get
			{
				return this.builder.Length;
			}
		}

		// Token: 0x0600B2C6 RID: 45766 RVA: 0x00246368 File Offset: 0x00244568
		private void SetNeedComma()
		{
			this.scopes[this.scopes.Count - 1] = true;
		}

		// Token: 0x0600B2C7 RID: 45767 RVA: 0x00246383 File Offset: 0x00244583
		private void EnterScope()
		{
			this.scopes.Add(false);
		}

		// Token: 0x0600B2C8 RID: 45768 RVA: 0x00246391 File Offset: 0x00244591
		private void LeaveScope()
		{
			this.scopes.RemoveAt(this.scopes.Count - 1);
		}

		// Token: 0x0600B2C9 RID: 45769 RVA: 0x002463AC File Offset: 0x002445AC
		private void WriteComma()
		{
			int num = this.scopes.Count - 1;
			if (this.scopes[num])
			{
				this.builder.Append(',');
				this.scopes[num] = false;
			}
		}

		// Token: 0x0600B2CA RID: 45770 RVA: 0x002463F0 File Offset: 0x002445F0
		public void WriteRecordStart()
		{
			this.WriteComma();
			this.EnterScope();
			this.builder.Append('{');
		}

		// Token: 0x0600B2CB RID: 45771 RVA: 0x0024640C File Offset: 0x0024460C
		public void WriteRecordEnd()
		{
			this.builder.Append('}');
			this.LeaveScope();
		}

		// Token: 0x0600B2CC RID: 45772 RVA: 0x00246422 File Offset: 0x00244622
		public void WriteFieldStart(string name)
		{
			this.WriteComma();
			this.WriteStringCore(name);
			this.builder.Append(':');
		}

		// Token: 0x0600B2CD RID: 45773 RVA: 0x0024643F File Offset: 0x0024463F
		public void WriteFieldEnd()
		{
			this.SetNeedComma();
		}

		// Token: 0x0600B2CE RID: 45774 RVA: 0x00246447 File Offset: 0x00244647
		public void WriteArrayStart()
		{
			this.WriteComma();
			this.builder.Append('[');
			this.EnterScope();
		}

		// Token: 0x0600B2CF RID: 45775 RVA: 0x00246463 File Offset: 0x00244663
		public void WriteArrayEnd()
		{
			this.builder.Append(']');
			this.LeaveScope();
			this.SetNeedComma();
		}

		// Token: 0x0600B2D0 RID: 45776 RVA: 0x0024647F File Offset: 0x0024467F
		public void WriteInt(int value)
		{
			this.WriteComma();
			this.builder.Append(value);
			this.SetNeedComma();
		}

		// Token: 0x0600B2D1 RID: 45777 RVA: 0x0024649A File Offset: 0x0024469A
		public void WriteDouble(double value)
		{
			this.WriteComma();
			this.builder.Append(value);
			this.SetNeedComma();
		}

		// Token: 0x0600B2D2 RID: 45778 RVA: 0x002464B5 File Offset: 0x002446B5
		public void WriteString(string value)
		{
			this.WriteString(value, value.Length);
		}

		// Token: 0x0600B2D3 RID: 45779 RVA: 0x002464C4 File Offset: 0x002446C4
		public void WriteString(string value, int length)
		{
			this.WriteComma();
			this.WriteStringCore(value, length);
			this.SetNeedComma();
		}

		// Token: 0x0600B2D4 RID: 45780 RVA: 0x002464DA File Offset: 0x002446DA
		public void WriteStringCore(string value)
		{
			this.WriteStringCore(value, value.Length);
		}

		// Token: 0x0600B2D5 RID: 45781 RVA: 0x002464EC File Offset: 0x002446EC
		public void WriteStringCore(string value, int length)
		{
			this.builder.EnsureCapacity(this.builder.Length + length + 2);
			this.builder.Append('"');
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				this.WriteChar(c);
			}
			this.builder.Append('"');
		}

		// Token: 0x0600B2D6 RID: 45782 RVA: 0x0024654B File Offset: 0x0024474B
		public TextWriter BeginString(IFormatProvider formatProvider = null)
		{
			return new JsWriter.JsStringWriter(this, formatProvider);
		}

		// Token: 0x0600B2D7 RID: 45783 RVA: 0x00246554 File Offset: 0x00244754
		public void WriteBool(bool value)
		{
			this.WriteComma();
			this.builder.Append(value ? "true" : "false");
			this.SetNeedComma();
		}

		// Token: 0x0600B2D8 RID: 45784 RVA: 0x0024657D File Offset: 0x0024477D
		public void WriteNull()
		{
			this.WriteComma();
			this.builder.Append("null");
			this.SetNeedComma();
		}

		// Token: 0x0600B2D9 RID: 45785 RVA: 0x0024659C File Offset: 0x0024479C
		public override string ToString()
		{
			return this.builder.ToString();
		}

		// Token: 0x0600B2DA RID: 45786 RVA: 0x002465AC File Offset: 0x002447AC
		private void WriteChar(char ch)
		{
			switch (ch)
			{
			case '\b':
				this.builder.Append('\\');
				this.builder.Append('b');
				return;
			case '\t':
				this.builder.Append('\\');
				this.builder.Append('t');
				return;
			case '\n':
				this.builder.Append('\\');
				this.builder.Append('n');
				return;
			case '\v':
				break;
			case '\f':
				this.builder.Append('\\');
				this.builder.Append('f');
				return;
			case '\r':
				this.builder.Append('\\');
				this.builder.Append('r');
				return;
			default:
				if (ch == '"' || ch == '\\')
				{
					this.builder.Append('\\');
					this.builder.Append(ch);
					return;
				}
				break;
			}
			if (ch < ' ' || ch > '~')
			{
				this.builder.Append('\\');
				this.builder.Append('u');
				this.builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X4}", (int)ch);
				return;
			}
			this.builder.Append(ch);
		}

		// Token: 0x04005B54 RID: 23380
		private StringBuilder builder;

		// Token: 0x04005B55 RID: 23381
		private List<bool> scopes;

		// Token: 0x02001BFB RID: 7163
		private class JsStringWriter : TextWriter
		{
			// Token: 0x0600B2DB RID: 45787 RVA: 0x002466E3 File Offset: 0x002448E3
			public JsStringWriter(JsWriter writer, IFormatProvider formatProvider = null)
				: base(formatProvider)
			{
				this.writer = writer;
				this.writer.WriteComma();
				this.writer.builder.Append('"');
			}

			// Token: 0x17002CDC RID: 11484
			// (get) Token: 0x0600B2DC RID: 45788 RVA: 0x00246711 File Offset: 0x00244911
			public override Encoding Encoding
			{
				get
				{
					return Encoding.ASCII;
				}
			}

			// Token: 0x0600B2DD RID: 45789 RVA: 0x00246718 File Offset: 0x00244918
			public override void Write(char value)
			{
				this.writer.WriteChar(value);
			}

			// Token: 0x0600B2DE RID: 45790 RVA: 0x00246726 File Offset: 0x00244926
			public override void Write(string value)
			{
				this.writer.builder.EnsureCapacity(value.Length);
				base.Write(value);
			}

			// Token: 0x0600B2DF RID: 45791 RVA: 0x00246746 File Offset: 0x00244946
			public override void Close()
			{
				base.Close();
				this.Dispose(true);
			}

			// Token: 0x0600B2E0 RID: 45792 RVA: 0x00246755 File Offset: 0x00244955
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (disposing && this.writer != null)
				{
					this.writer.builder.Append('"');
					this.writer.SetNeedComma();
					this.writer = null;
				}
			}

			// Token: 0x04005B56 RID: 23382
			private JsWriter writer;
		}
	}
}
