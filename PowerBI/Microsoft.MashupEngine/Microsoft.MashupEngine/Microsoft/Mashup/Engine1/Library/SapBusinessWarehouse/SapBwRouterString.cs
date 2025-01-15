using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004DC RID: 1244
	internal class SapBwRouterString
	{
		// Token: 0x06002899 RID: 10393 RVA: 0x0007947E File Offset: 0x0007767E
		private SapBwRouterString(List<SapBwRouterString.Station> stations)
		{
			this.Stations = stations;
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x0007948D File Offset: 0x0007768D
		private List<SapBwRouterString.Station> Stations { get; }

		// Token: 0x0600289B RID: 10395 RVA: 0x00079495 File Offset: 0x00077695
		public IEnumerable<KeyValuePair<string, string>> GetConnectionStringProperties()
		{
			SapBwRouterString.Station station = this.Stations.Last<SapBwRouterString.Station>();
			foreach (KeyValuePair<string, string> keyValuePair in station.GetConnectionStringProperties())
			{
				yield return keyValuePair;
			}
			IEnumerator<KeyValuePair<string, string>> enumerator = null;
			if (this.Stations.Count > 1)
			{
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					foreach (SapBwRouterString.Station station2 in this.Stations.Take(this.Stations.Count - 1))
					{
						station2.Write(stringWriter);
					}
					yield return new KeyValuePair<string, string>("SAPROUTER", stringWriter.ToString());
				}
				StringWriter stringWriter = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000794A8 File Offset: 0x000776A8
		public static bool TryNew(SapBwConnectionType connectionType, string serverName, string logonGroup, out SapBwRouterString routerString)
		{
			if (!SapBwRouterString.IsRouterString(serverName))
			{
				routerString = null;
				return false;
			}
			if (!SapBwRouterString.TryParseRouterString(connectionType, serverName, out routerString))
			{
				throw new FormatException(Strings.SapBwInvalidRouterString);
			}
			if (routerString.Stations.Any((SapBwRouterString.Station s) => s.HasPassword))
			{
				throw new FormatException(Strings.SapBwPasswordInRouterString);
			}
			if (connectionType != SapBwConnectionType.ApplicationHostBased)
			{
				if (connectionType == SapBwConnectionType.LoadBalanced)
				{
					SapBwRouterString.MessageServerStation messageServerStation = routerString.Stations.Last<SapBwRouterString.Station>() as SapBwRouterString.MessageServerStation;
					if (messageServerStation == null)
					{
						throw new FormatException(Strings.SapBwRouterStringMustContainMessageServer);
					}
					if (messageServerStation.Group != null && !messageServerStation.Group.Equals(logonGroup, StringComparison.OrdinalIgnoreCase))
					{
						throw new FormatException(Strings.SapBwRouterStringGroupMustMatch(messageServerStation.Group, logonGroup));
					}
					if (routerString.Stations.Count > 1)
					{
						if (routerString.Stations.Take(routerString.Stations.Count - 1).Any((SapBwRouterString.Station s) => s.Kind == SapBwRouterString.StationKind.MessageServer))
						{
							throw new FormatException(Strings.SapBwRouterInvalidMessageServerStation);
						}
					}
				}
			}
			else if (routerString.Stations.Any((SapBwRouterString.Station s) => s.Kind == SapBwRouterString.StationKind.MessageServer))
			{
				throw new FormatException(Strings.SapBwRouterStringMustNotContainMessageServer);
			}
			return true;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0007961C File Offset: 0x0007781C
		public static bool TryExtractServerFromResourcePath(string[] resourcePath, out string server)
		{
			string text = resourcePath[0];
			if (text.StartsWith("|H|", StringComparison.OrdinalIgnoreCase) || text.StartsWith("|M|", StringComparison.OrdinalIgnoreCase))
			{
				SapBwRouterString sapBwRouterString;
				if (!SapBwRouterString.TryParseRouterString(SapBwRouterString.ConnectionTypeFromPathLength(resourcePath.Length), text, '|', out sapBwRouterString))
				{
					goto IL_0097;
				}
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					foreach (SapBwRouterString.Station station in sapBwRouterString.Stations)
					{
						station.Write(stringWriter);
					}
					server = stringWriter.ToString();
					return true;
				}
			}
			if (Uri.CheckHostName(text) != UriHostNameType.Unknown)
			{
				server = text;
				return true;
			}
			IL_0097:
			server = null;
			return false;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000796E4 File Offset: 0x000778E4
		public static string BuildRouterStringOrNull(Dictionary<string, string> sapNetParameters)
		{
			SapBwRouterString.Station station;
			if (!SapBwRouterString.Station.TryNew(sapNetParameters, out station))
			{
				return null;
			}
			string text;
			if (!sapNetParameters.TryGetValue("SAPROUTER", out text) && station.Port == null)
			{
				return null;
			}
			string text2;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				stringWriter.Write(text);
				station.Write(stringWriter);
				text2 = stringWriter.ToString();
			}
			return text2;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x00079754 File Offset: 0x00077954
		private static SapBwConnectionType ConnectionTypeFromPathLength(int pathLength)
		{
			if (pathLength == 3)
			{
				return SapBwConnectionType.ApplicationHostBased;
			}
			if (pathLength == 4)
			{
				return SapBwConnectionType.LoadBalanced;
			}
			return SapBwConnectionType.Invalid;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00079763 File Offset: 0x00077963
		private static bool TryParseRouterString(SapBwConnectionType connectionType, string server, out SapBwRouterString routerString)
		{
			return SapBwRouterString.TryParseRouterString(connectionType, server, '/', out routerString);
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x00079770 File Offset: 0x00077970
		private static bool TryParseRouterString(SapBwConnectionType connectionType, string server, char separator, out SapBwRouterString routerString)
		{
			bool flag;
			try
			{
				List<SapBwRouterString.Station> list = new List<SapBwRouterString.Station>();
				foreach (SapBwRouterString.Station station in SapBwRouterString.GetStations(server, separator))
				{
					if (!station.IsValid())
					{
						routerString = null;
						return false;
					}
					list.Add(station);
				}
				if (list.Count == 0)
				{
					routerString = null;
					flag = false;
				}
				else
				{
					routerString = new SapBwRouterString(list);
					flag = true;
				}
			}
			catch (FormatException)
			{
				routerString = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x00079804 File Offset: 0x00077A04
		private static IEnumerable<SapBwRouterString.Station> GetStations(string routerString, char separator)
		{
			SapBwRouterString.Station station = null;
			bool allowLowercase = separator == '|';
			foreach (KeyValuePair<char, string> part in SapBwRouterString.ExtractParts(routerString, separator))
			{
				char c = (allowLowercase ? char.ToUpperInvariant(part.Key) : part.Key);
				if (c <= 'M')
				{
					if (c != 'G')
					{
						if (c != 'H')
						{
							if (c != 'M')
							{
								goto IL_0194;
							}
							if (station != null)
							{
								yield return station;
							}
							station = new SapBwRouterString.MessageServerStation(part.Value);
						}
						else
						{
							if (station != null)
							{
								yield return station;
							}
							station = new SapBwRouterString.HostStation(part.Value);
						}
					}
					else
					{
						if (station == null || station.Kind != SapBwRouterString.StationKind.MessageServer)
						{
							throw new FormatException();
						}
						((SapBwRouterString.MessageServerStation)station).Group = part.Value;
					}
				}
				else
				{
					if (c != 'P')
					{
						if (c != 'S')
						{
							if (c != 'W')
							{
								goto IL_0194;
							}
						}
						else
						{
							if (station == null)
							{
								throw new FormatException();
							}
							station.Port = part.Value;
							goto IL_019A;
						}
					}
					if (station == null)
					{
						throw new FormatException();
					}
					station.HasPassword = true;
				}
				IL_019A:
				part = default(KeyValuePair<char, string>);
				continue;
				IL_0194:
				throw new FormatException();
			}
			IEnumerator<KeyValuePair<char, string>> enumerator = null;
			if (station != null)
			{
				yield return station;
			}
			yield break;
			yield break;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0007981C File Offset: 0x00077A1C
		private static IEnumerable<KeyValuePair<char, string>> ExtractParts(string input, char separator)
		{
			string[] array = input.Split(new char[] { separator });
			if (array.Length % 2 != 1 || array[0].Length != 0)
			{
				throw new FormatException();
			}
			int num = (array.Length - 1) / 2;
			KeyValuePair<char, string>[] array2 = new KeyValuePair<char, string>[num];
			for (int i = 0; i < num; i++)
			{
				string text = array[2 * i + 1];
				string text2 = array[2 * i + 2];
				if (text.Length != 1)
				{
					throw new FormatException();
				}
				array2[i] = new KeyValuePair<char, string>(text[0], text2);
			}
			return array2;
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000798A6 File Offset: 0x00077AA6
		public static bool IsRouterString(string serverName)
		{
			return serverName != null && (serverName.StartsWith("/H/", StringComparison.Ordinal) || serverName.StartsWith("/M/", StringComparison.Ordinal));
		}

		// Token: 0x04001166 RID: 4454
		private const char slash = '/';

		// Token: 0x04001167 RID: 4455
		private const char pipe = '|';

		// Token: 0x04001168 RID: 4456
		private const char hostPrefix = 'H';

		// Token: 0x04001169 RID: 4457
		private const char messageServerPrefix = 'M';

		// Token: 0x0400116A RID: 4458
		private const char groupPrefix = 'G';

		// Token: 0x0400116B RID: 4459
		private const char servicePortPrefix = 'S';

		// Token: 0x0400116C RID: 4460
		private const char passwordPrefix = 'W';

		// Token: 0x0400116D RID: 4461
		private const char altPasswordPrefix = 'P';

		// Token: 0x020004DD RID: 1245
		private enum StationKind
		{
			// Token: 0x04001170 RID: 4464
			Host,
			// Token: 0x04001171 RID: 4465
			MessageServer
		}

		// Token: 0x020004DE RID: 1246
		private abstract class Station
		{
			// Token: 0x17000FB4 RID: 4020
			// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000798C9 File Offset: 0x00077AC9
			// (set) Token: 0x060028A6 RID: 10406 RVA: 0x000798D1 File Offset: 0x00077AD1
			public string Host
			{
				get
				{
					return this.host;
				}
				protected set
				{
					if (this.host != null || value.Length < 2 || Uri.CheckHostName(value) == UriHostNameType.Unknown)
					{
						throw new FormatException();
					}
					this.host = value;
				}
			}

			// Token: 0x17000FB5 RID: 4021
			// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000798F9 File Offset: 0x00077AF9
			// (set) Token: 0x060028A8 RID: 10408 RVA: 0x00079904 File Offset: 0x00077B04
			public string Port
			{
				get
				{
					return this.port;
				}
				set
				{
					int num;
					if (this.port == null && int.TryParse(value, out num) && num > 0 && num <= 65535)
					{
						this.port = value;
						return;
					}
					throw new FormatException();
				}
			}

			// Token: 0x17000FB6 RID: 4022
			// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0007993C File Offset: 0x00077B3C
			// (set) Token: 0x060028AA RID: 10410 RVA: 0x00079944 File Offset: 0x00077B44
			public bool HasPassword { get; set; }

			// Token: 0x17000FB7 RID: 4023
			// (get) Token: 0x060028AB RID: 10411
			public abstract SapBwRouterString.StationKind Kind { get; }

			// Token: 0x060028AC RID: 10412 RVA: 0x0007994D File Offset: 0x00077B4D
			public bool IsValid()
			{
				return !string.IsNullOrEmpty(this.Host);
			}

			// Token: 0x060028AD RID: 10413 RVA: 0x00079960 File Offset: 0x00077B60
			public virtual void Write(TextWriter writer)
			{
				writer.Write('/');
				SapBwRouterString.StationKind kind = this.Kind;
				if (kind != SapBwRouterString.StationKind.Host)
				{
					if (kind == SapBwRouterString.StationKind.MessageServer)
					{
						writer.Write('M');
					}
				}
				else
				{
					writer.Write('H');
				}
				writer.Write('/');
				writer.Write(this.Host);
				if (this.Port != null)
				{
					writer.Write('/');
					writer.Write('S');
					writer.Write('/');
					writer.Write(this.Port);
				}
			}

			// Token: 0x060028AE RID: 10414
			public abstract IEnumerable<KeyValuePair<string, string>> GetConnectionStringProperties();

			// Token: 0x060028AF RID: 10415 RVA: 0x000799D8 File Offset: 0x00077BD8
			public static bool TryNew(Dictionary<string, string> sapNetParameters, out SapBwRouterString.Station station)
			{
				string text;
				if (sapNetParameters.TryGetValue("ASHOST", out text))
				{
					station = new SapBwRouterString.HostStation(text);
					string text2;
					if (sapNetParameters.TryGetValue("ASSERV", out text2))
					{
						station.Port = text2;
					}
					return true;
				}
				if (sapNetParameters.TryGetValue("MSHOST", out text))
				{
					station = new SapBwRouterString.MessageServerStation(text);
					string text3;
					if (sapNetParameters.TryGetValue("MSSERV", out text3))
					{
						station.Port = text3;
					}
					string text4;
					if (sapNetParameters.TryGetValue("GROUP", out text4))
					{
						((SapBwRouterString.MessageServerStation)station).Group = text4;
					}
					return true;
				}
				station = null;
				return false;
			}

			// Token: 0x04001172 RID: 4466
			private string host;

			// Token: 0x04001173 RID: 4467
			private string port;
		}

		// Token: 0x020004DF RID: 1247
		private sealed class HostStation : SapBwRouterString.Station
		{
			// Token: 0x060028B1 RID: 10417 RVA: 0x00079A65 File Offset: 0x00077C65
			public HostStation(string host)
			{
				base.Host = host;
			}

			// Token: 0x17000FB8 RID: 4024
			// (get) Token: 0x060028B2 RID: 10418 RVA: 0x00002105 File Offset: 0x00000305
			public override SapBwRouterString.StationKind Kind
			{
				get
				{
					return SapBwRouterString.StationKind.Host;
				}
			}

			// Token: 0x060028B3 RID: 10419 RVA: 0x00079A74 File Offset: 0x00077C74
			public override IEnumerable<KeyValuePair<string, string>> GetConnectionStringProperties()
			{
				yield return new KeyValuePair<string, string>("ASHOST", base.Host);
				if (base.Port != null)
				{
					yield return new KeyValuePair<string, string>("ASSERV", base.Port);
				}
				yield break;
			}
		}

		// Token: 0x020004E1 RID: 1249
		private sealed class MessageServerStation : SapBwRouterString.Station
		{
			// Token: 0x060028BC RID: 10428 RVA: 0x00079A65 File Offset: 0x00077C65
			public MessageServerStation(string host)
			{
				base.Host = host;
			}

			// Token: 0x17000FBB RID: 4027
			// (get) Token: 0x060028BD RID: 10429 RVA: 0x00002139 File Offset: 0x00000339
			public override SapBwRouterString.StationKind Kind
			{
				get
				{
					return SapBwRouterString.StationKind.MessageServer;
				}
			}

			// Token: 0x17000FBC RID: 4028
			// (get) Token: 0x060028BE RID: 10430 RVA: 0x00079B8B File Offset: 0x00077D8B
			// (set) Token: 0x060028BF RID: 10431 RVA: 0x00079B93 File Offset: 0x00077D93
			public string Group
			{
				get
				{
					return this.group;
				}
				set
				{
					if (this.group != null || string.IsNullOrEmpty(value))
					{
						throw new FormatException();
					}
					this.group = value;
				}
			}

			// Token: 0x060028C0 RID: 10432 RVA: 0x00079BB2 File Offset: 0x00077DB2
			public override void Write(TextWriter writer)
			{
				base.Write(writer);
				if (this.Group != null)
				{
					writer.Write('/');
					writer.Write('G');
					writer.Write('/');
					writer.Write(this.Group);
				}
			}

			// Token: 0x060028C1 RID: 10433 RVA: 0x00079BE7 File Offset: 0x00077DE7
			public override IEnumerable<KeyValuePair<string, string>> GetConnectionStringProperties()
			{
				yield return new KeyValuePair<string, string>("MessageServer", base.Host);
				if (base.Port != null)
				{
					yield return new KeyValuePair<string, string>("MSSERV", base.Port);
				}
				if (this.Group != null)
				{
					yield return new KeyValuePair<string, string>("LogonGroup", this.Group);
				}
				yield break;
			}

			// Token: 0x04001179 RID: 4473
			private string group;
		}
	}
}
