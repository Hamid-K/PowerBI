using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D2C RID: 3372
	internal interface ICubeMetadataProvider
	{
		// Token: 0x06005ACD RID: 23245
		IdentifierCubeExpression GetIdentifier(int columnIndex);

		// Token: 0x06005ACE RID: 23246
		bool IsDimensionAttribute(IdentifierCubeExpression identifier);

		// Token: 0x06005ACF RID: 23247
		bool IsProperty(IdentifierCubeExpression identifier);

		// Token: 0x06005AD0 RID: 23248
		bool IsMeasure(IdentifierCubeExpression identifier);

		// Token: 0x06005AD1 RID: 23249
		IdentifierCubeExpression GetProperty(IdentifierCubeExpression attribute, CubePropertyKind kind, string name = null);

		// Token: 0x06005AD2 RID: 23250
		bool TryGetPropertyKey(IdentifierCubeExpression property, out IdentifierCubeExpression key);

		// Token: 0x06005AD3 RID: 23251
		IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measure, string propertyName);
	}
}
