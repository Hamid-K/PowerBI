using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000079 RID: 121
	[ImmutableObject(true)]
	internal sealed class FederatedConceptualSchema : IFederatedConceptualSchema
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00007724 File Offset: 0x00005924
		internal FederatedConceptualSchema(IEnumerable<IConceptualSchema> schemas)
		{
			Dictionary<string, IConceptualSchema> dictionary = FederatedConceptualSchema.CreateSchemaDictionary(schemas);
			this._schemas = dictionary;
			this._schemaCollection = dictionary.Values.AsIReadOnlyCollection<IConceptualSchema>();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007756 File Offset: 0x00005956
		internal FederatedConceptualSchema(params IConceptualSchema[] schemas)
			: this(schemas)
		{
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000775F File Offset: 0x0000595F
		public IReadOnlyCollection<IConceptualSchema> Schemas
		{
			get
			{
				return this._schemaCollection;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007767 File Offset: 0x00005967
		public bool TryGetSchema(string schemaId, out IConceptualSchema schema)
		{
			schemaId = schemaId ?? "";
			return this._schemas.TryGetValue(schemaId, out schema);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00007784 File Offset: 0x00005984
		private static Dictionary<string, IConceptualSchema> CreateSchemaDictionary(IEnumerable<IConceptualSchema> schemas)
		{
			Dictionary<string, IConceptualSchema> dictionary = new Dictionary<string, IConceptualSchema>(ConceptualNameComparer.Instance);
			foreach (IConceptualSchema conceptualSchema in schemas)
			{
				Contract.CheckValue<string>(conceptualSchema.SchemaId, "schemas", "schema.SchemaId");
				Contract.Check(!dictionary.ContainsKey(conceptualSchema.SchemaId), "Schemas must not have conflicting SchemaIds.");
				dictionary.Add(conceptualSchema.SchemaId, conceptualSchema);
			}
			return dictionary;
		}

		// Token: 0x040001A3 RID: 419
		private readonly IReadOnlyDictionary<string, IConceptualSchema> _schemas;

		// Token: 0x040001A4 RID: 420
		private readonly IReadOnlyCollection<IConceptualSchema> _schemaCollection;
	}
}
