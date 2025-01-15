using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OleDbInternalWrapperException : Exception
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004151 File Offset: 0x00002351
		internal OleDbInternalWrapperException(OleDbInternalErrorKind kind, Exception wrappedException)
		{
			this.m_kind = kind;
			this.m_wrappedException = wrappedException;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004167 File Offset: 0x00002367
		internal OleDbInternalErrorKind ErrorKind
		{
			get
			{
				return this.m_kind;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000416F File Offset: 0x0000236F
		internal Exception WrappedException
		{
			get
			{
				return this.m_wrappedException;
			}
		}

		// Token: 0x04000047 RID: 71
		private readonly OleDbInternalErrorKind m_kind;

		// Token: 0x04000048 RID: 72
		private readonly Exception m_wrappedException;
	}
}
