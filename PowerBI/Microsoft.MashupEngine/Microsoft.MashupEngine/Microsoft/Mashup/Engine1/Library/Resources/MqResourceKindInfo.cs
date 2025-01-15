using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.MQ;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000520 RID: 1312
	internal class MqResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x06002A42 RID: 10818 RVA: 0x0007E934 File Offset: 0x0007CB34
		public MqResourceKindInfo()
			: base("MQ", null, false, false, false, true, false, false, new AuthenticationInfo[]
			{
				new UsernamePasswordAuthenticationInfo(),
				new ImplicitAuthenticationInfo()
			}, null, null, new string[] { "EffectiveUserName" }, new DataSourceLocationFactory[] { MqDataSourceLocation.Factory })
		{
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0007E988 File Offset: 0x0007CB88
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			string text;
			string text2;
			string text3;
			string text4;
			if (!MqResource.TryParsePath(resourcePath, out text, out text2, out text3, out text4))
			{
				errorMessage = Strings.Resource_Mq_Invalid;
				resource = null;
				return false;
			}
			resource = MqResource.New(text, text2, text3, text4);
			errorMessage = null;
			return true;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x0007E9C8 File Offset: 0x0007CBC8
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			string text;
			string text2;
			string text3;
			return MqResource.TryParsePath(resourcePath, out hostName, out text, out text2, out text3);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0007E9E2 File Offset: 0x0007CBE2
		public override bool IsSubset(string permittedResourcePath, string attemptedResourcePath)
		{
			return MqResource.IsSubPath(permittedResourcePath, attemptedResourcePath);
		}
	}
}
