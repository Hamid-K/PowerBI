using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200008C RID: 140
	[NullableContext(1)]
	[Nullable(0)]
	public struct Status
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		[NullableContext(2)]
		public Status(StatusCode statusCode, string message)
		{
			this.statusCode = statusCode;
			this.message = message;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		public StatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		public static Status OK()
		{
			return new Status(StatusCode.OK, null);
		}

		// Token: 0x060003E7 RID: 999
		[DllImport("ParquetSharpNative")]
		internal static extern void String_Append(IntPtr cppString, string toAppend);

		// Token: 0x0400011D RID: 285
		private readonly StatusCode statusCode;

		// Token: 0x0400011E RID: 286
		[Nullable(2)]
		private readonly string message;

		// Token: 0x02000124 RID: 292
		// (Invoke) Token: 0x060009A6 RID: 2470
		[NullableContext(0)]
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate void GetMessage(IntPtr thunk, IntPtr cppString);
	}
}
