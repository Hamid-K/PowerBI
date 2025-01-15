using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000009 RID: 9
	internal sealed class ProgressivePackageReader : IDisposable
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		public ProgressivePackageReader(Stream packageStream)
		{
			this.m_messageReader = MessageFormatterFactory.CreateReader(packageStream);
			this.m_enumerator = this.m_messageReader.GetEnumerator();
			this.m_hasCurrentElement = false;
			this.m_enumeratorIsEmpty = false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		public MessageElement PeekElement()
		{
			try
			{
				if (!this.m_hasCurrentElement && !this.m_enumeratorIsEmpty)
				{
					this.m_hasCurrentElement = this.m_enumerator.MoveNext();
				}
				if (this.m_hasCurrentElement)
				{
					return this.m_enumerator.Current;
				}
				this.m_enumeratorIsEmpty = true;
			}
			catch (IOException ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error reading element: {0}.", new object[] { ex.ToString() });
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002240 File Offset: 0x00000440
		public MessageElement ConsumeElement()
		{
			MessageElement messageElement = this.PeekElement();
			this.m_hasCurrentElement = false;
			return messageElement;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002250 File Offset: 0x00000450
		public MessageElement ConsumeRequiredElement(string name)
		{
			MessageElement messageElement = this.ConsumeElement();
			if (!this.IsTargetElement(messageElement, name))
			{
				throw new ProgressiveFormatElementMissingException(name);
			}
			return messageElement;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002276 File Offset: 0x00000476
		public T ConsumeRequiredValue<T>(string name)
		{
			return (T)((object)this.ConsumeRequiredElement(name).Value);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000228C File Offset: 0x0000048C
		public MessageElement ConsumeOptionalElement(string name)
		{
			MessageElement messageElement = this.PeekElement();
			if (this.IsTargetElement(messageElement, name))
			{
				this.m_hasCurrentElement = false;
				return messageElement;
			}
			return null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022B4 File Offset: 0x000004B4
		public T ConsumeOptionalValue<T>(string name)
		{
			MessageElement messageElement = this.ConsumeOptionalElement(name);
			if (messageElement != null)
			{
				RSTrace.CatalogTrace.Assert(messageElement.Value is T, "Incorrect value type for element: {0}.", new object[] { name });
				return (T)((object)messageElement.Value);
			}
			return default(T);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002308 File Offset: 0x00000508
		private bool IsTargetElement(MessageElement element, string name)
		{
			return element != null && string.Equals(element.Name, name, StringComparison.Ordinal);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000231C File Offset: 0x0000051C
		public void Dispose()
		{
			if (this.m_messageReader == null)
			{
				return;
			}
			this.m_enumerator.Dispose();
			this.m_enumerator = null;
			this.m_messageReader.Dispose();
			this.m_messageReader = null;
		}

		// Token: 0x0400005A RID: 90
		private IMessageReader m_messageReader;

		// Token: 0x0400005B RID: 91
		private IEnumerator<MessageElement> m_enumerator;

		// Token: 0x0400005C RID: 92
		private bool m_hasCurrentElement;

		// Token: 0x0400005D RID: 93
		private bool m_enumeratorIsEmpty;
	}
}
