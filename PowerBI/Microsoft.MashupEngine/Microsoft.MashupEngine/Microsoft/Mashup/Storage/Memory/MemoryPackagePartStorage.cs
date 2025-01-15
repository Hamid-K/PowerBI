using System;
using System.IO;

namespace Microsoft.Mashup.Storage.Memory
{
	// Token: 0x0200209F RID: 8351
	public class MemoryPackagePartStorage : PackagePartStorage
	{
		// Token: 0x0600CC88 RID: 52360 RVA: 0x0028AC70 File Offset: 0x00288E70
		public MemoryPackagePartStorage()
			: this(new MemoryStream(), true, true)
		{
		}

		// Token: 0x0600CC89 RID: 52361 RVA: 0x0028AC7F File Offset: 0x00288E7F
		private MemoryPackagePartStorage(MemoryStream memoryStream, bool allowEdits, bool enableCompression)
			: this(memoryStream, allowEdits, enableCompression, null)
		{
		}

		// Token: 0x0600CC8A RID: 52362 RVA: 0x0028AC8B File Offset: 0x00288E8B
		private MemoryPackagePartStorage(MemoryStream memoryStream, bool allowEdits, bool enableCompression, ContentStorage contentStorage)
		{
			this.memoryStream = memoryStream;
			this.packagePartStorage = new ZipPackagePartStorage(memoryStream, allowEdits, enableCompression, contentStorage);
		}

		// Token: 0x0600CC8B RID: 52363 RVA: 0x0028ACAC File Offset: 0x00288EAC
		public static MemoryPackagePartStorage Deserialize(byte[] bytes, bool allowEdits = true, bool enableCompression = true)
		{
			MemoryStream memoryStream;
			if (allowEdits)
			{
				memoryStream = new MemoryStream();
				memoryStream.Write(bytes, 0, bytes.Length);
			}
			else
			{
				memoryStream = new MemoryStream(bytes);
			}
			return new MemoryPackagePartStorage(memoryStream, allowEdits, enableCompression);
		}

		// Token: 0x0600CC8C RID: 52364 RVA: 0x0028ACDE File Offset: 0x00288EDE
		public static byte[] Serialize(PackagePartStorage partStorage)
		{
			return MemoryPackagePartStorage.Clone(partStorage).Serialize();
		}

		// Token: 0x0600CC8D RID: 52365 RVA: 0x0028ACEC File Offset: 0x00288EEC
		public static MemoryPackagePartStorage Clone(PackagePartStorage partsStorage)
		{
			MemoryPackagePartStorage memoryPackagePartStorage = new MemoryPackagePartStorage(new MemoryStream(), true, true);
			partsStorage.CopyTo(memoryPackagePartStorage);
			return memoryPackagePartStorage;
		}

		// Token: 0x0600CC8E RID: 52366 RVA: 0x0028AD0E File Offset: 0x00288F0E
		public byte[] Serialize()
		{
			return this.memoryStream.ToArray();
		}

		// Token: 0x0600CC8F RID: 52367 RVA: 0x0028AD1B File Offset: 0x00288F1B
		public override bool TryAddPart(PackagePartType partType, string name, string contentType, byte[] content)
		{
			return this.packagePartStorage.TryAddPart(partType, name, contentType, content);
		}

		// Token: 0x0600CC90 RID: 52368 RVA: 0x0028AD2D File Offset: 0x00288F2D
		public override bool TryRemovePart(PackagePartType partType, string name)
		{
			return this.packagePartStorage.TryRemovePart(partType, name);
		}

		// Token: 0x0600CC91 RID: 52369 RVA: 0x0028AD3C File Offset: 0x00288F3C
		public override bool TryGetPartContent(PackagePartType partType, string name, out string contentType, out byte[] content)
		{
			return this.packagePartStorage.TryGetPartContent(partType, name, out contentType, out content);
		}

		// Token: 0x0600CC92 RID: 52370 RVA: 0x0028AD4E File Offset: 0x00288F4E
		public override bool TrySetPartContent(PackagePartType partType, string name, byte[] content)
		{
			return this.packagePartStorage.TrySetPartContent(partType, name, content);
		}

		// Token: 0x0600CC93 RID: 52371 RVA: 0x0028AD5E File Offset: 0x00288F5E
		public override bool TryGetPartContentType(PackagePartType partType, string name, out string contentType)
		{
			return this.packagePartStorage.TryGetPartContentType(partType, name, out contentType);
		}

		// Token: 0x0600CC94 RID: 52372 RVA: 0x0028AD6E File Offset: 0x00288F6E
		public override string[] GetPartNames(PackagePartType partType)
		{
			return this.packagePartStorage.GetPartNames(partType);
		}

		// Token: 0x0600CC95 RID: 52373 RVA: 0x0028AD7C File Offset: 0x00288F7C
		public override bool HasPart(PackagePartType partType, string name)
		{
			return this.packagePartStorage.HasPart(partType, name);
		}

		// Token: 0x0600CC96 RID: 52374 RVA: 0x0028AD8B File Offset: 0x00288F8B
		public override void Dispose()
		{
			this.packagePartStorage.Dispose();
		}

		// Token: 0x04006794 RID: 26516
		private MemoryStream memoryStream;

		// Token: 0x04006795 RID: 26517
		private ZipPackagePartStorage packagePartStorage;
	}
}
