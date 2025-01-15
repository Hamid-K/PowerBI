using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000602 RID: 1538
	[Serializable]
	internal sealed class SortFilterEventInfo
	{
		// Token: 0x0600549B RID: 21659 RVA: 0x00162E3E File Offset: 0x0016103E
		internal SortFilterEventInfo()
		{
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00162E46 File Offset: 0x00161046
		internal SortFilterEventInfo(TextBox eventSource)
		{
			this.m_eventSource = eventSource;
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x0600549D RID: 21661 RVA: 0x00162E55 File Offset: 0x00161055
		// (set) Token: 0x0600549E RID: 21662 RVA: 0x00162E5D File Offset: 0x0016105D
		internal TextBox EventSource
		{
			get
			{
				return this.m_eventSource;
			}
			set
			{
				this.m_eventSource = value;
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x0600549F RID: 21663 RVA: 0x00162E66 File Offset: 0x00161066
		// (set) Token: 0x060054A0 RID: 21664 RVA: 0x00162E6E File Offset: 0x0016106E
		internal VariantList[] EventSourceScopeInfo
		{
			get
			{
				return this.m_eventSourceScopeInfo;
			}
			set
			{
				this.m_eventSourceScopeInfo = value;
			}
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x00162E78 File Offset: 0x00161078
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.EventSource, Token.Reference, ObjectType.TextBox),
				new MemberInfo(MemberName.EventSourceScopeInfo, Token.Array, ObjectType.VariantList)
			});
		}

		// Token: 0x04002D01 RID: 11521
		[Reference]
		private TextBox m_eventSource;

		// Token: 0x04002D02 RID: 11522
		private VariantList[] m_eventSourceScopeInfo;
	}
}
