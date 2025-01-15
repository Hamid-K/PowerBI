using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EC5 RID: 3781
	internal sealed class CreateBlobActionValue : ActionValue
	{
		// Token: 0x0600646C RID: 25708 RVA: 0x00157FC0 File Offset: 0x001561C0
		public CreateBlobActionValue(AdlsBinaryValue target)
		{
			this.target = target;
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x0600646D RID: 25709 RVA: 0x00157FCF File Offset: 0x001561CF
		public AdlsBinaryValue Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x00157FD7 File Offset: 0x001561D7
		public override Value Execute()
		{
			throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this.target, null);
		}

		// Token: 0x040036E5 RID: 14053
		private readonly AdlsBinaryValue target;
	}
}
