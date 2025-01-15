using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x02000030 RID: 48
	public class TicketSerializer : IDataSerializer<AuthenticationTicket>
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000037F0 File Offset: 0x000019F0
		public virtual byte[] Serialize(AuthenticationTicket model)
		{
			byte[] array;
			using (MemoryStream memory = new MemoryStream())
			{
				using (GZipStream compression = new GZipStream(memory, CompressionLevel.Optimal))
				{
					using (BinaryWriter writer = new BinaryWriter(compression))
					{
						TicketSerializer.Write(writer, model);
					}
				}
				array = memory.ToArray();
			}
			return array;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000386C File Offset: 0x00001A6C
		public virtual AuthenticationTicket Deserialize(byte[] data)
		{
			AuthenticationTicket authenticationTicket;
			using (MemoryStream memory = new MemoryStream(data))
			{
				using (GZipStream compression = new GZipStream(memory, CompressionMode.Decompress))
				{
					using (BinaryReader reader = new BinaryReader(compression))
					{
						authenticationTicket = TicketSerializer.Read(reader);
					}
				}
			}
			return authenticationTicket;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000038E0 File Offset: 0x00001AE0
		public static void Write(BinaryWriter writer, AuthenticationTicket model)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			writer.Write(3);
			ClaimsIdentity identity = model.Identity;
			writer.Write(identity.AuthenticationType);
			TicketSerializer.WriteWithDefault(writer, identity.NameClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
			TicketSerializer.WriteWithDefault(writer, identity.RoleClaimType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
			writer.Write(identity.Claims.Count<Claim>());
			foreach (Claim claim in identity.Claims)
			{
				TicketSerializer.WriteWithDefault(writer, claim.Type, identity.NameClaimType);
				writer.Write(claim.Value);
				TicketSerializer.WriteWithDefault(writer, claim.ValueType, "http://www.w3.org/2001/XMLSchema#string");
				TicketSerializer.WriteWithDefault(writer, claim.Issuer, "LOCAL AUTHORITY");
				TicketSerializer.WriteWithDefault(writer, claim.OriginalIssuer, claim.Issuer);
			}
			string bc = identity.BootstrapContext as string;
			if (bc == null || string.IsNullOrWhiteSpace(bc))
			{
				writer.Write(0);
			}
			else
			{
				writer.Write(bc.Length);
				writer.Write(bc);
			}
			PropertiesSerializer.Write(writer, model.Properties);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003A20 File Offset: 0x00001C20
		public static AuthenticationTicket Read(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadInt32() != 3)
			{
				return null;
			}
			string authenticationType = reader.ReadString();
			string nameClaimType = TicketSerializer.ReadWithDefault(reader, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
			string roleClaimType = TicketSerializer.ReadWithDefault(reader, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
			int count = reader.ReadInt32();
			Claim[] claims = new Claim[count];
			for (int index = 0; index != count; index++)
			{
				string type = TicketSerializer.ReadWithDefault(reader, nameClaimType);
				string value = reader.ReadString();
				string valueType = TicketSerializer.ReadWithDefault(reader, "http://www.w3.org/2001/XMLSchema#string");
				string issuer = TicketSerializer.ReadWithDefault(reader, "LOCAL AUTHORITY");
				string originalIssuer = TicketSerializer.ReadWithDefault(reader, issuer);
				claims[index] = new Claim(type, value, valueType, issuer, originalIssuer);
			}
			ClaimsIdentity identity = new ClaimsIdentity(claims, authenticationType, nameClaimType, roleClaimType);
			int bootstrapContextSize = reader.ReadInt32();
			if (bootstrapContextSize > 0)
			{
				identity.BootstrapContext = reader.ReadString();
			}
			AuthenticationProperties properties = PropertiesSerializer.Read(reader);
			return new AuthenticationTicket(identity, properties);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003B04 File Offset: 0x00001D04
		private static void WriteWithDefault(BinaryWriter writer, string value, string defaultValue)
		{
			if (string.Equals(value, defaultValue, StringComparison.Ordinal))
			{
				writer.Write("\0");
				return;
			}
			writer.Write(value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003B24 File Offset: 0x00001D24
		private static string ReadWithDefault(BinaryReader reader, string defaultValue)
		{
			string value = reader.ReadString();
			if (string.Equals(value, "\0", StringComparison.Ordinal))
			{
				return defaultValue;
			}
			return value;
		}

		// Token: 0x0400004F RID: 79
		private const int FormatVersion = 3;

		// Token: 0x02000045 RID: 69
		private static class DefaultValues
		{
			// Token: 0x04000094 RID: 148
			public const string DefaultStringPlaceholder = "\0";

			// Token: 0x04000095 RID: 149
			public const string NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

			// Token: 0x04000096 RID: 150
			public const string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

			// Token: 0x04000097 RID: 151
			public const string LocalAuthority = "LOCAL AUTHORITY";

			// Token: 0x04000098 RID: 152
			public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";
		}
	}
}
