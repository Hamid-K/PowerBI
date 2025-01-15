using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B3 RID: 179
	public sealed class Hierarchy : ModelingObject, IOwned<SemanticQuery>, IXmlLoadable, IDeserializationCallback, IXmlWriteable, ICompileable, IValidationScope
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x000219D8 File Offset: 0x0001FBD8
		public Hierarchy()
		{
			this.m_groupings = new Hierarchy.GroupingCollection(this);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000219EC File Offset: 0x0001FBEC
		public Hierarchy(IQueryEntity baseEntity)
			: this()
		{
			this.BaseEntity = baseEntity;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x000219FB File Offset: 0x0001FBFB
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00021A03 File Offset: 0x0001FC03
		public IQueryEntity BaseEntity
		{
			get
			{
				return this.m_baseEntity;
			}
			set
			{
				if (value != null && EntityRefNode.IsBadIQueryEntity(value))
				{
					throw new ArgumentOutOfRangeException(DevExceptionMessages.EntityRefNode_UnexpectedIQueryEntity);
				}
				base.CheckWriteable();
				this.m_baseEntity = (IQueryEntityInternal)value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00021A2D File Offset: 0x0001FC2D
		public Hierarchy.GroupingCollection Groupings
		{
			get
			{
				return this.m_groupings;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00021A35 File Offset: 0x0001FC35
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00021A3D File Offset: 0x0001FC3D
		public Expression Filter
		{
			get
			{
				return this.m_filter;
			}
			set
			{
				base.CheckWriteable();
				this.m_filter = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00021A4C File Offset: 0x0001FC4C
		public SemanticQuery Query
		{
			get
			{
				return this.m_query;
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00021A54 File Offset: 0x0001FC54
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		void IOwned<SemanticQuery>.SetOwner(SemanticQuery newQuery)
		{
			if (this.m_query != null && newQuery != null)
			{
				throw new InvalidOperationException(DevExceptionMessages.ExistingOwner);
			}
			this.m_query = newQuery;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00021A74 File Offset: 0x0001FC74
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.Validation.PushScope(this);
			try
			{
				xr.LoadObject("Hierarchy", this);
			}
			finally
			{
				xr.Validation.PopScope();
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00021AC4 File Offset: 0x0001FCC4
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "BaseEntity")
				{
					xr.LoadObject(this);
					return true;
				}
				if (localName == "EntityID")
				{
					xr.Context.AddReference(this, xr.ReadReferenceByID("BaseEntity.EntityID", false));
					return true;
				}
				if (localName == "Groupings")
				{
					this.m_groupings.Load(xr);
					return true;
				}
				if (localName == "Filter")
				{
					xr.LoadObject(new Hierarchy.FilterLoader(this));
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00021B56 File Offset: 0x0001FD56
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "BaseEntity.EntityID")
			{
				this.m_baseEntity = ctx.CurrentModel.TryGetModelItem<ModelEntity>(reference, ctx.Validation);
				return true;
			}
			return false;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00021B88 File Offset: 0x0001FD88
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Hierarchy");
			if (this.m_baseEntity != null)
			{
				xw.WriteStartElement("BaseEntity");
				xw.WriteReferenceElement("EntityID", this.m_baseEntity);
				xw.WriteEndElement();
			}
			this.m_groupings.WriteTo(xw);
			if (this.m_filter != null)
			{
				xw.WriteStartElement("Filter");
				this.m_filter.WriteTo(xw);
				xw.WriteEndElement();
			}
			xw.WriteEndElement();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00021C01 File Offset: 0x0001FE01
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00021C0C File Offset: 0x0001FE0C
		internal void Compile(CompilationContext ctx)
		{
			if (this.m_query == null)
			{
				throw new InternalModelingException("m_query is null");
			}
			ctx.PushScope(this);
			try
			{
				base.Compile(ctx.ShouldPersist);
				if (this.m_baseEntity == null)
				{
					ctx.AddScopedError(ModelingErrorCode.MissingBaseEntity, SRErrors.MissingBaseEntity("BaseEntity", ctx.CurrentObjectDescriptor));
				}
				else if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.m_baseEntity.IsInvalidRefTarget)
				{
					if (this.m_baseEntity.ModelEntity == null)
					{
						throw new InternalModelingException("Null or unrecognized IQueryEntity");
					}
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound("BaseEntity.EntityID", ctx.CurrentObjectDescriptor, this.m_baseEntity.ModelEntity.ID.ToString()));
				}
				else if (this.m_baseEntity.Model != this.m_query.Model)
				{
					ctx.AddScopedError(ModelingErrorCode.WrongSemanticModel, SRErrors.WrongSemanticModel_QueryItem("BaseEntity", ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_baseEntity)));
				}
				else
				{
					ctx.PushContextEntity(this.m_baseEntity);
					try
					{
						this.m_groupings.Compile(ctx);
						if (ctx.ShouldApplySecurityFilters)
						{
							Expression expression = this.TryGetSecurityFilterCondition(ctx);
							if (expression != null)
							{
								if (this.m_filter == null)
								{
									this.m_filter = expression;
								}
								else
								{
									this.m_filter = new Expression(new FunctionNode(FunctionName.And, new Expression[] { expression, this.m_filter }));
								}
							}
						}
						if (this.m_filter != null)
						{
							this.m_filter.Compile(ctx, ExpressionCompilationFlags.Filter);
						}
					}
					finally
					{
						ctx.PopContextEntity();
					}
				}
			}
			finally
			{
				ctx.PopScope();
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00021DCC File Offset: 0x0001FFCC
		private Expression TryGetSecurityFilterCondition(CompilationContext ctx)
		{
			Dictionary<PathItem, Hierarchy.PathNode> dictionary = new Dictionary<PathItem, Hierarchy.PathNode>(this.m_groupings.Count);
			for (int i = 0; i < this.m_groupings.Count; i++)
			{
				Grouping grouping = this.m_groupings[i];
				ExpressionPath expressionPath;
				if (grouping.Expression.Path.Length > 0 && grouping.Expression.Path.TryGetFirstSecurityFilterCondition(ctx, out expressionPath, out expressionPath) != null)
				{
					Hierarchy.PathNode pathNode;
					if (dictionary.TryGetValue(grouping.Expression.Path[0], out pathNode))
					{
						pathNode.MergePath(grouping.Expression.Path, 0);
					}
					else
					{
						pathNode = new Hierarchy.PathNode(grouping.Expression.Path, 0);
						dictionary.Add(grouping.Expression.Path[0], pathNode);
					}
				}
			}
			Expression expression = this.m_baseEntity.TryGetSecurityFilterCondition(ctx);
			if (expression != null)
			{
				expression = expression.Clone();
			}
			if (dictionary.Count > 0)
			{
				foreach (Hierarchy.PathNode pathNode2 in dictionary.Values)
				{
					foreach (IList<PathItem> list in pathNode2.GetPaths())
					{
						ExpressionPath expressionPath2 = new ExpressionPath(list.Count);
						expressionPath2.AddRange(Hierarchy.PathNode.ReversePath(list));
						Expression expression2 = new Expression(new LiteralNode(true), expressionPath2);
						if (expression == null)
						{
							expression = expression2;
						}
						else
						{
							expression = new Expression(new FunctionNode(FunctionName.And, new Expression[] { expression, expression2 }));
						}
					}
				}
			}
			return expression;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00021F8C File Offset: 0x0002018C
		void ICompileable.Compile(CompilationContext ctx)
		{
			this.Compile(ctx);
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00021F95 File Offset: 0x00020195
		string IValidationScope.ObjectType
		{
			get
			{
				return "Hierarchy";
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00021F9C File Offset: 0x0002019C
		string IValidationScope.ObjectID
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00021FA3 File Offset: 0x000201A3
		string IValidationScope.ObjectName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000452 RID: 1106
		internal const string HierarchyElem = "Hierarchy";

		// Token: 0x04000453 RID: 1107
		private const string BaseEntityElem = "BaseEntity";

		// Token: 0x04000454 RID: 1108
		private const string EntityIdElem = "EntityID";

		// Token: 0x04000455 RID: 1109
		private const string GroupingsElem = "Groupings";

		// Token: 0x04000456 RID: 1110
		private const string FilterElem = "Filter";

		// Token: 0x04000457 RID: 1111
		private const string EntityIdProperty = "BaseEntity.EntityID";

		// Token: 0x04000458 RID: 1112
		private IQueryEntityInternal m_baseEntity;

		// Token: 0x04000459 RID: 1113
		private Expression m_filter;

		// Token: 0x0400045A RID: 1114
		private readonly Hierarchy.GroupingCollection m_groupings;

		// Token: 0x0400045B RID: 1115
		private SemanticQuery m_query;

		// Token: 0x020001AC RID: 428
		private class FilterLoader : ModelingXmlLoaderBase<Hierarchy>
		{
			// Token: 0x060010C5 RID: 4293 RVA: 0x00034B59 File Offset: 0x00032D59
			internal FilterLoader(Hierarchy item)
				: base(item)
			{
			}

			// Token: 0x060010C6 RID: 4294 RVA: 0x00034B64 File Offset: 0x00032D64
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Expression")
				{
					base.Item.Filter = new Expression();
					base.Item.Filter.Load(xr, false);
					return true;
				}
				return base.LoadXmlElement(xr);
			}
		}

		// Token: 0x020001AD RID: 429
		private sealed class PathNode
		{
			// Token: 0x060010C7 RID: 4295 RVA: 0x00034BB6 File Offset: 0x00032DB6
			internal PathNode(ExpressionPath path, int itemIndex)
			{
				if (itemIndex < 0 || itemIndex >= path.Length)
				{
					throw new InternalModelingException("itemIndex is out of range.");
				}
				this.m_pathItem = path[itemIndex];
				if (itemIndex + 1 < path.Length)
				{
					this.MergePath(path, itemIndex);
				}
			}

			// Token: 0x060010C8 RID: 4296 RVA: 0x00034BF8 File Offset: 0x00032DF8
			internal void MergePath(ExpressionPath path, int itemIndex)
			{
				if (itemIndex < 0 || itemIndex >= path.Length)
				{
					throw new InternalModelingException("itemIndex is out of range.");
				}
				if (!this.m_pathItem.Equals(path[itemIndex]))
				{
					throw new InternalModelingException("path is out of range.");
				}
				itemIndex++;
				if (itemIndex < path.Length)
				{
					if (this.m_children == null)
					{
						this.m_children = new Dictionary<PathItem, Hierarchy.PathNode>();
					}
					Hierarchy.PathNode pathNode;
					if (this.m_children.TryGetValue(path[itemIndex], out pathNode))
					{
						pathNode.MergePath(path, itemIndex);
						return;
					}
					pathNode = new Hierarchy.PathNode(path, itemIndex);
					this.m_children.Add(pathNode.m_pathItem, pathNode);
				}
			}

			// Token: 0x060010C9 RID: 4297 RVA: 0x00034C96 File Offset: 0x00032E96
			internal IEnumerable<IList<PathItem>> GetPaths()
			{
				if (this.m_children == null)
				{
					yield return new List<PathItem> { this.m_pathItem };
				}
				else
				{
					foreach (Hierarchy.PathNode pathNode in this.m_children.Values)
					{
						foreach (IList<PathItem> list in pathNode.GetPaths())
						{
							list.Add(this.m_pathItem);
							yield return list;
						}
						IEnumerator<IList<PathItem>> enumerator2 = null;
					}
					Dictionary<PathItem, Hierarchy.PathNode>.ValueCollection.Enumerator enumerator = default(Dictionary<PathItem, Hierarchy.PathNode>.ValueCollection.Enumerator);
				}
				yield break;
				yield break;
			}

			// Token: 0x060010CA RID: 4298 RVA: 0x00034CA6 File Offset: 0x00032EA6
			internal static IEnumerable<PathItem> ReversePath(IList<PathItem> pathToReverse)
			{
				int num;
				for (int i = pathToReverse.Count - 1; i >= 0; i = num)
				{
					yield return pathToReverse[i];
					num = i - 1;
				}
				yield break;
			}

			// Token: 0x04000704 RID: 1796
			private readonly PathItem m_pathItem;

			// Token: 0x04000705 RID: 1797
			private Dictionary<PathItem, Hierarchy.PathNode> m_children;

			// Token: 0x020001F7 RID: 503
			private class PathNodeEqualityComparer : IEqualityComparer<Hierarchy.PathNode>
			{
				// Token: 0x06001204 RID: 4612 RVA: 0x00037A61 File Offset: 0x00035C61
				private PathNodeEqualityComparer()
				{
				}

				// Token: 0x06001205 RID: 4613 RVA: 0x00037A69 File Offset: 0x00035C69
				public bool Equals(Hierarchy.PathNode x, Hierarchy.PathNode y)
				{
					if (x == null || y == null)
					{
						throw new InternalModelingException("TryGetSecurityFilterCondition.PathNodeEqualityComparer: At least one of path nodes is null.");
					}
					return x.m_pathItem.Equals(y.m_pathItem);
				}

				// Token: 0x06001206 RID: 4614 RVA: 0x00037A8D File Offset: 0x00035C8D
				public int GetHashCode(Hierarchy.PathNode obj)
				{
					if (obj == null)
					{
						throw new InternalModelingException("TryGetSecurityFilterCondition.PathNodeEqualityComparer: path node is null.");
					}
					return obj.m_pathItem.GetHashCode();
				}

				// Token: 0x04000869 RID: 2153
				internal static Hierarchy.PathNode.PathNodeEqualityComparer Instance = new Hierarchy.PathNode.PathNodeEqualityComparer();
			}
		}

		// Token: 0x020001AE RID: 430
		public sealed class GroupingCollection : OwnedCollection<Grouping, Hierarchy>, IXmlLoadable
		{
			// Token: 0x060010CB RID: 4299 RVA: 0x00034CB6 File Offset: 0x00032EB6
			internal GroupingCollection(Hierarchy owner)
				: base(owner)
			{
			}

			// Token: 0x170003ED RID: 1005
			public Grouping this[string name]
			{
				get
				{
					return base.Items.Find((Grouping g) => g.Name == name);
				}
			}

			// Token: 0x060010CD RID: 4301 RVA: 0x00034CF1 File Offset: 0x00032EF1
			internal void Load(ModelingXmlReader xr)
			{
				base.CheckWriteable();
				xr.LoadObject("Groupings", this);
			}

			// Token: 0x060010CE RID: 4302 RVA: 0x00034D05 File Offset: 0x00032F05
			bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
			{
				return false;
			}

			// Token: 0x060010CF RID: 4303 RVA: 0x00034D08 File Offset: 0x00032F08
			bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Grouping")
				{
					Grouping grouping = new Grouping();
					grouping.Load(xr);
					base.Add(grouping);
					return true;
				}
				return false;
			}

			// Token: 0x060010D0 RID: 4304 RVA: 0x00034D46 File Offset: 0x00032F46
			internal void WriteTo(ModelingXmlWriter xw)
			{
				xw.WriteCollectionElement<Grouping>("Groupings", this);
			}

			// Token: 0x060010D1 RID: 4305 RVA: 0x00034D54 File Offset: 0x00032F54
			internal void Compile(CompilationContext ctx)
			{
				CheckedCollection<Grouping>.CompileItems<Grouping>(this, ctx);
			}
		}
	}
}
