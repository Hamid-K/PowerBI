using System;
using System.Data;
using System.Linq;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.BIServer.HostingEnvironment.Cryptography.Exceptions;
using Microsoft.BIServer.HostingEnvironment.Storage;
using Microsoft.BIServer.HostingEnvironment.Storage.Exceptions;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.Configuration.Key
{
	// Token: 0x02000036 RID: 54
	public sealed class KeyRepository : IKeyRepository, IDisposable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00007C03 File Offset: 0x00005E03
		public KeyRepository()
		{
			this._sql = CatalogAccessFactory.NewConnection();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007C16 File Offset: 0x00005E16
		public KeyRepository(ISqlAccess existingSqlAccess)
		{
			this._sql = ReferenceSqlAccess.UseButDoNotDispose(existingSqlAccess);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007C2A File Offset: 0x00005E2A
		public byte[] GetEncryptedSymmetricKey()
		{
			if (!SymmetricKeyCrypto.Instance.IsInitialized)
			{
				return this.ReloadEncryptedSymmetricKey();
			}
			return SymmetricKeyCrypto.Instance.GetPublicKeyEncryptedSymmetricKey();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007C4C File Offset: 0x00005E4C
		public byte[] ReloadEncryptedSymmetricKey()
		{
			Guid installationId = ConfigReader.Current.InstallationId;
			InstallationInfo announcedKeyResults = this.GetAnnouncedKeyResults(installationId);
			byte[] encryptedSymmetricKey = this.GetEncryptedSymmetricKey(announcedKeyResults);
			SymmetricKeyCrypto.Instance.SetPublicKeyEncryptedSymmetricKey(encryptedSymmetricKey);
			return encryptedSymmetricKey;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007C80 File Offset: 0x00005E80
		private InstallationInfo GetAnnouncedKeyResults(Guid installationId)
		{
			using (SqlCommand sqlCommand = this._sql.PrepareStoredProcedure("GetAnnouncedKey"))
			{
				sqlCommand.Parameters.AddWithValue("InstallationID", installationId);
				using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
				{
					if (sqlDataReader.Read())
					{
						byte[] binaryOrNullColumn = this._sql.GetBinaryOrNullColumn(sqlDataReader, 0);
						string stringOrNullColumn = this._sql.GetStringOrNullColumn(sqlDataReader, 1);
						string stringOrNullColumn2 = this._sql.GetStringOrNullColumn(sqlDataReader, 2);
						return new InstallationInfo(installationId, stringOrNullColumn, stringOrNullColumn2, binaryOrNullColumn);
					}
				}
			}
			throw new SymmetricKeyNotInitializedException();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007D38 File Offset: 0x00005F38
		private byte[] GetEncryptedSymmetricKey(InstallationInfo installationInfo)
		{
			string machineName = Environment.MachineName;
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3;
			using (SqlCommand sqlCommand = this._sql.PrepareStoredProcedure("AnnounceOrGetKey"))
			{
				sqlCommand.Parameters.AddWithValue("@MachineName", machineName);
				sqlCommand.Parameters.AddWithValue("@InstanceName", installationInfo.InstanceName);
				sqlCommand.Parameters.AddWithValue("@InstallationID", installationInfo.InstallationId);
				sqlCommand.Parameters.AddWithValue("@PublicKey", installationInfo.PublicKey);
				SqlParameter sqlParameter = sqlCommand.Parameters.Add("@NumAnnouncedServices", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				string text = null;
				using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
				{
					if (!sqlDataReader.Read())
					{
						throw new DatabaseConfigurationException("Cannot read matching key for installation");
					}
					text = this._sql.GetStringOrNullColumn(sqlDataReader, 0);
					array = this._sql.GetBinaryOrNullColumn(sqlDataReader, 1);
					array2 = this._sql.GetBinaryOrNullColumn(sqlDataReader, 2);
					if (sqlDataReader.Read())
					{
						throw new DatabaseConfigurationException("More than one matching key record present");
					}
				}
				int num = (int)sqlParameter.Value;
				if (!machineName.Equals(text, StringComparison.OrdinalIgnoreCase) && installationInfo.PublicKey.SequenceEqual(array2))
				{
					using (SqlCommand sqlCommand2 = this._sql.PrepareStoredProcedure("SetMachineName"))
					{
						sqlCommand2.Parameters.AddWithValue("@MachineName", machineName);
						sqlCommand2.Parameters.AddWithValue("@InstallationID", installationInfo.InstallationId);
						sqlCommand2.ExecuteNonQuery();
					}
				}
				array3 = array;
			}
			return array3;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007F24 File Offset: 0x00006124
		public void Dispose()
		{
			this._sql.Dispose();
		}

		// Token: 0x04000197 RID: 407
		private readonly ISqlAccess _sql;
	}
}
