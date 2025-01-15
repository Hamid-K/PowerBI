using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F2 RID: 242
	public class EdmPropertyConstructor : EdmElement, IEdmPropertyConstructor, IEdmElement
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0001209E File Offset: 0x0001029E
		public EdmPropertyConstructor(string name, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000120CC File Offset: 0x000102CC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x000120D4 File Offset: 0x000102D4
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000313 RID: 787
		private readonly string name;

		// Token: 0x04000314 RID: 788
		private readonly IEdmExpression value;
	}
}
