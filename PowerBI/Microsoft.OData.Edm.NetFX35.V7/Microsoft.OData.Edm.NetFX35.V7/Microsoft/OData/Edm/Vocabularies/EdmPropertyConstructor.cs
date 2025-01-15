using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F9 RID: 249
	public class EdmPropertyConstructor : EdmElement, IEdmPropertyConstructor, IEdmElement
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x00013BBA File Offset: 0x00011DBA
		public EdmPropertyConstructor(string name, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00013BE8 File Offset: 0x00011DE8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400041F RID: 1055
		private readonly string name;

		// Token: 0x04000420 RID: 1056
		private readonly IEdmExpression value;
	}
}
