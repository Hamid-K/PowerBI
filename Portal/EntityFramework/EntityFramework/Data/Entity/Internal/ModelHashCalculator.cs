using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000126 RID: 294
	internal class ModelHashCalculator
	{
		// Token: 0x06001484 RID: 5252 RVA: 0x0003572C File Offset: 0x0003392C
		public virtual string Calculate(DbCompiledModel compiledModel)
		{
			DbProviderInfo providerInfo = compiledModel.ProviderInfo;
			DbModelBuilder dbModelBuilder = compiledModel.CachedModelBuilder.Clone();
			EdmMetadataContext.ConfigureEdmMetadata(dbModelBuilder.ModelConfiguration);
			EdmModel database = dbModelBuilder.Build(providerInfo).DatabaseMapping.Database;
			database.SchemaVersion = 2.0;
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				Indent = true
			}))
			{
				new SsdlSerializer().Serialize(database, providerInfo.ProviderInvariantName, providerInfo.ProviderManifestToken, xmlWriter, true);
			}
			return ModelHashCalculator.ComputeSha256Hash(stringBuilder.ToString());
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x000357D0 File Offset: 0x000339D0
		private static string ComputeSha256Hash(string input)
		{
			byte[] array = ModelHashCalculator.GetSha256HashAlgorithm().ComputeHash(Encoding.ASCII.GetBytes(input));
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00035830 File Offset: 0x00033A30
		private static SHA256 GetSha256HashAlgorithm()
		{
			SHA256 sha;
			try
			{
				sha = new SHA256CryptoServiceProvider();
			}
			catch (PlatformNotSupportedException)
			{
				sha = new SHA256Managed();
			}
			return sha;
		}
	}
}
