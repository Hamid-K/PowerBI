using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200021E RID: 542
	public class ResolvedQueryDefinitionBuilder<TParent> : IResolvedQueryReferenceProvider
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x0001DC01 File Offset: 0x0001BE01
		internal ResolvedQueryDefinitionBuilder(TParent parent, Action<ResolvedQueryDefinition> addToParent = null, string name = null)
			: this(parent, addToParent, new ResolvedQueryReferenceContext(), name)
		{
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0001DC14 File Offset: 0x0001BE14
		internal ResolvedQueryDefinitionBuilder(TParent parent, Action<ResolvedQueryDefinition> addToParent, ResolvedQueryReferenceContext referenceContext, string name = null)
		{
			this._parent = parent;
			this._addToParent = addToParent;
			this._referenceContext = referenceContext;
			this._referenceContext.Push(this);
			this._parameters = new List<ResolvedQueryParameterDeclaration>();
			this._let = new List<ResolvedQueryLetBinding>();
			this._from = new List<ResolvedQuerySource>();
			this._where = new List<ResolvedQueryFilter>();
			this._transform = new List<ResolvedQueryTransform>();
			this._orderBy = new List<ResolvedQuerySortClause>();
			this._select = new List<ResolvedQuerySelect>();
			this._visualShape = new List<ResolvedQueryAxis>();
			this._groupBy = new List<ResolvedQueryExpression>();
			this._name = name;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0001DCB3 File Offset: 0x0001BEB3
		public TParent Parent
		{
			get
			{
				Action<ResolvedQueryDefinition> addToParent = this._addToParent;
				if (addToParent != null)
				{
					addToParent(this.Build());
				}
				this._referenceContext.Pop(this);
				return this._parent;
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0001DCDE File Offset: 0x0001BEDE
		public ResolvedQueryParameterRefExpression ParameterRef(string name)
		{
			return this._referenceContext.ParameterRef(name);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		public ResolvedQueryLetRefExpression LetRef(string name)
		{
			return this._referenceContext.LetRef(name);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0001DCFA File Offset: 0x0001BEFA
		public ResolvedQueryExpression SourceRef(string name)
		{
			return this._referenceContext.SourceRef(name);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0001DD08 File Offset: 0x0001BF08
		bool IResolvedQueryReferenceProvider.TryParameterRef(string name, out ResolvedQueryParameterRefExpression parameterRef)
		{
			ResolvedQueryParameterDeclaration resolvedQueryParameterDeclaration = this._parameters.FirstOrDefault((ResolvedQueryParameterDeclaration b) => QueryNameComparer.Instance.Equals(b.Name, name));
			if (resolvedQueryParameterDeclaration == null)
			{
				parameterRef = null;
				return false;
			}
			parameterRef = resolvedQueryParameterDeclaration.ParameterRef();
			return true;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		bool IResolvedQueryReferenceProvider.TryLetRef(string name, out ResolvedQueryLetRefExpression letRef)
		{
			ResolvedQueryLetBinding resolvedQueryLetBinding = this._let.FirstOrDefault((ResolvedQueryLetBinding b) => QueryNameComparer.Instance.Equals(b.Name, name));
			if (resolvedQueryLetBinding == null)
			{
				letRef = null;
				return false;
			}
			letRef = resolvedQueryLetBinding.LetRef();
			return true;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0001DD90 File Offset: 0x0001BF90
		bool IResolvedQueryReferenceProvider.TrySourceRef(string name, out ResolvedQueryExpression sourceRef)
		{
			ResolvedQuerySource resolvedQuerySource = this._from.FirstOrDefault((ResolvedQuerySource s) => QueryNameComparer.Instance.Equals(s.Name, name));
			if (resolvedQuerySource == null)
			{
				sourceRef = null;
				return false;
			}
			ResolvedEntitySource resolvedEntitySource = resolvedQuerySource as ResolvedEntitySource;
			if (resolvedEntitySource != null)
			{
				sourceRef = resolvedEntitySource.SourceRef();
			}
			else
			{
				ResolvedExpressionSource resolvedExpressionSource = resolvedQuerySource as ResolvedExpressionSource;
				if (resolvedExpressionSource == null)
				{
					throw new InvalidOperationException("Unexpected source type");
				}
				sourceRef = resolvedExpressionSource.ExpressionSourceRef();
			}
			return true;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0001DDFE File Offset: 0x0001BFFE
		public ResolvedQueryDefinitionBuilder<TParent> WithName(string name)
		{
			this._name = name;
			return this;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0001DE08 File Offset: 0x0001C008
		public ResolvedQueryDefinitionBuilder<TParent> WithParameter(string name, ResolvedQueryExpression typeExpression)
		{
			ResolvedQueryParameterDeclaration resolvedQueryParameterDeclaration = new ResolvedQueryParameterDeclaration(name, typeExpression);
			this._parameters.Add(resolvedQueryParameterDeclaration);
			return this;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0001DE2A File Offset: 0x0001C02A
		public ResolvedQueryDefinitionBuilder<TParent> WithLet(string name, ResolvedQueryExpression expression)
		{
			return this.WithLet(expression.LetBinding(name));
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0001DE39 File Offset: 0x0001C039
		public ResolvedQueryLetBindingBuilder<ResolvedQueryDefinitionBuilder<TParent>> WithLet(string name)
		{
			return new ResolvedQueryLetBindingBuilder<ResolvedQueryDefinitionBuilder<TParent>>(this, delegate(ResolvedQueryLetBinding binding)
			{
				this.WithLet(binding);
			}, this._referenceContext, name);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0001DE54 File Offset: 0x0001C054
		public ResolvedQueryDefinitionBuilder<TParent> WithLet(ResolvedQueryLetBinding let)
		{
			this._let.Add(let);
			return this;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0001DE63 File Offset: 0x0001C063
		public ResolvedQuerySourceBuilder<ResolvedQueryDefinitionBuilder<TParent>> WithFrom(string name)
		{
			return new ResolvedQuerySourceBuilder<ResolvedQueryDefinitionBuilder<TParent>>(this, delegate(ResolvedQuerySource source)
			{
				this.WithFrom(source);
			}, this._referenceContext, name);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0001DE7E File Offset: 0x0001C07E
		public ResolvedQueryDefinitionBuilder<TParent> WithFrom(string name, ResolvedQueryExpression expression)
		{
			return this.WithFrom(expression.ExpressionSource(name));
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0001DE8D File Offset: 0x0001C08D
		public ResolvedQueryDefinitionBuilder<TParent> WithFrom(string name, IConceptualEntity entity)
		{
			return this.WithFrom(entity.EntitySource(name, null));
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0001DE9D File Offset: 0x0001C09D
		public ResolvedQueryDefinitionBuilder<TParent> WithFrom(ResolvedQuerySource source)
		{
			this._from.Add(source);
			return this;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		public ResolvedQueryDefinitionBuilder<TParent> WithWhere(ResolvedQueryExpression condition)
		{
			return this.WithWhere(condition.Filter(Util.EmptyReadOnlyCollection<ResolvedQueryExpression>(), null));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0001DEC0 File Offset: 0x0001C0C0
		public ResolvedQueryDefinitionBuilder<TParent> WithWhere(ResolvedQueryFilter filter)
		{
			this._where.Add(filter);
			return this;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0001DECF File Offset: 0x0001C0CF
		public ResolvedQueryTransformBuilder<ResolvedQueryDefinitionBuilder<TParent>> WithTransform(string name, string algorithm)
		{
			return new ResolvedQueryTransformBuilder<ResolvedQueryDefinitionBuilder<TParent>>(this, delegate(ResolvedQueryTransform transform)
			{
				this.WithTransform(transform);
			}, name, algorithm);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0001DEE5 File Offset: 0x0001C0E5
		public ResolvedQueryDefinitionBuilder<TParent> WithTransform(ResolvedQueryTransform transform)
		{
			this._transform.Add(transform);
			return this;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		public ResolvedQueryDefinitionBuilder<TParent> WithOrderBy(ResolvedQuerySortClause sort)
		{
			this._orderBy.Add(sort);
			return this;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0001DF03 File Offset: 0x0001C103
		public ResolvedQueryDefinitionBuilder<TParent> WithSelect(ResolvedQuerySelect select)
		{
			this._select.Add(select);
			return this;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0001DF14 File Offset: 0x0001C114
		public ResolvedQueryDefinitionBuilder<TParent> WithSelect(Func<ResolvedQueryDefinitionBuilder<TParent>, ResolvedQueryExpression> createExpression, string name = null, string nativeReferenceName = null)
		{
			ResolvedQueryExpression resolvedQueryExpression = createExpression(this);
			return this.WithSelect(resolvedQueryExpression, name, nativeReferenceName);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0001DF32 File Offset: 0x0001C132
		public ResolvedQueryDefinitionBuilder<TParent> WithSelect(ResolvedQueryExpression expression, string name = null, string nativeReferenceName = null)
		{
			this.WithSelect(new ResolvedQuerySelect(expression, name, nativeReferenceName));
			return this;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0001DF44 File Offset: 0x0001C144
		public ResolvedQueryVisualShapeAxisBuilder<ResolvedQueryDefinitionBuilder<TParent>> WithVisualShapeAxis(string axisName)
		{
			return new ResolvedQueryVisualShapeAxisBuilder<ResolvedQueryDefinitionBuilder<TParent>>(this, delegate(ResolvedQueryAxis axis)
			{
				this._visualShape.Add(axis);
			}, axisName);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0001DF59 File Offset: 0x0001C159
		public ResolvedQueryDefinitionBuilder<TParent> WithGroupBy(ResolvedQueryExpression groupBy)
		{
			this._groupBy.Add(groupBy);
			return this;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0001DF68 File Offset: 0x0001C168
		public ResolvedQueryDefinitionBuilder<TParent> WithTop(int top)
		{
			this._top = new int?(top);
			return this;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0001DF77 File Offset: 0x0001C177
		public ResolvedQueryDefinitionBuilder<TParent> WithSkip(int skip)
		{
			this._skip = new long?((long)skip);
			return this;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0001DF88 File Offset: 0x0001C188
		public ResolvedQueryDefinition Build()
		{
			return new ResolvedQueryDefinition(this._parameters, this._let, this._from, this._where, this._transform, this._orderBy, this._select, this._visualShape, this._groupBy, this._top, this._skip, this._name);
		}

		// Token: 0x0400073E RID: 1854
		private readonly TParent _parent;

		// Token: 0x0400073F RID: 1855
		private readonly Action<ResolvedQueryDefinition> _addToParent;

		// Token: 0x04000740 RID: 1856
		private readonly ResolvedQueryReferenceContext _referenceContext;

		// Token: 0x04000741 RID: 1857
		private readonly List<ResolvedQueryParameterDeclaration> _parameters;

		// Token: 0x04000742 RID: 1858
		private readonly List<ResolvedQueryLetBinding> _let;

		// Token: 0x04000743 RID: 1859
		private readonly List<ResolvedQuerySource> _from;

		// Token: 0x04000744 RID: 1860
		private readonly List<ResolvedQueryFilter> _where;

		// Token: 0x04000745 RID: 1861
		private readonly List<ResolvedQueryTransform> _transform;

		// Token: 0x04000746 RID: 1862
		private readonly List<ResolvedQuerySortClause> _orderBy;

		// Token: 0x04000747 RID: 1863
		private readonly List<ResolvedQuerySelect> _select;

		// Token: 0x04000748 RID: 1864
		private readonly List<ResolvedQueryAxis> _visualShape;

		// Token: 0x04000749 RID: 1865
		private readonly List<ResolvedQueryExpression> _groupBy;

		// Token: 0x0400074A RID: 1866
		private int? _top;

		// Token: 0x0400074B RID: 1867
		private long? _skip;

		// Token: 0x0400074C RID: 1868
		private string _name;
	}
}
