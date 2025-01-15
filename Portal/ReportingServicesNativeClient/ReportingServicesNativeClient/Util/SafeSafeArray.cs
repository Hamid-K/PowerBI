using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Util
{
	// Token: 0x02000018 RID: 24
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class SafeSafeArray : SafeHandle
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00009A54 File Offset: 0x00008E54
		public SafeSafeArray()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00009ACC File Offset: 0x00008ECC
		public unsafe static SafeSafeArray Create(byte[] managedArray)
		{
			long num = (long)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
			SafeSafeArray safeSafeArray = new SafeSafeArray();
			if (managedArray == null)
			{
				return null;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			uint exceptionCode;
			try
			{
				tagSAFEARRAYBOUND tagSAFEARRAYBOUND = managedArray.Length;
				*((ref tagSAFEARRAYBOUND) + 4) = 0;
				RuntimeHelpers.PrepareConstrainedRegions();
				tagSAFEARRAY* ptr;
				try
				{
				}
				finally
				{
					ptr = <Module>.SafeArrayCreate(17, 1U, &tagSAFEARRAYBOUND);
					if (ptr != null)
					{
						IntPtr intPtr = new IntPtr((void*)ptr);
						safeSafeArray.SetHandle(intPtr);
					}
				}
				if (ptr == null)
				{
					throw new OutOfMemoryException();
				}
				IntPtr intPtr2 = (IntPtr)(*(long*)(ptr + 16L / (long)sizeof(tagSAFEARRAY)));
				Marshal.Copy(managedArray, 0, intPtr2, managedArray.Length);
				return safeSafeArray;
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
						if (safeSafeArray != null)
						{
							safeSafeArray.Close();
						}
						<Module>._CxxThrowException(null, null);
						goto IL_00C0;
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
					IL_00C0:;
				}
				finally
				{
					<Module>.__CxxUnregisterExceptionObject(num, (int)num2);
				}
			}
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007240 File Offset: 0x00006640
		public unsafe tagSAFEARRAY* ToPointer()
		{
			return (tagSAFEARRAY*)this.handle.ToPointer();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00009F8C File Offset: 0x0000938C
		public unsafe void ZeroArray()
		{
			if (!this.IsInvalid)
			{
				<Module>.RtlSecureZeroMemory(*(long*)((byte*)this.handle.ToPointer() + 16L), (ulong)(*(int*)((byte*)this.handle.ToPointer() + 24L)));
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00009C18 File Offset: 0x00009018
		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe override bool ReleaseHandle()
		{
			return ((0 == <Module>.SafeArrayDestroy((tagSAFEARRAY*)this.handle.ToPointer())) ? 1 : 0) != 0;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00009AA4 File Offset: 0x00008EA4
		public override bool IsInvalid
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
