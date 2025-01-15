using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000C3 RID: 195
	[NullableContext(1)]
	[Nullable(0)]
	internal class JRaw : JValue
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002B0C8 File Offset: 0x000292C8
		[NullableContext(0)]
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

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002B113 File Offset: 0x00029313
		public JRaw(JRaw other)
			: base(other)
		{
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002B11C File Offset: 0x0002931C
		[NullableContext(2)]
		public JRaw(object rawJson)
			: base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002B128 File Offset: 0x00029328
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

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002B190 File Offset: 0x00029390
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
