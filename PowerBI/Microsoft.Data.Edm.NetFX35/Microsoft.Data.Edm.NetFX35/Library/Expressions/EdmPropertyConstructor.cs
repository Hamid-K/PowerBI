using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x02000198 RID: 408
	public class EdmPropertyConstructor : EdmElement, IEdmPropertyConstructor, IEdmElement
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0001854E File Offset: 0x0001674E
		public EdmPropertyConstructor(string name, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001857C File Offset: 0x0001677C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00018584 File Offset: 0x00016784
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000460 RID: 1120
		private readonly string name;

		// Token: 0x04000461 RID: 1121
		private readonly IEdmExpression value;
	}
}
