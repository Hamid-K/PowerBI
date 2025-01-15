using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000128 RID: 296
	internal class DaxExpression
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0002CBFE File Offset: 0x0002ADFE
		internal static DaxExpression Null
		{
			get
			{
				return DaxExpression.NullInstance;
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0002CC05 File Offset: 0x0002AE05
		internal static DaxExpression Definition(string text, IReadOnlyList<DaxResultColumn> definedColumns)
		{
			ArgumentValidation.CheckNotNullOrEmpty<DaxResultColumn>(definedColumns, "definedColumns");
			return new DaxExpression(text, definedColumns);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002CC1A File Offset: 0x0002AE1A
		internal static DaxExpression Scalar(string text)
		{
			return new DaxExpression(text, Util.EmptyArray<DaxResultColumn>());
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002CC27 File Offset: 0x0002AE27
		internal static DaxExpression ScalarOrTable(string text, IReadOnlyList<DaxResultColumn> resultColumns)
		{
			return new DaxExpression(text, resultColumns);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002CC30 File Offset: 0x0002AE30
		internal static DaxSortedExpression SortedTable(string text, IReadOnlyList<DaxResultColumn> resultColumns, IReadOnlyList<DaxSortItem> sortItems)
		{
			return new DaxSortedExpression(text, resultColumns, sortItems);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002CC3A File Offset: 0x0002AE3A
		internal static DaxExpression Table(string text, IReadOnlyList<DaxResultColumn> resultColumns, bool isTableScan = false)
		{
			if (isTableScan)
			{
				ArgumentValidation.CheckNotNull<IReadOnlyList<DaxResultColumn>>(resultColumns, "resultColumns");
			}
			else
			{
				ArgumentValidation.CheckNotNullOrEmpty<DaxResultColumn>(resultColumns, "resultColumns");
			}
			return new DaxExpression(text, resultColumns);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0002CC60 File Offset: 0x0002AE60
		internal static DaxExpression Table(string text, DaxResultColumn resultColumn)
		{
			return new DaxExpression(text, new DaxResultColumn[] { resultColumn });
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002CC76 File Offset: 0x0002AE76
		internal static DaxExpression Tuple(string text)
		{
			return new DaxExpression(text, Util.EmptyArray<DaxResultColumn>());
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002CC83 File Offset: 0x0002AE83
		internal static DaxExpression Void(string text)
		{
			return new DaxExpression(text, Util.EmptyArray<DaxResultColumn>());
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002CC90 File Offset: 0x0002AE90
		protected DaxExpression(string text, IReadOnlyList<DaxResultColumn> resultColumns)
		{
			this.Text = text;
			this.ResultColumns = resultColumns;
			DaxValidation.CheckCondition(resultColumns.Select((DaxResultColumn c) => c.ToResultColumnName()).Distinct(DaxRef.NameComparer).Count<string>() == resultColumns.Count, "DaxExpression specifies duplicate columns. Table expression columns must be unique.");
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002CCF7 File Offset: 0x0002AEF7
		private DaxExpression()
		{
			this.Text = string.Empty;
			this.ResultColumns = Util.EmptyArray<DaxResultColumn>();
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0002CD15 File Offset: 0x0002AF15
		public string Text { get; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0002CD1D File Offset: 0x0002AF1D
		public IReadOnlyList<DaxResultColumn> ResultColumns { get; }

		// Token: 0x06001066 RID: 4198 RVA: 0x0002CD25 File Offset: 0x0002AF25
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0002CD2D File Offset: 0x0002AF2D
		internal DaxColumnRef GetResultColumnReference(ConceptualTypeColumn columnType)
		{
			return this.GetResultColumnReference(columnType.EdmName);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0002CD3C File Offset: 0x0002AF3C
		internal DaxColumnRef GetResultColumnReference(string queryFieldName)
		{
			return this.ResultColumns.Single((DaxResultColumn r) => r.QueryFieldName == queryFieldName).DaxColumnRef;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0002CD78 File Offset: 0x0002AF78
		internal IReadOnlyList<TranslationResultField> ToResultFields(IReadOnlyList<ConceptualTypeColumn> fields)
		{
			List<TranslationResultField> list = new List<TranslationResultField>(this.ResultColumns.Count);
			foreach (DaxResultColumn daxResultColumn in this.ResultColumns)
			{
				foreach (ConceptualTypeColumn conceptualTypeColumn in fields)
				{
					if (daxResultColumn.QueryFieldName == conceptualTypeColumn.EdmName)
					{
						QueryResultFieldSortInformation sortInfo = this.GetSortInfo(daxResultColumn);
						TranslationResultField translationResultField = new TranslationResultField(conceptualTypeColumn, daxResultColumn.ToResultColumnName(), daxResultColumn.DaxColumnRef.ColumnName, sortInfo);
						list.Add(translationResultField);
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0002CE4C File Offset: 0x0002B04C
		protected virtual QueryResultFieldSortInformation GetSortInfo(DaxResultColumn resultColumn)
		{
			return null;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002CE4F File Offset: 0x0002B04F
		internal virtual DaxExpression ReplaceText(string newText)
		{
			return new DaxExpression(newText, this.ResultColumns);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0002CE5D File Offset: 0x0002B05D
		internal virtual DaxExpression ReplaceResultColumns(IReadOnlyList<DaxResultColumn> newResultColumns)
		{
			return new DaxExpression(this.Text, newResultColumns);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0002CE6C File Offset: 0x0002B06C
		internal DaxExpression EnsureUnqualifiedColumns()
		{
			HashSet<string> hashSet = new HashSet<string>(DaxRef.NameComparer);
			List<DaxResultColumn> list = null;
			int i = 0;
			int count = this.ResultColumns.Count;
			while (i < count)
			{
				DaxResultColumn daxResultColumn = this.ResultColumns[i];
				if (!hashSet.Add(daxResultColumn.DaxColumnRef.ColumnName))
				{
					throw new DaxTranslationException(DevErrors.DaxTranslation.DuplicateUnqualifiedColumnName(daxResultColumn.DaxColumnRef.ColumnName), CommandTreeTranslationErrorCode.DuplicateUnqualifiedColumnName);
				}
				if (!daxResultColumn.DaxColumnRef.TableRef.TableName.IsNullOrEmpty<char>())
				{
					if (list == null)
					{
						list = new List<DaxResultColumn>(this.ResultColumns);
					}
					list[i] = daxResultColumn.ToUnqualifiedColumn();
				}
				i++;
			}
			if (list == null)
			{
				return this;
			}
			return this.ReplaceResultColumns(list);
		}

		// Token: 0x04000A80 RID: 2688
		private static readonly DaxExpression NullInstance = new DaxExpression();
	}
}
