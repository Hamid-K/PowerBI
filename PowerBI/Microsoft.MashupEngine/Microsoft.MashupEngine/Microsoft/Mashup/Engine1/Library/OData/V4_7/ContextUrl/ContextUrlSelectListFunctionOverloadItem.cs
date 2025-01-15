using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x0200081C RID: 2076
	internal sealed class ContextUrlSelectListFunctionOverloadItem : ContextUrlSelectListItem
	{
		// Token: 0x06003BF3 RID: 15347 RVA: 0x000C2928 File Offset: 0x000C0B28
		public ContextUrlSelectListFunctionOverloadItem(EdmPathExpression pathToFunction, IEnumerable<string> parameterNames)
		{
			this.pathToFunction = pathToFunction;
			this.parameterNames = parameterNames;
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x0000244F File Offset: 0x0000064F
		public override SelectListItemKind Kind
		{
			get
			{
				return SelectListItemKind.FunctionOverload;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x000C293E File Offset: 0x000C0B3E
		public EdmPathExpression PathToFunction
		{
			get
			{
				return this.pathToFunction;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000C2946 File Offset: 0x000C0B46
		public IEnumerable<string> ParameterNames
		{
			get
			{
				return this.parameterNames;
			}
		}

		// Token: 0x04001F3A RID: 7994
		private readonly EdmPathExpression pathToFunction;

		// Token: 0x04001F3B RID: 7995
		private readonly IEnumerable<string> parameterNames;
	}
}
