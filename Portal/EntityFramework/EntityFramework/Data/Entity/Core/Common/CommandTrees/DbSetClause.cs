using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006DE RID: 1758
	public sealed class DbSetClause : DbModificationClause
	{
		// Token: 0x06005177 RID: 20855 RVA: 0x001239B1 File Offset: 0x00121BB1
		internal DbSetClause(DbExpression targetProperty, DbExpression sourceValue)
		{
			this._prop = targetProperty;
			this._val = sourceValue;
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x001239C7 File Offset: 0x00121BC7
		public DbExpression Property
		{
			get
			{
				return this._prop;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x001239CF File Offset: 0x00121BCF
		public DbExpression Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x001239D8 File Offset: 0x00121BD8
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			dumper.Begin("DbSetClause");
			if (this.Property != null)
			{
				dumper.Dump(this.Property, "Property");
			}
			if (this.Value != null)
			{
				dumper.Dump(this.Value, "Value");
			}
			dumper.End("DbSetClause");
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x00123A30 File Offset: 0x00121C30
		internal override TreeNode Print(DbExpressionVisitor<TreeNode> visitor)
		{
			TreeNode treeNode = new TreeNode("DbSetClause", new TreeNode[0]);
			if (this.Property != null)
			{
				treeNode.Children.Add(new TreeNode("Property", new TreeNode[] { this.Property.Accept<TreeNode>(visitor) }));
			}
			if (this.Value != null)
			{
				treeNode.Children.Add(new TreeNode("Value", new TreeNode[] { this.Value.Accept<TreeNode>(visitor) }));
			}
			return treeNode;
		}

		// Token: 0x04001DC6 RID: 7622
		private readonly DbExpression _prop;

		// Token: 0x04001DC7 RID: 7623
		private readonly DbExpression _val;
	}
}
