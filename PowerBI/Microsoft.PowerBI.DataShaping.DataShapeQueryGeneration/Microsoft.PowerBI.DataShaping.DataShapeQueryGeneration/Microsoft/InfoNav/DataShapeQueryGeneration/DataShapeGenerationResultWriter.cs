using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.DataShaping.Common.Json;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Data.Contracts.Utils;
using Microsoft.InfoNav.DataShapeQueryGeneration.DSQ;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000044 RID: 68
	internal static class DataShapeGenerationResultWriter
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000A638 File Offset: 0x00008838
		public static void Write(DataShapeGenerationResult dsqGenResult, DataShapeGenerationException dsqGenException, Stream stream, bool enableRemoteErrors, bool writeDataShape, IFeatureSwitchProvider featureSwitchProvider)
		{
			DataShapeGenerationResultWriter.Write(dsqGenResult.BindingDescriptor, dsqGenResult.DataShape, dsqGenException, stream, enableRemoteErrors, writeDataShape, featureSwitchProvider, dsqGenResult.InternalSchema, dsqGenResult.TestOnlyInternalDsqReferenceSchema);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A66C File Offset: 0x0000886C
		public static void Write(QueryBindingDescriptor descriptor, DataShape dataShape, DataShapeGenerationException dsqGenException, Stream stream, bool enableRemoteErrors, bool writeDataShape, IFeatureSwitchProvider featureSwitchProvider, IntermediateDataShapeTableSchema testOnlyInternalSchema = null, IntermediateDataShapeReferenceSchema testOnlyInternalDsqReferenceSchema = null)
		{
			JsonWriter jsonWriter = new JsonWriter(new StreamWriter(stream), false);
			jsonWriter.StartObjectScope();
			if (writeDataShape)
			{
				DataShapeGenerationResultWriter.WriteDataShape(dataShape, dsqGenException, enableRemoteErrors, stream, jsonWriter, featureSwitchProvider);
			}
			DataShapeGenerationResultWriter.WriteQueryBindingDescriptor(descriptor, stream, jsonWriter);
			if (testOnlyInternalSchema != null)
			{
				DataShapeGenerationResultWriter.WriteIntermediateTableSchema(testOnlyInternalSchema, stream, jsonWriter);
			}
			if (testOnlyInternalDsqReferenceSchema != null)
			{
				DataShapeGenerationResultWriter.WriteIntermediateDsqReferenceSchema(testOnlyInternalDsqReferenceSchema, stream, jsonWriter);
			}
			jsonWriter.EndObjectScope();
			jsonWriter.Flush();
			jsonWriter.Close();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A6D4 File Offset: 0x000088D4
		private static void WriteDataShape(DataShape dataShape, DataShapeGenerationException dsqGenException, bool enableRemoteErrors, Stream stream, JsonWriter jsonWriter, IFeatureSwitchProvider featureSwitchProvider)
		{
			if (dataShape != null)
			{
				jsonWriter.WriteName("DataShape");
				DataShapeQueryWriter.WriteJson(dataShape, jsonWriter);
				return;
			}
			if (dsqGenException != null)
			{
				jsonWriter.WriteName("DataShape");
				jsonWriter.WriteValueSeparator();
				jsonWriter.Flush();
				DataShapeResultWriterUtilities.WriteErrorDsr(new JsonStreamingStructureWriter(stream), enableRemoteErrors, dsqGenException, null, featureSwitchProvider);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A72B File Offset: 0x0000892B
		private static void WriteQueryBindingDescriptor(QueryBindingDescriptor descriptor, Stream stream, JsonWriter jsonWriter)
		{
			jsonWriter.WriteName("QueryBindingDescriptor");
			jsonWriter.WriteValueSeparator();
			jsonWriter.Flush();
			if (descriptor != null)
			{
				DataShapeGenerationResultWriter.ToJson<QueryBindingDescriptor>(descriptor, stream);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A74E File Offset: 0x0000894E
		private static void WriteIntermediateTableSchema(IntermediateDataShapeTableSchema schema, Stream stream, JsonWriter jsonWriter)
		{
			jsonWriter.WriteName("IntermediateTableSchema");
			jsonWriter.WriteValueSeparator();
			jsonWriter.Flush();
			if (schema != null)
			{
				NewtonsoftJsonSerializationUtil.ToJsonStream<IntermediateDataShapeTableSchema>(schema, stream);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A771 File Offset: 0x00008971
		private static void WriteIntermediateDsqReferenceSchema(IntermediateDataShapeReferenceSchema schema, Stream stream, JsonWriter jsonWriter)
		{
			jsonWriter.WriteName("IntermediateDsqReferenceSchema");
			jsonWriter.WriteValueSeparator();
			jsonWriter.Flush();
			if (schema != null)
			{
				NewtonsoftJsonSerializationUtil.ToJsonStream<IntermediateDataShapeReferenceSchema>(schema, stream);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A794 File Offset: 0x00008994
		private static void ToJson<T>(T obj, Stream outputStream)
		{
			new DataContractJsonSerializer(typeof(T)).WriteObject(outputStream, obj);
		}
	}
}
