using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000499 RID: 1177
	internal abstract class DataModelValidationRuleSet
	{
		// Token: 0x06003A06 RID: 14854 RVA: 0x000C008F File Offset: 0x000BE28F
		protected void AddRule(DataModelValidationRule rule)
		{
			this._rules.Add(rule);
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000C009D File Offset: 0x000BE29D
		protected void RemoveRule(DataModelValidationRule rule)
		{
			this._rules.Remove(rule);
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x000C00AC File Offset: 0x000BE2AC
		internal IEnumerable<DataModelValidationRule> GetRules(MetadataItem itemToValidate)
		{
			return this._rules.Where((DataModelValidationRule r) => r.ValidatedType.IsInstanceOfType(itemToValidate));
		}

		// Token: 0x0400135D RID: 4957
		private readonly List<DataModelValidationRule> _rules = new List<DataModelValidationRule>();
	}
}
