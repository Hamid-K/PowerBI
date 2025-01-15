using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000452 RID: 1106
	internal class ObjectSpanRewriter
	{
		// Token: 0x060035E5 RID: 13797 RVA: 0x000AD551 File Offset: 0x000AB751
		internal static bool EntityTypeEquals(EntityTypeBase entityType1, EntityTypeBase entityType2)
		{
			return entityType1 == entityType2;
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000AD558 File Offset: 0x000AB758
		internal static bool TryRewrite(DbQueryCommandTree tree, Span span, MergeOption mergeOption, AliasGenerator aliasGenerator, out DbExpression newQuery, out SpanIndex spanInfo)
		{
			newQuery = null;
			spanInfo = null;
			ObjectSpanRewriter objectSpanRewriter = null;
			bool flag = Span.RequiresRelationshipSpan(mergeOption);
			if (span != null && span.SpanList.Count > 0)
			{
				objectSpanRewriter = new ObjectFullSpanRewriter(tree, tree.Query, span, aliasGenerator);
			}
			else if (flag)
			{
				objectSpanRewriter = new ObjectSpanRewriter(tree, tree.Query, aliasGenerator);
			}
			if (objectSpanRewriter != null)
			{
				objectSpanRewriter.RelationshipSpan = flag;
				newQuery = objectSpanRewriter.RewriteQuery();
				if (newQuery != null)
				{
					spanInfo = objectSpanRewriter.SpanIndex;
				}
			}
			return spanInfo != null;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000AD5D0 File Offset: 0x000AB7D0
		internal ObjectSpanRewriter(DbCommandTree tree, DbExpression toRewrite, AliasGenerator aliasGenerator)
		{
			this._toRewrite = toRewrite;
			this._tree = tree;
			this._aliasGenerator = aliasGenerator;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060035E8 RID: 13800 RVA: 0x000AD5F8 File Offset: 0x000AB7F8
		internal MetadataWorkspace Metadata
		{
			get
			{
				return this._tree.MetadataWorkspace;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000AD605 File Offset: 0x000AB805
		internal DbExpression Query
		{
			get
			{
				return this._toRewrite;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000AD60D File Offset: 0x000AB80D
		// (set) Token: 0x060035EB RID: 13803 RVA: 0x000AD615 File Offset: 0x000AB815
		internal bool RelationshipSpan
		{
			get
			{
				return this._relationshipSpan;
			}
			set
			{
				this._relationshipSpan = value;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x000AD61E File Offset: 0x000AB81E
		internal SpanIndex SpanIndex
		{
			get
			{
				return this._spanIndex;
			}
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000AD628 File Offset: 0x000AB828
		internal DbExpression RewriteQuery()
		{
			DbExpression dbExpression = this.Rewrite(this._toRewrite);
			if (this._toRewrite == dbExpression)
			{
				return null;
			}
			return dbExpression;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000AD650 File Offset: 0x000AB850
		internal ObjectSpanRewriter.SpanTrackingInfo InitializeTrackingInfo(bool createAssociationEndTrackingInfo)
		{
			ObjectSpanRewriter.SpanTrackingInfo spanTrackingInfo = default(ObjectSpanRewriter.SpanTrackingInfo);
			spanTrackingInfo.ColumnDefinitions = new List<KeyValuePair<string, DbExpression>>();
			spanTrackingInfo.ColumnNames = new AliasGenerator(string.Format(CultureInfo.InvariantCulture, "Span{0}_Column", new object[] { this._spanCount }));
			spanTrackingInfo.SpannedColumns = new Dictionary<int, AssociationEndMember>();
			if (createAssociationEndTrackingInfo)
			{
				spanTrackingInfo.FullSpannedEnds = new Dictionary<AssociationEndMember, bool>();
			}
			return spanTrackingInfo;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000AD6BC File Offset: 0x000AB8BC
		internal virtual ObjectSpanRewriter.SpanTrackingInfo CreateEntitySpanTrackingInfo(DbExpression expression, EntityType entityType)
		{
			return default(ObjectSpanRewriter.SpanTrackingInfo);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000AD6D4 File Offset: 0x000AB8D4
		protected DbExpression Rewrite(DbExpression expression)
		{
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind == DbExpressionKind.Element)
			{
				return this.RewriteElementExpression((DbElementExpression)expression);
			}
			if (expressionKind == DbExpressionKind.Limit)
			{
				return this.RewriteLimitExpression((DbLimitExpression)expression);
			}
			BuiltInTypeKind builtInTypeKind = expression.ResultType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.CollectionType)
			{
				return this.RewriteCollection(expression);
			}
			if (builtInTypeKind == BuiltInTypeKind.EntityType)
			{
				return this.RewriteEntity(expression, (EntityType)expression.ResultType.EdmType);
			}
			if (builtInTypeKind != BuiltInTypeKind.RowType)
			{
				return expression;
			}
			return this.RewriteRow(expression, (RowType)expression.ResultType.EdmType);
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000AD768 File Offset: 0x000AB968
		private void AddSpannedRowType(RowType spannedType, TypeUsage originalType)
		{
			if (this._spanIndex == null)
			{
				this._spanIndex = new SpanIndex();
			}
			this._spanIndex.AddSpannedRowType(spannedType, originalType);
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000AD78A File Offset: 0x000AB98A
		private void AddSpanMap(RowType rowType, Dictionary<int, AssociationEndMember> columnMap)
		{
			if (this._spanIndex == null)
			{
				this._spanIndex = new SpanIndex();
			}
			this._spanIndex.AddSpanMap(rowType, columnMap);
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000AD7AC File Offset: 0x000AB9AC
		private DbExpression RewriteEntity(DbExpression expression, EntityType entityType)
		{
			if (DbExpressionKind.NewInstance == expression.ExpressionKind)
			{
				return expression;
			}
			this._spanCount++;
			int spanCount = this._spanCount;
			ObjectSpanRewriter.SpanTrackingInfo spanTrackingInfo = this.CreateEntitySpanTrackingInfo(expression, entityType);
			List<KeyValuePair<AssociationEndMember, AssociationEndMember>> relationshipSpanEnds = this.GetRelationshipSpanEnds(entityType);
			if (relationshipSpanEnds != null)
			{
				if (spanTrackingInfo.ColumnDefinitions == null)
				{
					spanTrackingInfo = this.InitializeTrackingInfo(false);
				}
				int num = spanTrackingInfo.ColumnDefinitions.Count + 1;
				foreach (KeyValuePair<AssociationEndMember, AssociationEndMember> keyValuePair in relationshipSpanEnds)
				{
					if (spanTrackingInfo.FullSpannedEnds == null || !spanTrackingInfo.FullSpannedEnds.ContainsKey(keyValuePair.Value))
					{
						DbExpression dbExpression = null;
						if (!this.TryGetNavigationSource(keyValuePair.Value, out dbExpression))
						{
							dbExpression = expression.GetEntityRef().NavigateAllowingAllRelationshipsInSameTypeHierarchy(keyValuePair.Key, keyValuePair.Value);
						}
						spanTrackingInfo.ColumnDefinitions.Add(new KeyValuePair<string, DbExpression>(spanTrackingInfo.ColumnNames.Next(), dbExpression));
						spanTrackingInfo.SpannedColumns[num] = keyValuePair.Value;
						num++;
					}
				}
			}
			if (spanTrackingInfo.ColumnDefinitions == null)
			{
				this._spanCount--;
				return expression;
			}
			spanTrackingInfo.ColumnDefinitions.Insert(0, new KeyValuePair<string, DbExpression>(string.Format(CultureInfo.InvariantCulture, "Span{0}_SpanRoot", new object[] { spanCount }), expression));
			DbNewInstanceExpression dbNewInstanceExpression = DbExpressionBuilder.NewRow(spanTrackingInfo.ColumnDefinitions);
			RowType rowType = (RowType)dbNewInstanceExpression.ResultType.EdmType;
			this.AddSpanMap(rowType, spanTrackingInfo.SpannedColumns);
			return dbNewInstanceExpression;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000AD948 File Offset: 0x000ABB48
		private DbExpression RewriteElementExpression(DbElementExpression expression)
		{
			DbExpression dbExpression = this.Rewrite(expression.Argument);
			if (expression.Argument != dbExpression)
			{
				expression = dbExpression.Element();
			}
			return expression;
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000AD974 File Offset: 0x000ABB74
		private DbExpression RewriteLimitExpression(DbLimitExpression expression)
		{
			DbExpression dbExpression = this.Rewrite(expression.Argument);
			if (expression.Argument != dbExpression)
			{
				expression = dbExpression.Limit(expression.Limit);
			}
			return expression;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000AD9A8 File Offset: 0x000ABBA8
		private DbExpression RewriteRow(DbExpression expression, RowType rowType)
		{
			DbLambdaExpression dbLambdaExpression = expression as DbLambdaExpression;
			DbNewInstanceExpression dbNewInstanceExpression;
			if (dbLambdaExpression != null)
			{
				dbNewInstanceExpression = dbLambdaExpression.Lambda.Body as DbNewInstanceExpression;
			}
			else
			{
				dbNewInstanceExpression = expression as DbNewInstanceExpression;
			}
			Dictionary<int, DbExpression> dictionary = null;
			Dictionary<int, DbExpression> dictionary2 = null;
			for (int i = 0; i < rowType.Properties.Count; i++)
			{
				EdmProperty edmProperty = rowType.Properties[i];
				DbExpression dbExpression;
				if (dbNewInstanceExpression != null)
				{
					dbExpression = dbNewInstanceExpression.Arguments[i];
				}
				else
				{
					dbExpression = expression.Property(edmProperty.Name);
				}
				DbExpression dbExpression2 = this.Rewrite(dbExpression);
				if (dbExpression2 != dbExpression)
				{
					if (dictionary2 == null)
					{
						dictionary2 = new Dictionary<int, DbExpression>();
					}
					dictionary2[i] = dbExpression2;
				}
				else
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<int, DbExpression>();
					}
					dictionary[i] = dbExpression;
				}
			}
			if (dictionary2 == null)
			{
				return expression;
			}
			List<DbExpression> list = new List<DbExpression>(rowType.Properties.Count);
			List<EdmProperty> list2 = new List<EdmProperty>(rowType.Properties.Count);
			for (int j = 0; j < rowType.Properties.Count; j++)
			{
				EdmProperty edmProperty2 = rowType.Properties[j];
				DbExpression dbExpression3 = null;
				if (!dictionary2.TryGetValue(j, out dbExpression3))
				{
					dbExpression3 = dictionary[j];
				}
				list.Add(dbExpression3);
				list2.Add(new EdmProperty(edmProperty2.Name, dbExpression3.ResultType));
			}
			RowType rowType2 = new RowType(list2, rowType.InitializerMetadata);
			TypeUsage typeUsage = TypeUsage.Create(rowType2);
			DbExpression dbExpression4 = typeUsage.New(list);
			if (dbNewInstanceExpression == null)
			{
				DbExpression dbExpression5 = expression.IsNull();
				DbExpression dbExpression6 = typeUsage.Null();
				dbExpression4 = DbExpressionBuilder.Case(new List<DbExpression>(new DbExpression[] { dbExpression5 }), new List<DbExpression>(new DbExpression[] { dbExpression6 }), dbExpression4);
			}
			this.AddSpannedRowType(rowType2, expression.ResultType);
			if (dbLambdaExpression != null && dbNewInstanceExpression != null)
			{
				dbExpression4 = DbLambda.Create(dbExpression4, dbLambdaExpression.Lambda.Variables).Invoke(dbLambdaExpression.Arguments);
			}
			return dbExpression4;
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000ADB8C File Offset: 0x000ABD8C
		private DbExpression RewriteCollection(DbExpression expression)
		{
			DbExpression dbExpression = expression;
			DbProjectExpression dbProjectExpression = null;
			if (DbExpressionKind.Project == expression.ExpressionKind)
			{
				dbProjectExpression = (DbProjectExpression)expression;
				dbExpression = dbProjectExpression.Input.Expression;
			}
			ObjectSpanRewriter.NavigationInfo navigationInfo = null;
			if (this.RelationshipSpan)
			{
				dbExpression = ObjectSpanRewriter.RelationshipNavigationVisitor.FindNavigationExpression(dbExpression, this._aliasGenerator, out navigationInfo);
			}
			if (navigationInfo != null)
			{
				this.EnterNavigationCollection(navigationInfo);
			}
			else
			{
				this.EnterCollection();
			}
			DbExpression dbExpression2 = expression;
			if (dbProjectExpression != null)
			{
				DbExpression dbExpression3 = this.Rewrite(dbProjectExpression.Projection);
				if (dbProjectExpression.Projection != dbExpression3)
				{
					dbExpression2 = dbExpression.BindAs(dbProjectExpression.Input.VariableName).Project(dbExpression3);
				}
			}
			else
			{
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(this._aliasGenerator.Next());
				DbExpression variable = dbExpressionBinding.Variable;
				DbExpression dbExpression4 = this.Rewrite(variable);
				if (variable != dbExpression4)
				{
					dbExpression2 = dbExpressionBinding.Project(dbExpression4);
				}
			}
			this.ExitCollection();
			if (navigationInfo != null && navigationInfo.InUse)
			{
				List<DbVariableReferenceExpression> list = new List<DbVariableReferenceExpression>(1);
				list.Add(navigationInfo.SourceVariable);
				List<DbExpression> list2 = new List<DbExpression>(1);
				list2.Add(navigationInfo.Source);
				dbExpression2 = DbExpressionBuilder.Lambda(dbExpression2, list).Invoke(list2);
			}
			return dbExpression2;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000ADC9F File Offset: 0x000ABE9F
		private void EnterCollection()
		{
			this._navSources.Push(null);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000ADCAD File Offset: 0x000ABEAD
		private void EnterNavigationCollection(ObjectSpanRewriter.NavigationInfo info)
		{
			this._navSources.Push(info);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000ADCBB File Offset: 0x000ABEBB
		private void ExitCollection()
		{
			this._navSources.Pop();
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000ADCCC File Offset: 0x000ABECC
		private bool TryGetNavigationSource(AssociationEndMember wasSourceNowTargetEnd, out DbExpression source)
		{
			source = null;
			ObjectSpanRewriter.NavigationInfo navigationInfo = null;
			if (this._navSources.Count > 0)
			{
				navigationInfo = this._navSources.Peek();
				if (navigationInfo != null && wasSourceNowTargetEnd != navigationInfo.SourceEnd)
				{
					navigationInfo = null;
				}
			}
			if (navigationInfo != null)
			{
				source = navigationInfo.SourceVariable;
				navigationInfo.InUse = true;
				return true;
			}
			return false;
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000ADD1C File Offset: 0x000ABF1C
		private List<KeyValuePair<AssociationEndMember, AssociationEndMember>> GetRelationshipSpanEnds(EntityType entityType)
		{
			List<KeyValuePair<AssociationEndMember, AssociationEndMember>> list = null;
			if (this._relationshipSpan)
			{
				foreach (AssociationType associationType in this._tree.MetadataWorkspace.GetItems<AssociationType>(DataSpace.CSpace))
				{
					if (2 == associationType.AssociationEndMembers.Count)
					{
						AssociationEndMember associationEndMember = associationType.AssociationEndMembers[0];
						AssociationEndMember associationEndMember2 = associationType.AssociationEndMembers[1];
						if (ObjectSpanRewriter.IsValidRelationshipSpan(entityType, associationType, associationEndMember, associationEndMember2))
						{
							if (list == null)
							{
								list = new List<KeyValuePair<AssociationEndMember, AssociationEndMember>>();
							}
							list.Add(new KeyValuePair<AssociationEndMember, AssociationEndMember>(associationEndMember, associationEndMember2));
						}
						if (ObjectSpanRewriter.IsValidRelationshipSpan(entityType, associationType, associationEndMember2, associationEndMember))
						{
							if (list == null)
							{
								list = new List<KeyValuePair<AssociationEndMember, AssociationEndMember>>();
							}
							list.Add(new KeyValuePair<AssociationEndMember, AssociationEndMember>(associationEndMember2, associationEndMember));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000ADDEC File Offset: 0x000ABFEC
		private static bool IsValidRelationshipSpan(EntityType compareType, AssociationType associationType, AssociationEndMember fromEnd, AssociationEndMember toEnd)
		{
			if (!associationType.IsForeignKey && (RelationshipMultiplicity.One == toEnd.RelationshipMultiplicity || toEnd.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne))
			{
				EntityType entityType = (EntityType)((RefType)fromEnd.TypeUsage.EdmType).ElementType;
				return ObjectSpanRewriter.EntityTypeEquals(compareType, entityType) || TypeSemantics.IsSubTypeOf(compareType, entityType) || TypeSemantics.IsSubTypeOf(entityType, compareType);
			}
			return false;
		}

		// Token: 0x0400116C RID: 4460
		private int _spanCount;

		// Token: 0x0400116D RID: 4461
		private SpanIndex _spanIndex;

		// Token: 0x0400116E RID: 4462
		private readonly DbExpression _toRewrite;

		// Token: 0x0400116F RID: 4463
		private bool _relationshipSpan;

		// Token: 0x04001170 RID: 4464
		private readonly DbCommandTree _tree;

		// Token: 0x04001171 RID: 4465
		private readonly Stack<ObjectSpanRewriter.NavigationInfo> _navSources = new Stack<ObjectSpanRewriter.NavigationInfo>();

		// Token: 0x04001172 RID: 4466
		private readonly AliasGenerator _aliasGenerator;

		// Token: 0x02000A54 RID: 2644
		internal struct SpanTrackingInfo
		{
			// Token: 0x04002A72 RID: 10866
			public List<KeyValuePair<string, DbExpression>> ColumnDefinitions;

			// Token: 0x04002A73 RID: 10867
			public AliasGenerator ColumnNames;

			// Token: 0x04002A74 RID: 10868
			public Dictionary<int, AssociationEndMember> SpannedColumns;

			// Token: 0x04002A75 RID: 10869
			public Dictionary<AssociationEndMember, bool> FullSpannedEnds;
		}

		// Token: 0x02000A55 RID: 2645
		private class NavigationInfo
		{
			// Token: 0x06006190 RID: 24976 RVA: 0x0015014E File Offset: 0x0014E34E
			public NavigationInfo(DbRelationshipNavigationExpression originalNavigation, DbRelationshipNavigationExpression rewrittenNavigation)
			{
				this._sourceEnd = (AssociationEndMember)originalNavigation.NavigateFrom;
				this._sourceRef = (DbVariableReferenceExpression)rewrittenNavigation.NavigationSource;
				this._source = originalNavigation.NavigationSource;
			}

			// Token: 0x170010B4 RID: 4276
			// (get) Token: 0x06006191 RID: 24977 RVA: 0x00150184 File Offset: 0x0014E384
			public AssociationEndMember SourceEnd
			{
				get
				{
					return this._sourceEnd;
				}
			}

			// Token: 0x170010B5 RID: 4277
			// (get) Token: 0x06006192 RID: 24978 RVA: 0x0015018C File Offset: 0x0014E38C
			public DbExpression Source
			{
				get
				{
					return this._source;
				}
			}

			// Token: 0x170010B6 RID: 4278
			// (get) Token: 0x06006193 RID: 24979 RVA: 0x00150194 File Offset: 0x0014E394
			public DbVariableReferenceExpression SourceVariable
			{
				get
				{
					return this._sourceRef;
				}
			}

			// Token: 0x04002A76 RID: 10870
			private readonly DbVariableReferenceExpression _sourceRef;

			// Token: 0x04002A77 RID: 10871
			private readonly AssociationEndMember _sourceEnd;

			// Token: 0x04002A78 RID: 10872
			private readonly DbExpression _source;

			// Token: 0x04002A79 RID: 10873
			public bool InUse;
		}

		// Token: 0x02000A56 RID: 2646
		private class RelationshipNavigationVisitor : DefaultExpressionVisitor
		{
			// Token: 0x06006194 RID: 24980 RVA: 0x0015019C File Offset: 0x0014E39C
			internal static DbExpression FindNavigationExpression(DbExpression expression, AliasGenerator aliasGenerator, out ObjectSpanRewriter.NavigationInfo navInfo)
			{
				navInfo = null;
				TypeUsage typeUsage = ((CollectionType)expression.ResultType.EdmType).TypeUsage;
				if (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsReferenceType(typeUsage))
				{
					return expression;
				}
				ObjectSpanRewriter.RelationshipNavigationVisitor relationshipNavigationVisitor = new ObjectSpanRewriter.RelationshipNavigationVisitor(aliasGenerator);
				DbExpression dbExpression = relationshipNavigationVisitor.Find(expression);
				if (expression != dbExpression)
				{
					navInfo = new ObjectSpanRewriter.NavigationInfo(relationshipNavigationVisitor._original, relationshipNavigationVisitor._rewritten);
					return dbExpression;
				}
				return expression;
			}

			// Token: 0x06006195 RID: 24981 RVA: 0x001501FD File Offset: 0x0014E3FD
			private RelationshipNavigationVisitor(AliasGenerator aliasGenerator)
			{
				this._aliasGenerator = aliasGenerator;
			}

			// Token: 0x06006196 RID: 24982 RVA: 0x0015020C File Offset: 0x0014E40C
			private DbExpression Find(DbExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x06006197 RID: 24983 RVA: 0x00150218 File Offset: 0x0014E418
			protected override DbExpression VisitExpression(DbExpression expression)
			{
				DbExpressionKind expressionKind = expression.ExpressionKind;
				if (expressionKind <= DbExpressionKind.Limit)
				{
					if (expressionKind != DbExpressionKind.Distinct && expressionKind != DbExpressionKind.Filter && expressionKind != DbExpressionKind.Limit)
					{
						return expression;
					}
				}
				else if (expressionKind <= DbExpressionKind.Project)
				{
					if (expressionKind != DbExpressionKind.OfType && expressionKind != DbExpressionKind.Project)
					{
						return expression;
					}
				}
				else if (expressionKind != DbExpressionKind.RelationshipNavigation && expressionKind - DbExpressionKind.Skip > 1)
				{
					return expression;
				}
				return base.VisitExpression(expression);
			}

			// Token: 0x06006198 RID: 24984 RVA: 0x00150268 File Offset: 0x0014E468
			public override DbExpression Visit(DbRelationshipNavigationExpression expression)
			{
				Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
				this._original = expression;
				string text = this._aliasGenerator.Next();
				DbVariableReferenceExpression dbVariableReferenceExpression = new DbVariableReferenceExpression(expression.NavigationSource.ResultType, text);
				this._rewritten = dbVariableReferenceExpression.Navigate(expression.NavigateFrom, expression.NavigateTo);
				return this._rewritten;
			}

			// Token: 0x06006199 RID: 24985 RVA: 0x001502C4 File Offset: 0x0014E4C4
			public override DbExpression Visit(DbFilterExpression expression)
			{
				Check.NotNull<DbFilterExpression>(expression, "expression");
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (dbExpression != expression.Input.Expression)
				{
					return dbExpression.BindAs(expression.Input.VariableName).Filter(expression.Predicate);
				}
				return expression;
			}

			// Token: 0x0600619A RID: 24986 RVA: 0x0015031C File Offset: 0x0014E51C
			public override DbExpression Visit(DbProjectExpression expression)
			{
				Check.NotNull<DbProjectExpression>(expression, "expression");
				DbExpression dbExpression = expression.Projection;
				if (DbExpressionKind.Deref == dbExpression.ExpressionKind)
				{
					dbExpression = ((DbDerefExpression)dbExpression).Argument;
				}
				if (DbExpressionKind.VariableReference == dbExpression.ExpressionKind && ((DbVariableReferenceExpression)dbExpression).VariableName.Equals(expression.Input.VariableName, StringComparison.Ordinal))
				{
					DbExpression dbExpression2 = this.Find(expression.Input.Expression);
					if (dbExpression2 != expression.Input.Expression)
					{
						return dbExpression2.BindAs(expression.Input.VariableName).Project(expression.Projection);
					}
				}
				return expression;
			}

			// Token: 0x0600619B RID: 24987 RVA: 0x001503B8 File Offset: 0x0014E5B8
			public override DbExpression Visit(DbSortExpression expression)
			{
				Check.NotNull<DbSortExpression>(expression, "expression");
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (dbExpression != expression.Input.Expression)
				{
					return dbExpression.BindAs(expression.Input.VariableName).Sort(expression.SortOrder);
				}
				return expression;
			}

			// Token: 0x0600619C RID: 24988 RVA: 0x00150410 File Offset: 0x0014E610
			public override DbExpression Visit(DbSkipExpression expression)
			{
				Check.NotNull<DbSkipExpression>(expression, "expression");
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (dbExpression != expression.Input.Expression)
				{
					return dbExpression.BindAs(expression.Input.VariableName).Skip(expression.SortOrder, expression.Count);
				}
				return expression;
			}

			// Token: 0x04002A7A RID: 10874
			private readonly AliasGenerator _aliasGenerator;

			// Token: 0x04002A7B RID: 10875
			private DbRelationshipNavigationExpression _original;

			// Token: 0x04002A7C RID: 10876
			private DbRelationshipNavigationExpression _rewritten;
		}
	}
}
