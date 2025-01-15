using System;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000115 RID: 277
	public abstract class EdmValue : IEdmValue, IEdmElement, IEdmDelayedValue
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0000DD4E File Offset: 0x0000BF4E
		protected EdmValue(IEdmTypeReference type)
		{
			this.type = type;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000DD5D File Offset: 0x0000BF5D
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000581 RID: 1409
		public abstract EdmValueKind ValueKind { get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000DD65 File Offset: 0x0000BF65
		IEdmValue IEdmDelayedValue.Value
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000219 RID: 537
		private readonly IEdmTypeReference type;
	}
}
