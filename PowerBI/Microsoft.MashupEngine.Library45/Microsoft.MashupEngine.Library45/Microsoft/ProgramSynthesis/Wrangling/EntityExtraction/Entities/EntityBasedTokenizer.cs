using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001DD RID: 477
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public abstract class EntityBasedTokenizer : Tokenizer<EntityToken, char, string>, IRenderableLiteral, IEquatable<EntityBasedTokenizer>
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0001FBF3 File Offset: 0x0001DDF3
		public static ImmutableHashSet<Type> KnownSubTypes
		{
			get
			{
				return EntityBasedTokenizer.KnownSubTypesLazy.Value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001FBFF File Offset: 0x0001DDFF
		public static ImmutableHashSet<string> KnownSubTypeNames
		{
			get
			{
				return EntityBasedTokenizer.KnownSubTypeNamesLazy.Value;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001FC0B File Offset: 0x0001DE0B
		public bool Equals(EntityBasedTokenizer other)
		{
			return other == this || (other != null && base.GetType() == other.GetType());
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001FC29 File Offset: 0x0001DE29
		public string RenderHumanReadable()
		{
			return base.GetType().FullName;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001FC38 File Offset: 0x0001DE38
		public XElement RenderXML()
		{
			XElement xelement = new XElement("EntityBasedTokenizer");
			string text = base.GetType().FullName ?? string.Empty;
			xelement.Add(new XElement("ActualType", new XCData(text)));
			return xelement;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001FC84 File Offset: 0x0001DE84
		private static ImmutableHashSet<Type> BuildKnownSubTypesSet()
		{
			Type myType = typeof(EntityBasedTokenizer);
			return (from t in myType.GetTypeInfo().Assembly.DefinedTypes
				where myType.IsAssignableFrom(t.AsType())
				select t.AsType()).ToImmutableHashSet<Type>();
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		public static EntityBasedTokenizer TryParseXML(XElement literal, DeserializationContext context)
		{
			if (literal.Elements().Count<XElement>() != 1 || literal.Elements().First<XElement>().Name != "ActualType")
			{
				return null;
			}
			return EntityBasedTokenizer.TryParseHumanReadable(literal.Elements().First<XElement>().Value, context);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		public static EntityBasedTokenizer TryParseHumanReadable(string literal, DeserializationContext context)
		{
			string text = null;
			if (EntityBasedTokenizer.KnownSubTypeNames.Contains(literal))
			{
				text = literal;
			}
			else if (literal.Contains(", "))
			{
				string literalPrefix = literal.Substring(0, literal.IndexOf(", ", StringComparison.Ordinal));
				text = EntityBasedTokenizer.KnownSubTypeNames.FirstOrDefault((string type) => type.StartsWith(literalPrefix, StringComparison.Ordinal));
			}
			if (text == null)
			{
				return null;
			}
			Type type2 = Type.GetType(text, false);
			if (type2 == null)
			{
				return null;
			}
			return Activator.CreateInstance(type2) as EntityBasedTokenizer;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001FDD2 File Offset: 0x0001DFD2
		public override bool Equals(object obj)
		{
			return obj != null && (obj == this || this.Equals(obj as EntityBasedTokenizer));
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001FDEB File Offset: 0x0001DFEB
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() * 2377;
		}

		// Token: 0x04000532 RID: 1330
		private static readonly Lazy<ImmutableHashSet<Type>> KnownSubTypesLazy = new Lazy<ImmutableHashSet<Type>>(new Func<ImmutableHashSet<Type>>(EntityBasedTokenizer.BuildKnownSubTypesSet));

		// Token: 0x04000533 RID: 1331
		private static readonly Lazy<ImmutableHashSet<string>> KnownSubTypeNamesLazy = new Lazy<ImmutableHashSet<string>>(() => EntityBasedTokenizer.KnownSubTypes.Select((Type t) => t.FullName).ToImmutableHashSet<string>());
	}
}
