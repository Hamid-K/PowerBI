using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.CompressionInterop
{
	// Token: 0x02000008 RID: 8
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class XPress9Compression
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020B3 File Offset: 0x000002B3
		internal static int GetAllocationSize(int size)
		{
			RuntimeChecks.Check((double)size * 1.1 <= 2147483647.0, "The size of the input buffer is too large for compression");
			return Math.Max((int)((double)size * 1.1), 4096);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020F0 File Offset: 0x000002F0
		internal unsafe static int Compress(byte[] inBuffer, int offset, int count, uint level, out byte[] outBuffer)
		{
			RuntimeChecks.CheckNonEmpty<byte>(inBuffer, "inBuffer");
			RuntimeChecks.Check(offset + count <= inBuffer.Length, "Input buffer too small.");
			outBuffer = new byte[XPress9Compression.GetAllocationSize(count)];
			byte[] array = XPress9Compression.ConvertSizeToHeader((uint)count);
			for (int i = 0; i < array.Length; i++)
			{
				outBuffer[i] = array[i];
			}
			uint num = (uint)(outBuffer.Length - array.Length);
			int num2;
			try
			{
				try
				{
					fixed (byte* ptr = &inBuffer[offset])
					{
						byte* ptr2 = ptr;
						try
						{
							fixed (byte* ptr3 = &outBuffer[4])
							{
								byte* ptr4 = ptr3;
								num2 = NativeMethods.Compress(ptr2, (uint)count, ptr4, &num, level);
							}
						}
						finally
						{
							byte* ptr3 = null;
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
			}
			catch (BadImageFormatException ex)
			{
				throw new XPress9CompressionException(FormattableString.Invariant(FormattableStringFactory.Create("{0} Is64BitProcess: {1}.", new object[]
				{
					ex.Message,
					Environment.Is64BitProcess
				})), GatewayExceptionUtils.InnerExceptionCreator.GetPipelineInnerException(ex, null), Array.Empty<PowerBIErrorDetail>());
			}
			if (num2 != 0)
			{
				XPress9Compression.HandleExceptions();
			}
			return (int)(num + 4U);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220C File Offset: 0x0000040C
		internal static byte[] Decompress(byte[] inBuffer, uint level)
		{
			byte[] array = null;
			byte[] array2;
			XPress9Compression.Decompress(inBuffer, level, out array2, ref array, 0, 0);
			return array2;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		internal unsafe static int Decompress(byte[] inBuffer, uint level, out byte[] outBuffer, ref byte[] readBuffer, int readBufferOffset, int readBufferCount)
		{
			RuntimeChecks.CheckNonEmpty<byte>(inBuffer, "inBuffer");
			RuntimeChecks.Check(inBuffer.Length > 4, "Input buffer too small.");
			byte[] array = new byte[4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = inBuffer[i];
			}
			uint num = XPress9Compression.ConvertHeaderToSize(array);
			if (num == 0U)
			{
				throw new XPress9DecompressionException("The header value is invalid", Array.Empty<PowerBIErrorDetail>());
			}
			uint num2 = (uint)inBuffer.Length;
			int num3;
			if (readBuffer != null && (long)readBufferCount >= (long)((ulong)num))
			{
				outBuffer = readBuffer;
				num3 = readBufferOffset;
			}
			else
			{
				outBuffer = new byte[num];
				num3 = 0;
			}
			int num4;
			try
			{
				try
				{
					fixed (byte* ptr = &inBuffer[4])
					{
						byte* ptr2 = ptr;
						try
						{
							fixed (byte* ptr3 = &outBuffer[num3])
							{
								byte* ptr4 = ptr3;
								num4 = NativeMethods.Decompress(ptr2, num2 - 4U, num, ptr4, level);
							}
						}
						finally
						{
							byte* ptr3 = null;
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
			}
			catch (BadImageFormatException ex)
			{
				throw new XPress9CompressionException(FormattableString.Invariant(FormattableStringFactory.Create("{0} Is64BitProcess: {1}.", new object[]
				{
					ex.Message,
					Environment.Is64BitProcess
				})), GatewayExceptionUtils.InnerExceptionCreator.GetPipelineInnerException(ex, null), Array.Empty<PowerBIErrorDetail>());
			}
			if (num4 != 0)
			{
				XPress9Compression.HandleExceptions();
			}
			return (int)num;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002364 File Offset: 0x00000564
		internal static byte[] ConvertSizeToHeader(uint size)
		{
			byte[] bytes = BitConverter.GetBytes(size);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			if (bytes.Length != 4)
			{
				throw new XPress9CompressionException("Header size is too large.", Array.Empty<PowerBIErrorDetail>());
			}
			return bytes;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000239C File Offset: 0x0000059C
		internal static uint ConvertHeaderToSize(byte[] header)
		{
			if (BitConverter.IsLittleEndian)
			{
				byte[] array = new byte[header.Length];
				Array.Copy(header, array, header.Length);
				Array.Reverse(array);
				return BitConverter.ToUInt32(array, 0);
			}
			return BitConverter.ToUInt32(header, 0);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023D8 File Offset: 0x000005D8
		private static void HandleExceptions()
		{
			throw new XPress9CompressionException(XPress9Compression.GetErrorMsg(), Array.Empty<PowerBIErrorDetail>());
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023E9 File Offset: 0x000005E9
		private static string GetErrorMsg()
		{
			return XPress9Compression.ErrorMessageBytesToString(NativeMethods.GetErrorMessage());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023F8 File Offset: 0x000005F8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private unsafe static string ErrorMessageBytesToString(byte* array)
		{
			if (array == null)
			{
				return null;
			}
			List<char> list = new List<char>();
			int num = 0;
			for (;;)
			{
				char c = (char)array[num];
				if (c == '\0')
				{
					break;
				}
				list.Add(c);
				num++;
			}
			return new string(list.ToArray());
		}

		// Token: 0x04000012 RID: 18
		private const int c_minCompressedSize = 4096;

		// Token: 0x04000013 RID: 19
		private const double c_looseRatio = 1.1;

		// Token: 0x04000014 RID: 20
		private const int c_headerLength = 4;
	}
}
