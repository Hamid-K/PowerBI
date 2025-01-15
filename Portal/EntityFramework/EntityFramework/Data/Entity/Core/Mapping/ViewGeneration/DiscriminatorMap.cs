using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000568 RID: 1384
	internal class DiscriminatorMap
	{
		// Token: 0x06004379 RID: 17273 RVA: 0x000E9C3C File Offset: 0x000E7E3C
		private DiscriminatorMap(DbPropertyExpression discriminator, List<KeyValuePair<object, EntityType>> typeMap, Dictionary<EdmProperty, DbExpression> propertyMap, Dictionary<RelProperty, DbExpression> relPropertyMap, EntitySet entitySet)
		{
			this.Discriminator = discriminator;
			this.TypeMap = new ReadOnlyCollection<KeyValuePair<object, EntityType>>(typeMap);
			this.PropertyMap = new ReadOnlyCollection<KeyValuePair<EdmProperty, DbExpression>>(propertyMap.ToList<KeyValuePair<EdmProperty, DbExpression>>());
			this.RelPropertyMap = new ReadOnlyCollection<KeyValuePair<RelProperty, DbExpression>>(relPropertyMap.ToList<KeyValuePair<RelProperty, DbExpression>>());
			this.EntitySet = entitySet;
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x000E9C90 File Offset: 0x000E7E90
		internal static bool TryCreateDiscriminatorMap(EntitySet entitySet, DbExpression queryView, out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			if (queryView.ExpressionKind != DbExpressionKind.Project)
			{
				return false;
			}
			DbProjectExpression dbProjectExpression = (DbProjectExpression)queryView;
			if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.Case)
			{
				return false;
			}
			DbCaseExpression dbCaseExpression = (DbCaseExpression)dbProjectExpression.Projection;
			if (dbProjectExpression.Projection.ResultType.EdmType.BuiltInTypeKind != BuiltInTypeKind.EntityType)
			{
				return false;
			}
			if (dbProjectExpression.Input.Expression.ExpressionKind != DbExpressionKind.Filter)
			{
				return false;
			}
			DbFilterExpression dbFilterExpression = (DbFilterExpression)dbProjectExpression.Input.Expression;
			HashSet<object> discriminatorDomain = new HashSet<object>();
			if (!ViewSimplifier.TryMatchDiscriminatorPredicate(dbFilterExpression, delegate(DbComparisonExpression equalsExp, object discriminatorValue)
			{
				discriminatorDomain.Add(discriminatorValue);
			}))
			{
				return false;
			}
			List<KeyValuePair<object, EntityType>> list = new List<KeyValuePair<object, EntityType>>();
			Dictionary<EdmProperty, DbExpression> dictionary = new Dictionary<EdmProperty, DbExpression>();
			Dictionary<RelProperty, DbExpression> dictionary2 = new Dictionary<RelProperty, DbExpression>();
			Dictionary<EntityType, List<RelProperty>> dictionary3 = new Dictionary<EntityType, List<RelProperty>>();
			DbPropertyExpression dbPropertyExpression = null;
			EdmProperty edmProperty = null;
			for (int i = 0; i < dbCaseExpression.When.Count; i++)
			{
				DbExpression dbExpression = dbCaseExpression.When[i];
				DbExpression dbExpression2 = dbCaseExpression.Then[i];
				string variableName = dbProjectExpression.Input.VariableName;
				DbPropertyExpression dbPropertyExpression2;
				object obj;
				if (!ViewSimplifier.TryMatchPropertyEqualsValue(dbExpression, variableName, out dbPropertyExpression2, out obj))
				{
					return false;
				}
				if (edmProperty == null)
				{
					edmProperty = (EdmProperty)dbPropertyExpression2.Property;
				}
				else if (edmProperty != dbPropertyExpression2.Property)
				{
					return false;
				}
				dbPropertyExpression = dbPropertyExpression2;
				EntityType entityType;
				if (!DiscriminatorMap.TryMatchEntityTypeConstructor(dbExpression2, dictionary, dictionary2, dictionary3, out entityType))
				{
					return false;
				}
				list.Add(new KeyValuePair<object, EntityType>(obj, entityType));
				discriminatorDomain.Remove(obj);
			}
			if (1 != discriminatorDomain.Count)
			{
				return false;
			}
			EntityType entityType2;
			if (dbCaseExpression.Else == null || !DiscriminatorMap.TryMatchEntityTypeConstructor(dbCaseExpression.Else, dictionary, dictionary2, dictionary3, out entityType2))
			{
				return false;
			}
			list.Add(new KeyValuePair<object, EntityType>(discriminatorDomain.Single<object>(), entityType2));
			if (!DiscriminatorMap.CheckForMissingRelProperties(dictionary2, dictionary3))
			{
				return false;
			}
			int num = list.Select((KeyValuePair<object, EntityType> map) => map.Key).Distinct(TrailingSpaceComparer.Instance).Count<object>();
			int count = list.Count;
			if (num != count)
			{
				return false;
			}
			discriminatorMap = new DiscriminatorMap(dbPropertyExpression, list, dictionary, dictionary2, entitySet);
			return true;
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x000E9EAC File Offset: 0x000E80AC
		private static bool CheckForMissingRelProperties(Dictionary<RelProperty, DbExpression> relPropertyMap, Dictionary<EntityType, List<RelProperty>> typeToRelPropertyMap)
		{
			foreach (RelProperty relProperty in relPropertyMap.Keys)
			{
				foreach (KeyValuePair<EntityType, List<RelProperty>> keyValuePair in typeToRelPropertyMap)
				{
					if (keyValuePair.Key.IsSubtypeOf(relProperty.FromEnd.TypeUsage.EdmType) && !keyValuePair.Value.Contains(relProperty))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x000E9F64 File Offset: 0x000E8164
		private static bool TryMatchEntityTypeConstructor(DbExpression then, Dictionary<EdmProperty, DbExpression> propertyMap, Dictionary<RelProperty, DbExpression> relPropertyMap, Dictionary<EntityType, List<RelProperty>> typeToRelPropertyMap, out EntityType entityType)
		{
			if (then.ExpressionKind != DbExpressionKind.NewInstance)
			{
				entityType = null;
				return false;
			}
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)then;
			entityType = (EntityType)dbNewInstanceExpression.ResultType.EdmType;
			for (int i = 0; i < entityType.Properties.Count; i++)
			{
				EdmProperty edmProperty = entityType.Properties[i];
				DbExpression dbExpression = dbNewInstanceExpression.Arguments[i];
				DbExpression dbExpression2;
				if (propertyMap.TryGetValue(edmProperty, out dbExpression2))
				{
					if (!DiscriminatorMap.ExpressionsCompatible(dbExpression, dbExpression2))
					{
						return false;
					}
				}
				else
				{
					propertyMap.Add(edmProperty, dbExpression);
				}
			}
			if (dbNewInstanceExpression.HasRelatedEntityReferences)
			{
				List<RelProperty> list;
				if (!typeToRelPropertyMap.TryGetValue(entityType, out list))
				{
					list = new List<RelProperty>();
					typeToRelPropertyMap[entityType] = list;
				}
				foreach (DbRelatedEntityRef dbRelatedEntityRef in dbNewInstanceExpression.RelatedEntityReferences)
				{
					RelProperty relProperty = new RelProperty((RelationshipType)dbRelatedEntityRef.TargetEnd.DeclaringType, dbRelatedEntityRef.SourceEnd, dbRelatedEntityRef.TargetEnd);
					DbExpression targetEntityReference = dbRelatedEntityRef.TargetEntityReference;
					DbExpression dbExpression3;
					if (relPropertyMap.TryGetValue(relProperty, out dbExpression3))
					{
						if (!DiscriminatorMap.ExpressionsCompatible(targetEntityReference, dbExpression3))
						{
							return false;
						}
					}
					else
					{
						relPropertyMap.Add(relProperty, targetEntityReference);
					}
					list.Add(relProperty);
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x000EA0BC File Offset: 0x000E82BC
		private static bool ExpressionsCompatible(DbExpression x, DbExpression y)
		{
			if (x.ExpressionKind != y.ExpressionKind)
			{
				return false;
			}
			DbExpressionKind expressionKind = x.ExpressionKind;
			if (expressionKind <= DbExpressionKind.Property)
			{
				if (expressionKind != DbExpressionKind.NewInstance)
				{
					if (expressionKind == DbExpressionKind.Property)
					{
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)x;
						DbPropertyExpression dbPropertyExpression2 = (DbPropertyExpression)y;
						return dbPropertyExpression.Property == dbPropertyExpression2.Property && DiscriminatorMap.ExpressionsCompatible(dbPropertyExpression.Instance, dbPropertyExpression2.Instance);
					}
				}
				else
				{
					DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)x;
					DbNewInstanceExpression dbNewInstanceExpression2 = (DbNewInstanceExpression)y;
					if (!dbNewInstanceExpression.ResultType.EdmType.EdmEquals(dbNewInstanceExpression2.ResultType.EdmType))
					{
						return false;
					}
					for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
					{
						if (!DiscriminatorMap.ExpressionsCompatible(dbNewInstanceExpression.Arguments[i], dbNewInstanceExpression2.Arguments[i]))
						{
							return false;
						}
					}
					return true;
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.Ref)
				{
					DbRefExpression dbRefExpression = (DbRefExpression)x;
					DbRefExpression dbRefExpression2 = (DbRefExpression)y;
					return dbRefExpression.EntitySet.EdmEquals(dbRefExpression2.EntitySet) && DiscriminatorMap.ExpressionsCompatible(dbRefExpression.Argument, dbRefExpression2.Argument);
				}
				if (expressionKind == DbExpressionKind.VariableReference)
				{
					return ((DbVariableReferenceExpression)x).VariableName == ((DbVariableReferenceExpression)y).VariableName;
				}
			}
			return false;
		}

		// Token: 0x0400181A RID: 6170
		internal readonly DbPropertyExpression Discriminator;

		// Token: 0x0400181B RID: 6171
		internal readonly ReadOnlyCollection<KeyValuePair<object, EntityType>> TypeMap;

		// Token: 0x0400181C RID: 6172
		internal readonly ReadOnlyCollection<KeyValuePair<EdmProperty, DbExpression>> PropertyMap;

		// Token: 0x0400181D RID: 6173
		internal readonly ReadOnlyCollection<KeyValuePair<RelProperty, DbExpression>> RelPropertyMap;

		// Token: 0x0400181E RID: 6174
		internal readonly EntitySet EntitySet;
	}
}
