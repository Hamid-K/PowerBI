using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000072 RID: 114
	public sealed class ODataMessageReaderSettings
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000BB18 File Offset: 0x00009D18
		public ODataMessageReaderSettings()
		{
			this.ClientCustomTypeResolver = null;
			this.PrimitiveTypeResolver = null;
			this.EnablePrimitiveTypeConversion = true;
			this.EnableMessageStreamDisposal = true;
			this.EnableCharactersCheck = false;
			this.ReadUntypedAsString = true;
			this.MaxProtocolVersion = ODataVersion.V4;
			this.Validations = ValidationKinds.All;
			this.Validator = new ReaderValidator(this);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000BB6F File Offset: 0x00009D6F
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000BB77 File Offset: 0x00009D77
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000BBB3 File Offset: 0x00009DB3
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000BBBB File Offset: 0x00009DBB
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000BBC9 File Offset: 0x00009DC9
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000BBD1 File Offset: 0x00009DD1
		public Func<IEdmType, string, IEdmType> ClientCustomTypeResolver { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000BBDA File Offset: 0x00009DDA
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000BBE2 File Offset: 0x00009DE2
		public Func<object, string, IEdmTypeReference> PrimitiveTypeResolver { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000BBEB File Offset: 0x00009DEB
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000BBF3 File Offset: 0x00009DF3
		public bool EnablePrimitiveTypeConversion { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000BBFC File Offset: 0x00009DFC
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000BC04 File Offset: 0x00009E04
		public bool EnableMessageStreamDisposal { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000BC0D File Offset: 0x00009E0D
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000BC15 File Offset: 0x00009E15
		public bool EnableCharactersCheck { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000BC1E File Offset: 0x00009E1E
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000BC26 File Offset: 0x00009E26
		public ODataVersion MaxProtocolVersion { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000BC2F File Offset: 0x00009E2F
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000BC4A File Offset: 0x00009E4A
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000BC53 File Offset: 0x00009E53
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0000BC5B File Offset: 0x00009E5B
		public bool ReadUntypedAsString { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000BC64 File Offset: 0x00009E64
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public Func<string, bool> ShouldIncludeAnnotation { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000BC75 File Offset: 0x00009E75
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000BC7D File Offset: 0x00009E7D
		internal IReaderValidator Validator { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000BC86 File Offset: 0x00009E86
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000BC8E File Offset: 0x00009E8E
		internal bool ThrowOnDuplicatePropertyNames { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000BC97 File Offset: 0x00009E97
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000BC9F File Offset: 0x00009E9F
		internal bool ThrowIfTypeConflictsWithMetadata { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		internal bool ThrowOnUndeclaredPropertyForNonOpenType { get; private set; }

		// Token: 0x0600040A RID: 1034 RVA: 0x0000BCBC File Offset: 0x00009EBC
		public ODataMessageReaderSettings Clone()
		{
			ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings();
			odataMessageReaderSettings.CopyFrom(this);
			return odataMessageReaderSettings;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000BCD8 File Offset: 0x00009ED8
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

		// Token: 0x0600040C RID: 1036 RVA: 0x0000BD02 File Offset: 0x00009F02
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation.Invoke(annotationName);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000BD20 File Offset: 0x00009F20
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
		}

		// Token: 0x040001FA RID: 506
		private Uri baseUri;

		// Token: 0x040001FB RID: 507
		private ODataMessageQuotas messageQuotas;

		// Token: 0x040001FC RID: 508
		private ValidationKinds validations;
	}
}
