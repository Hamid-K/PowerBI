using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000368 RID: 872
	public abstract class Spec : IEquatable<Spec>
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x00038370 File Offset: 0x00036570
		protected Spec(IEnumerable<State> inputs, bool disallowDuplicateInputs = true)
		{
			this.ProvidedInputs = (inputs as State[]) ?? inputs.ToArray<State>();
			this._inputSet = (inputs as HashSet<State>) ?? this.ProvidedInputs.ConvertToHashSet<State>();
			if (disallowDuplicateInputs && this._inputSet.Count != this.ProvidedInputs.Count)
			{
				throw new ArgumentException("Duplicate inputs are not permitted in this inductive specification");
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x000383DA File Offset: 0x000365DA
		public IReadOnlyList<State> ProvidedInputs { get; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal virtual bool IsExemptFromDynamicSoundnessCheck
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000383E4 File Offset: 0x000365E4
		public bool Equals(Spec other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			if (other.GetType() != base.GetType())
			{
				return false;
			}
			if (this._inputSet.Count != other._inputSet.Count)
			{
				return false;
			}
			if (!this._inputSet.SetEquals(other._inputSet))
			{
				return false;
			}
			foreach (State state in this.ProvidedInputs)
			{
				if (!this.EqualsOnInput(state, other))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600133E RID: 4926
		protected abstract bool CorrectOnProvided(State state, object output);

		// Token: 0x0600133F RID: 4927 RVA: 0x0003848C File Offset: 0x0003668C
		public bool CorrectOnAllProvided(IEnumerable<object> outputs)
		{
			return this.ProvidedInputs.ZipWith(outputs).All(new Func<Record<State, object>, bool>(this.Valid));
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000384AC File Offset: 0x000366AC
		public bool CorrectOnAllProvided(object output)
		{
			return this.ProvidedInputs.All((State i) => this.CorrectOnProvided(i, output));
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000384E4 File Offset: 0x000366E4
		public bool CorrectOnAllProvided(ProgramNode program)
		{
			return this.ProvidedInputs.All((State s) => this.CorrectOnProvided(s, program.Invoke(s)));
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0003851C File Offset: 0x0003671C
		internal bool Valid(Record<State, object> io)
		{
			return this.Valid(io.Item1, io.Item2);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00038530 File Offset: 0x00036730
		public bool Valid(State input, object output)
		{
			return !this._inputSet.Contains(input) || this.CorrectOnProvided(input, output);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Spec left, Spec right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Spec left, Spec right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0003854A File Offset: 0x0003674A
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj is Spec && this.Equals((Spec)obj)));
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00038570 File Offset: 0x00036770
		public sealed override int GetHashCode()
		{
			if (this._hash != null)
			{
				return this._hash.Value;
			}
			this._hash = new int?(this.ProvidedInputs.OrderIndependentHashCode((State s) => (773 * this.GetHashCodeOnInput(s)) ^ s.GetHashCode()));
			return this._hash.Value;
		}

		// Token: 0x06001348 RID: 4936
		protected abstract bool EqualsOnInput(State state, Spec other);

		// Token: 0x06001349 RID: 4937
		protected abstract int GetHashCodeOnInput(State state);

		// Token: 0x0600134A RID: 4938 RVA: 0x00004FAE File Offset: 0x000031AE
		protected internal virtual Spec NullToBottom()
		{
			return this;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00004FAE File Offset: 0x000031AE
		protected internal virtual Spec BottomToNull()
		{
			return this;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000385C4 File Offset: 0x000367C4
		internal XElement ToXML(Dictionary<object, int> identityCache = null)
		{
			return new XElement(base.GetType().Name, this.ProvidedInputs.Select((State s, int i) => new XElement("Example", new object[]
			{
				s.ToXML(identityCache),
				this.InputToXML(s, identityCache)
			}).WithAttribute("i", i + 1))).WithAttribute("examples", this.ProvidedInputs.Count<State>());
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0003862C File Offset: 0x0003682C
		public XElement SerializeToXML(SpecSerializationContext context)
		{
			Dictionary<object, int> dictionary = new Dictionary<object, int>();
			return this.SerializeToXML(dictionary, context);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00038647 File Offset: 0x00036847
		public XElement SerializeToXML(Dictionary<object, int> idCache, SpecSerializationContext ctx)
		{
			return this.InternedSerialize(idCache, ctx).WithAttribute("ObjectTypeName", base.GetType().Name);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00038668 File Offset: 0x00036868
		internal static Spec DeserializeFromXML(XElement node, SpecSerializationContext context)
		{
			XAttribute xattribute = node.Attribute("ObjectTypeName");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException("Invalid XML in call to DeserializeFromXML.");
			}
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			if (text != null)
			{
				int length = text.Length;
				switch (length)
				{
				case 7:
					if (text == "TopSpec")
					{
						return TopSpec.DeserializeFromXML(node, dictionary, context);
					}
					break;
				case 8:
				case 9:
				case 12:
				case 13:
				case 14:
				case 16:
					break;
				case 10:
					if (text == "PrefixSpec")
					{
						return PrefixSpec.DeserializeFromXML(node, dictionary, context);
					}
					break;
				case 11:
					if (text == "ExampleSpec")
					{
						return ExampleSpec.DeserializeFromXML(node, dictionary, context);
					}
					break;
				case 15:
				{
					char c = text[0];
					if (c != 'B')
					{
						if (c == 'S')
						{
							if (text == "SubsequenceSpec")
							{
								return SubsequenceSpec.DeserializeFromXML(node, dictionary, context);
							}
						}
					}
					else if (text == "BooleanSoftSpec")
					{
						return BooleanSoftSpec.DeserializeFromXML(node, dictionary, context);
					}
					break;
				}
				case 17:
					if (text == "OutputNotNullSpec")
					{
						return OutputNotNullSpec.DeserializeFromXML(node, dictionary, context);
					}
					break;
				case 18:
					if (text == "OutputNonEmptySpec")
					{
						return OutputNonEmptySpec.DeserializeFromXML(node, dictionary, context);
					}
					break;
				case 19:
				{
					char c = text[0];
					if (c != 'G')
					{
						if (c == 'I')
						{
							if (text == "InductiveConstraint")
							{
								throw new ArgumentException("Serialization and Deserialization of InductiveConstraint  is not supported.");
							}
						}
					}
					else if (text == "GroupedExamplesSpec")
					{
						return GroupedExamplesSpec.DeserializeFromXML(node, dictionary, context);
					}
					break;
				}
				default:
					if (length != 23)
					{
						if (length == 26)
						{
							if (text == "DisjunctiveSubsequenceSpec")
							{
								return DisjunctiveSubsequenceSpec.DeserializeFromXML(node, dictionary, context);
							}
						}
					}
					else
					{
						char c = text[0];
						if (c != 'B')
						{
							if (c == 'D')
							{
								if (text == "DisjunctiveExamplesSpec")
								{
									return DisjunctiveExamplesSpec.DeserializeFromXML(node, dictionary, context);
								}
							}
						}
						else if (text == "BooleanHardNegativeSpec")
						{
							return BooleanHardNegativeSpec.DeserializeFromXML(node, dictionary, context);
						}
					}
					break;
				}
			}
			return context.DeserializeSpec(node, dictionary);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000388B1 File Offset: 0x00036AB1
		internal static TSpec DeserializeFromXML<TSpec>(XElement node, SpecSerializationContext context) where TSpec : Spec
		{
			return (TSpec)((object)Spec.DeserializeFromXML(node, context));
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000388C0 File Offset: 0x00036AC0
		internal string SerializeToXMLString(SpecSerializationContext context, bool indent = false)
		{
			XElement xelement = this.SerializeToXML(context);
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = indent
			};
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					xelement.WriteTo(xmlWriter);
					xmlWriter.Flush();
					text = stringWriter.ToString();
				}
			}
			return text;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00038948 File Offset: 0x00036B48
		internal static Spec DeserializeFromXMLString(string xmlString, SpecSerializationContext context)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				CheckCharacters = false,
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
			Spec spec;
			using (StringReader stringReader = new StringReader(xmlString))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					spec = Spec.DeserializeFromXML(XElement.Load(xmlReader), context);
				}
			}
			return spec;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000389C0 File Offset: 0x00036BC0
		internal static TSpec DeserializeFromXMLString<TSpec>(string xmlString, SpecSerializationContext context) where TSpec : Spec
		{
			return (TSpec)((object)Spec.DeserializeFromXMLString(xmlString, context));
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000389D0 File Offset: 0x00036BD0
		private XElement InternedSerialize(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			int num;
			if (identityCache.TryGetValue(this, out num))
			{
				return new XElement("Reference", num);
			}
			XElement xelement = this.SerializeImpl(identityCache, context).WithAttribute("ObjectID", identityCache.Count);
			identityCache[this] = identityCache.Count;
			return xelement;
		}

		// Token: 0x06001355 RID: 4949
		protected abstract XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context);

		// Token: 0x06001356 RID: 4950
		protected abstract XElement InputToXML(State input, Dictionary<object, int> identityCache);

		// Token: 0x06001357 RID: 4951
		protected internal abstract Spec TransformInputs(Func<State, State> transformer);

		// Token: 0x06001358 RID: 4952 RVA: 0x00038A28 File Offset: 0x00036C28
		protected void ThrowSerializationUnsupportedException()
		{
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Serialization of Spec Type \"{0}\" is not supported.", new object[] { base.GetType() })));
		}

		// Token: 0x04000998 RID: 2456
		private const string XMLReferenceKey = "Reference";

		// Token: 0x04000999 RID: 2457
		private const string XMLObjectIdAttributeName = "ObjectID";

		// Token: 0x0400099A RID: 2458
		private readonly HashSet<State> _inputSet;

		// Token: 0x0400099B RID: 2459
		private int? _hash;
	}
}
