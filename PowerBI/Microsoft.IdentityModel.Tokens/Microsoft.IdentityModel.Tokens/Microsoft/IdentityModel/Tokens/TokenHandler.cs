using System;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200017C RID: 380
	public abstract class TokenHandler
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0004232B File Offset: 0x0004052B
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x00042333 File Offset: 0x00040533
		public virtual int MaximumTokenSizeInBytes
		{
			get
			{
				return this._maximumTokenSizeInBytes;
			}
			set
			{
				if (value >= 1)
				{
					this._maximumTokenSizeInBytes = value;
					return;
				}
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10101: MaximumTokenSizeInBytes must be greater than zero. value: '{0}'", new object[] { LogHelper.MarkAsNonPII(value) })));
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00042370 File Offset: 0x00040570
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x00042378 File Offset: 0x00040578
		[DefaultValue(true)]
		public bool SetDefaultTimesOnTokenCreation { get; set; } = true;

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00042381 File Offset: 0x00040581
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x00042389 File Offset: 0x00040589
		public int TokenLifetimeInMinutes
		{
			get
			{
				return this._defaultTokenLifetimeInMinutes;
			}
			set
			{
				if (value >= 1)
				{
					this._defaultTokenLifetimeInMinutes = value;
					return;
				}
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10104: TokenLifetimeInMinutes must be greater than zero. value: '{0}'", new object[] { LogHelper.MarkAsNonPII(value) })));
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000423C6 File Offset: 0x000405C6
		public virtual Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000423CD File Offset: 0x000405CD
		public virtual Task<TokenValidationResult> ValidateTokenAsync(SecurityToken token, TokenValidationParameters validationParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000423D4 File Offset: 0x000405D4
		public virtual SecurityToken ReadToken(string token)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000423DB File Offset: 0x000405DB
		internal virtual ClaimsIdentity CreateClaimsIdentityInternal(SecurityToken securityToken, TokenValidationParameters tokenValidationParameters, string issuer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400068B RID: 1675
		private int _defaultTokenLifetimeInMinutes = TokenHandler.DefaultTokenLifetimeInMinutes;

		// Token: 0x0400068C RID: 1676
		private int _maximumTokenSizeInBytes = 256000;

		// Token: 0x0400068D RID: 1677
		public static readonly int DefaultTokenLifetimeInMinutes = 60;
	}
}
