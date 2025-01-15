using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001CA RID: 458
	public class EdmPropertyConstructor : EdmElement, IEdmPropertyConstructor, IEdmElement
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x000197AD File Offset: 0x000179AD
		public EdmPropertyConstructor(string name, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x000197DB File Offset: 0x000179DB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000197E3 File Offset: 0x000179E3
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040004B1 RID: 1201
		private readonly string name;

		// Token: 0x040004B2 RID: 1202
		private readonly IEdmExpression value;
	}
}
