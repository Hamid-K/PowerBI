using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200000C RID: 12
	public abstract class AutodiscoverResponseCollection<TResponse> : AutodiscoverResponse, IEnumerable<TResponse>, IEnumerable where TResponse : AutodiscoverResponse
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000027C2 File Offset: 0x000017C2
		internal AutodiscoverResponseCollection()
		{
			this.responses = new List<TResponse>();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027D5 File Offset: 0x000017D5
		public int Count
		{
			get
			{
				return this.responses.Count;
			}
		}

		// Token: 0x17000015 RID: 21
		public TResponse this[int index]
		{
			get
			{
				return this.responses[index];
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000027F0 File Offset: 0x000017F0
		internal List<TResponse> Responses
		{
			get
			{
				return this.responses;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027F8 File Offset: 0x000017F8
		internal override void LoadFromXml(EwsXmlReader reader, string endElementName)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					if (reader.LocalName == this.GetResponseCollectionXmlElementName())
					{
						this.LoadResponseCollectionFromXml(reader);
					}
					else
					{
						base.LoadFromXml(reader, endElementName);
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, endElementName));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002838 File Offset: 0x00001838
		private void LoadResponseCollectionFromXml(EwsXmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1 && reader.LocalName == this.GetResponseInstanceXmlElementName())
					{
						TResponse tresponse = this.CreateResponseInstance();
						tresponse.LoadFromXml(reader, this.GetResponseInstanceXmlElementName());
						this.Responses.Add(tresponse);
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Autodiscover, this.GetResponseCollectionXmlElementName()));
			}
		}

		// Token: 0x06000036 RID: 54
		internal abstract string GetResponseCollectionXmlElementName();

		// Token: 0x06000037 RID: 55
		internal abstract string GetResponseInstanceXmlElementName();

		// Token: 0x06000038 RID: 56
		internal abstract TResponse CreateResponseInstance();

		// Token: 0x06000039 RID: 57 RVA: 0x000028A6 File Offset: 0x000018A6
		public IEnumerator<TResponse> GetEnumerator()
		{
			return this.responses.GetEnumerator();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028B8 File Offset: 0x000018B8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.responses.GetEnumerator();
		}

		// Token: 0x04000018 RID: 24
		private List<TResponse> responses;
	}
}
