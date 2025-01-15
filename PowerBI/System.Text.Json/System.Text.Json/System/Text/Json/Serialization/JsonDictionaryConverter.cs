using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200008A RID: 138
	internal abstract class JsonDictionaryConverter<TDictionary> : JsonResumableConverter<TDictionary>
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00025688 File Offset: 0x00023888
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002568B File Offset: 0x0002388B
		private protected sealed override ConverterStrategy GetDefaultConverterStrategy()
		{
			return ConverterStrategy.Dictionary;
		}

		// Token: 0x06000869 RID: 2153
		protected internal abstract bool OnWriteResume(Utf8JsonWriter writer, TDictionary dictionary, JsonSerializerOptions options, ref WriteStack state);
	}
}
