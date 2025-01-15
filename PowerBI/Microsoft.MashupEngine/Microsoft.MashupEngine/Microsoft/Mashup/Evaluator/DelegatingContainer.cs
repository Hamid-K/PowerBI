using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C6B RID: 7275
	internal abstract class DelegatingContainer : IContainer, IDisposable
	{
		// Token: 0x0600B53B RID: 46395 RVA: 0x0024CAE8 File Offset: 0x0024ACE8
		public DelegatingContainer(IContainer container)
		{
			this.InnerContainer = container;
		}

		// Token: 0x17002D47 RID: 11591
		// (get) Token: 0x0600B53C RID: 46396 RVA: 0x0024CAF7 File Offset: 0x0024ACF7
		// (set) Token: 0x0600B53D RID: 46397 RVA: 0x0024CAFF File Offset: 0x0024ACFF
		private protected IContainer InnerContainer { protected get; private set; }

		// Token: 0x17002D48 RID: 11592
		// (get) Token: 0x0600B53E RID: 46398 RVA: 0x0024CB08 File Offset: 0x0024AD08
		public virtual int ContainerID
		{
			get
			{
				return this.InnerContainer.ContainerID;
			}
		}

		// Token: 0x17002D49 RID: 11593
		// (get) Token: 0x0600B53F RID: 46399 RVA: 0x0024CB15 File Offset: 0x0024AD15
		public virtual bool IsHealthy
		{
			get
			{
				return this.InnerContainer.IsHealthy;
			}
		}

		// Token: 0x17002D4A RID: 11594
		// (get) Token: 0x0600B540 RID: 46400 RVA: 0x0024CB22 File Offset: 0x0024AD22
		public virtual IFeatureLoggingService Features
		{
			get
			{
				return this.InnerContainer.Features;
			}
		}

		// Token: 0x17002D4B RID: 11595
		// (get) Token: 0x0600B541 RID: 46401 RVA: 0x0024CB2F File Offset: 0x0024AD2F
		public virtual IMessenger Messenger
		{
			get
			{
				return this.InnerContainer.Messenger;
			}
		}

		// Token: 0x0600B542 RID: 46402 RVA: 0x0024CB3C File Offset: 0x0024AD3C
		public bool TryGetAs<T>(out T result) where T : class
		{
			return this.InnerContainer.TryGetAs<T>(out result);
		}

		// Token: 0x0600B543 RID: 46403 RVA: 0x0024CB4A File Offset: 0x0024AD4A
		public virtual void Kill()
		{
			this.InnerContainer.Kill();
		}

		// Token: 0x0600B544 RID: 46404 RVA: 0x0024CB57 File Offset: 0x0024AD57
		public virtual void Dispose()
		{
			if (this.InnerContainer != null)
			{
				this.InnerContainer.Dispose();
				this.InnerContainer = null;
			}
		}
	}
}
