using System;
using System.Text;

namespace NLog.Targets
{
	// Token: 0x02000041 RID: 65
	internal class JsonConverterLegacy : IJsonConverter, IJsonSerializer
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x000106D9 File Offset: 0x0000E8D9
		public JsonConverterLegacy(IJsonSerializer jsonSerializer)
		{
			this._jsonSerializer = jsonSerializer;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000106E8 File Offset: 0x0000E8E8
		public bool SerializeObject(object value, StringBuilder builder)
		{
			string text = this._jsonSerializer.SerializeObject(value);
			if (text == null)
			{
				return false;
			}
			builder.Append(text);
			return true;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00010710 File Offset: 0x0000E910
		string IJsonSerializer.SerializeObject(object value)
		{
			return this._jsonSerializer.SerializeObject(value);
		}

		// Token: 0x0400011B RID: 283
		private readonly IJsonSerializer _jsonSerializer;
	}
}
