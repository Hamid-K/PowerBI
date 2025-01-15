using System;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.IO
{
	// Token: 0x0200002E RID: 46
	public static class StreamExtensions
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0001021C File Offset: 0x0000E41C
		public static void CopyTo(this Stream source, Stream destination)
		{
			source.CopyTo(destination, 4096);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0001022C File Offset: 0x0000E42C
		public static void CopyTo(this Stream source, Stream destination, int bufferSize)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (!source.CanRead && !source.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment40.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment40.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!source.CanRead)
			{
				throw new NotSupportedException(Environment40.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment40.GetResourceString("NotSupported_UnwritableStream"));
			}
			StreamExtensions.InternalCopyTo(source, destination, bufferSize);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000102C8 File Offset: 0x0000E4C8
		private static void InternalCopyTo(Stream source, Stream destination, int bufferSize)
		{
			byte[] array = new byte[bufferSize];
			int num;
			while ((num = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, num);
			}
		}

		// Token: 0x0400002F RID: 47
		private const int _DefaultBufferSize = 4096;
	}
}
