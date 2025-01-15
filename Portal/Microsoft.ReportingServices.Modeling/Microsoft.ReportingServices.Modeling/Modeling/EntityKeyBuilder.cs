using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200009C RID: 156
	public sealed class EntityKeyBuilder : IDisposable
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x00018A9C File Offset: 0x00016C9C
		public EntityKeyBuilder()
		{
			this.m_stream = new MemoryStream(32);
			this.m_binaryWriter = new BinaryWriter(this.m_stream);
			this.m_keyPartWriter = new EntityKey.KeyPartWriter(this.m_binaryWriter);
			this.m_keyHeader = new EntityKey.KeyHeader();
		}

		// Token: 0x170001B1 RID: 433
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x00018AE9 File Offset: 0x00016CE9
		public bool GenerateCaseInsensitiveBase64Strings
		{
			[DebuggerStepThrough]
			set
			{
				this.m_base64SaltingSB = (value ? new StringBuilder() : null);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00018AFC File Offset: 0x00016CFC
		public EntityKey CreateKey(object[] keyParts, Type[] types)
		{
			return EntityKey.FromBase64String(this.CreateKeyAsBase64String(keyParts, types));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00018B0C File Offset: 0x00016D0C
		public string CreateKeyAsBase64String(object[] keyParts, Type[] types)
		{
			if (keyParts == null)
			{
				throw new ArgumentNullException("keyParts");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			if (keyParts.Length == 0 || keyParts.Length != types.Length)
			{
				throw new ArgumentOutOfRangeException("keyParts");
			}
			this.m_stream.Position = 0L;
			this.m_keyHeader.Init(keyParts);
			this.m_keyHeader.WriteTo(this.m_binaryWriter);
			for (int i = 0; i < keyParts.Length; i++)
			{
				if (keyParts[i] != null)
				{
					this.m_keyPartWriter.Write(keyParts[i], types[i]);
				}
			}
			this.m_binaryWriter.Flush();
			int num = (int)this.m_stream.Position;
			this.m_stream.Position = 0L;
			string text = Convert.ToBase64String(this.m_stream.GetBuffer(), 0, num);
			if (this.m_base64SaltingSB != null)
			{
				this.m_base64SaltingSB.Length = 0;
				foreach (char c in text)
				{
					if (char.IsLower(c))
					{
						this.m_base64SaltingSB.Append('.');
					}
					this.m_base64SaltingSB.Append(c);
				}
				text = this.m_base64SaltingSB.ToString();
			}
			return text;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00018C32 File Offset: 0x00016E32
		void IDisposable.Dispose()
		{
			this.m_stream.Close();
			this.m_binaryWriter.Close();
		}

		// Token: 0x04000392 RID: 914
		private readonly MemoryStream m_stream;

		// Token: 0x04000393 RID: 915
		private readonly BinaryWriter m_binaryWriter;

		// Token: 0x04000394 RID: 916
		private readonly EntityKey.KeyPartWriter m_keyPartWriter;

		// Token: 0x04000395 RID: 917
		private readonly EntityKey.KeyHeader m_keyHeader;

		// Token: 0x04000396 RID: 918
		private StringBuilder m_base64SaltingSB;
	}
}
