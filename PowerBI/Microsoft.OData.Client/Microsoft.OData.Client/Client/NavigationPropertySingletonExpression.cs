using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A5 RID: 165
	internal class NavigationPropertySingletonExpression : ResourceExpression
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00014FE8 File Offset: 0x000131E8
		internal NavigationPropertySingletonExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
			: base(source, type, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion)
		{
			this.memberExpression = memberExpression;
			this.resourceType = resourceType;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00015018 File Offset: 0x00013218
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10003;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001501F File Offset: 0x0001321F
		internal MemberExpression MemberExpression
		{
			get
			{
				return (MemberExpression)this.memberExpression;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001502C File Offset: 0x0001322C
		internal override Type ResourceType
		{
			get
			{
				return this.resourceType;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00004A70 File Offset: 0x00002C70
		internal override bool IsSingleton
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00015034 File Offset: 0x00013234
		internal override bool HasQueryOptions
		{
			get
			{
				return this.ExpandPaths.Count > 0 || this.CountOption == CountOption.CountQuery || this.CustomQueryOptions.Count > 0 || base.Projection != null;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00015066 File Offset: 0x00013266
		internal override bool IsOperationInvocation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001506C File Offset: 0x0001326C
		internal override ResourceExpression CreateCloneWithNewType(Type type)
		{
			return new NavigationPropertySingletonExpression(type, this.source, this.MemberExpression, TypeSystem.GetElementType(type), this.ExpandPaths.ToList<string>(), this.CountOption, this.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), base.Projection, base.ResourceTypeAs, base.UriVersion);
		}

		// Token: 0x04000237 RID: 567
		private readonly Expression memberExpression;

		// Token: 0x04000238 RID: 568
		private readonly Type resourceType;
	}
}
