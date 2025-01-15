using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200004B RID: 75
	public static class IMultipleResultsExtensions
	{
		// Token: 0x06000276 RID: 630 RVA: 0x00008334 File Offset: 0x00006534
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static IRowset GetResult(this IMultipleResults multipleResults)
		{
			Guid iunknown = IID.IUnknown;
			IntPtr intPtr;
			int result = multipleResults.GetResult(IntPtr.Zero, DBRESULTFLAG.DBRESULTFLAG_DEFAULT, ref iunknown, null, out intPtr);
			if (result == 265929)
			{
				TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceWarning("IMultipleResultsExtensions: hr is DB_S_NORESULT");
				return null;
			}
			Marshal.ThrowExceptionForHR(result);
			if (intPtr == IntPtr.Zero)
			{
				if (intPtr == IntPtr.Zero)
				{
					TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceWarning("IMultipleResultsExtensions: punk is IntPtr.Zero");
				}
				return null;
			}
			object objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			Marshal.Release(intPtr);
			return (IRowset)objectForIUnknown;
		}
	}
}
