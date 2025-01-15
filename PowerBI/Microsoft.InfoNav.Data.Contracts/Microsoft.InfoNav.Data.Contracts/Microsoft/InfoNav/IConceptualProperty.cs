using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000040 RID: 64
	public interface IConceptualProperty : IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000105 RID: 261
		string EdmName { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000106 RID: 262
		IConceptualEntity Entity { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000107 RID: 263
		DataType Type { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000108 RID: 264
		PropertyDataCategory DataCategory { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000109 RID: 265
		bool IsHidden { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600010A RID: 266
		bool IsPrivate { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600010B RID: 267
		int Ordinal { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600010C RID: 268
		string FormatString { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600010D RID: 269
		ConceptualPrimitiveType ConceptualDataType { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600010E RID: 270
		ConceptualDataCategory ConceptualDataCategory { get; }

		// Token: 0x0600010F RID: 271
		string GetFullName();

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000110 RID: 272
		string StableName { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000111 RID: 273
		bool IsStable { get; }
	}
}
