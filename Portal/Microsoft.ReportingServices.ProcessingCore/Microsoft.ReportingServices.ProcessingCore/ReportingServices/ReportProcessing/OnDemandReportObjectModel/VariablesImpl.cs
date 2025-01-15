using System;
using System.Collections;
using System.Threading;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007C0 RID: 1984
	internal sealed class VariablesImpl : Variables
	{
		// Token: 0x06007062 RID: 28770 RVA: 0x001D4596 File Offset: 0x001D2796
		internal VariablesImpl(bool lockAdd)
		{
			this.m_lockAdd = lockAdd;
			this.m_collection = new Hashtable();
		}

		// Token: 0x1700264D RID: 9805
		public override Variable this[string key]
		{
			get
			{
				if (key == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingVariableReference(key);
				}
				Variable variable = this.m_collection[key] as Variable;
				if (variable == null)
				{
					throw new ReportProcessingException_NonExistingVariableReference(key);
				}
				return variable;
			}
		}

		// Token: 0x06007064 RID: 28772 RVA: 0x001D45EC File Offset: 0x001D27EC
		internal void Add(VariableImpl variable)
		{
			try
			{
				if (this.m_lockAdd)
				{
					Monitor.Enter(this.m_collection);
				}
				this.m_collection.Add(variable.Name, variable);
			}
			finally
			{
				if (this.m_lockAdd)
				{
					Monitor.Exit(this.m_collection);
				}
			}
		}

		// Token: 0x1700264E RID: 9806
		// (get) Token: 0x06007065 RID: 28773 RVA: 0x001D4644 File Offset: 0x001D2844
		// (set) Token: 0x06007066 RID: 28774 RVA: 0x001D464C File Offset: 0x001D284C
		internal Hashtable Collection
		{
			get
			{
				return this.m_collection;
			}
			set
			{
				this.m_collection = value;
			}
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x001D4658 File Offset: 0x001D2858
		internal void ResetAll()
		{
			foreach (object obj in this.m_collection.Values)
			{
				((VariableImpl)obj).Reset();
			}
		}

		// Token: 0x04003A0C RID: 14860
		private bool m_lockAdd;

		// Token: 0x04003A0D RID: 14861
		private Hashtable m_collection;
	}
}
