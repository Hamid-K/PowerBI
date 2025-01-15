using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002049 RID: 8265
	public abstract class ContentStorage
	{
		// Token: 0x0600CA41 RID: 51777 RVA: 0x00286E83 File Offset: 0x00285083
		protected ContentStorage(ContentCache contentCache)
		{
			this.contentCache = contentCache;
		}

		// Token: 0x0600CA42 RID: 51778 RVA: 0x00286E92 File Offset: 0x00285092
		public void SetContent(Guid contentID, byte[] content)
		{
			this.SetContent(contentID, content, DateTime.MinValue);
		}

		// Token: 0x0600CA43 RID: 51779 RVA: 0x00286EA1 File Offset: 0x002850A1
		public void SetContent(Guid contentID, byte[] content, DateTime expiresAt)
		{
			this.SetContentCore(contentID, content, expiresAt);
			this.contentCache.AddContent(contentID, content);
		}

		// Token: 0x0600CA44 RID: 51780 RVA: 0x00286EB9 File Offset: 0x002850B9
		public bool TryGetContent(Guid contentID, out byte[] content)
		{
			return this.TryGetContent<byte[]>(contentID, (byte[] bytes) => bytes, out content);
		}

		// Token: 0x0600CA45 RID: 51781 RVA: 0x00286EE4 File Offset: 0x002850E4
		public bool TryGetContent<T>(Guid contentID, Func<byte[], T> deserializer, out T content)
		{
			if (this.contentCache.TryGetContent<T>(contentID, out content))
			{
				return true;
			}
			byte[] array;
			if (this.TryGetContentCore(contentID, out array))
			{
				content = deserializer(array);
				this.contentCache.AddContent<T>(contentID, content, array.Length);
				return true;
			}
			content = default(T);
			return false;
		}

		// Token: 0x0600CA46 RID: 51782 RVA: 0x00286F39 File Offset: 0x00285139
		public byte[] GetContent(Guid contentID)
		{
			return this.GetContent<byte[]>(contentID, (byte[] bytes) => bytes);
		}

		// Token: 0x0600CA47 RID: 51783 RVA: 0x00286F64 File Offset: 0x00285164
		public T GetContent<T>(Guid contentID, Func<byte[], T> deserializer)
		{
			T t;
			if (!this.TryGetContent<T>(contentID, deserializer, out t))
			{
				throw StorageExceptions.StorageException(Strings.Content_Storage_ContentMissing, null);
			}
			return t;
		}

		// Token: 0x0600CA48 RID: 51784
		protected abstract void SetContentCore(Guid contentID, byte[] content, DateTime expiresAt);

		// Token: 0x0600CA49 RID: 51785
		protected abstract bool TryGetContentCore(Guid contentID, out byte[] content);

		// Token: 0x040066DD RID: 26333
		private ContentCache contentCache;
	}
}
