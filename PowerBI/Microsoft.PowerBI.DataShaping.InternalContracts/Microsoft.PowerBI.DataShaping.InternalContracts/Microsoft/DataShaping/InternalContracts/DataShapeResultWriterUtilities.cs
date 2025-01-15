using System;
using System.IO;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200000C RID: 12
	internal static class DataShapeResultWriterUtilities
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021F8 File Offset: 0x000003F8
		internal static void WriteErrorDsr(IStreamingStructureEncodedWriter writer, bool enableRemoteErrors, HandledExceptionWrapper exWrapper, string dataShapeId, IFeatureSwitchProvider featureSwitchProvider)
		{
			DataShapeResultWriter dataShapeResultWriter = StreamingDsrWriterWrapperBase.CreateWriter<DataShapeResultWriter>(writer, DsrNames.V1);
			dataShapeResultWriter.Begin();
			CollectionWriter<DataShapeWriter> collectionWriter = dataShapeResultWriter.BeginDataShapes();
			DataShapeWriter dataShapeWriter = collectionWriter.BeginItem();
			if (dataShapeId != null)
			{
				dataShapeWriter.WriteId(dataShapeId);
			}
			ErrorWriter errorWriter = dataShapeWriter.BeginError();
			errorWriter.WriteException(exWrapper, enableRemoteErrors, featureSwitchProvider);
			errorWriter.End();
			dataShapeWriter.End();
			collectionWriter.End();
			dataShapeResultWriter.End();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002254 File Offset: 0x00000454
		internal static void WriteErrorDsr(Stream stream, bool enableRemoteErrors, HandledExceptionWrapper exWrapper, string dataShapeId, IFeatureSwitchProvider featureSwitchProvider)
		{
			using (JsonStreamingStructureWriter jsonStreamingStructureWriter = new JsonStreamingStructureWriter(stream))
			{
				DataShapeResultWriterUtilities.WriteErrorDsr(jsonStreamingStructureWriter, enableRemoteErrors, exWrapper, dataShapeId, featureSwitchProvider);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002290 File Offset: 0x00000490
		internal static int ToConceptualTypeCode(this Type columnType)
		{
			return (int)ConceptualTypeConverter.ToConceptualType(columnType);
		}
	}
}
