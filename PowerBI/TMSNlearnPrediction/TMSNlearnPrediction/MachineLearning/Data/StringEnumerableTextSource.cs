using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000142 RID: 322
	public sealed class StringEnumerableTextSource : IMultiStreamSource
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x00022EB4 File Offset: 0x000210B4
		public StringEnumerableTextSource(IExceptionContext ctx, IEnumerable<string> enumerable)
		{
			Contracts.CheckValue<IExceptionContext>(ctx, ctx, "ctx");
			this._ctx = ctx;
			Contracts.CheckValue<IEnumerable<string>>(this._ctx, enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00022EE7 File Offset: 0x000210E7
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00022EEA File Offset: 0x000210EA
		public string GetPathOrNull(int index)
		{
			return null;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00022EED File Offset: 0x000210ED
		public Stream Open(int index)
		{
			return new TextReaderStream(new StringEnumerableTextSource.StringEnumerableTextReader(this._ctx, this._enumerable));
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00022F05 File Offset: 0x00021105
		public TextReader OpenTextReader(int index)
		{
			return new StringEnumerableTextSource.StringEnumerableTextReader(this._ctx, this._enumerable);
		}

		// Token: 0x04000352 RID: 850
		private readonly IExceptionContext _ctx;

		// Token: 0x04000353 RID: 851
		private readonly IEnumerable<string> _enumerable;

		// Token: 0x02000143 RID: 323
		private sealed class StringEnumerableTextReader : TextReader
		{
			// Token: 0x06000689 RID: 1673 RVA: 0x00022F18 File Offset: 0x00021118
			public StringEnumerableTextReader(IExceptionContext ctx, IEnumerable<string> enumerable)
			{
				Contracts.CheckValue<IExceptionContext>(ctx, ctx, "ctx");
				this._ctx = ctx;
				Contracts.CheckValue<IEnumerable<string>>(this._ctx, enumerable, "enumerable");
				this._enumerator = enumerable.GetEnumerator();
				this._newLine = Environment.NewLine;
				this._newLineLength = this._newLine.Length;
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x00022F78 File Offset: 0x00021178
			public override int Read()
			{
				int num = this.Peek();
				if (num == -1)
				{
					return -1;
				}
				if (num == 10)
				{
					this._row = (this._enumerator.MoveNext() ? this._enumerator.Current : null);
					this._position = 0;
					return 10;
				}
				this._position++;
				return num;
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x00022FD4 File Offset: 0x000211D4
			public override int Peek()
			{
				this.CheckNotDisposed();
				if (!this.MoveToFirst())
				{
					return -1;
				}
				int length = this._row.Length;
				if (this._position >= length)
				{
					return (int)this._newLine[this._position - length];
				}
				return (int)this._row[this._position];
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0002302C File Offset: 0x0002122C
			public override string ReadLine()
			{
				this.CheckNotDisposed();
				if (!this.MoveToFirst())
				{
					return null;
				}
				if (this._position == 0)
				{
					string text = this._enumerator.Current;
					this._row = (this._enumerator.MoveNext() ? this._enumerator.Current : null);
					return text;
				}
				return base.ReadLine();
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x00023088 File Offset: 0x00021288
			public override string ReadToEnd()
			{
				this.CheckNotDisposed();
				StringBuilder stringBuilder = new StringBuilder();
				string text;
				while ((text = this.ReadLine()) != null)
				{
					stringBuilder.AppendLine(text);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x000230BB File Offset: 0x000212BB
			protected override void Dispose(bool disposing)
			{
				this._enumerator.Dispose();
				this._disposed = true;
				base.Dispose(disposing);
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x000230D6 File Offset: 0x000212D6
			private void CheckNotDisposed()
			{
				Contracts.Check(this._ctx, !this._disposed, "The reader has already been disposed.");
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x000230F1 File Offset: 0x000212F1
			private bool MoveToFirst()
			{
				if (this._row == null)
				{
					if (!this._enumerator.MoveNext())
					{
						return false;
					}
					this._row = this._enumerator.Current;
				}
				return true;
			}

			// Token: 0x04000354 RID: 852
			private const char EndOfLine = '\n';

			// Token: 0x04000355 RID: 853
			private readonly IEnumerator<string> _enumerator;

			// Token: 0x04000356 RID: 854
			private readonly string _newLine;

			// Token: 0x04000357 RID: 855
			private readonly int _newLineLength;

			// Token: 0x04000358 RID: 856
			private readonly IExceptionContext _ctx;

			// Token: 0x04000359 RID: 857
			private string _row;

			// Token: 0x0400035A RID: 858
			private int _position;

			// Token: 0x0400035B RID: 859
			private bool _disposed;
		}
	}
}
