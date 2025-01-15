using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000162 RID: 354
	internal sealed class QueryAllExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600142F RID: 5167 RVA: 0x0003A81E File Offset: 0x00038A1E
		internal QueryAllExpression(QueryAllKind allKind, ConceptualResultType conceptualResultType)
			: this(allKind, conceptualResultType, null, null, null, null)
		{
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0003A82C File Offset: 0x00038A2C
		internal QueryAllExpression(QueryAllKind allKind, ConceptualResultType conceptualResultType, EntitySet target, IConceptualEntity targetEntity = null)
			: this(allKind, conceptualResultType, target, null, targetEntity, null)
		{
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0003A83B File Offset: 0x00038A3B
		internal QueryAllExpression(QueryAllKind allKind, ConceptualResultType conceptualResultType, EntitySet target, EdmField field, IConceptualEntity targetEntity = null, IConceptualColumn column = null)
			: this(allKind, conceptualResultType, target, field.AsReadOnlyList<EdmField>(), targetEntity, (column != null) ? column.AsReadOnlyList<IConceptualColumn>() : null)
		{
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0003A85D File Offset: 0x00038A5D
		internal QueryAllExpression(QueryAllKind allKind, ConceptualResultType conceptualResultType, EntitySet target, IReadOnlyList<EdmField> fields, IConceptualEntity targetEntity = null, IReadOnlyList<IConceptualColumn> columns = null)
			: base(conceptualResultType)
		{
			this._allKind = allKind;
			this._target = target;
			this._targetEntity = targetEntity;
			this._fields = fields;
			this._columns = columns;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0003A88C File Offset: 0x00038A8C
		public QueryAllKind AllKind
		{
			get
			{
				return this._allKind;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0003A894 File Offset: 0x00038A94
		public EntitySet Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0003A89C File Offset: 0x00038A9C
		public IConceptualEntity TargetEntity
		{
			get
			{
				return this._targetEntity;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0003A8A4 File Offset: 0x00038AA4
		public IReadOnlyList<EdmField> Fields
		{
			get
			{
				return this._fields;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0003A8AC File Offset: 0x00038AAC
		public IReadOnlyList<IConceptualColumn> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003A8C8 File Offset: 0x00038AC8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryAllExpression queryAllExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryAllExpression>(this, other, out flag, out queryAllExpression))
			{
				return flag;
			}
			return this.AllKind == queryAllExpression.AllKind && object.Equals(this.Target, queryAllExpression.Target) && ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(this.TargetEntity, queryAllExpression.TargetEntity) && ((this.Fields == null && queryAllExpression.Fields == null) || (this.Fields != null && queryAllExpression.Fields != null && this.Fields.SetEquals(queryAllExpression.Fields))) && ((this.Columns == null && queryAllExpression.Columns == null) || (this.Columns != null && queryAllExpression.Columns != null && this.Columns.SetEquals(queryAllExpression.Columns)));
		}

		// Token: 0x04000B08 RID: 2824
		internal static ConceptualTableType NoArgAllResultType = ConceptualTableType.Empty;

		// Token: 0x04000B09 RID: 2825
		private readonly QueryAllKind _allKind;

		// Token: 0x04000B0A RID: 2826
		private readonly EntitySet _target;

		// Token: 0x04000B0B RID: 2827
		private readonly IConceptualEntity _targetEntity;

		// Token: 0x04000B0C RID: 2828
		private readonly IReadOnlyList<EdmField> _fields;

		// Token: 0x04000B0D RID: 2829
		private readonly IReadOnlyList<IConceptualColumn> _columns;
	}
}
