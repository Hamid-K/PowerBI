using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity
{
	// Token: 0x02001D7C RID: 7548
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class TokenizerCollection : IRenderableLiteral, IEnumerable<EntityBasedTokenizer>, IEnumerable, IEquatable<TokenizerCollection>
	{
		// Token: 0x17002A49 RID: 10825
		// (get) Token: 0x0600FDCB RID: 64971 RVA: 0x00363A63 File Offset: 0x00361C63
		public HashSet<EntityBasedTokenizer> Tokenizers { get; }

		// Token: 0x0600FDCC RID: 64972 RVA: 0x00363A6B File Offset: 0x00361C6B
		public TokenizerCollection(IEnumerable<EntityBasedTokenizer> tokenizers)
		{
			this.Tokenizers = new HashSet<EntityBasedTokenizer>(tokenizers);
		}

		// Token: 0x0600FDCD RID: 64973 RVA: 0x00363A7F File Offset: 0x00361C7F
		public TokenizerCollection(params EntityBasedTokenizer[] tokenizers)
			: this(tokenizers)
		{
		}

		// Token: 0x0600FDCE RID: 64974 RVA: 0x00363A88 File Offset: 0x00361C88
		public IEnumerator<EntityBasedTokenizer> GetEnumerator()
		{
			return this.Tokenizers.GetEnumerator();
		}

		// Token: 0x0600FDCF RID: 64975 RVA: 0x00363A9A File Offset: 0x00361C9A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600FDD0 RID: 64976 RVA: 0x00363AA2 File Offset: 0x00361CA2
		public bool Equals(TokenizerCollection other)
		{
			return other != null && (other == this || this.Tokenizers.SetEquals(other.Tokenizers));
		}

		// Token: 0x0600FDD1 RID: 64977 RVA: 0x00363AC0 File Offset: 0x00361CC0
		public override bool Equals(object other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals((TokenizerCollection)other)));
		}

		// Token: 0x0600FDD2 RID: 64978 RVA: 0x00363AEE File Offset: 0x00361CEE
		public override int GetHashCode()
		{
			return this.Tokenizers.OrderIndependentHashCode<EntityBasedTokenizer>();
		}

		// Token: 0x0600FDD3 RID: 64979 RVA: 0x00363AFC File Offset: 0x00361CFC
		public string RenderHumanReadable()
		{
			string text = "[{0}]";
			object[] array = new object[1];
			array[0] = string.Join(", ", this.Tokenizers.Select((EntityBasedTokenizer t) => "{" + t.RenderHumanReadable() + "}"));
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x0600FDD4 RID: 64980 RVA: 0x00363B58 File Offset: 0x00361D58
		public XElement RenderXML()
		{
			XElement xelement = new XElement("TokenizerCollection");
			xelement.Add(new XElement("Tokenizers", this.Tokenizers.Select((EntityBasedTokenizer t) => t.RenderXML()).ToList<XElement>()));
			return xelement;
		}

		// Token: 0x0600FDD5 RID: 64981 RVA: 0x00363BB8 File Offset: 0x00361DB8
		public static TokenizerCollection TryParseXML(XElement literal, DeserializationContext context)
		{
			IReadOnlyList<EntityBasedTokenizer> readOnlyList = (from el in literal.Elements().First((XElement e) => e.Name == "TokenizerCollection").Elements()
					.First((XElement e) => e.Name == "Tokenizers")
					.Elements()
				select EntityBasedTokenizer.TryParseXML(el, context)).ToList<EntityBasedTokenizer>();
			if (!readOnlyList.Any((EntityBasedTokenizer t) => t == null))
			{
				return new TokenizerCollection(readOnlyList);
			}
			return null;
		}

		// Token: 0x0600FDD6 RID: 64982 RVA: 0x00363C70 File Offset: 0x00361E70
		public static TokenizerCollection TryParseHumanReadable(string literal, DeserializationContext context)
		{
			literal = literal.Trim(new char[] { '[', ']' });
			return new TokenizerCollection(from str in (from s in literal.Split(new string[] { "}, " }, StringSplitOptions.None)
					select s.Trim(new char[] { '{', '}' })).ToList<string>()
				select EntityBasedTokenizer.TryParseHumanReadable(str, context));
		}

		// Token: 0x0600FDD7 RID: 64983 RVA: 0x00363CF6 File Offset: 0x00361EF6
		public TokenizerCollectionToExtractor CreateExtractorFor(EntityType entityType)
		{
			return new TokenizerCollectionToExtractor(this, entityType);
		}
	}
}
