using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000161 RID: 353
	[DataContract(Name = "ConceptualParameterMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualParameterMetadata
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000123D0 File Offset: 0x000105D0
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x000123D8 File Offset: 0x000105D8
		[DataMember(Name = "Kind", EmitDefaultValue = true, IsRequired = false, Order = 1)]
		public ParameterKind ParameterKind { get; set; }

		// Token: 0x060008E5 RID: 2277 RVA: 0x000123E1 File Offset: 0x000105E1
		internal ClientConceptualParameterMetadata()
		{
			this.ParameterKind = ParameterKind.WhatIf;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000123F0 File Offset: 0x000105F0
		internal ClientConceptualParameterMetadata(ParameterKind parameterKind, IReadOnlyList<string> mappedMParameters, bool supportsMultipleValues, string selectAllValue)
		{
			this.ParameterKind = parameterKind;
			if (mappedMParameters != null && mappedMParameters.Count > 0)
			{
				this.ParameterKind = ParameterKind.MParameters;
				this._mapppedMParameters = mappedMParameters.ToList<string>();
				this._supportsMultipleValues = supportsMultipleValues;
				this._supportsSelectAll = !string.IsNullOrWhiteSpace(selectAllValue);
			}
		}

		// Token: 0x04000476 RID: 1142
		[DataMember(Name = "MappedMParameterNames", EmitDefaultValue = false, IsRequired = false, Order = 0)]
		private readonly IList<string> _mapppedMParameters;

		// Token: 0x04000478 RID: 1144
		[DataMember(Name = "SupportsMultipleValues", EmitDefaultValue = false, IsRequired = false, Order = 2)]
		private readonly bool _supportsMultipleValues;

		// Token: 0x04000479 RID: 1145
		[DataMember(Name = "SupportsSelectAll", EmitDefaultValue = false, IsRequired = false, Order = 3)]
		private readonly bool _supportsSelectAll;
	}
}
