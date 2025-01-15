using System;
using Microsoft.OData.Buffers;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000098 RID: 152
	public sealed class ODataMessageReaderSettings
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0000E7B2 File Offset: 0x0000C9B2
		public ODataMessageReaderSettings()
			: this(ODataVersion.V4)
		{
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		public ODataMessageReaderSettings(ODataVersion odataVersion)
		{
			this.ClientCustomTypeResolver = null;
			this.PrimitiveTypeResolver = null;
			this.EnablePrimitiveTypeConversion = true;
			this.EnableMessageStreamDisposal = true;
			this.EnableCharactersCheck = false;
			this.Version = new ODataVersion?(odataVersion);
			this.LibraryCompatibility = ODataLibraryCompatibility.Latest;
			this.Validator = new ReaderValidator(this);
			if (odataVersion < ODataVersion.V401)
			{
				this.Validations = ValidationKinds.All;
				this.ReadUntypedAsString = true;
				this.MaxProtocolVersion = ODataVersion.V4;
				return;
			}
			this.Validations = ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType;
			this.ReadUntypedAsString = false;
			this.MaxProtocolVersion = odataVersion;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000E845 File Offset: 0x0000CA45
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000E84D File Offset: 0x0000CA4D
		public ODataLibraryCompatibility LibraryCompatibility { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000E856 File Offset: 0x0000CA56
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000E85E File Offset: 0x0000CA5E
		public ODataVersion? Version { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000E867 File Offset: 0x0000CA67
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000E86F File Offset: 0x0000CA6F
		public ICharArrayPool ArrayPool { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000E878 File Offset: 0x0000CA78
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000E880 File Offset: 0x0000CA80
		public ValidationKinds Validations
		{
			get
			{
				return this.validations;
			}
			set
			{
				this.validations = value;
				this.ThrowOnDuplicatePropertyNames = (this.validations & ValidationKinds.ThrowOnDuplicatePropertyNames) > ValidationKinds.None;
				this.ThrowIfTypeConflictsWithMetadata = (this.validations & ValidationKinds.ThrowIfTypeConflictsWithMetadata) > ValidationKinds.None;
				this.ThrowOnUndeclaredPropertyForNonOpenType = (this.validations & ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType) > ValidationKinds.None;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000E8BC File Offset: 0x0000CABC
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = UriUtils.EnsureTaillingSlash(value);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000E8D2 File Offset: 0x0000CAD2
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0000E8DA File Offset: 0x0000CADA
		public Func<IEdmType, string, IEdmType> ClientCustomTypeResolver { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000E8E3 File Offset: 0x0000CAE3
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000E8EB File Offset: 0x0000CAEB
		public Func<object, string, IEdmTypeReference> PrimitiveTypeResolver { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		public bool EnablePrimitiveTypeConversion { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000E905 File Offset: 0x0000CB05
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000E90D File Offset: 0x0000CB0D
		public bool EnableMessageStreamDisposal { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000E916 File Offset: 0x0000CB16
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000E91E File Offset: 0x0000CB1E
		public bool EnableCharactersCheck { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000E927 File Offset: 0x0000CB27
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000E92F File Offset: 0x0000CB2F
		public ODataVersion MaxProtocolVersion { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000E938 File Offset: 0x0000CB38
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0000E953 File Offset: 0x0000CB53
		public ODataMessageQuotas MessageQuotas
		{
			get
			{
				if (this.messageQuotas == null)
				{
					this.messageQuotas = new ODataMessageQuotas();
				}
				return this.messageQuotas;
			}
			set
			{
				this.messageQuotas = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000E95C File Offset: 0x0000CB5C
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000E964 File Offset: 0x0000CB64
		public bool ReadUntypedAsString { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000E96D File Offset: 0x0000CB6D
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000E975 File Offset: 0x0000CB75
		public Func<IEdmPrimitiveType, bool, string, IEdmProperty, bool> ReadAsStreamFunc { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000E97E File Offset: 0x0000CB7E
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000E986 File Offset: 0x0000CB86
		public Func<string, bool> ShouldIncludeAnnotation { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000E98F File Offset: 0x0000CB8F
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000E997 File Offset: 0x0000CB97
		internal IReaderValidator Validator { get; private set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		internal bool ThrowOnDuplicatePropertyNames { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000E9B1 File Offset: 0x0000CBB1
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
		internal bool ThrowIfTypeConflictsWithMetadata { get; private set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000E9C2 File Offset: 0x0000CBC2
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000E9CA File Offset: 0x0000CBCA
		internal bool ThrowOnUndeclaredPropertyForNonOpenType { get; private set; }

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		public ODataMessageReaderSettings Clone()
		{
			ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings();
			odataMessageReaderSettings.CopyFrom(this);
			return odataMessageReaderSettings;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		internal static ODataMessageReaderSettings CreateReaderSettings(IServiceProvider container, ODataMessageReaderSettings other)
		{
			ODataMessageReaderSettings odataMessageReaderSettings;
			if (container == null)
			{
				odataMessageReaderSettings = new ODataMessageReaderSettings();
			}
			else
			{
				odataMessageReaderSettings = container.GetRequiredService<ODataMessageReaderSettings>();
			}
			if (other != null)
			{
				odataMessageReaderSettings.CopyFrom(other);
			}
			return odataMessageReaderSettings;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000EA1A File Offset: 0x0000CC1A
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation(annotationName);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000EA38 File Offset: 0x0000CC38
		private void CopyFrom(ODataMessageReaderSettings other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(other, "other");
			this.BaseUri = other.BaseUri;
			this.ClientCustomTypeResolver = other.ClientCustomTypeResolver;
			this.PrimitiveTypeResolver = other.PrimitiveTypeResolver;
			this.EnableMessageStreamDisposal = other.EnableMessageStreamDisposal;
			this.EnablePrimitiveTypeConversion = other.EnablePrimitiveTypeConversion;
			this.EnableCharactersCheck = other.EnableCharactersCheck;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
			this.MaxProtocolVersion = other.MaxProtocolVersion;
			this.ReadUntypedAsString = other.ReadUntypedAsString;
			this.ShouldIncludeAnnotation = other.ShouldIncludeAnnotation;
			this.validations = other.validations;
			this.ThrowOnDuplicatePropertyNames = other.ThrowOnDuplicatePropertyNames;
			this.ThrowIfTypeConflictsWithMetadata = other.ThrowIfTypeConflictsWithMetadata;
			this.ThrowOnUndeclaredPropertyForNonOpenType = other.ThrowOnUndeclaredPropertyForNonOpenType;
			this.LibraryCompatibility = other.LibraryCompatibility;
			this.Version = other.Version;
			this.ReadAsStreamFunc = other.ReadAsStreamFunc;
		}

		// Token: 0x0400025A RID: 602
		private Uri baseUri;

		// Token: 0x0400025B RID: 603
		private ODataMessageQuotas messageQuotas;

		// Token: 0x0400025C RID: 604
		private ValidationKinds validations;
	}
}
