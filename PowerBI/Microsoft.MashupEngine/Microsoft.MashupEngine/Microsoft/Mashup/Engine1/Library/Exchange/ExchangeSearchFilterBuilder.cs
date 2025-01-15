using System;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BEB RID: 3051
	internal class ExchangeSearchFilterBuilder
	{
		// Token: 0x06005313 RID: 21267 RVA: 0x00119142 File Offset: 0x00117342
		public ExchangeSearchFilterBuilder(ExchangeColumnInfo[] columnInfos, QueryExpression expression)
		{
			this.columnInfos = columnInfos;
			this.expression = expression;
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x00119158 File Offset: 0x00117358
		public bool TryGetSearchFilter(FolderFilter oldFolderFilter, SearchFilter oldSearchFilter, out FolderFilter newFolderFilter, out SearchFilter newSearchFilter)
		{
			FolderFilter folderFilter;
			SearchFilter searchFilter;
			if (!this.TryVisitExpression(this.expression, out folderFilter, out searchFilter))
			{
				newFolderFilter = null;
				newSearchFilter = null;
				return false;
			}
			if (!oldFolderFilter.IsEmpty && folderFilter != null)
			{
				newFolderFilter = null;
				newSearchFilter = null;
				return false;
			}
			if (oldFolderFilter.IsEmpty && folderFilter != null)
			{
				newFolderFilter = (oldFolderFilter.HasSortDirection ? new FolderFilter(folderFilter, oldFolderFilter.SortDirection) : folderFilter);
			}
			else
			{
				newFolderFilter = oldFolderFilter;
			}
			if (searchFilter != null)
			{
				newSearchFilter = ExchangeSearchFilterHelper.GetSearchFilterCollection(LogicalOperator.And, oldSearchFilter, searchFilter);
			}
			else
			{
				newSearchFilter = oldSearchFilter;
			}
			return true;
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x001191D4 File Offset: 0x001173D4
		private bool TryVisitExpression(QueryExpression expression, out FolderFilter folderFilter, out SearchFilter itemSearchFilter)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.TryVisitBinaryExpression((BinaryQueryExpression)expression, out folderFilter, out itemSearchFilter);
			case QueryExpressionKind.Invocation:
				return this.TryVisitInvocationExpression((InvocationQueryExpression)expression, out folderFilter, out itemSearchFilter);
			case QueryExpressionKind.ArgumentAccess:
				return this.TryVisitExpression((ArgumentAccessQueryExpression)expression, out folderFilter, out itemSearchFilter);
			}
			folderFilter = null;
			itemSearchFilter = null;
			return false;
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x00119240 File Offset: 0x00117440
		private bool TryVisitInvocationExpression(InvocationQueryExpression invocationQueryExpression, out FolderFilter folderFilter, out SearchFilter itemSearchFilter)
		{
			ConstantQueryExpression constantQueryExpression = invocationQueryExpression.Function as ConstantQueryExpression;
			if (constantQueryExpression != null && constantQueryExpression.Value.Equals(Library.Text.Contains))
			{
				return this.TryGetColumnAccessValueFilter(invocationQueryExpression.Arguments[0], invocationQueryExpression.Arguments[1], FilterOptions.ContainsString, out folderFilter, out itemSearchFilter);
			}
			folderFilter = null;
			itemSearchFilter = null;
			return false;
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x00119298 File Offset: 0x00117498
		private bool TryVisitBinaryExpression(BinaryQueryExpression binaryExpression, out FolderFilter folderFilter, out SearchFilter itemSearchFilter)
		{
			switch (binaryExpression.Operator)
			{
			case BinaryOperator2.GreaterThan:
				return this.TryGetColumnAccessValueFilter(binaryExpression.Left, binaryExpression.Right, FilterOptions.IsGreaterThan, out folderFilter, out itemSearchFilter);
			case BinaryOperator2.LessThan:
				return this.TryGetColumnAccessValueFilter(binaryExpression.Left, binaryExpression.Right, FilterOptions.IsLessThan, out folderFilter, out itemSearchFilter);
			case BinaryOperator2.GreaterThanOrEquals:
				return this.TryGetColumnAccessValueFilter(binaryExpression.Left, binaryExpression.Right, FilterOptions.IsGreaterOrEqual, out folderFilter, out itemSearchFilter);
			case BinaryOperator2.LessThanOrEquals:
				return this.TryGetColumnAccessValueFilter(binaryExpression.Left, binaryExpression.Right, FilterOptions.IsLessOrEqual, out folderFilter, out itemSearchFilter);
			case BinaryOperator2.Equals:
				return this.TryGetColumnAccessValueFilter(binaryExpression.Left, binaryExpression.Right, FilterOptions.Equals, out folderFilter, out itemSearchFilter);
			case BinaryOperator2.NotEquals:
				return this.TryGetColumnAccessValueFilter(binaryExpression.Left, binaryExpression.Right, FilterOptions.NotEquals, out folderFilter, out itemSearchFilter);
			case BinaryOperator2.And:
				return this.TryVisitBinaryExpressionBoolean(binaryExpression, LogicalOperator.And, out folderFilter, out itemSearchFilter);
			case BinaryOperator2.Or:
				return this.TryVisitBinaryExpressionBoolean(binaryExpression, LogicalOperator.Or, out folderFilter, out itemSearchFilter);
			default:
				folderFilter = null;
				itemSearchFilter = null;
				return false;
			}
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x0011937C File Offset: 0x0011757C
		private bool TryVisitBinaryExpressionBoolean(BinaryQueryExpression binaryExpression, LogicalOperator op, out FolderFilter folderFilter, out SearchFilter itemSearchFilter)
		{
			FolderFilter folderFilter2;
			SearchFilter searchFilter;
			FolderFilter folderFilter3;
			SearchFilter searchFilter2;
			if (this.TryVisitExpression(binaryExpression.Left, out folderFilter2, out searchFilter) && this.TryVisitExpression(binaryExpression.Right, out folderFilter3, out searchFilter2))
			{
				if (op == LogicalOperator.And)
				{
					if (folderFilter2 == null || folderFilter3 == null)
					{
						folderFilter = folderFilter2 ?? folderFilter3;
						itemSearchFilter = ExchangeSearchFilterHelper.GetSearchFilterCollection(LogicalOperator.And, searchFilter, searchFilter2);
						return true;
					}
				}
				else if (op == LogicalOperator.Or)
				{
					if (folderFilter2 == null && searchFilter != null && folderFilter3 == null && searchFilter2 != null)
					{
						folderFilter = null;
						itemSearchFilter = ExchangeSearchFilterHelper.GetSearchFilterCollection(LogicalOperator.Or, searchFilter, searchFilter2);
						return true;
					}
					if (folderFilter2 != null && searchFilter == null && folderFilter3 != null && searchFilter2 == null)
					{
						folderFilter = new FolderFilter(folderFilter2, folderFilter3);
						itemSearchFilter = null;
						return true;
					}
				}
			}
			folderFilter = null;
			itemSearchFilter = null;
			return false;
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x00119410 File Offset: 0x00117610
		private bool TryGetValueFilter(ExchangeColumnInfo columnInfo, FilterOptions filterOption, Value value, out FolderFilter folderFilter, out SearchFilter searchFilter)
		{
			if (value.Type.TypeKind == columnInfo.Type.TypeKind && columnInfo.IsFoldable)
			{
				if (columnInfo.ColumnCategory == ColumnCategory.FolderPath && filterOption == FilterOptions.Equals)
				{
					folderFilter = new FolderFilter(value.AsString);
					searchFilter = null;
					return true;
				}
				if (columnInfo.ColumnCategory == ColumnCategory.PrimitiveColumn)
				{
					object foldableObjectFromValue = ExchangeSearchFilterBuilder.GetFoldableObjectFromValue(value);
					if (foldableObjectFromValue != null)
					{
						if (columnInfo.Property.Type.BaseType == typeof(Enum) && (filterOption != FilterOptions.Equals || !(foldableObjectFromValue is string) || !Enum.IsDefined(columnInfo.Property.Type, (string)foldableObjectFromValue)))
						{
							folderFilter = null;
							searchFilter = null;
							return false;
						}
						if (filterOption == FilterOptions.ContainsString && (string)foldableObjectFromValue == "")
						{
							folderFilter = null;
							searchFilter = null;
							return false;
						}
						if (columnInfo.Property.Type == typeof(DateTime))
						{
							folderFilter = null;
							return this.TryFilterDateTime(filterOption, foldableObjectFromValue, columnInfo, out searchFilter);
						}
						folderFilter = null;
						searchFilter = ExchangeSearchFilterHelper.GetSearchFilter(filterOption, columnInfo.Property, foldableObjectFromValue);
						return true;
					}
				}
			}
			folderFilter = null;
			searchFilter = null;
			return false;
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x00119538 File Offset: 0x00117738
		private bool TryFilterDateTime(FilterOptions filterOption, object datetimeObj, ExchangeColumnInfo columnInfo, out SearchFilter searchFilter)
		{
			DateTime dateTime = (DateTime)datetimeObj;
			switch (filterOption)
			{
			case FilterOptions.Equals:
			{
				SearchFilter searchFilter2 = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsLessThan, columnInfo.Property, dateTime.AddSeconds(1.0));
				SearchFilter searchFilter3 = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsGreaterOrEqual, columnInfo.Property, dateTime);
				searchFilter = ExchangeSearchFilterHelper.GetSearchFilterCollection(LogicalOperator.And, searchFilter2, searchFilter3);
				return true;
			}
			case FilterOptions.IsGreaterThan:
				searchFilter = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsGreaterOrEqual, columnInfo.Property, dateTime.AddSeconds(1.0));
				return true;
			case FilterOptions.IsLessThan:
				searchFilter = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsLessThan, columnInfo.Property, dateTime);
				return true;
			case FilterOptions.IsGreaterOrEqual:
				searchFilter = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsGreaterOrEqual, columnInfo.Property, dateTime);
				return true;
			case FilterOptions.IsLessOrEqual:
				searchFilter = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsLessThan, columnInfo.Property, dateTime.AddSeconds(1.0));
				return true;
			case FilterOptions.NotEquals:
			{
				SearchFilter searchFilter4 = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsLessThan, columnInfo.Property, dateTime);
				SearchFilter searchFilter5 = ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.IsGreaterOrEqual, columnInfo.Property, dateTime.AddSeconds(1.0));
				searchFilter = ExchangeSearchFilterHelper.GetSearchFilterCollection(LogicalOperator.Or, searchFilter4, searchFilter5);
				return true;
			}
			default:
				searchFilter = null;
				return false;
			}
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x00119678 File Offset: 0x00117878
		private bool TryGetColumnAccessValueFilter(QueryExpression left, QueryExpression right, FilterOptions filterOption, out FolderFilter folderFilter, out SearchFilter searchFilter)
		{
			ExchangeColumnInfo exchangeColumnInfo;
			Value value;
			if (this.TryGetColumnAccessValue(left, right, filterOption, out exchangeColumnInfo, out value))
			{
				return this.TryGetValueFilter(exchangeColumnInfo, filterOption, value, out folderFilter, out searchFilter);
			}
			folderFilter = null;
			searchFilter = null;
			return false;
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x001196AC File Offset: 0x001178AC
		private bool TryGetColumnAccessValue(QueryExpression left, QueryExpression right, FilterOptions filterOption, out ExchangeColumnInfo columnInfo, out Value value)
		{
			ColumnAccessQueryExpression columnAccessQueryExpression = null;
			ConstantQueryExpression constantQueryExpression = null;
			if (left.Kind == QueryExpressionKind.ColumnAccess && right.Kind == QueryExpressionKind.Constant)
			{
				columnAccessQueryExpression = (ColumnAccessQueryExpression)left;
				constantQueryExpression = (ConstantQueryExpression)right;
			}
			else if (filterOption == FilterOptions.Equals && right.Kind == QueryExpressionKind.ColumnAccess && left.Kind == QueryExpressionKind.Constant)
			{
				columnAccessQueryExpression = (ColumnAccessQueryExpression)right;
				constantQueryExpression = (ConstantQueryExpression)left;
			}
			if (columnAccessQueryExpression != null && constantQueryExpression != null)
			{
				columnInfo = this.columnInfos[columnAccessQueryExpression.Column];
				value = constantQueryExpression.Value;
				return true;
			}
			value = null;
			columnInfo = null;
			return false;
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x00119730 File Offset: 0x00117930
		private static object GetFoldableObjectFromValue(Value value)
		{
			if (value.IsText)
			{
				return value.AsString;
			}
			if (value.IsNumber)
			{
				try
				{
					return value.AsNumber.AsInteger32;
				}
				catch (ValueException)
				{
					return value.AsNumber.AsDouble;
				}
			}
			if (value.IsDateTime)
			{
				return value.AsDateTime.AsClrDateTime;
			}
			if (value.IsLogical)
			{
				return value.AsLogical.AsBoolean;
			}
			if (value.IsDuration)
			{
				return value.AsDuration.AsTimeSpan;
			}
			return null;
		}

		// Token: 0x04002DE6 RID: 11750
		private ExchangeColumnInfo[] columnInfos;

		// Token: 0x04002DE7 RID: 11751
		private QueryExpression expression;
	}
}
