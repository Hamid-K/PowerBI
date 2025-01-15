using System;
using System.Diagnostics;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D7B RID: 7547
	internal sealed class WorkingSetProcessContainerFactory : IProcessContainerFactory, IContainerFactory, IDisposable
	{
		// Token: 0x0600BB75 RID: 47989 RVA: 0x0025F525 File Offset: 0x0025D725
		public WorkingSetProcessContainerFactory(IProcessContainerFactory containerFactory, int maxWorkingSetInMB)
		{
			this.containerFactory = containerFactory;
			this.maxWorkingSetInMB = maxWorkingSetInMB;
		}

		// Token: 0x0600BB76 RID: 47990 RVA: 0x0025F53B File Offset: 0x0025D73B
		public IContainer CreateContainer()
		{
			return this.CreateProcessContainer();
		}

		// Token: 0x0600BB77 RID: 47991 RVA: 0x0025F544 File Offset: 0x0025D744
		public IProcessContainer CreateProcessContainer()
		{
			IProcessContainer processContainer2;
			using (EvaluatorTracing.CreateTrace("WorkingSetProcessContainerFactory/CreateContainer", null, TraceEventType.Information, null))
			{
				IProcessContainer processContainer = this.containerFactory.CreateProcessContainer();
				processContainer.SetProcessWorkingSetSize(this.maxWorkingSetInMB);
				processContainer2 = new WorkingSetProcessContainerFactory.Container(this, processContainer);
			}
			return processContainer2;
		}

		// Token: 0x0600BB78 RID: 47992 RVA: 0x0025F59C File Offset: 0x0025D79C
		public void Dispose()
		{
			this.containerFactory.Dispose();
		}

		// Token: 0x04005F72 RID: 24434
		private readonly IProcessContainerFactory containerFactory;

		// Token: 0x04005F73 RID: 24435
		private readonly int maxWorkingSetInMB;

		// Token: 0x02001D7C RID: 7548
		private sealed class Container : DelegatingContainer, IProcessContainer, IContainer, IDisposable
		{
			// Token: 0x0600BB79 RID: 47993 RVA: 0x0025F5A9 File Offset: 0x0025D7A9
			public Container(WorkingSetProcessContainerFactory factory, IProcessContainer container)
				: base(container)
			{
				this.factory = factory;
			}

			// Token: 0x0600BB7A RID: 47994 RVA: 0x0025F5BC File Offset: 0x0025D7BC
			public void SetProcessWorkingSetSize(int maxWorkingSetInMB)
			{
				int num = Math.Max(0, Math.Min(this.factory.maxWorkingSetInMB, maxWorkingSetInMB));
				((IProcessContainer)base.InnerContainer).SetProcessWorkingSetSize(num);
			}

			// Token: 0x04005F74 RID: 24436
			private readonly WorkingSetProcessContainerFactory factory;
		}
	}
}
