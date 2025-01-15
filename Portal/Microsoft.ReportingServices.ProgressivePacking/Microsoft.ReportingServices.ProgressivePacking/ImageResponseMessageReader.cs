using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000008 RID: 8
	internal class ImageResponseMessageReader : ImageMessageReader<ImageResponseMessageElement>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000022BF File Offset: 0x000004BF
		public ImageResponseMessageReader(IMessageReader reader)
		{
			this.m_reader = reader;
			this.m_enumerator = reader.GetEnumerator();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022DC File Offset: 0x000004DC
		public bool PeekIsErrorResponse(out MessageElement error)
		{
			error = null;
			MessageElement messageElement = this.Peek();
			if (ProgressiveTypeDictionary.IsErrorMessageElement(messageElement))
			{
				error = messageElement;
				return true;
			}
			return false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002301 File Offset: 0x00000501
		private MessageElement Peek()
		{
			if (!this.m_hasCurrentElement && !this.m_isEnumeratorEmpty)
			{
				this.m_hasCurrentElement = this.m_enumerator.MoveNext();
			}
			if (this.m_hasCurrentElement)
			{
				return this.m_enumerator.Current;
			}
			this.m_isEnumeratorEmpty = true;
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002340 File Offset: 0x00000540
		public override IEnumerator<ImageResponseMessageElement> GetEnumerator()
		{
			for (;;)
			{
				MessageElement messageElement = this.Peek();
				this.m_hasCurrentElement = false;
				if (messageElement == null)
				{
					break;
				}
				ImageResponseMessageElement imageResponseMessageElement = this.ReadImageResponseFromMessageElement(messageElement);
				yield return imageResponseMessageElement;
			}
			yield break;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002350 File Offset: 0x00000550
		private ImageResponseMessageElement ReadImageResponseFromMessageElement(MessageElement messageElement)
		{
			Stream stream = messageElement.Value as Stream;
			if (!"getExternalImagesResponse".Equals(messageElement.Name) || stream == null)
			{
				throw new InvalidOperationException("MessageElement is not an image response message element.");
			}
			ImageResponseMessageElement imageResponseMessageElement = new ImageResponseMessageElement();
			using (BinaryReader binaryReader = new BinaryReader(stream, MessageUtil.StringEncoding))
			{
				imageResponseMessageElement.Read(binaryReader);
			}
			return imageResponseMessageElement;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023C0 File Offset: 0x000005C0
		public override void InternalDispose()
		{
			this.m_reader.Dispose();
			this.m_enumerator.Dispose();
		}

		// Token: 0x04000007 RID: 7
		private readonly IMessageReader m_reader;

		// Token: 0x04000008 RID: 8
		private readonly IEnumerator<MessageElement> m_enumerator;

		// Token: 0x04000009 RID: 9
		private bool m_hasCurrentElement;

		// Token: 0x0400000A RID: 10
		private bool m_isEnumeratorEmpty;
	}
}
