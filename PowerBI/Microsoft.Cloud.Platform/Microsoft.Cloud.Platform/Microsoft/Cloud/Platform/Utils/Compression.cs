using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A1 RID: 417
	public static class Compression
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x00024E9C File Offset: 0x0002309C
		public static void DecompressFile(string compressedFilePath, string decompressedFilePath, int bufferSize = 65536)
		{
			string text = Path.Combine(Path.GetDirectoryName(decompressedFilePath), "{0}_{1}.tmp".FormatWithInvariantCulture(new object[]
			{
				Path.GetFileNameWithoutExtension(decompressedFilePath),
				Randomizer.GetS(5, 10)
			}));
			using (FileStream fileStream = new FileStream(compressedFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize))
			{
				using (FileStream fileStream2 = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize))
				{
					using (DeflateStream deflateStream = new DeflateStream(fileStream, CompressionMode.Decompress))
					{
						try
						{
							deflateStream.CopyTo(fileStream2);
						}
						catch (InvalidDataException ex)
						{
							throw new CorruptedFileException(compressedFilePath, text, string.Empty, ex);
						}
					}
				}
			}
			try
			{
				File.Move(text, decompressedFilePath);
			}
			catch (IOException ex2)
			{
				throw new RenameFileAfterDecompressionException(text, decompressedFilePath, compressedFilePath, string.Empty, ex2);
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00024F8C File Offset: 0x0002318C
		public static void CompressDirectory(string inputDirectory, string outputCompressedFile)
		{
			string[] files = Directory.GetFiles(inputDirectory, "*.*", SearchOption.AllDirectories);
			int num = ((inputDirectory[inputDirectory.Length - 1] == Path.DirectorySeparatorChar) ? inputDirectory.Length : (inputDirectory.Length + 1));
			string text = Path.GetFileName(outputCompressedFile);
			if (text == null)
			{
				text = "comp.tmp";
				outputCompressedFile = Path.Combine(outputCompressedFile, text);
			}
			string text2 = Path.Combine(Path.GetTempPath(), text);
			using (FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
				{
					string[] array = files;
					for (int i = 0; i < array.Length; i++)
					{
						string text3 = array[i].Substring(num);
						Compression.CompressFile(inputDirectory, text3, gzipStream);
					}
				}
			}
			File.Move(text2, outputCompressedFile);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00025070 File Offset: 0x00023270
		public static void DecompressToDirectory(string inputCompressedFile, string outputDirectory)
		{
			using (FileStream fileStream = new FileStream(inputCompressedFile, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress, true))
				{
					while (Compression.TryDecompressFile(outputDirectory, gzipStream))
					{
					}
				}
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000250CC File Offset: 0x000232CC
		public static byte[] CompressString(string str)
		{
			if (str == null)
			{
				return null;
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					using (StreamWriter streamWriter = new StreamWriter(gzipStream))
					{
						streamWriter.Write(str);
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00025150 File Offset: 0x00023350
		public static string DecompressString(byte[] compressedBinary)
		{
			if (compressedBinary == null)
			{
				return null;
			}
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressedBinary))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					using (StreamReader streamReader = new StreamReader(gzipStream))
					{
						try
						{
							text = streamReader.ReadToEnd();
						}
						catch (Exception ex)
						{
							if (!(ex is ArgumentNullException) && !(ex is InvalidOperationException) && !(ex is ArgumentOutOfRangeException) && !(ex is InvalidDataException) && !(ex is ObjectDisposedException))
							{
								throw;
							}
							TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Unexpected exception decompressing tile data. {0}", new object[] { ex });
							text = null;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00025224 File Offset: 0x00023424
		private static void CompressFile(string sDir, string sRelativePath, GZipStream zipStream)
		{
			char[] array = sRelativePath.ToCharArray();
			zipStream.Write(BitConverter.GetBytes(array.Length), 0, 4);
			foreach (char c in array)
			{
				zipStream.Write(BitConverter.GetBytes(c), 0, 2);
			}
			byte[] array3 = File.ReadAllBytes(Path.Combine(sDir, sRelativePath));
			zipStream.Write(BitConverter.GetBytes(array3.Length), 0, 4);
			zipStream.Write(array3, 0, array3.Length);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00025294 File Offset: 0x00023494
		private static bool TryDecompressFile(string sDir, GZipStream zipStream)
		{
			byte[] array = new byte[4];
			if (zipStream.Read(array, 0, 4) < 4)
			{
				return false;
			}
			int num = BitConverter.ToInt32(array, 0);
			array = new byte[2];
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				zipStream.Read(array, 0, 2);
				char c = BitConverter.ToChar(array, 0);
				stringBuilder.Append(c);
			}
			string text = stringBuilder.ToString();
			array = new byte[4];
			zipStream.Read(array, 0, 4);
			int num2 = BitConverter.ToInt32(array, 0);
			array = new byte[num2];
			zipStream.Read(array, 0, array.Length);
			string text2 = Path.Combine(sDir, text);
			string directoryName = Path.GetDirectoryName(text2);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				fileStream.Write(array, 0, num2);
			}
			return true;
		}

		// Token: 0x04000437 RID: 1079
		private const int c_defaultBufferSize = 65536;
	}
}
