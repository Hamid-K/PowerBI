using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003CC RID: 972
	internal sealed class PatternMatchRule : Rule
	{
		// Token: 0x06002E90 RID: 11920 RVA: 0x00094AF3 File Offset: 0x00092CF3
		internal PatternMatchRule(Node pattern, Rule.ProcessNodeDelegate processDelegate)
			: base(pattern.Op.OpType, processDelegate)
		{
			this.m_pattern = pattern;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x00094B10 File Offset: 0x00092D10
		private bool Match(Node pattern, Node original)
		{
			if (pattern.Op.OpType == OpType.Leaf)
			{
				return true;
			}
			if (pattern.Op.OpType != original.Op.OpType)
			{
				return false;
			}
			if (pattern.Children.Count != original.Children.Count)
			{
				return false;
			}
			for (int i = 0; i < pattern.Children.Count; i++)
			{
				if (!this.Match(pattern.Children[i], original.Children[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x00094B9B File Offset: 0x00092D9B
		internal override bool Match(Node node)
		{
			return this.Match(this.m_pattern, node);
		}

		// Token: 0x04000FB5 RID: 4021
		private readonly Node m_pattern;
	}
}
