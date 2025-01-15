using System;
using System.Collections.Generic;
using Microsoft.DataShaping;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000156 RID: 342
	internal sealed class WritableGeneratedDeclarationCollection : GeneratedDeclarationCollection
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x000341B9 File Offset: 0x000323B9
		public void AddSingleTableDeclaration(GeneratedTableDeclaration declaration)
		{
			this.m_tableDeclarations.Add(declaration.PlanName, declaration);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000341CD File Offset: 0x000323CD
		public void AddMultiTableDeclaration(GeneratedTableDeclaration declaration)
		{
			this.m_multiTableDeclarations.Add(declaration.PlanName, declaration);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000341E1 File Offset: 0x000323E1
		public void AddScalarDeclaration(GeneratedScalarDeclaration declaration)
		{
			this.m_scalarDeclarations.Add(declaration.PlanName, declaration);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000341F5 File Offset: 0x000323F5
		public void AddReconciledDeclaration(GeneratedTableDeclaration declaration)
		{
			this.m_reconciledDeclarations.Add(declaration.PlanName, declaration);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0003420C File Offset: 0x0003240C
		public bool TryReconcileAgainstExistingDeclaration(string planName, GeneratedTable table)
		{
			GeneratedTableDeclaration existingEquivalentDeclaration = this.GetExistingEquivalentDeclaration(table);
			if (existingEquivalentDeclaration == null)
			{
				return false;
			}
			this.m_reconciledDeclarations.Add(planName, existingEquivalentDeclaration);
			return true;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00034234 File Offset: 0x00032434
		private GeneratedTableDeclaration GetExistingEquivalentDeclaration(GeneratedTable table)
		{
			foreach (KeyValuePair<string, List<GeneratedTableDeclaration>> keyValuePair in this.m_multiTableDeclarations)
			{
				foreach (GeneratedTableDeclaration generatedTableDeclaration in keyValuePair.Value)
				{
					if (table.IsQueryExpressionEquivalent(generatedTableDeclaration.OriginalTable))
					{
						return generatedTableDeclaration;
					}
				}
			}
			foreach (KeyValuePair<string, GeneratedTableDeclaration> keyValuePair2 in this.m_tableDeclarations)
			{
				if (table.IsQueryExpressionEquivalent(keyValuePair2.Value.OriginalTable))
				{
					return keyValuePair2.Value;
				}
			}
			return null;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00034334 File Offset: 0x00032534
		public void AddEntityDeclaration(GeneratedEntityDeclaration declaration)
		{
			this.m_entityDeclarations.Add(declaration.PlanName, declaration);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00034348 File Offset: 0x00032548
		private bool HasDeclaration(string name)
		{
			return this.m_tableDeclarations.ContainsKey(name) || this.m_scalarDeclarations.ContainsKey(name) || this.m_entityDeclarations.ContainsKey(name);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00034374 File Offset: 0x00032574
		public ReadOnlyGeneratedDeclarationCollection AsReadOnly()
		{
			return new ReadOnlyGeneratedDeclarationCollection(this.m_tableDeclarations, this.m_multiTableDeclarations, this.m_scalarDeclarations, this.m_entityDeclarations, this.m_reconciledDeclarations);
		}
	}
}
