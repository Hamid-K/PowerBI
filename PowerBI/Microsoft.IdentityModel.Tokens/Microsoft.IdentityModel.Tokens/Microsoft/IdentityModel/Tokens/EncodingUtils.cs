using System;
using System.Buffers;
using System.Text;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200012F RID: 303
	internal static class EncodingUtils
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003B364 File Offset: 0x00039564
		internal static T PerformEncodingDependentOperation<T>(string input, Encoding encoding, Func<byte[], int, T> action)
		{
			return EncodingUtils.PerformEncodingDependentOperation<T>(input, 0, input.Length, encoding, action);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003B378 File Offset: 0x00039578
		internal static T PerformEncodingDependentOperation<T>(string input, int offset, int length, Encoding encoding, Func<byte[], int, T> action)
		{
			int num = encoding.GetMaxByteCount(length);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			T t;
			try
			{
				num = encoding.GetBytes(input, offset, length, array, 0);
				t = action(array, num);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return t;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003B3D0 File Offset: 0x000395D0
		internal static T PerformEncodingDependentOperation<T, TX, TY, TZ>(string input, int offset, int length, Encoding encoding, TX argx, TY argy, TZ argz, Func<byte[], int, TX, TY, TZ, T> action)
		{
			int num = encoding.GetMaxByteCount(length);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			T t;
			try
			{
				num = encoding.GetBytes(input, offset, length, array, 0);
				t = action(array, num, argx, argy, argz);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return t;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003B430 File Offset: 0x00039630
		internal static T PerformEncodingDependentOperation<T, TX>(string input, Encoding encoding, TX parameter, Func<byte[], int, TX, T> action)
		{
			return EncodingUtils.PerformEncodingDependentOperation<T, TX>(input, 0, input.Length, encoding, parameter, action);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003B444 File Offset: 0x00039644
		internal static T PerformEncodingDependentOperation<T, TX>(string input, int offset, int length, Encoding encoding, TX parameter, Func<byte[], int, TX, T> action)
		{
			int num = encoding.GetMaxByteCount(length);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			T t;
			try
			{
				num = encoding.GetBytes(input, offset, length, array, 0);
				t = action(array, num, parameter);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return t;
		}
	}
}
