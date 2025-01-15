using System;
using System.IO;

namespace Microsoft.Internal
{
	// Token: 0x0200018D RID: 397
	internal class ContextAwareTextReader<T, U> : TextReader where T : struct, IContext<U> where U : struct, IDisposable
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x0000E954 File Offset: 0x0000CB54
		public ContextAwareTextReader(T context, TextReader reader)
		{
			this.context = context;
			this.reader = reader;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000E96C File Offset: 0x0000CB6C
		public override int Peek()
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				num = this.reader.Peek();
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		public override int Read()
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				num = this.reader.Read();
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		public override int Read(char[] buffer, int index, int count)
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				num = this.reader.Read(buffer, index, count);
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				num = this.reader.ReadBlock(buffer, index, count);
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public override string ReadLine()
		{
			T t = this.context;
			U u = t.Enter();
			string text;
			try
			{
				text = this.reader.ReadLine();
			}
			finally
			{
				u.Dispose();
			}
			return text;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000EB04 File Offset: 0x0000CD04
		public override string ReadToEnd()
		{
			T t = this.context;
			U u = t.Enter();
			string text;
			try
			{
				text = this.reader.ReadToEnd();
			}
			finally
			{
				u.Dispose();
			}
			return text;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public override void Close()
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.reader.Close();
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				T t = this.context;
				U u = t.Enter();
				try
				{
					this.reader.Dispose();
				}
				finally
				{
					u.Dispose();
				}
			}
		}

		// Token: 0x0400049C RID: 1180
		private readonly T context;

		// Token: 0x0400049D RID: 1181
		private readonly TextReader reader;
	}
}
