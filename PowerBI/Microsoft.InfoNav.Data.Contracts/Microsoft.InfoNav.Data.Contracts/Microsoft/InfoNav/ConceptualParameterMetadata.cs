using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200002A RID: 42
	[ImmutableObject(true)]
	public sealed class ConceptualParameterMetadata
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00002829 File Offset: 0x00000A29
		public ConceptualParameterMetadata()
		{
			this._parameterKind = ParameterKind.WhatIf;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002838 File Offset: 0x00000A38
		public ConceptualParameterMetadata(ParameterKind parameterKind)
		{
			this._parameterKind = parameterKind;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002847 File Offset: 0x00000A47
		public ConceptualParameterMetadata(IReadOnlyList<ConceptualMParameter> mappedMParameters, bool supportsMultipleValues, string selectAllValue)
		{
			this._parameterKind = ParameterKind.MParameters;
			this._mappedMParameters = mappedMParameters;
			this._supportsMultipleValues = supportsMultipleValues;
			this._selectAllValue = selectAllValue;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000286B File Offset: 0x00000A6B
		public ParameterKind ParameterKind
		{
			get
			{
				return this._parameterKind;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002873 File Offset: 0x00000A73
		public IReadOnlyList<ConceptualMParameter> MappedMParameters
		{
			get
			{
				return this._mappedMParameters;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000287B File Offset: 0x00000A7B
		public bool SupportsMultipleValues
		{
			get
			{
				return this._supportsMultipleValues;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002883 File Offset: 0x00000A83
		public string SelectAllValue
		{
			get
			{
				return this._selectAllValue;
			}
		}

		// Token: 0x040000C3 RID: 195
		private readonly IReadOnlyList<ConceptualMParameter> _mappedMParameters;

		// Token: 0x040000C4 RID: 196
		private readonly string _selectAllValue;

		// Token: 0x040000C5 RID: 197
		private readonly ParameterKind _parameterKind;

		// Token: 0x040000C6 RID: 198
		private readonly bool _supportsMultipleValues;
	}
}
