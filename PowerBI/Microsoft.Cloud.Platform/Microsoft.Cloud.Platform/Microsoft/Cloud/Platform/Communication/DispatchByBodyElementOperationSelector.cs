using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C6 RID: 1222
	public class DispatchByBodyElementOperationSelector : IDispatchOperationSelector
	{
		// Token: 0x0600254D RID: 9549 RVA: 0x00084A49 File Offset: 0x00082C49
		public DispatchByBodyElementOperationSelector(Dictionary<string, string> dispatchDictionary)
		{
			this.m_dispatchDictionary = dispatchDictionary;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00084A58 File Offset: 0x00082C58
		public string SelectOperation(ref Message message)
		{
			string text = null;
			XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents();
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(readerAtBodyContents.LocalName, readerAtBodyContents.NamespaceURI);
			message = CommunicationUtilities.CreateMessageCopy(message, readerAtBodyContents);
			if (this.m_dispatchDictionary.TryGetValue(xmlQualifiedName.Name, out text))
			{
				return text;
			}
			return this.m_dispatchDictionary["default"];
		}

		// Token: 0x04000D21 RID: 3361
		private const string c_default = "default";

		// Token: 0x04000D22 RID: 3362
		private readonly Dictionary<string, string> m_dispatchDictionary;
	}
}
