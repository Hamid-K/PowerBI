using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000154 RID: 340
	internal abstract class GeneratedDeclarationCollection
	{
		// Token: 0x06000C8A RID: 3210 RVA: 0x00033F6C File Offset: 0x0003216C
		protected GeneratedDeclarationCollection()
		{
			this.m_tableDeclarations = new Dictionary<string, GeneratedTableDeclaration>(StringComparer.Ordinal);
			this.m_multiTableDeclarations = new Dictionary<string, List<GeneratedTableDeclaration>>(StringComparer.Ordinal);
			this.m_scalarDeclarations = new Dictionary<string, GeneratedScalarDeclaration>(StringComparer.Ordinal);
			this.m_entityDeclarations = new Dictionary<string, GeneratedEntityDeclaration>(StringComparer.Ordinal);
			this.m_reconciledDeclarations = new Dictionary<string, List<GeneratedTableDeclaration>>();
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00033FCA File Offset: 0x000321CA
		protected GeneratedDeclarationCollection(Dictionary<string, GeneratedTableDeclaration> tableDeclarations, Dictionary<string, List<GeneratedTableDeclaration>> multiTableDeclarations, Dictionary<string, GeneratedScalarDeclaration> scalarDeclarations, Dictionary<string, GeneratedEntityDeclaration> entityDeclarations, Dictionary<string, List<GeneratedTableDeclaration>> reconciledDeclarationNames)
		{
			this.m_tableDeclarations = tableDeclarations;
			this.m_multiTableDeclarations = multiTableDeclarations;
			this.m_scalarDeclarations = scalarDeclarations;
			this.m_entityDeclarations = entityDeclarations;
			this.m_reconciledDeclarations = reconciledDeclarationNames;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00033FF8 File Offset: 0x000321F8
		public ExpressionReferenceNameToTableMapping ExpressionReferenceNameToTableMapping
		{
			get
			{
				IEnumerable<KeyValuePair<string, QueryTable>> enumerable = from t in this.m_multiTableDeclarations.SelectMany((KeyValuePair<string, List<GeneratedTableDeclaration>> v) => v.Value)
					select new KeyValuePair<string, QueryTable>(t.QueryName, t.OriginalTable.QueryTable);
				return new ExpressionReferenceNameToTableMapping(this.m_tableDeclarations.Select((KeyValuePair<string, GeneratedTableDeclaration> pair) => new KeyValuePair<string, QueryTable>(pair.Value.QueryName, pair.Value.OriginalTable.QueryTable)).Concat(enumerable));
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0003408C File Offset: 0x0003228C
		public GeneratedTableDeclaration GetSingleTableDeclaration(string name)
		{
			List<GeneratedTableDeclaration> list;
			if (this.m_reconciledDeclarations.Count > 0 && this.m_reconciledDeclarations.TryGetValue(name, out list))
			{
				return list.Single(StringUtil.FormatInvariant("Expected only 1 table declaration for plan name '{0}'", new object[] { name }), Array.Empty<string>());
			}
			return this.m_tableDeclarations[name];
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000340E4 File Offset: 0x000322E4
		public List<GeneratedTableDeclaration> GetMultiTableDeclarations(string name)
		{
			List<GeneratedTableDeclaration> list;
			if (this.m_reconciledDeclarations.Count > 0 && this.m_reconciledDeclarations.TryGetValue(name, out list))
			{
				return list;
			}
			return this.m_multiTableDeclarations[name];
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003411D File Offset: 0x0003231D
		public GeneratedScalarDeclaration GetScalarDeclaration(string name)
		{
			return this.m_scalarDeclarations[name];
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003412C File Offset: 0x0003232C
		internal GeneratedTableDeclaration GetTableDeclarationByQueryName(string queryName)
		{
			foreach (GeneratedTableDeclaration generatedTableDeclaration in this.m_tableDeclarations.Values)
			{
				if (generatedTableDeclaration.QueryName == queryName)
				{
					return generatedTableDeclaration;
				}
			}
			throw new KeyNotFoundException("No declaration exists with specified query name");
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003419C File Offset: 0x0003239C
		public GeneratedEntityDeclaration GetEntityDeclaration(string name)
		{
			return this.m_entityDeclarations[name];
		}

		// Token: 0x04000641 RID: 1601
		protected Dictionary<string, GeneratedTableDeclaration> m_tableDeclarations;

		// Token: 0x04000642 RID: 1602
		protected Dictionary<string, List<GeneratedTableDeclaration>> m_multiTableDeclarations;

		// Token: 0x04000643 RID: 1603
		protected Dictionary<string, GeneratedScalarDeclaration> m_scalarDeclarations;

		// Token: 0x04000644 RID: 1604
		protected Dictionary<string, GeneratedEntityDeclaration> m_entityDeclarations;

		// Token: 0x04000645 RID: 1605
		protected Dictionary<string, List<GeneratedTableDeclaration>> m_reconciledDeclarations;
	}
}
