using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction
{
	// Token: 0x02001D88 RID: 7560
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public abstract class CustomExtractor : IRenderableLiteral, IEquatable<CustomExtractor>
	{
		// Token: 0x0600FE1D RID: 65053
		public abstract IReadOnlyList<Record<uint, uint>> Extract(string s);

		// Token: 0x0600FE1E RID: 65054
		public abstract void BindTranslation(Module module, string name, TargetLanguage translationTarget, string headerModuleName);

		// Token: 0x17002A55 RID: 10837
		// (get) Token: 0x0600FE1F RID: 65055
		public abstract double Score { get; }

		// Token: 0x17002A56 RID: 10838
		// (get) Token: 0x0600FE20 RID: 65056 RVA: 0x00364692 File Offset: 0x00362892
		private static ImmutableHashSet<Type> KnownSubTypes
		{
			get
			{
				return CustomExtractor.KnownSubTypesLazy.Value;
			}
		}

		// Token: 0x17002A57 RID: 10839
		// (get) Token: 0x0600FE21 RID: 65057 RVA: 0x0036469E File Offset: 0x0036289E
		private static ImmutableHashSet<string> KnownSubTypeNames
		{
			get
			{
				return CustomExtractor.KnownSubTypeNamesLazy.Value;
			}
		}

		// Token: 0x0600FE22 RID: 65058 RVA: 0x003646AC File Offset: 0x003628AC
		internal static CustomExtractor TryParseXML(XElement literal, DeserializationContext context)
		{
			string text = literal.Elements().First((XElement e) => e.Name == "TypeName").Value;
			if (!CustomExtractor.KnownSubTypeNames.Contains(text))
			{
				if (!text.Contains(", "))
				{
					return null;
				}
				string typeNamePrefix = text.Substring(0, text.IndexOf(", ", StringComparison.Ordinal));
				text = CustomExtractor.KnownSubTypeNames.FirstOrDefault((string knownSubTypeName) => knownSubTypeName.StartsWith(typeNamePrefix, StringComparison.Ordinal));
				if (text == null)
				{
					return null;
				}
			}
			Type type = Type.GetType(text);
			if (type == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Could not resolve type named {0}.", new object[] { text })));
			}
			Optional<object> optional = StdLiteralParsing.TryParse(literal.Elements().First((XElement e) => e.Name == "Serialization"), type, context);
			return (optional.HasValue ? optional.Value : null) as CustomExtractor;
		}

		// Token: 0x0600FE23 RID: 65059 RVA: 0x003647BC File Offset: 0x003629BC
		internal static CustomExtractor TryParseHumanReadable(string literal, DeserializationContext context)
		{
			if (literal[0] != '"' || literal[literal.Length - 1] != '"')
			{
				return null;
			}
			literal = literal.Trim(new char[] { '"' });
			int num = literal.IndexOf("{", StringComparison.OrdinalIgnoreCase);
			int num2 = literal.IndexOf("}", StringComparison.OrdinalIgnoreCase);
			string text = literal.Substring(num + 1, num2 - num - 1);
			if (!CustomExtractor.KnownSubTypeNames.Contains(text))
			{
				return null;
			}
			Type type = Type.GetType(text);
			if (type == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Could not resolve type named {0}.", new object[] { text })));
			}
			literal = literal.Substring(num2 + 1);
			int num3 = literal.LastIndexOf(")", StringComparison.OrdinalIgnoreCase);
			int num4 = literal.IndexOf("(", StringComparison.OrdinalIgnoreCase);
			Optional<object> optional = StdLiteralParsing.TryParse(literal.Substring(num4 + 1, num3 - num4 - 1), type, context);
			return (optional.HasValue ? optional.Value : null) as CustomExtractor;
		}

		// Token: 0x0600FE24 RID: 65060
		protected abstract XElement RenderXMLImpl();

		// Token: 0x0600FE25 RID: 65061
		protected abstract string RenderHumanReadableImpl();

		// Token: 0x0600FE26 RID: 65062 RVA: 0x003648BC File Offset: 0x00362ABC
		public string RenderHumanReadable()
		{
			if (!CustomExtractor.KnownSubTypes.Contains(base.GetType()))
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Type \"{0}\" is not supported by {1}.{2}", new object[]
				{
					base.GetType(),
					typeof(CustomExtractor),
					"RenderHumanReadable"
				})));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("\"{{{0}}}({1})\"", new object[]
			{
				base.GetType().FullName,
				this.RenderHumanReadableImpl()
			}));
		}

		// Token: 0x0600FE27 RID: 65063 RVA: 0x00364940 File Offset: 0x00362B40
		public XElement RenderXML()
		{
			if (!CustomExtractor.KnownSubTypes.Contains(base.GetType()))
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Type \"{0}\" is not supported by {1}.{2}", new object[]
				{
					base.GetType(),
					typeof(CustomExtractor),
					"RenderXML"
				})));
			}
			string fullName = base.GetType().FullName;
			if (fullName == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Could not determine type of object: {0}", new object[] { this })));
			}
			XElement xelement = new XElement("CustomExtractor");
			xelement.Add(new XElement("TypeName", fullName));
			xelement.Add(new XElement("Serialization", this.RenderXMLImpl()));
			return xelement;
		}

		// Token: 0x0600FE28 RID: 65064
		public abstract bool Equals(CustomExtractor other);

		// Token: 0x0600FE29 RID: 65065
		public abstract override int GetHashCode();

		// Token: 0x0600FE2A RID: 65066 RVA: 0x00364A06 File Offset: 0x00362C06
		public override bool Equals(object obj)
		{
			return obj != null && (obj == this || (!(base.GetType() != obj.GetType()) && this.Equals((CustomExtractor)obj)));
		}

		// Token: 0x04005F1E RID: 24350
		private static readonly Lazy<ImmutableHashSet<Type>> KnownSubTypesLazy = new Lazy<ImmutableHashSet<Type>>(() => new Type[]
		{
			typeof(RegexBasedExtractor),
			typeof(TokenizerCollectionToExtractor)
		}.ToImmutableHashSet<Type>());

		// Token: 0x04005F1F RID: 24351
		private static readonly Lazy<ImmutableHashSet<string>> KnownSubTypeNamesLazy = new Lazy<ImmutableHashSet<string>>(() => CustomExtractor.KnownSubTypes.Select((Type t) => t.FullName).ToImmutableHashSet<string>());
	}
}
