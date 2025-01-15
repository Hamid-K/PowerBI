using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000509 RID: 1289
	internal class CodeFirstOSpaceTypeFactory : OSpaceTypeFactory
	{
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x000D3E68 File Offset: 0x000D2068
		public override List<Action> ReferenceResolutions
		{
			get
			{
				return this._referenceResolutions;
			}
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000D3E70 File Offset: 0x000D2070
		public override void LogLoadMessage(string message, EdmType relatedType)
		{
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000D3E72 File Offset: 0x000D2072
		public override void LogError(string errorMessage, EdmType relatedType)
		{
			throw new MetadataException(Strings.InvalidSchemaEncountered(errorMessage));
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000D3E7F File Offset: 0x000D207F
		public override void TrackClosure(Type type)
		{
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x000D3E81 File Offset: 0x000D2081
		public override Dictionary<EdmType, EdmType> CspaceToOspace
		{
			get
			{
				return this._cspaceToOspace;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x000D3E89 File Offset: 0x000D2089
		public override Dictionary<string, EdmType> LoadedTypes
		{
			get
			{
				return this._loadedTypes;
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000D3E91 File Offset: 0x000D2091
		public override void AddToTypesInAssembly(EdmType type)
		{
		}

		// Token: 0x04001635 RID: 5685
		private readonly List<Action> _referenceResolutions = new List<Action>();

		// Token: 0x04001636 RID: 5686
		private readonly Dictionary<EdmType, EdmType> _cspaceToOspace = new Dictionary<EdmType, EdmType>();

		// Token: 0x04001637 RID: 5687
		private readonly Dictionary<string, EdmType> _loadedTypes = new Dictionary<string, EdmType>();
	}
}
