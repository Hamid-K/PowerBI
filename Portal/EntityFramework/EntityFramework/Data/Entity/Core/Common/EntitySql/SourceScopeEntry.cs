using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200066B RID: 1643
	internal sealed class SourceScopeEntry : ScopeEntry, IGroupExpressionExtendedInfo, IGetAlternativeName
	{
		// Token: 0x06004EBE RID: 20158 RVA: 0x0011EC99 File Offset: 0x0011CE99
		internal SourceScopeEntry(DbVariableReferenceExpression varRef)
			: this(varRef, null)
		{
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x0011ECA3 File Offset: 0x0011CEA3
		internal SourceScopeEntry(DbVariableReferenceExpression varRef, string[] alternativeName)
			: base(ScopeEntryKind.SourceVar)
		{
			this._varBasedExpression = varRef;
			this._alternativeName = alternativeName;
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x0011ECBA File Offset: 0x0011CEBA
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varBasedExpression;
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x0011ECC2 File Offset: 0x0011CEC2
		DbExpression IGroupExpressionExtendedInfo.GroupVarBasedExpression
		{
			get
			{
				return this._groupVarBasedExpression;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x0011ECCA File Offset: 0x0011CECA
		DbExpression IGroupExpressionExtendedInfo.GroupAggBasedExpression
		{
			get
			{
				return this._groupAggBasedExpression;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x0011ECD2 File Offset: 0x0011CED2
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x0011ECDA File Offset: 0x0011CEDA
		internal bool IsJoinClauseLeftExpr { get; set; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x0011ECE3 File Offset: 0x0011CEE3
		string[] IGetAlternativeName.AlternativeName
		{
			get
			{
				return this._alternativeName;
			}
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x0011ECEC File Offset: 0x0011CEEC
		internal SourceScopeEntry AddParentVar(DbVariableReferenceExpression parentVarRef)
		{
			if (this._propRefs == null)
			{
				this._propRefs = new List<string>(2);
				this._propRefs.Add(((DbVariableReferenceExpression)this._varBasedExpression).VariableName);
			}
			this._varBasedExpression = parentVarRef;
			for (int i = this._propRefs.Count - 1; i >= 0; i--)
			{
				this._varBasedExpression = this._varBasedExpression.Property(this._propRefs[i]);
			}
			this._propRefs.Add(parentVarRef.VariableName);
			return this;
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x0011ED76 File Offset: 0x0011CF76
		internal void ReplaceParentVar(DbVariableReferenceExpression parentVarRef)
		{
			if (this._propRefs == null)
			{
				this._varBasedExpression = parentVarRef;
				return;
			}
			this._propRefs.RemoveAt(this._propRefs.Count - 1);
			this.AddParentVar(parentVarRef);
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x0011EDA8 File Offset: 0x0011CFA8
		internal void AdjustToGroupVar(DbVariableReferenceExpression parentVarRef, DbVariableReferenceExpression parentGroupVarRef, DbVariableReferenceExpression groupAggRef)
		{
			this.ReplaceParentVar(parentVarRef);
			this._groupVarBasedExpression = parentGroupVarRef;
			this._groupAggBasedExpression = groupAggRef;
			if (this._propRefs != null)
			{
				for (int i = this._propRefs.Count - 2; i >= 0; i--)
				{
					this._groupVarBasedExpression = this._groupVarBasedExpression.Property(this._propRefs[i]);
					this._groupAggBasedExpression = this._groupAggBasedExpression.Property(this._propRefs[i]);
				}
			}
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x0011EE24 File Offset: 0x0011D024
		internal void RollbackAdjustmentToGroupVar(DbVariableReferenceExpression pregroupParentVarRef)
		{
			this._groupVarBasedExpression = null;
			this._groupAggBasedExpression = null;
			this.ReplaceParentVar(pregroupParentVarRef);
		}

		// Token: 0x04001C75 RID: 7285
		private readonly string[] _alternativeName;

		// Token: 0x04001C76 RID: 7286
		private List<string> _propRefs;

		// Token: 0x04001C77 RID: 7287
		private DbExpression _varBasedExpression;

		// Token: 0x04001C78 RID: 7288
		private DbExpression _groupVarBasedExpression;

		// Token: 0x04001C79 RID: 7289
		private DbExpression _groupAggBasedExpression;
	}
}
