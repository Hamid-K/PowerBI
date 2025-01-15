using System;
using System.IO;
using System.Linq;
using System.Text;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x02000143 RID: 323
	public static class StreamHelpers
	{
		// Token: 0x06000FB1 RID: 4017 RVA: 0x00028240 File Offset: 0x00026440
		public static void CopyAndSkipBom(this Stream input, Stream output, Encoding encoding)
		{
			int num = EncodingHelpers.Utf8BOM.Length;
			byte[] array = new byte[num];
			long position = input.Position;
			input.Read(array, 0, num);
			if (array.SequenceEqual(EncodingHelpers.Utf8BOM))
			{
				InternalLogger.Debug("input has UTF8 BOM");
			}
			else
			{
				InternalLogger.Debug("input hasn't a UTF8 BOM");
				input.Position = position;
			}
			input.Copy(output);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0002829E File Offset: 0x0002649E
		public static void Copy(this Stream input, Stream output)
		{
			input.CopyWithOffset(output, 0);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000282A8 File Offset: 0x000264A8
		public static void CopyWithOffset(this Stream input, Stream output, int offset)
		{
			if (offset < 0)
			{
				throw new ArgumentException("negative offset");
			}
			if (offset > 0)
			{
				input.Seek((long)offset, SeekOrigin.Current);
			}
			byte[] array = new byte[4096];
			int num;
			while ((num = input.Read(array, 0, array.Length)) > 0)
			{
				output.Write(array, 0, num);
			}
		}
	}
}
