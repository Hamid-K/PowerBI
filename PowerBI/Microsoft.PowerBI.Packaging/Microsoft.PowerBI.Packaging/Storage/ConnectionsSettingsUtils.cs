using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000044 RID: 68
	public static class ConnectionsSettingsUtils
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00006C26 File Offset: 0x00004E26
		public static ConnectionsSettings FromStorage(this ConnectionsSettingsStorage storedSettings)
		{
			if (storedSettings == null)
			{
				return null;
			}
			return new ConnectionsSettings(storedSettings.Connections.FromStorage(), storedSettings.RemoteArtifacts.FromStorage(), storedSettings.OriginalWorkspaceObjectId);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006C4E File Offset: 0x00004E4E
		public static ConnectionsSettingsStorage ToStorage(this ConnectionsSettings settings)
		{
			if (settings == null)
			{
				return null;
			}
			return new ConnectionsSettingsStorage
			{
				Connections = settings.Connections.ToStorage(),
				RemoteArtifacts = settings.RemoteArtifacts.ToStorage(),
				OriginalWorkspaceObjectId = settings.OriginalWorkspaceObjectId
			};
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006C88 File Offset: 0x00004E88
		public static Dictionary<string, ConnectionProperties> FromStorage(this List<ConnectionPropertiesStorage> storedConnections)
		{
			if (storedConnections == null)
			{
				return null;
			}
			return storedConnections.ToDictionary((ConnectionPropertiesStorage storedConnection) => storedConnection.Name, (ConnectionPropertiesStorage storedConnection) => new ConnectionProperties
			{
				Name = storedConnection.Name,
				ConnectionString = storedConnection.ConnectionString,
				IsMultiDimensional = storedConnection.IsMultiDimensional,
				ConnectionType = storedConnection.ConnectionType,
				PbiServiceGroupId = storedConnection.PbiServiceGroupId,
				PbiServiceModelId = storedConnection.PbiServiceModelId,
				PbiModelVirtualServerName = storedConnection.PbiModelVirtualServerName,
				PbiModelDatabaseName = storedConnection.PbiModelDatabaseName
			});
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006CDE File Offset: 0x00004EDE
		public static List<ConnectionPropertiesStorage> ToStorage(this Dictionary<string, ConnectionProperties> connections)
		{
			if (connections == null)
			{
				return null;
			}
			return connections.Select((KeyValuePair<string, ConnectionProperties> connection) => new ConnectionPropertiesStorage
			{
				Name = connection.Value.Name,
				ConnectionString = connection.Value.ConnectionString,
				IsMultiDimensional = connection.Value.IsMultiDimensional,
				ConnectionType = connection.Value.ConnectionType,
				PbiServiceModelId = connection.Value.PbiServiceModelId,
				PbiServiceGroupId = connection.Value.PbiServiceGroupId,
				PbiModelVirtualServerName = connection.Value.PbiModelVirtualServerName,
				PbiModelDatabaseName = connection.Value.PbiModelDatabaseName
			}).ToList<ConnectionPropertiesStorage>();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006D0F File Offset: 0x00004F0F
		public static List<RemoteArtifactProperties> FromStorage(this IEnumerable<RemoteArtifactPropertiesStorage> storedArtifacts)
		{
			if (storedArtifacts == null)
			{
				return null;
			}
			return storedArtifacts.Select((RemoteArtifactPropertiesStorage storedArtifact) => new RemoteArtifactProperties
			{
				DatasetId = storedArtifact.DatasetId,
				ReportId = storedArtifact.ReportId
			}).ToList<RemoteArtifactProperties>();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006D40 File Offset: 0x00004F40
		public static List<RemoteArtifactPropertiesStorage> ToStorage(this IEnumerable<RemoteArtifactProperties> artifacts)
		{
			if (artifacts == null)
			{
				return null;
			}
			return artifacts.Select((RemoteArtifactProperties artifact) => new RemoteArtifactPropertiesStorage
			{
				DatasetId = artifact.DatasetId,
				ReportId = artifact.ReportId
			}).ToList<RemoteArtifactPropertiesStorage>();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006D71 File Offset: 0x00004F71
		public static ConnectionsSettings DeserializeConnectionsSettings(IStreamablePowerBIPackagePartContent content)
		{
			return ConnectionsSettingsUtils.DeserializeConnectionsSettingsStorage(content).FromStorage();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006D7E File Offset: 0x00004F7E
		public static ConnectionsSettingsStorage DeserializeConnectionsSettingsStorage(IStreamablePowerBIPackagePartContent content)
		{
			return ConnectionsSettingsUtils.DeserializeConnectionsSettingsStorage(PowerBIPackagingUtils.GetContentAsBytes(content, PowerBIPackager.IsConnectionsOptional));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006D90 File Offset: 0x00004F90
		public static ConnectionsSettingsStorage DeserializeConnectionsSettingsStorage(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return new ConnectionsSettingsStorage();
			}
			if (data[0] == 123)
			{
				return ConnectionsSettingsUtils.DeserializeJson(data);
			}
			return ConnectionsSettingsUtils.DeserializeBinary(data);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006DB3 File Offset: 0x00004FB3
		public static IStreamablePowerBIPackagePartContent SerializeConnectionsSettings(ConnectionsSettings settings)
		{
			return new StreamablePowerBIPackagePartContent(ConnectionsSettingsUtils.SerializeJson(settings), "");
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006DC5 File Offset: 0x00004FC5
		public static IStreamablePowerBIPackagePartContent SerializeConnectionsSettings(ConnectionsSettingsStorage storedSettings)
		{
			return new StreamablePowerBIPackagePartContent(ConnectionsSettingsUtils.SerializeJson(storedSettings), "");
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public static bool IsLiveConnect(this ConnectionsSettings connectionsSettings)
		{
			Dictionary<string, ConnectionProperties> dictionary = ((connectionsSettings != null) ? connectionsSettings.Connections : null);
			return dictionary != null && dictionary.Count > 0;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006E00 File Offset: 0x00005000
		private static ConnectionsSettingsStorage DeserializeJson(byte[] data)
		{
			ConnectionsSettingsStorage connectionsSettingsStorage = ConnectionsSettingsUtils.DeserializeJsonObject<ConnectionsSettingsStorage>(ConnectionsSettingsUtils.JsonContentEncoding.GetString(data));
			ConnectionsSettingsVersion.ValidateVersion(connectionsSettingsStorage);
			return connectionsSettingsStorage;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006E18 File Offset: 0x00005018
		private static T DeserializeJsonObject<T>(string jsonContent)
		{
			T t;
			try
			{
				t = JsonConvert.DeserializeObject<T>(jsonContent);
			}
			catch (JsonReaderException ex)
			{
				throw new FileFormatException("Invalid json content", ex);
			}
			return t;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006E4C File Offset: 0x0000504C
		private static ConnectionsSettingsStorage DeserializeBinary(byte[] data)
		{
			return ConnectionsSettingsUtils.ConvertSettingsFromV0Storage(BinarySerializationReader.DeserializeBytes<ConnectionsSettingsStorageV0>(data));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006E5C File Offset: 0x0000505C
		private static ConnectionsSettingsStorage ConvertSettingsFromV0Storage(ConnectionsSettingsStorageV0 storage)
		{
			if (storage == null)
			{
				return null;
			}
			ConnectionsSettingsStorage connectionsSettingsStorage = new ConnectionsSettingsStorage();
			if (storage.Connections != null)
			{
				connectionsSettingsStorage.Connections = new List<ConnectionPropertiesStorage>(storage.Connections.Count);
				foreach (KeyValuePair<string, ConnectionPropertiesStorageV0> keyValuePair in storage.Connections)
				{
					ConnectionPropertiesStorage connectionPropertiesStorage = new ConnectionPropertiesStorage
					{
						Name = keyValuePair.Key,
						ConnectionString = keyValuePair.Value.ConnectionString
					};
					connectionsSettingsStorage.Connections.Add(connectionPropertiesStorage);
				}
			}
			return connectionsSettingsStorage;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006F04 File Offset: 0x00005104
		private static byte[] SerializeJson(ConnectionsSettings settings)
		{
			return ConnectionsSettingsUtils.SerializeJson(settings.ToStorage());
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006F14 File Offset: 0x00005114
		private static byte[] SerializeJson(ConnectionsSettingsStorage storedSettings)
		{
			storedSettings.Version = ConnectionsSettingsVersion.GetVersion(storedSettings);
			string text = ConnectionsSettingsUtils.SerializeJsonObject<ConnectionsSettingsStorage>(storedSettings);
			return ConnectionsSettingsUtils.JsonContentEncoding.GetBytes(text);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006F3F File Offset: 0x0000513F
		private static string SerializeJsonObject<T>(T value)
		{
			return JsonConvert.SerializeObject(value);
		}

		// Token: 0x04000112 RID: 274
		private static readonly Encoding JsonContentEncoding = Encoding.UTF8;
	}
}
