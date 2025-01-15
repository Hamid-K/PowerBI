using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A3E RID: 6718
	public struct property : IProgramNodeBuilder, IEquatable<property>
	{
		// Token: 0x1700250D RID: 9485
		// (get) Token: 0x0600DD1A RID: 56602 RVA: 0x002F0812 File Offset: 0x002EEA12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD1B RID: 56603 RVA: 0x002F081A File Offset: 0x002EEA1A
		private property(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD1C RID: 56604 RVA: 0x002F0823 File Offset: 0x002EEA23
		public static property CreateUnsafe(ProgramNode node)
		{
			return new property(node);
		}

		// Token: 0x0600DD1D RID: 56605 RVA: 0x002F082C File Offset: 0x002EEA2C
		public static property? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.property)
			{
				return null;
			}
			return new property?(property.CreateUnsafe(node));
		}

		// Token: 0x0600DD1E RID: 56606 RVA: 0x002F0866 File Offset: 0x002EEA66
		public static property CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new property(new Hole(g.Symbol.property, holeId));
		}

		// Token: 0x0600DD1F RID: 56607 RVA: 0x002F087E File Offset: 0x002EEA7E
		public bool Is_Property(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Property;
		}

		// Token: 0x0600DD20 RID: 56608 RVA: 0x002F0898 File Offset: 0x002EEA98
		public bool Is_Property(GrammarBuilders g, out Property value)
		{
			if (this.Node.GrammarRule == g.Rule.Property)
			{
				value = Property.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Property);
			return false;
		}

		// Token: 0x0600DD21 RID: 56609 RVA: 0x002F08D0 File Offset: 0x002EEAD0
		public Property? As_Property(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Property)
			{
				return null;
			}
			return new Property?(Property.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD22 RID: 56610 RVA: 0x002F0910 File Offset: 0x002EEB10
		public Property Cast_Property(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Property)
			{
				return Property.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Property is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD23 RID: 56611 RVA: 0x002F0965 File Offset: 0x002EEB65
		public bool Is_SelectProperty(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectProperty;
		}

		// Token: 0x0600DD24 RID: 56612 RVA: 0x002F097F File Offset: 0x002EEB7F
		public bool Is_SelectProperty(GrammarBuilders g, out SelectProperty value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectProperty)
			{
				value = SelectProperty.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectProperty);
			return false;
		}

		// Token: 0x0600DD25 RID: 56613 RVA: 0x002F09B4 File Offset: 0x002EEBB4
		public SelectProperty? As_SelectProperty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectProperty)
			{
				return null;
			}
			return new SelectProperty?(SelectProperty.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD26 RID: 56614 RVA: 0x002F09F4 File Offset: 0x002EEBF4
		public SelectProperty Cast_SelectProperty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectProperty)
			{
				return SelectProperty.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectProperty is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD27 RID: 56615 RVA: 0x002F0A4C File Offset: 0x002EEC4C
		public T Switch<T>(GrammarBuilders g, Func<Property, T> func0, Func<SelectProperty, T> func1)
		{
			Property property;
			if (this.Is_Property(g, out property))
			{
				return func0(property);
			}
			SelectProperty selectProperty;
			if (this.Is_SelectProperty(g, out selectProperty))
			{
				return func1(selectProperty);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol property");
		}

		// Token: 0x0600DD28 RID: 56616 RVA: 0x002F0AA4 File Offset: 0x002EECA4
		public void Switch(GrammarBuilders g, Action<Property> func0, Action<SelectProperty> func1)
		{
			Property property;
			if (this.Is_Property(g, out property))
			{
				func0(property);
				return;
			}
			SelectProperty selectProperty;
			if (this.Is_SelectProperty(g, out selectProperty))
			{
				func1(selectProperty);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol property");
		}

		// Token: 0x0600DD29 RID: 56617 RVA: 0x002F0AFB File Offset: 0x002EECFB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD2A RID: 56618 RVA: 0x002F0B10 File Offset: 0x002EED10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD2B RID: 56619 RVA: 0x002F0B3A File Offset: 0x002EED3A
		public bool Equals(property other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400542F RID: 21551
		private ProgramNode _node;
	}
}
