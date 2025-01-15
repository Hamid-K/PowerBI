using System;
using System.Reflection;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003C RID: 60
	public class JsonContractResolver : DefaultContractResolver
	{
		// Token: 0x0600023E RID: 574 RVA: 0x00007DE6 File Offset: 0x00005FE6
		public JsonContractResolver(MediaTypeFormatter formatter)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this._formatter = formatter;
			base.IgnoreSerializableAttribute = false;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007E0C File Offset: 0x0000600C
		private void ConfigureProperty(MemberInfo member, JsonProperty property)
		{
			if (this._formatter.RequiredMemberSelector != null && this._formatter.RequiredMemberSelector.IsRequiredMember(member))
			{
				property.Required = 1;
				property.DefaultValueHandling = new DefaultValueHandling?(0);
				property.NullValueHandling = new NullValueHandling?(0);
				return;
			}
			property.Required = 0;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007E60 File Offset: 0x00006060
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);
			this.ConfigureProperty(member, jsonProperty);
			return jsonProperty;
		}

		// Token: 0x040000A6 RID: 166
		private readonly MediaTypeFormatter _formatter;
	}
}
