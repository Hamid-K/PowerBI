using System;
using System.Collections.Generic;
using System.Data.Entity.Edm;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AF RID: 1199
	internal sealed class EdmModelValidationVisitor : EdmModelVisitor
	{
		// Token: 0x06003AD9 RID: 15065 RVA: 0x000C286F File Offset: 0x000C0A6F
		internal EdmModelValidationVisitor(EdmModelValidationContext context, EdmModelRuleSet ruleSet)
		{
			this._context = context;
			this._ruleSet = ruleSet;
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000C2890 File Offset: 0x000C0A90
		protected internal override void VisitMetadataItem(MetadataItem item)
		{
			if (this._visitedItems.Add(item))
			{
				this.EvaluateItem(item);
			}
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000C28A8 File Offset: 0x000C0AA8
		private void EvaluateItem(MetadataItem item)
		{
			foreach (DataModelValidationRule dataModelValidationRule in this._ruleSet.GetRules(item))
			{
				dataModelValidationRule.Evaluate(this._context, item);
			}
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000C2900 File Offset: 0x000C0B00
		internal void Visit(EdmModel model)
		{
			this.EvaluateItem(model);
			this.VisitEdmModel(model);
		}

		// Token: 0x04001468 RID: 5224
		private readonly EdmModelValidationContext _context;

		// Token: 0x04001469 RID: 5225
		private readonly EdmModelRuleSet _ruleSet;

		// Token: 0x0400146A RID: 5226
		private readonly HashSet<MetadataItem> _visitedItems = new HashSet<MetadataItem>();
	}
}
