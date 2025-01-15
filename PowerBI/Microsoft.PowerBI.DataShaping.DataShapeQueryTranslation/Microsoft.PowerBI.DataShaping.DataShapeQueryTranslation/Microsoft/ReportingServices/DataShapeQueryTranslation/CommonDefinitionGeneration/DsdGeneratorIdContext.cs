using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration
{
	// Token: 0x02000122 RID: 290
	internal sealed class DsdGeneratorIdContext
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002AFD1 File Offset: 0x000291D1
		internal DsdGeneratorIdContext()
		{
			this.m_usedIds = new HashSet<string>(StringComparer.Ordinal);
			this.m_idsByItem = new Dictionary<object, string>();
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002AFF4 File Offset: 0x000291F4
		public string MakeUniqueId(string candidate)
		{
			while (!this.m_usedIds.Add(candidate))
			{
				candidate = StringUtil.IncrementDigitSuffix(candidate);
			}
			return candidate;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002B00F File Offset: 0x0002920F
		public string MakeUniqueId(IIdentifiable item)
		{
			return this.MakeUniqueId(item.Id.Value, item);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002B023 File Offset: 0x00029223
		public string MakeUniqueId(string candidate, object item)
		{
			candidate = this.MakeUniqueId(candidate);
			this.m_idsByItem.Add(item, candidate);
			return candidate;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002B03C File Offset: 0x0002923C
		public bool RegisterUniqueId(IIdentifiable item)
		{
			return this.RegisterUniqueId(item.Id.Value, item);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002B050 File Offset: 0x00029250
		public bool RegisterUniqueId(string candidate, object item)
		{
			if (this.m_usedIds.Add(candidate))
			{
				this.m_idsByItem.Add(item, candidate);
				return true;
			}
			return false;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002B070 File Offset: 0x00029270
		public bool TryGetId(object item, out string id)
		{
			return this.m_idsByItem.TryGetValue(item, out id);
		}

		// Token: 0x04000593 RID: 1427
		private readonly HashSet<string> m_usedIds;

		// Token: 0x04000594 RID: 1428
		private readonly Dictionary<object, string> m_idsByItem;
	}
}
