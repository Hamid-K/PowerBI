using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000652 RID: 1618
	internal abstract class GroupAggregateInfo
	{
		// Token: 0x06004DDD RID: 19933 RVA: 0x001183A9 File Offset: 0x001165A9
		protected GroupAggregateInfo(GroupAggregateKind aggregateKind, GroupAggregateExpr astNode, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion)
		{
			this.AggregateKind = aggregateKind;
			this.AstNode = astNode;
			this.ErrCtx = errCtx;
			this.DefiningScopeRegion = definingScopeRegion;
			this.SetContainingAggregate(containingAggregate);
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x001183D6 File Offset: 0x001165D6
		protected void AttachToAstNode(string aggregateName, TypeUsage resultType)
		{
			this.AggregateName = aggregateName;
			this.AggregateStubExpression = resultType.Null();
			this.AstNode.AggregateInfo = this;
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x001183F7 File Offset: 0x001165F7
		internal void DetachFromAstNode()
		{
			this.AstNode.AggregateInfo = null;
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x00118408 File Offset: 0x00116608
		internal void UpdateScopeIndex(int referencedScopeIndex, SemanticResolver sr)
		{
			ScopeRegion definingScopeRegion = sr.GetDefiningScopeRegion(referencedScopeIndex);
			if (this._innermostReferencedScopeRegion == null || this._innermostReferencedScopeRegion.ScopeRegionIndex < definingScopeRegion.ScopeRegionIndex)
			{
				this._innermostReferencedScopeRegion = definingScopeRegion;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06004DE1 RID: 19937 RVA: 0x0011843F File Offset: 0x0011663F
		// (set) Token: 0x06004DE2 RID: 19938 RVA: 0x00118447 File Offset: 0x00116647
		internal ScopeRegion InnermostReferencedScopeRegion
		{
			get
			{
				return this._innermostReferencedScopeRegion;
			}
			set
			{
				this._innermostReferencedScopeRegion = value;
			}
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x00118450 File Offset: 0x00116650
		internal void ValidateAndComputeEvaluatingScopeRegion(SemanticResolver sr)
		{
			this._evaluatingScopeRegion = this._innermostReferencedScopeRegion ?? this.DefiningScopeRegion;
			if (!this._evaluatingScopeRegion.IsAggregating)
			{
				int scopeRegionIndex = this._evaluatingScopeRegion.ScopeRegionIndex;
				this._evaluatingScopeRegion = null;
				foreach (ScopeRegion scopeRegion in sr.ScopeRegions.Skip(scopeRegionIndex))
				{
					if (scopeRegion.IsAggregating)
					{
						this._evaluatingScopeRegion = scopeRegion;
						break;
					}
				}
				if (this._evaluatingScopeRegion == null)
				{
					throw new EntitySqlException(Strings.GroupVarNotFoundInScope);
				}
			}
			this.ValidateContainedAggregates(this._evaluatingScopeRegion.ScopeRegionIndex, this.DefiningScopeRegion.ScopeRegionIndex);
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00118514 File Offset: 0x00116714
		private void ValidateContainedAggregates(int outerBoundaryScopeRegionIndex, int innerBoundaryScopeRegionIndex)
		{
			if (this._containedAggregates != null)
			{
				foreach (GroupAggregateInfo groupAggregateInfo in this._containedAggregates)
				{
					if (groupAggregateInfo.EvaluatingScopeRegion.ScopeRegionIndex >= outerBoundaryScopeRegionIndex && groupAggregateInfo.EvaluatingScopeRegion.ScopeRegionIndex <= innerBoundaryScopeRegionIndex)
					{
						int num;
						int num2;
						string text = EntitySqlException.FormatErrorContext(this.ErrCtx.CommandText, this.ErrCtx.InputPosition, this.ErrCtx.ErrorContextInfo, this.ErrCtx.UseContextInfoAsResourceIdentifier, out num, out num2);
						throw new EntitySqlException(Strings.NestedAggregateCannotBeUsedInAggregate(EntitySqlException.FormatErrorContext(groupAggregateInfo.ErrCtx.CommandText, groupAggregateInfo.ErrCtx.InputPosition, groupAggregateInfo.ErrCtx.ErrorContextInfo, groupAggregateInfo.ErrCtx.UseContextInfoAsResourceIdentifier, out num, out num2), text));
					}
					groupAggregateInfo.ValidateContainedAggregates(outerBoundaryScopeRegionIndex, innerBoundaryScopeRegionIndex);
				}
			}
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x00118614 File Offset: 0x00116814
		internal void SetContainingAggregate(GroupAggregateInfo containingAggregate)
		{
			if (this._containingAggregate != null)
			{
				this._containingAggregate.RemoveContainedAggregate(this);
			}
			this._containingAggregate = containingAggregate;
			if (this._containingAggregate != null)
			{
				this._containingAggregate.AddContainedAggregate(this);
			}
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x00118645 File Offset: 0x00116845
		private void AddContainedAggregate(GroupAggregateInfo containedAggregate)
		{
			if (this._containedAggregates == null)
			{
				this._containedAggregates = new List<GroupAggregateInfo>();
			}
			this._containedAggregates.Add(containedAggregate);
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00118666 File Offset: 0x00116866
		private void RemoveContainedAggregate(GroupAggregateInfo containedAggregate)
		{
			this._containedAggregates.Remove(containedAggregate);
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x00118675 File Offset: 0x00116875
		internal ScopeRegion EvaluatingScopeRegion
		{
			get
			{
				return this._evaluatingScopeRegion;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06004DE9 RID: 19945 RVA: 0x0011867D File Offset: 0x0011687D
		internal GroupAggregateInfo ContainingAggregate
		{
			get
			{
				return this._containingAggregate;
			}
		}

		// Token: 0x04001C30 RID: 7216
		private ScopeRegion _innermostReferencedScopeRegion;

		// Token: 0x04001C31 RID: 7217
		private List<GroupAggregateInfo> _containedAggregates;

		// Token: 0x04001C32 RID: 7218
		internal readonly GroupAggregateKind AggregateKind;

		// Token: 0x04001C33 RID: 7219
		internal readonly GroupAggregateExpr AstNode;

		// Token: 0x04001C34 RID: 7220
		internal readonly ErrorContext ErrCtx;

		// Token: 0x04001C35 RID: 7221
		internal readonly ScopeRegion DefiningScopeRegion;

		// Token: 0x04001C36 RID: 7222
		private ScopeRegion _evaluatingScopeRegion;

		// Token: 0x04001C37 RID: 7223
		private GroupAggregateInfo _containingAggregate;

		// Token: 0x04001C38 RID: 7224
		internal string AggregateName;

		// Token: 0x04001C39 RID: 7225
		internal DbNullExpression AggregateStubExpression;
	}
}
