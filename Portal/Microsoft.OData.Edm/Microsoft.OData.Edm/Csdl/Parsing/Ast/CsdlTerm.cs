using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E7 RID: 487
	internal class CsdlTerm : CsdlNamedElement
	{
		// Token: 0x06000D72 RID: 3442 RVA: 0x00025DD0 File Offset: 0x00023FD0
		public CsdlTerm(string name, CsdlTypeReference type, string appliesTo, string defaultValue, CsdlLocation location)
			: base(name, location)
		{
			this.type = type;
			this.appliesTo = appliesTo;
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x00025DF1 File Offset: 0x00023FF1
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00025DF9 File Offset: 0x00023FF9
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00025E01 File Offset: 0x00024001
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x04000767 RID: 1895
		private readonly CsdlTypeReference type;

		// Token: 0x04000768 RID: 1896
		private readonly string appliesTo;

		// Token: 0x04000769 RID: 1897
		private readonly string defaultValue;
	}
}
