using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200083F RID: 2111
	internal sealed class Annotations
	{
		// Token: 0x06003CB3 RID: 15539 RVA: 0x000C5964 File Offset: 0x000C3B64
		public Annotations()
		{
			this.capabilities = new Dictionary<IEdmVocabularyAnnotatable, Capabilities>();
			this.conformanceLevel = ConformanceLevel.Advanced;
			this.supportsBatch = false;
			this.supportsConcurrentRequests = false;
			this.supportsCrossJoin = false;
			this.supportsBatchContinueOnError = false;
			this.filters = new HashSet<string>();
			this.supportedFormats = new HashSet<string>();
			this.applySupported = false;
			this.aggregateTransformations = new HashSet<string>();
			this.propertyRestrictions = false;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000C59D4 File Offset: 0x000C3BD4
		public void TryAddCapability(IEdmVocabularyAnnotatable key, Capabilities capabilities)
		{
			if (!this.capabilities.ContainsKey(key))
			{
				this.capabilities[key] = capabilities;
			}
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000C59F4 File Offset: 0x000C3BF4
		public Capabilities GetElementCapability(Microsoft.OData.Edm.IEdmElement key)
		{
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = key as IEdmVocabularyAnnotatable;
			Capabilities capabilities;
			if (edmVocabularyAnnotatable != null && this.capabilities.TryGetValue(edmVocabularyAnnotatable, out capabilities))
			{
				return capabilities;
			}
			return this.CreateDefaultCapabilities();
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000C5A24 File Offset: 0x000C3C24
		public Capabilities GetElementCapability(string key)
		{
			foreach (KeyValuePair<IEdmVocabularyAnnotatable, Capabilities> keyValuePair in this.capabilities)
			{
				if (((Microsoft.OData.Edm.IEdmNamedElement)keyValuePair.Key).Name == key)
				{
					return keyValuePair.Value;
				}
			}
			return this.CreateDefaultCapabilities();
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000C5A9C File Offset: 0x000C3C9C
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x000C5AA4 File Offset: 0x000C3CA4
		public ConformanceLevel ConformanceLevel
		{
			get
			{
				return this.conformanceLevel;
			}
			internal set
			{
				this.conformanceLevel = value;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000C5AAD File Offset: 0x000C3CAD
		// (set) Token: 0x06003CBA RID: 15546 RVA: 0x000C5AB5 File Offset: 0x000C3CB5
		public bool SupportsBatch
		{
			get
			{
				return this.supportsBatch;
			}
			internal set
			{
				this.supportsBatch = value;
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000C5ABE File Offset: 0x000C3CBE
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x000C5AC6 File Offset: 0x000C3CC6
		public bool SupportsConcurrentRequests
		{
			get
			{
				return this.supportsConcurrentRequests;
			}
			internal set
			{
				this.supportsConcurrentRequests = value;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000C5ACF File Offset: 0x000C3CCF
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x000C5AD7 File Offset: 0x000C3CD7
		public bool SupportsCrossJoin
		{
			get
			{
				return this.supportsCrossJoin;
			}
			internal set
			{
				this.supportsCrossJoin = value;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000C5AE0 File Offset: 0x000C3CE0
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x000C5AE8 File Offset: 0x000C3CE8
		public bool SupportsBatchContinueOnError
		{
			get
			{
				return this.supportsBatchContinueOnError;
			}
			internal set
			{
				this.supportsBatchContinueOnError = value;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000C5AF1 File Offset: 0x000C3CF1
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x000C5AF9 File Offset: 0x000C3CF9
		public bool ApplySupported
		{
			get
			{
				return this.applySupported;
			}
			internal set
			{
				this.applySupported = value;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000C5B02 File Offset: 0x000C3D02
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x000C5B0A File Offset: 0x000C3D0A
		public bool PropertyRestrictions
		{
			get
			{
				return this.propertyRestrictions;
			}
			internal set
			{
				this.propertyRestrictions = value;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000C5B13 File Offset: 0x000C3D13
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x000C5B1B File Offset: 0x000C3D1B
		public HashSet<string> AggregateTransformations
		{
			get
			{
				return this.aggregateTransformations;
			}
			internal set
			{
				this.aggregateTransformations = value;
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000C5B24 File Offset: 0x000C3D24
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x000C5B2C File Offset: 0x000C3D2C
		public HashSet<string> Filters
		{
			get
			{
				return this.filters;
			}
			internal set
			{
				this.filters = value;
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000C5B35 File Offset: 0x000C3D35
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x000C5B3D File Offset: 0x000C3D3D
		public HashSet<string> SupportedFormats
		{
			get
			{
				return this.supportedFormats;
			}
			internal set
			{
				this.supportedFormats = value;
			}
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x000C5B48 File Offset: 0x000C3D48
		public Capabilities CreateDefaultCapabilities()
		{
			Capabilities capabilities = new Capabilities
			{
				FilterFunctions = this.filters,
				SupportsSkip = (this.ConformanceLevel == ConformanceLevel.Advanced),
				SupportsTop = (this.ConformanceLevel != ConformanceLevel.Minimal),
				SupportsSelect = (this.ConformanceLevel != ConformanceLevel.Minimal)
			};
			if (this.conformanceLevel == ConformanceLevel.Minimal)
			{
				capabilities.SupportsExpand = false;
				capabilities.Navigability = NavigationType.None;
				capabilities.SupportsCount = false;
				capabilities.SupportsFilter = false;
				capabilities.RequiresFilter = false;
				capabilities.SupportsSort = false;
			}
			else if (this.conformanceLevel == ConformanceLevel.Intermediate)
			{
				capabilities.SupportsExpand = false;
				capabilities.Navigability = NavigationType.Single;
				capabilities.SupportsCount = false;
				capabilities.SupportsFilter = true;
				capabilities.RequiresFilter = false;
				capabilities.SupportsSort = false;
			}
			else
			{
				capabilities.SupportsExpand = true;
				capabilities.Navigability = NavigationType.Recursive;
				capabilities.SupportsCount = true;
				capabilities.SupportsFilter = true;
				capabilities.RequiresFilter = false;
				capabilities.SupportsSort = true;
			}
			return capabilities;
		}

		// Token: 0x04001FC8 RID: 8136
		private readonly Dictionary<IEdmVocabularyAnnotatable, Capabilities> capabilities;

		// Token: 0x04001FC9 RID: 8137
		private ConformanceLevel conformanceLevel;

		// Token: 0x04001FCA RID: 8138
		private bool applySupported;

		// Token: 0x04001FCB RID: 8139
		private bool propertyRestrictions;

		// Token: 0x04001FCC RID: 8140
		private bool supportsBatch;

		// Token: 0x04001FCD RID: 8141
		private bool supportsConcurrentRequests;

		// Token: 0x04001FCE RID: 8142
		private bool supportsCrossJoin;

		// Token: 0x04001FCF RID: 8143
		private bool supportsBatchContinueOnError;

		// Token: 0x04001FD0 RID: 8144
		private HashSet<string> filters;

		// Token: 0x04001FD1 RID: 8145
		private HashSet<string> supportedFormats;

		// Token: 0x04001FD2 RID: 8146
		private HashSet<string> aggregateTransformations;
	}
}
