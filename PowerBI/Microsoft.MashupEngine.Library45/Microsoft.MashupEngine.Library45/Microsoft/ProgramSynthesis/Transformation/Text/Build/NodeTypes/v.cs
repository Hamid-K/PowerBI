using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C3E RID: 7230
	public struct v : IProgramNodeBuilder, IEquatable<v>
	{
		// Token: 0x170028D4 RID: 10452
		// (get) Token: 0x0600F3D0 RID: 62416 RVA: 0x00343656 File Offset: 0x00341856
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F3D1 RID: 62417 RVA: 0x0034365E File Offset: 0x0034185E
		private v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F3D2 RID: 62418 RVA: 0x00343667 File Offset: 0x00341867
		public static v CreateUnsafe(ProgramNode node)
		{
			return new v(node);
		}

		// Token: 0x0600F3D3 RID: 62419 RVA: 0x00343670 File Offset: 0x00341870
		public static v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.v)
			{
				return null;
			}
			return new v?(v.CreateUnsafe(node));
		}

		// Token: 0x0600F3D4 RID: 62420 RVA: 0x003436AA File Offset: 0x003418AA
		public static v CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new v(new Hole(g.Symbol.v, holeId));
		}

		// Token: 0x0600F3D5 RID: 62421 RVA: 0x003436C2 File Offset: 0x003418C2
		public bool Is_ChooseInput(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ChooseInput;
		}

		// Token: 0x0600F3D6 RID: 62422 RVA: 0x003436DC File Offset: 0x003418DC
		public bool Is_ChooseInput(GrammarBuilders g, out ChooseInput value)
		{
			if (this.Node.GrammarRule == g.Rule.ChooseInput)
			{
				value = ChooseInput.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ChooseInput);
			return false;
		}

		// Token: 0x0600F3D7 RID: 62423 RVA: 0x00343714 File Offset: 0x00341914
		public ChooseInput? As_ChooseInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ChooseInput)
			{
				return null;
			}
			return new ChooseInput?(ChooseInput.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3D8 RID: 62424 RVA: 0x00343754 File Offset: 0x00341954
		public ChooseInput Cast_ChooseInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ChooseInput)
			{
				return ChooseInput.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ChooseInput is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3D9 RID: 62425 RVA: 0x003437A9 File Offset: 0x003419A9
		public bool Is_v_indexInputString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.v_indexInputString;
		}

		// Token: 0x0600F3DA RID: 62426 RVA: 0x003437C3 File Offset: 0x003419C3
		public bool Is_v_indexInputString(GrammarBuilders g, out v_indexInputString value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.v_indexInputString)
			{
				value = v_indexInputString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(v_indexInputString);
			return false;
		}

		// Token: 0x0600F3DB RID: 62427 RVA: 0x003437F8 File Offset: 0x003419F8
		public v_indexInputString? As_v_indexInputString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.v_indexInputString)
			{
				return null;
			}
			return new v_indexInputString?(v_indexInputString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3DC RID: 62428 RVA: 0x00343838 File Offset: 0x00341A38
		public v_indexInputString Cast_v_indexInputString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.v_indexInputString)
			{
				return v_indexInputString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_v_indexInputString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3DD RID: 62429 RVA: 0x00343890 File Offset: 0x00341A90
		public T Switch<T>(GrammarBuilders g, Func<ChooseInput, T> func0, Func<v_indexInputString, T> func1)
		{
			ChooseInput chooseInput;
			if (this.Is_ChooseInput(g, out chooseInput))
			{
				return func0(chooseInput);
			}
			v_indexInputString v_indexInputString;
			if (this.Is_v_indexInputString(g, out v_indexInputString))
			{
				return func1(v_indexInputString);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol v");
		}

		// Token: 0x0600F3DE RID: 62430 RVA: 0x003438E8 File Offset: 0x00341AE8
		public void Switch(GrammarBuilders g, Action<ChooseInput> func0, Action<v_indexInputString> func1)
		{
			ChooseInput chooseInput;
			if (this.Is_ChooseInput(g, out chooseInput))
			{
				func0(chooseInput);
				return;
			}
			v_indexInputString v_indexInputString;
			if (this.Is_v_indexInputString(g, out v_indexInputString))
			{
				func1(v_indexInputString);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol v");
		}

		// Token: 0x0600F3DF RID: 62431 RVA: 0x0034393F File Offset: 0x00341B3F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F3E0 RID: 62432 RVA: 0x00343954 File Offset: 0x00341B54
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F3E1 RID: 62433 RVA: 0x0034397E File Offset: 0x00341B7E
		public bool Equals(v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B2D RID: 23341
		private ProgramNode _node;
	}
}
