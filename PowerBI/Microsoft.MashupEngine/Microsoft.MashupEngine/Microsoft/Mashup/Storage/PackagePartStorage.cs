using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002085 RID: 8325
	public abstract class PackagePartStorage : IDisposable
	{
		// Token: 0x0600CBAC RID: 52140 RVA: 0x0028915F File Offset: 0x0028735F
		public static PackagePartType[] GetPartTypes()
		{
			return PackagePartStorage.partTypes;
		}

		// Token: 0x0600CBAD RID: 52141
		public abstract bool HasPart(PackagePartType partType, string name);

		// Token: 0x0600CBAE RID: 52142
		public abstract bool TryAddPart(PackagePartType partType, string name, string contentType, byte[] content);

		// Token: 0x0600CBAF RID: 52143
		public abstract bool TryRemovePart(PackagePartType partType, string name);

		// Token: 0x0600CBB0 RID: 52144
		public abstract bool TryGetPartContent(PackagePartType partType, string name, out string contentType, out byte[] content);

		// Token: 0x0600CBB1 RID: 52145
		public abstract bool TrySetPartContent(PackagePartType partType, string name, byte[] content);

		// Token: 0x0600CBB2 RID: 52146
		public abstract bool TryGetPartContentType(PackagePartType partType, string name, out string contentType);

		// Token: 0x0600CBB3 RID: 52147
		public abstract string[] GetPartNames(PackagePartType partType);

		// Token: 0x0600CBB4 RID: 52148
		public abstract void Dispose();

		// Token: 0x0600CBB5 RID: 52149 RVA: 0x00289168 File Offset: 0x00287368
		public void Clear()
		{
			foreach (PackagePartType packagePartType in PackagePartStorage.partTypes)
			{
				foreach (string text in this.GetPartNames(packagePartType))
				{
					if (!this.TryRemovePart(packagePartType, text))
					{
						throw new InvalidOperationException();
					}
				}
			}
		}

		// Token: 0x0600CBB6 RID: 52150 RVA: 0x002891C0 File Offset: 0x002873C0
		public void CopyTo(PackagePartStorage packagePartStorage)
		{
			foreach (PackagePartType packagePartType in PackagePartStorage.partTypes)
			{
				foreach (string text in this.GetPartNames(packagePartType))
				{
					string text2;
					byte[] array2;
					if (!this.TryGetPartContent(packagePartType, text, out text2, out array2))
					{
						throw new InvalidOperationException();
					}
					if (!packagePartStorage.TryAddPart(packagePartType, text, text2, array2))
					{
						throw new InvalidOperationException();
					}
				}
			}
		}

		// Token: 0x0600CBB7 RID: 52151 RVA: 0x00289230 File Offset: 0x00287430
		public byte[] GetHash(HashAlgorithm hash)
		{
			HashStream hashStream = new HashStream(hash);
			this.WriteCanonicalizedContents(hashStream);
			return hashStream.FinishAndGetHash();
		}

		// Token: 0x0600CBB8 RID: 52152 RVA: 0x00289251 File Offset: 0x00287451
		private void WriteCanonicalizedContents(Stream stream)
		{
			PackagePartStorage.CanonicalWriter.Write(stream, this);
		}

		// Token: 0x0600CBB9 RID: 52153 RVA: 0x0028925C File Offset: 0x0028745C
		public static bool PackagePartsEqual(PackagePartStorage packagePartStorage1, PackagePartStorage packagePartStorage2)
		{
			PackagePartType[] array = PackagePartStorage.GetPartTypes();
			for (int i = 0; i < array.Length; i++)
			{
				if (!PackagePartStorage.PackagePartsEqual(array[i], packagePartStorage1, packagePartStorage2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600CBBA RID: 52154 RVA: 0x0028928C File Offset: 0x0028748C
		public static bool PackagePartsEqual(PackagePartType partType, PackagePartStorage packagePartStorage1, PackagePartStorage packagePartStorage2)
		{
			string[] partNames = packagePartStorage1.GetPartNames(partType);
			string[] partNames2 = packagePartStorage2.GetPartNames(partType);
			if (partNames.Length != partNames2.Length)
			{
				return false;
			}
			foreach (string text in partNames)
			{
				string text2;
				byte[] array2;
				if (!packagePartStorage1.TryGetPartContent(partType, text, out text2, out array2))
				{
					throw new InvalidOperationException();
				}
				string text3;
				byte[] array3;
				if (packagePartStorage2.TryGetPartContent(partType, text, out text3, out array3))
				{
					if (string.Compare(text2, text3, StringComparison.OrdinalIgnoreCase) != 0)
					{
						return false;
					}
					if (!PackagePartStorage.BytesEqual(array2, array3))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600CBBB RID: 52155 RVA: 0x00289308 File Offset: 0x00287508
		internal static bool BytesEqual(byte[] bytes1, byte[] bytes2)
		{
			if (bytes1.Length != bytes2.Length)
			{
				return false;
			}
			for (int i = 0; i < bytes1.Length; i++)
			{
				if (bytes1[i] != bytes2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600CBBD RID: 52157 RVA: 0x00289338 File Offset: 0x00287538
		// Note: this type is marked as 'beforefieldinit'.
		static PackagePartStorage()
		{
			PackagePartType[] array = new PackagePartType[3];
			array[0] = PackagePartType.Config;
			array[1] = PackagePartType.Content;
			PackagePartStorage.partTypes = array;
		}

		// Token: 0x0400675E RID: 26462
		private static readonly PackagePartType[] partTypes;

		// Token: 0x02002086 RID: 8326
		private static class CanonicalWriter
		{
			// Token: 0x0600CBBE RID: 52158 RVA: 0x00289350 File Offset: 0x00287550
			public static void Write(Stream stream, PackagePartStorage packagePartStorage)
			{
				foreach (PackagePartType packagePartType in PackagePartStorage.GetPartTypes())
				{
					string[] partNames = packagePartStorage.GetPartNames(packagePartType);
					Array.Sort<string>(partNames);
					foreach (string text in partNames)
					{
						string text2;
						byte[] array2;
						if (!packagePartStorage.TryGetPartContent(packagePartType, text, out text2, out array2))
						{
							throw new InvalidOperationException();
						}
						PackagePartStorage.CanonicalWriter.Write(stream, packagePartType.ToString());
						PackagePartStorage.CanonicalWriter.Write(stream, text);
						PackagePartStorage.CanonicalWriter.Write(stream, text2.ToUpperInvariant());
						PackagePartStorage.CanonicalWriter.Write(stream, array2);
					}
				}
			}

			// Token: 0x0600CBBF RID: 52159 RVA: 0x002893E0 File Offset: 0x002875E0
			private static void Write(Stream stream, string data)
			{
				PackagePartStorage.CanonicalWriter.Write(stream, PackagePartStorage.CanonicalWriter.utf8WithoutPreamble.GetBytes(data));
			}

			// Token: 0x0600CBC0 RID: 52160 RVA: 0x002893F4 File Offset: 0x002875F4
			private static void Write(Stream stream, byte[] data)
			{
				byte[] bytes = BitConverter.GetBytes(data.Length);
				stream.Write(bytes, 0, bytes.Length);
				stream.Write(data, 0, data.Length);
			}

			// Token: 0x0400675F RID: 26463
			private static readonly Encoding utf8WithoutPreamble = new UTF8Encoding(false);
		}
	}
}
