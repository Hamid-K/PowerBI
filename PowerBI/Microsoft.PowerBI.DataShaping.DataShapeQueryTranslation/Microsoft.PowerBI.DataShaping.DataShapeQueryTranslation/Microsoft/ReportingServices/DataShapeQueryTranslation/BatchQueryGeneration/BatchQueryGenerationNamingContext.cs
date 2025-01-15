using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200013E RID: 318
	internal sealed class BatchQueryGenerationNamingContext
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002F16E File Offset: 0x0002D36E
		internal BatchQueryGenerationNamingContext()
		{
			this.m_names = new HashSet<string>(QueryNamingContext.NameComparer);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002F186 File Offset: 0x0002D386
		public bool RegisterName(string name)
		{
			return this.m_names.Add(name);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002F194 File Offset: 0x0002D394
		public void RegisterNames(IEnumerable<string> names)
		{
			foreach (string text in names)
			{
				this.RegisterName(text);
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002F1E0 File Offset: 0x0002D3E0
		public string CreateAndRegisterName(QueryExpression expression, string fallbackName)
		{
			string text = expression.GetDefaultName();
			if (text == null)
			{
				text = fallbackName;
			}
			return this.CreateAndRegisterUniqueName(text);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002F200 File Offset: 0x0002D400
		public string CreateAndRegisterUniqueName(string candidate)
		{
			return QueryNamingContext.CreateAndRegisterUniqueName(this.m_names, candidate, null, null);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002F210 File Offset: 0x0002D410
		internal HashSet<string> RegisteredNames
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x040005F1 RID: 1521
		private readonly HashSet<string> m_names;
	}
}
