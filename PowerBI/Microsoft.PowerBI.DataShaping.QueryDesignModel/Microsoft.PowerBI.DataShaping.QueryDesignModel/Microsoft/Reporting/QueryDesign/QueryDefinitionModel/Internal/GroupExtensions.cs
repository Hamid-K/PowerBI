using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000DF RID: 223
	internal static class GroupExtensions
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x00023426 File Offset: 0x00021626
		internal static IEnumerable<EntitySet> FindEntitySetReferences(this Group group)
		{
			return group.Keys.SelectMany((GroupKey key) => key.Expression.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)).Distinct<EntitySet>();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00023457 File Offset: 0x00021657
		internal static IEnumerable<IConceptualEntity> FindEntityReferences(this Group group)
		{
			return group.Keys.SelectMany((GroupKey key) => key.Expression.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)).Distinct<IConceptualEntity>();
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00023488 File Offset: 0x00021688
		internal static QueryExpression GetSingleKeyExpression(this Group group)
		{
			return group.Keys.Single<GroupKey>().Expression;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002349A File Offset: 0x0002169A
		internal static QueryExpression GetSingleGeneratedGroupKeyDataTableExpression(this Group group)
		{
			ArgumentValidation.CheckNotNull<Group>(group, "group");
			ArgumentValidation.CheckAs<GeneratedGroupKey>(group.Keys.Single<GroupKey>(), "generatedGroupKey");
			return (group.Keys.Single<GroupKey>() as GeneratedGroupKey).DataTableExpression;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000234D3 File Offset: 0x000216D3
		internal static IEnumerable<string> OmitMissingGroups(this IEnumerable<string> groupRefs, IEnumerable<Group> groups)
		{
			return groupRefs.Intersect(groups.Select((Group g) => g.Name), EdmItem.IdentityComparer);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00023505 File Offset: 0x00021705
		internal static bool SequenceEqualByName(this IEnumerable<string> groupRefs, IEnumerable<Group> groups)
		{
			return groupRefs.SequenceEqual(groups.Select((Group g) => g.Name), EdmItem.IdentityComparer);
		}
	}
}
