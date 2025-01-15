using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000C5 RID: 197
	public class SelectExpandQueryOption
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x000167D0 File Offset: 0x000149D0
		public SelectExpandQueryOption(string select, string expand, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (string.IsNullOrEmpty(select) && string.IsNullOrEmpty(expand))
			{
				throw Error.Argument(SRResources.SelectExpandEmptyOrNull, new object[0]);
			}
			if (queryOptionParser == null)
			{
				throw Error.ArgumentNull("queryOptionParser");
			}
			if (!(context.ElementType is IEdmStructuredType))
			{
				throw Error.Argument(SRResources.SelectNonStructured, new object[] { context.ElementType });
			}
			this.Context = context;
			this.RawSelect = select;
			this.RawExpand = expand;
			this.Validator = SelectExpandQueryValidator.GetSelectExpandQueryValidator(context);
			this._queryOptionParser = queryOptionParser;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00016878 File Offset: 0x00014A78
		internal SelectExpandQueryOption(string select, string expand, ODataQueryContext context, SelectExpandClause selectExpandClause)
			: this(select, expand, context)
		{
			this._selectExpandClause = selectExpandClause;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001688C File Offset: 0x00014A8C
		internal SelectExpandQueryOption(string select, string expand, ODataQueryContext context)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (string.IsNullOrEmpty(select) && string.IsNullOrEmpty(expand))
			{
				throw Error.Argument(SRResources.SelectExpandEmptyOrNull, new object[0]);
			}
			if (!(context.ElementType is IEdmStructuredType))
			{
				throw Error.Argument("context", SRResources.SelectNonStructured, new object[] { context.ElementType.ToTraceString() });
			}
			this.Context = context;
			this.RawSelect = select;
			this.RawExpand = expand;
			this.Validator = SelectExpandQueryValidator.GetSelectExpandQueryValidator(context);
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, new Dictionary<string, string>
			{
				{ "$select", select },
				{ "$expand", expand }
			}, context.RequestContainer);
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00016967 File Offset: 0x00014B67
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0001696F File Offset: 0x00014B6F
		public ODataQueryContext Context { get; private set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00016978 File Offset: 0x00014B78
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x00016980 File Offset: 0x00014B80
		public string RawSelect { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00016989 File Offset: 0x00014B89
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x00016991 File Offset: 0x00014B91
		public string RawExpand { get; private set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001699A File Offset: 0x00014B9A
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x000169A2 File Offset: 0x00014BA2
		public SelectExpandQueryValidator Validator { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000169AB File Offset: 0x00014BAB
		public SelectExpandClause SelectExpandClause
		{
			get
			{
				if (this._selectExpandClause == null)
				{
					this._selectExpandClause = this._queryOptionParser.ParseSelectAndExpand();
				}
				return this._selectExpandClause;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000169CC File Offset: 0x00014BCC
		internal SelectExpandClause ProcessedSelectExpandClause
		{
			get
			{
				if (this._processedSelectExpandClause != null)
				{
					return this._processedSelectExpandClause;
				}
				this._processedSelectExpandClause = this.ProcessLevels();
				return this._processedSelectExpandClause;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x000169EF File Offset: 0x00014BEF
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x000169F7 File Offset: 0x00014BF7
		public int LevelsMaxLiteralExpansionDepth
		{
			get
			{
				return this._levelsMaxLiteralExpansionDepth;
			}
			set
			{
				if (value < 0)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("LevelsMaxLiteralExpansionDepth", value, 0);
				}
				this._levelsMaxLiteralExpansionDepth = value;
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00016A1C File Offset: 0x00014C1C
		public IQueryable ApplyTo(IQueryable queryable, ODataQuerySettings settings)
		{
			if (queryable == null)
			{
				throw Error.ArgumentNull("queryable");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "ApplyTo" });
			}
			ODataQuerySettings odataQuerySettings = this.Context.UpdateQuerySettings(settings, queryable);
			return SelectExpandBinder.Bind(queryable, odataQuerySettings, this);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00016A88 File Offset: 0x00014C88
		public object ApplyTo(object entity, ODataQuerySettings settings)
		{
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "ApplyTo" });
			}
			ODataQuerySettings odataQuerySettings = this.Context.UpdateQuerySettings(settings, null);
			return SelectExpandBinder.Bind(entity, odataQuerySettings, this);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00016AF3 File Offset: 0x00014CF3
		public void Validate(ODataValidationSettings validationSettings)
		{
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			if (this.Validator != null)
			{
				this.Validator.Validate(this, validationSettings);
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00016B18 File Offset: 0x00014D18
		internal SelectExpandClause ProcessLevels()
		{
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(this.Context.TargetProperty, this.Context.TargetStructuredType, this.Context.Model, this.Context.DefaultQuerySettings);
			bool flag;
			bool flag2;
			return this.ProcessLevels(this.SelectExpandClause, (this.LevelsMaxLiteralExpansionDepth < 0) ? 2 : this.LevelsMaxLiteralExpansionDepth, modelBoundQuerySettings, out flag, out flag2);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00016B7C File Offset: 0x00014D7C
		private SelectExpandClause ProcessLevels(SelectExpandClause selectExpandClause, int levelsMaxLiteralExpansionDepth, ModelBoundQuerySettings querySettings, out bool levelsEncountered, out bool isMaxLevel)
		{
			levelsEncountered = false;
			isMaxLevel = false;
			if (selectExpandClause == null)
			{
				return null;
			}
			IEnumerable<SelectItem> enumerable = this.ProcessLevels(selectExpandClause.SelectedItems, levelsMaxLiteralExpansionDepth, querySettings, out levelsEncountered, out isMaxLevel);
			if (enumerable == null)
			{
				return null;
			}
			if (levelsEncountered)
			{
				return new SelectExpandClause(enumerable, selectExpandClause.AllSelected);
			}
			return selectExpandClause;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00016BC4 File Offset: 0x00014DC4
		private IEnumerable<SelectItem> ProcessLevels(IEnumerable<SelectItem> selectItems, int levelsMaxLiteralExpansionDepth, ModelBoundQuerySettings querySettings, out bool levelsEncountered, out bool isMaxLevel)
		{
			levelsEncountered = false;
			isMaxLevel = false;
			IList<SelectItem> list = new List<SelectItem>();
			foreach (SelectItem selectItem in selectItems)
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = selectItem as ExpandedNavigationSelectItem;
				if (expandedNavigationSelectItem == null)
				{
					list.Add(selectItem);
				}
				else
				{
					bool flag;
					bool flag2;
					ExpandedNavigationSelectItem expandedNavigationSelectItem2 = this.ProcessLevels(expandedNavigationSelectItem, levelsMaxLiteralExpansionDepth, querySettings, out flag, out flag2);
					if (expandedNavigationSelectItem.LevelsOption != null && expandedNavigationSelectItem.LevelsOption.Level > 0L && expandedNavigationSelectItem2 == null)
					{
						return null;
					}
					if (expandedNavigationSelectItem.LevelsOption != null)
					{
						isMaxLevel = isMaxLevel || flag2;
					}
					levelsEncountered = levelsEncountered || flag;
					if (expandedNavigationSelectItem2 != null)
					{
						list.Add(expandedNavigationSelectItem2);
					}
				}
			}
			return list;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00016C80 File Offset: 0x00014E80
		private void GetAutoSelectExpandItems(IEdmEntityType baseEntityType, IEdmModel model, IEdmNavigationSource navigationSource, bool isAllSelected, ModelBoundQuerySettings modelBoundQuerySettings, int depth, out List<SelectItem> autoSelectItems, out List<SelectItem> autoExpandItems)
		{
			autoSelectItems = new List<SelectItem>();
			IEnumerable<IEdmStructuralProperty> autoSelectProperties = EdmLibHelpers.GetAutoSelectProperties(null, baseEntityType, model, modelBoundQuerySettings);
			foreach (IEdmStructuralProperty edmStructuralProperty in autoSelectProperties)
			{
				PathSelectItem pathSelectItem = new PathSelectItem(new ODataSelectPath(new List<ODataPathSegment>
				{
					new PropertySegment(edmStructuralProperty)
				}));
				autoSelectItems.Add(pathSelectItem);
			}
			autoExpandItems = new List<SelectItem>();
			depth--;
			if (depth < 0)
			{
				return;
			}
			foreach (IEdmNavigationProperty edmNavigationProperty in EdmLibHelpers.GetAutoExpandNavigationProperties(null, baseEntityType, model, !isAllSelected, modelBoundQuerySettings))
			{
				IEdmNavigationSource edmNavigationSource = navigationSource.FindNavigationTarget(edmNavigationProperty);
				if (edmNavigationSource != null)
				{
					List<ODataPathSegment> list = new List<ODataPathSegment>
					{
						new NavigationPropertySegment(edmNavigationProperty, edmNavigationSource)
					};
					ODataExpandPath odataExpandPath = new ODataExpandPath(list);
					SelectExpandClause selectExpandClause = new SelectExpandClause(new List<SelectItem>(), true);
					ExpandedNavigationSelectItem expandedNavigationSelectItem = new ExpandedNavigationSelectItem(odataExpandPath, edmNavigationSource, selectExpandClause);
					modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(edmNavigationProperty, edmNavigationProperty.ToEntityType(), model, null);
					int maxExpandDepth = SelectExpandQueryOption.GetMaxExpandDepth(modelBoundQuerySettings, edmNavigationProperty.Name);
					if (maxExpandDepth != 0 && maxExpandDepth < depth)
					{
						depth = maxExpandDepth;
					}
					List<SelectItem> list2;
					List<SelectItem> list3;
					this.GetAutoSelectExpandItems(edmNavigationSource.EntityType(), model, expandedNavigationSelectItem.NavigationSource, true, modelBoundQuerySettings, depth, out list2, out list3);
					selectExpandClause = new SelectExpandClause(list2.Concat(list3), list2.Count == 0);
					expandedNavigationSelectItem = new ExpandedNavigationSelectItem(odataExpandPath, edmNavigationSource, selectExpandClause);
					autoExpandItems.Add(expandedNavigationSelectItem);
					if (!isAllSelected || autoSelectProperties.Count<IEdmStructuralProperty>() != 0)
					{
						PathSelectItem pathSelectItem2 = new PathSelectItem(new ODataSelectPath(list));
						autoExpandItems.Add(pathSelectItem2);
					}
				}
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00016E58 File Offset: 0x00015058
		private ExpandedNavigationSelectItem ProcessLevels(ExpandedNavigationSelectItem expandItem, int levelsMaxLiteralExpansionDepth, ModelBoundQuerySettings querySettings, out bool levelsEncounteredInExpand, out bool isMaxLevelInExpand)
		{
			isMaxLevelInExpand = false;
			int i;
			if (expandItem.LevelsOption == null)
			{
				levelsEncounteredInExpand = false;
				i = 1;
			}
			else
			{
				levelsEncounteredInExpand = true;
				if (expandItem.LevelsOption.IsMaxLevel)
				{
					isMaxLevelInExpand = true;
					i = levelsMaxLiteralExpansionDepth;
				}
				else
				{
					i = (int)expandItem.LevelsOption.Level;
				}
			}
			if (i <= 0 || i > levelsMaxLiteralExpansionDepth)
			{
				return null;
			}
			ExpandedNavigationSelectItem expandedNavigationSelectItem = null;
			SelectExpandClause selectExpandClause = null;
			bool flag = false;
			bool flag2 = false;
			IEdmEntityType edmEntityType = expandItem.NavigationSource.EntityType();
			IEdmNavigationProperty navigationProperty = (expandItem.PathToNavigationProperty.LastSegment as NavigationPropertySegment).NavigationProperty;
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(navigationProperty, navigationProperty.ToEntityType(), this.Context.Model, null);
			while (selectExpandClause == null && i > 0)
			{
				selectExpandClause = this.ProcessLevels(expandItem.SelectAndExpand, levelsMaxLiteralExpansionDepth - i, modelBoundQuerySettings, out flag, out flag2);
				i--;
			}
			if (selectExpandClause == null)
			{
				return null;
			}
			i++;
			int num = SelectExpandQueryOption.GetMaxExpandDepth(querySettings, navigationProperty.Name);
			if (num == 0 || levelsMaxLiteralExpansionDepth > num)
			{
				num = levelsMaxLiteralExpansionDepth;
			}
			List<SelectItem> list;
			List<SelectItem> list2;
			this.GetAutoSelectExpandItems(edmEntityType, this.Context.Model, expandItem.NavigationSource, selectExpandClause.AllSelected, modelBoundQuerySettings, num - 1, out list, out list2);
			if (expandItem.SelectAndExpand.SelectedItems.Any((SelectItem it) => it is PathSelectItem))
			{
				list.Clear();
			}
			if (i > 1)
			{
				SelectExpandQueryOption.RemoveSameExpandItem(navigationProperty, list2);
			}
			List<SelectItem> list3 = new List<SelectItem>(list2);
			bool flag3 = list.Count<SelectItem>() + list2.Count<SelectItem>() != 0;
			bool flag4 = list.Count == 0 && selectExpandClause.AllSelected;
			while (i > 0)
			{
				list3 = SelectExpandQueryOption.RemoveExpandItemExceedMaxDepth(num - i, list2);
				SelectExpandClause selectExpandClause2;
				if (expandedNavigationSelectItem == null)
				{
					if (flag3)
					{
						selectExpandClause2 = new SelectExpandClause(new SelectItem[0].Concat(selectExpandClause.SelectedItems).Concat(list).Concat(list3), flag4);
					}
					else
					{
						selectExpandClause2 = selectExpandClause;
					}
				}
				else if (selectExpandClause.AllSelected)
				{
					selectExpandClause2 = new SelectExpandClause(new SelectItem[] { expandedNavigationSelectItem }.Concat(selectExpandClause.SelectedItems).Concat(list).Concat(list3), flag4);
				}
				else
				{
					PathSelectItem pathSelectItem = new PathSelectItem(new ODataSelectPath(expandItem.PathToNavigationProperty));
					SelectItem[] array = new SelectItem[] { expandedNavigationSelectItem, pathSelectItem };
					selectExpandClause2 = new SelectExpandClause(new SelectItem[0].Concat(selectExpandClause.SelectedItems).Concat(array).Concat(list)
						.Concat(list3), flag4);
				}
				expandedNavigationSelectItem = new ExpandedNavigationSelectItem(expandItem.PathToNavigationProperty, expandItem.NavigationSource, selectExpandClause2, expandItem.FilterOption, expandItem.OrderByOption, expandItem.TopOption, expandItem.SkipOption, expandItem.CountOption, expandItem.SearchOption, null, expandItem.ComputeOption, expandItem.ApplyOption);
				i--;
				if (flag2)
				{
					selectExpandClause = this.ProcessLevels(expandItem.SelectAndExpand, levelsMaxLiteralExpansionDepth - i, modelBoundQuerySettings, out flag, out flag2);
				}
			}
			levelsEncounteredInExpand = levelsEncounteredInExpand || flag || flag3;
			isMaxLevelInExpand = isMaxLevelInExpand || flag2;
			return expandedNavigationSelectItem;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001712C File Offset: 0x0001532C
		private static List<SelectItem> RemoveExpandItemExceedMaxDepth(int depth, IEnumerable<SelectItem> autoExpandItems)
		{
			List<SelectItem> list = new List<SelectItem>();
			if (depth <= 0)
			{
				using (IEnumerator<SelectItem> enumerator = autoExpandItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SelectItem selectItem = enumerator.Current;
						if (!(selectItem is ExpandedNavigationSelectItem))
						{
							list.Add(selectItem);
						}
					}
					return list;
				}
			}
			foreach (SelectItem selectItem2 in autoExpandItems)
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = selectItem2 as ExpandedNavigationSelectItem;
				if (expandedNavigationSelectItem != null)
				{
					SelectExpandClause selectExpandClause = new SelectExpandClause(SelectExpandQueryOption.RemoveExpandItemExceedMaxDepth(depth - 1, expandedNavigationSelectItem.SelectAndExpand.SelectedItems), expandedNavigationSelectItem.SelectAndExpand.AllSelected);
					expandedNavigationSelectItem = new ExpandedNavigationSelectItem(expandedNavigationSelectItem.PathToNavigationProperty, expandedNavigationSelectItem.NavigationSource, selectExpandClause);
					list.Add(expandedNavigationSelectItem);
				}
				else
				{
					list.Add(selectItem2);
				}
			}
			return list;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017218 File Offset: 0x00015418
		private static void RemoveSameExpandItem(IEdmNavigationProperty navigationProperty, List<SelectItem> autoExpandItems)
		{
			for (int i = 0; i < autoExpandItems.Count; i++)
			{
				IEdmNavigationProperty navigationProperty2 = ((autoExpandItems[i] as ExpandedNavigationSelectItem).PathToNavigationProperty.LastSegment as NavigationPropertySegment).NavigationProperty;
				if (navigationProperty.Name.Equals(navigationProperty2.Name))
				{
					autoExpandItems.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00017274 File Offset: 0x00015474
		private static int GetMaxExpandDepth(ModelBoundQuerySettings querySettings, string propertyName)
		{
			int num = 0;
			if (querySettings != null)
			{
				ExpandConfiguration expandConfiguration;
				if (querySettings.ExpandConfigurations.TryGetValue(propertyName, out expandConfiguration))
				{
					num = expandConfiguration.MaxDepth;
				}
				else if (querySettings.DefaultExpandType != null)
				{
					SelectExpandType? defaultExpandType = querySettings.DefaultExpandType;
					SelectExpandType selectExpandType = SelectExpandType.Disabled;
					if (!((defaultExpandType.GetValueOrDefault() == selectExpandType) & (defaultExpandType != null)))
					{
						num = querySettings.DefaultMaxDepth;
					}
				}
			}
			return num;
		}

		// Token: 0x04000192 RID: 402
		private SelectExpandClause _selectExpandClause;

		// Token: 0x04000193 RID: 403
		private ODataQueryOptionParser _queryOptionParser;

		// Token: 0x04000194 RID: 404
		private SelectExpandClause _processedSelectExpandClause;

		// Token: 0x04000195 RID: 405
		private int _levelsMaxLiteralExpansionDepth = -1;
	}
}
