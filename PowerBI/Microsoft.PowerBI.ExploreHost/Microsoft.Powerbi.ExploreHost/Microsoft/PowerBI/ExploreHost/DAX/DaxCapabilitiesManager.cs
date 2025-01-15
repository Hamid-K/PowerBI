using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Edm;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Query.Contracts.DaxCapabilities;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ExploreHost.DAX
{
	// Token: 0x02000091 RID: 145
	internal sealed class DaxCapabilitiesManager : IDaxCapabilitiesManager
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x0000BE38 File Offset: 0x0000A038
		public DaxCapabilitiesManager(IPowerViewHandler powerViewHandler)
		{
			this.m_daxFunctionsCache = new ConcurrentDictionary<string, ReadOnlyCollection<DaxFunction>>(StringComparer.Ordinal);
			this.m_powerViewHandler = powerViewHandler;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000BE58 File Offset: 0x0000A058
		public DaxCapabilities GetDaxCapabilities(string databaseID)
		{
			IList<DaxFunction> daxFunctions = this.GetDaxFunctions(databaseID);
			string text = "2.0";
			ModelDaxCapabilities modelDaxCapabilities;
			ExploreHostUtils.GetConceptualSchema(this.m_powerViewHandler, databaseID, text, null, out modelDaxCapabilities);
			return DaxCapabilitiesManager.BuildDaxCapabilities(daxFunctions, modelDaxCapabilities);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000BE94 File Offset: 0x0000A094
		public ReadOnlyCollection<DaxFunction> GetDaxFunctions(string databaseID)
		{
			string connectionString = this.m_powerViewHandler.GetDataSourceInfo(databaseID).ConnectionString;
			return this.m_daxFunctionsCache.GetOrAdd(connectionString, (string key) => DaxCapabilitiesManager.GetDaxFunctionsCore(this.m_powerViewHandler, databaseID));
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		internal static DaxCapabilities BuildDaxCapabilities(IList<DaxFunction> functions, ModelDaxCapabilities daxCapabilities)
		{
			List<DaxOperator> list = new List<DaxOperator>(4);
			if (daxCapabilities.SupportsVariables)
			{
				list.Add(new DaxOperator
				{
					Name = "VAR"
				});
				list.Add(new DaxOperator
				{
					Name = "RETURN"
				});
			}
			if (daxCapabilities.SupportsInOperator)
			{
				list.Add(new DaxOperator
				{
					Name = "IN"
				});
			}
			if (functions.FirstOrDefault((DaxFunction func) => func.Name == "NOT") != null)
			{
				list.Add(new DaxOperator
				{
					Name = "NOT"
				});
			}
			return new DaxCapabilities
			{
				SupportedFunctions = functions,
				SupportedOperators = list,
				SupportsVariations = DaxCapabilitiesUtils.SupportsVariations(functions),
				SupportsTableConstructor = daxCapabilities.SupportsTableConstructor,
				SupportsVirtualColumns = daxCapabilities.SupportsVirtualColumns
			};
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		private static ReadOnlyCollection<DaxFunction> GetDaxFunctionsCore(IPowerViewHandler powerViewHandler, string databaseID)
		{
			List<DaxFunction> daxFunctionsCore = DaxCapabilitiesManager.GetDaxFunctionsCore(powerViewHandler, databaseID, 4);
			List<DaxFunction> daxFunctionsCore2 = DaxCapabilitiesManager.GetDaxFunctionsCore(powerViewHandler, databaseID, 3);
			List<DaxFunction> list = new List<DaxFunction>(daxFunctionsCore.Count + daxFunctionsCore2.Count);
			list.AddRange(daxFunctionsCore);
			list.AddRange(daxFunctionsCore2);
			return list.AsReadOnlyCollection<DaxFunction>();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C004 File Offset: 0x0000A204
		private static List<DaxFunction> GetDaxFunctionsCore(IPowerViewHandler powerViewHandler, string databaseID, int functionsOrigin)
		{
			return DaxCapabilitiesUtils.ParseDaxFunctions(ExploreHostUtils.GetSchemaDataSet(powerViewHandler, databaseID, "MDSCHEMA_FUNCTIONS", DaxCapabilitiesUtils.GetFunctionSchemaRestrictions(functionsOrigin, null))).ToList<DaxFunction>();
		}

		// Token: 0x040001BD RID: 445
		private readonly ConcurrentDictionary<string, ReadOnlyCollection<DaxFunction>> m_daxFunctionsCache;

		// Token: 0x040001BE RID: 446
		private readonly IPowerViewHandler m_powerViewHandler;
	}
}
