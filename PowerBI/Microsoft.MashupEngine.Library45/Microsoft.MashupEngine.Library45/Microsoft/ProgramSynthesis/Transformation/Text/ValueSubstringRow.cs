using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BBE RID: 7102
	[JsonObject(MemberSerialization.OptIn)]
	internal class ValueSubstringRow : IRow, IEquatable<IRow>
	{
		// Token: 0x0600E880 RID: 59520 RVA: 0x00314CA7 File Offset: 0x00312EA7
		private ValueSubstringRow(IImmutableDictionary<string, object> values)
		{
			this.Values = values;
			this.hashCode = this.Values.OrderIndependentKeyValueHashCode<string, object>();
		}

		// Token: 0x0600E881 RID: 59521 RVA: 0x00314CC8 File Offset: 0x00312EC8
		[JsonConstructor]
		public ValueSubstringRow(IDictionary<string, object> values)
			: this(values.ToImmutableDictionary((KeyValuePair<string, object> kv) => kv.Key, delegate(KeyValuePair<string, object> kv)
			{
				if (!(kv.Value is string))
				{
					return kv.Value;
				}
				return ValueSubstring.Create((string)kv.Value, null, null, null, null);
			}))
		{
		}

		// Token: 0x0600E882 RID: 59522 RVA: 0x00314D20 File Offset: 0x00312F20
		public ValueSubstringRow(IDictionary<string, string> values)
			: this(values.ToImmutableDictionary((KeyValuePair<string, string> kv) => kv.Key, (KeyValuePair<string, string> kv) => ValueSubstring.Create(kv.Value, null, null, null, null)))
		{
		}

		// Token: 0x0600E883 RID: 59523 RVA: 0x00314D78 File Offset: 0x00312F78
		public ValueSubstringRow(IDictionary<string, ValueSubstring> values)
			: this(values.ToImmutableDictionary((KeyValuePair<string, ValueSubstring> kv) => kv.Key, (KeyValuePair<string, ValueSubstring> kv) => kv.Value))
		{
		}

		// Token: 0x0600E884 RID: 59524 RVA: 0x00314DD0 File Offset: 0x00312FD0
		public ValueSubstringRow(IRow row, IEnumerable<string> columns = null, IReadOnlyDictionary<string, Token> allowedTokens = null)
			: this((columns ?? row.ColumnNames).ToImmutableDictionary((string columnName) => columnName, delegate(string columnName)
			{
				object obj;
				if (!row.TryGetValue(columnName, out obj))
				{
					return null;
				}
				if (obj is string || obj is ValueSubstring)
				{
					string text = (obj as string) ?? obj.ToString();
					IReadOnlyDictionary<string, Token> allowedTokens2 = allowedTokens;
					return ValueSubstring.Create(text, null, null, null, allowedTokens2);
				}
				return obj;
			}))
		{
		}

		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x0600E885 RID: 59525 RVA: 0x00314E38 File Offset: 0x00313038
		[JsonProperty(PropertyName = "Values")]
		private IDictionary<string, object> ValuesAsIDictionary
		{
			get
			{
				return this.Values.ToDictionary((KeyValuePair<string, object> kv) => kv.Key, delegate(KeyValuePair<string, object> kv)
				{
					ValueSubstring valueSubstring = kv.Value as ValueSubstring;
					return ((valueSubstring != null) ? valueSubstring.Value : null) ?? kv.Value;
				});
			}
		}

		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x0600E886 RID: 59526 RVA: 0x00314E8E File Offset: 0x0031308E
		private IImmutableDictionary<string, object> Values { get; }

		// Token: 0x0600E887 RID: 59527 RVA: 0x00314E96 File Offset: 0x00313096
		public bool Equals(IRow other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ValueSubstringRow)other)));
		}

		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x0600E888 RID: 59528 RVA: 0x00314EC4 File Offset: 0x003130C4
		public IEnumerable<string> ColumnNames
		{
			get
			{
				return this.Values.Keys;
			}
		}

		// Token: 0x0600E889 RID: 59529 RVA: 0x00314ED4 File Offset: 0x003130D4
		public bool TryGetValue(string columnName, out object value)
		{
			object obj;
			bool flag = this.Values.TryGetValue(columnName, out obj);
			value = obj;
			return flag;
		}

		// Token: 0x170026B9 RID: 9913
		internal object this[string columnName]
		{
			get
			{
				return this.Values[columnName];
			}
		}

		// Token: 0x0600E88B RID: 59531 RVA: 0x00314F00 File Offset: 0x00313100
		public bool Equals(ValueSubstringRow other)
		{
			return this.Values.ReadOnlyDictionaryEquals(other.Values, null);
		}

		// Token: 0x0600E88C RID: 59532 RVA: 0x00314E96 File Offset: 0x00313096
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ValueSubstringRow)obj)));
		}

		// Token: 0x0600E88D RID: 59533 RVA: 0x00314F14 File Offset: 0x00313114
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x0600E88E RID: 59534 RVA: 0x00314F1C File Offset: 0x0031311C
		public override string ToString()
		{
			return "[" + string.Join(", ", this.Values.Select((KeyValuePair<string, object> kv) => FormattableString.Invariant(FormattableStringFactory.Create("[{0}] = \"{1}\"", new object[] { kv.Key, kv.Value })))) + "]";
		}

		// Token: 0x0600E88F RID: 59535 RVA: 0x00314F6C File Offset: 0x0031316C
		internal XElement SerializeToXML(Dictionary<object, int> identityCache)
		{
			return new XElement("ValueSubstringRow", this.Values.Select(delegate(KeyValuePair<string, object> kvp)
			{
				XName xname = "Column";
				object[] array = new object[3];
				array[0] = new XElement("ColumnId", kvp.Key);
				int num = 1;
				XName xname2 = "ColumnType";
				object obj;
				if (!(kvp.Value is ValueSubstring))
				{
					if (!(kvp.Value is decimal))
					{
						if (!(kvp.Value is double))
						{
							if (!(kvp.Value is DateTime))
							{
								if (kvp.Value != null)
								{
									throw new NotImplementedException(string.Format("Unexpected column type: type={0}, value={1}", kvp.Value.GetType(), kvp.Value));
								}
								obj = null;
							}
							else
							{
								obj = "DateTime";
							}
						}
						else
						{
							obj = "Double";
						}
					}
					else
					{
						obj = "Decimal";
					}
				}
				else
				{
					obj = "ValueSubstring";
				}
				array[num] = new XElement(xname2, obj);
				int num2 = 2;
				XName xname3 = "ColumnValue";
				ValueSubstring valueSubstring = kvp.Value as ValueSubstring;
				array[num2] = new XElement(xname3, (valueSubstring != null) ? valueSubstring.SerializeToXML(identityCache) : kvp.Value.ToLiteral(identityCache));
				return new XElement(xname, array);
			}));
		}

		// Token: 0x0600E890 RID: 59536 RVA: 0x00314FAC File Offset: 0x003131AC
		public static ValueSubstringRow DeserializeFromXML(XElement node, Dictionary<int, object> identityCache)
		{
			if (node.Name != "ValueSubstringRow")
			{
				throw new InvalidOperationException();
			}
			return new ValueSubstringRow(node.Elements().ToDictionary(delegate(XElement colNode)
			{
				XElement xelement = colNode.Element("ColumnId");
				string text = ((xelement != null) ? xelement.Value : null);
				if (text == null)
				{
					throw new InvalidOperationException();
				}
				return text;
			}, delegate(XElement colNode)
			{
				XElement xelement2 = colNode.Element("ColumnValue");
				if (xelement2 == null)
				{
					throw new InvalidOperationException();
				}
				XElement xelement3 = colNode.Element("ColumnType");
				string text2 = ((xelement3 != null) ? xelement3.Value : null);
				if (xelement2.IsEmpty)
				{
					return null;
				}
				if (text2 == "DateTime")
				{
					return StdLiteralParsing.TryParse<DateTime>(xelement2.Value, default(DeserializationContext)).Value;
				}
				if (text2 == "Decimal")
				{
					return StdLiteralParsing.TryParse<decimal>(xelement2.Value, default(DeserializationContext)).Value;
				}
				if (text2 == "Double")
				{
					return StdLiteralParsing.TryParse<double>(xelement2.Value, default(DeserializationContext)).Value;
				}
				XElement xelement4 = xelement2.Elements().SingleOrDefault<XElement>();
				if (xelement4 == null)
				{
					return null;
				}
				return ValueSubstring.DeserializeFromXML(xelement4, identityCache);
			}));
		}

		// Token: 0x04005892 RID: 22674
		[JsonIgnore]
		private readonly int hashCode;
	}
}
