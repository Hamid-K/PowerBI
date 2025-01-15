using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000229 RID: 553
	internal class ReportObjectModelContext
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x00029CF2 File Offset: 0x00027EF2
		internal ReportObjectModelContext()
		{
			this.Init();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00029D00 File Offset: 0x00027F00
		private void Init()
		{
			this.m_rdlFunctions = new Dictionary<string, RdlFunctionDefinition>(StringUtil.CaseInsensitiveComparer);
			this.AddAggregate(new RdlFunctionDefinition("Sum", typeof(FunctionAggrSum), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Numeric),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Avg", typeof(FunctionAggrAvg), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Numeric),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Max", typeof(FunctionAggrMax), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Min", typeof(FunctionAggrMin), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Count", typeof(FunctionAggrCount), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("CountDistinct", typeof(FunctionAggrCountDistinct), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("CountRows", typeof(FunctionAggrCountRows), new RdlFunctionArg[]
			{
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("StDev", typeof(FunctionAggrStdev), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Numeric),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("StDevP", typeof(FunctionAggrStdevp), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Numeric),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Var", typeof(FunctionAggrVar), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Numeric),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("VarP", typeof(FunctionAggrVarp), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Numeric),
				new RdlFunctionArg(false, RdlArgTypes.Scope),
				new RdlFunctionArg(false, RdlArgTypes.Recursive)
			}));
			this.AddAggregate(new RdlFunctionDefinition("First", typeof(FunctionAggrFirst), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
				new RdlFunctionArg(false, RdlArgTypes.Scope)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Last", typeof(FunctionAggrLast), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
				new RdlFunctionArg(false, RdlArgTypes.Scope)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Previous", typeof(FunctionAggrPrev), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
				new RdlFunctionArg(false, RdlArgTypes.Scope)
			}));
			this.AddAggregate(new RdlFunctionDefinition("RunningValue", typeof(FunctionAggrRunningValue), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
				new RdlFunctionArg(true, RdlArgTypes.AggregateFunction),
				new RdlFunctionArg(true, RdlArgTypes.Scope)
			}));
			this.AddAggregate(new RdlFunctionDefinition("RowNumber", typeof(FunctionAggrRowNumber), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Scope)
			}));
			this.AddAggregate(new RdlFunctionDefinition("Aggregate", typeof(FunctionAggrAggregate), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(false, RdlArgTypes.Scope)
			}));
			this.AddFunction(new RdlFunctionDefinition("Level", typeof(FunctionAggrLevel), new RdlFunctionArg[]
			{
				new RdlFunctionArg(false, RdlArgTypes.Scope)
			}));
			this.AddFunction(new RdlFunctionDefinition("InScope", typeof(FunctionAggrInScope), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Scope)
			}));
			this.AddFunction(new RdlFunctionDefinition("CreateDrillthroughContext", typeof(CreateDrillthroughContext), Array.Empty<RdlFunctionArg>()));
			this.AddFunction(new RdlFunctionDefinition("MinValue", typeof(FunctionMinValue), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(false, RdlArgTypes.Variant, true)
			}));
			this.AddFunction(new RdlFunctionDefinition("MaxValue", typeof(FunctionMaxValue), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(false, RdlArgTypes.Variant, true)
			}));
			this.AddFunction(new RdlFunctionDefinition("Lookup", typeof(FunctionLookup), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Scope)
			}));
			this.AddFunction(new RdlFunctionDefinition("LookupSet", typeof(FunctionLookupSet), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Scope)
			}));
			this.AddFunction(new RdlFunctionDefinition("MultiLookup", typeof(FunctionMultiLookup), new RdlFunctionArg[]
			{
				new RdlFunctionArg(true, RdlArgTypes.VariantArray),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Variant),
				new RdlFunctionArg(true, RdlArgTypes.Scope)
			}));
			this.m_fields = new RdlFieldsCollection();
			this.m_parameters = new RdlParametersCollection();
			this.m_reportItems = new RdlReportItemsCollection();
			this.m_dataSources = new RdlDataSourcesCollection();
			this.m_dataSets = new RdlDataSetsCollection();
			this.m_variables = new RdlVariablesCollection();
			this.m_globals = new RdlGlobalsCollection();
			this.m_user = new RdlUsersCollection();
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0002A316 File Offset: 0x00028516
		private void AddAggregate(RdlFunctionDefinition functionDef)
		{
			this.m_rdlFunctions.Add(functionDef.Name, functionDef);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0002A32A File Offset: 0x0002852A
		private void AddFunction(RdlFunctionDefinition functionDef)
		{
			this.m_rdlFunctions.Add(functionDef.Name, functionDef);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0002A33E File Offset: 0x0002853E
		internal bool TryMatchRdlFunction(string name, out RdlFunctionDefinition functionDef)
		{
			return this.m_rdlFunctions.TryGetValue(name, out functionDef);
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0002A34D File Offset: 0x0002854D
		internal IComplexRdlCollection Fields
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0002A355 File Offset: 0x00028555
		internal IComplexRdlCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0002A35D File Offset: 0x0002855D
		internal IComplexRdlCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0002A365 File Offset: 0x00028565
		internal IComplexRdlCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0002A36D File Offset: 0x0002856D
		internal IComplexRdlCollection DataSets
		{
			get
			{
				return this.m_dataSets;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0002A375 File Offset: 0x00028575
		internal ISimpleRdlCollection Globals
		{
			get
			{
				return this.m_globals;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0002A37D File Offset: 0x0002857D
		internal ISimpleRdlCollection User
		{
			get
			{
				return this.m_user;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0002A385 File Offset: 0x00028585
		internal IComplexRdlCollection Variables
		{
			get
			{
				return this.m_variables;
			}
		}

		// Token: 0x040005DF RID: 1503
		private Dictionary<string, RdlFunctionDefinition> m_rdlFunctions;

		// Token: 0x040005E0 RID: 1504
		private IComplexRdlCollection m_fields;

		// Token: 0x040005E1 RID: 1505
		private IComplexRdlCollection m_parameters;

		// Token: 0x040005E2 RID: 1506
		private IComplexRdlCollection m_reportItems;

		// Token: 0x040005E3 RID: 1507
		private IComplexRdlCollection m_dataSources;

		// Token: 0x040005E4 RID: 1508
		private IComplexRdlCollection m_dataSets;

		// Token: 0x040005E5 RID: 1509
		private IComplexRdlCollection m_variables;

		// Token: 0x040005E6 RID: 1510
		private ISimpleRdlCollection m_globals;

		// Token: 0x040005E7 RID: 1511
		private ISimpleRdlCollection m_user;
	}
}
