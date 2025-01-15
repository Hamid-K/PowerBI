using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EC6 RID: 3782
	internal sealed class ReplaceBlobActionValue : ActionValue
	{
		// Token: 0x0600646F RID: 25711 RVA: 0x00157FEA File Offset: 0x001561EA
		public ReplaceBlobActionValue(AdlsBinaryValue target, AdlsBinaryValue source)
		{
			this.target = target;
			this.source = source;
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x06006470 RID: 25712 RVA: 0x00158000 File Offset: 0x00156200
		public AdlsBinaryValue Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x06006471 RID: 25713 RVA: 0x00158008 File Offset: 0x00156208
		public AdlsBinaryValue Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x00158010 File Offset: 0x00156210
		public override Value Execute()
		{
			throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this.target, null);
		}

		// Token: 0x040036E6 RID: 14054
		private readonly AdlsBinaryValue target;

		// Token: 0x040036E7 RID: 14055
		private readonly AdlsBinaryValue source;
	}
}
