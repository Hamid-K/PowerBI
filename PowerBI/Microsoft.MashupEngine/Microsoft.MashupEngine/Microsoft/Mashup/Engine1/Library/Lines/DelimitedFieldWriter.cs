using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009CA RID: 2506
	internal class DelimitedFieldWriter
	{
		// Token: 0x06004756 RID: 18262 RVA: 0x000EF076 File Offset: 0x000ED276
		public DelimitedFieldWriter(string delimiter, bool quoting)
			: this(new DelimitedFieldWriter.StringEnumerable(delimiter), quoting)
		{
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x000EF085 File Offset: 0x000ED285
		public DelimitedFieldWriter(IEnumerable<string> delimiters, bool quoting)
		{
			this.delimiters = delimiters.GetEnumerator();
			this.quoting = quoting;
			this.delimiter = string.Empty;
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x000EF0AC File Offset: 0x000ED2AC
		public void WriteField(string text)
		{
			if (this.builder == null)
			{
				this.builder = new StringBuilder();
			}
			else
			{
				this.builder.Append(this.delimiter);
			}
			if (this.delimiters.MoveNext())
			{
				this.delimiter = this.delimiters.Current;
			}
			else
			{
				this.delimiter = string.Empty;
			}
			if (this.quoting && DelimitedFieldWriter.RequiresQuoting(text, this.delimiter))
			{
				this.builder.Append('"');
				foreach (char c in text)
				{
					this.builder.Append(c);
					if (c == '"')
					{
						this.builder.Append('"');
					}
				}
				this.builder.Append('"');
				return;
			}
			this.builder.Append(text);
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x000EF184 File Offset: 0x000ED384
		public override string ToString()
		{
			if (this.builder == null)
			{
				return string.Empty;
			}
			return this.builder.ToString();
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x000EF1A0 File Offset: 0x000ED3A0
		private static bool RequiresQuoting(string text, string delimiter)
		{
			int num = ((delimiter.Length > 0) ? ((int)delimiter[0]) : (-1));
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c == '\n' || c == '\r' || c == '"')
				{
					return true;
				}
				if ((int)c == num && DelimitedFieldWriter.StartsWith(text, i, delimiter))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x000EF1FC File Offset: 0x000ED3FC
		private static bool StartsWith(string text, int offset, string value)
		{
			if (offset + value.Length > text.Length)
			{
				return false;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (text[offset + i] != value[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040025F2 RID: 9714
		private IEnumerator<string> delimiters;

		// Token: 0x040025F3 RID: 9715
		private StringBuilder builder;

		// Token: 0x040025F4 RID: 9716
		private string delimiter;

		// Token: 0x040025F5 RID: 9717
		private bool quoting;

		// Token: 0x020009CB RID: 2507
		private class StringEnumerable : IEnumerable<string>, IEnumerable
		{
			// Token: 0x0600475C RID: 18268 RVA: 0x000EF241 File Offset: 0x000ED441
			public StringEnumerable(string text)
			{
				this.enumerator = new DelimitedFieldWriter.StringEnumerable.StringEnumerator(text);
			}

			// Token: 0x0600475D RID: 18269 RVA: 0x000EF255 File Offset: 0x000ED455
			public IEnumerator<string> GetEnumerator()
			{
				return this.enumerator;
			}

			// Token: 0x0600475E RID: 18270 RVA: 0x000EF25D File Offset: 0x000ED45D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040025F6 RID: 9718
			private IEnumerator<string> enumerator;

			// Token: 0x020009CC RID: 2508
			private class StringEnumerator : IEnumerator<string>, IDisposable, IEnumerator
			{
				// Token: 0x0600475F RID: 18271 RVA: 0x000EF265 File Offset: 0x000ED465
				public StringEnumerator(string value)
				{
					this.value = value;
				}

				// Token: 0x170016C7 RID: 5831
				// (get) Token: 0x06004760 RID: 18272 RVA: 0x000EF274 File Offset: 0x000ED474
				public string Current
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x06004761 RID: 18273 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x170016C8 RID: 5832
				// (get) Token: 0x06004762 RID: 18274 RVA: 0x000EF274 File Offset: 0x000ED474
				object IEnumerator.Current
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x06004763 RID: 18275 RVA: 0x00002139 File Offset: 0x00000339
				public bool MoveNext()
				{
					return true;
				}

				// Token: 0x06004764 RID: 18276 RVA: 0x0000336E File Offset: 0x0000156E
				public void Reset()
				{
				}

				// Token: 0x040025F7 RID: 9719
				private string value;
			}
		}
	}
}
