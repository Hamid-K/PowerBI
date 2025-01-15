using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001E3 RID: 483
	internal sealed class ODataVerboseJsonParameterReader : ODataParameterReaderCore
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x00032B78 File Offset: 0x00030D78
		internal ODataVerboseJsonParameterReader(ODataVerboseJsonInputContext verboseJsonInputContext, IEdmFunctionImport functionImport)
			: base(verboseJsonInputContext, functionImport)
		{
			this.verboseJsonInputContext = verboseJsonInputContext;
			this.verboseJsonPropertyAndValueDeserializer = new ODataVerboseJsonPropertyAndValueDeserializer(verboseJsonInputContext);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00032B98 File Offset: 0x00030D98
		protected override bool ReadAtStartImplementation()
		{
			this.verboseJsonPropertyAndValueDeserializer.ReadPayloadStart(false);
			if (this.verboseJsonPropertyAndValueDeserializer.JsonReader.NodeType == JsonNodeType.EndOfInput)
			{
				base.PopScope(ODataParameterReaderState.Start);
				base.EnterScope(ODataParameterReaderState.Completed, null, null);
				return false;
			}
			this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadStartObject();
			if (this.EndOfParameters())
			{
				this.ReadParametersEnd();
				return false;
			}
			this.ReadNextParameter();
			return true;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00032BFD File Offset: 0x00030DFD
		protected override bool ReadNextParameterImplementation()
		{
			base.PopScope(this.State);
			if (this.EndOfParameters())
			{
				this.ReadParametersEnd();
				return false;
			}
			this.ReadNextParameter();
			return true;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00032C22 File Offset: 0x00030E22
		protected override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataVerboseJsonCollectionReader(this.verboseJsonInputContext, expectedItemTypeReference, this);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00032C31 File Offset: 0x00030E31
		private bool EndOfParameters()
		{
			return this.verboseJsonPropertyAndValueDeserializer.JsonReader.NodeType == JsonNodeType.EndObject;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00032C46 File Offset: 0x00030E46
		private void ReadParametersEnd()
		{
			this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadEndObject();
			this.verboseJsonPropertyAndValueDeserializer.ReadPayloadEnd(false);
			base.PopScope(ODataParameterReaderState.Start);
			base.EnterScope(ODataParameterReaderState.Completed, null, null);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00032C74 File Offset: 0x00030E74
		private void ReadNextParameter()
		{
			string text = this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadPropertyName();
			IEdmTypeReference parameterTypeReference = base.GetParameterTypeReference(text);
			object obj;
			ODataParameterReaderState odataParameterReaderState;
			switch (parameterTypeReference.TypeKind())
			{
			case EdmTypeKind.Primitive:
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = parameterTypeReference.AsPrimitive();
				if (edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Stream)
				{
					throw new ODataException(Strings.ODataJsonParameterReader_UnsupportedPrimitiveParameterType(text, edmPrimitiveTypeReference.PrimitiveKind()));
				}
				obj = this.verboseJsonPropertyAndValueDeserializer.ReadNonEntityValue(edmPrimitiveTypeReference, null, null, true, text);
				odataParameterReaderState = ODataParameterReaderState.Value;
				goto IL_0122;
			}
			case EdmTypeKind.Complex:
				obj = this.verboseJsonPropertyAndValueDeserializer.ReadNonEntityValue(parameterTypeReference, null, null, true, text);
				odataParameterReaderState = ODataParameterReaderState.Value;
				goto IL_0122;
			case EdmTypeKind.Collection:
				obj = null;
				if (this.verboseJsonPropertyAndValueDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
				{
					obj = this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadPrimitiveValue();
					if (obj != null)
					{
						throw new ODataException(Strings.ODataJsonParameterReader_NullCollectionExpected(JsonNodeType.PrimitiveValue, obj));
					}
					odataParameterReaderState = ODataParameterReaderState.Value;
					goto IL_0122;
				}
				else
				{
					if (((IEdmCollectionType)parameterTypeReference.Definition).ElementType.TypeKind() == EdmTypeKind.Entity)
					{
						throw new ODataException(Strings.ODataJsonParameterReader_UnsupportedParameterTypeKind(text, "Entity Collection"));
					}
					odataParameterReaderState = ODataParameterReaderState.Collection;
					goto IL_0122;
				}
				break;
			}
			throw new ODataException(Strings.ODataJsonParameterReader_UnsupportedParameterTypeKind(text, parameterTypeReference.TypeKind()));
			IL_0122:
			base.EnterScope(odataParameterReaderState, text, obj);
		}

		// Token: 0x04000524 RID: 1316
		private readonly ODataVerboseJsonInputContext verboseJsonInputContext;

		// Token: 0x04000525 RID: 1317
		private readonly ODataVerboseJsonPropertyAndValueDeserializer verboseJsonPropertyAndValueDeserializer;
	}
}
