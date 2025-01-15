using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002144 RID: 8516
	internal abstract class OpenXmlWriter : IDisposable
	{
		// Token: 0x0600D3A1 RID: 54177 RVA: 0x0029FB6B File Offset: 0x0029DD6B
		public static OpenXmlWriter Create(OpenXmlPart openXmlPart)
		{
			return new OpenXmlPartWriter(openXmlPart);
		}

		// Token: 0x0600D3A2 RID: 54178 RVA: 0x0029FB73 File Offset: 0x0029DD73
		public static OpenXmlWriter Create(OpenXmlPart openXmlPart, Encoding encoding)
		{
			return new OpenXmlPartWriter(openXmlPart, encoding);
		}

		// Token: 0x0600D3A3 RID: 54179 RVA: 0x0029FB7C File Offset: 0x0029DD7C
		public static OpenXmlWriter Create(Stream partStream)
		{
			return new OpenXmlPartWriter(partStream);
		}

		// Token: 0x0600D3A4 RID: 54180 RVA: 0x0029FB84 File Offset: 0x0029DD84
		public static OpenXmlWriter Create(Stream partStream, Encoding encoding)
		{
			return new OpenXmlPartWriter(partStream, encoding);
		}

		// Token: 0x0600D3A5 RID: 54181
		public abstract void WriteStartDocument();

		// Token: 0x0600D3A6 RID: 54182
		public abstract void WriteStartDocument(bool standalone);

		// Token: 0x0600D3A7 RID: 54183
		public abstract void WriteStartElement(OpenXmlReader elementReader);

		// Token: 0x0600D3A8 RID: 54184
		public abstract void WriteStartElement(OpenXmlReader elementReader, IEnumerable<OpenXmlAttribute> attributes);

		// Token: 0x0600D3A9 RID: 54185
		public abstract void WriteStartElement(OpenXmlReader elementReader, IEnumerable<OpenXmlAttribute> attributes, IEnumerable<KeyValuePair<string, string>> namespaceDeclarations);

		// Token: 0x0600D3AA RID: 54186
		public abstract void WriteStartElement(OpenXmlElement elementObject);

		// Token: 0x0600D3AB RID: 54187
		public abstract void WriteStartElement(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes);

		// Token: 0x0600D3AC RID: 54188
		public abstract void WriteStartElement(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes, IEnumerable<KeyValuePair<string, string>> namespaceDeclarations);

		// Token: 0x0600D3AD RID: 54189
		public abstract void WriteEndElement();

		// Token: 0x0600D3AE RID: 54190
		public abstract void WriteElement(OpenXmlElement elementObject);

		// Token: 0x0600D3AF RID: 54191
		public abstract void WriteString(string text);

		// Token: 0x0600D3B0 RID: 54192
		public abstract void Close();

		// Token: 0x0600D3B1 RID: 54193 RVA: 0x0029FB8D File Offset: 0x0029DD8D
		protected virtual void ThrowIfObjectDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600D3B2 RID: 54194 RVA: 0x0029FBA8 File Offset: 0x0029DDA8
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					this.Close();
				}
				this._disposed = true;
			}
		}

		// Token: 0x0600D3B3 RID: 54195 RVA: 0x0029FBC2 File Offset: 0x0029DDC2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400699F RID: 27039
		private bool _disposed;
	}
}
