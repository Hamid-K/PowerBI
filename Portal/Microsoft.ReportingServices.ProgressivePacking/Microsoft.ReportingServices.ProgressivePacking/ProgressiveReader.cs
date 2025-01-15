using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000011 RID: 17
	internal class ProgressiveReader : IMessageReader, IEnumerable<MessageElement>, IEnumerable, IDisposable
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002A24 File Offset: 0x00000C24
		internal ProgressiveReader(BinaryReader reader)
		{
			this.m_reader = reader;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A34 File Offset: 0x00000C34
		private object InternalReadValue(Type type)
		{
			if (type == typeof(string))
			{
				return this.m_reader.ReadString();
			}
			if (type == typeof(string[]))
			{
				return MessageUtil.ReadStringArray(this.m_reader);
			}
			if (type == typeof(bool))
			{
				return this.m_reader.ReadBoolean();
			}
			if (type == typeof(int))
			{
				return this.m_reader.ReadInt32();
			}
			if (type == typeof(Stream))
			{
				this.m_lastStream = new LengthEncodedReadableStream(this.m_reader);
				return this.m_lastStream;
			}
			if (type == typeof(Dictionary<string, object>))
			{
				return this.ReadDictionary();
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B0C File Offset: 0x00000D0C
		private Dictionary<string, object> ReadDictionary()
		{
			int num = this.m_reader.ReadInt32();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(num);
			for (int i = 0; i < num; i++)
			{
				if (this.m_reader.ReadInt32() != 3)
				{
					throw new NotImplementedException();
				}
				dictionary.Add(this.m_reader.ReadString(), this.m_reader.ReadBoolean());
			}
			return dictionary;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B70 File Offset: 0x00000D70
		public IEnumerator<MessageElement> GetEnumerator()
		{
			if (this.m_enumeratorCreated)
			{
				throw new InvalidOperationException();
			}
			this.m_enumeratorCreated = true;
			while (this.m_reader.BaseStream.CanRead)
			{
				string text;
				object obj;
				try
				{
					this.VerifyLastStream();
					text = this.m_reader.ReadString();
					if (string.IsNullOrEmpty(text))
					{
						yield break;
					}
					if (StringComparer.OrdinalIgnoreCase.Compare(text, ".") == 0)
					{
						continue;
					}
					Type type = ProgressiveTypeDictionary.GetType(text);
					if (type == null)
					{
						throw new NotImplementedException();
					}
					obj = this.InternalReadValue(type);
				}
				catch (EndOfStreamException ex)
				{
					throw new IOException("end of stream", ex);
				}
				catch (IOException)
				{
					throw;
				}
				catch (Exception ex2)
				{
					throw new IOException("reader", ex2);
				}
				yield return new MessageElement(text, obj);
			}
			yield break;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B7F File Offset: 0x00000D7F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<MessageElement>)this).GetEnumerator();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B87 File Offset: 0x00000D87
		private void VerifyLastStream()
		{
			if (this.m_lastStream == null)
			{
				return;
			}
			if (this.m_lastStream.Closed)
			{
				this.m_lastStream = null;
				return;
			}
			throw new InvalidOperationException("last stream");
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public void Dispose()
		{
			if (this.m_lastStream != null)
			{
				this.m_lastStream.Dispose();
				this.m_lastStream = null;
			}
		}

		// Token: 0x04000021 RID: 33
		internal const string Format = "Progressive";

		// Token: 0x04000022 RID: 34
		internal const int MajorVersion = 1;

		// Token: 0x04000023 RID: 35
		internal const int MinorVersion = 0;

		// Token: 0x04000024 RID: 36
		private BinaryReader m_reader;

		// Token: 0x04000025 RID: 37
		private LengthEncodedReadableStream m_lastStream;

		// Token: 0x04000026 RID: 38
		private bool m_enumeratorCreated;
	}
}
