using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client
{
	// Token: 0x020000CA RID: 202
	[SuppressMessage("Microsoft.Design", "CA1010", Justification = "required for this feature")]
	[SuppressMessage("Microsoft.Naming", "CA1710", Justification = "required for this feature")]
	public sealed class ChangeOperationResponse : OperationResponse
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0001C182 File Offset: 0x0001A382
		internal ChangeOperationResponse(HeaderCollection headers, Descriptor descriptor)
			: base(headers)
		{
			this.descriptor = descriptor;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001C192 File Offset: 0x0001A392
		public Descriptor Descriptor
		{
			get
			{
				return this.descriptor;
			}
		}

		// Token: 0x040002EA RID: 746
		private Descriptor descriptor;
	}
}
