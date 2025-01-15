using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Json
{
	// Token: 0x0200002B RID: 43
	public sealed class DefaultJsonConverter : JsonConverter
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003516 File Offset: 0x00001716
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003519 File Offset: 0x00001719
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000351C File Offset: 0x0000171C
		public override bool CanConvert(Type objectType)
		{
			return false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000351F File Offset: 0x0000171F
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw Contract.ExceptNotSupported("ReadJson called on DefaultJsonConverter.");
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000352B File Offset: 0x0000172B
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw Contract.ExceptNotSupported("WriteJson called on DefaultJsonConverter.");
		}
	}
}
