using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200001A RID: 26
	[ImmutableObject(true)]
	internal sealed class ConceptualVariationSource : IConceptualVariationSource, IEquatable<IConceptualVariationSource>
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00003DDA File Offset: 0x00001FDA
		internal ConceptualVariationSource(IConceptualProperty parentProperty, string name, bool isDefault, IConceptualNavigationProperty navigationProperty, IConceptualHierarchy defaultHierarchy, IConceptualProperty defaultProperty)
		{
			this._parentProperty = parentProperty;
			this._name = name;
			this._isDefault = isDefault;
			this._navigationProperty = navigationProperty;
			this._defaultHierarchy = defaultHierarchy;
			this._defaultProperty = defaultProperty;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003E0F File Offset: 0x0000200F
		public IConceptualProperty ParentProperty
		{
			get
			{
				return this._parentProperty;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003E17 File Offset: 0x00002017
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003E1F File Offset: 0x0000201F
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003E27 File Offset: 0x00002027
		public IConceptualNavigationProperty NavigationProperty
		{
			get
			{
				return this._navigationProperty;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003E2F File Offset: 0x0000202F
		public IConceptualHierarchy DefaultHierarchy
		{
			get
			{
				return this._defaultHierarchy;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003E37 File Offset: 0x00002037
		public IConceptualProperty DefaultProperty
		{
			get
			{
				return this._defaultProperty;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003E3F File Offset: 0x0000203F
		public bool Equals(IConceptualVariationSource other)
		{
			return this == other;
		}

		// Token: 0x040000AA RID: 170
		private readonly IConceptualProperty _parentProperty;

		// Token: 0x040000AB RID: 171
		private readonly string _name;

		// Token: 0x040000AC RID: 172
		private readonly bool _isDefault;

		// Token: 0x040000AD RID: 173
		private readonly IConceptualNavigationProperty _navigationProperty;

		// Token: 0x040000AE RID: 174
		private readonly IConceptualHierarchy _defaultHierarchy;

		// Token: 0x040000AF RID: 175
		private readonly IConceptualProperty _defaultProperty;
	}
}
