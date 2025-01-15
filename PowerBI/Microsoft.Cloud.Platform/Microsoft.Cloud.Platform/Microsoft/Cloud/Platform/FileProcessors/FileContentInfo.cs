using System;
using System.IO;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000EA RID: 234
	public sealed class FileContentInfo : IFileContentInfo
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00017C50 File Offset: 0x00015E50
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00017C58 File Offset: 0x00015E58
		public DateTime LastModified { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00017C61 File Offset: 0x00015E61
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00017C69 File Offset: 0x00015E69
		public byte[] FileContents { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00017C72 File Offset: 0x00015E72
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00017C7A File Offset: 0x00015E7A
		public string HashString { get; private set; }

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017C83 File Offset: 0x00015E83
		public FileContentInfo([NotNull] string fullPath)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(fullPath, "fullPath");
			this.LastModified = File.GetLastWriteTimeUtc(fullPath);
			this.FileContents = File.ReadAllBytes(fullPath);
			this.HashString = FileContentInfo.CalculateHashString(this.FileContents);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00017CBF File Offset: 0x00015EBF
		public FileContentInfo([NotNull] byte[] fileContents, DateTime lastFileModifiedTime)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<byte[]>(fileContents, "fileContents");
			this.LastModified = lastFileModifiedTime;
			this.FileContents = fileContents;
			this.HashString = FileContentInfo.CalculateHashString(this.FileContents);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017CF4 File Offset: 0x00015EF4
		internal FileContentInfo([NotNull] IFileContentInfo fileContentInfo, [NotNull] byte[] fileContent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IFileContentInfo>(fileContentInfo, "fileContentInfo");
			ExtendedDiagnostics.EnsureArgumentNotNull<byte[]>(fileContent, "fileContent");
			this.LastModified = fileContentInfo.LastModified;
			this.FileContents = fileContent;
			this.HashString = FileContentInfo.CalculateHashString(this.FileContents);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00017D44 File Offset: 0x00015F44
		private static string CalculateHashString(byte[] fileContent)
		{
			MD5CryptoServiceProvider md5CryptoServiceProvider = FileContentInfo.s_hashProvider;
			byte[] array;
			lock (md5CryptoServiceProvider)
			{
				array = FileContentInfo.s_hashProvider.ComputeHash(fileContent);
			}
			return BitConverter.ToString(array).Replace("-", "");
		}

		// Token: 0x04000240 RID: 576
		private static readonly MD5CryptoServiceProvider s_hashProvider = new MD5CryptoServiceProvider();
	}
}
