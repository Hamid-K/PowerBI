using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019F8 RID: 6648
	internal abstract class LibraryFile : ILibraryService, IModule
	{
		// Token: 0x0600A825 RID: 43045 RVA: 0x0022BD48 File Offset: 0x00229F48
		protected LibraryFile(string source)
		{
			this.source = source;
			this.loadVersion = Guid.NewGuid().ToString();
			this.Exports = LibraryFile.EmptyKeys.Instance;
		}

		// Token: 0x17002AD4 RID: 10964
		// (get) Token: 0x0600A826 RID: 43046 RVA: 0x0022BD86 File Offset: 0x00229F86
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17002AD5 RID: 10965
		// (get) Token: 0x0600A827 RID: 43047 RVA: 0x0022BD8E File Offset: 0x00229F8E
		// (set) Token: 0x0600A828 RID: 43048 RVA: 0x0022BD96 File Offset: 0x00229F96
		public string ModuleName { get; set; }

		// Token: 0x17002AD6 RID: 10966
		// (get) Token: 0x0600A829 RID: 43049 RVA: 0x0022BD9F File Offset: 0x00229F9F
		public string LoadVersion
		{
			get
			{
				return this.loadVersion;
			}
		}

		// Token: 0x17002AD7 RID: 10967
		// (get) Token: 0x0600A82A RID: 43050 RVA: 0x0022BDA7 File Offset: 0x00229FA7
		// (set) Token: 0x0600A82B RID: 43051 RVA: 0x0022BDAF File Offset: 0x00229FAF
		public IKeys Exports { get; set; }

		// Token: 0x17002AD8 RID: 10968
		// (get) Token: 0x0600A82C RID: 43052
		public abstract VerifyResult Verification { get; }

		// Token: 0x17002AD9 RID: 10969
		// (get) Token: 0x0600A82D RID: 43053
		public abstract X509Certificate2[] Signers { get; }

		// Token: 0x0600A82E RID: 43054
		public abstract string GetNearestSupportedCulture(string cultureName, out CultureInfo culture);

		// Token: 0x0600A82F RID: 43055
		protected abstract string GetResourceString(string cultureName, string stringName);

		// Token: 0x0600A830 RID: 43056
		protected abstract byte[] GetResourceFile(string filename);

		// Token: 0x0600A831 RID: 43057 RVA: 0x0022BDB8 File Offset: 0x00229FB8
		public static bool New(byte[] bytes, out LibraryFile result, out Exception error)
		{
			if (bytes.Length <= 4 || bytes[0] != LibraryFile.zipMagic[0] || bytes[1] != LibraryFile.zipMagic[1] || bytes[2] != LibraryFile.zipMagic[2] || bytes[3] != LibraryFile.zipMagic[3])
			{
				result = new LibraryFile.PqFile(bytes);
				error = null;
				return true;
			}
			LibraryFile.PqxFile pqxFile;
			if (LibraryFile.PqxFile.TryOpen(bytes, out pqxFile, out error))
			{
				result = pqxFile;
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600A832 RID: 43058 RVA: 0x0022BE1C File Offset: 0x0022A01C
		public string[] GetLoadedVersions(string[] moduleNames)
		{
			return new string[] { this.LoadVersion };
		}

		// Token: 0x0600A833 RID: 43059 RVA: 0x0022BD86 File Offset: 0x00229F86
		public string GetSource(string moduleName)
		{
			return this.source;
		}

		// Token: 0x0600A834 RID: 43060 RVA: 0x0022BE2D File Offset: 0x0022A02D
		public string GetResourceString(string moduleName, string cultureName, string stringName)
		{
			return this.GetResourceString(cultureName, stringName);
		}

		// Token: 0x0600A835 RID: 43061 RVA: 0x0022BE37 File Offset: 0x0022A037
		public byte[] GetResourceFile(string moduleName, string filename)
		{
			return this.GetResourceFile(filename);
		}

		// Token: 0x0600A836 RID: 43062 RVA: 0x00002105 File Offset: 0x00000305
		public ModuleTrustLevel GetTrustLevel(string moduleName)
		{
			return ModuleTrustLevel.Unknown;
		}

		// Token: 0x0600A837 RID: 43063 RVA: 0x000091AE File Offset: 0x000073AE
		public ISerializableValue GetLibraries(string cultureName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600A838 RID: 43064 RVA: 0x000091AE File Offset: 0x000073AE
		public ISerializableValue GetLibraryExports(string cultureName, string libraryIdentifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600A839 RID: 43065 RVA: 0x000091AE File Offset: 0x000073AE
		public ISerializableValue GetLibraryDataSources(string cultureName, string libraryIdentifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17002ADA RID: 10970
		// (get) Token: 0x0600A83A RID: 43066 RVA: 0x0022BE40 File Offset: 0x0022A040
		string IModule.Name
		{
			get
			{
				return this.ModuleName;
			}
		}

		// Token: 0x17002ADB RID: 10971
		// (get) Token: 0x0600A83B RID: 43067 RVA: 0x0022BD9F File Offset: 0x00229F9F
		string IModule.Version
		{
			get
			{
				return this.loadVersion;
			}
		}

		// Token: 0x17002ADC RID: 10972
		// (get) Token: 0x0600A83C RID: 43068 RVA: 0x000020FA File Offset: 0x000002FA
		IRecordValue IModule.Metadata
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002ADD RID: 10973
		// (get) Token: 0x0600A83D RID: 43069 RVA: 0x001D0891 File Offset: 0x001CEA91
		ResourceKindInfo[] IModule.DataSources
		{
			get
			{
				return EmptyArray<ResourceKindInfo>.Instance;
			}
		}

		// Token: 0x17002ADE RID: 10974
		// (get) Token: 0x0600A83E RID: 43070 RVA: 0x000020FA File Offset: 0x000002FA
		ResourceKindInfo IModule.DynamicModuleDataSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04005788 RID: 22408
		private static readonly byte[] zipMagic = new byte[] { 80, 75, 3, 4 };

		// Token: 0x04005789 RID: 22409
		private readonly string source;

		// Token: 0x0400578A RID: 22410
		private readonly string loadVersion;

		// Token: 0x020019F9 RID: 6649
		private sealed class PqFile : LibraryFile
		{
			// Token: 0x0600A840 RID: 43072 RVA: 0x0022BE60 File Offset: 0x0022A060
			public PqFile(byte[] bytes)
				: base(LibraryFile.PqFile.ReadSource(bytes))
			{
			}

			// Token: 0x0600A841 RID: 43073 RVA: 0x0022BE70 File Offset: 0x0022A070
			private static string ReadSource(byte[] bytes)
			{
				string text;
				using (Stream stream = new MemoryStream(bytes))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						text = streamReader.ReadToEnd();
					}
				}
				return text;
			}

			// Token: 0x17002ADF RID: 10975
			// (get) Token: 0x0600A842 RID: 43074 RVA: 0x00075E2C File Offset: 0x0007402C
			public override VerifyResult Verification
			{
				get
				{
					return VerifyResult.NotSigned;
				}
			}

			// Token: 0x17002AE0 RID: 10976
			// (get) Token: 0x0600A843 RID: 43075 RVA: 0x0022BEC8 File Offset: 0x0022A0C8
			public override X509Certificate2[] Signers
			{
				get
				{
					return EmptyArray<X509Certificate2>.Instance;
				}
			}

			// Token: 0x0600A844 RID: 43076 RVA: 0x0022BECF File Offset: 0x0022A0CF
			public override string GetNearestSupportedCulture(string cultureName, out CultureInfo culture)
			{
				culture = null;
				return null;
			}

			// Token: 0x0600A845 RID: 43077 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string GetResourceString(string cultureName, string stringName)
			{
				return null;
			}

			// Token: 0x0600A846 RID: 43078 RVA: 0x000020FA File Offset: 0x000002FA
			protected override byte[] GetResourceFile(string fileName)
			{
				return null;
			}
		}

		// Token: 0x020019FA RID: 6650
		private class PqxFile : LibraryFile
		{
			// Token: 0x0600A847 RID: 43079 RVA: 0x0022BED8 File Offset: 0x0022A0D8
			private PqxFile(byte[] bytes, string prefix = null, HashSet<string> artifacts = null)
				: base(LibraryFile.PqxFile.ReadSource(bytes, artifacts))
			{
				this.lockObject = new object();
				this.bytes = bytes;
				this.resourceSets = new Dictionary<string, ResourceSet>();
				this.prefix = prefix ?? "resources";
				this.verification = VerifyResult.NotSigned;
				this.signers = EmptyArray<X509Certificate2>.Instance;
			}

			// Token: 0x0600A848 RID: 43080 RVA: 0x0022BF34 File Offset: 0x0022A134
			private PqxFile(byte[] bytes, Package package, HashSet<string> artifacts)
				: this(bytes, null, artifacts)
			{
				this.artifacts = artifacts;
				PackageDigitalSignatureManager packageDigitalSignatureManager = new PackageDigitalSignatureManager(package);
				try
				{
					this.verification = packageDigitalSignatureManager.VerifySignatures(false);
				}
				catch (CryptographicException)
				{
					this.verification = VerifyResult.NotSigned;
					return;
				}
				catch (UriFormatException)
				{
					this.verification = VerifyResult.InvalidSignature;
					return;
				}
				this.signers = packageDigitalSignatureManager.Signatures.Select((PackageDigitalSignature s) => new X509Certificate2(s.Signer)).ToArray<X509Certificate2>();
				foreach (string text in artifacts)
				{
					foreach (PackageDigitalSignature packageDigitalSignature in packageDigitalSignatureManager.Signatures)
					{
						Uri uri = PackUriHelper.CreatePartUri(new Uri(text, UriKind.Relative));
						if (!packageDigitalSignature.SignedParts.Contains(uri))
						{
							this.verification = VerifyResult.NotSigned;
							break;
						}
					}
				}
			}

			// Token: 0x0600A849 RID: 43081 RVA: 0x0022C064 File Offset: 0x0022A264
			public static bool TryOpen(byte[] bytes, out LibraryFile.PqxFile pqxFile, out Exception error)
			{
				try
				{
					using (Stream stream = new MemoryStream(bytes))
					{
						using (Package package = Package.Open(stream, FileMode.Open, FileAccess.Read))
						{
							HashSet<string> hashSet = new HashSet<string>(from part in package.GetParts()
								select part.Uri.OriginalString.Substring(1).Split(new char[] { '/' }) into name
								where name.Length == 1
								select name[0]);
							if (hashSet.Count > 0)
							{
								pqxFile = new LibraryFile.PqxFile(bytes, package, hashSet);
								error = null;
								return true;
							}
						}
					}
					pqxFile = new LibraryFile.PqxFile(bytes, null, null);
					error = null;
					return true;
				}
				catch (InvalidOperationException ex)
				{
					error = ex;
					pqxFile = null;
				}
				catch (FileFormatException ex2)
				{
					error = ex2;
					pqxFile = null;
				}
				return false;
			}

			// Token: 0x17002AE1 RID: 10977
			// (get) Token: 0x0600A84A RID: 43082 RVA: 0x0022C18C File Offset: 0x0022A38C
			public override VerifyResult Verification
			{
				get
				{
					return this.verification;
				}
			}

			// Token: 0x17002AE2 RID: 10978
			// (get) Token: 0x0600A84B RID: 43083 RVA: 0x0022C194 File Offset: 0x0022A394
			public override X509Certificate2[] Signers
			{
				get
				{
					return this.signers;
				}
			}

			// Token: 0x0600A84C RID: 43084 RVA: 0x0022C19C File Offset: 0x0022A39C
			public override string GetNearestSupportedCulture(string cultureName, out CultureInfo culture)
			{
				this.EnsureInitialized();
				for (culture = new CultureInfo(cultureName); culture != null; culture = culture.Parent)
				{
					string name = culture.Name;
					if (culture == CultureInfo.InvariantCulture)
					{
						culture = null;
						break;
					}
					string text = this.prefix + "." + name + ".resx";
					if (this.zipArchive.Contains(text))
					{
						return name;
					}
				}
				return null;
			}

			// Token: 0x0600A84D RID: 43085 RVA: 0x0022C204 File Offset: 0x0022A404
			private static string ReadSource(byte[] bytes, HashSet<string> artifacts)
			{
				using (Stream stream = new MemoryStream(bytes))
				{
					using (ZipArchive zipArchive = new ZipArchive(stream))
					{
						ZipArchiveEntry zipArchiveEntry = null;
						foreach (ZipArchiveEntry zipArchiveEntry2 in zipArchive.Entries)
						{
							if ((artifacts == null || artifacts.Contains(zipArchiveEntry2.FullName)) && MashupFileExtension.IsKnownSourceExtension(zipArchiveEntry2.FullName) && !MashupFileExtension.IsSdkArtifact(zipArchiveEntry2.FullName))
							{
								if (zipArchiveEntry != null)
								{
									throw new InvalidOperationException(Strings.ModuleMultipleSourceFiles(zipArchiveEntry.FullName, zipArchiveEntry2.FullName));
								}
								zipArchiveEntry = zipArchiveEntry2;
							}
						}
						if (zipArchiveEntry != null)
						{
							using (Stream stream2 = zipArchiveEntry.Open())
							{
								using (StreamReader streamReader = new StreamReader(stream2))
								{
									return streamReader.ReadToEnd();
								}
							}
						}
					}
				}
				throw new InvalidOperationException(Strings.ModuleNoSourceFile);
			}

			// Token: 0x0600A84E RID: 43086 RVA: 0x0022C334 File Offset: 0x0022A534
			private Stream OpenStream()
			{
				return new MemoryStream(this.bytes);
			}

			// Token: 0x0600A84F RID: 43087 RVA: 0x0022C344 File Offset: 0x0022A544
			private void EnsureInitialized()
			{
				if (this.zipArchive == null)
				{
					object obj = this.lockObject;
					lock (obj)
					{
						if (this.zipArchive == null)
						{
							this.zipArchive = new LibraryFile.PqxFile.IndexedZipArchive(this.OpenStream(), this.artifacts);
						}
					}
				}
			}

			// Token: 0x0600A850 RID: 43088 RVA: 0x0022C3A8 File Offset: 0x0022A5A8
			private List<ResourceSet> GetResourcesForCulture(CultureInfo culture)
			{
				List<ResourceSet> list = new List<ResourceSet>();
				while (culture != null)
				{
					string name = culture.Name;
					string text;
					if (culture == CultureInfo.InvariantCulture)
					{
						culture = null;
						text = string.Empty;
					}
					else
					{
						culture = culture.Parent;
						text = ".";
					}
					ResourceSet resourceSet;
					if (!this.resourceSets.TryGetValue(name, out resourceSet))
					{
						string text2 = this.prefix + text + name + ".resx";
						Stream stream;
						if (this.zipArchive.TryOpenFile(text2, out stream))
						{
							resourceSet = new ResourceSet(new ResXResourceReader(stream));
						}
						this.resourceSets.Add(name, resourceSet);
					}
					if (resourceSet != null)
					{
						list.Add(resourceSet);
					}
				}
				return list;
			}

			// Token: 0x0600A851 RID: 43089 RVA: 0x0022C448 File Offset: 0x0022A648
			protected override string GetResourceString(string cultureName, string stringName)
			{
				this.EnsureInitialized();
				CultureInfo cultureInfo = (string.IsNullOrEmpty(cultureName) ? CultureInfo.InvariantCulture : new CultureInfo(cultureName));
				object obj = this.lockObject;
				List<ResourceSet> resourcesForCulture;
				lock (obj)
				{
					resourcesForCulture = this.GetResourcesForCulture(cultureInfo);
				}
				for (int i = 0; i < resourcesForCulture.Count; i++)
				{
					string @string = resourcesForCulture[i].GetString(stringName);
					if (@string != null)
					{
						return @string;
					}
				}
				return null;
			}

			// Token: 0x0600A852 RID: 43090 RVA: 0x0022C4D4 File Offset: 0x0022A6D4
			protected override byte[] GetResourceFile(string filename)
			{
				this.EnsureInitialized();
				object obj = this.lockObject;
				lock (obj)
				{
					Stream stream;
					if (this.zipArchive.TryOpenFile(filename, out stream))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (stream)
							{
								LibraryFile.PqxFile.CopyStream(stream, memoryStream);
								return memoryStream.ToArray();
							}
						}
					}
				}
				return null;
			}

			// Token: 0x0600A853 RID: 43091 RVA: 0x0022C570 File Offset: 0x0022A770
			private static void CopyStream(Stream input, Stream output)
			{
				byte[] array = new byte[65536];
				int num;
				while ((num = input.Read(array, 0, array.Length)) > 0)
				{
					output.Write(array, 0, num);
				}
			}

			// Token: 0x0600A854 RID: 43092 RVA: 0x0022C5A3 File Offset: 0x0022A7A3
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600A855 RID: 43093 RVA: 0x0022C5B4 File Offset: 0x0022A7B4
			protected virtual void Dispose(bool disposing)
			{
				if (this.zipArchive == null)
				{
					return;
				}
				if (disposing)
				{
					this.zipArchive.Dispose();
					this.zipArchive = null;
					foreach (ResourceSet resourceSet in this.resourceSets.Values)
					{
						resourceSet.Dispose();
					}
					this.resourceSets.Clear();
				}
			}

			// Token: 0x0400578D RID: 22413
			private const string resxFilePrefix = "resources";

			// Token: 0x0400578E RID: 22414
			private const string resxFileSuffix = ".resx";

			// Token: 0x0400578F RID: 22415
			private readonly object lockObject;

			// Token: 0x04005790 RID: 22416
			private readonly string prefix;

			// Token: 0x04005791 RID: 22417
			private readonly byte[] bytes;

			// Token: 0x04005792 RID: 22418
			private readonly Dictionary<string, ResourceSet> resourceSets;

			// Token: 0x04005793 RID: 22419
			private readonly VerifyResult verification;

			// Token: 0x04005794 RID: 22420
			private readonly X509Certificate2[] signers;

			// Token: 0x04005795 RID: 22421
			private readonly HashSet<string> artifacts;

			// Token: 0x04005796 RID: 22422
			private LibraryFile.PqxFile.IndexedZipArchive zipArchive;

			// Token: 0x020019FB RID: 6651
			private sealed class IndexedZipArchive : IDisposable
			{
				// Token: 0x0600A856 RID: 43094 RVA: 0x0022C634 File Offset: 0x0022A834
				public IndexedZipArchive(Stream stream, HashSet<string> artifacts)
				{
					this.stream = stream;
					this.zip = new ZipArchive(this.stream);
					this.zipEntries = new Dictionary<string, ZipArchiveEntry>(StringComparer.OrdinalIgnoreCase);
					this.disposed = false;
					foreach (ZipArchiveEntry zipArchiveEntry in this.zip.Entries)
					{
						if (!(zipArchiveEntry.Name == string.Empty) && (artifacts == null || artifacts.Contains(zipArchiveEntry.Name)))
						{
							this.zipEntries.Add(zipArchiveEntry.FullName, zipArchiveEntry);
						}
					}
				}

				// Token: 0x0600A857 RID: 43095 RVA: 0x0022C6EC File Offset: 0x0022A8EC
				public bool TryOpenFile(string filename, out Stream stream)
				{
					ZipArchiveEntry zipArchiveEntry;
					if (this.disposed || !this.zipEntries.TryGetValue(filename, out zipArchiveEntry))
					{
						stream = null;
						return false;
					}
					stream = zipArchiveEntry.Open();
					return true;
				}

				// Token: 0x0600A858 RID: 43096 RVA: 0x0022C71F File Offset: 0x0022A91F
				public bool Contains(string filename)
				{
					return this.zipEntries.ContainsKey(filename);
				}

				// Token: 0x0600A859 RID: 43097 RVA: 0x0022C72D File Offset: 0x0022A92D
				public void Dispose()
				{
					if (!this.disposed)
					{
						this.zip.Dispose();
						this.stream.Dispose();
						this.disposed = true;
					}
				}

				// Token: 0x04005797 RID: 22423
				private readonly Stream stream;

				// Token: 0x04005798 RID: 22424
				private readonly ZipArchive zip;

				// Token: 0x04005799 RID: 22425
				private readonly Dictionary<string, ZipArchiveEntry> zipEntries;

				// Token: 0x0400579A RID: 22426
				private bool disposed;
			}
		}

		// Token: 0x020019FD RID: 6653
		private sealed class EmptyKeys : IKeys, IEnumerable<string>, IEnumerable
		{
			// Token: 0x0600A860 RID: 43104 RVA: 0x000020FD File Offset: 0x000002FD
			private EmptyKeys()
			{
			}

			// Token: 0x17002AE3 RID: 10979
			// (get) Token: 0x0600A861 RID: 43105 RVA: 0x00002105 File Offset: 0x00000305
			public int Length
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17002AE4 RID: 10980
			public string this[int index]
			{
				get
				{
					throw new ArgumentOutOfRangeException();
				}
			}

			// Token: 0x0600A863 RID: 43107 RVA: 0x001BAF25 File Offset: 0x001B9125
			public bool TryGetIndex(string key, out int index)
			{
				index = -1;
				return false;
			}

			// Token: 0x0600A864 RID: 43108 RVA: 0x0022C7A4 File Offset: 0x0022A9A4
			public IEnumerator<string> GetEnumerator()
			{
				return ((IEnumerable<string>)EmptyArray<string>.Instance).GetEnumerator();
			}

			// Token: 0x0600A865 RID: 43109 RVA: 0x0022C7B0 File Offset: 0x0022A9B0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return EmptyArray<string>.Instance.GetEnumerator();
			}

			// Token: 0x040057A0 RID: 22432
			public static readonly IKeys Instance = new LibraryFile.EmptyKeys();
		}
	}
}
