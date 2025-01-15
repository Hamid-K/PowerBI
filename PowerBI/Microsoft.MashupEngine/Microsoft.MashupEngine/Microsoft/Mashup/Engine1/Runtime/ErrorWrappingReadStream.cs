using System;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001293 RID: 4755
	internal class ErrorWrappingReadStream : DelegatingStream
	{
		// Token: 0x06007CF1 RID: 31985 RVA: 0x0000FF57 File Offset: 0x0000E157
		public ErrorWrappingReadStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x001ACA7C File Offset: 0x001AAC7C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			try
			{
				num = base.Read(buffer, offset, count);
			}
			catch (ValueException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw ValueException.NewDataSourceError(ex.Message, Value.Null, ex);
			}
			catch (Exception ex2)
			{
				if (!SafeExceptions.IsSafeException(ex2))
				{
					throw;
				}
				throw ValueException.NewDataFormatError(ex2.Message, Value.Null, ex2);
			}
			return num;
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x001ACAF4 File Offset: 0x001AACF4
		public override int ReadByte()
		{
			int num;
			try
			{
				num = base.ReadByte();
			}
			catch (ValueException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw ValueException.NewDataSourceError(ex.Message, Value.Null, ex);
			}
			catch (Exception ex2)
			{
				if (!SafeExceptions.IsSafeException(ex2))
				{
					throw;
				}
				throw ValueException.NewDataFormatError(ex2.Message, Value.Null, ex2);
			}
			return num;
		}
	}
}
