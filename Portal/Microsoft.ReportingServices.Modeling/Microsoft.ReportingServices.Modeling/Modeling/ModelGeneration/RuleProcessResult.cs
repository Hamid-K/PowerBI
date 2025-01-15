using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F5 RID: 245
	public sealed class RuleProcessResult
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x00028DFE File Offset: 0x00026FFE
		public RuleProcessResult(bool success, ModelItem itemCreated, ModelItem itemModified, string message)
		{
			ModelItem[] array;
			if (itemCreated != null)
			{
				(array = new ModelItem[1])[0] = itemCreated;
			}
			else
			{
				array = null;
			}
			ModelItem[] array2;
			if (itemModified != null)
			{
				(array2 = new ModelItem[1])[0] = itemModified;
			}
			else
			{
				array2 = null;
			}
			this..ctor(success, array, array2, new string[] { message });
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00028E34 File Offset: 0x00027034
		public RuleProcessResult(bool success, ICollection<ModelItem> itemsCreated, ICollection<ModelItem> itemsModified, params string[] messages)
		{
			this.m_success = success;
			if (itemsCreated != null)
			{
				this.m_itemsCreated = new ReadOnlyCollection<ModelItem>(ArrayUtil.ToArray<ModelItem>(itemsCreated));
			}
			else
			{
				this.m_itemsCreated = RuleProcessResult.EmptyModelItemCollection;
			}
			if (itemsModified != null)
			{
				this.m_itemsModified = new ReadOnlyCollection<ModelItem>(ArrayUtil.ToArray<ModelItem>(itemsModified));
			}
			else
			{
				this.m_itemsModified = RuleProcessResult.EmptyModelItemCollection;
			}
			this.m_messages = new ReadOnlyCollection<string>(messages);
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00028E9D File Offset: 0x0002709D
		public bool Success
		{
			get
			{
				return this.m_success;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00028EA5 File Offset: 0x000270A5
		public ReadOnlyCollection<ModelItem> ItemsCreated
		{
			get
			{
				return this.m_itemsCreated;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00028EAD File Offset: 0x000270AD
		public ReadOnlyCollection<ModelItem> ItemsModified
		{
			get
			{
				return this.m_itemsModified;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00028EB5 File Offset: 0x000270B5
		public ReadOnlyCollection<string> Messages
		{
			get
			{
				return this.m_messages;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00028EBD File Offset: 0x000270BD
		public bool Skipped
		{
			get
			{
				return this.m_success && this.m_itemsCreated.Count == 0 && this.m_itemsModified.Count == 0;
			}
		}

		// Token: 0x04000511 RID: 1297
		private static ReadOnlyCollection<ModelItem> EmptyModelItemCollection = new ReadOnlyCollection<ModelItem>(new ModelItem[0]);

		// Token: 0x04000512 RID: 1298
		private bool m_success;

		// Token: 0x04000513 RID: 1299
		private ReadOnlyCollection<ModelItem> m_itemsCreated;

		// Token: 0x04000514 RID: 1300
		private ReadOnlyCollection<ModelItem> m_itemsModified;

		// Token: 0x04000515 RID: 1301
		private ReadOnlyCollection<string> m_messages;
	}
}
