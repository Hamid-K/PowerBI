using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.BIServer.Configuration.Exceptions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000005 RID: 5
	public class CustomHeaderHelper
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000208C File Offset: 0x0000028C
		public CustomHeaderHelper()
		{
			this._customHeaderXml = StaticConfig.Current.GetOrDefault(ConfigSettings.CustomHeaders.ToString(), string.Empty);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020CE File Offset: 0x000002CE
		public CustomHeaderHelper(string customHeaderXml)
		{
			this._customHeaderXml = customHeaderXml;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020E8 File Offset: 0x000002E8
		public Dictionary<IPathMatcher, Header> GetHeaderRules()
		{
			Dictionary<IPathMatcher, Header> dictionary = new Dictionary<IPathMatcher, Header>();
			List<Header> customHeaders = this.GetCustomHeaders();
			try
			{
				if (customHeaders != null && customHeaders.Count > 0)
				{
					using (List<Header>.Enumerator enumerator = customHeaders.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Header header = enumerator.Current;
							IPathMatcher pathMatcher = new PathMatcher(header.Pattern);
							dictionary.Add(pathMatcher, header);
						}
						goto IL_005E;
					}
					goto IL_0059;
					IL_005E:
					return dictionary;
				}
				IL_0059:
				return dictionary;
			}
			catch (Exception ex)
			{
				throw new ConfigException("Failed to generate custom headers rules due to exception", ex);
			}
			return dictionary;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002184 File Offset: 0x00000384
		private List<Header> GetCustomHeaders()
		{
			List<Header> list = null;
			try
			{
				if (!string.IsNullOrEmpty(this._customHeaderXml) && !string.IsNullOrWhiteSpace(this._customHeaderXml))
				{
					list = (List<Header>)new XmlSerializer(typeof(List<Header>), new XmlRootAttribute("CustomHeaders")).Deserialize(new StringReader(this._customHeaderXml));
				}
			}
			catch (Exception ex)
			{
				throw new ConfigException("Failed to generate custom headers rules due to exception", ex);
			}
			return list;
		}

		// Token: 0x04000031 RID: 49
		private readonly string _customHeaderXml = string.Empty;
	}
}
