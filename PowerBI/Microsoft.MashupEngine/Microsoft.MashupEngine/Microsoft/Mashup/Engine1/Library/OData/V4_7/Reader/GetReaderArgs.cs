using System;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x0200078F RID: 1935
	internal sealed class GetReaderArgs
	{
		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x000B70D9 File Offset: 0x000B52D9
		// (set) Token: 0x060038BC RID: 14524 RVA: 0x000B70E1 File Offset: 0x000B52E1
		public bool Catch404 { get; set; }

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x000B70EA File Offset: 0x000B52EA
		// (set) Token: 0x060038BE RID: 14526 RVA: 0x000B70F2 File Offset: 0x000B52F2
		public Uri Uri { get; set; }

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x000B70FB File Offset: 0x000B52FB
		// (set) Token: 0x060038C0 RID: 14528 RVA: 0x000B7103 File Offset: 0x000B5303
		public int? Column { get; set; }

		// Token: 0x060038C1 RID: 14529 RVA: 0x000B710C File Offset: 0x000B530C
		internal static Lazy<IODataPayloadReader> MakeReader(Func<IODataPayloadReader> ctor)
		{
			return new Lazy<IODataPayloadReader>(ctor);
		}
	}
}
