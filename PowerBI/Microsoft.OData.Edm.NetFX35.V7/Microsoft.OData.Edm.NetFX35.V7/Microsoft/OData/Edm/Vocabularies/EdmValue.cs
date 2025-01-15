using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000124 RID: 292
	public abstract class EdmValue : IEdmValue, IEdmElement, IEdmDelayedValue
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x00014014 File Offset: 0x00012214
		protected EdmValue(IEdmTypeReference type)
		{
			this.type = type;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00014023 File Offset: 0x00012223
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000799 RID: 1945
		public abstract EdmValueKind ValueKind { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001402B File Offset: 0x0001222B
		IEdmValue IEdmDelayedValue.Value
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000435 RID: 1077
		private readonly IEdmTypeReference type;
	}
}
