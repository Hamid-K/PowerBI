using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav
{
	// Token: 0x02000041 RID: 65
	public interface IConceptualSchema : IAnnotatableRoot
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000112 RID: 274
		string SchemaId { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000113 RID: 275
		string DisplayName { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000114 RID: 276
		LanguageIdentifier Language { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000115 RID: 277
		ConceptualCollation ConceptualCollation { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000116 RID: 278
		string Extends { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000117 RID: 279
		IReadOnlyList<IConceptualEntity> Entities { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000118 RID: 280
		ConceptualSchemaStatistics Statistics { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000119 RID: 281
		ConceptualCapabilities Capabilities { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600011A RID: 282
		IConceptualMeasure DefaultMeasure { get; }

		// Token: 0x0600011B RID: 283
		bool TryGetEntity(string referenceName, out IConceptualEntity entity);

		// Token: 0x0600011C RID: 284
		bool TryGetEntityByEdmName(string qualifiedName, out IConceptualEntity entity);

		// Token: 0x0600011D RID: 285
		bool TryGetPropertyByEdmName(EdmPropertyRef edmPropertyRef, out IConceptualProperty property);
	}
}
