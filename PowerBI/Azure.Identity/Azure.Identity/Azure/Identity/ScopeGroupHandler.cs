using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x0200007B RID: 123
	internal sealed class ScopeGroupHandler : IScopeHandler
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		public static IScopeHandler Current
		{
			get
			{
				return ScopeGroupHandler._currentAsyncLocal.Value;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public ScopeGroupHandler(string groupName)
		{
			this._groupName = groupName;
			ScopeGroupHandler._currentAsyncLocal.Value = this;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		public DiagnosticScope CreateScope(ClientDiagnostics diagnostics, string name)
		{
			if (this.IsGroup(name))
			{
				return diagnostics.CreateScope(name, 0);
			}
			if (this._childScopes == null)
			{
				this._childScopes = new Dictionary<string, ScopeGroupHandler.ChildScopeInfo>();
			}
			this._childScopes[name] = new ScopeGroupHandler.ChildScopeInfo(diagnostics, name);
			return default(DiagnosticScope);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000D01A File Offset: 0x0000B21A
		public void Start(string name, in DiagnosticScope scope)
		{
			if (this.IsGroup(name))
			{
				scope.Start();
				return;
			}
			this._childScopes[name].StartDateTime = DateTime.UtcNow;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000D044 File Offset: 0x0000B244
		public void Dispose(string name, in DiagnosticScope scope)
		{
			if (!this.IsGroup(name))
			{
				return;
			}
			Dictionary<string, ScopeGroupHandler.ChildScopeInfo> childScopes = this._childScopes;
			ScopeGroupHandler.ChildScopeInfo childScopeInfo;
			if (childScopes == null)
			{
				childScopeInfo = null;
			}
			else
			{
				childScopeInfo = childScopes.Values.LastOrDefault((ScopeGroupHandler.ChildScopeInfo i) => i.Exception == null);
			}
			ScopeGroupHandler.ChildScopeInfo childScopeInfo2 = childScopeInfo;
			if (childScopeInfo2 != null)
			{
				ScopeGroupHandler.SucceedChildScope(childScopeInfo2);
			}
			scope.Dispose();
			ScopeGroupHandler._currentAsyncLocal.Value = null;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		public void Fail(string name, in DiagnosticScope scope, Exception exception)
		{
			if (this._childScopes == null)
			{
				scope.Failed(exception);
				return;
			}
			if (this.IsGroup(name))
			{
				if (exception is OperationCanceledException)
				{
					ScopeGroupHandler.FailChildScope(this._childScopes.Values.Last((ScopeGroupHandler.ChildScopeInfo i) => i.Exception == exception));
				}
				else
				{
					foreach (ScopeGroupHandler.ChildScopeInfo childScopeInfo in this._childScopes.Values)
					{
						ScopeGroupHandler.FailChildScope(childScopeInfo);
					}
				}
				scope.Failed(exception);
				return;
			}
			this._childScopes[name].Exception = exception;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000D180 File Offset: 0x0000B380
		private static void SucceedChildScope(ScopeGroupHandler.ChildScopeInfo scopeInfo)
		{
			using (DiagnosticScope diagnosticScope = scopeInfo.Diagnostics.CreateScope(scopeInfo.Name, 0))
			{
				diagnosticScope.SetStartTime(scopeInfo.StartDateTime);
				diagnosticScope.Start();
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		private static void FailChildScope(ScopeGroupHandler.ChildScopeInfo scopeInfo)
		{
			using (DiagnosticScope diagnosticScope = scopeInfo.Diagnostics.CreateScope(scopeInfo.Name, 0))
			{
				diagnosticScope.SetStartTime(scopeInfo.StartDateTime);
				diagnosticScope.Start();
				diagnosticScope.Failed(scopeInfo.Exception);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000D238 File Offset: 0x0000B438
		private bool IsGroup(string name)
		{
			return string.Equals(name, this._groupName, StringComparison.Ordinal);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000D253 File Offset: 0x0000B453
		void IScopeHandler.Start(string name, in DiagnosticScope scope)
		{
			this.Start(name, in scope);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000D25D File Offset: 0x0000B45D
		void IScopeHandler.Dispose(string name, in DiagnosticScope scope)
		{
			this.Dispose(name, in scope);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000D267 File Offset: 0x0000B467
		void IScopeHandler.Fail(string name, in DiagnosticScope scope, Exception exception)
		{
			this.Fail(name, in scope, exception);
		}

		// Token: 0x04000265 RID: 613
		private static readonly AsyncLocal<IScopeHandler> _currentAsyncLocal = new AsyncLocal<IScopeHandler>();

		// Token: 0x04000266 RID: 614
		private readonly string _groupName;

		// Token: 0x04000267 RID: 615
		private Dictionary<string, ScopeGroupHandler.ChildScopeInfo> _childScopes;

		// Token: 0x0200011E RID: 286
		private class ChildScopeInfo
		{
			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001A9E5 File Offset: 0x00018BE5
			public ClientDiagnostics Diagnostics { get; }

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001A9ED File Offset: 0x00018BED
			public string Name { get; }

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001A9F5 File Offset: 0x00018BF5
			// (set) Token: 0x06000604 RID: 1540 RVA: 0x0001A9FD File Offset: 0x00018BFD
			public DateTime StartDateTime { get; set; }

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001AA06 File Offset: 0x00018C06
			// (set) Token: 0x06000606 RID: 1542 RVA: 0x0001AA0E File Offset: 0x00018C0E
			public Exception Exception { get; set; }

			// Token: 0x06000607 RID: 1543 RVA: 0x0001AA17 File Offset: 0x00018C17
			public ChildScopeInfo(ClientDiagnostics diagnostics, string name)
			{
				this.Diagnostics = diagnostics;
				this.Name = name;
			}
		}
	}
}
