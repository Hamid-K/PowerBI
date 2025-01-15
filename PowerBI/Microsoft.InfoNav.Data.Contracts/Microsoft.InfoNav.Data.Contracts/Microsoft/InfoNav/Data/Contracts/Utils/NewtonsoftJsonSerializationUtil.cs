using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.Utils
{
	// Token: 0x02000093 RID: 147
	public static class NewtonsoftJsonSerializationUtil
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00009716 File Offset: 0x00007916
		public static void ToJsonStream<T>(T obj, Stream stream)
		{
			NewtonsoftJsonSerializationUtil.ToJsonStream<T>(obj, stream, NewtonsoftJsonSerializationUtil.JsonSerializer);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009724 File Offset: 0x00007924
		public static void ToJsonStream<T>(T obj, Stream stream, JsonSerializer serializer)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream, NewtonsoftJsonSerializationUtil.Utf8noBOM, NewtonsoftJsonSerializationUtil.DefaultStreamWriterBufferSize, true))
			{
				serializer.Serialize(streamWriter, obj);
			}
		}

		// Token: 0x040001CB RID: 459
		private static readonly JsonSerializer JsonSerializer = JsonSerializer.Create();

		// Token: 0x040001CC RID: 460
		private static readonly int DefaultStreamWriterBufferSize = 1024;

		// Token: 0x040001CD RID: 461
		private static readonly UTF8Encoding Utf8noBOM = new UTF8Encoding(false, true);
	}
}
