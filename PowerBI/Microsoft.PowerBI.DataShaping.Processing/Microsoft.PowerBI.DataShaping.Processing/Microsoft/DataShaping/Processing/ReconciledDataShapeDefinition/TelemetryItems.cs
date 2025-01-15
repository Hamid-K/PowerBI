using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000054 RID: 84
	internal sealed class TelemetryItems
	{
		// Token: 0x0600021B RID: 539 RVA: 0x000063C1 File Offset: 0x000045C1
		internal TelemetryItems()
		{
			this._items = new List<TelemetryItem>();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000063D4 File Offset: 0x000045D4
		internal void Add(string id, ExpressionNode node)
		{
			this._items.Add(new TelemetryItem(id, node));
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000063E8 File Offset: 0x000045E8
		internal Dictionary<string, object> Evaluate(IExpressionEvaluator<object> evaluator)
		{
			if (this._items == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (TelemetryItem telemetryItem in this._items)
			{
				dictionary.Add(telemetryItem.Name, evaluator.Evaluate(telemetryItem.Expression));
			}
			return dictionary;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006458 File Offset: 0x00004658
		internal IList<TelemetryItem> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x0400014A RID: 330
		private const string KeyValueSeparator = ":";

		// Token: 0x0400014B RID: 331
		private const string EntriesSeparator = ",";

		// Token: 0x0400014C RID: 332
		private readonly IList<TelemetryItem> _items;
	}
}
