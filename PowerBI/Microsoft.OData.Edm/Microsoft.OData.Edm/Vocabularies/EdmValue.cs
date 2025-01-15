using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000131 RID: 305
	public abstract class EdmValue : IEdmValue, IEdmElement, IEdmDelayedValue
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x000124F4 File Offset: 0x000106F4
		protected EdmValue(IEdmTypeReference type)
		{
			this.type = type;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00012503 File Offset: 0x00010703
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060007D9 RID: 2009
		public abstract EdmValueKind ValueKind { get; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001250B File Offset: 0x0001070B
		IEdmValue IEdmDelayedValue.Value
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400033A RID: 826
		private readonly IEdmTypeReference type;
	}
}
