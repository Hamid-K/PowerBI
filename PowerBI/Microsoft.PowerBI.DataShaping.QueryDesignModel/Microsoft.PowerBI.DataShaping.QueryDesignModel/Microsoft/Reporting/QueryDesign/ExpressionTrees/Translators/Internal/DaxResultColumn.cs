using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000142 RID: 322
	internal readonly struct DaxResultColumn : IDaxGroupItem
	{
		// Token: 0x06001187 RID: 4487 RVA: 0x00030DBF File Offset: 0x0002EFBF
		internal static DaxResultColumn FromColumn(ConceptualTypeColumn column, EntitySet entitySet, IConceptualEntity entity = null)
		{
			return new DaxResultColumn(column.EdmName, DaxRef.Column(column, entitySet, entity));
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		internal DaxResultColumn(string queryFieldName, DaxColumnRef columnRef)
		{
			this.QueryFieldName = queryFieldName;
			this.DaxColumnRef = columnRef;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00030DE4 File Offset: 0x0002EFE4
		public string QueryFieldName { get; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00030DEC File Offset: 0x0002EFEC
		public DaxColumnRef DaxColumnRef { get; }

		// Token: 0x0600118B RID: 4491 RVA: 0x00030DF4 File Offset: 0x0002EFF4
		public DaxResultColumn ToUnqualifiedColumn()
		{
			return new DaxResultColumn(this.QueryFieldName, this.DaxColumnRef.ToUnqualifiedReference());
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00030E1C File Offset: 0x0002F01C
		public string ToResultColumnName()
		{
			return this.DaxColumnRef.TableName + ("[" + DaxRef.Column(this.DaxColumnRef.ColumnName).ColumnName + "]");
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00030E68 File Offset: 0x0002F068
		public string ToIntermediateResultColumnName()
		{
			if (string.IsNullOrEmpty(this.DaxColumnRef.TableName))
			{
				return this.DaxColumnRef.ColumnName;
			}
			return this.DaxColumnRef.ToString();
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00030EB0 File Offset: 0x0002F0B0
		DaxExpression IDaxGroupItem.ToExpression()
		{
			return DaxExpression.Table(this.DaxColumnRef.ToString(), this);
		}
	}
}
