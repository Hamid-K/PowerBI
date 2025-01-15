using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003DB RID: 987
	internal abstract class RuleProcessingContext
	{
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x0009505B File Offset: 0x0009325B
		internal Command Command
		{
			get
			{
				return this.m_command;
			}
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x00095063 File Offset: 0x00093263
		internal virtual void PreProcess(Node node)
		{
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x00095065 File Offset: 0x00093265
		internal virtual void PreProcessSubTree(Node node)
		{
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x00095067 File Offset: 0x00093267
		internal virtual void PostProcess(Node node, Rule rule)
		{
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x00095069 File Offset: 0x00093269
		internal virtual void PostProcessSubTree(Node node)
		{
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x0009506B File Offset: 0x0009326B
		internal virtual int GetHashCode(Node node)
		{
			return node.GetHashCode();
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x00095073 File Offset: 0x00093273
		internal RuleProcessingContext(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x04000FCE RID: 4046
		private readonly Command m_command;
	}
}
