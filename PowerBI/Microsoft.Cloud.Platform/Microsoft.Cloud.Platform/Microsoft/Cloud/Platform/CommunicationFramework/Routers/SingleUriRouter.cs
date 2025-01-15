using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework.Routers
{
	// Token: 0x02000477 RID: 1143
	public class SingleUriRouter : Router, IEquatable<SingleUriRouter>
	{
		// Token: 0x0600238C RID: 9100 RVA: 0x00080883 File Offset: 0x0007EA83
		public SingleUriRouter(Uri uri)
			: this(uri, Enumerable.Empty<Type>())
		{
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00080891 File Offset: 0x0007EA91
		public SingleUriRouter(Uri uri, IEnumerable<Type> retryToSameEndpointExceptions)
			: base(true, new RoundRobinRetryPolicy(Enumerable.Empty<Type>(), retryToSameEndpointExceptions))
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Uri>(uri, "uri");
			this.m_uriHolder = new List<Uri> { uri }.AsReadOnly();
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x000808C7 File Offset: 0x0007EAC7
		public override string Identifier
		{
			get
			{
				if (this.m_uriHolder[0].IsAbsoluteUri)
				{
					return this.m_uriHolder[0].OriginalString;
				}
				return this.m_uriHolder[0].ToString();
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000808FF File Offset: 0x0007EAFF
		public override IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<IEnumerable<Uri>>(callback, state, this.m_uriHolder);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00080876 File Offset: 0x0007EA76
		public override IEnumerable<Uri> EndGetEndpoints(IAsyncResult result)
		{
			return ((CompletedAsyncResult<IEnumerable<Uri>>)result).End();
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0008090E File Offset: 0x0007EB0E
		public bool Equals(SingleUriRouter other)
		{
			return other != null && object.Equals(this.m_uriHolder[0], other.m_uriHolder[0]);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x00080932 File Offset: 0x0007EB32
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SingleUriRouter);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00080940 File Offset: 0x0007EB40
		public override int GetHashCode()
		{
			return this.m_uriHolder[0].GetHashCode();
		}

		// Token: 0x04000C66 RID: 3174
		private readonly ReadOnlyCollection<Uri> m_uriHolder;
	}
}
