using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F3D RID: 3901
	public struct records : IProgramNodeBuilder, IEquatable<records>
	{
		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06006C69 RID: 27753 RVA: 0x00162EEE File Offset: 0x001610EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C6A RID: 27754 RVA: 0x00162EF6 File Offset: 0x001610F6
		private records(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C6B RID: 27755 RVA: 0x00162EFF File Offset: 0x001610FF
		public static records CreateUnsafe(ProgramNode node)
		{
			return new records(node);
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x00162F08 File Offset: 0x00161108
		public static records? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.records)
			{
				return null;
			}
			return new records?(records.CreateUnsafe(node));
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x00162F42 File Offset: 0x00161142
		public static records CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new records(new Hole(g.Symbol.records, holeId));
		}

		// Token: 0x06006C6E RID: 27758 RVA: 0x00162F5A File Offset: 0x0016115A
		public bool Is_records_skip(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.records_skip;
		}

		// Token: 0x06006C6F RID: 27759 RVA: 0x00162F74 File Offset: 0x00161174
		public bool Is_records_skip(GrammarBuilders g, out records_skip value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.records_skip)
			{
				value = records_skip.CreateUnsafe(this.Node);
				return true;
			}
			value = default(records_skip);
			return false;
		}

		// Token: 0x06006C70 RID: 27760 RVA: 0x00162FAC File Offset: 0x001611AC
		public records_skip? As_records_skip(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.records_skip)
			{
				return null;
			}
			return new records_skip?(records_skip.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C71 RID: 27761 RVA: 0x00162FEC File Offset: 0x001611EC
		public records_skip Cast_records_skip(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.records_skip)
			{
				return records_skip.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_records_skip is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x00163041 File Offset: 0x00161241
		public bool Is_Select(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Select;
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x0016305B File Offset: 0x0016125B
		public bool Is_Select(GrammarBuilders g, out Select value)
		{
			if (this.Node.GrammarRule == g.Rule.Select)
			{
				value = Select.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Select);
			return false;
		}

		// Token: 0x06006C74 RID: 27764 RVA: 0x00163090 File Offset: 0x00161290
		public Select? As_Select(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Select)
			{
				return null;
			}
			return new Select?(Select.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C75 RID: 27765 RVA: 0x001630D0 File Offset: 0x001612D0
		public Select Cast_Select(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Select)
			{
				return Select.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Select is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C76 RID: 27766 RVA: 0x00163125 File Offset: 0x00161325
		public bool Is_Group(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Group;
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x0016313F File Offset: 0x0016133F
		public bool Is_Group(GrammarBuilders g, out Group value)
		{
			if (this.Node.GrammarRule == g.Rule.Group)
			{
				value = Group.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Group);
			return false;
		}

		// Token: 0x06006C78 RID: 27768 RVA: 0x00163174 File Offset: 0x00161374
		public Group? As_Group(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Group)
			{
				return null;
			}
			return new Group?(Group.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x001631B4 File Offset: 0x001613B4
		public Group Cast_Group(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Group)
			{
				return Group.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Group is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C7A RID: 27770 RVA: 0x00163209 File Offset: 0x00161409
		public bool Is_MergeEvery(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MergeEvery;
		}

		// Token: 0x06006C7B RID: 27771 RVA: 0x00163223 File Offset: 0x00161423
		public bool Is_MergeEvery(GrammarBuilders g, out MergeEvery value)
		{
			if (this.Node.GrammarRule == g.Rule.MergeEvery)
			{
				value = MergeEvery.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MergeEvery);
			return false;
		}

		// Token: 0x06006C7C RID: 27772 RVA: 0x00163258 File Offset: 0x00161458
		public MergeEvery? As_MergeEvery(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MergeEvery)
			{
				return null;
			}
			return new MergeEvery?(MergeEvery.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C7D RID: 27773 RVA: 0x00163298 File Offset: 0x00161498
		public MergeEvery Cast_MergeEvery(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MergeEvery)
			{
				return MergeEvery.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MergeEvery is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C7E RID: 27774 RVA: 0x001632F0 File Offset: 0x001614F0
		public T Switch<T>(GrammarBuilders g, Func<records_skip, T> func0, Func<Select, T> func1, Func<Group, T> func2, Func<MergeEvery, T> func3)
		{
			records_skip records_skip;
			if (this.Is_records_skip(g, out records_skip))
			{
				return func0(records_skip);
			}
			Select select;
			if (this.Is_Select(g, out select))
			{
				return func1(select);
			}
			Group group;
			if (this.Is_Group(g, out group))
			{
				return func2(group);
			}
			MergeEvery mergeEvery;
			if (this.Is_MergeEvery(g, out mergeEvery))
			{
				return func3(mergeEvery);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol records");
		}

		// Token: 0x06006C7F RID: 27775 RVA: 0x00163370 File Offset: 0x00161570
		public void Switch(GrammarBuilders g, Action<records_skip> func0, Action<Select> func1, Action<Group> func2, Action<MergeEvery> func3)
		{
			records_skip records_skip;
			if (this.Is_records_skip(g, out records_skip))
			{
				func0(records_skip);
				return;
			}
			Select select;
			if (this.Is_Select(g, out select))
			{
				func1(select);
				return;
			}
			Group group;
			if (this.Is_Group(g, out group))
			{
				func2(group);
				return;
			}
			MergeEvery mergeEvery;
			if (this.Is_MergeEvery(g, out mergeEvery))
			{
				func3(mergeEvery);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol records");
		}

		// Token: 0x06006C80 RID: 27776 RVA: 0x001633EF File Offset: 0x001615EF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C81 RID: 27777 RVA: 0x00163404 File Offset: 0x00161604
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C82 RID: 27778 RVA: 0x0016342E File Offset: 0x0016162E
		public bool Equals(records other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F28 RID: 12072
		private ProgramNode _node;
	}
}
