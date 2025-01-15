using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthorizeAttribute : AuthorizationFilterAttribute
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004793 File Offset: 0x00002993
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000047A4 File Offset: 0x000029A4
		public string Roles
		{
			get
			{
				return this._roles ?? string.Empty;
			}
			set
			{
				this._roles = value;
				this._rolesSplit = AuthorizeAttribute.SplitString(value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000047B9 File Offset: 0x000029B9
		public override object TypeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000047C1 File Offset: 0x000029C1
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000047D2 File Offset: 0x000029D2
		public string Users
		{
			get
			{
				return this._users ?? string.Empty;
			}
			set
			{
				this._users = value;
				this._usersSplit = AuthorizeAttribute.SplitString(value);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000047E8 File Offset: 0x000029E8
		protected virtual bool IsAuthorized(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			IPrincipal principal = actionContext.ControllerContext.RequestContext.Principal;
			return principal != null && principal.Identity != null && principal.Identity.IsAuthenticated && (this._usersSplit.Length == 0 || this._usersSplit.Contains(principal.Identity.Name, StringComparer.OrdinalIgnoreCase)) && (this._rolesSplit.Length == 0 || this._rolesSplit.Any(new Func<string, bool>(principal.IsInRole)));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000487C File Offset: 0x00002A7C
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (AuthorizeAttribute.SkipAuthorization(actionContext))
			{
				return;
			}
			if (!this.IsAuthorized(actionContext))
			{
				this.HandleUnauthorizedRequest(actionContext);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000048A5 File Offset: 0x00002AA5
		protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, SRResources.RequestNotAuthorized);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000048D5 File Offset: 0x00002AD5
		private static bool SkipAuthorization(HttpActionContext actionContext)
		{
			return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004900 File Offset: 0x00002B00
		internal static string[] SplitString(string original)
		{
			if (string.IsNullOrEmpty(original))
			{
				return AuthorizeAttribute._emptyArray;
			}
			return (from piece in original.Split(new char[] { ',' })
				let trimmed = piece.Trim()
				where !string.IsNullOrEmpty(trimmed)
				select trimmed).ToArray<string>();
		}

		// Token: 0x04000033 RID: 51
		private static readonly string[] _emptyArray = new string[0];

		// Token: 0x04000034 RID: 52
		private readonly object _typeId = new object();

		// Token: 0x04000035 RID: 53
		private string _roles;

		// Token: 0x04000036 RID: 54
		private string[] _rolesSplit = AuthorizeAttribute._emptyArray;

		// Token: 0x04000037 RID: 55
		private string _users;

		// Token: 0x04000038 RID: 56
		private string[] _usersSplit = AuthorizeAttribute._emptyArray;
	}
}
