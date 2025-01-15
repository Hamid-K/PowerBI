using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000034 RID: 52
	public sealed class UriEntityOperationParameter : UriOperationParameter
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000816B File Offset: 0x0000636B
		public UriEntityOperationParameter(string name, object value)
			: base(name, value)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008175 File Offset: 0x00006375
		public UriEntityOperationParameter(string name, object value, bool useEntityReference)
			: this(name, value)
		{
			this.useEntityReference = useEntityReference;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008186 File Offset: 0x00006386
		internal bool UseEntityReference
		{
			get
			{
				return this.useEntityReference;
			}
		}

		// Token: 0x0400008B RID: 139
		private readonly bool useEntityReference;
	}
}
