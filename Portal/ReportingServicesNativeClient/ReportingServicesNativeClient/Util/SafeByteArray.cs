using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Util
{
	// Token: 0x0200001A RID: 26
	internal class SafeByteArray : SafeHandle
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00009CF0 File Offset: 0x000090F0
		internal SafeByteArray()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00009D1C File Offset: 0x0000911C
		internal unsafe static SafeByteArray Create(byte[] managedArray)
		{
			long num = (long)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
			SafeByteArray safeByteArray = new SafeByteArray();
			RuntimeHelpers.PrepareConstrainedRegions();
			uint exceptionCode;
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				byte* ptr2;
				try
				{
				}
				finally
				{
					byte* ptr = <Module>.new[]((ulong)((long)managedArray.Length));
					ptr2 = ptr;
					if (ptr != null)
					{
						IntPtr intPtr = new IntPtr((void*)ptr);
						safeByteArray.SetHandle(intPtr);
					}
				}
				safeByteArray.m_cb = (ulong)((long)managedArray.Length);
				IntPtr intPtr2 = (IntPtr)((void*)ptr2);
				Marshal.Copy(managedArray, 0, intPtr2, managedArray.Length);
			}
			catch when (delegate
			{
				// Failed to create a 'catch-when' expression
				exceptionCode = (uint)Marshal.GetExceptionCode();
				endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
			})
			{
				uint num2 = 0U;
				<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
				try
				{
					try
					{
						if (safeByteArray != null)
						{
							safeByteArray.Close();
						}
						<Module>._CxxThrowException(null, null);
						goto IL_00AA;
					}
					catch when (delegate
					{
						// Failed to create a 'catch-when' expression
						num2 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
						endfilter(num2 != 0U);
					})
					{
					}
					if (num2 != 0U)
					{
						throw;
					}
					IL_00AA:;
				}
				finally
				{
					<Module>.__CxxUnregisterExceptionObject(num, (int)num2);
				}
			}
			return safeByteArray;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007280 File Offset: 0x00006680
		internal unsafe byte* ToPointer()
		{
			return (byte*)this.handle.ToPointer();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000A004 File Offset: 0x00009404
		internal void ZeroArray()
		{
			if (!this.IsInvalid)
			{
				<Module>.RtlSecureZeroMemory(this.handle.ToPointer(), this.m_cb);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00009E4C File Offset: 0x0000924C
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			<Module>.delete[](this.handle.ToPointer());
			return true;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00009AA4 File Offset: 0x00008EA4
		public override bool IsInvalid
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x04000058 RID: 88
		private ulong m_cb = 0UL;
	}
}
