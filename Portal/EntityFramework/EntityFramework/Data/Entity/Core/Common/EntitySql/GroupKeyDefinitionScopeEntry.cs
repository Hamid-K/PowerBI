using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000655 RID: 1621
	internal sealed class GroupKeyDefinitionScopeEntry : ScopeEntry, IGroupExpressionExtendedInfo, IGetAlternativeName
	{
		// Token: 0x06004DEB RID: 19947 RVA: 0x00118693 File Offset: 0x00116893
		internal GroupKeyDefinitionScopeEntry(DbExpression varBasedExpression, DbExpression groupVarBasedExpression, DbExpression groupAggBasedExpression, string[] alternativeName)
			: base(ScopeEntryKind.GroupKeyDefinition)
		{
			this._varBasedExpression = varBasedExpression;
			this._groupVarBasedExpression = groupVarBasedExpression;
			this._groupAggBasedExpression = groupAggBasedExpression;
			this._alternativeName = alternativeName;
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x001186B9 File Offset: 0x001168B9
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varBasedExpression;
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06004DED RID: 19949 RVA: 0x001186C1 File Offset: 0x001168C1
		DbExpression IGroupExpressionExtendedInfo.GroupVarBasedExpression
		{
			get
			{
				return this._groupVarBasedExpression;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06004DEE RID: 19950 RVA: 0x001186C9 File Offset: 0x001168C9
		DbExpression IGroupExpressionExtendedInfo.GroupAggBasedExpression
		{
			get
			{
				return this._groupAggBasedExpression;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06004DEF RID: 19951 RVA: 0x001186D1 File Offset: 0x001168D1
		string[] IGetAlternativeName.AlternativeName
		{
			get
			{
				return this._alternativeName;
			}
		}

		// Token: 0x04001C3F RID: 7231
		private readonly DbExpression _varBasedExpression;

		// Token: 0x04001C40 RID: 7232
		private readonly DbExpression _groupVarBasedExpression;

		// Token: 0x04001C41 RID: 7233
		private readonly DbExpression _groupAggBasedExpression;

		// Token: 0x04001C42 RID: 7234
		private readonly string[] _alternativeName;
	}
}
