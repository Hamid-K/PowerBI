using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200018F RID: 399
	public class TokenValidationParameters
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x000427EC File Offset: 0x000409EC
		protected TokenValidationParameters(TokenValidationParameters other)
		{
			if (other == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentNullException("other"));
			}
			this.AlgorithmValidator = other.AlgorithmValidator;
			TokenValidationParameters actorValidationParameters = other.ActorValidationParameters;
			this.ActorValidationParameters = ((actorValidationParameters != null) ? actorValidationParameters.Clone() : null);
			this.AudienceValidator = other.AudienceValidator;
			this._authenticationType = other._authenticationType;
			this.ClockSkew = other.ClockSkew;
			this.ConfigurationManager = other.ConfigurationManager;
			this.CryptoProviderFactory = other.CryptoProviderFactory;
			this.DebugId = other.DebugId;
			this.IncludeTokenOnFailedValidation = other.IncludeTokenOnFailedValidation;
			this.IgnoreTrailingSlashWhenValidatingAudience = other.IgnoreTrailingSlashWhenValidatingAudience;
			this.IssuerSigningKey = other.IssuerSigningKey;
			this.IssuerSigningKeyResolver = other.IssuerSigningKeyResolver;
			this.IssuerSigningKeyResolverUsingConfiguration = other.IssuerSigningKeyResolverUsingConfiguration;
			this.IssuerSigningKeys = other.IssuerSigningKeys;
			this.IssuerSigningKeyValidator = other.IssuerSigningKeyValidator;
			this.IssuerSigningKeyValidatorUsingConfiguration = other.IssuerSigningKeyValidatorUsingConfiguration;
			this.IssuerValidator = other.IssuerValidator;
			this.IssuerValidatorAsync = other.IssuerValidatorAsync;
			this.IssuerValidatorUsingConfiguration = other.IssuerValidatorUsingConfiguration;
			this.LifetimeValidator = other.LifetimeValidator;
			this.LogTokenId = other.LogTokenId;
			this.LogValidationExceptions = other.LogValidationExceptions;
			this.NameClaimType = other.NameClaimType;
			this.NameClaimTypeRetriever = other.NameClaimTypeRetriever;
			this.PropertyBag = other.PropertyBag;
			this.RefreshBeforeValidation = other.RefreshBeforeValidation;
			this.RequireAudience = other.RequireAudience;
			this.RequireExpirationTime = other.RequireExpirationTime;
			this.RequireSignedTokens = other.RequireSignedTokens;
			this.RoleClaimType = other.RoleClaimType;
			this.RoleClaimTypeRetriever = other.RoleClaimTypeRetriever;
			this.SaveSigninToken = other.SaveSigninToken;
			this.SignatureValidator = other.SignatureValidator;
			this.SignatureValidatorUsingConfiguration = other.SignatureValidatorUsingConfiguration;
			this.TokenDecryptionKey = other.TokenDecryptionKey;
			this.TokenDecryptionKeyResolver = other.TokenDecryptionKeyResolver;
			this.TokenDecryptionKeys = other.TokenDecryptionKeys;
			this.TokenReader = other.TokenReader;
			this.TokenReplayCache = other.TokenReplayCache;
			this.TokenReplayValidator = other.TokenReplayValidator;
			this.TransformBeforeSignatureValidation = other.TransformBeforeSignatureValidation;
			this.TryAllIssuerSigningKeys = other.TryAllIssuerSigningKeys;
			this.TypeValidator = other.TypeValidator;
			this.ValidateActor = other.ValidateActor;
			this.ValidateAudience = other.ValidateAudience;
			this.ValidateIssuer = other.ValidateIssuer;
			this.ValidateIssuerSigningKey = other.ValidateIssuerSigningKey;
			this.ValidateLifetime = other.ValidateLifetime;
			this.ValidateSignatureLast = other.ValidateSignatureLast;
			this.ValidateTokenReplay = other.ValidateTokenReplay;
			this.ValidateWithLKG = other.ValidateWithLKG;
			this.ValidAlgorithms = other.ValidAlgorithms;
			this.ValidAudience = other.ValidAudience;
			this.ValidAudiences = other.ValidAudiences;
			this.ValidIssuer = other.ValidIssuer;
			this.ValidIssuers = other.ValidIssuers;
			this.ValidTypes = other.ValidTypes;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00042B00 File Offset: 0x00040D00
		public TokenValidationParameters()
		{
			this.LogTokenId = true;
			this.LogValidationExceptions = true;
			this.RequireExpirationTime = true;
			this.RequireSignedTokens = true;
			this.RequireAudience = true;
			this.SaveSigninToken = false;
			this.TryAllIssuerSigningKeys = true;
			this.ValidateActor = false;
			this.ValidateAudience = true;
			this.ValidateIssuer = true;
			this.ValidateIssuerSigningKey = false;
			this.ValidateLifetime = true;
			this.ValidateTokenReplay = false;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00042BA1 File Offset: 0x00040DA1
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x00042BA9 File Offset: 0x00040DA9
		public TokenValidationParameters ActorValidationParameters { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00042BB2 File Offset: 0x00040DB2
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x00042BBA File Offset: 0x00040DBA
		public AlgorithmValidator AlgorithmValidator { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00042BC3 File Offset: 0x00040DC3
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x00042BCB File Offset: 0x00040DCB
		public AudienceValidator AudienceValidator { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00042BD4 File Offset: 0x00040DD4
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x00042BDC File Offset: 0x00040DDC
		public string AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentNullException("AuthenticationType"));
				}
				this._authenticationType = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00042BFD File Offset: 0x00040DFD
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x00042C08 File Offset: 0x00040E08
		[DefaultValue(300)]
		public TimeSpan ClockSkew
		{
			get
			{
				return this._clockSkew;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10100: ClockSkew must be greater than TimeSpan.Zero. value: '{0}'", new object[] { LogHelper.MarkAsNonPII(value) })));
				}
				this._clockSkew = value;
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00042C57 File Offset: 0x00040E57
		public virtual TokenValidationParameters Clone()
		{
			return new TokenValidationParameters(this)
			{
				IsClone = true
			};
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00042C68 File Offset: 0x00040E68
		public virtual ClaimsIdentity CreateClaimsIdentity(SecurityToken securityToken, string issuer)
		{
			string text;
			if (this.NameClaimTypeRetriever != null)
			{
				text = this.NameClaimTypeRetriever(securityToken, issuer);
			}
			else
			{
				text = this.NameClaimType;
			}
			string text2;
			if (this.RoleClaimTypeRetriever != null)
			{
				text2 = this.RoleClaimTypeRetriever(securityToken, issuer);
			}
			else
			{
				text2 = this.RoleClaimType;
			}
			LogHelper.LogInformation("IDX10245: Creating claims identity from the validated token: '{0}'.", new object[] { securityToken });
			return new ClaimsIdentity(this.AuthenticationType ?? TokenValidationParameters.DefaultAuthenticationType, text ?? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", text2 ?? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00042CF3 File Offset: 0x00040EF3
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x00042CFB File Offset: 0x00040EFB
		public BaseConfigurationManager ConfigurationManager { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00042D04 File Offset: 0x00040F04
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x00042D0C File Offset: 0x00040F0C
		public CryptoProviderFactory CryptoProviderFactory { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00042D15 File Offset: 0x00040F15
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00042D1D File Offset: 0x00040F1D
		public string DebugId { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00042D26 File Offset: 0x00040F26
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x00042D2E File Offset: 0x00040F2E
		[DefaultValue(true)]
		public bool IgnoreTrailingSlashWhenValidatingAudience { get; set; } = true;

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00042D37 File Offset: 0x00040F37
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00042D3F File Offset: 0x00040F3F
		public bool IncludeTokenOnFailedValidation { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00042D48 File Offset: 0x00040F48
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00042D50 File Offset: 0x00040F50
		public IssuerSigningKeyValidator IssuerSigningKeyValidator { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00042D59 File Offset: 0x00040F59
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00042D61 File Offset: 0x00040F61
		public IssuerSigningKeyValidatorUsingConfiguration IssuerSigningKeyValidatorUsingConfiguration { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00042D6A File Offset: 0x00040F6A
		public IDictionary<string, object> InstancePropertyBag { get; } = new Dictionary<string, object>();

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00042D72 File Offset: 0x00040F72
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x00042D7A File Offset: 0x00040F7A
		public bool IsClone { get; protected set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00042D83 File Offset: 0x00040F83
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x00042D8B File Offset: 0x00040F8B
		public SecurityKey IssuerSigningKey { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00042D94 File Offset: 0x00040F94
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x00042D9C File Offset: 0x00040F9C
		public IssuerSigningKeyResolver IssuerSigningKeyResolver { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00042DA5 File Offset: 0x00040FA5
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00042DAD File Offset: 0x00040FAD
		public IssuerSigningKeyResolverUsingConfiguration IssuerSigningKeyResolverUsingConfiguration { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00042DB6 File Offset: 0x00040FB6
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00042DBE File Offset: 0x00040FBE
		public IEnumerable<SecurityKey> IssuerSigningKeys { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00042DC7 File Offset: 0x00040FC7
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00042DCF File Offset: 0x00040FCF
		public IssuerValidator IssuerValidator { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00042DD8 File Offset: 0x00040FD8
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00042DE0 File Offset: 0x00040FE0
		internal IssuerValidatorAsync IssuerValidatorAsync { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00042DE9 File Offset: 0x00040FE9
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00042DF1 File Offset: 0x00040FF1
		public IssuerValidatorUsingConfiguration IssuerValidatorUsingConfiguration { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00042DFA File Offset: 0x00040FFA
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00042E02 File Offset: 0x00041002
		public TransformBeforeSignatureValidation TransformBeforeSignatureValidation { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00042E0B File Offset: 0x0004100B
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00042E13 File Offset: 0x00041013
		public LifetimeValidator LifetimeValidator { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00042E1C File Offset: 0x0004101C
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00042E24 File Offset: 0x00041024
		[DefaultValue(true)]
		public bool LogTokenId { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00042E2D File Offset: 0x0004102D
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00042E35 File Offset: 0x00041035
		[DefaultValue(true)]
		public bool LogValidationExceptions { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00042E3E File Offset: 0x0004103E
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00042E46 File Offset: 0x00041046
		public string NameClaimType
		{
			get
			{
				return this._nameClaimType;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", "IDX10102: NameClaimType cannot be null or whitespace."));
				}
				this._nameClaimType = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00042E6C File Offset: 0x0004106C
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00042E74 File Offset: 0x00041074
		public Func<SecurityToken, string, string> NameClaimTypeRetriever { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00042E7D File Offset: 0x0004107D
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00042E85 File Offset: 0x00041085
		public IDictionary<string, object> PropertyBag { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00042E8E File Offset: 0x0004108E
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00042E96 File Offset: 0x00041096
		[DefaultValue(false)]
		public bool RefreshBeforeValidation { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00042E9F File Offset: 0x0004109F
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00042EA7 File Offset: 0x000410A7
		[DefaultValue(true)]
		public bool RequireAudience { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00042EB0 File Offset: 0x000410B0
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00042EB8 File Offset: 0x000410B8
		[DefaultValue(true)]
		public bool RequireExpirationTime { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00042EC1 File Offset: 0x000410C1
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x00042EC9 File Offset: 0x000410C9
		[DefaultValue(true)]
		public bool RequireSignedTokens { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00042ED2 File Offset: 0x000410D2
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x00042EDA File Offset: 0x000410DA
		public string RoleClaimType
		{
			get
			{
				return this._roleClaimType;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", "IDX10103: RoleClaimType cannot be null or whitespace."));
				}
				this._roleClaimType = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00042F00 File Offset: 0x00041100
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00042F08 File Offset: 0x00041108
		public Func<SecurityToken, string, string> RoleClaimTypeRetriever { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00042F11 File Offset: 0x00041111
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00042F19 File Offset: 0x00041119
		[DefaultValue(false)]
		public bool SaveSigninToken { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00042F22 File Offset: 0x00041122
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x00042F2A File Offset: 0x0004112A
		public SignatureValidator SignatureValidator { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00042F33 File Offset: 0x00041133
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00042F3B File Offset: 0x0004113B
		public SignatureValidatorUsingConfiguration SignatureValidatorUsingConfiguration { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00042F44 File Offset: 0x00041144
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00042F4C File Offset: 0x0004114C
		public SecurityKey TokenDecryptionKey { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00042F55 File Offset: 0x00041155
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00042F5D File Offset: 0x0004115D
		public TokenDecryptionKeyResolver TokenDecryptionKeyResolver { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00042F66 File Offset: 0x00041166
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x00042F6E File Offset: 0x0004116E
		public IEnumerable<SecurityKey> TokenDecryptionKeys { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00042F77 File Offset: 0x00041177
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x00042F7F File Offset: 0x0004117F
		public TokenReader TokenReader { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00042F88 File Offset: 0x00041188
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x00042F90 File Offset: 0x00041190
		public ITokenReplayCache TokenReplayCache { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00042F99 File Offset: 0x00041199
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x00042FA1 File Offset: 0x000411A1
		public TokenReplayValidator TokenReplayValidator { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00042FAA File Offset: 0x000411AA
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00042FB2 File Offset: 0x000411B2
		[DefaultValue(true)]
		public bool TryAllIssuerSigningKeys { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00042FBB File Offset: 0x000411BB
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00042FC3 File Offset: 0x000411C3
		public TypeValidator TypeValidator { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00042FCC File Offset: 0x000411CC
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x00042FD4 File Offset: 0x000411D4
		[DefaultValue(false)]
		public bool ValidateActor { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00042FDD File Offset: 0x000411DD
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00042FE5 File Offset: 0x000411E5
		[DefaultValue(true)]
		public bool ValidateAudience { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00042FEE File Offset: 0x000411EE
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x00042FF6 File Offset: 0x000411F6
		[DefaultValue(true)]
		public bool ValidateIssuer { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00042FFF File Offset: 0x000411FF
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x00043007 File Offset: 0x00041207
		[DefaultValue(false)]
		public bool ValidateWithLKG { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00043010 File Offset: 0x00041210
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x00043018 File Offset: 0x00041218
		[DefaultValue(false)]
		public bool ValidateIssuerSigningKey { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00043021 File Offset: 0x00041221
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x00043029 File Offset: 0x00041229
		[DefaultValue(true)]
		public bool ValidateLifetime { get; set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00043032 File Offset: 0x00041232
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x0004303A File Offset: 0x0004123A
		[DefaultValue(false)]
		public bool ValidateSignatureLast { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00043043 File Offset: 0x00041243
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0004304B File Offset: 0x0004124B
		[DefaultValue(false)]
		public bool ValidateTokenReplay { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00043054 File Offset: 0x00041254
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0004305C File Offset: 0x0004125C
		public IEnumerable<string> ValidAlgorithms { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00043065 File Offset: 0x00041265
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x0004306D File Offset: 0x0004126D
		public string ValidAudience { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00043076 File Offset: 0x00041276
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0004307E File Offset: 0x0004127E
		public IEnumerable<string> ValidAudiences { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00043087 File Offset: 0x00041287
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x0004308F File Offset: 0x0004128F
		public string ValidIssuer { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00043098 File Offset: 0x00041298
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x000430A0 File Offset: 0x000412A0
		public IEnumerable<string> ValidIssuers { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x000430A9 File Offset: 0x000412A9
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x000430B1 File Offset: 0x000412B1
		public IEnumerable<string> ValidTypes { get; set; }

		// Token: 0x04000692 RID: 1682
		private string _authenticationType;

		// Token: 0x04000693 RID: 1683
		private TimeSpan _clockSkew = TokenValidationParameters.DefaultClockSkew;

		// Token: 0x04000694 RID: 1684
		private string _nameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		// Token: 0x04000695 RID: 1685
		private string _roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x04000696 RID: 1686
		public static readonly string DefaultAuthenticationType = "AuthenticationTypes.Federation";

		// Token: 0x04000697 RID: 1687
		public static readonly TimeSpan DefaultClockSkew = TimeSpan.FromSeconds(300.0);

		// Token: 0x04000698 RID: 1688
		public const int DefaultMaximumTokenSizeInBytes = 256000;
	}
}
