using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x02000094 RID: 148
	internal class ProjectionQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0000FBA6 File Offset: 0x0000DDA6
		internal ProjectionQueryOptionExpression(Type type, LambdaExpression lambda, List<string> paths)
			: base(type)
		{
			this.lambda = lambda;
			this.paths = paths;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000FBBD File Offset: 0x0000DDBD
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10009;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		internal LambdaExpression Selector
		{
			get
			{
				return this.lambda;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		internal List<string> Paths
		{
			get
			{
				return this.paths;
			}
		}

		// Token: 0x04000143 RID: 323
		private readonly LambdaExpression lambda;

		// Token: 0x04000144 RID: 324
		private readonly List<string> paths;
	}
}
