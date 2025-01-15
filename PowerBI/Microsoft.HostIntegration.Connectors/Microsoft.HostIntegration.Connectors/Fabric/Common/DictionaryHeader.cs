using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003CF RID: 975
	internal abstract class DictionaryHeader : SerializableHeader
	{
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00069F17 File Offset: 0x00068117
		public sealed override string Name
		{
			get
			{
				return this.DictionaryName.Value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00069F24 File Offset: 0x00068124
		public sealed override string Namespace
		{
			get
			{
				return this.DictionaryNamespace.Value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002256 RID: 8790
		public abstract XmlDictionaryString DictionaryName { get; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002257 RID: 8791
		public abstract XmlDictionaryString DictionaryNamespace { get; }

		// Token: 0x06002258 RID: 8792 RVA: 0x00069F31 File Offset: 0x00068131
		protected override void OnWriteStartElement(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(this.DictionaryName, this.DictionaryNamespace);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00069F45 File Offset: 0x00068145
		public override bool IsMessageVersionSupported(MessageVersion messageVersion)
		{
			if (messageVersion == null)
			{
				throw new ArgumentNullException("messageVersion");
			}
			return messageVersion.Envelope == EnvelopeVersion.Soap12;
		}
	}
}
