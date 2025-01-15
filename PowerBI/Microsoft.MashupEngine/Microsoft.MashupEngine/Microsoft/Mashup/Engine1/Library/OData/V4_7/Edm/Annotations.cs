using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm
{
	// Token: 0x02000809 RID: 2057
	internal sealed class Annotations
	{
		// Token: 0x06003B48 RID: 15176 RVA: 0x000C123C File Offset: 0x000BF43C
		public Annotations()
		{
			this.capabilities = new Dictionary<Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable, Capabilities>();
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

		// Token: 0x06003B49 RID: 15177 RVA: 0x000C12AC File Offset: 0x000BF4AC
		public void TryAddCapability(Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable key, Capabilities capabilities)
		{
			if (!this.capabilities.ContainsKey(key))
			{
				this.capabilities[key] = capabilities;
			}
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000C12CC File Offset: 0x000BF4CC
		public Capabilities GetElementCapability(Microsoft.OData.Edm.IEdmElement key)
		{
			Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable edmVocabularyAnnotatable = key as Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable;
			Capabilities capabilities;
			if (edmVocabularyAnnotatable != null && this.capabilities.TryGetValue(edmVocabularyAnnotatable, out capabilities))
			{
				return capabilities;
			}
			return this.CreateDefaultCapabilities();
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000C12FC File Offset: 0x000BF4FC
		public Capabilities GetElementCapability(string key)
		{
			foreach (KeyValuePair<Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable, Capabilities> keyValuePair in this.capabilities)
			{
				if (((Microsoft.OData.Edm.IEdmNamedElement)keyValuePair.Key).Name == key)
				{
					return keyValuePair.Value;
				}
			}
			return this.CreateDefaultCapabilities();
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x000C1374 File Offset: 0x000BF574
		// (set) Token: 0x06003B4D RID: 15181 RVA: 0x000C137C File Offset: 0x000BF57C
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

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000C1385 File Offset: 0x000BF585
		// (set) Token: 0x06003B4F RID: 15183 RVA: 0x000C138D File Offset: 0x000BF58D
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

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000C1396 File Offset: 0x000BF596
		// (set) Token: 0x06003B51 RID: 15185 RVA: 0x000C139E File Offset: 0x000BF59E
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

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000C13A7 File Offset: 0x000BF5A7
		// (set) Token: 0x06003B53 RID: 15187 RVA: 0x000C13AF File Offset: 0x000BF5AF
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

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000C13B8 File Offset: 0x000BF5B8
		// (set) Token: 0x06003B55 RID: 15189 RVA: 0x000C13C0 File Offset: 0x000BF5C0
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

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000C13C9 File Offset: 0x000BF5C9
		// (set) Token: 0x06003B57 RID: 15191 RVA: 0x000C13D1 File Offset: 0x000BF5D1
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

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000C13DA File Offset: 0x000BF5DA
		// (set) Token: 0x06003B59 RID: 15193 RVA: 0x000C13E2 File Offset: 0x000BF5E2
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

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000C13EB File Offset: 0x000BF5EB
		// (set) Token: 0x06003B5B RID: 15195 RVA: 0x000C13F3 File Offset: 0x000BF5F3
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

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000C13FC File Offset: 0x000BF5FC
		// (set) Token: 0x06003B5D RID: 15197 RVA: 0x000C1404 File Offset: 0x000BF604
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

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000C140D File Offset: 0x000BF60D
		// (set) Token: 0x06003B5F RID: 15199 RVA: 0x000C1415 File Offset: 0x000BF615
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

		// Token: 0x06003B60 RID: 15200 RVA: 0x000C1420 File Offset: 0x000BF620
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
				capabilities.SupportsCount = false;
				capabilities.SupportsFilter = false;
				capabilities.RequiresFilter = false;
				capabilities.SupportsSort = false;
			}
			else if (this.conformanceLevel == ConformanceLevel.Intermediate)
			{
				capabilities.SupportsExpand = false;
				capabilities.SupportsCount = false;
				capabilities.SupportsFilter = true;
				capabilities.RequiresFilter = false;
				capabilities.SupportsSort = false;
			}
			else
			{
				capabilities.SupportsExpand = true;
				capabilities.SupportsCount = true;
				capabilities.SupportsFilter = true;
				capabilities.RequiresFilter = false;
				capabilities.SupportsSort = true;
			}
			return capabilities;
		}

		// Token: 0x04001EDE RID: 7902
		private readonly Dictionary<Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable, Capabilities> capabilities;

		// Token: 0x04001EDF RID: 7903
		private ConformanceLevel conformanceLevel;

		// Token: 0x04001EE0 RID: 7904
		private bool applySupported;

		// Token: 0x04001EE1 RID: 7905
		private bool propertyRestrictions;

		// Token: 0x04001EE2 RID: 7906
		private bool supportsBatch;

		// Token: 0x04001EE3 RID: 7907
		private bool supportsConcurrentRequests;

		// Token: 0x04001EE4 RID: 7908
		private bool supportsCrossJoin;

		// Token: 0x04001EE5 RID: 7909
		private bool supportsBatchContinueOnError;

		// Token: 0x04001EE6 RID: 7910
		private HashSet<string> filters;

		// Token: 0x04001EE7 RID: 7911
		private HashSet<string> supportedFormats;

		// Token: 0x04001EE8 RID: 7912
		private HashSet<string> aggregateTransformations;
	}
}
