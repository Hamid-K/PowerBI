using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000679 RID: 1657
	internal sealed class CollectionTypeDefinition : Node
	{
		// Token: 0x06004F0A RID: 20234 RVA: 0x0011F788 File Offset: 0x0011D988
		internal CollectionTypeDefinition(Node elementTypeDef)
		{
			this._elementTypeDef = elementTypeDef;
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x0011F797 File Offset: 0x0011D997
		internal Node ElementTypeDef
		{
			get
			{
				return this._elementTypeDef;
			}
		}

		// Token: 0x04001CC0 RID: 7360
		private readonly Node _elementTypeDef;
	}
}
