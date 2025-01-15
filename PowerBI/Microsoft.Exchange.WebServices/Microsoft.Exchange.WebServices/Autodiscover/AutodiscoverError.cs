using System;
using System.ComponentModel;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200000A RID: 10
	[EditorBrowsable(1)]
	[Serializable]
	public sealed class AutodiscoverError
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000263C File Offset: 0x0000163C
		private AutodiscoverError()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002644 File Offset: 0x00001644
		internal static AutodiscoverError Parse(EwsXmlReader reader)
		{
			AutodiscoverError autodiscoverError = new AutodiscoverError();
			autodiscoverError.time = reader.ReadAttributeValue("Time");
			autodiscoverError.id = reader.ReadAttributeValue("Id");
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					string localName;
					if ((localName = reader.LocalName) != null)
					{
						if (localName == "ErrorCode")
						{
							autodiscoverError.errorCode = reader.ReadElementValue<int>();
							goto IL_009A;
						}
						if (localName == "Message")
						{
							autodiscoverError.message = reader.ReadElementValue();
							goto IL_009A;
						}
						if (localName == "DebugData")
						{
							autodiscoverError.debugData = reader.ReadElementValue();
							goto IL_009A;
						}
					}
					reader.SkipCurrentElement();
				}
				IL_009A:;
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, "Error"));
			return autodiscoverError;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026FA File Offset: 0x000016FA
		public string Time
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002702 File Offset: 0x00001702
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000270A File Offset: 0x0000170A
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002712 File Offset: 0x00001712
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000271A File Offset: 0x0000171A
		public string DebugData
		{
			get
			{
				return this.debugData;
			}
		}

		// Token: 0x04000010 RID: 16
		private string time;

		// Token: 0x04000011 RID: 17
		private string id;

		// Token: 0x04000012 RID: 18
		private int errorCode;

		// Token: 0x04000013 RID: 19
		private string message;

		// Token: 0x04000014 RID: 20
		private string debugData;
	}
}
