using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public class TokenIdProviderReference : ITokenIdProvider, IObjectReferenceContainer
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001A2C0 File Offset: 0x000184C0
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0001A2C8 File Offset: 0x000184C8
		public string ObjectName { get; set; }

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001A2D1 File Offset: 0x000184D1
		public void AcquireReferences()
		{
			if (this.m_interface == null)
			{
				this.UpdateReferences();
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001A2E4 File Offset: 0x000184E4
		public void UpdateReferences()
		{
			object @object = SqlClr.ObjectManager.GetObject(this.ObjectName);
			this.m_interface = @object as ITokenIdProvider;
			if (this.m_interface == null)
			{
				throw new Exception(string.Format("The specified object of type '{0}' does not implement ITokenIdProvider.", @object.GetType().ToString()));
			}
			if (this.m_interface is IObjectReferenceContainer)
			{
				(this.m_interface as IObjectReferenceContainer).UpdateReferences();
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001A34E File Offset: 0x0001854E
		public void ReleaseReferences()
		{
			if (this.m_interface is IObjectReferenceContainer)
			{
				(this.m_interface as IObjectReferenceContainer).ReleaseReferences();
			}
			this.m_interface = null;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001A374 File Offset: 0x00018574
		public long MemoryUsage
		{
			get
			{
				if (this.m_interface is IMemoryUsage)
				{
					return (this.m_interface as IMemoryUsage).MemoryUsage;
				}
				return 0L;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001A396 File Offset: 0x00018596
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0001A3B8 File Offset: 0x000185B8
		public long MemoryLimit
		{
			get
			{
				if (this.m_interface is IMemoryLimit)
				{
					return (this.m_interface as IMemoryLimit).MemoryLimit;
				}
				return 0L;
			}
			set
			{
				if (this.m_interface is IMemoryLimit)
				{
					(this.m_interface as IMemoryLimit).MemoryLimit = value;
					return;
				}
				if (value != 0L)
				{
					throw new Exception("Referenced object does not implement IMemoryLimit.");
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001A3E7 File Offset: 0x000185E7
		public bool SupportsGetToken
		{
			get
			{
				return this.m_interface.SupportsGetToken;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public bool TryGetTokenId(StringExtent token, int domainId, out int tokenId)
		{
			return this.m_interface.TryGetTokenId(token, domainId, out tokenId);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001A404 File Offset: 0x00018604
		public int GetOrCreateTokenId(StringExtent token, int domainId)
		{
			return this.m_interface.GetOrCreateTokenId(token, domainId);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001A413 File Offset: 0x00018613
		public int CreateTokenId(int domainId)
		{
			return this.m_interface.CreateTokenId(domainId);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001A421 File Offset: 0x00018621
		public StringExtent GetToken(int token)
		{
			return this.m_interface.GetToken(token);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001A42F File Offset: 0x0001862F
		public bool TryGetToken(int tokenId, out StringExtent token)
		{
			return this.m_interface.TryGetToken(tokenId, out token);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001A43E File Offset: 0x0001863E
		public int GetDomainId(int tokenId)
		{
			return this.m_interface.GetDomainId(tokenId);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001A44C File Offset: 0x0001864C
		public bool IsTemporary(int token)
		{
			return this.m_interface.IsTemporary(token);
		}

		// Token: 0x040001FE RID: 510
		[NonSerialized]
		private ITokenIdProvider m_interface;
	}
}
