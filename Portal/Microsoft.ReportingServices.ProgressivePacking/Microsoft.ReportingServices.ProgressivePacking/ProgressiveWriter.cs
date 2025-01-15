using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000012 RID: 18
	internal class ProgressiveWriter : IMessageWriter, IDisposable
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002BCD File Offset: 0x00000DCD
		internal ProgressiveWriter(BinaryWriter writer)
		{
			this.m_writer = writer;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void WriteMessage(string name, object value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			if (StringComparer.OrdinalIgnoreCase.Compare(name, ".") == 0)
			{
				this.m_writer.Write(name);
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = this.VerifyType(name, value.GetType());
			this.VerifyLastStream();
			this.m_writer.Write(name);
			this.InternalWriteValue(value, type);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002C51 File Offset: 0x00000E51
		public Stream CreateWritableStream(string name)
		{
			this.VerifyType(name, typeof(Stream));
			this.VerifyLastStream();
			this.m_lastStream = new LengthEncodedWritableStream(this.m_writer, name);
			return this.m_lastStream;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C83 File Offset: 0x00000E83
		private Type VerifyType(string name, Type type)
		{
			Type type2 = ProgressiveTypeDictionary.GetType(name);
			if (type2 == null)
			{
				throw new NotImplementedException();
			}
			if (!type2.IsAssignableFrom(type))
			{
				throw new ArgumentException("wrong type", "value");
			}
			return type2;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private void InternalWriteValue(object value, Type type)
		{
			if (type == typeof(string))
			{
				this.m_writer.Write(value as string);
				return;
			}
			if (type == typeof(string[]))
			{
				MessageUtil.WriteStringArray(this.m_writer, value as string[]);
				return;
			}
			if (type == typeof(bool))
			{
				this.m_writer.Write((bool)value);
				return;
			}
			if (type == typeof(int))
			{
				this.m_writer.Write((int)value);
				return;
			}
			if (type == typeof(Stream))
			{
				throw new InvalidOperationException("stream");
			}
			if (type == typeof(Dictionary<string, object>))
			{
				this.Write((Dictionary<string, object>)value);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D94 File Offset: 0x00000F94
		private void Write(Dictionary<string, object> value)
		{
			this.m_writer.Write(value.Count);
			foreach (KeyValuePair<string, object> keyValuePair in value)
			{
				if (!(keyValuePair.Value is bool))
				{
					throw new NotImplementedException();
				}
				this.m_writer.Write(3);
				this.m_writer.Write(keyValuePair.Key);
				this.m_writer.Write((bool)keyValuePair.Value);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E38 File Offset: 0x00001038
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				this.m_writer.Write(string.Empty);
				this.m_disposed = true;
				if (this.m_lastStream != null)
				{
					this.m_lastStream.Dispose();
					this.m_lastStream = null;
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E73 File Offset: 0x00001073
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

		// Token: 0x04000027 RID: 39
		internal const string Format = "Progressive";

		// Token: 0x04000028 RID: 40
		internal const int MajorVersion = 1;

		// Token: 0x04000029 RID: 41
		internal const int MinorVersion = 0;

		// Token: 0x0400002A RID: 42
		private BinaryWriter m_writer;

		// Token: 0x0400002B RID: 43
		private bool m_disposed;

		// Token: 0x0400002C RID: 44
		private LengthEncodedWritableStream m_lastStream;
	}
}
