using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000083 RID: 131
	internal abstract class DataShapeResultParserBase
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00008605 File Offset: 0x00006805
		protected DataShapeResultParserBase(DsrNames names)
		{
			this.Names = names;
		}

		// Token: 0x06000307 RID: 775
		protected abstract IList<DataShape> ParseDataShapes(JsonReader reader, JsonSerializer serializer);

		// Token: 0x06000308 RID: 776 RVA: 0x00008614 File Offset: 0x00006814
		internal DataShapeResult ParseDsr(JsonReader reader, JsonSerializer serializer)
		{
			DataShapeResultParserBase.ValidateReaderState(reader, "DataShapes property", JsonToken.PropertyName, this.Names.DataShapes);
			DataShapeResultParserBase.ReadNext(reader, "DataShapes array start", JsonToken.StartArray, null);
			DataShapeResult dataShapeResult = new DataShapeResult();
			dataShapeResult.DataShapes = this.ParseDataShapes(reader, serializer);
			DataShapeResultParserBase.ReadNext(reader, "DSR end.", JsonToken.EndObject, null);
			return dataShapeResult;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00008666 File Offset: 0x00006866
		protected static void ReadNext(JsonReader reader, string readLocation, JsonToken expectedTokenType, string expectedPropertyName = null)
		{
			if (!reader.Read())
			{
				throw new JsonSerializationException(StringUtil.FormatInvariant("Unexpected end of json input at {0}.", readLocation));
			}
			DataShapeResultParserBase.ValidateReaderState(reader, readLocation, expectedTokenType, expectedPropertyName);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000868C File Offset: 0x0000688C
		protected static void ValidateReaderState(JsonReader reader, string readLocation, JsonToken expectedTokenType, string expectedPropertyName = null)
		{
			if (reader.TokenType != expectedTokenType)
			{
				throw new JsonSerializationException(StringUtil.FormatInvariant("Next token should be {0}, not {1}.", readLocation, reader.TokenType));
			}
			if (expectedPropertyName != null && (string)reader.Value != expectedPropertyName)
			{
				throw new JsonSerializationException(StringUtil.FormatInvariant("Unexpected Property at {0}. Expected {1}, not {2}.", readLocation, expectedPropertyName, (string)reader.Value));
			}
		}

		// Token: 0x040001B2 RID: 434
		protected readonly DsrNames Names;
	}
}
