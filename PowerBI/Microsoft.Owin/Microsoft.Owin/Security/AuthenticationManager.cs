using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200001F RID: 31
	internal class AuthenticationManager : IAuthenticationManager
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00003E7A File Offset: 0x0000207A
		public AuthenticationManager(IOwinContext context)
		{
			this._context = context;
			this._request = this._context.Request;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003E9C File Offset: 0x0000209C
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00003ECA File Offset: 0x000020CA
		public ClaimsPrincipal User
		{
			get
			{
				IPrincipal user = this._request.User;
				if (user == null)
				{
					return null;
				}
				return (user as ClaimsPrincipal) ?? new ClaimsPrincipal(user);
			}
			set
			{
				this._request.User = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00003ED8 File Offset: 0x000020D8
		internal Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> AuthenticateDelegate
		{
			get
			{
				return this._context.Get<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>("security.Authenticate");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003EEC File Offset: 0x000020EC
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00003F1B File Offset: 0x0000211B
		public AuthenticationResponseChallenge AuthenticationResponseChallenge
		{
			get
			{
				Tuple<string[], IDictionary<string, string>> challenge = this.ChallengeEntry;
				if (challenge == null)
				{
					return null;
				}
				return new AuthenticationResponseChallenge(challenge.Item1, new AuthenticationProperties(challenge.Item2));
			}
			set
			{
				if (value == null)
				{
					this.ChallengeEntry = null;
					return;
				}
				this.ChallengeEntry = Tuple.Create<string[], IDictionary<string, string>>(value.AuthenticationTypes, value.Properties.Dictionary);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00003F44 File Offset: 0x00002144
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00003F87 File Offset: 0x00002187
		public AuthenticationResponseGrant AuthenticationResponseGrant
		{
			get
			{
				Tuple<IPrincipal, IDictionary<string, string>> grant = this.SignInEntry;
				if (grant == null)
				{
					return null;
				}
				return new AuthenticationResponseGrant((grant.Item1 as ClaimsPrincipal) ?? new ClaimsPrincipal(grant.Item1), new AuthenticationProperties(grant.Item2));
			}
			set
			{
				if (value == null)
				{
					this.SignInEntry = null;
					return;
				}
				this.SignInEntry = Tuple.Create<IPrincipal, IDictionary<string, string>>(value.Principal, value.Properties.Dictionary);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00003FB0 File Offset: 0x000021B0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00003FDA File Offset: 0x000021DA
		public AuthenticationResponseRevoke AuthenticationResponseRevoke
		{
			get
			{
				string[] revoke = this.SignOutEntry;
				if (revoke == null)
				{
					return null;
				}
				return new AuthenticationResponseRevoke(revoke, new AuthenticationProperties(this.SignOutPropertiesEntry));
			}
			set
			{
				if (value == null)
				{
					this.SignOutEntry = null;
					this.SignOutPropertiesEntry = null;
					return;
				}
				this.SignOutEntry = value.AuthenticationTypes;
				this.SignOutPropertiesEntry = value.Properties.Dictionary;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000400B File Offset: 0x0000220B
		public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
		{
			return this.GetAuthenticationTypes((AuthenticationDescription _) => true);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004034 File Offset: 0x00002234
		public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
		{
			List<AuthenticationDescription> descriptions = new List<AuthenticationDescription>();
			this.GetAuthenticationTypes(delegate(IDictionary<string, object> rawDescription)
			{
				AuthenticationDescription description = new AuthenticationDescription(rawDescription);
				if (predicate(description))
				{
					descriptions.Add(description);
				}
			}).Wait();
			return descriptions;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004078 File Offset: 0x00002278
		private Task GetAuthenticationTypes(Action<IDictionary<string, object>> callback)
		{
			return this.Authenticate(null, delegate(IIdentity _, IDictionary<string, string> __, IDictionary<string, object> description, object ___)
			{
				callback(description);
			}, null);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000040A8 File Offset: 0x000022A8
		public async Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
		{
			IEnumerable<AuthenticateResult> enumerable = await this.AuthenticateAsync(new string[] { authenticationType });
			return enumerable.SingleOrDefault<AuthenticateResult>();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000040F4 File Offset: 0x000022F4
		public async Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
		{
			List<AuthenticateResult> results = new List<AuthenticateResult>();
			await this.Authenticate(authenticationTypes, new Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>(AuthenticationManager.AuthenticateAsyncCallback), results);
			return results;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004140 File Offset: 0x00002340
		private static void AuthenticateAsyncCallback(IIdentity identity, IDictionary<string, string> properties, IDictionary<string, object> description, object state)
		{
			List<AuthenticateResult> list = (List<AuthenticateResult>)state;
			list.Add(new AuthenticateResult(identity, new AuthenticationProperties(properties), new AuthenticationDescription(description)));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000416C File Offset: 0x0000236C
		public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
		{
			this._context.Response.StatusCode = 401;
			AuthenticationResponseChallenge priorChallenge = this.AuthenticationResponseChallenge;
			if (priorChallenge == null)
			{
				this.AuthenticationResponseChallenge = new AuthenticationResponseChallenge(authenticationTypes, properties);
				return;
			}
			string[] mergedAuthTypes = priorChallenge.AuthenticationTypes.Concat(authenticationTypes).ToArray<string>();
			if (properties != null && properties.Dictionary != priorChallenge.Properties.Dictionary)
			{
				foreach (KeyValuePair<string, string> propertiesPair in properties.Dictionary)
				{
					priorChallenge.Properties.Dictionary[propertiesPair.Key] = propertiesPair.Value;
				}
			}
			this.AuthenticationResponseChallenge = new AuthenticationResponseChallenge(mergedAuthTypes, priorChallenge.Properties);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004238 File Offset: 0x00002438
		public void Challenge(params string[] authenticationTypes)
		{
			this.Challenge(new AuthenticationProperties(), authenticationTypes);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004248 File Offset: 0x00002448
		public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
		{
			AuthenticationResponseRevoke priorRevoke = this.AuthenticationResponseRevoke;
			if (priorRevoke != null)
			{
				string[] filteredSignOuts = priorRevoke.AuthenticationTypes.Where((string authType) => !identities.Any((ClaimsIdentity identity) => identity.AuthenticationType.Equals(authType, StringComparison.Ordinal))).ToArray<string>();
				if (filteredSignOuts.Length < priorRevoke.AuthenticationTypes.Length)
				{
					if (filteredSignOuts.Length == 0)
					{
						this.AuthenticationResponseRevoke = null;
					}
					else
					{
						this.AuthenticationResponseRevoke = new AuthenticationResponseRevoke(filteredSignOuts);
					}
				}
			}
			AuthenticationResponseGrant priorGrant = this.AuthenticationResponseGrant;
			if (priorGrant == null)
			{
				this.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identities), properties);
				return;
			}
			ClaimsIdentity[] mergedIdentities = priorGrant.Principal.Identities.Concat(identities).ToArray<ClaimsIdentity>();
			if (properties != null && properties.Dictionary != priorGrant.Properties.Dictionary)
			{
				foreach (KeyValuePair<string, string> propertiesPair in properties.Dictionary)
				{
					priorGrant.Properties.Dictionary[propertiesPair.Key] = propertiesPair.Value;
				}
			}
			this.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(mergedIdentities), priorGrant.Properties);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000437C File Offset: 0x0000257C
		public void SignIn(params ClaimsIdentity[] identities)
		{
			this.SignIn(new AuthenticationProperties(), identities);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000438C File Offset: 0x0000258C
		public void SignOut(AuthenticationProperties properties, string[] authenticationTypes)
		{
			AuthenticationResponseGrant priorGrant = this.AuthenticationResponseGrant;
			if (priorGrant != null)
			{
				ClaimsIdentity[] filteredIdentities = priorGrant.Principal.Identities.Where((ClaimsIdentity identity) => !authenticationTypes.Contains(identity.AuthenticationType, StringComparer.Ordinal)).ToArray<ClaimsIdentity>();
				if (filteredIdentities.Length < priorGrant.Principal.Identities.Count<ClaimsIdentity>())
				{
					if (filteredIdentities.Length == 0)
					{
						this.AuthenticationResponseGrant = null;
					}
					else
					{
						this.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(filteredIdentities), priorGrant.Properties);
					}
				}
			}
			AuthenticationResponseRevoke priorRevoke = this.AuthenticationResponseRevoke;
			if (priorRevoke == null)
			{
				this.AuthenticationResponseRevoke = new AuthenticationResponseRevoke(authenticationTypes, properties);
				return;
			}
			if (properties != null && properties.Dictionary != priorRevoke.Properties.Dictionary)
			{
				foreach (KeyValuePair<string, string> propertiesPair in properties.Dictionary)
				{
					priorRevoke.Properties.Dictionary[propertiesPair.Key] = propertiesPair.Value;
				}
			}
			string[] mergedAuthTypes = priorRevoke.AuthenticationTypes.Concat(authenticationTypes).ToArray<string>();
			this.AuthenticationResponseRevoke = new AuthenticationResponseRevoke(mergedAuthTypes, priorRevoke.Properties);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000044C8 File Offset: 0x000026C8
		public void SignOut(string[] authenticationTypes)
		{
			this.SignOut(new AuthenticationProperties(), authenticationTypes);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000044D8 File Offset: 0x000026D8
		public async Task Authenticate(string[] authenticationTypes, Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object> callback, object state)
		{
			Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> authenticateDelegate = this.AuthenticateDelegate;
			if (authenticateDelegate != null)
			{
				await authenticateDelegate(authenticationTypes, callback, state);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004533 File Offset: 0x00002733
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00004545 File Offset: 0x00002745
		public Tuple<IPrincipal, IDictionary<string, string>> SignInEntry
		{
			get
			{
				return this._context.Get<Tuple<IPrincipal, IDictionary<string, string>>>("security.SignIn");
			}
			set
			{
				this._context.Set<Tuple<IPrincipal, IDictionary<string, string>>>("security.SignIn", value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004559 File Offset: 0x00002759
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000456B File Offset: 0x0000276B
		public string[] SignOutEntry
		{
			get
			{
				return this._context.Get<string[]>("security.SignOut");
			}
			set
			{
				this._context.Set<string[]>("security.SignOut", value);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000457F File Offset: 0x0000277F
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00004591 File Offset: 0x00002791
		public IDictionary<string, string> SignOutPropertiesEntry
		{
			get
			{
				return this._context.Get<IDictionary<string, string>>("security.SignOutProperties");
			}
			set
			{
				this._context.Set<IDictionary<string, string>>("security.SignOutProperties", value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000045A5 File Offset: 0x000027A5
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000045B7 File Offset: 0x000027B7
		public Tuple<string[], IDictionary<string, string>> ChallengeEntry
		{
			get
			{
				return this._context.Get<Tuple<string[], IDictionary<string, string>>>("security.Challenge");
			}
			set
			{
				this._context.Set<Tuple<string[], IDictionary<string, string>>>("security.Challenge", value);
			}
		}

		// Token: 0x04000040 RID: 64
		private readonly IOwinContext _context;

		// Token: 0x04000041 RID: 65
		private readonly IOwinRequest _request;
	}
}
