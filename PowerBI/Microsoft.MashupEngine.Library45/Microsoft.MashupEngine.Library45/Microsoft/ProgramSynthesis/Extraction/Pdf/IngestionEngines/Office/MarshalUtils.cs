using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DB9 RID: 3513
	internal static class MarshalUtils
	{
		// Token: 0x0600594F RID: 22863 RVA: 0x0011BFC4 File Offset: 0x0011A1C4
		[NullableContext(1)]
		[return: Nullable(new byte[] { 2, 1 })]
		public static T[] MarshalCClassArray<T>(this IntPtr array, UIntPtr length) where T : class, new()
		{
			if (array == IntPtr.Zero)
			{
				return null;
			}
			if (length.ToUInt64() > 2147483647UL)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("Attempted to marshal a too large {0} array (length={1}).", typeof(T), length));
			}
			T[] array2 = new T[(uint)length];
			int num = Marshal.SizeOf(typeof(T));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new T();
				Marshal.PtrToStructure<T>(array, array2[i]);
				array += num;
			}
			return array2;
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x0011C064 File Offset: 0x0011A264
		[return: Nullable(new byte[] { 2, 1 })]
		public static TResult[] MarshalCArray<[Nullable(2)] TResult, TStruct>(this IntPtr array, UIntPtr length, [Nullable(new byte[] { 1, 0, 1 })] Func<TStruct, TResult> func) where TStruct : struct
		{
			if (array == IntPtr.Zero)
			{
				return null;
			}
			if (length.ToUInt64() > 2147483647UL)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("Attempted to marshal a too large {0} array (length={1}).", typeof(TStruct), length));
			}
			TResult[] array2 = new TResult[(uint)length];
			int num = Marshal.SizeOf(typeof(TStruct));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = func((TStruct)((object)Marshal.PtrToStructure(array, typeof(TStruct))));
				array += num;
			}
			return array2;
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x0011C10A File Offset: 0x0011A30A
		[return: Nullable(new byte[] { 2, 0 })]
		public static T[] MarshalCStructArray<T>(this IntPtr array, UIntPtr length) where T : struct
		{
			return array.MarshalCArray(length, (T x) => x);
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x0011C132 File Offset: 0x0011A332
		[NullableContext(1)]
		public static T MarshalCArrayElement<[Nullable(2)] T>(this IntPtr array, int idx)
		{
			return (T)((object)Marshal.PtrToStructure(array + idx * Marshal.SizeOf(typeof(T)), typeof(T)));
		}
	}
}
