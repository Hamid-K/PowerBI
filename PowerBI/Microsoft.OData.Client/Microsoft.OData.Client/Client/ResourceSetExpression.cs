using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x02000017 RID: 23
	[DebuggerDisplay("ResourceSetExpression {Source}.{MemberExpression}")]
	internal class ResourceSetExpression : QueryableResourceExpression
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000048EC File Offset: 0x00002AEC
		internal ResourceSetExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
			: base(type, source, memberExpression, resourceType, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004910 File Offset: 0x00002B10
		internal ResourceSetExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion, string operationName, Dictionary<string, string> operationParameters, bool isAction)
			: base(type, source, memberExpression, resourceType, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion, operationName, operationParameters, isAction)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000493A File Offset: 0x00002B3A
		public override ExpressionType NodeType
		{
			get
			{
				if (this.source == null)
				{
					return (ExpressionType)10000;
				}
				return (ExpressionType)10002;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000494F File Offset: 0x00002B4F
		internal override bool IsSingleton
		{
			get
			{
				return base.KeyPredicateConjuncts.Count > 0;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000494F File Offset: 0x00002B4F
		internal bool HasKeyPredicate
		{
			get
			{
				return base.KeyPredicateConjuncts.Count > 0;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000495F File Offset: 0x00002B5F
		internal override bool IsOperationInvocation
		{
			get
			{
				return base.OperationName != null;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000496C File Offset: 0x00002B6C
		protected override QueryableResourceExpression CreateCloneWithNewTypes(Type newType, Type newResourceType)
		{
			return new ResourceSetExpression(newType, this.source, base.MemberExpression, newResourceType, this.ExpandPaths.ToList<string>(), this.CountOption, this.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), base.Projection, base.ResourceTypeAs, base.UriVersion, base.OperationName, base.OperationParameters, base.IsAction);
		}
	}
}
