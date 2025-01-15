using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D1 RID: 209
	[SuppressMessage("Microsoft.Design", "CA1010", Justification = "required for this feature")]
	[SuppressMessage("Microsoft.Naming", "CA1710", Justification = "required for this feature")]
	public abstract class OperationResponse
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001C9CE File Offset: 0x0001ABCE
		internal OperationResponse(HeaderCollection headers)
		{
			this.headers = headers;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001C9DD File Offset: 0x0001ABDD
		public IDictionary<string, string> Headers
		{
			get
			{
				return this.headers.UnderlyingDictionary;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001C9EA File Offset: 0x0001ABEA
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001C9F2 File Offset: 0x0001ABF2
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			internal set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001C9FB File Offset: 0x0001ABFB
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001CA03 File Offset: 0x0001AC03
		public Exception Error
		{
			get
			{
				return this.innerException;
			}
			set
			{
				this.innerException = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		internal HeaderCollection HeaderCollection
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x040002F4 RID: 756
		private readonly HeaderCollection headers;

		// Token: 0x040002F5 RID: 757
		private int statusCode;

		// Token: 0x040002F6 RID: 758
		private Exception innerException;
	}
}
