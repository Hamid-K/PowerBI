using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000091 RID: 145
	public abstract class State : IEquatable<State>, ICachedEquatable<State>
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000BF20 File Offset: 0x0000A120
		protected bool ShouldComputeHashCode { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000334 RID: 820
		public abstract IEnumerable<KeyValuePair<Symbol, object>> Bindings { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000BF28 File Offset: 0x0000A128
		public Optional<object> FunctionalInput { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000336 RID: 822
		public abstract int Count { get; }

		// Token: 0x06000337 RID: 823
		public abstract bool TryGetValue(Symbol symbol, out object value);

		// Token: 0x06000338 RID: 824
		public abstract State Bind(Symbol symbol, object value);

		// Token: 0x06000339 RID: 825
		public abstract State Unbind(Symbol symbol);

		// Token: 0x0600033A RID: 826 RVA: 0x0000BF30 File Offset: 0x0000A130
		protected State(Optional<object> functionalInput, bool shouldComputeHash)
		{
			this.FunctionalInput = functionalInput;
			this.ShouldComputeHashCode = shouldComputeHash;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000BF48 File Offset: 0x0000A148
		public State BindFunctionalInput(Symbol symbol)
		{
			return this.WithoutFunctionalInput().Bind(symbol, this.FunctionalInput.Value);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BF70 File Offset: 0x0000A170
		public bool NonCachedEquals(State other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.Count != other.Count)
			{
				return false;
			}
			if (this.ShouldComputeHashCode != other.ShouldComputeHashCode)
			{
				return false;
			}
			if (this.FunctionalInput.HasValue != other.FunctionalInput.HasValue)
			{
				return false;
			}
			if (this.FunctionalInput.HasValue && !ValueEquality.Comparer.Equals(this.FunctionalInput.Value, other.FunctionalInput.Value))
			{
				return false;
			}
			foreach (KeyValuePair<Symbol, object> keyValuePair in this.Bindings)
			{
				object obj;
				if (!other.TryGetValue(keyValuePair.Key, out obj))
				{
					return false;
				}
				if (!ValueEquality.Comparer.Equals(keyValuePair.Value, obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C070 File Offset: 0x0000A270
		public virtual bool Equals(State other)
		{
			return CachedObjectEquality<State>.Instance.Equals(this, other);
		}

		// Token: 0x0600033E RID: 830
		public abstract State WithFunctionalInput(object functionalInput, bool replaceExisting = false);

		// Token: 0x0600033F RID: 831
		public abstract State WithoutFunctionalInput();

		// Token: 0x06000340 RID: 832 RVA: 0x0000C07E File Offset: 0x0000A27E
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj is State && this.Equals((State)obj)));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x17000117 RID: 279
		public object this[Symbol symbol]
		{
			get
			{
				object obj;
				if (this.TryGetValue(symbol, out obj))
				{
					return obj;
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C0CB File Offset: 0x0000A2CB
		public static State CreateForExecution(Symbol symbol, object value)
		{
			return State.Create(symbol, value, Optional<object>.Nothing, false);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C0DA File Offset: 0x0000A2DA
		public static State CreateForExecution(params KeyValuePair<Symbol, object>[] values)
		{
			return State.Create(values, Optional<object>.Nothing, false);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public static State CreateForLearning(Symbol symbol, object value)
		{
			return State.CreateForLearning(symbol, value, Optional<object>.Nothing);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		public static State CreateForLearning(Symbol symbol, object value, Optional<object> functionalInput)
		{
			return State.Create(symbol, value, functionalInput, true);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C101 File Offset: 0x0000A301
		private static State Create(Symbol symbol, object value, Optional<object> functionalInput, bool shouldComputeHash)
		{
			if (symbol.Grammar.FreeVariableHeight.OrElse(2147483647) < 8)
			{
				return new State.SmallState(new KeyValuePair<Symbol, object>(symbol, value), functionalInput, shouldComputeHash);
			}
			return new State.BigState(ImmutableDictionary.Create<Symbol, object>().Add(symbol, value), functionalInput, shouldComputeHash);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C13D File Offset: 0x0000A33D
		public static State CreateForLearning(IList<KeyValuePair<Symbol, object>> values)
		{
			return State.CreateForLearning(values, Optional<object>.Nothing);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C14A File Offset: 0x0000A34A
		public static State CreateForLearning(IList<KeyValuePair<Symbol, object>> values, Optional<object> functionalInput)
		{
			return State.Create(values, functionalInput, true);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C154 File Offset: 0x0000A354
		private static State Create(IList<KeyValuePair<Symbol, object>> values, Optional<object> functionalInput, bool shouldComputeHash)
		{
			if (values.Count > 0 && values[0].Key.Grammar.FreeVariableHeight.OrElse(2147483647) < 8)
			{
				return new State.SmallState(values, functionalInput, shouldComputeHash);
			}
			return new State.BigState(ImmutableDictionary.CreateRange<Symbol, object>(values), functionalInput, shouldComputeHash);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000C1A6 File Offset: 0x0000A3A6
		public static State CreateForLearning(params KeyValuePair<Symbol, object>[] values)
		{
			return State.CreateForLearning(values);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public State Substitute(Symbol oldSymbol, Symbol newSymbol)
		{
			object obj;
			if (!this.TryGetValue(oldSymbol, out obj))
			{
				return this;
			}
			return this.Unbind(oldSymbol).Bind(newSymbol, obj);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public override string ToString()
		{
			string text = "{{{0}, FunctionalInput := {1}}}";
			object[] array = new object[2];
			array[0] = string.Join(", ", this.Bindings.Select(delegate(KeyValuePair<Symbol, object> kvp)
			{
				Symbol key = kvp.Key;
				return ((key != null) ? key.ToString() : null) + " := " + kvp.Value.ToLiteral(null);
			}));
			array[1] = this.FunctionalInput.ToLiteral(null);
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C248 File Offset: 0x0000A448
		public XElement ToXML(Dictionary<object, int> identityCache = null)
		{
			IEnumerable<XElement> enumerable = this.Bindings.Select((KeyValuePair<Symbol, object> b) => new XElement("Binding", new XCData(b.Value.ToLiteral(identityCache))).WithAttribute("symbol", b.Key));
			return new XElement("State", enumerable.AppendItem(new XElement("FunctionalInput", new XCData(this.FunctionalInput.ToLiteral(null))))).WithAttribute("BindingsCount", this.Count);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C2C9 File Offset: 0x0000A4C9
		public XElement SerializeToXML(SpecSerializationContext serializers)
		{
			return this.SerializeToXML(new Dictionary<object, int>(), serializers);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C2D7 File Offset: 0x0000A4D7
		public XElement SerializeToXML(Dictionary<object, int> identityCache, SpecSerializationContext serializers)
		{
			return this.InternedSerialize(identityCache, serializers);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		private XElement InternedSerialize(Dictionary<object, int> identityCache, SpecSerializationContext serializers)
		{
			int num;
			if (identityCache.TryGetValue(this, out num))
			{
				return new XElement("Reference", num);
			}
			XElement xelement = this.SerializeImpl(identityCache, serializers).WithAttribute("ObjectID", identityCache.Count);
			identityCache[this] = identityCache.Count;
			return xelement;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C33C File Offset: 0x0000A53C
		private XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext serializers)
		{
			List<XElement> list = this.Bindings.Select((KeyValuePair<Symbol, object> b) => new XElement("Binding", serializers.SerializeObject(b.Value, identityCache)).WithAttribute("symbol", b.Key)).ToList<XElement>();
			if (this.FunctionalInput.HasValue)
			{
				XElement xelement = new XElement("FunctionalInput");
				xelement.Add(this.FunctionalInput.Select((object f) => serializers.SerializeObject(f, identityCache)));
				list.Add(xelement);
			}
			return new XElement("State", list);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C3D3 File Offset: 0x0000A5D3
		public static State DeserializeFromXML(XElement node, SpecSerializationContext serializers)
		{
			return State.DeserializeFromXML(node, serializers, new Dictionary<int, object>());
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
		public static State DeserializeFromXML(XElement node, SpecSerializationContext serializers, Dictionary<int, object> identityCache)
		{
			return State.InternedDeserialize(node, serializers, identityCache);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public static State InternedDeserialize(XElement node, SpecSerializationContext serializers, Dictionary<int, object> identityCache)
		{
			int num;
			if (node.Name == "Reference")
			{
				num = int.Parse(node.Value);
				return (State)identityCache[num];
			}
			State state = State.DeserializeImpl(node, serializers, identityCache);
			XAttribute xattribute = node.Attribute("ObjectID");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				throw new InvalidOperationException();
			}
			num = int.Parse(text);
			identityCache[num] = state;
			return state;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000C468 File Offset: 0x0000A668
		public static State DeserializeImpl(XElement node, SpecSerializationContext serializers, Dictionary<int, object> identityCache)
		{
			if (node.Name != "State")
			{
				throw new InvalidOperationException();
			}
			IList<KeyValuePair<Symbol, object>> list = node.Elements("Binding").Select(delegate(XElement bindingNode)
			{
				XAttribute xattribute = bindingNode.Attribute("symbol");
				string text = ((xattribute != null) ? xattribute.Value : null);
				if (text == null)
				{
					throw new InvalidOperationException();
				}
				string text2 = text;
				Symbol symbol = serializers.Grammar.Symbol(text2);
				object obj = serializers.DeserializeObject(bindingNode.Elements().Single<XElement>(), identityCache);
				return new KeyValuePair<Symbol, object>(symbol, obj);
			}).ToList<KeyValuePair<Symbol, object>>();
			XElement xelement = node.Elements("FunctionalInput").SingleOrDefault<XElement>();
			Optional<object> optional = Optional<object>.Nothing;
			if (xelement != null)
			{
				optional = serializers.DeserializeObject(xelement.Elements().Single<XElement>(), identityCache).Some<object>();
			}
			return State.CreateForLearning(list, optional);
		}

		// Token: 0x04000189 RID: 393
		private const int SmallStateThreshold = 8;

		// Token: 0x0400018A RID: 394
		private int _hashCode;

		// Token: 0x02000092 RID: 146
		private class BigState : State
		{
			// Token: 0x06000357 RID: 855 RVA: 0x0000C514 File Offset: 0x0000A714
			internal BigState(ImmutableDictionary<Symbol, object> bindings, Optional<object> functionalInput, bool shouldComputeHash)
				: base(functionalInput, shouldComputeHash)
			{
				this._bindings = bindings;
				this._hashCode = (shouldComputeHash ? MathUtils.OrderIndependentCombiner(bindings.OrderIndependentHashCode(KeyValueComparer<Symbol, object>.ValueEqualityInstance), ValueEquality.Comparer.GetHashCode(base.FunctionalInput)) : 0);
			}

			// Token: 0x06000358 RID: 856 RVA: 0x0000C561 File Offset: 0x0000A761
			private BigState(ImmutableDictionary<Symbol, object> bindings, Optional<object> functionalInput, int hashCode, bool shouldComputeHash)
				: base(functionalInput, shouldComputeHash)
			{
				this._bindings = bindings;
				this._hashCode = hashCode;
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0000C57A File Offset: 0x0000A77A
			public override IEnumerable<KeyValuePair<Symbol, object>> Bindings
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000C582 File Offset: 0x0000A782
			public override bool TryGetValue(Symbol symbol, out object value)
			{
				return this._bindings.TryGetValue(symbol, out value);
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0000C591 File Offset: 0x0000A791
			public override int Count
			{
				get
				{
					return this._bindings.Count;
				}
			}

			// Token: 0x0600035C RID: 860 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
			public override State Bind(Symbol symbol, object value)
			{
				KeyValuePair<Symbol, object> keyValuePair = new KeyValuePair<Symbol, object>(symbol, value);
				object obj;
				if (this.TryGetValue(symbol, out obj))
				{
					if (!base.ShouldComputeHashCode)
					{
						return new State.BigState(this._bindings.Remove(symbol).Add(symbol, value), base.FunctionalInput, base.ShouldComputeHashCode);
					}
					KeyValuePair<Symbol, object> keyValuePair2 = new KeyValuePair<Symbol, object>(symbol, obj);
					int hashCode = KeyValueComparer<Symbol, object>.ValueEqualityInstance.GetHashCode(keyValuePair2);
					int hashCode2 = KeyValueComparer<Symbol, object>.ValueEqualityInstance.GetHashCode(keyValuePair);
					int num = MathUtils.OrderIndependentCombiner(MathUtils.OrderIndependentCombinerInverse(this._hashCode, hashCode), hashCode2);
					return new State.BigState(this._bindings.Remove(symbol).Add(symbol, value), base.FunctionalInput, num, base.ShouldComputeHashCode);
				}
				else
				{
					if (!base.ShouldComputeHashCode)
					{
						return new State.BigState(this._bindings.Add(symbol, value), base.FunctionalInput, base.ShouldComputeHashCode);
					}
					int hashCode3 = KeyValueComparer<Symbol, object>.ValueEqualityInstance.GetHashCode(keyValuePair);
					int num2 = MathUtils.OrderIndependentCombiner(this._hashCode, hashCode3);
					return new State.BigState(this._bindings.Add(symbol, value), base.FunctionalInput, num2, base.ShouldComputeHashCode);
				}
			}

			// Token: 0x0600035D RID: 861 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
			public override State Unbind(Symbol symbol)
			{
				object obj;
				if (!this.TryGetValue(symbol, out obj))
				{
					return this;
				}
				if (!base.ShouldComputeHashCode)
				{
					return new State.BigState(this._bindings.Remove(symbol), base.FunctionalInput, base.ShouldComputeHashCode);
				}
				KeyValuePair<Symbol, object> keyValuePair = new KeyValuePair<Symbol, object>(symbol, obj);
				int hashCode = KeyValueComparer<Symbol, object>.ValueEqualityInstance.GetHashCode(keyValuePair);
				int num = MathUtils.OrderIndependentCombinerInverse(this._hashCode, hashCode);
				return new State.BigState(this._bindings.Remove(symbol), base.FunctionalInput, num, base.ShouldComputeHashCode);
			}

			// Token: 0x0600035E RID: 862 RVA: 0x0000C734 File Offset: 0x0000A934
			public override State WithFunctionalInput(object functionalInput, bool replaceExisting = false)
			{
				if (base.FunctionalInput.HasValue && !replaceExisting)
				{
					throw new InvalidOperationException("State already has a functional input.");
				}
				Optional<object> optional = functionalInput.Some<object>();
				if (!base.ShouldComputeHashCode)
				{
					return new State.BigState(this._bindings, optional, base.ShouldComputeHashCode);
				}
				int num = MathUtils.OrderIndependentCombiner(MathUtils.OrderIndependentCombinerInverse(this._hashCode, ValueEquality.Comparer.GetHashCode(base.FunctionalInput)), ValueEquality.Comparer.GetHashCode(optional));
				return new State.BigState(this._bindings, optional, num, base.ShouldComputeHashCode);
			}

			// Token: 0x0600035F RID: 863 RVA: 0x0000C7CC File Offset: 0x0000A9CC
			public override State WithoutFunctionalInput()
			{
				if (!base.FunctionalInput.HasValue)
				{
					return this;
				}
				Optional<object> nothing = Optional<object>.Nothing;
				if (!base.ShouldComputeHashCode)
				{
					return new State.BigState(this._bindings, nothing, base.ShouldComputeHashCode);
				}
				int num = MathUtils.OrderIndependentCombiner(MathUtils.OrderIndependentCombinerInverse(this._hashCode, ValueEquality.Comparer.GetHashCode(base.FunctionalInput)), ValueEquality.Comparer.GetHashCode(nothing));
				return new State.BigState(this._bindings, nothing, num, base.ShouldComputeHashCode);
			}

			// Token: 0x0400018D RID: 397
			private readonly ImmutableDictionary<Symbol, object> _bindings;
		}

		// Token: 0x02000093 RID: 147
		private class SmallState : State
		{
			// Token: 0x06000360 RID: 864 RVA: 0x0000C855 File Offset: 0x0000AA55
			internal SmallState(KeyValuePair<Symbol, object> binding, Optional<object> functionalInput, bool shouldComputeHash)
				: this(ImmutableList.Create<KeyValuePair<Symbol, object>>(binding), functionalInput, shouldComputeHash ? MathUtils.OrderIndependentCombiner(MathUtils.OrderIndependentHashCode<KeyValuePair<Symbol, object>>(binding, KeyValueComparer<Symbol, object>.ValueEqualityInstance), ValueEquality.Comparer.GetHashCode(functionalInput)) : 0, shouldComputeHash)
			{
			}

			// Token: 0x06000361 RID: 865 RVA: 0x0000C88B File Offset: 0x0000AA8B
			internal SmallState(IEnumerable<KeyValuePair<Symbol, object>> bindings, Optional<object> functionalInput, bool shouldComputeHash)
				: this((bindings as ImmutableList<KeyValuePair<Symbol, object>>) ?? ImmutableList.CreateRange<KeyValuePair<Symbol, object>>(bindings), functionalInput, shouldComputeHash)
			{
			}

			// Token: 0x06000362 RID: 866 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
			private SmallState(ImmutableList<KeyValuePair<Symbol, object>> bindings, Optional<object> functionalInput, bool shouldComputeHash)
				: base(functionalInput, shouldComputeHash)
			{
				this._bindings = bindings;
				this._hashCode = (shouldComputeHash ? MathUtils.OrderIndependentCombiner(this._bindings.OrderIndependentHashCode(KeyValueComparer<Symbol, object>.ValueEqualityInstance), ValueEquality.Comparer.GetHashCode(functionalInput)) : 0);
			}

			// Token: 0x06000363 RID: 867 RVA: 0x0000C8F5 File Offset: 0x0000AAF5
			private SmallState(ImmutableList<KeyValuePair<Symbol, object>> bindings, Optional<object> functionalInput, int hashCode, bool shouldComputeHash)
				: base(functionalInput, shouldComputeHash)
			{
				this._bindings = bindings;
				this._hashCode = hashCode;
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0000C90E File Offset: 0x0000AB0E
			public override int Count
			{
				get
				{
					return this._bindings.Count;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000C91B File Offset: 0x0000AB1B
			public override IEnumerable<KeyValuePair<Symbol, object>> Bindings
			{
				get
				{
					int num;
					for (int i = this.Count - 1; i >= 0; i = num - 1)
					{
						yield return this._bindings[i];
						num = i;
					}
					yield break;
				}
			}

			// Token: 0x06000366 RID: 870 RVA: 0x0000C92C File Offset: 0x0000AB2C
			public override bool TryGetValue(Symbol symbol, out object value)
			{
				for (int i = this.Count - 1; i >= 0; i--)
				{
					KeyValuePair<Symbol, object> keyValuePair = this._bindings[i];
					if (!(symbol != keyValuePair.Key))
					{
						value = keyValuePair.Value;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x06000367 RID: 871 RVA: 0x0000C978 File Offset: 0x0000AB78
			public override State Bind(Symbol symbol, object value)
			{
				KeyValuePair<Symbol, object> keyValuePair = new KeyValuePair<Symbol, object>(symbol, value);
				if (!base.ShouldComputeHashCode)
				{
					return new State.SmallState(this._bindings.Add(keyValuePair), base.FunctionalInput, base.ShouldComputeHashCode);
				}
				int hashCode = KeyValueComparer<Symbol, object>.ValueEqualityInstance.GetHashCode(keyValuePair);
				for (int i = this.Count - 1; i >= 0; i--)
				{
					KeyValuePair<Symbol, object> keyValuePair2 = this._bindings[i];
					if (!(keyValuePair2.Key != symbol))
					{
						int hashCode2 = KeyValueComparer<Symbol, object>.ValueEqualityInstance.GetHashCode(keyValuePair2);
						int num = MathUtils.OrderIndependentCombiner(MathUtils.OrderIndependentCombinerInverse(this._hashCode, hashCode2), hashCode);
						return new State.SmallState(this._bindings.SetItem(i, keyValuePair), base.FunctionalInput, num, base.ShouldComputeHashCode);
					}
				}
				return new State.SmallState(this._bindings.Add(keyValuePair), base.FunctionalInput, MathUtils.OrderIndependentCombiner(this._hashCode, hashCode), base.ShouldComputeHashCode);
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0000CA5C File Offset: 0x0000AC5C
			public override State Unbind(Symbol symbol)
			{
				int i = this.Count - 1;
				while (i >= 0)
				{
					KeyValuePair<Symbol, object> keyValuePair = this._bindings[i];
					if (!(keyValuePair.Key != symbol))
					{
						if (!base.ShouldComputeHashCode)
						{
							return new State.SmallState(this._bindings.RemoveAt(i), base.FunctionalInput, base.ShouldComputeHashCode);
						}
						int hashCode = KeyValueComparer<Symbol, object>.ValueEqualityInstance.GetHashCode(keyValuePair);
						int num = MathUtils.OrderIndependentCombinerInverse(this._hashCode, hashCode);
						return new State.SmallState(this._bindings.RemoveAt(i), base.FunctionalInput, num, base.ShouldComputeHashCode);
					}
					else
					{
						i--;
					}
				}
				return this;
			}

			// Token: 0x06000369 RID: 873 RVA: 0x0000CAFC File Offset: 0x0000ACFC
			public override State WithFunctionalInput(object functionalInput, bool replaceExisting = false)
			{
				if (base.FunctionalInput.HasValue && !replaceExisting)
				{
					throw new InvalidOperationException("State already has a functional input.");
				}
				Optional<object> optional = functionalInput.Some<object>();
				if (!base.ShouldComputeHashCode)
				{
					return new State.SmallState(this._bindings, optional, base.ShouldComputeHashCode);
				}
				int num = MathUtils.OrderIndependentCombiner(MathUtils.OrderIndependentCombinerInverse(this._hashCode, ValueEquality.Comparer.GetHashCode(base.FunctionalInput)), ValueEquality.Comparer.GetHashCode(optional));
				return new State.SmallState(this._bindings, optional, num, base.ShouldComputeHashCode);
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000CB94 File Offset: 0x0000AD94
			public override State WithoutFunctionalInput()
			{
				if (!base.FunctionalInput.HasValue)
				{
					return this;
				}
				Optional<object> nothing = Optional<object>.Nothing;
				if (!base.ShouldComputeHashCode)
				{
					return new State.SmallState(this._bindings, nothing, base.ShouldComputeHashCode);
				}
				int num = MathUtils.OrderIndependentCombiner(MathUtils.OrderIndependentCombinerInverse(this._hashCode, ValueEquality.Comparer.GetHashCode(base.FunctionalInput)), ValueEquality.Comparer.GetHashCode(nothing));
				return new State.SmallState(this._bindings, nothing, num, base.ShouldComputeHashCode);
			}

			// Token: 0x0400018E RID: 398
			private readonly ImmutableList<KeyValuePair<Symbol, object>> _bindings;
		}
	}
}
