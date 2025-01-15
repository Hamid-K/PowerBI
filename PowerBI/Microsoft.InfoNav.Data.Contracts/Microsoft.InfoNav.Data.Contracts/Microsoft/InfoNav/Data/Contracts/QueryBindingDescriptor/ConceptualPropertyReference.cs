using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000CB RID: 203
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ConceptualPropertyReference : IEquatable<ConceptualPropertyReference>
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0000C06A File Offset: 0x0000A26A
		internal ConceptualPropertyReference(IConceptualProperty prop)
		{
			this.Entity = prop.Entity.Name;
			this.Property = prop.Name;
			this.Schema = ConceptualSchemaNames.NormalizeSchemaNameForSerialization(prop.Entity);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public ConceptualPropertyReference(string entity, string property, string schema = null)
		{
			this.Entity = entity;
			this.Property = property;
			this.Schema = ConceptualSchemaNames.NormalizeForSerialization(schema);
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000C0C2 File Offset: 0x0000A2C2
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		[DataMember(IsRequired = true, Order = 10)]
		public string Entity { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000C0D3 File Offset: 0x0000A2D3
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		[DataMember(IsRequired = true, Order = 20)]
		public string Property { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string Schema { get; set; }

		// Token: 0x06000543 RID: 1347 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public override bool Equals(object obj)
		{
			ConceptualPropertyReference conceptualPropertyReference = obj as ConceptualPropertyReference;
			if (conceptualPropertyReference != null)
			{
				return this.Equals(conceptualPropertyReference);
			}
			return base.Equals(obj);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000C120 File Offset: 0x0000A320
		public bool Equals(ConceptualPropertyReference other)
		{
			return other != null && (ConceptualNameComparer.Instance.Equals(this.Entity, other.Entity) && ConceptualNameComparer.Instance.Equals(this.Property, other.Property)) && ConceptualNameComparer.Instance.Equals(this.Schema, other.Schema);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000C17A File Offset: 0x0000A37A
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Entity, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Property, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Schema, ConceptualNameComparer.Instance));
		}
	}
}
