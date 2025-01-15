using System;
using System.Xml;

namespace NLog.Internal.Fakeables
{
	// Token: 0x0200016C RID: 364
	internal interface IFileSystem
	{
		// Token: 0x06001113 RID: 4371
		bool FileExists(string path);

		// Token: 0x06001114 RID: 4372
		XmlReader LoadXmlFile(string path);
	}
}
