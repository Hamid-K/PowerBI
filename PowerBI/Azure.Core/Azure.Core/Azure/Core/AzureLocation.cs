using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct AzureLocation : IEquatable<AzureLocation>
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005055 File Offset: 0x00003255
		private static Dictionary<string, AzureLocation> PublicCloudLocations { get; } = new Dictionary<string, AzureLocation>();

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000505C File Offset: 0x0000325C
		public static AzureLocation EastAsia { get; } = AzureLocation.CreateStaticReference("eastasia", "East Asia");

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005063 File Offset: 0x00003263
		public static AzureLocation SoutheastAsia { get; } = AzureLocation.CreateStaticReference("southeastasia", "Southeast Asia");

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000506A File Offset: 0x0000326A
		public static AzureLocation CentralUS { get; } = AzureLocation.CreateStaticReference("centralus", "Central US");

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005071 File Offset: 0x00003271
		public static AzureLocation EastUS { get; } = AzureLocation.CreateStaticReference("eastus", "East US");

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005078 File Offset: 0x00003278
		public static AzureLocation EastUS2 { get; } = AzureLocation.CreateStaticReference("eastus2", "East US 2");

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000507F File Offset: 0x0000327F
		public static AzureLocation WestUS { get; } = AzureLocation.CreateStaticReference("westus", "West US");

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005086 File Offset: 0x00003286
		public static AzureLocation WestUS2 { get; } = AzureLocation.CreateStaticReference("westus2", "West US 2");

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000508D File Offset: 0x0000328D
		public static AzureLocation WestUS3 { get; } = AzureLocation.CreateStaticReference("westus3", "West US 3");

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005094 File Offset: 0x00003294
		public static AzureLocation NorthCentralUS { get; } = AzureLocation.CreateStaticReference("northcentralus", "North Central US");

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000509B File Offset: 0x0000329B
		public static AzureLocation SouthCentralUS { get; } = AzureLocation.CreateStaticReference("southcentralus", "South Central US");

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000050A2 File Offset: 0x000032A2
		public static AzureLocation NorthEurope { get; } = AzureLocation.CreateStaticReference("northeurope", "North Europe");

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000050A9 File Offset: 0x000032A9
		public static AzureLocation WestEurope { get; } = AzureLocation.CreateStaticReference("westeurope", "West Europe");

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000050B0 File Offset: 0x000032B0
		public static AzureLocation JapanWest { get; } = AzureLocation.CreateStaticReference("japanwest", "Japan West");

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000050B7 File Offset: 0x000032B7
		public static AzureLocation JapanEast { get; } = AzureLocation.CreateStaticReference("japaneast", "Japan East");

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000050BE File Offset: 0x000032BE
		public static AzureLocation BrazilSouth { get; } = AzureLocation.CreateStaticReference("brazilsouth", "Brazil South");

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000050C5 File Offset: 0x000032C5
		public static AzureLocation AustraliaEast { get; } = AzureLocation.CreateStaticReference("australiaeast", "Australia East");

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000050CC File Offset: 0x000032CC
		public static AzureLocation AustraliaSoutheast { get; } = AzureLocation.CreateStaticReference("australiasoutheast", "Australia Southeast");

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000050D3 File Offset: 0x000032D3
		public static AzureLocation SouthIndia { get; } = AzureLocation.CreateStaticReference("southindia", "South India");

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000050DA File Offset: 0x000032DA
		public static AzureLocation CentralIndia { get; } = AzureLocation.CreateStaticReference("centralindia", "Central India");

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000050E1 File Offset: 0x000032E1
		public static AzureLocation WestIndia { get; } = AzureLocation.CreateStaticReference("westindia", "West India");

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000050E8 File Offset: 0x000032E8
		public static AzureLocation CanadaCentral { get; } = AzureLocation.CreateStaticReference("canadacentral", "Canada Central");

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000050EF File Offset: 0x000032EF
		public static AzureLocation CanadaEast { get; } = AzureLocation.CreateStaticReference("canadaeast", "Canada East");

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000050F6 File Offset: 0x000032F6
		public static AzureLocation UKSouth { get; } = AzureLocation.CreateStaticReference("uksouth", "UK South");

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000050FD File Offset: 0x000032FD
		public static AzureLocation UKWest { get; } = AzureLocation.CreateStaticReference("ukwest", "UK West");

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005104 File Offset: 0x00003304
		public static AzureLocation WestCentralUS { get; } = AzureLocation.CreateStaticReference("westcentralus", "West Central US");

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000510B File Offset: 0x0000330B
		public static AzureLocation KoreaCentral { get; } = AzureLocation.CreateStaticReference("koreacentral", "Korea Central");

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005112 File Offset: 0x00003312
		public static AzureLocation KoreaSouth { get; } = AzureLocation.CreateStaticReference("koreasouth", "Korea South");

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005119 File Offset: 0x00003319
		public static AzureLocation FranceCentral { get; } = AzureLocation.CreateStaticReference("francecentral", "France Central");

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005120 File Offset: 0x00003320
		public static AzureLocation FranceSouth { get; } = AzureLocation.CreateStaticReference("francesouth", "France South");

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005127 File Offset: 0x00003327
		public static AzureLocation AustraliaCentral { get; } = AzureLocation.CreateStaticReference("australiacentral", "Australia Central");

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000512E File Offset: 0x0000332E
		public static AzureLocation AustraliaCentral2 { get; } = AzureLocation.CreateStaticReference("australiacentral2", "Australia Central 2");

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005135 File Offset: 0x00003335
		public static AzureLocation UAECentral { get; } = AzureLocation.CreateStaticReference("uaecentral", "UAE Central");

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000513C File Offset: 0x0000333C
		public static AzureLocation UAENorth { get; } = AzureLocation.CreateStaticReference("uaenorth", "UAE North");

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005143 File Offset: 0x00003343
		public static AzureLocation SouthAfricaNorth { get; } = AzureLocation.CreateStaticReference("southafricanorth", "South Africa North");

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000514A File Offset: 0x0000334A
		public static AzureLocation SouthAfricaWest { get; } = AzureLocation.CreateStaticReference("southafricawest", "South Africa West");

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005151 File Offset: 0x00003351
		public static AzureLocation SwedenCentral { get; } = AzureLocation.CreateStaticReference("swedencentral", "Sweden Central");

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005158 File Offset: 0x00003358
		public static AzureLocation SwedenSouth { get; } = AzureLocation.CreateStaticReference("swedensouth", "Sweden South");

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000515F File Offset: 0x0000335F
		public static AzureLocation SwitzerlandNorth { get; } = AzureLocation.CreateStaticReference("switzerlandnorth", "Switzerland North");

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005166 File Offset: 0x00003366
		public static AzureLocation SwitzerlandWest { get; } = AzureLocation.CreateStaticReference("switzerlandwest", "Switzerland West");

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000516D File Offset: 0x0000336D
		public static AzureLocation GermanyNorth { get; } = AzureLocation.CreateStaticReference("germanynorth", "Germany North");

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005174 File Offset: 0x00003374
		public static AzureLocation GermanyWestCentral { get; } = AzureLocation.CreateStaticReference("germanywestcentral", "Germany West Central");

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000517B File Offset: 0x0000337B
		public static AzureLocation GermanyCentral { get; } = AzureLocation.CreateStaticReference("germanycentral", "Germany Central");

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005182 File Offset: 0x00003382
		public static AzureLocation GermanyNorthEast { get; } = AzureLocation.CreateStaticReference("germanynortheast", "Germany Northeast");

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005189 File Offset: 0x00003389
		public static AzureLocation NorwayWest { get; } = AzureLocation.CreateStaticReference("norwaywest", "Norway West");

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005190 File Offset: 0x00003390
		public static AzureLocation NorwayEast { get; } = AzureLocation.CreateStaticReference("norwayeast", "Norway East");

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005197 File Offset: 0x00003397
		public static AzureLocation BrazilSoutheast { get; } = AzureLocation.CreateStaticReference("brazilsoutheast", "Brazil Southeast");

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000519E File Offset: 0x0000339E
		public static AzureLocation ChinaNorth { get; } = AzureLocation.CreateStaticReference("chinanorth", "China North");

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000051A5 File Offset: 0x000033A5
		public static AzureLocation ChinaEast { get; } = AzureLocation.CreateStaticReference("chinaeast", "China East");

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000051AC File Offset: 0x000033AC
		public static AzureLocation ChinaNorth2 { get; } = AzureLocation.CreateStaticReference("chinanorth2", "China North 2");

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000051B3 File Offset: 0x000033B3
		public static AzureLocation ChinaNorth3 { get; } = AzureLocation.CreateStaticReference("chinanorth3", "China North 3");

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000051BA File Offset: 0x000033BA
		public static AzureLocation ChinaEast2 { get; } = AzureLocation.CreateStaticReference("chinaeast2", "China East 2");

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000051C1 File Offset: 0x000033C1
		public static AzureLocation ChinaEast3 { get; } = AzureLocation.CreateStaticReference("chinaeast3", "China East 3");

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000051C8 File Offset: 0x000033C8
		public static AzureLocation QatarCentral { get; } = AzureLocation.CreateStaticReference("qatarcentral", "Qatar Central");

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000051CF File Offset: 0x000033CF
		public static AzureLocation USDoDCentral { get; } = AzureLocation.CreateStaticReference("usdodcentral", "US DoD Central");

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000051D6 File Offset: 0x000033D6
		public static AzureLocation USDoDEast { get; } = AzureLocation.CreateStaticReference("usdodeast", "US DoD East");

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000051DD File Offset: 0x000033DD
		public static AzureLocation USGovArizona { get; } = AzureLocation.CreateStaticReference("usgovarizona", "US Gov Arizona");

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000051E4 File Offset: 0x000033E4
		public static AzureLocation USGovTexas { get; } = AzureLocation.CreateStaticReference("usgovtexas", "US Gov Texas");

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000051EB File Offset: 0x000033EB
		public static AzureLocation USGovVirginia { get; } = AzureLocation.CreateStaticReference("usgovvirginia", "US Gov Virginia");

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000051F2 File Offset: 0x000033F2
		public static AzureLocation USGovIowa { get; } = AzureLocation.CreateStaticReference("usgoviowa", "US Gov Iowa");

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000051F9 File Offset: 0x000033F9
		public static AzureLocation IsraelCentral { get; } = AzureLocation.CreateStaticReference("israelcentral", "Israel Central");

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005200 File Offset: 0x00003400
		public static AzureLocation ItalyNorth { get; } = AzureLocation.CreateStaticReference("italynorth", "Italy North");

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005207 File Offset: 0x00003407
		public static AzureLocation PolandCentral { get; } = AzureLocation.CreateStaticReference("polandcentral", "Poland Central");

		// Token: 0x0600019D RID: 413 RVA: 0x00005210 File Offset: 0x00003410
		public AzureLocation(string location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			bool flag;
			this.Name = AzureLocation.GetNameFromDisplayName(location, out flag);
			string text = (flag ? this.Name : location.ToLowerInvariant());
			AzureLocation azureLocation;
			if (AzureLocation.PublicCloudLocations.TryGetValue(text, out azureLocation))
			{
				this.Name = azureLocation.Name;
				this.DisplayName = azureLocation.DisplayName;
				return;
			}
			this.DisplayName = (flag ? location : null);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005282 File Offset: 0x00003482
		public AzureLocation(string name, string displayName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.DisplayName = displayName;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000052A0 File Offset: 0x000034A0
		private static string GetNameFromDisplayName(string name, out bool foundSpace)
		{
			foundSpace = false;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in name)
			{
				if (c == ' ')
				{
					foundSpace = true;
				}
				else
				{
					stringBuilder.Append(char.ToLowerInvariant(c));
				}
			}
			if (!foundSpace)
			{
				return name;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000052F4 File Offset: 0x000034F4
		public string Name { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000052FC File Offset: 0x000034FC
		[Nullable(2)]
		public string DisplayName
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005304 File Offset: 0x00003504
		private static AzureLocation CreateStaticReference(string name, string displayName)
		{
			AzureLocation azureLocation = new AzureLocation(name, displayName);
			AzureLocation.PublicCloudLocations.Add(name, azureLocation);
			return azureLocation;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005327 File Offset: 0x00003527
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005330 File Offset: 0x00003530
		public static implicit operator AzureLocation(string location)
		{
			AzureLocation azureLocation;
			if (location != null && AzureLocation.PublicCloudLocations.TryGetValue(location, out azureLocation))
			{
				return azureLocation;
			}
			return new AzureLocation(location);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005357 File Offset: 0x00003557
		public bool Equals(AzureLocation other)
		{
			return this.Name == other.Name;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000536B File Offset: 0x0000356B
		public static implicit operator string(AzureLocation location)
		{
			return location.ToString();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000537C File Offset: 0x0000357C
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is AzureLocation)
			{
				AzureLocation azureLocation = (AzureLocation)obj;
				return this.Equals(azureLocation);
			}
			return false;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000053A8 File Offset: 0x000035A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000053B5 File Offset: 0x000035B5
		public static bool operator ==(AzureLocation left, AzureLocation right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000053BF File Offset: 0x000035BF
		public static bool operator !=(AzureLocation left, AzureLocation right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000086 RID: 134
		private const char Space = ' ';
	}
}
