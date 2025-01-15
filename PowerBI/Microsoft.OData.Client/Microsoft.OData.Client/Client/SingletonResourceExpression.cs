using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x02000018 RID: 24
	[DebuggerDisplay("SingletonResourceExpression {Source}.{MemberExpression}")]
	internal class SingletonResourceExpression : QueryableResourceExpression
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004A0C File Offset: 0x00002C0C
		internal SingletonResourceExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
			: base(type, source, memberExpression, resourceType, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion)
		{
			base.UseFilterAsPredicate = true;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004A38 File Offset: 0x00002C38
		internal SingletonResourceExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion, string functionName, Dictionary<string, string> functionParameters, bool isAction)
			: base(type, source, memberExpression, resourceType, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion, functionName, functionParameters, isAction)
		{
			base.UseFilterAsPredicate = true;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004A69 File Offset: 0x00002C69
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10001;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004A70 File Offset: 0x00002C70
		internal override bool IsSingleton
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000495F File Offset: 0x00002B5F
		internal override bool IsOperationInvocation
		{
			get
			{
				return base.OperationName != null;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004A74 File Offset: 0x00002C74
		protected override QueryableResourceExpression CreateCloneWithNewTypes(Type newType, Type newResourceType)
		{
			return new SingletonResourceExpression(newType, this.source, base.MemberExpression, newResourceType, this.ExpandPaths.ToList<string>(), this.CountOption, this.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), base.Projection, base.ResourceTypeAs, base.UriVersion, base.OperationName, base.OperationParameters, base.IsAction);
		}
	}
}
