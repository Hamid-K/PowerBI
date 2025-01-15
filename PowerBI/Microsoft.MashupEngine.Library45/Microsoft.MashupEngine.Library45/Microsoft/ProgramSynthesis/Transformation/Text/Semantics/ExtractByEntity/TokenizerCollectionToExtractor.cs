using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity
{
	// Token: 0x02001D80 RID: 7552
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class TokenizerCollectionToExtractor : CustomExtractor, IEquatable<TokenizerCollectionToExtractor>
	{
		// Token: 0x17002A4A RID: 10826
		// (get) Token: 0x0600FDE4 RID: 64996 RVA: 0x00363D8C File Offset: 0x00361F8C
		public TokenizerCollection Tokenizers { get; }

		// Token: 0x17002A4B RID: 10827
		// (get) Token: 0x0600FDE5 RID: 64997 RVA: 0x00363D94 File Offset: 0x00361F94
		public EntityType EntityType { get; }

		// Token: 0x17002A4C RID: 10828
		// (get) Token: 0x0600FDE6 RID: 64998 RVA: 0x00363D9C File Offset: 0x00361F9C
		public Type EntityTokenType { get; }

		// Token: 0x0600FDE7 RID: 64999 RVA: 0x00363DA4 File Offset: 0x00361FA4
		public TokenizerCollectionToExtractor(TokenizerCollection tokenizers, EntityType entityType)
		{
			this.Tokenizers = tokenizers;
			this.EntityType = entityType;
			this.EntityTokenType = EntityMappings.EntityDescriptors[entityType].Type;
		}

		// Token: 0x0600FDE8 RID: 65000 RVA: 0x00363DD0 File Offset: 0x00361FD0
		public override IReadOnlyList<Record<uint, uint>> Extract(string s)
		{
			return (from t in TokenFilters.ResolveSubsumptionByPrecedence(from tok in this.Tokenizers.SelectMany((EntityBasedTokenizer tok) => tok.Tokenize(s))
					where EntityMappings.TypeToEntityType.ContainsKey(tok.GetType())
					select tok)
				where t.GetType() == this.EntityTokenType
				select Record.Create<uint, uint>((uint)t.Start, (uint)t.End)).ToList<Record<uint, uint>>();
		}

		// Token: 0x0600FDE9 RID: 65001 RVA: 0x00363E6B File Offset: 0x0036206B
		public override void BindTranslation(Microsoft.ProgramSynthesis.Translation.Module module, string name, TargetLanguage translationTarget, string headerModuleName)
		{
			if (translationTarget == TargetLanguage.Python)
			{
				this.BindPythonTranslation((PythonModule)module, name);
				return;
			}
			if (translationTarget != TargetLanguage.Java)
			{
				throw new NotImplementedException(string.Format("Unknown {0} to bind.", translationTarget));
			}
			throw new NotImplementedException("Java is no longer supported as a translationTarget.");
		}

		// Token: 0x0600FDEA RID: 65002 RVA: 0x00363EA4 File Offset: 0x003620A4
		private void BindPythonTranslation(PythonModule module, string name)
		{
			foreach (EntityBasedTokenizer entityBasedTokenizer in this.Tokenizers)
			{
				foreach (KeyValuePair<string, string> keyValuePair in TranslationMapping.GetSourceCodeForType(entityBasedTokenizer.GetType()))
				{
					string key = keyValuePair.Key;
					string text;
					if (!module.TryGetAuxiliaryCode(key, out text))
					{
						module.AppendAuxiliaryCode(key, keyValuePair.Value);
					}
				}
			}
			string text2 = FormattableString.Invariant(FormattableStringFactory.Create("static_literal_{0}", new object[] { Guid.NewGuid().ToString().Replace("-", "_") }));
			string text3 = FormattableString.Invariant(FormattableStringFactory.Create("EntityType.{0}", new object[] { this.EntityType.ToString() }));
			string text4 = "[{0}]";
			object[] array = new object[1];
			array[0] = string.Join(", ", this.Tokenizers.Select((EntityBasedTokenizer t) => t.GetType().GetTypeInfo().Name + "()"));
			string text5 = FormattableString.Invariant(FormattableStringFactory.Create(text4, array));
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("self.{0} = TokenizerCollectionToExtractor({1}, {2})", new object[] { text2, text3, text5 })));
			List<Record<string, Type>> list = new List<Record<string, Type>> { Record.Create<string, Type>("region", typeof(ValueSubstring)) };
			Type typeFromHandle = typeof(Record<int?, int?>);
			CodeBuilder codeBuilder2 = new CodeBuilder(4U);
			codeBuilder2.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return list(filter(lambda match: match[0] >= region.start and match[1] <= region.end, self.{0}.extract(region.get_value())))", new object[] { text2 })));
			OpaqueGeneratedFunction opaqueGeneratedFunction = new OpaqueGeneratedFunction(list, typeFromHandle, codeBuilder, codeBuilder2);
			module.Bind(name, opaqueGeneratedFunction);
		}

		// Token: 0x17002A4D RID: 10829
		// (get) Token: 0x0600FDEB RID: 65003 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public override double Score
		{
			get
			{
				return 1.0;
			}
		}

		// Token: 0x0600FDEC RID: 65004 RVA: 0x003640A4 File Offset: 0x003622A4
		public override bool Equals(CustomExtractor other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals((TokenizerCollectionToExtractor)other)));
		}

		// Token: 0x0600FDED RID: 65005 RVA: 0x003640D4 File Offset: 0x003622D4
		public override int GetHashCode()
		{
			return (this.Tokenizers.OrderIndependentHashCode<EntityBasedTokenizer>() * 1201) ^ this.EntityType.GetHashCode();
		}

		// Token: 0x0600FDEE RID: 65006 RVA: 0x00364107 File Offset: 0x00362307
		public bool Equals(TokenizerCollectionToExtractor other)
		{
			return other != null && (other == this || (this.Tokenizers.Equals(other.Tokenizers) && this.EntityType == other.EntityType));
		}

		// Token: 0x0600FDEF RID: 65007 RVA: 0x00364138 File Offset: 0x00362338
		public new static TokenizerCollectionToExtractor TryParseXML(XElement literal, DeserializationContext context)
		{
			XElement xelement = literal.Elements().First((XElement e) => e.Name == "TokenizerCollection");
			EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), literal.Elements().First((XElement e) => e.Name == "EntityType").Value);
			return new TokenizerCollectionToExtractor(TokenizerCollection.TryParseXML(xelement, context), entityType);
		}

		// Token: 0x0600FDF0 RID: 65008 RVA: 0x003641C0 File Offset: 0x003623C0
		public new static TokenizerCollectionToExtractor TryParseHumanReadable(string literal, DeserializationContext context)
		{
			if (!literal.StartsWith("TokenizerCollectionToExtractor(", StringComparison.Ordinal))
			{
				return null;
			}
			int num = literal.IndexOf("(", StringComparison.OrdinalIgnoreCase);
			int num2 = literal.Length - 1;
			string text = literal.Substring(num + 1, num2 - num - 1);
			int num3 = text.LastIndexOf(",", StringComparison.OrdinalIgnoreCase);
			string text2 = text.Substring(num3 + 1).Trim();
			EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), text2);
			return new TokenizerCollectionToExtractor(TokenizerCollection.TryParseHumanReadable(text.Substring(0, num3), context), entityType);
		}

		// Token: 0x0600FDF1 RID: 65009 RVA: 0x00364248 File Offset: 0x00362448
		protected override XElement RenderXMLImpl()
		{
			XElement xelement = new XElement("TokenizerCollectionToExtractor");
			xelement.Add(new XElement("TokenizerCollection", this.Tokenizers.RenderXML()));
			xelement.Add(new XElement("EntityType", this.EntityType.ToString()));
			return xelement;
		}

		// Token: 0x0600FDF2 RID: 65010 RVA: 0x003642AD File Offset: 0x003624AD
		protected override string RenderHumanReadableImpl()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("TokenizerCollectionToExtractor({0}, {1})", new object[]
			{
				this.Tokenizers.RenderHumanReadable(),
				this.EntityType
			}));
		}
	}
}
