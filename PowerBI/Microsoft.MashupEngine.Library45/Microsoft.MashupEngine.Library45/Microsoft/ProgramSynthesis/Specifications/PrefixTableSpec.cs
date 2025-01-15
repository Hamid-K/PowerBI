using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000363 RID: 867
	public class PrefixTableSpec : Spec
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x00037D3C File Offset: 0x00035F3C
		public PrefixTableSpec(State input, ITable<object> example)
			: base(new State[] { input }, true)
		{
			Dictionary<State, ITable<object>> dictionary = new Dictionary<State, ITable<object>>();
			dictionary[input] = example;
			this.PrefixTables = dictionary;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00037D70 File Offset: 0x00035F70
		public PrefixTableSpec(IDictionary<State, ITable<object>> dict)
			: base(dict.Keys, true)
		{
			this.PrefixTables = dict.ToDictionary((KeyValuePair<State, ITable<object>> kvp) => kvp.Key, (KeyValuePair<State, ITable<object>> kvp) => kvp.Value);
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00037DD4 File Offset: 0x00035FD4
		public IDictionary<State, ITable<object>> PrefixTables { get; }

		// Token: 0x0600131B RID: 4891 RVA: 0x00037DDC File Offset: 0x00035FDC
		protected override bool CorrectOnProvided(State state, object output)
		{
			if (output != null && !(output is Bottom))
			{
				ITable<object> table = output as ITable<object>;
				if (table != null)
				{
					ITable<object> table2 = this.PrefixTables[state];
					if (table2.ColumnNames.SequenceEqual(table.ColumnNames))
					{
						return table2.ZipWith(output.ToEnumerable<object>()).All2((IEnumerable<object> a, object b) => a.SequenceEqual(b.ToEnumerable<object>()));
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00037E54 File Offset: 0x00036054
		protected override bool EqualsOnInput(State state, Spec other)
		{
			PrefixTableSpec prefixTableSpec = other as PrefixTableSpec;
			if (prefixTableSpec == null)
			{
				return false;
			}
			ITable<object> table = this.PrefixTables[state];
			ITable<object> table2 = prefixTableSpec.PrefixTables[state];
			return table.ColumnNames.SequenceEqual(table2.ColumnNames) && table.SequenceEqual(table2, SequenceEquality<object>.Comparer);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00037EA8 File Offset: 0x000360A8
		protected override int GetHashCodeOnInput(State state)
		{
			return ValueEquality.Comparer.GetHashCode(this.PrefixTables[state]);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00037EC0 File Offset: 0x000360C0
		public override string ToString()
		{
			return this.PrefixTables.Select((KeyValuePair<State, ITable<object>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> ? ⊃ {1}", new object[]
			{
				kvp.Key,
				kvp.Value.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
			}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00037F10 File Offset: 0x00036110
		protected internal override Spec BottomToNull()
		{
			if (this.PrefixTables.Values.All((ITable<object> e) => e.All((IEnumerable<object> row) => !row.Contains(Bottom.Value))))
			{
				return this;
			}
			return new PrefixTableSpec(this.PrefixTables.ToDictionary((KeyValuePair<State, ITable<object>> kvp) => kvp.Key, (KeyValuePair<State, ITable<object>> kvp) => new Table<object>(kvp.Value.ColumnNames, kvp.Value.Rows.Select(delegate(IEnumerable<object> row)
			{
				Func<object, object> func;
				if ((func = PrefixTableSpec.<>O.<0>__BottomToNull) == null)
				{
					func = (PrefixTableSpec.<>O.<0>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
				}
				return row.Select(func).ToList<object>();
			}), null)));
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00037FA0 File Offset: 0x000361A0
		protected internal override Spec NullToBottom()
		{
			if (this.PrefixTables.Values.All((ITable<object> e) => e.All((IEnumerable<object> row) => !row.Contains(null))))
			{
				return this;
			}
			return new PrefixTableSpec(this.PrefixTables.ToDictionary((KeyValuePair<State, ITable<object>> kvp) => kvp.Key, (KeyValuePair<State, ITable<object>> kvp) => new Table<object>(kvp.Value.ColumnNames, kvp.Value.Rows.Select(delegate(IEnumerable<object> row)
			{
				Func<object, object> func;
				if ((func = PrefixTableSpec.<>O.<1>__NullToBottom) == null)
				{
					func = (PrefixTableSpec.<>O.<1>__NullToBottom = new Func<object, object>(ObjectUtils.NullToBottom));
				}
				return row.Select(func).ToList<object>();
			}), null)));
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0003802E File Offset: 0x0003622E
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("Subtable", this.PrefixTables[input].CollectionToXML("Examples", "Item", ObjectFormatting.ToString, identityCache, Array.Empty<Func<IEnumerable<object>, XAttribute>>()));
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00038064 File Offset: 0x00036264
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new PrefixTableSpec(this.PrefixTables.ToDictionary((KeyValuePair<State, ITable<object>> kvp) => transformer(kvp.Key), (KeyValuePair<State, ITable<object>> kvp) => kvp.Value));
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000380BC File Offset: 0x000362BC
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			Func<IEnumerable<object>, XElement> <>9__1;
			return new XElement("PrefixTableSpec", new XElement("Examples", this.PrefixTables.Select(delegate(KeyValuePair<State, ITable<object>> kvp)
			{
				XName xname = "Example";
				object[] array = new object[2];
				array[0] = kvp.Key.SerializeToXML(identityCache, context);
				int num = 1;
				XName xname2 = "Output";
				IEnumerable<IEnumerable<object>> value = kvp.Value;
				Func<IEnumerable<object>, XElement> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IEnumerable<object> v) => context.SerializeObject(v, identityCache));
				}
				array[num] = new XElement(xname2, value.Select(func));
				return new XElement(xname, array);
			})));
		}

		// Token: 0x02000364 RID: 868
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000982 RID: 2434
			public static Func<object, object> <0>__BottomToNull;

			// Token: 0x04000983 RID: 2435
			public static Func<object, object> <1>__NullToBottom;
		}
	}
}
