using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000C2 RID: 194
	internal class JRaw : JValue
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x0002A9D8 File Offset: 0x00028BD8
		public static async Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			JRaw jraw;
			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
				{
					await jsonWriter.WriteTokenSyncReadingAsync(reader, cancellationToken).ConfigureAwait(false);
					jraw = new JRaw(sw.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002AA23 File Offset: 0x00028C23
		public JRaw(JRaw other)
			: base(other)
		{
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002AA2C File Offset: 0x00028C2C
		[NullableContext(2)]
		public JRaw(object rawJson)
			: base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002AA38 File Offset: 0x00028C38
		public static JRaw Create(JsonReader reader)
		{
			JRaw jraw;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					jraw = new JRaw(stringWriter.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002AAA0 File Offset: 0x00028CA0
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
