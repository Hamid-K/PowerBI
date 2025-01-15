using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B9 RID: 1721
	[Serializable]
	internal sealed class ReportDrillthroughInfo
	{
		// Token: 0x06005CD0 RID: 23760 RVA: 0x0017A3B6 File Offset: 0x001785B6
		internal ReportDrillthroughInfo()
		{
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x0017A3BE File Offset: 0x001785BE
		// (set) Token: 0x06005CD2 RID: 23762 RVA: 0x0017A3C6 File Offset: 0x001785C6
		internal DrillthroughHashtable DrillthroughInformation
		{
			get
			{
				return this.m_drillthroughHashtable;
			}
			set
			{
				this.m_drillthroughHashtable = value;
			}
		}

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x06005CD3 RID: 23763 RVA: 0x0017A3CF File Offset: 0x001785CF
		// (set) Token: 0x06005CD4 RID: 23764 RVA: 0x0017A3D7 File Offset: 0x001785D7
		internal TokensHashtable RewrittenCommands
		{
			get
			{
				return this.m_rewrittenCommands;
			}
			set
			{
				this.m_rewrittenCommands = value;
			}
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x0017A3E0 File Offset: 0x001785E0
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.RewrittenCommands, ObjectType.TokensHashtable),
				new MemberInfo(MemberName.DrillthroughHashtable, ObjectType.DrillthroughHashtable)
			});
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x0017A426 File Offset: 0x00178626
		internal void AddDrillthrough(string drillthroughId, DrillthroughInformation drillthroughInfo)
		{
			if (this.m_drillthroughHashtable == null)
			{
				this.m_drillthroughHashtable = new DrillthroughHashtable();
			}
			this.m_drillthroughHashtable.Add(drillthroughId, drillthroughInfo);
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x0017A448 File Offset: 0x00178648
		internal void AddRewrittenCommand(int id, object value)
		{
			lock (this)
			{
				if (this.m_rewrittenCommands == null)
				{
					this.m_rewrittenCommands = new TokensHashtable();
				}
				if (!this.m_rewrittenCommands.ContainsKey(id))
				{
					this.m_rewrittenCommands.Add(id, value);
				}
			}
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x06005CD8 RID: 23768 RVA: 0x0017A4AC File Offset: 0x001786AC
		internal int Count
		{
			get
			{
				if (this.m_drillthroughHashtable == null)
				{
					return 0;
				}
				return this.m_drillthroughHashtable.Count;
			}
		}

		// Token: 0x04002F9A RID: 12186
		private TokensHashtable m_rewrittenCommands;

		// Token: 0x04002F9B RID: 12187
		private DrillthroughHashtable m_drillthroughHashtable;
	}
}
