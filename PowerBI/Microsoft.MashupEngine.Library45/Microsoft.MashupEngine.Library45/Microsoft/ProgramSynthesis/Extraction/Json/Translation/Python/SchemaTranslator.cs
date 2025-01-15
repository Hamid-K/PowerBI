using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Schema.Element;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Translation.Python
{
	// Token: 0x02000B9D RID: 2973
	public class SchemaTranslator : SchemaElementVisitor<string, JsonRegion>
	{
		// Token: 0x06004B89 RID: 19337 RVA: 0x000EEA42 File Offset: 0x000ECC42
		private SchemaTranslator(string headerModuleName)
		{
			this.HeaderModuleName = headerModuleName;
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x000EEA51 File Offset: 0x000ECC51
		private string HeaderModuleName { get; }

		// Token: 0x06004B8B RID: 19339 RVA: 0x000EEA5C File Offset: 0x000ECC5C
		public override string VisitSequenceElement(SequenceElement<JsonRegion> sequenceElement)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}.SequenceElement({1}, {2}, {3}, {4})", new object[]
			{
				this.HeaderModuleName,
				sequenceElement.Name.ToLiteral(null),
				sequenceElement.IsNullable,
				sequenceElement.UseOutput,
				sequenceElement.Child.Accept<string>(this)
			}));
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x000EEAC4 File Offset: 0x000ECCC4
		public override string VisitStructElement(StructElement<JsonRegion> structElement)
		{
			string text = ((structElement.Children == null) ? "None" : FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { string.Join(", ", structElement.Children.Select((ISchemaElement<JsonRegion> c) => c.Accept<string>(this))) })));
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}.StructElement({1}, {2}, {3}, {4})", new object[]
			{
				this.HeaderModuleName,
				structElement.Name.ToLiteral(null),
				structElement.IsNullable,
				structElement.UseOutput,
				text
			}));
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x000EEB68 File Offset: 0x000ECD68
		public static string Translate(Program p, string headerModuleName)
		{
			SchemaTranslator schemaTranslator = new SchemaTranslator(headerModuleName);
			return p.Schema.Accept<string>(schemaTranslator);
		}
	}
}
