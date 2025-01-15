using System;

namespace dotless.Core.Parser.Infrastructure.Nodes
{
	// Token: 0x0200005C RID: 92
	public class BooleanNode : Node
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00014F1A File Offset: 0x0001311A
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00014F22 File Offset: 0x00013122
		public bool Value { get; set; }

		// Token: 0x06000405 RID: 1029 RVA: 0x00014F2B File Offset: 0x0001312B
		public BooleanNode(bool value)
		{
			this.Value = value;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00014F3A File Offset: 0x0001313A
		protected override Node CloneCore()
		{
			return new BooleanNode(this.Value);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00014F48 File Offset: 0x00013148
		public override string ToString()
		{
			return this.Value.ToString().ToLowerInvariant();
		}
	}
}
