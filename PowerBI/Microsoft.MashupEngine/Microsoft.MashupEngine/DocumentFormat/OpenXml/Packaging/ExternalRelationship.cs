using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200214A RID: 8522
	internal class ExternalRelationship : ReferenceRelationship
	{
		// Token: 0x0600D3D7 RID: 54231 RVA: 0x002A15FC File Offset: 0x0029F7FC
		protected internal ExternalRelationship(Uri externalUri, string relationshipType, string id)
			: base(externalUri, true, relationshipType, id)
		{
		}
	}
}
