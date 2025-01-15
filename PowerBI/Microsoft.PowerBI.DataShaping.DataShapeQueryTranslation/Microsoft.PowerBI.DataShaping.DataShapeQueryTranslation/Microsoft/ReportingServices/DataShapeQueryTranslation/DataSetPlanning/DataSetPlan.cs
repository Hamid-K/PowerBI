using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000DF RID: 223
	internal sealed class DataSetPlan : IDataSetPlan, IStructuredToString
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x000233F8 File Offset: 0x000215F8
		internal DataSetPlan(string name, int planIndex, List<ScopePlanElement> elements, bool filterEmptyGroups, bool suppressSortingAndLimits, ExtensionSchema extensionSchema, string dataSourceVariables, IReadOnlyList<ModelParameter> modelParameters, IReadOnlyList<QueryParameterDeclaration> queryParameters)
		{
			this.m_name = name;
			this.m_planIndex = planIndex;
			this.m_scopes = elements.AsReadOnly();
			this.m_filterEmptyGroups = filterEmptyGroups;
			this.m_suppressSortingAndLimits = suppressSortingAndLimits;
			this.m_extensionSchema = extensionSchema;
			this.m_dataSourceVariables = dataSourceVariables;
			this.m_modelParameters = modelParameters;
			this.m_queryParameters = queryParameters;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00023455 File Offset: 0x00021655
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0002345D File Offset: 0x0002165D
		public int PlanIndex
		{
			get
			{
				return this.m_planIndex;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00023465 File Offset: 0x00021665
		public ReadOnlyCollection<ScopePlanElement> Scopes
		{
			get
			{
				return this.m_scopes;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0002346D File Offset: 0x0002166D
		public bool FilterEmptyGroups
		{
			get
			{
				return this.m_filterEmptyGroups;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00023475 File Offset: 0x00021675
		public bool SuppressSortingAndLimits
		{
			get
			{
				return this.m_suppressSortingAndLimits;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0002347D File Offset: 0x0002167D
		public ExtensionSchema ExtensionSchema
		{
			get
			{
				return this.m_extensionSchema;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00023485 File Offset: 0x00021685
		public string DataSourceVariables
		{
			get
			{
				return this.m_dataSourceVariables;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0002348D File Offset: 0x0002168D
		public IReadOnlyList<ModelParameter> ModelParameters
		{
			get
			{
				return this.m_modelParameters;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00023495 File Offset: 0x00021695
		public IReadOnlyList<QueryParameterDeclaration> QueryParameters
		{
			get
			{
				return this.m_queryParameters;
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000234A0 File Offset: 0x000216A0
		public DataSetPlan AddExpression(ExpressionPlanElement expressionElement, IScope targetScope)
		{
			List<ScopePlanElement> list = new List<ScopePlanElement>(this.m_scopes.Count);
			for (int i = 0; i < this.m_scopes.Count; i++)
			{
				ScopePlanElement scopePlanElement = this.m_scopes[i];
				if (scopePlanElement.Scope == targetScope)
				{
					scopePlanElement = scopePlanElement.AddNestedPlanElement(expressionElement);
				}
				list.Add(scopePlanElement);
			}
			return new DataSetPlan(this.Name, this.PlanIndex, list, this.FilterEmptyGroups, this.SuppressSortingAndLimits, this.ExtensionSchema, this.DataSourceVariables, this.ModelParameters, this.QueryParameters);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00023530 File Offset: 0x00021730
		public int GetInnermostProjectedGroupIndex()
		{
			int i;
			for (i = this.m_scopes.Count - 1; i >= 0; i--)
			{
				ScopePlanElement scopePlanElement = this.m_scopes[i];
				if (scopePlanElement.Scope.ObjectType == ObjectType.DataMember && scopePlanElement.IsProjected)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002357C File Offset: 0x0002177C
		public List<IScope> GetScopesInMeasureContext(ScopeTree scopeTree)
		{
			List<IScope> list = new List<IScope>();
			if (this.m_scopes.Count == 1)
			{
				list.Add(this.m_scopes[0].Scope);
				return list;
			}
			IList<DataMemberPlanElement> list2 = (from s in this.m_scopes.OfType<DataMemberPlanElement>()
				where !s.DataMember.ContextOnly && s.DataMember.IsDynamic
				select s).Evaluate<DataMemberPlanElement>();
			using (IEnumerator<ScopePlanElement> enumerator = this.m_scopes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ScopePlanElement scopeElement = enumerator.Current;
					if (list2.All((DataMemberPlanElement dataMemberElement) => scopeTree.IsSameOrParentScope(dataMemberElement.Scope, scopeElement.Scope)))
					{
						list.Add(scopeElement.Scope);
					}
				}
			}
			return list;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00023670 File Offset: 0x00021870
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataSetPlan");
			builder.WriteAttribute<string>("Name", this.m_name, false, false);
			builder.WriteAttribute<int>("PlanIndex", this.m_planIndex, true, false);
			builder.WriteAttribute<bool>("FilterEmptyGroups", this.m_filterEmptyGroups, true, false);
			builder.WriteAttribute<bool>("SuppressSortingAndLimits", this.m_suppressSortingAndLimits, false, false);
			builder.WriteProperty<ReadOnlyCollection<ScopePlanElement>>("Scopes", this.m_scopes, false);
			builder.WriteProperty<IReadOnlyList<ModelParameter>>("ModelParameters", this.m_modelParameters, false);
			builder.WriteProperty<IReadOnlyList<QueryParameterDeclaration>>("QueryParmeters", this.m_queryParameters, false);
			builder.EndObject();
		}

		// Token: 0x04000455 RID: 1109
		private readonly string m_name;

		// Token: 0x04000456 RID: 1110
		private readonly int m_planIndex;

		// Token: 0x04000457 RID: 1111
		private readonly ReadOnlyCollection<ScopePlanElement> m_scopes;

		// Token: 0x04000458 RID: 1112
		private readonly bool m_filterEmptyGroups;

		// Token: 0x04000459 RID: 1113
		private readonly bool m_suppressSortingAndLimits;

		// Token: 0x0400045A RID: 1114
		private readonly ExtensionSchema m_extensionSchema;

		// Token: 0x0400045B RID: 1115
		private readonly string m_dataSourceVariables;

		// Token: 0x0400045C RID: 1116
		private readonly IReadOnlyList<ModelParameter> m_modelParameters;

		// Token: 0x0400045D RID: 1117
		private readonly IReadOnlyList<QueryParameterDeclaration> m_queryParameters;
	}
}
