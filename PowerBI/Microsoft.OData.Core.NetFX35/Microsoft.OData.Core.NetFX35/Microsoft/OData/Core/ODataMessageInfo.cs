using System;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200017E RID: 382
	public sealed class ODataMessageInfo
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00031B94 File Offset: 0x0002FD94
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00031B9C File Offset: 0x0002FD9C
		public ODataMediaType MediaType { get; internal set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00031BA5 File Offset: 0x0002FDA5
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00031BAD File Offset: 0x0002FDAD
		public Encoding Encoding { get; internal set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00031BB6 File Offset: 0x0002FDB6
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00031BBE File Offset: 0x0002FDBE
		public IEdmModel Model { get; internal set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00031BC7 File Offset: 0x0002FDC7
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00031BCF File Offset: 0x0002FDCF
		public bool IsResponse { get; internal set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00031BD8 File Offset: 0x0002FDD8
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00031BE0 File Offset: 0x0002FDE0
		public Func<Stream> GetMessageStream { get; internal set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00031BE9 File Offset: 0x0002FDE9
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00031BF1 File Offset: 0x0002FDF1
		public IODataUrlResolver UrlResolver { get; internal set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00031BFA File Offset: 0x0002FDFA
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00031C02 File Offset: 0x0002FE02
		internal ODataPayloadKind PayloadKind { get; set; }
	}
}
