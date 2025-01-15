using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav
{
	// Token: 0x02000038 RID: 56
	public interface IConceptualEntity : IConceptualDisplayItem, IEquatable<IConceptualEntity>
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000C9 RID: 201
		string EdmName { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000CA RID: 202
		string EntityContainerName { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000CB RID: 203
		IConceptualSchema Schema { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000CC RID: 204
		string Extends { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000CD RID: 205
		ConceptualEntityVisibilityType Visibility { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000CE RID: 206
		bool IsDateTable { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000CF RID: 207
		IReadOnlyList<IConceptualProperty> Properties { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000D0 RID: 208
		IReadOnlyList<IConceptualNavigationProperty> NavigationProperties { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000D1 RID: 209
		IReadOnlyList<IConceptualHierarchy> Hierarchies { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000D2 RID: 210
		IReadOnlyList<IConceptualColumn> KeyColumns { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000D3 RID: 211
		IReadOnlyList<IConceptualDisplayFolder> DisplayFolders { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000D4 RID: 212
		IConceptualColumn DefaultLabelColumn { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000D5 RID: 213
		IConceptualColumn DefaultImageColumn { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000D6 RID: 214
		IReadOnlyList<IConceptualProperty> DefaultFieldProperties { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000D7 RID: 215
		ConceptualEntityStatistics Statistics { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000D8 RID: 216
		ConceptualTableType ConceptualResultType { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000D9 RID: 217
		string StableName { get; }

		// Token: 0x060000DA RID: 218
		bool TryGetProperty(string referenceName, out IConceptualProperty conceptualProperty);

		// Token: 0x060000DB RID: 219
		bool TryGetPropertyByEdmName(string edmName, out IConceptualProperty conceptualProperty);

		// Token: 0x060000DC RID: 220
		bool TryGetHierarchy(string name, out IConceptualHierarchy conceptualHierarchy);

		// Token: 0x060000DD RID: 221
		bool TryGetHierarchyByEdmName(string edmName, out IConceptualHierarchy conceptualHierarchy);

		// Token: 0x060000DE RID: 222
		string GetFullName();
	}
}
